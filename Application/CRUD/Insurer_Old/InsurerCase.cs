using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionList;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.CRUD.Insurer.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.CRUD.Insurer
{
    public class InsurerCase
    {
        private readonly DbContextEntity _context;
        private readonly IMapper _mapper;



        public InsurerCase(DbContextEntity context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<InsurerResponseDto> GetAllInsurers()
        {
            var insurers = _context.Insurer.ToList();
            return _mapper.Map<List<InsurerResponseDto>>(insurers);
        }

        public InsurerResponseDto? GetInsurerById(int id)
        {

            var insurer = _context.Insurer.Find(id);
            if (insurer == null)
            {

                throw new EntityNotFoundException();
            }

            return _mapper.Map<InsurerResponseDto>(insurer);


        }

        public InsurerResponseDto CreateInsurer([FromBody] CreateInsurerDto createDto)
        {
            try
            {

                var newInsurer = _mapper.Map<InsurerEntity>(createDto);
                newInsurer.CreationDate = DateTime.Now;

                _context.Insurer.Add(newInsurer);
                _context.SaveChanges();

                var newInsurerResponse = _mapper.Map<InsurerResponseDto>(newInsurer);

                return newInsurerResponse;


            }
            catch (Exception ex) { throw; }
        }

        public InsurerResponseDto UpdateInsurer(InsurerEntity insurerEntity)
        {
            _context.Entry(insurerEntity).State = EntityState.Modified;
            _context.SaveChanges();

            if (insurerEntity == null)
            {
                throw new EntityNotFoundException();
            }

            var updateInsurerResponse = _mapper.Map<InsurerResponseDto>(insurerEntity);

            return updateInsurerResponse;
        }

        public void DeleteInsurer(int id)
        {
            var deleteInsurer = _context.Insurer.Find(id);
            if (deleteInsurer == null)
            {
                throw new ProviderNotFoundException();
            }

            _context.Insurer.Remove(deleteInsurer);
            _context.SaveChanges();

        }
    }
}
