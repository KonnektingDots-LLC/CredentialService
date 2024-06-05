using cred_system_back_end_app.Application.DTO.Requests;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Insurers.Commands.Handlers
{
    public class UpdateInsurerEmployeeHandler : IRequestHandler<UpdateInsurerEmployeeCommand>
    {
        private readonly IInsurerEmployeeRepository _insurerEmployeeRepository;

        public UpdateInsurerEmployeeHandler(IInsurerEmployeeRepository insurerEmployeeRepository)
        {
            _insurerEmployeeRepository = insurerEmployeeRepository;
        }

        public async Task Handle(UpdateInsurerEmployeeCommand request, CancellationToken cancellationToken)
        {
            await UpdateInsurerEmployee(request.UserRegisterRequest);
        }

        public async Task UpdateInsurerEmployee(UserRegisterRequestDto userRegisterRequetsDto)
        {
            var insurerEmployeeFound = await _insurerEmployeeRepository.GetByInsurerEmployeeEmailAsync(userRegisterRequetsDto.Email)
                ?? throw new InsurerEmployeeNotFoundException("No employee matches the given email.");

            insurerEmployeeFound.Name = userRegisterRequetsDto.Name;
            insurerEmployeeFound.LastName = userRegisterRequetsDto.LastName;
            insurerEmployeeFound.MiddleName = userRegisterRequetsDto.MiddleName;
            insurerEmployeeFound.SurName = userRegisterRequetsDto.Surname;

            await _insurerEmployeeRepository.UpdateAsync(insurerEmployeeFound);
        }
    }
}
