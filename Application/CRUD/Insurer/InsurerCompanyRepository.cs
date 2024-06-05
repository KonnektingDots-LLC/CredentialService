using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.CRUD.Insurer
{
    public class InsurerCompanyRepository
    {
        private readonly DbContextEntity _dbContext;

        public InsurerCompanyRepository(DbContextEntity dbContext) 
        { 
            _dbContext = dbContext;
        }

        public async Task<InsurerCompanyEntity> GetByName(string name) 
        {
            var insurer = await _dbContext.InsurerCompany
                .FirstOrDefaultAsync(ic => ic.Name == name);
            
            if (insurer == null)
            {
                //TODO: create more specific exception.
                throw new AggregateException($"No insurer named {name} could be found.");
            }

            return insurer;
        }        
        
        public async Task<InsurerCompanyEntity> GetByAdmin(string email) 
        {
            var insurer = await _dbContext.InsurerAdmin
                .Include(ia => ia.InsurerCompany)
                .FirstOrDefaultAsync(ia => ia.Email == email);
            
            if (insurer == null)
            {
                //TODO: create more specific exception.
                throw new AggregateException($"No insurer company associated to {email} could be found.");
            }

            return insurer.InsurerCompany;
        }

        public async Task<InsurerCompanyEntity> GetByEmployee(string email)
        {
            var employee = await _dbContext.InsurerEmployee
                .Include(ia => ia.InsurerCompany)
                .FirstOrDefaultAsync(ia => ia.Email == email);

            if (employee == null)
            {
                //TODO: create more specific exception.
                throw new AggregateException($"No insurer company associated to {email} could be found.");
            }

            return employee.InsurerCompany;
        }

        public async Task<IEnumerable<InsurerCompanyEntity>> GetByProvider(int providerId)
        {
            try
            {
                var insurers = await _dbContext.ProviderPlanAccept
                    .Where(p => p.ProviderId == providerId)
                    .Include(p => p.PlanAcceptList)
                    .ThenInclude(pa => pa.InsurerCompany)
                    .Select(p => p.PlanAcceptList.InsurerCompany)
                    .ToListAsync();


                if (insurers == null)
                {
                    //TODO: create more specific exception.
                    throw new AggregateException($"No insurer company associated to the providerId could be found.");
                }

                return insurers;
            }
            catch(Exception ex) 
            { 
                throw ex;
            }
        }
    }
}
