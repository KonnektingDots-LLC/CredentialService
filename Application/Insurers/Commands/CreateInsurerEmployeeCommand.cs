using cred_system_back_end_app.Domain.Interfaces;
using MediatR;

namespace cred_system_back_end_app.Application.Insurers.Commands
{
    public record class CreateInsurerEmployeeCommand(string? InsurerEmployeeEmail, string? UserEmail) : IRequest, ITransactionPipeline;
}
