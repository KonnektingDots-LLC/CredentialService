using Newtonsoft.Json;

namespace cred_system_back_end_app.Infrastructure.PdfReport.DTO
{
    public class EducationAndTrainingDto
    {
        [JsonProperty("EducationSchool")]
        public EducationSchoolDto[]? EducationSchool { get; set; }

        [JsonProperty("EducationInternship")]
        public EducationInternshipDto[]? EducationInternship { get; set; }

        [JsonProperty("EducationResidency")]
        public EducationResidencyDto[]? EducationResidency { get; set; }

        [JsonProperty("EducationFellowship")]
        public EducationFellowshipDto[]? EducationFellowship { get; set; }

        [JsonProperty("EducationBoard")]
        public EducationBoardDto[]? EducationBoard { get; set; }
    }
}
