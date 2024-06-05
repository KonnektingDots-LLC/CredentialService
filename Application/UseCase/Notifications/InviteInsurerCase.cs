using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.UseCase.Insurer.DTO.CreateInsurerEmployee;
using cred_system_back_end_app.Application.UseCase.Notifications.DTO;
using cred_system_back_end_app.Infrastructure.B2C;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Infrastructure.Smtp.InsurerInvitationNotification;
using cred_system_back_end_app.Infrastructure.Smtp.InsurerInvitationNotification.DTO;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.UseCase.Notifications
{
    public class InviteInsurerCase
    {

        private readonly DbContextEntity _contextEntity;
        private readonly InsurerInvitationNotificationEmail _insurerNotificationEmail;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly GetB2CInfo _getB2CInfo;

        public InviteInsurerCase(DbContextEntity contextEntity,
            InsurerInvitationNotificationEmail insurerNotificationEmail,
            IConfiguration configuration, IMapper mapper, GetB2CInfo getB2CInfo)
        {
            _contextEntity = contextEntity;
            _insurerNotificationEmail = insurerNotificationEmail;
            _configuration = configuration;
            _mapper = mapper;
            _getB2CInfo = getB2CInfo;
        }

        public async Task SendInvitationEmailAsync(InsurerInviteInfoDto request)
        {

            var insurerAdmin = await _contextEntity.InsurerAdmin.Where(ia => ia.Email == _getB2CInfo.Email).FirstOrDefaultAsync();
            if (insurerAdmin == null)
            {
                throw new InsurerAdminNotFoundException();
            }

            InsurerInvitationNotificationRequestDto emailRequest = new InsurerInvitationNotificationRequestDto
            {
                ToEmail = request.Email,
                InsurerName = insurerAdmin.Name,
                Link = _configuration["FeUrl"] + "?event=II&email=" + request.Email,
            };
            CreateInsurerEmployeeRequestDto insurerEmployee = new();
            insurerEmployee.Email = request.Email;
   

            await CreateInsurerEmployee(insurerEmployee,insurerAdmin.InsurerCompanyId);
            await _insurerNotificationEmail.SendEmailAsync(emailRequest);
        }

        public async Task CreateInsurerEmployee(CreateInsurerEmployeeRequestDto newInsurerEmployee,string companyId)
        {
            var insurerCompanyFound = _contextEntity.InsurerCompany.Where(IC => IC.Id == companyId).FirstOrDefault();

            if (insurerCompanyFound == null)
            {
                throw new InsurerCompanyNotFoundException();
            }

            var insurerEmployeeFound = _contextEntity.InsurerEmployee.Where(IE => IE.Email == newInsurerEmployee.Email).FirstOrDefault();
            if (insurerEmployeeFound == null)
            {
                var insurerEmployee = _mapper.Map<InsurerEmployeeEntity>(newInsurerEmployee);
                insurerEmployee.InsurerCompanyId = insurerCompanyFound.Id;
                insurerEmployee.CreatedBy = _getB2CInfo.Email;

                await _contextEntity.InsurerEmployee.AddAsync(insurerEmployee);
                _contextEntity.SaveChanges();
            }

        }
    }
}
