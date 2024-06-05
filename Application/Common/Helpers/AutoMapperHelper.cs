using AutoMapper;
using cred_system_back_end_app.Domain.Entities;

namespace cred_system_back_end_app.Application.Common.Helpers
{
    public static class AutoMapperHelper
    {
        private static IMappingExpression<MedicalSchoolEntity, MedicalSchoolEntity> IgnoreRecordHistory(this IMappingExpression<MedicalSchoolEntity, MedicalSchoolEntity> mappingExpression)
        {
            mappingExpression
                .ForMember(c => c.CreatedBy, opt => opt.Ignore())
                .ForMember(c => c.CreationDate, opt => opt.Ignore())
                .ForMember(c => c.ModifiedBy, opt => opt.Ignore())
                .ForMember(c => c.ModifiedDate, opt => opt.Ignore());

            return mappingExpression;
        }
    }
}
