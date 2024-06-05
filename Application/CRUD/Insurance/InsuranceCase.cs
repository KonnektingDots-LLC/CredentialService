using AutoMapper;
using cred_system_back_end_app.Application.Common.ExceptionHandling.ExceptionClass;
using cred_system_back_end_app.Application.CRUD.Insurance.DTO;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;

namespace cred_system_back_end_app.Application.CRUD.Insurance
{
    public class InsuranceCase
    {
        private readonly DbContextEntity _context;
        private readonly IMapper _mapper;

        public InsuranceCase(DbContextEntity context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<InsuranceCarrierListResponseDto> GetAllMalpracticeCarrier()
        {
            var malpracticeCarriers = _context.MalpracticeCarrierList.ToList();
            if (malpracticeCarriers.Count == 0) { throw new MalpracticeNotFoundException(); };
            return _mapper.Map<List<InsuranceCarrierListResponseDto>>(malpracticeCarriers);
        }


        public List<InsuranceCarrierListResponseDto> GetAllProfessionalLiabilityCarrier()
        {
            var proliaCarriers = _context.ProfessionalCarrierList.ToList();
            if (proliaCarriers.Count == 0) { throw new ProfessionalLiabilityNotFoundException(); };
            return _mapper.Map<List<InsuranceCarrierListResponseDto>>(proliaCarriers);
        }
    }
}
