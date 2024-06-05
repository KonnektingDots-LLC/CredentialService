using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionList;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto;

namespace cred_system_back_end_app.Application.CRUD.Delegate
{
    public class DelegateRepository
    {
        private readonly DbContextEntity _context;
        private readonly IMapper _mapper;

        public DelegateRepository(DbContextEntity context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //        public List<DelegateResponseDto> GetAllDelegates()
        //        {
        //            var delegates = _context.Delegate.ToList();
        //            return _mapper.Map<List<DelegateResponseDto>>(delegates);
        //        }

        public async Task<IEnumerable<DelegateEntity>> GetDelegatesByProviderAsync(int providerId)
        {
            var delegates = await _context.ProviderDelegate
                .Where(p => p.ProviderId == providerId)
                .Select(p => p.Delegate)
                .ToListAsync();

            return delegates;
        }        
        
        public async Task<IEnumerable<ProviderDelegateEntity>> GetProviderDelegatesByProviderId(int providerId)
        {
            var delegates = await _context.ProviderDelegate
                .Where(p => p.ProviderId == providerId)
                .Include(p => p.Delegate)
                .ToListAsync();

            return delegates;
        }

        public async Task<IEnumerable<DelegateEntity>> GetDelegatesByEmailAsync(string email)
        {
            var delegates = await _context.Delegate
                .Where(d => d.Email == email)
                .ToListAsync();

            return delegates;
        }

        public async Task<(ProviderEntity, DelegateEntity)> UpdateProviderDelegateAsync(bool isActive,int DelegateId,string ProviderEmail)
        {
            var provider = await _context.Provider.Where(p => p.Email == ProviderEmail).FirstOrDefaultAsync();
            
            if (provider == null) { throw new ProviderNotFoundException(); }

            var providerDelegate = await _context.ProviderDelegate
                .Where(pd => pd.ProviderId == provider.Id && pd.DelegateId == DelegateId)
                .Include(d => d.Delegate)
                .FirstOrDefaultAsync();

            if (providerDelegate == null)
            {
                throw new AggregateException("No delegate associated with the provider.");
            }

            providerDelegate.IsActive = isActive;

            using var transaction = await _context.Database.BeginTransactionAsync();

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return (provider, providerDelegate.Delegate);
        }

        public async Task<bool> DelegateEmailExistsAsync(string delegateEmail) 
        {
            var delegateEntity = await _context.Delegate
                .Where(d => d.Email == delegateEmail)
                .FirstOrDefaultAsync();

            if (delegateEntity == null) 
            { 
                return false;
            }

            return true;
        }

        public async Task InsertAsync(DelegateEntity delegateEntity)
        {
            await _context.SaveAsTransactionAsync(delegateEntity);
        }

        public async Task<int> UpdateAsync(string email, string fullName)
        {
            var delegateEntity = await _context.Delegate.FirstOrDefaultAsync(d => d.Email == email);

            if (delegateEntity == null) 
            {
                throw new AggregateException("UpdateDelegateError:  delegate not found");
            }
            
            delegateEntity.FullName = fullName;
            delegateEntity.IsActive = true;
            
            await _context.UpdateAsync(delegateEntity);

            return delegateEntity.Id;
        }   

        public async Task InsertAsync(ProviderDelegateEntity providerDelegateEntity)
        {
            await _context.SaveAsTransactionAsync(providerDelegateEntity);
        }

        public async Task InsertIfNotExistsAsync(ProviderDelegateEntity providerDelegateEntity)
        {
            if (await ProviderDelegateExistsAsync(providerDelegateEntity.ProviderId, providerDelegateEntity.DelegateId)) 
            {
                return;
            }

            await _context.SaveAsTransactionAsync(providerDelegateEntity);
        }

        public async Task<bool> ProviderDelegateExistsAsync(int providerId, int delegateId)
        {
            var providerDelegate = await _context.ProviderDelegate
                .Where(d => d.ProviderId == providerId && d.DelegateId == delegateId)
                .FirstOrDefaultAsync();

            if (providerDelegate == null)
            {
                return false;
            }

            return true;
        }

        //        public DelegateResponseDto CreateDelegate([FromBody] CreateDelegateDto createDto)
        //        {
        //            try
        //            {

        //                var newDelegate = _mapper.Map<DelegateEntity>(createDto);
        //                newDelegate.CreationDate = DateTime.Now;

        //                _context.Delegate.Add(newDelegate);
        //                _context.SaveChanges();

        //                var newDelegateResponse = _mapper.Map<DelegateResponseDto>(newDelegate);

        //                return newDelegateResponse;


        //            }
        //            catch (Exception ex) { throw; }
        //        }

        //        public DelegateResponseDto UpdateDelegate(DelegateEntity delegateEntity)
        //        {
        //            _context.Entry(delegateEntity).State = EntityState.Modified;
        //            _context.SaveChanges();

        //            if (delegateEntity == null)
        //            {
        //                throw new EntityNotFoundException();
        //            }

        //            var updateDelegateResponse = _mapper.Map<DelegateResponseDto>(delegateEntity);

        //            return updateDelegateResponse;
        //        }

        //        public void DeleteDelegate(int id)
        //        {
        //            var deleteDelegate = _context.Delegate.Find(id);
        //            if (deleteDelegate == null)
        //            {
        //                throw new EntityNotFoundException();
        //            }

        //            _context.Delegate.Remove(deleteDelegate);
        //            _context.SaveChanges();

        //        }

        //        public List<ProvDelAddrResponseDto> GetProviderByDelegate(int delegateId)
        //        {

        //            var _delegate = _context.Delegate.Find(delegateId);
        //            var _providerDelegate = _context.ProviderDelegate.Include(pd => pd.Provider).Where(e => e.DelegateId == delegateId).ToList();
        //            var _provider = _context.Provider.Include(pd => pd.Address).ToList();
        //            var _addressProvider = _mapper.Map<List<ProvDelAddrResponseDto>>(_providerDelegate);
        //            if (_delegate == null || _providerDelegate == null)
        //            {

        //                throw new EntityNotFoundException();
        //            }

        //            return _addressProvider;


        //        }

        //        public List<ProviderEntity> GetProvidersByNPIDelegate(int delegateId, string NPI)
        //        {

        //            var _delegate = _context.Delegate.Find(delegateId);
        //            var _providerDelegate = _context.ProviderDelegate.Include(pd => pd.Provider).Where(e => e.DelegateId == delegateId).ToList();
        //            var _providerByNPI = _context.Provider.Include(pd => pd.Address).Where(e => e.BillingNPI == NPI || e.RenderingNPI == NPI).ToList();

        //            if (_delegate == null || _providerDelegate == null || _providerByNPI == null)
        //            {

        //                throw new EntityNotFoundException();
        //            }

        //            return _providerByNPI;


        //        }

    }
}
