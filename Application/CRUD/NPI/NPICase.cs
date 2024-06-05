using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.Common.ResponseDTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.AspNetCore.Mvc;

namespace cred_system_back_end_app.Application.CRUD.NPI
{
    public class NPICase
    {
        private readonly DbContextEntity _context;
        private readonly IMapper _mapper;



        public NPICase(DbContextEntity context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //public List<PNPIEntity> GetAllNPIs()
        //{
        //    var NPIs = _context.PNPI.ToList();
        //    return NPIs;
        //}

        //public PNPIEntity? GetNPIById(int id)
        //{

        //    var NPIs = _context.PNPI.Find(id);
        //    if (NPIs == null)
        //    {

        //        throw new EntityNotFoundException();
        //    }

        //    return NPIs;


        //}

        //public PNPIEntity CreateNPI([FromBody] CreateNPIDto createDto)
        //{
        //    try
        //    {

        //        var newNPI = _mapper.Map<PNPIEntity>(createDto);
        //        newNPI.CreationDate = DateTime.Now;

        //        _context.PNPI.Add(newNPI);
        //        _context.SaveChanges();

        //        return newNPI;

        //    }
        //    catch (Exception ex) { throw; }
        //}


        //public List<PNPITypeListEntity> GetAllNPITypes()
        //{
        //    var NPITypes = _context.PNPITypeList.ToList();
        //    return NPITypes;
        //}

        //public PNPITypeListEntity? GetNPITypeById(int id)
        //{

        //    var NPITypes = _context.PNPITypeList.Find(id);
        //    if (NPITypes == null)
        //    {

        //        throw new EntityNotFoundException();
        //    }

        //    return NPITypes;


        //}

        //public PNPITypeListEntity CreateNPIType([FromBody] CreateNPITypeDto createDto)
        //{
        //    try
        //    {

        //        var newNPIType = _mapper.Map<PNPITypeListEntity>(createDto);
        //        newNPIType.CreationDate = DateTime.Now;

        //        _context.PNPITypeList.Add(newNPIType);
        //        _context.SaveChanges();

        //        return newNPIType;

        //    }
        //    catch (Exception ex) { throw; }
        //}

        public bool IsValidNPI(string request)
        {
            int[] digits = request.Select(c => int.Parse(c.ToString())).ToArray();
            int checkDigitExpected = digits[digits.Length - 1];
            digits = digits.Take(9).ToArray();


            for (int i = 0; i <= digits.Length - 1; i += 2)
            {
                int doubledDigit = digits[i] * 2;
                digits[i] = doubledDigit > 9 ? doubledDigit - 9 : doubledDigit;
            }

            // Sum all the digits (including the doubled digits) and constant number
            int sum = digits.Sum() + 24;

            //Calculate the check digit
            int checkDigit = sum * 9 % 10;

            //Check if the last digit of the input matches the calculated check digit
            return checkDigitExpected == checkDigit;
        }
    }
}
