using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BasicMedicalHistory.Models;

namespace BasicMedicalHistory.Migrations
{
    [DbContext(typeof(BmhContext))]
    [Migration("20160617215102_addedCustUserNameToModels")]
    partial class addedCustUserNameToModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BasicMedicalHistory.Models.Allergy", b =>
                {
                    b.Property<int>("AllergyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CustUserName");

                    b.Property<int>("CustomerId");

                    b.Property<string>("Name");

                    b.Property<string>("Notes");

                    b.Property<string>("Reaction");

                    b.Property<bool>("ShowOnPublicView");

                    b.HasKey("AllergyId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Allergy");
                });

            modelBuilder.Entity("BasicMedicalHistory.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BirthDate");

                    b.Property<string>("BloodType");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CustAddress");

                    b.Property<string>("CustCity");

                    b.Property<string>("CustEmail");

                    b.Property<string>("CustFirst");

                    b.Property<string>("CustLast");

                    b.Property<string>("CustPhone");

                    b.Property<string>("CustState");

                    b.Property<string>("CustUserName");

                    b.Property<string>("EyeColor");

                    b.Property<string>("Gender");

                    b.Property<string>("Hair");

                    b.Property<string>("Height");

                    b.Property<string>("QrCode");

                    b.Property<string>("Weight");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("BasicMedicalHistory.Models.CustomerMed", b =>
                {
                    b.Property<int>("CustomerMedId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CustUserName");

                    b.Property<int>("CustomerId");

                    b.Property<string>("Frequency");

                    b.Property<int>("MedicationId");

                    b.Property<string>("Notes");

                    b.Property<bool>("ShowOnPublicView");

                    b.Property<string>("Usage");

                    b.HasKey("CustomerMedId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("MedicationId");

                    b.ToTable("CustomerMed");
                });

            modelBuilder.Entity("BasicMedicalHistory.Models.EmContact", b =>
                {
                    b.Property<int>("EmContactId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CustUserName");

                    b.Property<int>("CustomerId");

                    b.Property<string>("EmContactName");

                    b.Property<string>("EmergencyContactPhone");

                    b.Property<string>("Relationship");

                    b.Property<bool>("ShowOnPublicView");

                    b.HasKey("EmContactId");

                    b.HasIndex("CustomerId");

                    b.ToTable("EmContact");
                });

            modelBuilder.Entity("BasicMedicalHistory.Models.Insurance", b =>
                {
                    b.Property<int>("InsuranceId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BIN");

                    b.Property<string>("CustUserName");

                    b.Property<int>("CustomerId");

                    b.Property<int>("Deducatable");

                    b.Property<string>("GroupNumber");

                    b.Property<string>("IdNumber");

                    b.Property<string>("InsuranceProvider");

                    b.Property<string>("Notes");

                    b.Property<string>("Phone");

                    b.Property<bool>("ShowOnPublicView");

                    b.HasKey("InsuranceId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Insurance");
                });

            modelBuilder.Entity("BasicMedicalHistory.Models.MedicalCondition", b =>
                {
                    b.Property<int>("MedicalConditionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CustUserName");

                    b.Property<int>("CustomerId");

                    b.Property<string>("Description");

                    b.Property<string>("MedicalConditionName");

                    b.Property<bool>("ShowOnPublicView");

                    b.HasKey("MedicalConditionId");

                    b.HasIndex("CustomerId");

                    b.ToTable("MedicalCondition");
                });

            modelBuilder.Entity("BasicMedicalHistory.Models.Medication", b =>
                {
                    b.Property<int>("MedicationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BrandName");

                    b.Property<string>("Dosage");

                    b.Property<string>("DrugInteractions");

                    b.Property<string>("GenericName");

                    b.Property<string>("SideEffects");

                    b.HasKey("MedicationId");

                    b.ToTable("Medication");
                });

            modelBuilder.Entity("BasicMedicalHistory.Models.Physician", b =>
                {
                    b.Property<int>("PhysicianId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("BusinessName");

                    b.Property<string>("City");

                    b.Property<string>("CustUserName");

                    b.Property<int>("CustomerId");

                    b.Property<string>("Phone");

                    b.Property<string>("PhysicianName");

                    b.Property<bool>("ShowOnPublicView");

                    b.Property<string>("State");

                    b.Property<string>("Title");

                    b.HasKey("PhysicianId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Physician");
                });

            modelBuilder.Entity("BasicMedicalHistory.Models.Allergy", b =>
                {
                    b.HasOne("BasicMedicalHistory.Models.Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BasicMedicalHistory.Models.CustomerMed", b =>
                {
                    b.HasOne("BasicMedicalHistory.Models.Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BasicMedicalHistory.Models.Medication")
                        .WithMany()
                        .HasForeignKey("MedicationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BasicMedicalHistory.Models.EmContact", b =>
                {
                    b.HasOne("BasicMedicalHistory.Models.Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BasicMedicalHistory.Models.Insurance", b =>
                {
                    b.HasOne("BasicMedicalHistory.Models.Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BasicMedicalHistory.Models.MedicalCondition", b =>
                {
                    b.HasOne("BasicMedicalHistory.Models.Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BasicMedicalHistory.Models.Physician", b =>
                {
                    b.HasOne("BasicMedicalHistory.Models.Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
