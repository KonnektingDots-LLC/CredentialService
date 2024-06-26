﻿using AutoMapper;
using cred_system_back_end_app.Application.Common.EqualityComparers;
using cred_system_back_end_app.Application.UseCase.Submit.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.UseCase.Submit.ResubmitServices
{
    public class MalpracticeModificationService : EntityModificationServiceBase
    {
        private readonly DbContextEntity _dbContextEntity;
        private readonly OIGNumberComparer _oIGNumberComparer;

        public MalpracticeModificationService
        (
            DbContextEntity dbContextEntity,
            IMapper mapper,
            OIGNumberComparer oIGNumberComparer
        )
            : base(dbContextEntity, mapper)
        {
            _dbContextEntity = dbContextEntity;
            _oIGNumberComparer = oIGNumberComparer;

        }

        public async Task Modify(MalpracticeDTO malpracticeDTO, int providerId)
        {
            var newMalpractice = Common.Mappers.DTOToEntity.InsuranceHelper
                .GetMalpracticeEntities(malpracticeDTO, providerId);

            var oldMalpractice = await _dbContextEntity.Malpractice
                .Where(o => o.ProviderId == providerId)
                .Include(o => o.MalpracticeOIGCaseNumbers)
                .FirstOrDefaultAsync();
                   

            if (oldMalpractice != null)
            {
                var currentOIGCaseNumbers = oldMalpractice.MalpracticeOIGCaseNumbers;
                var newOIGCaseNumbers = malpracticeDTO.OigCaseNumber
                .Select(n => new MalpracticeOIGCaseNumbers
                {
                    Malpractice = newMalpractice,
                    OIGCaseNumber = n
                }); ;

                await ModifyEntity(newMalpractice, oldMalpractice);

                await ModifyRelations(currentOIGCaseNumbers, newOIGCaseNumbers, _oIGNumberComparer);
            }
            else
            {
                await ModifyEntity(newMalpractice, oldMalpractice=null);
            }

            


        }
    }
}
