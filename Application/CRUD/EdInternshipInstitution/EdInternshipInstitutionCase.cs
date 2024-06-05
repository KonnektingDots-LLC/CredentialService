//using AutoMapper;
//using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
//using cred_system_back_end_app.Application.Common.ResponseDTO;
//using cred_system_back_end_app.Application.UseCase.EdInternshipInstitution.DTO;
//using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
//using cred_system_back_end_app.Infrastructure.DB.Entity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace cred_system_back_end_app.Application.UseCase.EdInternshipInstitution
//{
//    public class EdInternshipInstitutionCase
//    {
//        private readonly DbContextEntity _context;
//        private readonly IMapper _mapper;


//        public EdInternshipInstitutionCase(DbContextEntity context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public List<EdInternshipInstitutionResponseDto> GetAllEdInternshipInstitution()
//        {
//            var EdInternshipInstitution = _context.EdInternshipInstitution.ToList();
//            return _mapper.Map<List<EdInternshipInstitutionResponseDto>>(EdInternshipInstitution);
//        }

//        public EdInternshipInstitutionResponseDto? GetEdInternshipInstitutionById(int id)
//        {

//            var EdInternshipInstitution = _context.EdInternshipInstitution.Find(id);
//            if (EdInternshipInstitution == null)
//            {

//                throw new EdInternshipInstitutionNotFoundException();
//            }

//            return _mapper.Map<EdInternshipInstitutionResponseDto>(EdInternshipInstitution);


//        }

//        public EdInternshipInstitutionResponseDto CreateEdInternshipInstitution([FromBody] CreateEdInternshipInstitutionDto createDto)
//        {
//            try
//            {

//                var newEdInternshipInstitution = _mapper.Map<EdInternshipInstitutionEntity>(createDto);
//                newEdInternshipInstitution.CreationDate = DateTime.Now;
//                newEdInternshipInstitution.ModifiedDate = DateTime.Now;


//                _context.EdInternshipInstitution.Add(newEdInternshipInstitution);
//                _context.SaveChanges();

//                var newEdInternshipInstitutionResponse = _mapper.Map<EdInternshipInstitutionResponseDto>(newEdInternshipInstitution);

//                return newEdInternshipInstitutionResponse;


//            }
//            catch (Exception ex) { throw; }
//        }

//        public EdInternshipInstitutionResponseDto UpdateEdInternshipInstitution(EdInternshipInstitutionEntity edintershipinstitutionEntity)
//        {
//            _context.Entry(edintershipinstitutionEntity).State = EntityState.Modified;
//            _context.SaveChanges();

//            if (edintershipinstitutionEntity == null)
//            {
//                throw new EntityNotFoundException();
//            }

//            var updateEdInternshipInstitutionResponse = _mapper.Map<EdInternshipInstitutionResponseDto>(edintershipinstitutionEntity);

//            return updateEdInternshipInstitutionResponse;
//        }
//    }
//}
