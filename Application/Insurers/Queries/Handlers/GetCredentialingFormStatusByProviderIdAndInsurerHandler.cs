using AutoMapper;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.DTO.Responses;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Exceptions;
using cred_system_back_end_app.Domain.Interfaces;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using MediatR;

namespace cred_system_back_end_app.Application.Insurers.Queries.Handlers
{
    public class GetCredentialingFormStatusByProviderIdAndInsurerHandler : IRequestHandler<GetCredentialingFormStatusByProviderIdAndInsurerQuery, ProviderInsurerCompanyStatusAndHistoryResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IInsurerCompanyRepository _insurerCompanyRepo;
        private readonly IInsurerEmployeeRepository _insurerEmployeeRepo;
        private readonly IProviderInsurerCompanyStatusService _providerInsurerCompanyStatusService;
        private readonly IProviderInsurerCompanyStatusHistoryRepository _providerInsurerCompanyStatusHistoryRepo;

        public GetCredentialingFormStatusByProviderIdAndInsurerHandler(IMapper mapper, IInsurerCompanyRepository insurerCompanyRepository,
            IInsurerEmployeeRepository insurerEmployeeRepository, IProviderInsurerCompanyStatusService providerInsurerCompanyStatusService,
            IProviderInsurerCompanyStatusHistoryRepository providerInsurerCompanyStatusHistoryRepo)
        {
            _insurerCompanyRepo = insurerCompanyRepository;
            _mapper = mapper;
            _insurerEmployeeRepo = insurerEmployeeRepository;
            _providerInsurerCompanyStatusService = providerInsurerCompanyStatusService;
            _providerInsurerCompanyStatusHistoryRepo = providerInsurerCompanyStatusHistoryRepo;
        }

        public async Task<ProviderInsurerCompanyStatusAndHistoryResponseDto> Handle(GetCredentialingFormStatusByProviderIdAndInsurerQuery request, CancellationToken cancellationToken)
        {
            return await GetPICSAndPICSHByProviderAndInsurerCompany(request.ProviderId, request.InsurerEmail, request.Role);
        }

        public async Task<ProviderInsurerCompanyStatusAndHistoryResponseDto> GetPICSAndPICSHByProviderAndInsurerCompany(int providerId, string? email, string? role)
        {
            InsurerCompanyEntity? insurerCompany;
            string insurerCompanyId;
            switch (role)
            {
                case CredRole.ADMIN_INSURER:
                    insurerCompany = await _insurerCompanyRepo.GetByInsurerAdminEmailAsync(email);
                    if (insurerCompany == null)
                    {
                        throw new InsurerEmployeeNotFoundException("No insurer company associated to user email could be found.");
                    }
                    insurerCompanyId = insurerCompany.Id;
                    break;
                case CredRole.INSURER:
                    var insurerEmployees = await _insurerEmployeeRepo.SearchByInsurerEmployeeEmailAsync(email, true);
                    var insurerEmployee = insurerEmployees.FirstOrDefault();

                    if (!insurerEmployee.IsActive)
                    {
                        throw new AccessDeniedException();
                    }
                    insurerCompanyId = insurerEmployee.InsurerCompanyId;
                    break;
                default:
                    throw new AccessDeniedException();
            }

            var pics = await _providerInsurerCompanyStatusService.GetProviderInsurerCompanyStatusByProviderIdAndInsurerCompanyId(providerId, insurerCompanyId);
            var picsh = await _providerInsurerCompanyStatusHistoryRepo.GetProviderInsurerCompanyStatusHistoryByProviderInsurerCompanyStatusId(pics.Id);

            var picsDto = _mapper.Map<ProviderInsurerCompanyStatusResponseDto>(pics);
            picsDto.CurrentStatusDate = pics.CurrentStatusDate.ToString(DateFormats.IIPCA_DATE_FROMAT);
            picsDto.SubmitDate = pics.SubmitDate.ToString(DateFormats.IIPCA_DATE_FROMAT);
            picsDto.CommentDate = pics.CommentDate?.ToString(DateFormats.IIPCA_DATE_FROMAT);

            var picshDto = picsh.Select(s => new ProviderInsurerCompanyStatusHistoryResponseDto
            {
                ProviderInsurerCompanyStatusId = s.ProviderInsurerCompanyStatusId,
                InsurerStatusTypeId = s.InsurerStatusTypeId,
                StatusDate = s.StatusDate.ToString(DateFormats.IIPCA_DATE_FROMAT),
                Comment = s.Comment,
                CommentDate = s.CommentDate?.ToString(DateFormats.IIPCA_DATE_FROMAT),
                CreatedBy = s.CreatedBy
            });

            var result = new ProviderInsurerCompanyStatusAndHistoryResponseDto
            {
                ProviderInsurerCompanyStatusResponse = picsDto,
                ProviderInsurerCompanyStatusHistoryResponse = picshDto.ToList()
            };

            return result;
        }
    }
}
