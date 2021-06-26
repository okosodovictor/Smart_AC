﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SmartAC.Infrastructure.Database;

namespace SmartAC.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210121144339_AddDeviceIdToSensorReading")]
    partial class AddDeviceIdToSensorReading
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("SmartAC.Infrastructure.Entities.Client", b =>
                {
                    b.Property<Guid>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Secret")
                        .HasColumnType("text");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("text");

                    b.HasKey("ClientId");

                    b.HasIndex("SerialNumber")
                        .IsUnique();

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            ClientId = new Guid("8bd99aa7-539a-46f3-821b-dfce8ec57762"),
                            Secret = "a79b6fc9-3884-4232-84ef-0bd8a687fa12",
                            SerialNumber = "Device-001"
                        },
                        new
                        {
                            ClientId = new Guid("69aef910-b87e-415a-91d2-8bed0f54c545"),
                            Secret = "b17a76fc-5754-4422-8aa5-7c9b0db4401f",
                            SerialNumber = "Device-002"
                        },
                        new
                        {
                            ClientId = new Guid("2244f0ba-6e36-4b78-813f-021c749bfe47"),
                            Secret = "5733e2b1-bab9-442a-b671-59423e7a2b4c",
                            SerialNumber = "Device-003"
                        });
                });

            modelBuilder.Entity("SmartAC.Infrastructure.Entities.Device", b =>
                {
                    b.Property<Guid>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FirmwareVersion")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("RegistrationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("text");

                    b.HasKey("DeviceId");

                    b.HasIndex("SerialNumber")
                        .IsUnique();

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("SmartAC.Infrastructure.Entities.SensorReading", b =>
                {
                    b.Property<Guid>("SensorReadingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("AirHumidity")
                        .HasColumnType("numeric");

                    b.Property<decimal>("CarbonMonoxide")
                        .HasColumnType("numeric");

                    b.Property<Guid>("DeviceId")
                        .HasColumnType("uuid");

                    b.Property<string>("HealthStatus")
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<decimal>("Temperature")
                        .HasColumnType("numeric");

                    b.Property<DateTimeOffset>("TimeStamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("SensorReadingId");

                    b.HasIndex("DeviceId");

                    b.ToTable("SensorReadings");
                });

            modelBuilder.Entity("SmartAC.Infrastructure.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SmartAC.Infrastructure.Entities.SensorReading", b =>
                {
                    b.HasOne("SmartAC.Infrastructure.Entities.Device", "Device")
                        .WithMany("SensorReadings")
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Device");
                });

            modelBuilder.Entity("SmartAC.Infrastructure.Entities.Device", b =>
                {
                    b.Navigation("SensorReadings");
                });
#pragma warning restore 612, 618
        }
    }
}
