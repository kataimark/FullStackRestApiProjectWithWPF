using Microsoft.EntityFrameworkCore;
using GBJ0CK_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GBJ0CK_HFT_2021222.Repository
{
    public class LolPlayerDbContext : DbContext
    {
        public virtual DbSet<LolManager> LolManagers { get; set; }
        public virtual DbSet<LolTeam> LolTeams { get; set; }
        public virtual DbSet<LolPlayer> LolPlayers { get; set; }

        public LolPlayerDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies().UseInMemoryDatabase("LolPlayerdb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LolTeam>(entity =>
            {
                entity.HasOne(LolTeam => LolTeam.LolManager)
                      .WithMany(LolManager => LolManager.LolTeams)
                      .HasForeignKey(LolTeam => LolTeam.LolManager_Id)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<LolPlayer>(entity =>
            {
                entity.HasOne(LolPlayer => LolPlayer.LolTeam)
                      .WithMany(LolTeam => LolTeam.LolPlayers)
                      .HasForeignKey(LolPlayer => LolPlayer.LolTeam_Id)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            LolManager LolManager1 = new LolManager() { Id = 1, Name = "Bengi", Employees = 28 };
            LolManager LolManager2 = new LolManager() { Id = 2, Name = "Ocelote", Employees = 32 };
            LolManager LolManager3 = new LolManager() { Id = 3, Name = "AoD", Employees = 34 };
            LolManager LolManager4 = new LolManager() { Id = 4, Name = "Ssong", Employees = 33 };
            LolManager LolManager5 = new LolManager() { Id = 5, Name = "Maokai", Employees = 29 };

            LolTeam LolTeam1 = new LolTeam() { Id = 1, Name = "T1", Owner = "Bengi", LolManager_Id = 1 };
            LolTeam LolTeam2 = new LolTeam() { Id = 2, Name = "G2", Owner = "Ocelote", LolManager_Id = 2 };
            LolTeam LolTeam3 = new LolTeam() { Id = 3, Name = "Astralis", Owner = "AoD", LolManager_Id = 3 };
            LolTeam LolTeam4 = new LolTeam() { Id = 4, Name = "DRX", Owner = "Ssong", LolManager_Id = 4 };
            LolTeam LolTeam5 = new LolTeam() { Id = 5, Name = "EDG", Owner = "Maokai", LolManager_Id = 5 };


            LolPlayer LolPlayer1 = new LolPlayer() { Id = 1, Name = "Zeus", Age = 18, Price = 100, LolTeam_Id = 1 };
            LolPlayer LolPlayer2 = new LolPlayer() { Id = 2, Name = "Oner", Age = 19, Price = 200, LolTeam_Id = 1 };
            LolPlayer LolPlayer3 = new LolPlayer() { Id = 3, Name = "Faker", Age = 26, Price = 300, LolTeam_Id = 1 };
            LolPlayer LolPlayer4 = new LolPlayer() { Id = 4, Name = "Gumayusi", Age = 20, Price = 400, LolTeam_Id = 1 };
            LolPlayer LolPlayer5 = new LolPlayer() { Id = 5, Name = "Keria", Age = 20, Price = 500, LolTeam_Id = 1 };
            LolPlayer LolPlayer6 = new LolPlayer() { Id = 6, Name = "Broken Blade", Age = 22, Price = 100, LolTeam_Id = 2 };
            LolPlayer LolPlayer7 = new LolPlayer() { Id = 7, Name = "Jankos", Age = 27, Price = 200, LolTeam_Id = 2 };
            LolPlayer LolPlayer8 = new LolPlayer() { Id = 8, Name = "caPs", Age = 22, Price = 300, LolTeam_Id = 2 };
            LolPlayer LolPlayer9 = new LolPlayer() { Id = 9, Name = "Flakked", Age = 21, Price = 400, LolTeam_Id = 2 };
            LolPlayer LolPlayer10 = new LolPlayer() { Id = 10, Name = "Targamas", Age = 22, Price = 500, LolTeam_Id = 2 };
            LolPlayer LolPlayer11 = new LolPlayer() { Id = 11, Name = "Vizicsacsi", Age = 29, Price = 100, LolTeam_Id = 3 };
            LolPlayer LolPlayer12 = new LolPlayer() { Id = 12, Name = "Xerxe", Age = 22, Price = 200, LolTeam_Id = 3 };
            LolPlayer LolPlayer13 = new LolPlayer() { Id = 13, Name = "Dajor", Age = 19, Price = 300, LolTeam_Id = 3 };
            LolPlayer LolPlayer14 = new LolPlayer() { Id = 14, Name = "Kobbe", Age = 26, Price = 400, LolTeam_Id = 3 };
            LolPlayer LolPlayer15 = new LolPlayer() { Id = 15, Name = "JaeongHoon", Age = 22, Price = 500, LolTeam_Id = 3 };
            LolPlayer LolPlayer16 = new LolPlayer() { Id = 16, Name = "Kingen", Age = 22, Price = 100, LolTeam_Id = 4 };
            LolPlayer LolPlayer17 = new LolPlayer() { Id = 17, Name = "Pyosik", Age = 22, Price = 200, LolTeam_Id = 4 };
            LolPlayer LolPlayer18 = new LolPlayer() { Id = 18, Name = "Zeka", Age = 19, Price = 300, LolTeam_Id = 4 };
            LolPlayer LolPlayer19 = new LolPlayer() { Id = 19, Name = "Deft", Age = 26, Price = 400, LolTeam_Id = 4 };
            LolPlayer LolPlayer20 = new LolPlayer() { Id = 20, Name = "BeryL", Age = 25, Price = 500, LolTeam_Id = 4 };
            LolPlayer LolPlayer21 = new LolPlayer() { Id = 21, Name = "Flandre", Age = 24, Price = 100, LolTeam_Id = 5 };
            LolPlayer LolPlayer22 = new LolPlayer() { Id = 22, Name = "Jiejie", Age = 21, Price = 200, LolTeam_Id = 5 };
            LolPlayer LolPlayer23 = new LolPlayer() { Id = 23, Name = "Scout", Age = 24, Price = 300, LolTeam_Id = 5 };
            LolPlayer LolPlayer24 = new LolPlayer() { Id = 24, Name = "Viper", Age = 22, Price = 400, LolTeam_Id = 5 };
            LolPlayer LolPlayer25 = new LolPlayer() { Id = 25, Name = "Meiko", Age = 24, Price = 500, LolTeam_Id = 5 };


            modelBuilder.Entity<LolManager>().HasData(LolManager1, LolManager2, LolManager3, LolManager4, LolManager5);
            modelBuilder.Entity<LolTeam>().HasData(LolTeam1, LolTeam2, LolTeam3, LolTeam4, LolTeam5);
            modelBuilder.Entity<LolPlayer>().HasData(LolPlayer1, LolPlayer2, LolPlayer3, LolPlayer4, LolPlayer5, LolPlayer6, LolPlayer7, LolPlayer8, LolPlayer9, LolPlayer10, LolPlayer11, LolPlayer12, LolPlayer13, LolPlayer14, LolPlayer15, LolPlayer16, LolPlayer17, LolPlayer18, LolPlayer19, LolPlayer20, LolPlayer21, LolPlayer22, LolPlayer23, LolPlayer24, LolPlayer25);

        }
    }
}
