using AutoMapper;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;

namespace cred_system_back_end_app.Application.CRUD.MedicalGroup
{
    public class MedicalGroupRepository
    {
        private readonly DbContextEntity _context;

        private readonly IMapper _mapper;

        public MedicalGroupRepository(DbContextEntity context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }        
        
        public MedicalGroupRepository(DbContextEntity context)
        {
            _context = context;
        }

        public MedicalGroupEntity GetMedicalGroupById(int Id, int medicalGroupType)
        {
            return _context.MedicalGroup
                .Where(mg => mg.Id == Id)
                .Where(mg => mg.MedicalGroupTypeId == medicalGroupType)
                .ToList()
                .FirstOrDefault();
        }

        #region helpers

        #endregion
    }
}
