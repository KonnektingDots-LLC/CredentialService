//using AutoMapper;
//using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
//using cred_system_back_end_app.Application.Common.ResponseDTO;
//using cred_system_back_end_app.Application.UseCase.EdMedicalSchool.DTO;
//using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
//using cred_system_back_end_app.Infrastructure.DB.Entity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace cred_system_back_end_app.Application.UseCase.EdMedicalSchool
//{
//    public class EdMedicalSchoolCase
//    {
//        private readonly DbContextEntity _context;
//        private readonly IMapper _mapper;


//        public EdMedicalSchoolCase(DbContextEntity context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public List<EdMedicalSchoolResponseDto> GetAllEdMedicalSchool()
//        {
//            var EdMedicalSchool = _context.EdMedicalSchool.ToList();
//            return _mapper.Map<List<EdMedicalSchoolResponseDto>>(EdMedicalSchool);
//        }

//        public EdMedicalSchoolResponseDto? GetEdMedicalSchoolById(int id)
//        {

//            var EdMedicalSchool = _context.EdMedicalSchool.Find(id);
//            if (EdMedicalSchool == null)
//            {

//                throw new EdMedicalSchoolNotFoundException();
//            }

//            return _mapper.Map<EdMedicalSchoolResponseDto>(EdMedicalSchool);


//        }

//        public EdMedicalSchoolResponseDto CreateEdMedicalSchool([FromBody] CreateEdMedicalSchoolDto createDto)
//        {
//            try
//            {

//                var newEdMedicalSchool = _mapper.Map<EdMedicalSchoolEntity>(createDto);
//                newEdMedicalSchool.CreationDate = DateTime.Now;
//                newEdMedicalSchool.ModifiedDate = DateTime.Now;


//                _context.EdMedicalSchool.Add(newEdMedicalSchool);
//                _context.SaveChanges();

//                var newEdMedicalSchoolResponse = _mapper.Map<EdMedicalSchoolResponseDto>(newEdMedicalSchool);

//                return newEdMedicalSchoolResponse;


//            }
//            catch (Exception ex) { throw; }
//        }

//        public EdMedicalSchoolResponseDto UpdateEdMedicalSchool(EdMedicalSchoolEntity edmedicalschoolEntity)
//        {
//            _context.Entry(edmedicalschoolEntity).State = EntityState.Modified;
//            _context.SaveChanges();

//            if (edmedicalschoolEntity == null)
//            {
//                throw new EntityNotFoundException();
//            }

//            var updateEdMedicalSchoolResponse = _mapper.Map<EdMedicalSchoolResponseDto>(edmedicalschoolEntity);

//            return updateEdMedicalSchoolResponse;
//        }
//    }
//}
