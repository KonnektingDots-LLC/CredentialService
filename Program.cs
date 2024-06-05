using Autofac;
using Autofac.Extensions.DependencyInjection;
using Azure.Identity;
using cred_system_back_end_app;
using cred_system_back_end_app.API.Exception;
using cred_system_back_end_app.Application.Behavior;
using cred_system_back_end_app.Application.Common.Constants;
using cred_system_back_end_app.Application.Common.EqualityComparers;
using cred_system_back_end_app.Application.Common.Helpers;
using cred_system_back_end_app.Application.Common.Mappers;
using cred_system_back_end_app.Application.Common.Services;
using cred_system_back_end_app.Application.Delegates.Queries.Handlers;
using cred_system_back_end_app.Application.Providers.Commands.Handlers;
using cred_system_back_end_app.Domain;
using cred_system_back_end_app.Domain.Services;
using cred_system_back_end_app.Domain.Services.Submit;
using cred_system_back_end_app.Domain.Services.Submit.ModificationServices;
using cred_system_back_end_app.Domain.Services.Submit.ModificationServices.EducationModificationServices;
using cred_system_back_end_app.Domain.Settings;
using cred_system_back_end_app.Infrastructure;
using cred_system_back_end_app.Infrastructure.AzureBlobStorage;
using cred_system_back_end_app.Infrastructure.B2C;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;
using cred_system_back_end_app.Infrastructure.Data.Repositories;
using cred_system_back_end_app.Infrastructure.PdfReport.CredentialingApplication;
using cred_system_back_end_app.Infrastructure.PdfReport.DTO;
using cred_system_back_end_app.Infrastructure.PdfReport.PDFServices;
using cred_system_back_end_app.Infrastructure.PdfReport.PDFServices.AttestationPDF;
using cred_system_back_end_app.Infrastructure.Smtp;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using Serilog;
using Serilog.Formatting.Compact;
using System.Reflection;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting up!");

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
    builder.Services.AddSwaggerGen(options => {
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
    // Decorator to log the Mediator calls (MediatR)
    builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    // Decorator to db transaction
    builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

    builder.Services.AddScoped<FireAndForgetService>();
    builder.Services.AddScoped<SubmitNotificationManager>();
    builder.Services.AddScoped<SubmitCredentialingFormHandler>();
    builder.Services.AddScoped<SubmitDBContextManager>();
    builder.Services.AddScoped<PDFDataRepository>();
    builder.Services.AddScoped<GetProvidersByDelegateIdHandler>();
    builder.Services.AddScoped<SmtpClient>();
    builder.Services.AddScoped<PdfGeneratorClient<IIPCAPdfRootDto>>();
    builder.Services.AddScoped<PdfGeneratorClient<AttestationPDFRequestDTO>>();
    builder.Services.AddScoped<IIPCAPDFService>();
    builder.Services.AddScoped<AttestationPDFService>();
    builder.Services.AddScoped<NpiHelper>();
    builder.Services.AddScoped<DocumentUploadService>();
    builder.Services.AddScoped<MultiFileUploadService>();
    builder.Services.AddScoped<DocumentDownloadService>();
    builder.Services.AddScoped<GetB2CInfo>();
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

            string[] PD = { CredRole.PROVIDER, CredRole.DELEGATE };
            options.AddPolicy(CredPolicy.ACCESS_AS_PD,
                     policy => policy.RequireClaim(CredTokenKey.ROLE, PD));

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

            string[] PAD = { CredRole.PROVIDER, CredRole.ADMIN, CredRole.DELEGATE };
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


    builder.Services.AddHttpContextAccessor();
    builder.Host.UseSerilog((context, services, configuration) =>
    {
        configuration.ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}] {Level} {TransactionId} {Message:lj}{NewLine}{Exception}")
        // This is only for Dev purpose
        .WriteTo.File(new CompactJsonFormatter(), "./logs/app.json", rollingInterval: RollingInterval.Day, shared: true)
        // This is to write to the Azure Diagnostics Log Stream (VM).
        .WriteTo.File(Path.Combine(Environment.GetEnvironmentVariable("HOME") ?? "logs", "LogFiles", "Application", "diagnostics.txt")
            , rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 10 * 1024 * 1024, retainedFileCountLimit: 2
            , rollOnFileSizeLimit: true, shared: true, flushToDiskInterval: TimeSpan.FromSeconds(1));
    });

    // Autofac IoC
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new CoreServiceModule());
        containerBuilder.RegisterModule(new InfrastructureServiceModule());
    });
    // Logger
    var app = builder.Build();

    // Inject logger transaction id to all request
    app.Use(async (context, next) =>
    {
        context.Request.Headers.Add("x-transaction-id", Guid.NewGuid().ToString());
        await next();
    });

    // Enabling request logging
    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

        // Adding aditional data - currently only a unique transaction id
        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        {
            diagnosticContext.Set("TransactionId", httpContext.Request.Headers["x-transaction-id"][0]);
        };
    });

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
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandle exeption ocurred during bootstrapping.");
}
finally
{
    Log.CloseAndFlush();
}