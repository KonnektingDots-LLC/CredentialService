using AutoMapper;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;

namespace cred_system_back_end_app.Application.CRUD.SignUpHistory
{
    public class SignUpHistoryCase
    {
        private readonly DbContextEntity _context;

        public SignUpHistoryCase(DbContextEntity context)
        {
            _context = context;

        }

        public async Task<Empty> CreateSignUpHistory(SignUpHistoryEntity request)
        {
            await _context.SignUpHistory.AddAsync(request);
            await _context.SaveChangesAsync();

            return new();
        }
    }
}
