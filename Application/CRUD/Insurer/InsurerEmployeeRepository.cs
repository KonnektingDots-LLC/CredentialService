using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.CRUD.MedicalGroup.DTO;
using cred_system_back_end_app.Application.UseCase.Insurer.DTO.CreateInsurerEmployee;
using cred_system_back_end_app.Application.UseCase.Insurer.DTO.UpdateInsurerEmployee;
using cred_system_back_end_app.Infrastructure.B2C;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace cred_system_back_end_app.Application.CRUD.Insurer
{
    public class InsurerEmployeeRepository
    {

        private readonly DbContextEntity _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GetB2CInfo _getB2CInfo;


        public InsurerEmployeeRepository(DbContextEntity dbContext, 
            IHttpContextAccessor httpContextAccessor, IMapper mapper,
            GetB2CInfo getB2CInfo)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor; 
            _mapper = mapper;
            _getB2CInfo = getB2CInfo;
        }

        public async Task<InsurerEmployeeEntity> CreateInsurerEmployee(CreateInsurerEmployeeRequestDto insurerEmployee)
        {
            var insurerCompanyFound = _dbContext.InsurerCompany.Where(IC => IC.Id == insurerEmployee.InsurerCompanyId).FirstOrDefault();

            if (insurerCompanyFound == null)
            {
                throw new EntityNotFoundException();
            }

            var insurerEmployeeFound = _dbContext.InsurerEmployee.Where(IE => IE.Email == insurerEmployee.Email).FirstOrDefault();
            if (insurerEmployeeFound != null) { throw new InsurerEmployeeDuplicateException(); }

            var newInsurerEmployee = _mapper.Map<InsurerEmployeeEntity>(insurerEmployee);
            newInsurerEmployee.CreatedBy = _getB2CInfo.Email;

           await _dbContext.InsurerEmployee.AddAsync(newInsurerEmployee);
           _dbContext.SaveChanges();

            return newInsurerEmployee;
        }

        public async Task UpdateInsurerEmployeeAsync(bool isActive, string email) 
        { 
            var employee = await _dbContext.InsurerEmployee
                .Where(i => i.Email == email)
                .FirstOrDefaultAsync();

            if (employee == null) 
            {
                throw new AggregateException("No employee matches the given email.");
            }

            employee.IsActive = isActive;
            
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        }

        public async Task<(IEnumerable<InsurerEmployeeEntity>, int)> GetByInsurerAdminEmail(int offset, int limitPerPage, string email) 
        {
            var insurer = await _dbContext.InsurerAdmin
                .Where(x => x.Email == email)
                .Include(x => x.InsurerCompany)
                .FirstOrDefaultAsync();

            var employeeCount = await _dbContext.InsurerEmployee
                .Where(ie => ie.InsurerCompanyId == insurer.InsurerCompanyId)
                .CountAsync();

            if (employeeCount < offset)
            {
                throw new AggregateException("Pagination offset exceeds record count.");
            }

            var employees = await _dbContext.InsurerEmployee
                .Where(ie => ie.InsurerCompanyId == insurer.InsurerCompanyId)
                .Skip(offset)
                .Take(limitPerPage)
                .ToListAsync();

            return (employees, employeeCount);
        }


        public async Task<(IEnumerable<InsurerEmployeeEntity>, int)> GetBySearchInsurerAdminEmail(int offset, int limitPerPage, string email, string search)
        {
            var searchValue = search.Replace(" ", "");

            var insurer = await _dbContext.InsurerAdmin
                .Where(x => x.Email == email)
                .Include(x => x.InsurerCompany)
                .FirstOrDefaultAsync();

            var employeeCount = await _dbContext.InsurerEmployee
                .Where(ie => ie.InsurerCompanyId == insurer.InsurerCompanyId
                    && ((ie.Name + ie.MiddleName + ie.LastName + ie.SurName)
                 .Contains(searchValue) || ie.Email.Contains(searchValue)))
                .CountAsync();
            
            var employees = await _dbContext.InsurerEmployee
                .Where(ie => ie.InsurerCompanyId == insurer.InsurerCompanyId
                    && ((ie.Name + ie.MiddleName +ie.LastName+ie.SurName)
                 .Contains(searchValue) || ie.Email.Contains(searchValue)))
                .Skip(offset)
                .Take(limitPerPage)
                .ToListAsync();

            return (employees, employeeCount);
        }

        public async Task UpdateInsurerEmployee(UpdateInsurerEmployeeRequestDto insurerEmployee)
        {
            var insurerEmployeeFound = await _dbContext.InsurerEmployee.Where(IC => IC.Email == _getB2CInfo.Email).FirstOrDefaultAsync();

            if (insurerEmployeeFound == null)
            {
                throw new InsurerNotFoundException();
            }

            insurerEmployeeFound.ModifiedBy = _getB2CInfo.Email;
            insurerEmployeeFound.ModifiedDate = DateTime.Now;
            insurerEmployeeFound.Name = insurerEmployee.Name;
            insurerEmployeeFound.LastName = insurerEmployee.LastName;
            insurerEmployeeFound.MiddleName = insurerEmployee.MiddleName;
            insurerEmployeeFound.SurName = insurerEmployee.SurName;

            _dbContext.Entry(insurerEmployeeFound).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<InsurerEmployeeEntity>> GetByEmail(string email, bool includeCompany = false) 
        { 
            if (includeCompany) 
            {
                return await _dbContext.InsurerEmployee
                  .Where(i => i.Email == email)
                  .Include(i => i.InsurerCompany)
                  .ToListAsync();
            }

            return await _dbContext.InsurerEmployee
                .Where(i => i.Email == email)
                .ToListAsync();
        }
    }
}
