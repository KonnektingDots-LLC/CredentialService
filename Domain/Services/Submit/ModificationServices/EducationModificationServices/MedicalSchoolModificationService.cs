using AutoMapper;
using cred_system_back_end_app.Application.Common.EqualityComparers;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Domain.Services.Submit.DTO.EducationDTOs;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Domain.Services.Submit.ModificationServices.EducationModificationServices
{
    public class MedicalSchoolModificationService : EntityModificationServiceBase
    {
        private readonly DbContextEntity _dbContextEntity;
        private readonly IDocumentLocationRepository _documentLocationRepository;

        public MedicalSchoolModificationService
        (
            DbContextEntity dbContextEntity,
            IMapper mapper,
            IDocumentLocationRepository documentLocationRepository
        ) : base(dbContextEntity, mapper)
        {
            _dbContextEntity = dbContextEntity;
            _documentLocationRepository = documentLocationRepository;
        }

        public async Task Modify(int providerId, IEnumerable<MedicalSchoolDTO> medicalSchoolDTOs)
        {
            var medicalSchoolComprarer = new ListMemberEntityComparer<MedicalSchoolEntity>();

            foreach (var medicalSchoolDto in medicalSchoolDTOs)
            {
                var documentLocationIntership = await _documentLocationRepository.GetByProviderIdAndDocumentTypeAndUploadfilename(
                                                                providerId,
                                                                medicalSchoolDto.DiplomaFile.DocumentTypeId,
                                                                medicalSchoolDto.DiplomaFile.Name)
                    ?? throw new DocumentNotFoundException(providerId, medicalSchoolDto.DiplomaFile.DocumentTypeId, medicalSchoolDto.DiplomaFile.Name);

                medicalSchoolDto.DiplomaFile.AzureBlobFilename = documentLocationIntership?.AzureBlobFilename;
            }
            var newMedicalSchools = Application.Common.Mappers.DTOToEntity.Education
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
