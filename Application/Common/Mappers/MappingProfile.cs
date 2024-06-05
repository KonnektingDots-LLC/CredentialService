using AutoMapper;
using cred_system_back_end_app.Application.DTO;
using cred_system_back_end_app.Application.DTO.Documents;
using cred_system_back_end_app.Application.DTO.Requests;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Application.Common.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Provider
            CreateMap<ProviderTypeEntity, ListResponseDto>();
            CreateMap<CreateProviderRequestDto, ProviderEntity>();
            CreateMap<ProviderEntity, ProviderResponseDto>();
            CreateMap<CreateProviderByDelegateDto, PersonDTO>();
            CreateMap<CreateProviderByDelegateDto, CredsDTO>();
            CreateMap<PersonDTO, ProviderEntity>();
            CreateMap<ProviderEntity, ProviderCredFormResponseDto>();
            CreateMap<ProviderEntity, CreatedProviderResponseDto>();
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

            //Delegate
            CreateMap<CreateProviderByDelegateDto, CreateDelegateDto>();
            CreateMap<ProviderDelegateEntity, AddressResponseDto>();
            CreateMap<ProviderDelegateEntity, ProviderDelegateDto>();
            CreateMap<DelegateTypeEntity, DelegateTypeDto>();
            CreateMap<DelegateCompanyEntity, DelegateCompanyDto>();
            CreateMap<DelegateEntity, DelegateResponseDto>();
            CreateMap<CreateDelegateDto, DelegateEntity>();

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
            CreateMap<AddressStateEntity, AddressStateResponseDto>();
            CreateMap<AddressEntity, AddressResponseDto>();
            CreateMap<AddressCountryEntity, ListResponseDto>();
            CreateMap<AddressEntity, AddressEntity>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.CreatedBy, opt => opt.Ignore())
                .ForMember(c => c.CreationDate, opt => opt.Ignore())
                .ForMember(c => c.ModifiedBy, opt => opt.Ignore())
                .ForMember(c => c.ModifiedDate, opt => opt.Ignore());


            //Corporation
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
            CreateMap<SpecialtyListEntity, SpecialtyResponseDto>();

            //SubSpecialtyList
            CreateMap<SubSpecialtyListEntity, SubSpecialtyResponseDto>();

            //Document
            CreateMap<DocumentDetailsDto, DocumentLocationEntity>();
            CreateMap<DocumentLocationEntity, DocumentDetailsResponseDto>();
            CreateMap<DocumentLocationEntity, DocumentByProviderResponseDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DocumentType.Name))
                .ForMember(dest => dest.SpecialtyName, opt => opt.MapFrom(src => src.ProviderSpecialty.SpecialtyList.Name))
                .ForMember(dest => dest.SubSpecialtyName, opt => opt.MapFrom(src => src.ProviderSubSpecialty.SubSpecialtyList.Name));

            //Hospital
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
            CreateMap<MedicalSchoolEntity, MedicalSchoolEntity>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.AddressId, opt => opt.Ignore())
                .ForMember(c => c.CreatedBy, opt => opt.Ignore())
                .ForMember(c => c.CreationDate, opt => opt.Ignore())
                .ForMember(c => c.ModifiedBy, opt => opt.Ignore())
                .ForMember(c => c.ModifiedDate, opt => opt.Ignore());

            //JsonProviderDraft
            CreateMap<ProviderDraftDto, JsonProviderFormEntity>();
            CreateMap<ProviderDraftDto, JsonProviderFormHistoryEntity>();
            CreateMap<JsonProviderFormEntity, CredentialingFormSnapshotResponseDto>();
            CreateMap<JsonProviderFormEntity, JsonProviderFormHistoryEntity>();

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


            //Cred Form
            CreateMap<CredFormEntity, CredFormResponseDto>();

            //ProviderInsurerCompanyStatus
            CreateMap<ProviderInsurerCompanyStatusEntity, ProviderInsurerCompanyStatusResponseDto>();

            //ProviderInsurerCompanyStatusHistory
            CreateMap<ProviderInsurerCompanyStatusHistoryDto, ProviderInsurerCompanyStatusHistoryEntity>();
            CreateMap<ProviderInsurerCompanyStatusHistoryEntity, ProviderInsurerCompanyStatusHistoryResponseDto>();
            CreateMap<ProviderInsurerCompanyStatusHistoryResponseDto, ProviderInsurerCompanyStatusHistoryEntity>();
        }
    }
}
