using cred_system_back_end_app.Application.Common.RequestDto;
using cred_system_back_end_app.Application.UseCase.Insurer.DTO.RegisterProvider;

namespace cred_system_back_end_app.Application.Common.Mappers.DTOToEntity
{
    public static class NamesMapper
    {
        public static Names GetNames(this UpdateAdminBaseDTO updateAdminBaseDTO)
        {
            return new Names
            {
                FirstName = updateAdminBaseDTO.Name,
                LastName = updateAdminBaseDTO.LastName,
                MiddleName = updateAdminBaseDTO.MiddleName,
                Surname = updateAdminBaseDTO.Surname
            };
        }
    }
}
