using System.Net;

namespace cred_system_back_end_app.API.Exception
{
    public enum ExceptionMapper
    {

        [ErrorInfo((int)HttpStatusCode.NotFound, "Provider Not Found")]
        ProviderNotFoundException = 1,

        [ErrorInfo((int)HttpStatusCode.NotFound, "User Not Found")]
        UserNotFoundException = 2,

        [ErrorInfo((int)HttpStatusCode.BadRequest, "Email Request Failed")]
        EmailFailedException = 3,

        [ErrorInfo((int)HttpStatusCode.NotFound, "Email is not valid")]
        EmailNotValidException = 4,

        [ErrorInfo((int)HttpStatusCode.NotFound, "Delegate Not Found")]
        DelegateNotFoundException = 5,

        [ErrorInfo((int)HttpStatusCode.NotFound, "Insurer Not Found")]
        InsurerNotFoundException = 6,

        [ErrorInfo((int)HttpStatusCode.InternalServerError, "Application Exception")]
        ApplicationException = 7,

        [ErrorInfo((int)HttpStatusCode.InternalServerError, "Smtp Authentication Exception")]
        AuthenticationException = 8,

        [ErrorInfo((int)HttpStatusCode.NotFound, "Entity Not Found Exception")]
        EntityNotFoundException = 9,

        [ErrorInfo((int)HttpStatusCode.BadRequest, "Invalid Password Exception")]
        InvalidPasswordException = 10,

        [ErrorInfo((int)HttpStatusCode.BadRequest, "Invalid Secret Key Exception")]
        InvalidSecretKeyException = 11,

        [ErrorInfo((int)HttpStatusCode.BadRequest, "Log Exception")]
        LogException = 12,

        [ErrorInfo((int)HttpStatusCode.BadRequest, "Request Exception")]
        RequestInvalidException = 13,

        [ErrorInfo((int)HttpStatusCode.BadRequest, "Storage Account Exception")]
        StorageAccountException = 14,

        [ErrorInfo((int)HttpStatusCode.NotFound, "Corporation Not Found")]
        CorporationNotFoundException = 15,

        [ErrorInfo((int)HttpStatusCode.NotFound, "Specialty Not Found")]
        SpecialtyNotFoundException = 16,

        [ErrorInfo((int)HttpStatusCode.NotFound, "SpecialtyList Not Found")]
        SpecialtyListNotFoundException = 17,

        [ErrorInfo((int)HttpStatusCode.NotFound, "SubSpecialty Not Found")]
        SubSpecialtyNotFoundException = 18,

        [ErrorInfo((int)HttpStatusCode.NotFound, "SubSpecialtyList Not Found")]
        SubSpecialtyListNotFoundException = 19,

        [ErrorInfo((int)HttpStatusCode.NotFound, "Hospital Not Found")]
        HospitalNotFoundException = 20,

        [ErrorInfo((int)HttpStatusCode.NotFound, "EdFellowshipInstitution Not Found")]
        EdFellowshipInstitutionNotFoundException = 21,

        [ErrorInfo((int)HttpStatusCode.NotFound, "EdInternshipInstitution Not Found")]
        EdInternshipInstitutionNotFoundException = 22,

        [ErrorInfo((int)HttpStatusCode.NotFound, "EdMedicalSchool Not Found")]
        EdMedicalSchoolNotFoundException = 23,

        [ErrorInfo((int)HttpStatusCode.NotFound, "EdResidencyInstitution Not Found")]
        EdResidencyInstitutionNotFoundException = 24,

        [ErrorInfo((int)HttpStatusCode.NotFound, "ProviderType Not Found")]
        ProviderTypeNotFoundException = 25,

        [ErrorInfo((int)HttpStatusCode.NotFound, "Address State Not Found")]
        AddressStateNotFoundException = 26,

        [ErrorInfo((int)HttpStatusCode.NotFound, "Address Type Not Found")]
        AddressTypeNotFoundException = 27,

        [ErrorInfo((int)HttpStatusCode.NotFound, "Hospital Privilege Not Found")]
        HospitalPrivilegeNotFoundException = 28,

        [ErrorInfo((int)HttpStatusCode.NotFound, "Malpractice Carrier Not Found")]
        MalpracticeNotFoundException = 29,

        [ErrorInfo((int)HttpStatusCode.NotFound, "Plan Accept Not Found")]
        PlanAcceptNotFoundException = 30,

        [ErrorInfo((int)HttpStatusCode.NotFound, "Document Type Not Found")]
        DocumentTypeException = 31,

        [ErrorInfo((int)HttpStatusCode.NotFound, "Document Type Not Found")]
        ProfessionalLiabilityNotFoundException = 32,

        [ErrorInfo((int)HttpStatusCode.NotFound, "Document Not Found")]
        DocumentNotFoundException = 33,

        [ErrorInfo((int)HttpStatusCode.Conflict, "Insurer already exists")]
        InsurerEmployeeDuplicateException = 34,

        [ErrorInfo((int)HttpStatusCode.NotFound, "Insurer Admin Not Found")]
        InsurerAdminNotFoundException = 35,

        [ErrorInfo((int)HttpStatusCode.NotFound, "OCS Admin Not Found")]
        OCSAdminNotFoundException = 36,

        [ErrorInfo((int)HttpStatusCode.Unauthorized, "Access Denied")]
        AccessDeniedException = 37,

        [ErrorInfo((int)HttpStatusCode.NotFound, "Insurer Company Not Found")]
        InsurerCompanyNotFoundException = 38,

        [ErrorInfo((int)HttpStatusCode.NotFound, "Country Not Found")]
        AddressCountryNotFoundException = 40,

        [ErrorInfo((int)HttpStatusCode.Unauthorized, "Not new record is allowed")]
        DeniedNewRecordException = 41,

        [ErrorInfo((int)HttpStatusCode.Unauthorized, "File size out of range")]
        FileSizeException = 42,



        [ErrorInfo((int)HttpStatusCode.InternalServerError, "An error occurred while processing your request.")]
        Default = 9999
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ErrorInfoAttribute : Attribute
    {
        public int ErrorStatusCode { get; }
        public string ErrorMessage { get; }
        public ErrorInfoAttribute(int errorCode, string errorMessage)
        {
            ErrorStatusCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }

}
