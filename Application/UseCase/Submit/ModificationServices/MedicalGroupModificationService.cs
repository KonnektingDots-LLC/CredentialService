using AutoMapper;
using cred_system_back_end_app.Application.Common.Constant;
using cred_system_back_end_app.Application.Common.Mappers.DTOToEntity;
using cred_system_back_end_app.Application.UseCase.Submit.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.UseCase.Submit.ResubmitServices
{
    public class MedicalGroupModificationService : EntityModificationServiceBase
    {
        private readonly DbContextEntity _dbContextEntity;

        public MedicalGroupModificationService
        (
            DbContextEntity dbContextEntity,
            IMapper mapper
        )
            : base(dbContextEntity, mapper)
        {
            _dbContextEntity = dbContextEntity;
        }

        public async Task Modify(MedicalGroupDTO medicalGroup, int medicalGroupType, int providerId) 
        {
            var oldMedicalGroups = _dbContextEntity.ProviderMedicalGroup
                .Where(p => p.ProviderId == providerId)
                .Include(p => p.MedicalGroup)
                .Include(p => p.MedicalGroup.Address)
                .ToList();

            //var oldMedicalGroup = oldMedicalGroups.Single(o => o.MedicalGroup.MedicalGroupTypeId == medicalGroupType).MedicalGroup;
            MedicalGroupEntity oldMedicalGroup = new MedicalGroupEntity();
            oldMedicalGroup = null;

            var oldProviderMedicalGroup = oldMedicalGroups.Where(o => o.MedicalGroup.MedicalGroupTypeId == medicalGroupType).FirstOrDefault();
            if (oldProviderMedicalGroup != null )
            {
               oldMedicalGroup = oldProviderMedicalGroup.MedicalGroup;
            }

            var newMedicalGroup = MedicalGroup.GetMedicalGroupEntity(medicalGroup, medicalGroupType);
            if (oldMedicalGroup == null) 
            {
                var providerMedicalGroupEntities = new ProviderMedicalGroupEntity
                {
                    ProviderId = providerId,
                    MedicalGroup = newMedicalGroup,
                };
                
                _dbContextEntity.AddRange(providerMedicalGroupEntities);
            }
            
            await ModifyEntity(newMedicalGroup, oldMedicalGroup);
        }
    }


}
