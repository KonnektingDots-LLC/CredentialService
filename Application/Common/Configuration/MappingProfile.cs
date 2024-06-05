using AutoMapper;
using cred_system_back_end_app.Application.Common.RequestDto;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.Common.ResponseTO;
using cred_system_back_end_app.Application.CRUD.Address.DTO;
using cred_system_back_end_app.Application.CRUD.Corporation.DTO;
using cred_system_back_end_app.Application.CRUD.CredForm.DTO;
using cred_system_back_end_app.Application.CRUD.Delegate.DTO;
using cred_system_back_end_app.Application.CRUD.Document.DTO;
using cred_system_back_end_app.Application.CRUD.EdFellowshipInstitution.DTO;
using cred_system_back_end_app.Application.CRUD.EdInternshipInstitution.DTO;
using cred_system_back_end_app.Application.CRUD.EdMedicalSchool.DTO;
using cred_system_back_end_app.Application.CRUD.EdResidencyInstitutionEntity.DTO;
using cred_system_back_end_app.Application.CRUD.Hospital.DTO;
using cred_system_back_end_app.Application.CRUD.Insurance.DTO;
using cred_system_back_end_app.Application.CRUD.Insurer.DTO;
using cred_system_back_end_app.Application.CRUD.Provider.DTO;
using cred_system_back_end_app.Application.CRUD.ProviderDraft.DTO;
using cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatus.DTO;
using cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatusHistory.DTO;
using cred_system_back_end_app.Application.UseCase.Insurer.DTO.CreateInsurerEmployee;
using cred_system_back_end_app.Application.UseCase.Notifications.DTO;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Infrastructure.FileSystem.GetDocument.DTO;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.Common.Constants;

namespace cred_system_back_end_app.Application.Common.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {   
            //Provider
            CreateMap<CreateProviderDto, ProviderEntity>();
            CreateMap<ProviderEntity, ProviderResponseDto>();
            CreateMap<CreateProviderByDelegateDto, PersonDTO>();
            CreateMap<CreateProviderByDelegateDto, CredsDTO>();
            CreateMap<PersonDTO, ProviderEntity>();
            CreateMap<ProviderEntity, ProviderByDelegateResponseDto>();
            CreateMap<ProviderDelegateEntity, ProvDelAddrResponseDto>();
            CreateMap<ProviderEntity, ProvDelAddrResponseDto>();
            CreateMap<ProviderEntity, CreatedProviderResponseDto>();
            CreateMap<ProviderTypeEntity, ProviderTypeResponseDto>();
            CreateMap<PlanAcceptListEntity, ListResponseDto>();
            CreateMap<ProviderAddressEntity, ProviderAddressEntity>()
                .ForMember(c => c.ProviderId, opt => opt.Ignore())
                .ForMember(c => c.AddressId, opt => opt.Ignore())
                .ForMember(c => c.CreatedBy, opt => opt.Ignore())
                .ForMember(c => c.CreationDate, opt => opt.Ignore())
                .ForMember(c => c.ModifiedBy, opt => opt.Ignore())
                .ForMember(c => c.ModifiedDate, opt => opt.Ignore());

            CreateMap<ProviderDetailEntity, ProviderDetailEntity>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.CreatedBy, opt => opt.Ignore())
                .ForMember(c => c.CreationDate, opt => opt.Ignore())
                .ForMember(c => c.ModifiedBy, opt => opt.Ignore())
                .ForMember(c => c.ModifiedDate, opt => opt.Ignore());

            //MultiNPI
            CreateMap<MultipleNPIDTO, MultipleNPIEntity>();


            //Insurer
            CreateMap<CreateInsurerDto, InsurerEntity>();
            CreateMap<InsurerEntity, InsurerResponseDto>();

            //Delegate
            CreateMap<CreateDelegateDto, DelegateEntity>();
            CreateMap<DelegateEntity, DelegateResponseDto>();
            CreateMap<CreateProviderByDelegateDto, CreateDelegateDto>();
            CreateMap<ProviderDelegateEntity, AddressResponseDto>();
            CreateMap<DelegateEntity, UseCase.Delegate.DTO.DelegateResponseDto>();
            CreateMap<UseCase.Delegate.DTO.CreateDelegateDto, DelegateEntity>();
            CreateMap<UseCase.Delegate.DTO.CreateProviderDelegateDto, ProviderDelegateEntity>();


            // PMG

            CreateMap<MedicalGroupEntity, MedicalGroupEntity>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.CreatedBy, opt => opt.Ignore())
                .ForMember(c => c.CreationDate, opt => opt.Ignore())
                .ForMember(c => c.ModifiedBy, opt => opt.Ignore())
                .ForMember(c => c.ModifiedDate, opt => opt.Ignore());
            CreateMap<ProviderMedicalGroupEntity, ProviderMedicalGroupEntity>()
                .ForMember(c => c.ProviderId, opt => opt.Ignore())
                .ForMember(c => c.MedicalGroupId, opt => opt.Ignore())
                .ForMember(c => c.CreatedBy, opt => opt.Ignore())
                .ForMember(c => c.CreationDate, opt => opt.Ignore())
                .ForMember(c => c.ModifiedBy, opt => opt.Ignore())
                .ForMember(c => c.ModifiedDate, opt => opt.Ignore());


            //Address
            CreateMap<AddressTypeEntity, AddressTypeResponseDto>();
            CreateMap<AddressStateEntity, AddressStateResponseDto>();
            CreateMap<CreateAddressDto, AddressEntity>();
            CreateMap<AddressEntity, AddressResponseDto>();
            CreateMap<AddressCountryEntity, ListResponseDto>();
            CreateMap<AddressEntity, AddressEntity>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.CreatedBy, opt => opt.Ignore())
                .ForMember(c => c.CreationDate, opt => opt.Ignore())
                .ForMember(c => c.ModifiedBy, opt => opt.Ignore())
                .ForMember(c => c.ModifiedDate, opt => opt.Ignore());


            //Address Contact
            CreateMap<CreateAddressContactDto, AddressContactEntity>();
            CreateMap<AddressContactEntity, AddressContactResponseDto>();

            //Corporation
            CreateMap<CreateCorporationDto, CorporationEntity>();
            CreateMap<CorporationEntity, CorporationResponseDto>();
            CreateMap<CorporationEntity, CorporationEntity>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.CreatedBy, opt => opt.Ignore())
                .ForMember(c => c.CreationDate, opt => opt.Ignore())
                .ForMember(c => c.ModifiedBy, opt => opt.Ignore())
                .ForMember(c => c.ModifiedDate, opt => opt.Ignore());

            CreateMap<ProviderCorporationEntity, ProviderCorporationEntity>()
                .ForMember(c => c.CorporationId, opt => opt.Ignore())
                .ForMember(c => c.ProviderId, opt => opt.Ignore())
                .ForMember(c => c.CreatedBy, opt => opt.Ignore())
                .ForMember(c => c.CreationDate, opt => opt.Ignore())
                .ForMember(c => c.ModifiedBy, opt => opt.Ignore())
                .ForMember(c => c.ModifiedDate, opt => opt.Ignore());


            //User
            CreateMap<CreateAccountDto, CredsDTO>();
            CreateMap<CreateAccountDto, PersonDTO>();

            //Specialty
            //CreateMap<CreateSpecialtyDto, PSpecialtyEntity>();
            CreateMap<SpecialtyListEntity, SpecialtyResponseDto>();

            ////SpecialtyList
            //CreateMap<CreateSpecialtyListDto, PSpecialtyListEntity>();
            //CreateMap<PSpecialtyListEntity, SpecialtyListResponseDto>();

            ////SubSpecialty
            //CreateMap<CreateSubSpecialtyDto, PSubSpecialtyEntity>();
            //CreateMap<PSubSpecialtyEntity, SubSpecialtyResponseDto>();

            //SubSpecialtyList
            CreateMap<SubSpecialtyListEntity, SubSpecialtyResponseDto>();
            //CreateMap<CreateSubSpecialtyListDto, PSubSpecialtyListEntity>();
            //CreateMap<PSubSpecialtyListEntity, SubSpecialtyListResponseDto>();

            //Document
            CreateMap<DocumentDetailsDto, DocumentLocationEntity>();
            CreateMap<DocumentLocationEntity,DocumentDetailsResponseDto>();
            CreateMap<DocumentLocationEntity, DocumentByProviderResponseDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DocumentType.Name))
                .ForMember(dest => dest.SpecialtyName, opt => opt.MapFrom(src => src.ProviderSpecialty.SpecialtyList.Name))
                .ForMember(dest => dest.SubSpecialtyName, opt => opt.MapFrom(src => src.ProviderSubSpecialty.SubSpecialtyList.Name));

            //Hospital
            CreateMap<CreateHospitalDto, HospitalEntity>();
            CreateMap<HospitalEntity, HospitalResponseDto>();
            CreateMap<HospitalListEntity, HospitalListResponseDto>();
            CreateMap<HospPriviledgeListEntity, HospPrivilegeResponseDto>();
            CreateMap<HospitalEntity, HospitalEntity>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.CreatedBy, opt => opt.Ignore())
                .ForMember(c => c.CreationDate, opt => opt.Ignore())
                .ForMember(c => c.ModifiedBy, opt => opt.Ignore())
                .ForMember(c => c.ModifiedDate, opt => opt.Ignore());
            CreateMap<ProviderHospitalEntity, ProviderHospitalEntity>()
                .ForMember(c => c.HospitalId, opt => opt.Ignore())
                .ForMember(c => c.ProviderId, opt => opt.Ignore())
                .ForMember(c => c.CreatedBy, opt => opt.Ignore())
                .ForMember(c => c.CreationDate, opt => opt.Ignore())
                .ForMember(c => c.ModifiedBy, opt => opt.Ignore())
                .ForMember(c => c.ModifiedDate, opt => opt.Ignore());

            //EdFellowshipInstitution
            CreateMap<CreateEdFellowshipInstitutionDto, EdFellowshipInstitutionEntity>();
            CreateMap<EdFellowshipInstitutionEntity, EdFellowshipInstitutionResponseDto>();

            //EdInternshipInstitution
            CreateMap<CreateEdInternshipInstitutionDto, EdInternshipInstitutionEntity>();
            CreateMap<EdInternshipInstitutionEntity, EdInternshipInstitutionResponseDto>();
            CreateMap<ProviderEducationInfoEntity, ProviderEducationInfoEntity>()
                .ForMember(c => c.EducationInfoId, opt => opt.Ignore())
                .ForMember(c => c.ProviderId, opt => opt.Ignore())
                .ForMember(c => c.CreatedBy, opt => opt.Ignore())
                .ForMember(c => c.CreationDate, opt => opt.Ignore())
                .ForMember(c => c.ModifiedBy, opt => opt.Ignore())
                .ForMember(c => c.ModifiedDate, opt => opt.Ignore());

            CreateMap<EducationInfoEntity, EducationInfoEntity>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.AddressId, opt => opt.Ignore())
                .ForMember(c => c.CreatedBy, opt => opt.Ignore())
                .ForMember(c => c.CreationDate, opt => opt.Ignore())
                .ForMember(c => c.ModifiedBy, opt => opt.Ignore())
                .ForMember(c => c.ModifiedDate, opt => opt.Ignore());

            CreateMap<BoardEntity, BoardEntity>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.ProviderId, opt => opt.Ignore())
                .ForMember(c => c.CreatedBy, opt => opt.Ignore())
                .ForMember(c => c.CreationDate, opt => opt.Ignore())
                .ForMember(c => c.ModifiedBy, opt => opt.Ignore())
                .ForMember(c => c.ModifiedDate, opt => opt.Ignore());

            CreateMap<MedicalLicenseEntity, MedicalLicenseEntity>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.ProviderId, opt => opt.Ignore())
                .ForMember(c => c.CreatedBy, opt => opt.Ignore())
                .ForMember(c => c.CreationDate, opt => opt.Ignore())
                .ForMember(c => c.ModifiedBy, opt => opt.Ignore())
                .ForMember(c => c.ModifiedDate, opt => opt.Ignore());

            //EdMedicalSchool
            CreateMap<CreateEdMedicalSchoolDto, EdMedicalSchoolEntity>();
            CreateMap<EdMedicalSchoolEntity, EdMedicalSchoolResponseDto>();
            CreateMap<MedicalSchoolEntity, MedicalSchoolEntity>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.AddressId, opt => opt.Ignore())
                .ForMember(c => c.CreatedBy, opt => opt.Ignore())
                .ForMember(c => c.CreationDate, opt => opt.Ignore())
                .ForMember(c => c.ModifiedBy, opt => opt.Ignore())
                .ForMember(c => c.ModifiedDate, opt => opt.Ignore());

            //EdResidencyInstitution
            CreateMap<CreateEdResidencyInstitutionDto, EdResidencyInstitutionEntity>();
            CreateMap<EdResidencyInstitutionEntity, EdResidencyInstitutionResponseDto>();

            //JsonProviderDraft
            CreateMap<ProviderDraftDto, JsonProviderFormEntity>();
            CreateMap<ProviderDraftDto, JsonProviderFormHistoryEntity>();
            CreateMap<JsonProviderFormEntity, ProviderDraftResponseDto>();
            CreateMap<JsonProviderFormEntity,JsonProviderFormHistoryEntity>();

            //Insurance
            CreateMap<MalpracticeCarrierListEntity, InsuranceCarrierListResponseDto>();
            CreateMap<ProfessionalLiabilityCarrierListEntity, InsuranceCarrierListResponseDto>();
            CreateMap<MalpracticeEntity, MalpracticeEntity>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.CreatedBy, opt => opt.Ignore())
                .ForMember(c => c.CreationDate, opt => opt.Ignore())
                .ForMember(c => c.ModifiedBy, opt => opt.Ignore())
                .ForMember(c => c.ModifiedDate, opt => opt.Ignore());

            CreateMap<ProfessionalLiabilityEntity, ProfessionalLiabilityEntity>()
            .ForMember(c => c.Id, opt => opt.Ignore())
            .ForMember(c => c.CreatedBy, opt => opt.Ignore())
            .ForMember(c => c.CreationDate, opt => opt.Ignore())
            .ForMember(c => c.ModifiedBy, opt => opt.Ignore())
            .ForMember(c => c.ModifiedDate, opt => opt.Ignore());

            //Insurer
            CreateMap<InsurerEmployeeEntity, CreateInsurerEmployeeResponseDto>();
            CreateMap<CreateInsurerEmployeeRequestDto, InsurerEmployeeEntity>();

            //Notification
            CreateMap<CreateNotificationStatusRequestDto, NotificationStatusEntity>();
            CreateMap<CreateNotificationErrorRequestDto, NotificationErrorEntity>();
            CreateMap<CreateNotificationRequestDto, NotificationEntity>();

            //Cred Form
            CreateMap<CredFormEntity, CredFormResponseDto>();
            CreateMap<ProviderEntity, ProviderCredFormResponseDto>();

            //ProviderInsurerCompanyStatus
            CreateMap<ProviderInsurerCompanyStatusDto, ProviderInsurerCompanyStatusEntity>();
            CreateMap<ProviderInsurerCompanyStatusEntity, ProviderInsurerCompanyStatusResponseDto>();
            CreateMap<ProviderInsurerCompanyStatusEntity, UpdateProviderInsurerCompanyStatusDto>();

            //ProviderInsurerCompanyStatusHistory
            CreateMap<ProviderInsurerCompanyStatusHistoryDto, ProviderInsurerCompanyStatusHistoryEntity>();
            CreateMap<ProviderInsurerCompanyStatusHistoryEntity, ProviderInsurerCompanyStatusHistoryResponseDto>();
            CreateMap<ProviderInsurerCompanyStatusHistoryResponseDto, ProviderInsurerCompanyStatusHistoryEntity>();               
        }
    }
}
