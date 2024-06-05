using AutoMapper;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.CRUD.CredForm.DTO;
using cred_system_back_end_app.Application.CRUD.MedicalGroup.DTO;
using cred_system_back_end_app.Application.CRUD.Provider.DTO;
using cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatus.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatus
{
    public class ProviderInsurerCompanyStatusRepository
    {
        private readonly DbContextEntity _context;
        private readonly IMapper _mapper;

        public ProviderInsurerCompanyStatusRepository(DbContextEntity context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProviderInsurerCompanyStatusResponseDto> CreateProviderInsurerCompanyStatus(ProviderInsurerCompanyStatusDto providerInsurerCompanyStatus)
        {

            var newProviderInsurerCompanyStatus = _mapper.Map<ProviderInsurerCompanyStatusEntity>(providerInsurerCompanyStatus);
            newProviderInsurerCompanyStatus.InsurerStatusTypeId = StatusType.PENDING;


            _context.ProviderInsurerCompanyStatus.Add(newProviderInsurerCompanyStatus);
            _context.SaveChanges();

            var newProviderInsurerCompanyStatusResponse = _mapper.Map<ProviderInsurerCompanyStatusResponseDto>(newProviderInsurerCompanyStatus);


            return newProviderInsurerCompanyStatusResponse;
        }

        public async Task<ProviderInsurerCompanyStatusEntity> UpdateProviderInsurerCompanyStatus(UpdateProviderInsurerCompanyStatusDto providerInsurerCompanyStatus)
        {
            var insurerStatusType = _context.InsurerStatusType.Where(ist => ist.Id == providerInsurerCompanyStatus.InsurerStatusTypeId).FirstOrDefault();

            if (insurerStatusType == null)
            {
                throw new EntityNotFoundException();
            }

            var UpdateproviderInsurerCompanyStatus = await _context.ProviderInsurerCompanyStatus.Where(r => r.Id == providerInsurerCompanyStatus.Id).FirstOrDefaultAsync();

            UpdateproviderInsurerCompanyStatus.InsurerStatusTypeId = providerInsurerCompanyStatus.InsurerStatusTypeId;
            UpdateproviderInsurerCompanyStatus.CurrentStatusDate = providerInsurerCompanyStatus.CurrentStatusDate;
            UpdateproviderInsurerCompanyStatus.SubmitDate = providerInsurerCompanyStatus.SubmitDate;
            UpdateproviderInsurerCompanyStatus.Comment = providerInsurerCompanyStatus.Comment;
            UpdateproviderInsurerCompanyStatus.CommentDate = providerInsurerCompanyStatus.CommentDate;
            UpdateproviderInsurerCompanyStatus.ModifiedBy = providerInsurerCompanyStatus.ModifiedBy;
            UpdateproviderInsurerCompanyStatus.ModifiedDate = DateTime.Now;

            await _context.UpdateAsync(UpdateproviderInsurerCompanyStatus);
            await _context.SaveChangesAsync();

            return UpdateproviderInsurerCompanyStatus;
        }

        public ProviderInsurerCompanyStatusEntity AddProviderInsurerCompanyStatus(ProviderInsurerCompanyStatusDto providerInsurerCompanyStatus)
        {

            var newProviderInsurerCompanyStatus = _mapper.Map<ProviderInsurerCompanyStatusEntity>(providerInsurerCompanyStatus);
            newProviderInsurerCompanyStatus.InsurerStatusTypeId = StatusType.PENDING;

            //_context.ProviderInsurerCompanyStatus.AddRange(newProviderInsurerCompanyStatus);
            //await _context.SaveChangesAsync();

            return newProviderInsurerCompanyStatus;
        }

        public async Task<ProviderInsurerCompanyStatusEntity> ModifyProviderInsurerCompanyStatus(UpdateProviderInsurerCompanyStatusDto providerInsurerCompanyStatus)
        {
            var insurerStatusType = _context.InsurerStatusType.Where(ist => ist.Id == providerInsurerCompanyStatus.InsurerStatusTypeId).FirstOrDefault();

            if (insurerStatusType == null)
            {
                throw new EntityNotFoundException();
            }

            var UpdateproviderInsurerCompanyStatus = await _context.ProviderInsurerCompanyStatus.Where(r => r.Id == providerInsurerCompanyStatus.Id).FirstOrDefaultAsync();

            UpdateproviderInsurerCompanyStatus.InsurerStatusTypeId = providerInsurerCompanyStatus.InsurerStatusTypeId;
            UpdateproviderInsurerCompanyStatus.CurrentStatusDate = providerInsurerCompanyStatus.CurrentStatusDate;
            UpdateproviderInsurerCompanyStatus.SubmitDate = providerInsurerCompanyStatus.SubmitDate;
            UpdateproviderInsurerCompanyStatus.Comment = providerInsurerCompanyStatus.Comment;
            UpdateproviderInsurerCompanyStatus.CommentDate = providerInsurerCompanyStatus.CommentDate;
            UpdateproviderInsurerCompanyStatus.ModifiedBy = providerInsurerCompanyStatus.ModifiedBy;
            UpdateproviderInsurerCompanyStatus.ModifiedDate = DateTime.Now;

            return UpdateproviderInsurerCompanyStatus;
        }

        public async Task<ProviderInsurerCompanyStatusEntity> GetProviderInsurerCompanyStatusByProviderIdAndInsurerCompanyId(int providerId, string insurerCompanyId)
        {
            var provider = _context.Provider.Where(r => r.Id == providerId).FirstOrDefault();

            if (provider == null)
            {
                throw new EntityNotFoundException();
            }

            var insurerCompany = _context.InsurerCompany.Where(r => r.Id == insurerCompanyId).FirstOrDefault();

            if (insurerCompany == null)
            {
                throw new EntityNotFoundException();
            }

            var providerInsurerCompanyStatusEntity = await _context.ProviderInsurerCompanyStatus
                                                    .Where(r => r.ProviderId == providerId 
                                                            && r.InsurerCompanyId == insurerCompanyId ).FirstOrDefaultAsync();

            if (providerInsurerCompanyStatusEntity == null) { throw new EntityNotFoundException(); }

            return providerInsurerCompanyStatusEntity;
        }

        public async Task<ProviderInsurerCompanyStatusEntity> GetProviderInsurerCompanyStatus(int id)
        {

            var providerInsurerCompanyStatusEntity = await _context.ProviderInsurerCompanyStatus.FindAsync(id);

            if (providerInsurerCompanyStatusEntity == null) { throw new EntityNotFoundException(); }

            return providerInsurerCompanyStatusEntity;
        }

        public async Task<IEnumerable<ProviderInsurerCompanyStatusEntity>> GetInsurerStatusesByProviderIdAsync(int providerId)
        {
            var providerInsurerCompanyStatusEntity = await _context.ProviderInsurerCompanyStatus
                                                                        .Where(r => r.ProviderId == providerId)
                                                                        .Include(r => r.InsurerCompany)
                                                                        .ToListAsync();                                                                   

            if (providerInsurerCompanyStatusEntity == null) 
            { 
                throw new EntityNotFoundException(); 
            }

            return providerInsurerCompanyStatusEntity;
        }

        public async Task<(IEnumerable<ProviderInsurerCompanyStatusEntity>, int total)> GetInsurerStatusesByProviderIdAsync(int providerId, int offset, int limitPerPage)
        {
            var providerInsurerCompanyStatuses = _context.ProviderInsurerCompanyStatus
                                                            .Where(r => r.ProviderId == providerId)
                                                            .Include(r => r.InsurerCompany)
                                                            .Include(r => r.Provider)
                                                            .Include(r => r.InsurerStatusType);
            
            var statusesCount = providerInsurerCompanyStatuses.Count();

            var paginatedStatuses = await providerInsurerCompanyStatuses
                                            .Paginated(offset, limitPerPage)
                                            .OrderBy(p => p.InsurerStatusType.PrioritySorting)
                                            .ToListAsync();

            if (paginatedStatuses.Count == 0)
            {
                throw new EntityNotFoundException();
            }

            return (paginatedStatuses, statusesCount);
        }
    }
}
