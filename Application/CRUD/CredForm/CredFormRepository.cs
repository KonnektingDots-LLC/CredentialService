using AutoMapper;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionList;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.Common.ResponseTO;
using cred_system_back_end_app.Application.CRUD.CredForm.DTO;
using cred_system_back_end_app.Application.CRUD.MedicalGroup.DTO;
using cred_system_back_end_app.Application.CRUD.Provider;
using cred_system_back_end_app.Application.CRUD.Provider.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace cred_system_back_end_app.Application.CRUD.CredForm
{
    public class CredFormRepository
    {
        private readonly DbContextEntity _context;
        private readonly IMapper _mapper;
        private readonly ProviderRepository _providerRepo;

        public CredFormRepository(DbContextEntity context, IMapper mapper, ProviderRepository providerRepository)
        {
            _context = context;
            _mapper = mapper;
            _providerRepo = providerRepository;
        }

        public async Task<CredFormResponseDto> GetCredFormByEmail(string email) 
        { 
            var credForm = await _context.CredForm.Where(p => p.Email == email).OrderBy(p => p.Version).LastOrDefaultAsync();
            if (credForm == null)
            {
                throw new EntityNotFoundException();
            }
            var credFormStatus = await _context.CredFormStatusType.Where(cfs => cfs.Id == credForm.CredFormStatusTypeId).FirstOrDefaultAsync();
            var state = await _context.StateType.Where(s => s.Id == credFormStatus.StateTypeId).FirstOrDefaultAsync();
            var credFormResponse = _mapper.Map<CredFormResponseDto>(credForm);
            credFormResponse.State = state.Id;
            return credFormResponse;
        }

        //public async Task<CredFormResponseDto> CreateCredFormVersion(string email,string createdBy)
        //{

        //    if (ExistsCredFormByEmail(email))
        //    {
        //        var credForm = await _context.CredForm.Where(p => p.Email == email).OrderBy(p => p.Version).LastOrDefaultAsync();
        //        var credFormStatus = await _context.CredFormStatusType.Where(cfs => cfs.Id == credForm.CredFormStatusTypeId).FirstOrDefaultAsync();
        //        var state = await _context.StateType.Where(s => s.Id == credFormStatus.StateTypeId).FirstOrDefaultAsync();

        //        if (state.Id == StateType.CLOSED)
        //        {
        //            throw new DeniedNewRecordException();//quitar para la fase 2
        //            //var newCredForm = SaveNewCredForm(email, credForm.Version+1, createdBy);
        //            //var credFormStatus = await _context.CredFormStatusType.Where(cfs => cfs.Id == newCredForm.CredFormStatusTypeId).FirstOrDefaultAsync();
        //            //var state = await _context.StateType.Where(s => s.Id == credFormStatus.StateTypeId).FirstOrDefaultAsync();
        //            //var credFormResponse = _mapper.Map<CredFormResponseDto>(newCredForm);
        //            //credFormResponse.State = state.Id;
        //            //return credFormResponse;
        //        }
        //        else
        //        {
        //            throw new DeniedNewRecordException(); //Not allow create new version

        //        }
        //    }
        //    else
        //    {

        //        var newCredForm = SaveNewCredForm(email,1,createdBy);
        //        var credFormStatus = await _context.CredFormStatusType.Where(cfs => cfs.Id == newCredForm.CredFormStatusTypeId).FirstOrDefaultAsync();
        //        var state = await _context.StateType.Where(s => s.Id == credFormStatus.StateTypeId).FirstOrDefaultAsync();
        //        var credFormResponse = _mapper.Map<CredFormResponseDto>(newCredForm);
        //        credFormResponse.State = state.Id;
        //        return credFormResponse;
        //    }

        //}

        public async Task<CreateCredFormProviderResponseDto> CreateCredFormVersion(string email,CreateProviderDto createProviderDto)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                if (ExistsCredFormByEmail(email)) { throw new DeniedNewRecordException(); }

                var newCredForm = setCredFormEntity(email, 1);
                var credFormStatus = await _context.CredFormStatusType.Where(cfs => cfs.Id == newCredForm.CredFormStatusTypeId).FirstOrDefaultAsync();
                if (credFormStatus == null) { throw new EntityNotFoundException(); }

                var state = await _context.StateType.Where(s => s.Id == credFormStatus.StateTypeId).FirstOrDefaultAsync();
                if (state == null) { throw new EntityNotFoundException(); }

                var newProviderEntity = _providerRepo.setProviderEntity(createProviderDto);
                newProviderEntity.Email = email;
                newProviderEntity.CreatedBy = email;
                newProviderEntity.CredForm = newCredForm;

                _context.Add(newCredForm);
                _context.Add(newProviderEntity);

                await _context.SaveChangesAsync();

                var credFormResponse = _mapper.Map<CredFormResponseDto>(newCredForm);
                credFormResponse.State = state.Id;

                var providerResponse = _mapper.Map<CreatedProviderResponseDto>(newProviderEntity);
                CreateCredFormProviderResponseDto createCredFormProviderResponseDto = new CreateCredFormProviderResponseDto()
                {
                    CredFormResponse = credFormResponse,
                    CreatedProviderResponse = providerResponse
                };
                 //Commit the transaction
                 dbContextTransaction.Commit();
                return createCredFormProviderResponseDto;
            }

        }

        public bool ExistsCredFormByEmail(string email)
        {
            return _context.CredForm.Where(p => p.Email == email).Any();
        }


        public CredFormEntity setCredFormEntity(string email, int version)
        {                
            return new CredFormEntity
            {
                Email = email,
                Version = version,
                CredFormStatusTypeId = StatusType.DRAFT,
                CreatedBy = email,
            };
        }


        public async Task SetStatusAndSave(SetCredFormStatusDto setCredFormStatus)
        {
            var credForm = await SetStatus(setCredFormStatus);

            await _context.UpdateAsync(credForm);
            await _context.SaveChangesAsync();
        }

        public async Task<CredFormEntity> SetStatus(SetCredFormStatusDto setCredFormStatus)
        {
            var credForm = await _context.CredForm.Where(r => r.Id == setCredFormStatus.Id).FirstOrDefaultAsync();
            credForm.CredFormStatusTypeId = setCredFormStatus.Status;
            credForm.ModifiedBy = setCredFormStatus.ModifiedBy;
            credForm.ModifiedDate = DateTime.Now;

            return credForm;
        }

        public async Task SetStatus(int providerId, string statusId)
        {
            var credForm =
                (
                    await _context.Provider
                    .Where(p => p.Id == providerId)
                    .Include(p => p.CredForm)
                    .FirstOrDefaultAsync()
                ).CredForm;
                

            credForm.CredFormStatusTypeId = statusId;

            //await _context.UpdateAsync(credForm);
            //await _context.SaveChangesAsync();
      
        }

        public async Task SetModify(int providerId, string modifiedBy, DateTime modifiedDate)
        {
            var credForm =
                (
                    await _context.Provider
                    .Where(p => p.Id == providerId)
                    .Include(p => p.CredForm)
                    .FirstOrDefaultAsync()
                ).CredForm;

            credForm.ModifiedBy = modifiedBy;
            credForm.ModifiedDate = DateTime.Now;

        }

        public async Task SetSubmitDate(int providerId, DateTime submitDate)
        {
            var credForm =
                (
                    await _context.Provider
                    .Where(p => p.Id == providerId)
                    .Include(p => p.CredForm)
                    .FirstOrDefaultAsync()
                ).CredForm;


            credForm.SubmitDate = submitDate;

        }

        public async Task SetReSubmitDate(int providerId, DateTime submitDate)
        {
            var credForm =
                (
                    await _context.Provider
                    .Where(p => p.Id == providerId)
                    .Include(p => p.CredForm)
                    .FirstOrDefaultAsync()
                ).CredForm;


            credForm.ReSubmitDate = submitDate;

        }


        public async Task<CredFormEntity> GetCredForm(int id)
        {
            var credForm = await _context.CredForm.FindAsync(id);
            if (credForm == null) { throw new EntityNotFoundException(); }

            return credForm;
        }

        public async Task<CredFormStatusTypeEntity> GetCredFormStatusType(string id)
        {
            var credFormStatusType = await _context.CredFormStatusType.FindAsync(id);
            if (credFormStatusType == null) { throw new EntityNotFoundException(); }

            return credFormStatusType;
        }

        public async Task<CredFormEntity> GetCredFormByProviderId(int providerId)
        {
            var provider = await _context.Provider
                     .Where(p => p.Id == providerId)
                     .Include(p => p.CredForm)
                     .FirstOrDefaultAsync();

            if (provider == null) { throw new  ProviderNotFoundException(); }
            if (provider.CredForm == null) { throw new EntityNotFoundException(); }

            return provider.CredForm;
               
        }
    }
}
