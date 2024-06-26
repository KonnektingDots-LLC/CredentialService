﻿using AutoMapper;
using cred_system_back_end_app.Application.Common.Mappers.DTOToEntity;
using cred_system_back_end_app.Application.UseCase.Submit.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Infrastructure.PdfReport.DTO;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.UseCase.Submit.ResubmitServices
{
    public class LicensesModificationService : EntityModificationServiceBase
    {
        private readonly DbContextEntity _dbContextEntity;

        public LicensesModificationService
        (
            DbContextEntity dbContextEntity,
            IMapper mapper
        )
            : base(dbContextEntity, mapper)
        {
            _dbContextEntity = dbContextEntity;
        }

        public async Task Modify(LicensesCertificatesDTO licenseAndCertificationDto, int providerId) 
        {
            var newPrMedicalLicenses = License.GetMedicalLicenseEntities(licenseAndCertificationDto, providerId);

            var currentPrMedicalLicenses = await _dbContextEntity.MedicalLicense
                .Where(m => m.ProviderId == providerId)
                .ToListAsync();

            foreach(var newLicense in newPrMedicalLicenses) 
            {
                var currentMedicalLicense = currentPrMedicalLicenses
                    .Where(m => m.MedicalLicenseTypeId == newLicense.MedicalLicenseTypeId)
                    .FirstOrDefault();

                if (currentMedicalLicense == null) 
                {
                    _dbContextEntity.Add(newLicense);

                    continue;
                }  

                await ModifyEntity(newLicense, currentMedicalLicense);
            }
        }
    }
}
