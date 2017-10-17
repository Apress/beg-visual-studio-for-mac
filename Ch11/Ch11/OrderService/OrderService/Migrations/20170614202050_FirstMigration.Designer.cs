using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OrderService.Models;

namespace OrderService.Migrations
{
    [DbContext(typeof(OrderContext))]
    [Migration("20170614202050_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("OrderService.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CustomerName");

                    b.Property<DateTime>("OrderDate");

                    b.Property<string>("OrderDescription");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });
        }
    }
}
