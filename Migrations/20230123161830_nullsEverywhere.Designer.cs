﻿// <auto-generated />
using System;
using BancoApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankApp.Migrations
{
    [DbContext(typeof(BankContext))]
    [Migration("20230123161830_nullsEverywhere")]
    partial class nullsEverywhere
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BancoApp.Models.Cliente", b =>
                {
                    b.Property<int>("IdCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCliente"));

                    b.Property<string>("TipoPersona")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("numeroDocumento")
                        .HasColumnType("int");

                    b.Property<string>("tipoDocumento")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCliente");

                    b.ToTable("cliente", (string)null);

                    b.HasDiscriminator<string>("TipoPersona").HasValue("Cliente");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("BancoApp.Models.Producto", b =>
                {
                    b.Property<int>("ProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductoId"));

                    b.Property<int?>("ClienteId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoProducto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductoId");

                    b.HasIndex("ClienteId");

                    b.ToTable("Producto", (string)null);

                    b.HasDiscriminator<string>("TipoProducto").HasValue("Producto");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("BancoApp.Models.Restriccion", b =>
                {
                    b.Property<int>("RestriccionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RestriccionId"));

                    b.Property<int?>("ClienteId")
                        .HasColumnType("int");

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("fecha")
                        .HasColumnType("datetime2");

                    b.HasKey("RestriccionId");

                    b.HasIndex("ClienteId")
                        .IsUnique()
                        .HasFilter("[ClienteId] IS NOT NULL");

                    b.ToTable("Restriccion", (string)null);
                });

            modelBuilder.Entity("BancoApp.Models.SolicitudPaquete", b =>
                {
                    b.Property<int>("SolPaqueteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SolPaqueteId"));

                    b.Property<bool>("Aprobada")
                        .HasColumnType("bit");

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<string>("CodigoPaquete")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaAprobacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaSolicitud")
                        .HasColumnType("datetime2");

                    b.Property<string>("MotivoRechazo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SolPaqueteId");

                    b.HasIndex("ClienteId");

                    b.ToTable("SolicitudPaquete", (string)null);
                });

            modelBuilder.Entity("BancoApp.Models.SolicitudPrestamo", b =>
                {
                    b.Property<int>("SolPrestamoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SolPrestamoId"));

                    b.Property<bool>("Aprobada")
                        .HasColumnType("bit");

                    b.Property<int?>("ClienteId")
                        .HasColumnType("int");

                    b.Property<string>("CodigoPrestamo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaSolicitud")
                        .HasColumnType("datetime2");

                    b.HasKey("SolPrestamoId");

                    b.HasIndex("ClienteId");

                    b.ToTable("SolicitudPrestamo", (string)null);
                });

            modelBuilder.Entity("BancoApp.Models.TarjetaCredito", b =>
                {
                    b.Property<int>("TarjetaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TarjetaId"));

                    b.Property<int>("PaqueteProductoId")
                        .HasColumnType("int");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<string>("descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("limiteCredito")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("TarjetaId");

                    b.HasIndex("PaqueteProductoId");

                    b.ToTable("TarjetaCredito", (string)null);
                });

            modelBuilder.Entity("BancoApp.Models.PersonaFisica", b =>
                {
                    b.HasBaseType("BancoApp.Models.Cliente");

                    b.Property<string>("apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Fisica");
                });

            modelBuilder.Entity("BancoApp.Models.PersonaJuridica", b =>
                {
                    b.HasBaseType("BancoApp.Models.Cliente");

                    b.Property<string>("razonSocial")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Juridica");
                });

            modelBuilder.Entity("BancoApp.Models.Paquete", b =>
                {
                    b.HasBaseType("BancoApp.Models.Producto");

                    b.Property<int?>("TarjetasId")
                        .HasColumnType("int");

                    b.Property<bool>("esCrediticio")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("Paquete");
                });

            modelBuilder.Entity("BancoApp.Models.Prestamo", b =>
                {
                    b.HasBaseType("BancoApp.Models.Producto");

                    b.Property<bool>("esPrendario")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("Prestamo");
                });

            modelBuilder.Entity("BancoApp.Models.Producto", b =>
                {
                    b.HasOne("BancoApp.Models.Cliente", "Cliente")
                        .WithMany("Producto")
                        .HasForeignKey("ClienteId");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("BancoApp.Models.Restriccion", b =>
                {
                    b.HasOne("BancoApp.Models.Cliente", "Cliente")
                        .WithOne("Restriccion")
                        .HasForeignKey("BancoApp.Models.Restriccion", "ClienteId");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("BancoApp.Models.SolicitudPaquete", b =>
                {
                    b.HasOne("BancoApp.Models.Cliente", "Cliente")
                        .WithMany("SolicitudPaquetes")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("BancoApp.Models.SolicitudPrestamo", b =>
                {
                    b.HasOne("BancoApp.Models.Cliente", "Cliente")
                        .WithMany("SolicitudPrestamos")
                        .HasForeignKey("ClienteId");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("BancoApp.Models.TarjetaCredito", b =>
                {
                    b.HasOne("BancoApp.Models.Paquete", "Paquete")
                        .WithMany("tarjetas")
                        .HasForeignKey("PaqueteProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paquete");
                });

            modelBuilder.Entity("BancoApp.Models.Cliente", b =>
                {
                    b.Navigation("Producto");

                    b.Navigation("Restriccion");

                    b.Navigation("SolicitudPaquetes");

                    b.Navigation("SolicitudPrestamos");
                });

            modelBuilder.Entity("BancoApp.Models.Paquete", b =>
                {
                    b.Navigation("tarjetas");
                });
#pragma warning restore 612, 618
        }
    }
}
