﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sensor.Infrastructure;

namespace Sensor.Infrastructure.Migrations
{
    [DbContext(typeof(SensorDBContext))]
    partial class SensorDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9");

            modelBuilder.Entity("Sensor.Domain.TemperatureHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("TemperatureValues")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("TemperatureHistories");
                });
#pragma warning restore 612, 618
        }
    }
}