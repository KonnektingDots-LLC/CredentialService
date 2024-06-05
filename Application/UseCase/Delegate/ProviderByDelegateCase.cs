using AutoMapper;
using cred_system_back_end_app.Application.Common.Enum;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.UseCase.Delegate.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace cred_system_back_end_app.Application.UseCase.Delegate
{
    public class ProviderByDelegateCase
    {
        private readonly DbContextEntity _context;
        private readonly IMapper _mapper;

        public ProviderByDelegateCase(DbContextEntity context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ProviderByDelegateResponseDto> GetProviderByDelegateId(int delegateId)
        {
            List<ProviderByDelegateResponseDto> result = new List<ProviderByDelegateResponseDto>();
            var _delegate = _context.Delegate.FirstOrDefault(d => d.Id == delegateId);
            var _providerDelegate = _context.ProviderDelegate
                .Include(pd => pd.Provider)
                .Where(e => e.DelegateId == delegateId && e.IsActive == true)
                .ToList();

            if (_delegate == null || _providerDelegate == null)
            {
                throw new EntityNotFoundException();
            }

            foreach (var providerDelegate in _providerDelegate)
            {
                var provider = providerDelegate.Provider;
                var credFormStatusTypeId = _context.CredForm.Find(provider.CredFormId).CredFormStatusTypeId;
                var credFormStatusTypeEntity = _context.CredFormStatusType.Find(credFormStatusTypeId);
                var resultItem = new ProviderByDelegateResponseDto
                {
                    ProviderId = provider.Id,
                    Name = provider.FirstName,
                    MiddleName = provider.MiddleName,
                    LastName = provider.LastName,
                    SurName = provider.SurName,
                    Email = provider.Email,
                    PhoneNumber = provider.PhoneNumber,
                    RenderingNPI = provider.RenderingNPI,
                    BillingNPI = provider.BillingNPI,
                    StatusName = credFormStatusTypeEntity.Name,
                    PrioritySorting = (int)credFormStatusTypeEntity.PrioritySorting
                };

                result.Add(resultItem);

            }

            var resultSorted = result.OrderBy(x => x.PrioritySorting).ToList();

            return resultSorted;


        }
    }
}
