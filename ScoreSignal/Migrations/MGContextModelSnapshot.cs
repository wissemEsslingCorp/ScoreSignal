﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScoreSignal.Data;

#nullable disable

namespace ScoreSignal.Migrations
{
    [DbContext(typeof(MGContext))]
    partial class MGContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("ScoreSignal.Models.Equipe", b =>
                {
                    b.Property<int>("EquipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Abreviation")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nom")
                        .HasColumnType("TEXT");

                    b.HasKey("EquipeId");

                    b.ToTable("Equipe");
                });

            modelBuilder.Entity("ScoreSignal.Models.Evenement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Buteur")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Etat_match")
                        .HasColumnType("TEXT");

                    b.Property<int?>("EvenementId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MatchId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Score")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Temps")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EvenementId");

                    b.HasIndex("MatchId");

                    b.ToTable("Evenement");
                });

            modelBuilder.Entity("ScoreSignal.Models.Match", b =>
                {
                    b.Property<int>("MatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Equipe1")
                        .HasColumnType("TEXT");

                    b.Property<string>("Equipe2")
                        .HasColumnType("TEXT");

                    b.Property<string>("Ligue")
                        .HasColumnType("TEXT");

                    b.Property<int>("ScoreEquipe1")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ScoreEquipe2")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Temps")
                        .HasColumnType("TEXT");

                    b.HasKey("MatchId");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("ScoreSignal.Models.Evenement", b =>
                {
                    b.HasOne("ScoreSignal.Models.Evenement", null)
                        .WithMany("Evenements")
                        .HasForeignKey("EvenementId");

                    b.HasOne("ScoreSignal.Models.Match", "Match")
                        .WithMany("Evenements")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Match");
                });

            modelBuilder.Entity("ScoreSignal.Models.Evenement", b =>
                {
                    b.Navigation("Evenements");
                });

            modelBuilder.Entity("ScoreSignal.Models.Match", b =>
                {
                    b.Navigation("Evenements");
                });
#pragma warning restore 612, 618
        }
    }
}
