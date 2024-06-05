using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Application.Common.Mappers.DTOToEntity
{
    public static class Attestation
    {
        public static AttestationEntity GetAttestationEntity(SubmitRequestDTO submitDto)
        {
            var attestationData = submitDto.Content.Attestation;

            return new AttestationEntity
            {
                AttestationTypeId = 2,
                IsAccept = attestationData.IsAccept,
                ProviderId = submitDto.Content.Setup.ProviderId,
                AttestationDate = DateTimeHelper.ParseDate(attestationData.AttestationDate),
            };
        }
    }
}
