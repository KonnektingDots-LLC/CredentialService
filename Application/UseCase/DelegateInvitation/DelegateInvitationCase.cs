using AutoMapper;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.Common.ResponseTO;
using cred_system_back_end_app.Application.UseCase.DelegateInvitation.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.UseCase.DelegateInvitation
{
    public class DelegateInvitationCase
    {

        private readonly IMapper _mapper;


        public DelegateInvitationCase(DbContextEntity context, IMapper mapper)
        {

            _mapper = mapper;
        }

        public InvitationResponseDto Invite(DelegateInvitationDto invitation)
        {
    
            return new InvitationResponseDto();
        }
    }
}
