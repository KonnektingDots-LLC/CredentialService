﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;

#nullable disable

namespace cred_system_back_end_app.Migrations
{
    [DbContext(typeof(DbContextEntity))]
    [Migration("20230830200857_UpdateOrganizationTypeField")]
    partial class UpdateOrganizationTypeField
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.AddressServiceHourEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AddressId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DayOfWeek")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("HourFrom")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("HourTo")
                        .HasColumnType("time");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsClosed")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AddressServiceHourEntity");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.CitizenshipTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CitizenshipTypeEntityId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExpiredDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsExpired")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CitizenshipTypeEntityId");

                    b.ToTable("CitizenshipTypeEntity");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.DocumentCommentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DocumentCommentTypeId")
                        .HasColumnType("int");

                    b.Property<string>("DocumentLocationAzureBlobFilename")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("DocumentLocationId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DocumentCommentTypeId");

                    b.HasIndex("DocumentLocationAzureBlobFilename");

                    b.ToTable("DocumentComment");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.DocumentCommentTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DocumentCommentType");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.DocumentLocationEntity", b =>
                {
                    b.Property<string>("AzureBlobFilename")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ContainerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DocumentTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LetterDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrganizationTypeId")
                        .HasColumnType("int");

                    b.Property<int>("ProviderId")
                        .HasColumnType("int");

                    b.Property<long>("SizeInBytes")
                        .HasColumnType("bigint");

                    b.Property<string>("UploadBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UploadDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UploadFilename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AzureBlobFilename");

                    b.HasIndex("DocumentTypeId");

                    b.HasIndex("OrganizationTypeId");

                    b.ToTable("DocumentLocation");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.DocumentTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("hasNPI")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("DocumentType");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.InsurerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InsurerEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InsurerFullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InsurerTypeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProviderInsurerId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Insurer");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.JsonProviderFormEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<string>("JsonBody")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProviderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("JsonProviderForm");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.JsonProviderFormHistoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<string>("JsonBody")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProviderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("JsonProviderFormHistory");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.MultipleNPIEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CorporateName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NPI")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<int>("ProviderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProviderId");

                    b.ToTable("MultipleNPI");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.OrganizationTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExpiredDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsExpired")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("OrganizationType");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.ProviderDetailEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Blocked")
                        .HasColumnType("bit");

                    b.Property<string>("CitizenshipNumber")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("CitizenshipTypeId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("CurriculumVitaeEvidence")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("NPICertificateEvidence")
                        .HasColumnType("bit");

                    b.Property<string>("PRMedicalLicense")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<bool>("PenalRecordEvidence")
                        .HasColumnType("bit");

                    b.Property<int>("ProviderId")
                        .HasColumnType("int");

                    b.Property<string>("TaxIdNumber")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("UnderInvestigation")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CitizenshipTypeId");

                    b.HasIndex("ProviderId")
                        .IsUnique();

                    b.ToTable("ProviderDetail");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.ProviderEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BillingNPI")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProviderTypeId")
                        .HasColumnType("int");

                    b.Property<string>("RenderingNPI")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<bool>("SameAsRenderingNPI")
                        .HasColumnType("bit");

                    b.Property<string>("SurName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ProviderTypeId");

                    b.ToTable("Provider");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.ProviderTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("ExpiredDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsExpired")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("ProviderType");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.SignUpHistoryEntity", b =>
                {
                    b.Property<Guid>("IdB2C")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdB2C");

                    b.ToTable("SignUpHistory");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.SpecialtyListEntity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExpiredDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsExpired")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizationTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationTypeId");

                    b.ToTable("SpecialtyList");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.SubSpecialtyListEntity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ExpiredDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsExpired")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizationTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationTypeId");

                    b.ToTable("SubSpecialtyList");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.CitizenshipTypeEntity", b =>
                {
                    b.HasOne("cred_system_back_end_app.Infrastructure.DB.Entity.CitizenshipTypeEntity", null)
                        .WithMany("CitizenshipType")
                        .HasForeignKey("CitizenshipTypeEntityId");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.DocumentCommentEntity", b =>
                {
                    b.HasOne("cred_system_back_end_app.Infrastructure.DB.Entity.DocumentCommentTypeEntity", "DocumentCommentType")
                        .WithMany("DocumentComments")
                        .HasForeignKey("DocumentCommentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cred_system_back_end_app.Infrastructure.DB.Entity.DocumentLocationEntity", "DocumentLocation")
                        .WithMany("DocumentComments")
                        .HasForeignKey("DocumentLocationAzureBlobFilename")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentCommentType");

                    b.Navigation("DocumentLocation");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.DocumentLocationEntity", b =>
                {
                    b.HasOne("cred_system_back_end_app.Infrastructure.DB.Entity.DocumentTypeEntity", "DocumentType")
                        .WithMany("DocumentLocations")
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cred_system_back_end_app.Infrastructure.DB.Entity.OrganizationTypeEntity", "OrganizationType")
                        .WithMany()
                        .HasForeignKey("OrganizationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentType");

                    b.Navigation("OrganizationType");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.MultipleNPIEntity", b =>
                {
                    b.HasOne("cred_system_back_end_app.Infrastructure.DB.Entity.ProviderEntity", "Provider")
                        .WithMany("MultipleNPI")
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.ProviderDetailEntity", b =>
                {
                    b.HasOne("cred_system_back_end_app.Infrastructure.DB.Entity.CitizenshipTypeEntity", "CitizenshipType")
                        .WithMany()
                        .HasForeignKey("CitizenshipTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cred_system_back_end_app.Infrastructure.DB.Entity.ProviderEntity", "Provider")
                        .WithOne("ProviderDetail")
                        .HasForeignKey("cred_system_back_end_app.Infrastructure.DB.Entity.ProviderDetailEntity", "ProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CitizenshipType");

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.ProviderEntity", b =>
                {
                    b.HasOne("cred_system_back_end_app.Infrastructure.DB.Entity.ProviderTypeEntity", "ProviderType")
                        .WithMany("Provider")
                        .HasForeignKey("ProviderTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProviderType");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.SpecialtyListEntity", b =>
                {
                    b.HasOne("cred_system_back_end_app.Infrastructure.DB.Entity.OrganizationTypeEntity", "OrganizationType")
                        .WithMany("Specialty")
                        .HasForeignKey("OrganizationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrganizationType");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.SubSpecialtyListEntity", b =>
                {
                    b.HasOne("cred_system_back_end_app.Infrastructure.DB.Entity.OrganizationTypeEntity", "OrganizationType")
                        .WithMany("SubSpecialty")
                        .HasForeignKey("OrganizationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrganizationType");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.CitizenshipTypeEntity", b =>
                {
                    b.Navigation("CitizenshipType");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.DocumentCommentTypeEntity", b =>
                {
                    b.Navigation("DocumentComments");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.DocumentLocationEntity", b =>
                {
                    b.Navigation("DocumentComments");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.DocumentTypeEntity", b =>
                {
                    b.Navigation("DocumentLocations");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.OrganizationTypeEntity", b =>
                {
                    b.Navigation("Specialty");

                    b.Navigation("SubSpecialty");
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.ProviderEntity", b =>
                {
                    b.Navigation("MultipleNPI");

                    b.Navigation("ProviderDetail")
                        .IsRequired();
                });

            modelBuilder.Entity("cred_system_back_end_app.Infrastructure.DB.Entity.ProviderTypeEntity", b =>
                {
                    b.Navigation("Provider");
                });
#pragma warning restore 612, 618
        }
    }
}
