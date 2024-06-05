using AutoMapper;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionList;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.Common.ResponseTO;
using cred_system_back_end_app.Application.CRUD.Provider.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace cred_system_back_end_app.Application.CRUD.Provider
{
    public class ProviderRepository
    {
        private readonly DbContextEntity _context;
        private readonly IMapper _mapper;
        private string _email;
        //private readonly DelegateCase _delegateCase;

        public ProviderRepository(DbContextEntity context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            // _delegateCase = delegateCase;
        }

        public ProviderRepository(DbContextEntity context)
        {
            _context = context;
        }

        public List<ProviderResponseDto> GetAllProviders()
        {
            var providers = _context.Provider.ToList();
            return _mapper.Map<List<ProviderResponseDto>>(providers);
        }

        public async Task<(List<ProviderEntity>, int)> GetAllProviders(int offset, int limit, string? search)
        {
            int providerCount = 0;
            List<ProviderEntity> providers;

            if (search.IsNullOrEmpty())
            {
                providers = await _context.Provider
                            .Include(r => r.CredForm)
                            .ThenInclude(c => c.CredFormStatusType)
                            .OrderBy(o => o.CredForm.CredFormStatusType.PrioritySorting)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();
               providerCount = _context.Provider.Count();
            }
            else
            {
                var searchFullname = search.Replace(" ","");
                bool IsAllDigits(string search) => search.All(char.IsDigit);
                if (!IsAllDigits(search))
                {
                    providers = await _context.Provider
                           .Where(p => (p.FirstName+ p.MiddleName + p.LastName + p.SurName)
                           .Contains(searchFullname))
                           .Include(r => r.CredForm)
                           .ThenInclude(c => c.CredFormStatusType)
                           .OrderBy(o => o.CredForm.CredFormStatusType.PrioritySorting)
                           .Skip(offset)
                           .Take(limit)
                           .ToListAsync();

                    providerCount = _context.Provider
                            .Where(p => (p.FirstName + p.MiddleName + p.LastName + p.SurName)
                            .Contains(searchFullname))
                            .Count();
                }
                else
                {
                    providers = await _context.Provider
                            .Where(p => p.BillingNPI == search || p.RenderingNPI == search)
                            .Include(r => r.CredForm)
                            .ThenInclude(c => c.CredFormStatusType)
                            .OrderBy(o => o.CredForm.CredFormStatusType.PrioritySorting)
                            .Skip(offset)
                            .Take(limit)
                            .ToListAsync();


                    providerCount = _context.Provider
                            .Where(p => p.BillingNPI == search || p.RenderingNPI == search)                           
                            .Count();
                }

            }
            return (providers, providerCount);
        }

        public ProviderResponseDto? GetProviderById(int id)
        {

            var provider = _context.Provider.Find(id);
            if (provider == null)
            {

                throw new ProviderNotFoundException();
            }

            return _mapper.Map<ProviderResponseDto>(provider);
        }        
        
        public ProviderEntity GetProviderEntityById(int id)
        {
            try
            {
                return _context.Provider.Find(id);

            }catch (Exception ex)
            {
                throw new Exception();
            }
        }        
        
        public async Task<(IEnumerable<ProviderEntity>, int)> GetProvidersByInsurerCompany(string insurerId, int offset, int limit,string? search)
        {
            try
            {

                var acceptedPlanIds = await _context.InsurerCompany
                        .Include(ic => ic.AcceptedPlans)
                        .Where(ic => ic.Id == insurerId)
                        .SelectMany(ic => ic.AcceptedPlans.Select(ap => ap.Id))
                        .ToListAsync();

                int providerCount = 0;
                List<ProviderEntity> providers;

                if (search.IsNullOrEmpty())
                {
                    providers = await _context.ProviderPlanAccept
                        .Where(pa => acceptedPlanIds.Contains(pa.PlanAcceptListId))
                        .Select(pa => pa.Provider)
                        .Distinct()
                        .Skip(offset)
                        .Take(limit)
                        .ToListAsync();

                    providerCount = await _context.ProviderPlanAccept
                        .Where(pa => acceptedPlanIds.Contains(pa.PlanAcceptListId))
                        .Select(x => x.Provider)
                        .Distinct()
                        .CountAsync();
                }
                else
                {
                    bool IsAllDigits(string search) => search.All(char.IsDigit);
                    if (!IsAllDigits(search))
                    {
                        var searchFullname = search.Replace(" ", "");
                        providers = await _context.ProviderPlanAccept
                                .Where(pa => acceptedPlanIds.Contains(pa.PlanAcceptListId))
                                .Select(p => p.Provider).Where(p => (p.FirstName +  p.MiddleName +  p.LastName +  p.SurName)
                                .Contains(searchFullname))
                                .Distinct()
                                .Skip(offset)
                                .Take(limit)
                                .ToListAsync();

                        providerCount = await _context.ProviderPlanAccept
                        .Where(pa => acceptedPlanIds.Contains(pa.PlanAcceptListId))
                        .Select(p => p.Provider).Where(p => (p.FirstName + p.MiddleName + p.LastName + p.SurName)
                        .Contains(searchFullname))
                        .Distinct()
                        .CountAsync();
                    }
                    else
                    {

                        providers = await _context.ProviderPlanAccept
                                 .Where(pa => acceptedPlanIds.Contains(pa.PlanAcceptListId))
                                 .Select(p => p.Provider).Where(p => p.BillingNPI == search || p.RenderingNPI == search)
                                 .Distinct()
                                 .Skip(offset)
                                 .Take(limit)
                                 .ToListAsync();

                        providerCount = await _context.ProviderPlanAccept
                        .Where(pa => acceptedPlanIds.Contains(pa.PlanAcceptListId))
                        .Select(p => p.Provider).Where(p => p.BillingNPI == search || p.RenderingNPI == search)
                        .Distinct()
                        .CountAsync();
                    }

                }

                return (providers, providerCount);

            }catch (Exception ex)
            {
                throw new Exception();
            }
        }

        //public CreatedProviderResponseDto CreateProvider([FromBody] CreateProviderDto createDto)
        //{
        //    using (var dbContextTransaction = _context.Database.BeginTransaction())
        //    {
        //        var providerTypeFound = _context.ProviderType.Where(pt => pt.Id == createDto.ProviderTypeId).FirstOrDefault();

        //        if (providerTypeFound == null)
        //        {
        //            throw new ProviderTypeNotFoundException();
        //        }

        //        //Email validation, Provider Email must be queal to CredFormEmail
        //        var credForm = _context.CredForm.Find(createDto.CredFormId);
        //        if (credForm.Email != createDto.Email) 
        //        {
        //            throw new EmailFailedException();
        //        }

        //        var newProvider = _mapper.Map<ProviderEntity>(createDto);
        //        newProvider.CreatedBy = _email;//Get from B2C                

        //        _context.Provider.Add(newProvider);
        //        _context.SaveChanges();

        //        // Commit the transaction
        //        dbContextTransaction.Commit();

        //        var newProviderResponse = _mapper.Map<CreatedProviderResponseDto>(newProvider);


        //        return newProviderResponse;
        //    }
        //}

        public ProviderResponseDto UpdateProvider(ProviderEntity providerEntity)
        {
            _context.Entry(providerEntity).State = EntityState.Modified;
            _context.SaveChanges();

            if (providerEntity == null)
            {
                throw new ProviderNotFoundException();
            }

            var updateProviderResponse = _mapper.Map<ProviderResponseDto>(providerEntity);

            return updateProviderResponse;
        }

        public void DeleteProvider(int id)
        {
            var deleteProvider = _context.Provider.Find(id);
            if (deleteProvider == null)
            {
                throw new ProviderNotFoundException();
            }

            _context.Provider.Remove(deleteProvider);
            _context.SaveChanges();


        }

        public List<ProviderByDelegateResponseDto> GetProvidersByDelegate(int delegateId)
        {
            //var providers = _context.Provider.ToList();
            //return _mapper.Map<List<ProviderResponseDto>>(providers);

            return new List<ProviderByDelegateResponseDto>();
        }

        public List<ProviderTypeResponseDto> GetAllProviderTypes()
        {
            var providerType = _context.ProviderType.ToList();
            if (providerType.Count == 0) { throw new ProviderTypeNotFoundException(); };
            return _mapper.Map<List<ProviderTypeResponseDto>>(providerType);
        }
        public List<ListResponseDto> GetAllAcceptPlanList()
        {
            var planAccept = _context.PlanAcceptList.ToList();
            if (planAccept.Count == 0) { throw new PlanAcceptNotFoundException(); };
            return _mapper.Map<List<ListResponseDto>>(planAccept);
        }

        public ProviderResponseDto? GetProviderByEmail(string email)
        {

            var provider = _context.Provider.Where(p => p.Email == email).FirstOrDefault();
            if (provider == null)
            {

                throw new ProviderNotFoundException();
            }

            return _mapper.Map<ProviderResponseDto>(provider);


        }

        public ProviderCredFormResponseDto? GetProviderByCredFormId(int credFormId)
        {

            var provider = _context.Provider.Where(p => p.CredFormId == credFormId).Include(cf => cf.CredForm).FirstOrDefault();
            if (provider == null)
            {

                throw new ProviderNotFoundException();
            }
            var providerCredFormResponse = _mapper.Map<ProviderCredFormResponseDto>(provider);
            providerCredFormResponse.Version = provider.CredForm.Version;
            providerCredFormResponse.ProviderStatus = provider.CredForm.CredFormStatusTypeId;

            return providerCredFormResponse;
        }

        //TODO: ProviderDetail should have its own repo?
        public async Task<ProviderDetailEntity> GetProviderDetailByProviderId(int providerId) 
        {
            return await _context.ProviderDetail
                .Where(p => p.ProviderId == providerId)
                .FirstOrDefaultAsync();
        }

        public void SetEmail(string email)
        {
            _email = email;
        }

        public async Task<(IEnumerable<ProviderInsurerCompanyStatusEntity>, int)> 
            GetProviderInsurerCompanyStatusByInsurerCompany(string insurerCompanyId, int offset, int limit, string? search)
        {
            try
            {

                int picsCount = 0;
                List<ProviderInsurerCompanyStatusEntity> providerInsurerCompanyStatus;

                if (search.IsNullOrEmpty())
                {
                    providerInsurerCompanyStatus = await _context.ProviderInsurerCompanyStatus
                        .Where(pics => pics.InsurerCompanyId == insurerCompanyId)
                        .Include(pics => pics.Provider)
                        .Include(pics => pics.InsurerStatusType)
                        .OrderBy(pics => pics.InsurerStatusType.PrioritySorting)
                        .Skip(offset)
                        .Take(limit)
                        .ToListAsync();

                    picsCount = await _context.ProviderInsurerCompanyStatus
                        .Where(pics => pics.InsurerCompanyId == insurerCompanyId)
                        .CountAsync();
                }
                else
                {
                    bool IsAllDigits(string search) => search.All(char.IsDigit);
                    if (!IsAllDigits(search))
                    {
                        var searchFullname = search.Replace(" ", "");

                        providerInsurerCompanyStatus = await _context.ProviderInsurerCompanyStatus
                        .Where(pics => pics.InsurerCompanyId == insurerCompanyId 
                            && (pics.Provider.FirstName + pics.Provider.MiddleName + pics.Provider.LastName + pics.Provider.SurName)
                            .Contains(searchFullname))
                        .Include(pics => pics.Provider)
                        .Include(pics => pics.InsurerStatusType)
                        .OrderBy(pics => pics.InsurerStatusType.PrioritySorting)
                        .Skip(offset)
                        .Take(limit)
                        .ToListAsync();

                        picsCount = await _context.ProviderInsurerCompanyStatus
                        .Where(pics => pics.InsurerCompanyId == insurerCompanyId
                            && (pics.Provider.FirstName + pics.Provider.MiddleName + pics.Provider.LastName + pics.Provider.SurName)
                            .Contains(searchFullname))
                        .CountAsync();
                    }
                    else
                    {

                        providerInsurerCompanyStatus = await _context.ProviderInsurerCompanyStatus
                        .Where(pics => pics.InsurerCompanyId == insurerCompanyId
                            && (pics.Provider.BillingNPI == search || pics.Provider.RenderingNPI == search))                           
                        .Include(pics => pics.Provider)
                        .Include(pics => pics.InsurerStatusType)
                        .OrderBy(pics => pics.InsurerStatusType.PrioritySorting)
                        .Skip(offset)
                        .Take(limit)
                        .ToListAsync();

                        picsCount = await _context.ProviderInsurerCompanyStatus
                        .Where(pics => pics.InsurerCompanyId == insurerCompanyId
                            && (pics.Provider.BillingNPI == search || pics.Provider.RenderingNPI == search))
                        .CountAsync();
                    }

                }

                return (providerInsurerCompanyStatus, picsCount);

            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public ProviderEntity setProviderEntity(CreateProviderDto createProviderDto)
        {
            return _mapper.Map<ProviderEntity>(createProviderDto);
        }
    }
}
