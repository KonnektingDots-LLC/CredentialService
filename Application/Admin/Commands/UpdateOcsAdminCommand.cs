using cred_system_back_end_app.Application.DTO.Requests;
using cred_system_back_end_app.Domain.Interfaces;
using MediatR;

namespace cred_system_back_end_app.Application.Admin.Commands
{
    public record class UpdateOcsAdminCommand(UserRegisterRequestDto UserRegisterRequest) : IRequest, ITransactionPipeline;
}
