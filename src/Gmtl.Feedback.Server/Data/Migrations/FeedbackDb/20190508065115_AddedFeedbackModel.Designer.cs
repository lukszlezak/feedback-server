﻿// <auto-generated />
using System;
using Gmtl.Feedback.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Gmtl.Feedback.Server.Migrations.FeedbackDb
{
    [DbContext(typeof(FeedbackDbContext))]
    [Migration("20190508065115_AddedFeedbackModel")]
    partial class AddedFeedbackModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Gmtl.Feedback.Server.Models.Domain", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Domains");
                });

            modelBuilder.Entity("Gmtl.Feedback.Server.Models.Feedback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid>("DomainId");

                    b.Property<string>("Message")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Feedbacks");
                });
#pragma warning restore 612, 618
        }
    }
}