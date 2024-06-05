// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using cred_system_back_end_app.Domain.Services.Submit.DTO;
using cred_system_back_end_app.Domain.Services.Submit.DTO.EducationDTOs;
using System.Text.Json.Serialization;

public class SubmitRequestDTO
{
    [JsonPropertyName("jsonSubmit")]
    public SubmitContentRequestDTO Content { get; set; }
    public string JsonProviderForm { get; set; }

}
public class SubmitContentRequestDTO
{
    public SetupDTO Setup { get; set; }
    public IndividualPracticeProfileDTO IndividualPracticeProfile { get; set; }
    public List<CorporationDTO>? IncorporatedPracticeProfile { get; set; }
    public List<AddressAndLocationDTO> AddressAndLocation { get; set; }
    public SpecialtiesAndSubspecialtiesDTO SpecialtiesAndSubspecialties { get; set; }
    public MedicalGroupDTO? Pcp { get; set; }
    public MedicalGroupDTO? F330 { get; set; }
    public HospitalAffiliationsDTO? HospitalAffiliations { get; set; }
    public EducationAndTrainingDTO EducationAndTraining { get; set; }

    public CriminalRecordDTO CriminalRecord { get; set; }
    public InsuranceDTO? Insurance { get; set; }
    public AttestationDTO Attestation { get; set; }
}
