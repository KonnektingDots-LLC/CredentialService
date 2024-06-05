//using AutoMapper;
//using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
//using cred_system_back_end_app.Application.Common.ResponseDTO;
//using cred_system_back_end_app.Application.UseCase.EdResidencyInstitution.DTO;
//using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
//using cred_system_back_end_app.Infrastructure.DB.Entity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace cred_system_back_end_app.Application.UseCase.EdResidencyInstitution
//{
//    public class EdResidencyInstitutionCase
//    {
//        private readonly DbContextEntity _context;
//        private readonly IMapper _mapper;


//        public EdResidencyInstitutionCase(DbContextEntity context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public List<EdResidencyInstitutionResponseDto> GetAllEdResidencyInstitution()
//        {
//            var EdResidencyInstitution = _context.EdResidencyInstitution.ToList();
//            return _mapper.Map<List<EdResidencyInstitutionResponseDto>>(EdResidencyInstitution);
//        }

//        public EdResidencyInstitutionResponseDto? GetEdResidencyInstitutionById(int id)
//        {

//            var EdResidencyInstitution = _context.EdResidencyInstitution.Find(id);
//            if (EdResidencyInstitution == null)
//            {

//                throw new EdResidencyInstitutionNotFoundException();
//            }

//            return _mapper.Map<EdResidencyInstitutionResponseDto>(EdResidencyInstitution);


//        }

//        public EdResidencyInstitutionResponseDto CreateEdResidencyInstitution([FromBody] CreateEdResidencyInstitutionDto createDto)
//        {
//            try
//            {

//                var newEdResidencyInstitution = _mapper.Map<EdResidencyInstitutionEntity>(createDto);
//                newEdResidencyInstitution.CreationDate = DateTime.Now;
//                newEdResidencyInstitution.ModifiedDate = DateTime.Now;


//                _context.EdResidencyInstitution.Add(newEdResidencyInstitution);
//                _context.SaveChanges();

//                var newEdResidencyInstitutionResponse = _mapper.Map<EdResidencyInstitutionResponseDto>(newEdResidencyInstitution);

//                return newEdResidencyInstitutionResponse;


//            }
//            catch (Exception ex) { throw; }
//        }

//        public EdResidencyInstitutionResponseDto UpdateEdResidencyInstitution(EdResidencyInstitutionEntity edresidencyinstitutionEntity)
//        {
//            _context.Entry(edresidencyinstitutionEntity).State = EntityState.Modified;
//            _context.SaveChanges();

//            if (edresidencyinstitutionEntity == null)
//            {
//                throw new EntityNotFoundException();
//            }

//            var updateEdResidencyInstitutionResponse = _mapper.Map<EdResidencyInstitutionResponseDto>(edresidencyinstitutionEntity);

//            return updateEdResidencyInstitutionResponse;
//        }
//    }
//}
