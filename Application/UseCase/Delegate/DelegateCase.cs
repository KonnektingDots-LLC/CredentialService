using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.Common.Services;
using cred_system_back_end_app.Application.CRUD.Delegate;
using cred_system_back_end_app.Application.UseCase.Delegate.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Infrastructure.Smtp.ProfileCompletionNotification;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace cred_system_back_end_app.Application.UseCase.Delegate
{
    public class DelegateCase
    {
        private readonly DbContextEntity _context;
        private readonly IMapper _mapper;
        private string _email;
        public string UserEmail;
        private readonly DelegateRepository _delegateRepo;
        private readonly FireAndForgetService _fireAndForget;

        public DelegateCase(
            DbContextEntity context, 
            IMapper mapper,
            DelegateRepository delegateRepository,
            ProfileCompletionNotificationEmail profileCompleteEmail,
            FireAndForgetService fireAndForgetService)
        {
            _context = context;
            _mapper = mapper;
            _delegateRepo = delegateRepository;
            _fireAndForget = fireAndForgetService;
        }

        public void CreateProviderDelegate(CreateProviderDelegateDto createDto)
        {
            try
            {
                if (!DoesProviderDelegateExist(createDto))
                {
                    //var provider = _context.ProviderDelegate.FirstOrDefault(p => p.Id == createDto.ProviderId);

                    var newProviderDelegate = _mapper.Map<ProviderDelegateEntity>(createDto);
                    newProviderDelegate.CreationDate = DateTime.Now;
                    newProviderDelegate.CreatedBy = _email;

                    _context.ProviderDelegate.Add(newProviderDelegate);
                    _context.SaveChanges();

                    RemoveDelegateInvitation(createDto);
                }
                else
                {
                    throw new Exception("ProviderDelegate already exists.");
                }
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        private void RemoveDelegateInvitation(CreateProviderDelegateDto createDto)
        {
            var delegat = _context.Delegate.FirstOrDefault(d => d.Id == createDto.DelegateId);

            _context.DelegateInviteEmails
                .Where(d => d.ProviderId == createDto.ProviderId && d.Email == delegat.Email)
                .ExecuteDelete();
        }

        public async Task SetEmail(string email)
        {
            _email = email;
        }

        public CreateDelegateResponseDto CreateDelegate(CreateDelegateDto createDto)
        {
            try
            {
                if (!DelegateIsRegistered(createDto.Email))
                {
                    CreateDelegateResponseDto response = new CreateDelegateResponseDto();
                    var newDelegate = _mapper.Map<DelegateEntity>(createDto);
                    newDelegate.CreationDate = DateTime.Now;
                    newDelegate.CreatedBy = _email;

                    _context.Delegate.Add(newDelegate);

                    _context.SaveChanges();

                    response.Id = newDelegate.Id;

                    return response;
                }
                else
                {
                    throw new Exception("Delegate already exists.");
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CreateDelegateResponseDto> FinishDelegateRegistrationAsync(CreateDelegateDto createDelegateDto) 
        { 
            if (DelegateIsRegistered(createDelegateDto.Email)) 
            {
                throw new Exception("Delegate is already registered.");
            }

            var delegateId = await _delegateRepo.UpdateAsync(createDelegateDto.Email, createDelegateDto.FullName);

            _fireAndForget
                .Execute<ProfileCompletionNotificationEmail>(async p => await p.SendEmailAsync(createDelegateDto.Email));

            return new CreateDelegateResponseDto { Id = delegateId };
        }

        public DelegateResponseDto? GetDelegateByEmail(string email)
        {
            var delegat = _context.Delegate.FirstOrDefault(d => d.Email == email);
            
            if (delegat == null)
            {
                throw new EntityNotFoundException();
            }

            var result = _mapper.Map<DelegateResponseDto>(delegat);

            var delegateType = _context.DelegateType.FirstOrDefault(dt => dt.Id == delegat.DelegateTypeId);
            DelegateTypeDto delegType = new DelegateTypeDto
            {
                Id = delegateType.Id,
                Name = delegateType.Name,
                IsActive = delegateType.IsActive,
                IsExpired = delegateType.IsExpired,
                ExpiredDate = delegateType.ExpiredDate
            };

            var delegateCompany = _context.DelegateCompany.FirstOrDefault(dc => dc.Id == delegat.DelegateCompanyId);
            DelegateCompanyDto delegCompany = new DelegateCompanyDto
            {
                Name = delegateCompany.Name,
                RepresentativeFullName = delegateCompany.RepresentativeFullName,
                RepresentativeEmail = delegateCompany.RepresentativeEmail,
                IsActive = delegateCompany.IsActive
            };

            var delegateProviders = _context.ProviderDelegate.Where(pd => pd.DelegateId == delegat.Id).ToList();

            List<ProviderDelegateDto> providers = new List<ProviderDelegateDto>();
            if (delegateProviders.Any())
            {
                for (int i = 0; i < delegateProviders.Count; i++)
                {
                    ProviderDelegateDto newProvider = new ProviderDelegateDto();
                    var provid = delegateProviders[i];

                    newProvider.ProviderId = provid.ProviderId;
                    newProvider.IsActive = provid.IsActive;
                    providers.Add(newProvider);
                }
            }

            result.DelegateType = delegType;
            result.DelegateCompany = delegCompany;
            result.ProviderDelegate = delegateProviders.Any() ? providers : null;

            return result;
        }

        public async Task<DelegateListResponseDTO> GetByProvider(int providerId, int currentPage, int limitPerPage) 
        {
            var delegateCount = await _context.ProviderDelegate
                .CountAsync(pd => pd.ProviderId == providerId);

            var delegateList = new DelegateListResponseDTO
            {
                CurrentPage = currentPage,
                LimitPerPage = limitPerPage,
                TotalNumberOfPages = (int)PaginationHelper.GetTotalNumberOfPages(limitPerPage, delegateCount)
            };

            var offset = PaginationHelper.GetOffset(currentPage, limitPerPage);

            var providerDelegates = await _context.ProviderDelegate
                .Where(pd => pd.ProviderId == providerId)
                .Include(pd => pd.Delegate)
                .Paginated(offset, limitPerPage)
                .ToListAsync();
            
            if (!providerDelegates.Any()) 
            {
                delegateList.Content = new DelegateInfoDto[]{ };
                return delegateList;
            }

            var delegateInfoDTOs = providerDelegates.Select(p => new DelegateInfoDto
            {
                Id = p.DelegateId,
                FullName = p.Delegate.FullName,
                Email = p.Delegate.Email,
                IsActive = p.IsActive,
            })
            .ToArray();

            delegateList.Content = delegateInfoDTOs;

            return delegateList;
        }

        public bool DoesDelegateExist(string email)
        {
            var delgate = _context.Delegate.FirstOrDefault(d => d.Email == email);

            return delgate == null ? false : true;
        }

        public bool DelegateIsRegistered(string email)
        {
            var delgate = _context.Delegate.FirstOrDefault(d => d.Email == email);

            if (delgate == null) 
            { 
                return false;
            }

            var isRegistered = !delgate.FullName.IsNullOrEmpty();

            return isRegistered;
        }

        public async Task<bool> DelegateInviteExists(string email)
        {
            var delgate = await _context.DelegateInviteEmails.FirstOrDefaultAsync(d => d.Email == email);

            return delgate == null ? false : true;
        }

        public bool DoesProviderDelegateExist(CreateProviderDelegateDto createDto)
        {
            var record = _context.ProviderDelegate.FirstOrDefault(d => d.DelegateId == createDto.DelegateId && d.ProviderId == createDto.ProviderId);

            return record == null ? false : true;
        }

        public async Task SetDelegateStatusAsync(bool isActive, int delegateId)
        {
            var (provider, theDelegate) = await _delegateRepo.UpdateProviderDelegateAsync(isActive, delegateId, UserEmail);

            SendNotificationToDelegate(provider, theDelegate);

            SendNotificationToProvider(provider, theDelegate);
        }

        #region
        private void SendNotificationToDelegate(ProviderEntity provider, DelegateEntity theDelegate)
        {
            _fireAndForget
                .Execute<DelegateStatusUpdateNotificationManager>
                (
                    async manager => await manager.SendNotification(theDelegate.Email, provider)
                );
        }

        private void SendNotificationToProvider(ProviderEntity provider, DelegateEntity theDelegate)
        {
            _fireAndForget
                .Execute<ProviderDelegateStatusNotificationManager>
                (
                    async manager => await manager.SendNotification(theDelegate.FullName, provider)
                );
        }
        #endregion
    }
}
