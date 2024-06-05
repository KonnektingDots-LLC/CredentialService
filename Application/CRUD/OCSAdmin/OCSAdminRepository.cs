using cred_system_back_end_app.Application.Common.RequestDto;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.CRUD.OCSAdmin
{
    public class OCSAdminRepository
    {
        private readonly DbContextEntity _dbContext;

        public OCSAdminRepository(DbContextEntity dbContextEntity) 
        { 
            _dbContext = dbContextEntity;
        }

        public async Task<IEnumerable<OCSAdminEntity>> GetByEmailAsync(string email) 
        {
            return await _dbContext.OCSAdmin.Where(oa => oa.Email == email).ToListAsync();
        }

        public async Task UpdateOCSAdminAsync(Names names, string email)
        {
            var ocsAdminResults = await GetByEmailAsync(email);

            if (!ocsAdminResults.Any()) 
            {
                throw new AggregateException($"Error updating OCS admin: no admin found with email '{email}'");
            }

            var ocsAdmin = ocsAdminResults.FirstOrDefault();

            ocsAdmin.Name = names.FirstName; 
            ocsAdmin.MiddleName = names.MiddleName;
            ocsAdmin.LastName = names.LastName;
            ocsAdmin.Surname = names.Surname;
            ocsAdmin.Email = email;
            ocsAdmin.ModifiedBy = email;
            ocsAdmin.ModifiedDate = DateTime.Now;

            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        }
    }
}
