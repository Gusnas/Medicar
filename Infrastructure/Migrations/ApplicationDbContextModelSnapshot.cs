﻿// <auto-generated />
using System;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Agenda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Dia")
                        .HasColumnType("date");

                    b.Property<int>("MedicoId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Agenda");
                });

            modelBuilder.Entity("Domain.Entities.AgendaHorario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AgendaId")
                        .HasColumnType("integer");

                    b.Property<bool>("Disponivel")
                        .HasColumnType("boolean");

                    b.Property<TimeOnly>("Horario")
                        .HasColumnType("time without time zone");

                    b.HasKey("Id");

                    b.ToTable("AgendaHorario");
                });

            modelBuilder.Entity("Domain.Entities.Consulta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataAgendamento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateOnly>("Dia")
                        .HasColumnType("date");

                    b.Property<TimeOnly>("Horario")
                        .HasColumnType("time without time zone");

                    b.Property<int>("MedicoId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Consulta");
                });

            modelBuilder.Entity("Domain.Entities.Medico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CRM")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Medico");
                });
#pragma warning restore 612, 618
        }
    }
}
