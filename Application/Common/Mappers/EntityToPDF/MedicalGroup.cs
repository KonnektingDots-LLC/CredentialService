using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Infrastructure.PdfReport.DTO;

namespace cred_system_back_end_app.Application.Common.Mappers.EntityToPDF
{
    public class MedicalGroup
    {
        public static FederalQualifiedHealthCenter330Dto GetF330DTO(MedicalGroupEntity f330Data, string specialty)
        {
            var physicalAddresses = AddressHelper.GetAllAddressesByType(f330Data.Address, AddressTypes.Physical);
            var postalAddresses = AddressHelper.GetAllAddressesByType(f330Data.Address, AddressTypes.Mail);
            var serviceHours = physicalAddresses?.FirstOrDefault()?.AddressServiceHours;

            return new FederalQualifiedHealthCenter330Dto
            {
                F330GroupName = f330Data.Name,
                F330BillingNPI = f330Data.BillingNPI,
                F330TaxIdNumber = f330Data.TaxId,
                F330MedicaidId = f330Data.MedicaidId,
                F330RenderingNPI = f330Data.NPI,
                F330PhysicalAddress = physicalAddresses?.FirstOrDefault()?.GetFormattedAddressString(),
                F330MailAddress = postalAddresses.FirstOrDefault()?.GetFormattedAddressString(),
                F330EndorsementDate = f330Data.EndorsementLetterDate?.ToString(DateFormats.IIPCA_DATE_FROMAT),
                F330ContactPhoneNum = f330Data.ContactPhone,
                F330EmployerIdNum = f330Data.EmployerId_EIN,
                F330Email = f330Data.EmailAddress,
                F330Specialist = specialty,
                F330ProvSpecialty = f330Data.CareType?.Name,
                F330VITALServHours = serviceHours?.GetFormattedServiceHoursString()
            };
        }

        public static PrimaryCarePhysicianPCPDto GetPrimaryCarePhysicianPCPDTO(MedicalGroupEntity pcpData, string specialty)
        {
            var physicalAddresses = AddressHelper.GetAllAddressesByType(pcpData.Address, AddressTypes.Physical);
            var postalAddresses = AddressHelper.GetAllAddressesByType(pcpData.Address, AddressTypes.Mail);
            var serviceHours = physicalAddresses?.FirstOrDefault()?.AddressServiceHours;

            return new PrimaryCarePhysicianPCPDto
            {
                PcpGroupName = pcpData.Name,
                PcpBillingNPI = pcpData.BillingNPI,
                PcpTaxIdNumber = pcpData.TaxId,
                PcpMedicaidId = pcpData.MedicaidId,
                PcpRenderingNPI = pcpData.NPI,
                PcpEndorsementDate = pcpData.EndorsementLetterDate?.ToString(DateFormats.IIPCA_DATE_FROMAT),
                PcpContactPhoneNum = pcpData.ContactPhone,
                PcpEmployerIdNum = pcpData.EmployerId_EIN,
                PcpEmail = pcpData.EmailAddress,
                PcpPhysicalAddress = physicalAddresses?.FirstOrDefault()?.GetFormattedAddressString(),
                PcpMailAddress = postalAddresses.FirstOrDefault()?.GetFormattedAddressString(),
                PcpSpecialist = specialty,
                PcpProvSpecialty = pcpData.CareType?.Name,
                PcpVITALServHours = serviceHours?.GetFormattedServiceHoursString(),
            };
        }

    }
}
