using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Application.CRUD.Hospital.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.CRUD.Hospital
{
    public class HospitalCase
    {
        private readonly DbContextEntity _context;
        private readonly IMapper _mapper;


        public HospitalCase(DbContextEntity context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<HospitalListResponseDto> GetAllHospitalList()
        {
            var hospitals = _context.HospitalList.ToList();
            if (hospitals.Count == 0) { throw new HospitalNotFoundException(); }
            return _mapper.Map<List<HospitalListResponseDto>>(hospitals);
        }

        public List<HospPrivilegeResponseDto> GetAllPrivilegeList()
        {
            var privileges = _context.HospPriviledgeList.ToList();
            if (privileges.Count == 0) { throw new HospitalPrivilegeNotFoundException(); }
            return _mapper.Map<List<HospPrivilegeResponseDto>>(privileges);
        }

        //public List<HospitalResponseDto> GetAllHospital()
        //{
        //    var hospital = _context.Hospital.ToList();
        //    return _mapper.Map<List<HospitalResponseDto>>(hospital);
        //}

        //public HospitalResponseDto? GetHospitalById(int id)
        //{

        //    var hospital = _context.Hospital.Find(id);
        //    if (hospital == null)
        //    {

        //        throw new HospitalNotFoundException();
        //    }

        //    return _mapper.Map<HospitalResponseDto>(hospital);


        //}

        //public HospitalResponseDto CreateHospital([FromBody] CreateHospitalDto createDto)
        //{
        //    try
        //    {

        //        var newHospital = _mapper.Map<HospitalEntity>(createDto);
        //        newHospital.CreationDate = DateTime.Now;
        //        newHospital.ModifiedDate = DateTime.Now;


        //        _context.Hospital.Add(newHospital);
        //        _context.SaveChanges();

        //        var newHospitalResponse = _mapper.Map<HospitalResponseDto>(newHospital);

        //        return newHospitalResponse;


        //    }
        //    catch (Exception ex) { throw; }
        //}

        //public HospitalResponseDto UpdateHospital(HospitalEntity hospitalEntity)
        //{
        //    _context.Entry(hospitalEntity).State = EntityState.Modified;
        //    _context.SaveChanges();

        //    if (hospitalEntity == null)
        //    {
        //        throw new EntityNotFoundException();
        //    }

        //    var updateHospitalResponse = _mapper.Map<HospitalResponseDto>(hospitalEntity);

        //    return updateHospitalResponse;
        //}
    }
}
