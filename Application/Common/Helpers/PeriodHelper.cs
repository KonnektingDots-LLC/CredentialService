﻿using cred_system_back_end_app.Application.UseCase.Submit.DTO;
using cred_system_back_end_app.Infrastructure.DB.Entity;

namespace cred_system_back_end_app.Application.Common.Helpers
{
    public static class PeriodHelper
    {
        public static PeriodEntity GetPeriodEntity(PeriodDTO periodDTO) 
        {
            return new PeriodEntity()
            {
                PeriodMonthFrom = periodDTO.FromMonth,
                PeriodYearFrom = periodDTO.FromYear,
                PeriodMonthTo = periodDTO.ToMonth,
                PeriodYearTo = periodDTO.ToYear,
            };
        }

        public static string GetFormattedStartDate(this PeriodEntity peridodData)
        {
            return $"{peridodData.PeriodMonthFrom}/{peridodData.PeriodYearFrom}";
        }

        public static string GetFormattedEndDate(this PeriodEntity peridodData)
        {
            return $"{peridodData.PeriodMonthTo}/{peridodData.PeriodYearTo}";
        }
    }
}
