using System;
using Ice.Discord.Internal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ice.Discord.Internal.Migrations
{
    [DbContext(typeof(ContactsBotDbContext))]
    [Migration("20161219204719_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("IceBot.Data.Karma", b =>
                {
                    b.Property<long>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("KarmaCount");

                    b.HasKey("UserID");

                    b.ToTable("Karmas","ContactsBotSchema");
                });

            modelBuilder.Entity("IceBot.Data.Log", b =>
                {
                    b.Property<long>("LogID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Exception");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.Property<string>("Message");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("LogID");

                    b.ToTable("Logs","ContactsBotSchema");
                });

            modelBuilder.Entity("IceBot.Data.Memo", b =>
                {
                    b.Property<string>("MemoName")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("Message")
                        .HasMaxLength(500);

                    b.Property<long>("UserID");

                    b.HasKey("MemoName");

                    b.ToTable("Memos","ContactsBotSchema");
                });
        }
    }
}
