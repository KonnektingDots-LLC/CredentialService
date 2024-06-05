using AutoMapper;
using cred_system_back_end_app.Domain.Entities;
using cred_system_back_end_app.Domain.Interfaces.Repositories;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Infrastructure.Data.Repositories
{
    public class CredFormRepository : GenericAuditRepository<CredFormEntity, int>, ICredFormRepository
    {
        private readonly DbContextEntity _dbContextEntity;
        private readonly IMapper _mapper;

        public CredFormRepository(DbContextEntity context, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(context, httpContextAccessor)
        {
            _dbContextEntity = context;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override async Task<CredFormEntity> AddAndSaveAsync(CredFormEntity entity)
        {
            var saved = await base.AddAndSaveAsync(entity);

            return await _dbContextEntity.CredForm
                .Where(cf => cf.Id == saved.Id)
                .Include(cf => cf.CredFormStatusType)
                .FirstAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<CredFormEntity?> GetByIdAsync(int id)
        {
            return await _dbContextEntity.CredForm
                .Where(cf => cf.Id == id)
                .Include(cf => cf.Provider)
                .FirstAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<CredFormEntity?> GetByEmailAsync(string email)
        {
            return await _dbContextEntity.CredForm.Where(p => p.Email == email).OrderBy(p => p.Version).LastOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setCredFormStatus"></param>
        /// <returns></returns>
        public async Task SetStatusAndSave(int id, string newStatus)
        {
            var credForm = await _dbContextEntity.CredForm.
                Where(r => r.Id == id).FirstOrDefaultAsync();
            credForm.CredFormStatusTypeId = newStatus;
            await base.UpdateAsync(credForm);
        }
    }
}
