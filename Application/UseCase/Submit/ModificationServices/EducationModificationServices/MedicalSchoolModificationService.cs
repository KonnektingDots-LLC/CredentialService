using AutoMapper;
using cred_system_back_end_app.Application.Common.EqualityComparers;
using cred_system_back_end_app.Application.CRUD.Document;
using cred_system_back_end_app.Application.UseCase.Submit.DTO.EducationDTOs;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.UseCase.Submit.ResubmitServices.EducationResubmitServices
{
    public class MedicalSchoolModificationService : EntityModificationServiceBase
    {
        private readonly DbContextEntity _dbContextEntity;
        private readonly IMapper _mapper;
        private readonly DocumentCase _documentCase;

        public MedicalSchoolModificationService
        (
            DbContextEntity dbContextEntity,
            IMapper mapper,
            DocumentCase documentCase
        ) : base(dbContextEntity, mapper)
        {
            _dbContextEntity = dbContextEntity;
            _mapper = mapper;
            _documentCase = documentCase;
        }

        public async Task Modify(int providerId, IEnumerable<MedicalSchoolDTO> medicalSchoolDTOs)
        {
            var medicalSchoolComprarer = new ListMemberEntityComparer<MedicalSchoolEntity>();

            foreach (var medicalSchoolDto in medicalSchoolDTOs)
            {
                var documentLocationIntership = _documentCase.GetDocumentLocationEntityByProviderIdDocTypeFilename(
                                                                providerId,
                                                                medicalSchoolDto.DiplomaFile.DocumentTypeId,
                                                                medicalSchoolDto.DiplomaFile.Name);

                medicalSchoolDto.DiplomaFile.AzureBlobFilename = documentLocationIntership?.AzureBlobFilename;
            }
            var newMedicalSchools = Common.Mappers.DTOToEntity.Education
                .GetMedicalSchoolEntities(medicalSchoolDTOs, providerId);

            var oldMedicalSchools = _dbContextEntity.MedicalSchool
                .Where(m => m.ProviderId == providerId)
                .Include(m => m.MedicalSchoolDocument)
                .Include(m => m.Address);

            await ModifyList
            (
                oldMedicalSchools, 
                newMedicalSchools, 
                newMedicalShool => UpdateListMember(newMedicalShool, oldMedicalSchools), 
                newMedicalSchool => AddListMember(newMedicalSchool), 
                medicalSchoolsToDelete => RemoveListMembers(medicalSchoolsToDelete)
            );
        }
    }
}
