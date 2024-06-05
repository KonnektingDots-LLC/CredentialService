using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.Common.Mappers.EntityToPDF;
using cred_system_back_end_app.Application.UseCase.Submit.DTO;
using cred_system_back_end_app.Application.UseCase.Submit.DTO.EducationDTOs;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;

namespace cred_system_back_end_app.Application.Common.Mappers.DTOToEntity
{
    public static class Education
    {
        public static IEnumerable<MedicalSchoolEntity> GetMedicalSchoolEntities(SubmitRequestDTO submitData, int providerId)
        {
            var medicalSchools = submitData.Content.EducationAndTraining.MedicalSchool;

            return GetMedicalSchoolEntities(medicalSchools, providerId);
        }

        public static IEnumerable<MedicalSchoolEntity> GetMedicalSchoolEntities(IEnumerable<MedicalSchoolDTO> medicalSchoolDTOs, int providerId)
        {
            return medicalSchoolDTOs.Select(medicalSchool => GetMedicalSchoolEntity(medicalSchool, providerId));
        }

        public static IEnumerable<ProviderEducationInfoEntity> GetProviderInternshipEntities(IEnumerable<InternshipDTO> internships, int providerId)
        {
            return internships.Select(internship => new ProviderEducationInfoEntity
            {
                PublicId = internship.PublicId,
                ProviderId = providerId,
                EducationInfo = GetInternshipEntity(internship),

            });
        }

        //public static IEnumerable<EducationInfoDocumentEntity> GetEducationInfoDocumentIntershipEntities(IEnumerable<InternshipDTO> internships)
        //{
        //    return internships.Select(educationInfoDocument => new EducationInfoDocumentEntity
        //    {
        //        PublicId = educationInfoDocument.PublicId,                
        //        AzureBlobFilename = educationInfoDocument.EvidenceFile.AzureBlobFilename,
        //        EducationInfo = GetInternshipEntity(educationInfoDocument),

        //    });
        //}

        public static IEnumerable<ProviderEducationInfoEntity> GetProviderResidencyEntities(IEnumerable<ResidencyDTO> residencyDTOs, int providerId)
        {
            return residencyDTOs.Select(residency => new ProviderEducationInfoEntity
            {
                PublicId = residency.PublicId,
                ProviderId = providerId,
                EducationInfo = GetResidencyEntity(residency)
            });
        }        
        
        public static IEnumerable<ProviderEducationInfoEntity> GetProviderFellowshipEntities(IEnumerable<FellowshipDTO> fellowshipDTOs, int providerId)
        {
            return fellowshipDTOs.Select(fellowship => new ProviderEducationInfoEntity
            {
                PublicId = fellowship.PublicId,
                ProviderId = providerId,
                EducationInfo = GetFellowshipEntity(fellowship)
            });
        }

        public static IEnumerable<EducationInfoEntity> GetInternshipEntities(SubmitRequestDTO submitData)
        {
            var internships = submitData.Content.EducationAndTraining.Internship;

            return internships.Select(internship => GetInternshipEntity(internship));
        }
        public static IEnumerable<EducationInfoEntity> GetInternshipEntities(IEnumerable<InternshipDTO> internships)
        {
            return internships.Select(internship => GetInternshipEntity(internship));
        }

        public static IEnumerable<EducationInfoEntity> GetResidencyEntities(SubmitRequestDTO submitData)
        {
            var residencies = submitData.Content.EducationAndTraining.Residency;

            return residencies.Select(residency => GetResidencyEntity(residency));
        }

        public static IEnumerable<EducationInfoEntity> GetFellowshipEntities(SubmitRequestDTO submitData)
        {
            var boardCertificates = submitData.Content.EducationAndTraining.Fellowship;

            return boardCertificates.Select(fellowship => GetFellowshipEntity(fellowship));
        }

        public static IEnumerable<BoardSpecialtyEntity> GetBoardSpecialtyEntities(int[] boardSpecialties, BoardEntity boardEntity)
        {
            return boardSpecialties.Select(specialtyId => new BoardSpecialtyEntity
            {
                SpecialtyId = specialtyId,
                Board = boardEntity
            });
        }

        public static BoardEntity GetBoardEntity(BoardCertificateDTO boardData, int providerId)
        {
            var newBoard = new BoardEntity
            {
                PublicId = boardData.PublicId,
                ProviderId = providerId,
                SBCertificateIssuedDate = DateTimeHelper.ParseDate(boardData.IssuedDate),
                SBCertificateExpirationDate = DateTimeHelper.ParseDate(boardData.ExpirationDate),
                EvidenceSubmitted = boardData.CertificateFile != null ? true : false,

            };
            //Document RelationShip
            var newBoardDocument = new BoardDocumentEntity
            {
                
                AzureBlobFilename = boardData.CertificateFile.AzureBlobFilename,
                Board = newBoard,
            };

            newBoard.BoardDocument = newBoardDocument;
            return newBoard;

        }

        #region helpers
        private static EducationInfoEntity GetResidencyEntity(ResidencyDTO residencyData)
        {
            var newEducationInfoEntity = new EducationInfoEntity()
            {
                EducationTypeId = EducationTypes.Residency,
                EducationCompletionDate = DateTimeHelper.ParseDate(residencyData.CompletionDate),
                EducationPeriod = PeriodHelper.GetPeriodEntity(residencyData.Attendance),
                InstitutionName = residencyData.InstitutionName,
                Address = AddressHelper.GetAddressEntity(
                    residencyData.AddressInfo,
                    AddressTypes.Physical),
                ProgramType = residencyData.ProgramType,
                Residency = new ResidencyEntity
                {
                    PostGraduateCompletionDate = DateTimeHelper.ParseDate(residencyData.PostGraduateCompletionDate)
                }
            };

            //Document RelationShip
            var newEducationInfoDocument = new EducationInfoDocumentEntity
            {
                
                AzureBlobFilename = residencyData.EvidenceFile.AzureBlobFilename,
                EducationInfo = newEducationInfoEntity,
            };

            newEducationInfoEntity.EducationInfoDocument = newEducationInfoDocument;
            return newEducationInfoEntity;
        }

        private static EducationInfoEntity GetFellowshipEntity(FellowshipDTO fellowshipDTO)
        {
            var newEducationInfoEntity = new EducationInfoEntity()
            {
                EducationTypeId = EducationTypes.Fellowship,
                EducationCompletionDate = DateTimeHelper.ParseDate(fellowshipDTO.CompletionDate),
                EducationPeriod = PeriodHelper.GetPeriodEntity(fellowshipDTO.Attendance),
                InstitutionName = fellowshipDTO.InstitutionName,
                Address = AddressHelper.GetAddressEntity(
                    fellowshipDTO.AddressInfo,
                    AddressTypes.Physical),
                ProgramType = fellowshipDTO.ProgramType,
            };

            //Document RelationShip
            var newEducationInfoDocument = new EducationInfoDocumentEntity
            {
                
                AzureBlobFilename = fellowshipDTO.EvidenceFile.AzureBlobFilename,
                EducationInfo = newEducationInfoEntity,
            };

            newEducationInfoEntity.EducationInfoDocument = newEducationInfoDocument;
            return newEducationInfoEntity;
        }

        private static MedicalSchoolEntity GetMedicalSchoolEntity(MedicalSchoolDTO medicalSchool, int providerId)
        {
           var newMedicalSchool = new MedicalSchoolEntity
            {
                PublicId = medicalSchool.PublicId,
                ProviderId = providerId,
                Name = medicalSchool.SchoolName,
                Address = AddressHelper.GetAddressEntity(medicalSchool.AddressInfo, AddressTypes.Physical),
                GraduationMonth = medicalSchool.GraduationMonth,
                GraduationYear = medicalSchool.GraduationYear,
                MSSpecialty = medicalSchool.Specialty,
                SpecialtyCompletionDate = DateTimeHelper.ParseDate(medicalSchool.SpecialtyCompletionDate),
                MSSpecialtyDegreeRecieved = medicalSchool.SpecialtyDegree,
                SpecialtyDegree = medicalSchool.SpecialtyDegree,

            };
            //Document RelationShip
            var newMedicalSchoolDocument = new MedicalSchoolDocumentEntity
            {
                
                AzureBlobFilename = medicalSchool.DiplomaFile.AzureBlobFilename,
                MedicalSchool = newMedicalSchool,
            };

            newMedicalSchool.MedicalSchoolDocument = newMedicalSchoolDocument;
            return newMedicalSchool;
        }

        private static EducationInfoEntity GetEducationInfoEntity(MedicalSchoolDTO medicalSchool, int educationType)
        {
            return new EducationInfoEntity()
            {
                EducationTypeId = educationType,
                EducationPeriodId = 0,
                EducationCompletionDate = DateTimeHelper.ParseDate(medicalSchool.SpecialtyCompletionDate),
                InstitutionName = medicalSchool.SchoolName,
                Address = AddressHelper.GetAddressEntity(medicalSchool.AddressInfo, AddressTypes.Physical),
                ProgramType = medicalSchool.Specialty,
                CreatedBy = "qwqwe",
                CreationDate = DateTime.Now
            };
        }

        private static EducationInfoEntity GetInternshipEntity(InternshipDTO internship)
        {
            var newEducationInfoEntity = new EducationInfoEntity()
            {
                EducationTypeId = EducationTypes.Internship,
                EducationPeriod = PeriodHelper.GetPeriodEntity(internship.Attendance),
                InstitutionName = internship.InstitutionName,
                Address = AddressHelper.GetAddressEntity(internship.AddressInfo, AddressTypes.Physical),
                ProgramType = internship.ProgramType,
            };

            //Document RelationShip
            var newEducationInfoDocument = new EducationInfoDocumentEntity
            {
                
                AzureBlobFilename = internship.EvidenceFile.AzureBlobFilename,
                EducationInfo = newEducationInfoEntity,
            };

            newEducationInfoEntity.EducationInfoDocument = newEducationInfoDocument;
            return newEducationInfoEntity;
          
        }
        #endregion
    }
}

