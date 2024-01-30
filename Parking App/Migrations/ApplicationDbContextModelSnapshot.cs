﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Parking_App;

#nullable disable

namespace Parking_App.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Parking_App.Entities.Estancia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("HoraIngreso")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("HoraSalida")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TicketId")
                        .HasColumnType("int");

                    b.Property<int>("VehiculoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.HasIndex("VehiculoId");

                    b.ToTable("Estancias");
                });

            modelBuilder.Entity("Parking_App.Entities.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("HoraEmision")
                        .HasColumnType("datetime2");

                    b.Property<int>("HorasCobradas")
                        .HasColumnType("int");

                    b.Property<decimal>("ImporteCobrado")
                        .HasPrecision(4, 2)
                        .HasColumnType("decimal(4,2)");

                    b.Property<int>("VehiculoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehiculoId");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("Parking_App.Entities.TipoVehiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Tarifa")
                        .HasPrecision(4, 2)
                        .HasColumnType("decimal(4,2)");

                    b.HasKey("Id");

                    b.ToTable("TipoVehiculos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "Oficial",
                            Tarifa = 0m
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "Residente",
                            Tarifa = 0.05m
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "No Residente",
                            Tarifa = 0.5m
                        });
                });

            modelBuilder.Entity("Parking_App.Entities.Vehiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<int>("IdTipoVehiculo")
                        .HasColumnType("int");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.HasKey("Id");

                    b.HasIndex("IdTipoVehiculo");

                    b.ToTable("Vehiculos");
                });

            modelBuilder.Entity("Parking_App.Entities.Estancia", b =>
                {
                    b.HasOne("Parking_App.Entities.Ticket", null)
                        .WithMany("Estancia")
                        .HasForeignKey("TicketId");

                    b.HasOne("Parking_App.Entities.Vehiculo", "Vehiculo")
                        .WithMany("Estancia")
                        .HasForeignKey("VehiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehiculo");
                });

            modelBuilder.Entity("Parking_App.Entities.Ticket", b =>
                {
                    b.HasOne("Parking_App.Entities.Vehiculo", "Vehiculo")
                        .WithMany()
                        .HasForeignKey("VehiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehiculo");
                });

            modelBuilder.Entity("Parking_App.Entities.Vehiculo", b =>
                {
                    b.HasOne("Parking_App.Entities.TipoVehiculo", "TipoVehiculo")
                        .WithMany()
                        .HasForeignKey("IdTipoVehiculo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoVehiculo");
                });

            modelBuilder.Entity("Parking_App.Entities.Ticket", b =>
                {
                    b.Navigation("Estancia");
                });

            modelBuilder.Entity("Parking_App.Entities.Vehiculo", b =>
                {
                    b.Navigation("Estancia");
                });
#pragma warning restore 612, 618
        }
    }
}
