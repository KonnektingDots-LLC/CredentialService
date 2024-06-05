using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionList;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.CRUD.ProviderDraft.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.CRUD.ProviderDraft
{
    public class ProviderDraftCase
    {
        private readonly DbContextEntity _context;
        private readonly IMapper _mapper;
        private string _email;


        public ProviderDraftCase(DbContextEntity context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task SetEmail(string email)
        {
            _email = email;
        }

        //SaveAndNextJsonProviderDraft
        public async Task<Empty> SaveJsonProvider(ProviderDraftDto request)
        {
            Empty response = new();
            try
            {
                var creationDate = DateTime.Now;
                var newProviderDraft = _mapper.Map<JsonProviderFormEntity>(request);
                newProviderDraft.CreatedBy = _email; //Get from B2C
                var newProviderDraftHistory = _mapper.Map<JsonProviderFormHistoryEntity>(request);
                newProviderDraft.CreationDate = creationDate;
                newProviderDraftHistory.CreationDate = creationDate;
                newProviderDraftHistory.CreatedBy = _email; //Get from B2C


                var existingJsonRecord = await _context.JsonProviderForm.Where(x => x.ProviderId == request.ProviderId).FirstOrDefaultAsync();


                if (existingJsonRecord == null)
                {
                    //Add New
                    await _context.JsonProviderForm.AddAsync(newProviderDraft);
                }
                else
                {

                    //Update
                    existingJsonRecord.ModifiedDate = creationDate;
                    existingJsonRecord.ProviderId = request.ProviderId;
                    existingJsonRecord.ModifiedBy = _email; //Get from B2C
                    existingJsonRecord.JsonBody = request.JsonBody;
                    _context.Entry(existingJsonRecord).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                }

                await _context.JsonProviderFormHistory.AddAsync(newProviderDraftHistory);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return response;
        }

        public async Task<ProviderDraftResponseDto> GetJsonProvider(int providerId)
        {
            ProviderDraftResponseDto jsonProvider = new();


            var existingJsonRecord = await _context.JsonProviderForm.Where(x => x.ProviderId == providerId).FirstOrDefaultAsync();


            if (existingJsonRecord == null)
            {
                throw new ProviderNotFoundException();
            }

            jsonProvider = _mapper.Map<ProviderDraftResponseDto>(existingJsonRecord);


            return jsonProvider;
        }

        public async Task<bool> IsProviderCompleted(int providerId)
        {           
            var existingJsonRecord = await _context.JsonProviderForm.Where(x => x.ProviderId == providerId).FirstOrDefaultAsync();

            if (existingJsonRecord == null)
            {
                throw new ProviderNotFoundException();
            }         

            return existingJsonRecord.IsCompleted;
        }

        public async Task ModifyJsonProvider(string json,int providerId, string modifiedBy)
        {
            var modifiedDate = DateTime.Now;
            var existingJsonRecord = await _context.JsonProviderForm.Where(x => x.ProviderId == providerId).FirstOrDefaultAsync();


            if (existingJsonRecord == null)
            {
                throw new EntityNotFoundException();
            }
            else
            {

                //Update
                //existingJsonRecord.ModifiedDate = modifiedDate;
                //existingJsonRecord.ProviderId = providerId;
                //existingJsonRecord.ModifiedBy = modifiedBy;
                existingJsonRecord.JsonBody = json;
                _context.Entry(existingJsonRecord).State = EntityState.Modified;
                //await _context.SaveChangesAsync();

            }


            JsonProviderFormHistoryEntity newProviderDraftHistory = new JsonProviderFormHistoryEntity()
            {
                ProviderId = providerId,
                JsonBody = json,
                CreatedBy = modifiedBy,
                CreationDate = modifiedDate,
            };


            await _context.JsonProviderFormHistory.AddRangeAsync(newProviderDraftHistory);
            await _context.SaveChangesAsync();

        }       

    }
}
