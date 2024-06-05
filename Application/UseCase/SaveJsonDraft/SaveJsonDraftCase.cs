using AutoMapper;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.CRUD.MedicalGroup.DTO;
using cred_system_back_end_app.Application.CRUD.ProviderDraft.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Infrastructure.FileSystem.MultiFileUpload;
using cred_system_back_end_app.Infrastructure.FileSystem.MultiFileUpload.DTO;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Nodes;

namespace cred_system_back_end_app.Application.UseCase.SaveJsonDraft
{
    public class SaveJsonDraftCase
    {
        private readonly DbContextEntity _context;
        private readonly IMapper _mapper;
        private string _email;
        


        public SaveJsonDraftCase(DbContextEntity context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task SetEmail(string email)
        {
            _email = email;
        }
        public async Task<Empty> SaveJsonProvider(ProviderDraftDto request)
        {
            Empty response = new();
            try
            {
                var todayDate = DateTime.Now;
                var newProviderDraftHistory = _mapper.Map<JsonProviderFormHistoryEntity>(request);
                newProviderDraftHistory.CreationDate = todayDate;
                newProviderDraftHistory.CreatedBy = _email; //Get from B2C

                var existingJsonRecord = await _context.JsonProviderForm.Where(x => x.ProviderId == request.ProviderId).FirstOrDefaultAsync();


                if (existingJsonRecord == null)
                {
                    //Add New
                    var newProviderDraft = _mapper.Map<JsonProviderFormEntity>(request);
                    newProviderDraft.CreatedBy = _email; //Get from B2C
                    newProviderDraft.CreationDate = todayDate;
                    await _context.JsonProviderForm.AddAsync(newProviderDraft);
                }
                else
                {

                    //Update
                    existingJsonRecord.ModifiedDate = todayDate;
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


    }
}
