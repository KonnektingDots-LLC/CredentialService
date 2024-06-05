using Azure.Identity;
using cred_system_back_end_app.Application.Common.Mapper;
using cred_system_back_end_app.Application.CRUD.Address;
using cred_system_back_end_app.Application.CRUD.Document;
using cred_system_back_end_app.Application.CRUD.Hospital;
using cred_system_back_end_app.Application.CRUD.Insurance;
using cred_system_back_end_app.Application.CRUD.Insurer;
using cred_system_back_end_app.Application.CRUD.NPI;
using cred_system_back_end_app.Application.CRUD.Provider;
using cred_system_back_end_app.Application.CRUD.ProviderDraft;
using cred_system_back_end_app.Application.CRUD.Specialty;
using cred_system_back_end_app.Application.CRUD.SubSpecialty;
using cred_system_back_end_app.Application.UseCase.SaveJsonDraft;
using cred_system_back_end_app.Application.UseCase.Notifications;
using cred_system_back_end_app.Application.UseCase.Submit;
using cred_system_back_end_app.Controllers;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.FileSystem.MultiFileUpload;
using cred_system_back_end_app.Infrastructure.PdfReport.CredentialingApplication;
using cred_system_back_end_app.Infrastructure.Settings;
using cred_system_back_end_app.Infrastructure.Smpt;
using cred_system_back_end_app.Infrastructure.Smtp.DelegateInvitationNotification;
using cred_system_back_end_app.Infrastructure.Smtp.ProfileCompletionNotification;
using cred_system_back_end_app.Infrastructure.Smtp.ProviderReviewNotification;
using Microsoft.EntityFrameworkCore;
using cred_system_back_end_app.Infrastructure.FileSystem.GetDocument;
using cred_system_back_end_app.Application.UseCase.Delegate;
using Microsoft.Identity.Web;
using cred_system_back_end_app;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Logging;
using cred_system_back_end_app.Infrastructure.B2C;
using cred_system_back_end_app.Application.CRUD.PDFData;
using cred_system_back_end_app.Application.CRUD.AssociativeEntities;
using cred_system_back_end_app.Application.CRUD.MedicalGroup;
using cred_system_back_end_app.Application.UseCase.Corporation;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.UseCase.Insurer;
using cred_system_back_end_app.Infrastructure.Smtp.InsurerInvitationNotification;
using cred_system_back_end_app.Application.UseCase.OCSAdmin;
using cred_system_back_end_app.Application.CRUD.OCSAdmin;
using cred_system_back_end_app.Application.UseCase.Provider;
using cred_system_back_end_app.Application.Common.Services;
using cred_system_back_end_app.Infrastructure.Smtp.ProviderSubmitToInsurerNotification;
using cred_system_back_end_app.Infrastructure.Smtp.ProviderSubmitToDelegateNotification;
using cred_system_back_end_app.Infrastructure.Smtp.ProviderSubmitNotification;
using cred_system_back_end_app.Application.CRUD.Delegate;
using cred_system_back_end_app.Application.CRUD.Notification;
using cred_system_back_end_app.Infrastructure.Smtp.DelegateStatusUpdateNotification;
using cred_system_back_end_app.Infrastructure.Smtp.ProviderDelegateUpdateNotification;
using cred_system_back_end_app.Infrastructure.PdfReport.DTO;
using cred_system_back_end_app.Application.Common.Services.PDFServices.AttestationPDF;
using cred_system_back_end_app.Application.Common.Services.PDFServices;
using cred_system_back_end_app.Application.CRUD.IIPCAFormSections;
using cred_system_back_end_app.Application.UseCase.CredForm;
using cred_system_back_end_app.Application.CRUD.CredForm;
using cred_system_back_end_app.Application.UseCase.Submit.ResubmitServices;
using cred_system_back_end_app.Application.UseCase.Submit.ResubmitServices.EducationResubmitServices;
using cred_system_back_end_app.Application.Common.EqualityComparers;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using cred_system_back_end_app.Application.CRUD.ChangeLogservices;
using cred_system_back_end_app.Application.UseCase.ProviderInsurerCompanyStatus;
using cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatusHistory;
using cred_system_back_end_app.Application.CRUD.ProviderInsurerCompanyStatus;
using cred_system_back_end_app.Application.UseCase.Notifications.NotificationManagers;
using Microsoft.Identity.Client;
using cred_system_back_end_app.Infrastructure.Smtp.InsurerToProviderStatusNotification;
using cred_system_back_end_app.Infrastructure.Smtp.InsurerToProviderStatusRTPNotification;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment}.json", optional: true)
    .AddEnvironmentVariables();

var hasb2c = true;

builder.Services.AddControllers(options =>
{
    //options.Filters.Add(typeof(B2CTokenMiddleware));
    //options.Filters.Add<B2CTokenMiddleware>();
    options.Filters.Add<ExceptionHandler>();// Global Handler Exception

    if (hasb2c)
    {
        var policy = new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .Build();

        options.Filters.Add(new AuthorizeFilter(policy));
        //options.Filters.Add(new AuthorizeFilter(new AuthorizeFilter((IEnumerable<IAuthorizeData>)typeof(MyCustomAuthAttribute))));
    }

});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<FireAndForgetService>();
builder.Services.AddScoped<SubmitNotificationManager>();
builder.Services.AddScoped<ProviderSubmitToInsurerCase>();
builder.Services.AddScoped<ProviderSubmitToDelegateCase>();
builder.Services.AddScoped<ProviderSubmitCase>();
builder.Services.AddScoped<ProviderSubmitToInsurerNotificationEmail>();
builder.Services.AddScoped<ProviderSubmitToDelegateNotificationEmail>();
builder.Services.AddScoped<ProviderSubmitNotificationEmail>();
builder.Services.AddScoped<SubmitCase>();
builder.Services.AddScoped<SubmitDBContextManager>();
builder.Services.AddScoped<ProviderRepository>();
builder.Services.AddScoped<SubSpecialtyRepository>();
builder.Services.AddScoped<ProviderCorporationRepository>();
builder.Services.AddScoped<ProviderMedicalGroupRepository>();
builder.Services.AddScoped<MedicalGroupRepository>();
builder.Services.AddScoped<CorporationRepository>();
builder.Services.AddScoped<PDFDataRepository>();
builder.Services.AddScoped<InviteDelegateCase>();
builder.Services.AddScoped<ProviderReviewCase>();
builder.Services.AddScoped<ProfileCompleteCase>();
builder.Services.AddScoped<DelegateCase>();
builder.Services.AddScoped<ProviderByDelegateCase>();
builder.Services.AddScoped<ProviderRepository>();
builder.Services.AddScoped<SmtpClient>();
builder.Services.AddScoped<InsurerCase>();
builder.Services.AddScoped<PdfGeneratorClient<IIPCAPdfRootDto>>();
builder.Services.AddScoped<PdfGeneratorClient<AttestationPDFRequestDTO>>();
builder.Services.AddScoped<IIPCAPDFService>();
builder.Services.AddScoped<AttestationPDFService>();
builder.Services.AddScoped<AttestationRepository>();
builder.Services.AddScoped<AddressCase>();
builder.Services.AddScoped<NPICase>();
builder.Services.AddScoped<DelegateInvitationNotificationEmail>();
builder.Services.AddScoped<ProviderReviewNotificationEmail>();
builder.Services.AddScoped<ProfileCompletionNotificationEmail>();
builder.Services.AddScoped<SpecialtyCase>();
builder.Services.AddScoped<SubSpecialtyRepository>();
builder.Services.AddScoped<DelegateRepository>();
builder.Services.AddScoped<DocumentCase>();
builder.Services.AddScoped<HospitalCase>();
builder.Services.AddScoped<ProviderDraftCase>();
builder.Services.AddScoped<InsuranceCase>();
builder.Services.AddScoped<MultiFileUploadCase>();
builder.Services.AddScoped<SaveJsonDraftCase>();
builder.Services.AddScoped<GetDocumentCase>();
builder.Services.AddScoped<InsurerUseCase>();
builder.Services.AddScoped<InsurerAdminRepository>();
builder.Services.AddScoped<ProviderRepository>();
builder.Services.AddScoped<InsurerEmployeeRepository>();
builder.Services.AddScoped<GetB2CInfo>();
builder.Services.AddScoped<InviteInsurerCase>();
builder.Services.AddScoped<InsurerInvitationNotificationEmail>();
builder.Services.AddScoped<OCSAdminUseCase>();
builder.Services.AddScoped<OCSAdminRepository>();
builder.Services.AddScoped<InsurerCompanyRepository>();
builder.Services.AddScoped<ProviderUseCase>();
builder.Services.AddScoped<SaveNotificationCase>();
builder.Services.AddScoped<NotificationProfileCompletionDetailRepository>();
builder.Services.AddScoped<DelegateStatusUpdateNotificationManager>();
builder.Services.AddScoped<NotificationManagerBase>();
builder.Services.AddScoped<DelegateStatusUpdateEmail>();
builder.Services.AddScoped<ProviderDelegateStatusUpdate>();
builder.Services.AddScoped<ProviderDelegateStatusNotificationManager>();
builder.Services.AddScoped<IndividualPracticeProfileModificationService>();
builder.Services.AddScoped<SpecialtiesModificationService>();
builder.Services.AddScoped<ProviderSpecialtyComparer>();
builder.Services.AddScoped<ProviderSubspecialtyComparer>();
builder.Services.AddScoped<AddressAndLocationModificationService>();
builder.Services.AddScoped<IncorporatedProfileModificationService>();
builder.Services.AddScoped<MedicalGroupModificationService>();
builder.Services.AddScoped<HospitalAffiliationModificationService>();
builder.Services.AddScoped<MedicalSchoolModificationService>();
builder.Services.AddScoped<FellowshipModificationService>();
builder.Services.AddScoped<InternshipModificationService>();
builder.Services.AddScoped<ResidencyModificationService>();
builder.Services.AddScoped<BoardModificationService>();
builder.Services.AddScoped<BoardSpecialtyComparer>();
builder.Services.AddScoped<LicensesModificationService>();
builder.Services.AddScoped<MalpracticeModificationService>();
builder.Services.AddScoped<OIGNumberComparer>();
builder.Services.AddScoped<ProfessionalLiabilityModificationService>();
builder.Services.AddScoped<CorporationSubSpecialtyComparer>();
builder.Services.AddScoped<CredFormUseCase>();
builder.Services.AddScoped<CredFormRepository>();
builder.Services.AddScoped<ChangeLogRepository>();
builder.Services.AddScoped<ProviderInsurerCompanyStatusHistoryRepository>();
builder.Services.AddScoped<ProviderInsurerCompanyStatusRepository>();
builder.Services.AddScoped<ProviderInsurerCompanyStatusUseCase>();
builder.Services.AddScoped<ProviderSubmitNotificationManager>();
builder.Services.AddScoped<InsurerSubmitNotificationManager>();
builder.Services.AddScoped<DelegateSubmitNotificationManager>();
builder.Services.AddScoped<InsurerToProviderStatusNotificationManager>();
builder.Services.AddScoped<InsurerToProviderStatusRTPNotificationManager>();
builder.Services.AddScoped<InsurerToProviderStatusCase>();
builder.Services.AddScoped<InsurerToProviderStatusRTPCase>();
builder.Services.AddScoped<InsurerToProviderStatusEmail>();
builder.Services.AddScoped<InsurerToProviderStatusRTPEmail>();

if (builder.Environment.IsDevelopment())
{
    var sqlConnectionString = builder.Configuration.GetConnectionString("SqlServer");
    //sqlConnectionString = builder.Configuration.GetConnectionString("DevSqlServer");
    builder.Services.AddDbContext<DbContextEntity>(
        o => o.UseSqlServer(sqlConnectionString).EnableDetailedErrors(true).EnableSensitiveDataLogging(true));

    //365
    builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

    //Blob
    builder.Services.Configure<BlobSetting>(builder.Configuration.GetSection("AzureStorageAccount"));

    IdentityModelEventSource.ShowPII = true;
}
else
{

    //PROD
    string keyVaultEndpoint = builder.Configuration.GetSection("AzureKeyVault:KeyVaultURL").Value;
    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultEndpoint), new DefaultAzureCredential())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


    builder.Services.AddDbContext<DbContextEntity>(
            o => o.UseSqlServer(builder.Configuration["DatabaseConnectionString"]));

    ////365
    //builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

    ////Blob
    //builder.Services.Configure<BlobSetting>(builder.Configuration.GetSection("AzureStorageAccount"));
}

if (hasb2c)
{
    builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration, "AzureAdB2C");
    builder.Services.AddAzureAppConfiguration().AddAuthenticationWithAuthorizationSupport(builder.Configuration);

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(CredPolicy.ACCESS_AS_DELEGATE,
             policy => policy.RequireClaim(CredTokenKey.ROLE, CredRole.DELEGATE));

        options.AddPolicy(CredPolicy.ACCESS_AS_PROVIDER,
             policy => policy.RequireClaim(CredTokenKey.ROLE, CredRole.PROVIDER));

        string[] PD = { CredRole.PROVIDER, CredRole.DELEGATE};
        options.AddPolicy(CredPolicy.ACCESS_AS_PD,
                 policy => policy.RequireClaim(CredTokenKey.ROLE,PD));

        options.AddPolicy(CredPolicy.ACCESS_AS_INSURER,
             policy => policy.RequireClaim(CredTokenKey.ROLE, CredRole.INSURER));

        options.AddPolicy(CredPolicy.ACCESS_AS_ADMIN_INSURER,
             policy => policy.RequireClaim(CredTokenKey.ROLE, CredRole.ADMIN_INSURER));

        options.AddPolicy(CredPolicy.ACCESS_AS_ADMIN,
            policy => policy.RequireClaim(CredTokenKey.ROLE, CredRole.ADMIN));

        string[] IAA = { CredRole.INSURER, CredRole.ADMIN_INSURER, CredRole.ADMIN };
        options.AddPolicy(CredPolicy.ACCESS_AS_IAA,
                 policy => policy.RequireClaim(CredTokenKey.ROLE, IAA));

        string[] IAINS = { CredRole.INSURER, CredRole.ADMIN_INSURER };
        options.AddPolicy(CredPolicy.ACCESS_AS_IAINS,
                 policy => policy.RequireClaim(CredTokenKey.ROLE, IAINS));

        string[] PAD = { CredRole.PROVIDER, CredRole.ADMIN, CredRole.DELEGATE};
        options.AddPolicy(CredPolicy.ACCESS_AS_PAD,
                 policy => policy.RequireClaim(CredTokenKey.ROLE, PAD));

        //ALL
        string[] ALL = { CredRole.INSURER, CredRole.ADMIN_INSURER, CredRole.ADMIN,
                        CredRole.DELEGATE, CredRole.PROVIDER };
        options.AddPolicy(CredPolicy.ACCESS_AS_ALL,
                 policy => policy.RequireClaim(CredTokenKey.ROLE, ALL));
    });

}


//Mappper
builder.Services.AddAutoMapper(typeof(MappingProfile));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.UseMiddleware<ExceptionHandler>();//ExceptionHandler
}
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
