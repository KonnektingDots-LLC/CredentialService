using cred_system_back_end_app.Application.Common.RequestDto;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.CRUD.Insurer
{
    public class InsurerAdminRepository
    {
        private readonly DbContextEntity _dbContext;

        public InsurerAdminRepository(DbContextEntity dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<InsurerAdminEntity>> GetByEmail(string email) 
        {
            return await _dbContext.InsurerAdmin
                        .Where(ia => ia.Email == email)
                        .ToListAsync();
        }        
        
        public async Task UpdateInsurerAdmin(string email, Names names)
        {

            var insurerAdmin = await _dbContext.InsurerAdmin
                        .FirstOrDefaultAsync(ia => ia.Email == email);

            if (insurerAdmin == null)
            {
                throw new AggregateException("Insurer admin was not previously whitelisted.");
            }

            using var transaction = _dbContext.Database.BeginTransaction();

            insurerAdmin.Name = names.FirstName;
            insurerAdmin.LastName = names.LastName;
            insurerAdmin.MiddleName = names.MiddleName;
            insurerAdmin.Surname = names.Surname;
            insurerAdmin.ModifiedBy = email;
            insurerAdmin.ModifiedDate = DateTime.Now;

            _dbContext.SaveChanges();

            await transaction.CommitAsync();
        }

        public async Task<IEnumerable<InsurerAdminEntity>> GetAdminsByProvider(int providerId) 
        {
            var insurerAdmins = await _dbContext.ProviderPlanAccept
                .Where(p => p.ProviderId == providerId)
                .Include(p => p.PlanAcceptList.InsurerCompany.InsurerAdmins)
                .SelectMany(p => p.PlanAcceptList.InsurerCompany.InsurerAdmins)
                .ToListAsync();


            if (insurerAdmins == null)
            {
                //TODO: create more specific exception.
                throw new AggregateException($"No insurer company associated to the providerId could be found.");
            }

            return insurerAdmins;
        }
    }
}
