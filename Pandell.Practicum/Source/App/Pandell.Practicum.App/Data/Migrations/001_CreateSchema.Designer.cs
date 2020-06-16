using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pandell.Practicum.App.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("001_CreateSchema")]
    partial class CreateSchema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity("RandomSequence",
                m =>
                {
                    m.Property<byte[]>("Id").HasColumnType("BINARY(16)");
                    m.Property<string>("GeneratedSequence").HasColumnType("JSON").IsRequired();
                    m.Property<DateTime>("DateInserted").HasColumnType("DATETIME").IsRequired();
                    m.Property<DateTime>("DateUpdated").HasColumnType("DATETIME");
                    m.Property<string>("LastModifiedBy").HasColumnType("TEXT").IsRequired();
                    m.HasKey("Id");
                    m.ToTable("RandomSequence");
                });
        }
    }
}