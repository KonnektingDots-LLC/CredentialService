//using AutoMapper;
//using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
//using cred_system_back_end_app.Application.Common.ResponseDTO;
//using cred_system_back_end_app.Application.UseCase.EdFellowshipInstitution.DTO;
//using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
//using cred_system_back_end_app.Infrastructure.DB.Entity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace cred_system_back_end_app.Application.UseCase.EdFellowshipInstitution
//{
//    public class EdFellowshipInstitutionCase
//    {
//        private readonly DbContextEntity _context;
//        private readonly IMapper _mapper;


//        public EdFellowshipInstitutionCase(DbContextEntity context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        public List<EdFellowshipInstitutionResponseDto> GetAllEdFellowshipInstitution()
//        {
//            var edfellowshipinstitution = _context.EdFellowshipInstitution.ToList();
//            return _mapper.Map<List<EdFellowshipInstitutionResponseDto>>(edfellowshipinstitution);
//        }

//        public EdFellowshipInstitutionResponseDto? GetEdFellowshipInstitutionById(int id)
//        {

//            var edfellowshipinstitution = _context.EdFellowshipInstitution.Find(id);
//            if (edfellowshipinstitution == null)
//            {

//                throw new EdFellowshipInstitutionNotFoundException();
//            }

//            return _mapper.Map<EdFellowshipInstitutionResponseDto>(edfellowshipinstitution);


//        }

//        public EdFellowshipInstitutionResponseDto CreateEdFellowshipInstitution([FromBody] CreateEdFellowshipInstitutionDto createDto)
//        {
//            try
//            {

//                var newEdFellowshipInstitution = _mapper.Map<EdFellowshipInstitutionEntity>(createDto);
//                newEdFellowshipInstitution.CreationDate = DateTime.Now;
//                newEdFellowshipInstitution.ModifiedDate = DateTime.Now;


//                _context.EdFellowshipInstitution.Add(newEdFellowshipInstitution);
//                _context.SaveChanges();

//                var newEdFellowshipInstitutionResponse = _mapper.Map<EdFellowshipInstitutionResponseDto>(newEdFellowshipInstitution);

//                return newEdFellowshipInstitutionResponse;


//            }
//            catch (Exception ex) { throw; }
//        }

//        public EdFellowshipInstitutionResponseDto UpdateEdFellowshipInstitution(EdFellowshipInstitutionEntity edfellowshipinstitutionEntity)
//        {
//            _context.Entry(edfellowshipinstitutionEntity).State = EntityState.Modified;
//            _context.SaveChanges();

//            if (edfellowshipinstitutionEntity == null)
//            {
//                throw new EntityNotFoundException();
//            }

//            var updateEdFellowshipInstitutionResponse = _mapper.Map<EdFellowshipInstitutionResponseDto>(edfellowshipinstitutionEntity);

//            return updateEdFellowshipInstitutionResponse;
//        }
//    }
//}
