﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using iread_interaction_ms.DataAccess.Data;

namespace iread_interaction_ms.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210822121532_delete-int-index-drawing")]
    partial class deleteintindexdrawing
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("iread_interaction_ms.DataAccess.Data.Entity.Audio", b =>
                {
                    b.Property<int>("AudioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AttachmentId")
                        .HasColumnType("int");

                    b.Property<string>("EndWord")
                        .HasColumnType("text");

                    b.Property<int?>("EndWordIndex")
                        .HasColumnType("int");

                    b.Property<string>("FirstWord")
                        .HasColumnType("text");

                    b.Property<int?>("FirstWordIndex")
                        .HasColumnType("int");

                    b.Property<int>("InteractionId")
                        .HasColumnType("int");

                    b.HasKey("AudioId");

                    b.HasIndex("InteractionId");

                    b.ToTable("Audios");
                });

            modelBuilder.Entity("iread_interaction_ms.DataAccess.Data.Entity.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CommentType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("InteractionId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WordTimesTamp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CommentId");

                    b.HasIndex("InteractionId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("iread_interaction_ms.DataAccess.Data.Entity.Drawing", b =>
                {
                    b.Property<int>("DrawingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AudioId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<string>("Comment")
                        .HasColumnType("text");

                    b.Property<int>("InteractionId")
                        .HasColumnType("int");

                    b.Property<string>("Points")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("DrawingId");

                    b.HasIndex("InteractionId");

                    b.ToTable("Drawings");
                });

            modelBuilder.Entity("iread_interaction_ms.DataAccess.Data.Entity.HighLight", b =>
                {
                    b.Property<int>("HighLightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("EndWord")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EndWordIndex")
                        .HasColumnType("int");

                    b.Property<string>("FirstWord")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FirstWordIndex")
                        .HasColumnType("int");

                    b.Property<int>("InteractionId")
                        .HasColumnType("int");

                    b.HasKey("HighLightId");

                    b.HasIndex("InteractionId");

                    b.ToTable("HighLights");
                });

            modelBuilder.Entity("iread_interaction_ms.DataAccess.Data.Entity.Interaction", b =>
                {
                    b.Property<int>("InteractionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PageId")
                        .HasColumnType("int");

                    b.Property<int>("StoryId")
                        .HasColumnType("int");

                    b.Property<string>("StudentId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("InteractionId");

                    b.ToTable("Interactions");
                });

            modelBuilder.Entity("iread_interaction_ms.DataAccess.Data.Entity.Audio", b =>
                {
                    b.HasOne("iread_interaction_ms.DataAccess.Data.Entity.Interaction", "Interaction")
                        .WithMany()
                        .HasForeignKey("InteractionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interaction");
                });

            modelBuilder.Entity("iread_interaction_ms.DataAccess.Data.Entity.Comment", b =>
                {
                    b.HasOne("iread_interaction_ms.DataAccess.Data.Entity.Interaction", "Interaction")
                        .WithMany("Comments")
                        .HasForeignKey("InteractionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interaction");
                });

            modelBuilder.Entity("iread_interaction_ms.DataAccess.Data.Entity.Drawing", b =>
                {
                    b.HasOne("iread_interaction_ms.DataAccess.Data.Entity.Interaction", "Interaction")
                        .WithMany("Drawings")
                        .HasForeignKey("InteractionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interaction");
                });

            modelBuilder.Entity("iread_interaction_ms.DataAccess.Data.Entity.HighLight", b =>
                {
                    b.HasOne("iread_interaction_ms.DataAccess.Data.Entity.Interaction", "Interaction")
                        .WithMany()
                        .HasForeignKey("InteractionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interaction");
                });

            modelBuilder.Entity("iread_interaction_ms.DataAccess.Data.Entity.Interaction", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Drawings");
                });
#pragma warning restore 612, 618
        }
    }
}
