using Moq;
using NUnit.Framework;
using GBJ0CK_HFT_2021222.Logic;
using GBJ0CK_HFT_2021222.Models;
using GBJ0CK_HFT_2021222.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Humanizer.In;

namespace GBJ0CK_HFT_2021222.Test
{
    [TestFixture]
    public class Tester
    {
        LolPlayerLogic playerl;
        LolTeamLogic teaml;
        LolManagerLogic managerl;

        [SetUp]
        public void Setup()
        {
            Mock<IRepository<LolPlayer>> mockLolPlayerRepo = new Mock<IRepository<LolPlayer>>();
            Mock<IRepository<LolTeam>> mockLolTeamRepo = new Mock<IRepository<LolTeam>>();
            Mock<IRepository<LolManager>> mockLolManagerRepo = new Mock<IRepository<LolManager>>();


            mockLolPlayerRepo.Setup(x => x.Read(It.IsAny<int>())).Returns(
                new LolPlayer()
                {
                    Id = 1,
                    Name = "Zeus",
                    Age = 18,
                    Price = 100,
                    LolTeam_Id = 1
                });

            mockLolPlayerRepo.Setup(x => x.ReadAll()).Returns(FakeLolPlayerObject);
            mockLolTeamRepo.Setup(x => x.ReadAll()).Returns(FakeLolTeamObject);
            mockLolManagerRepo.Setup(x => x.ReadAll()).Returns(FakeLolManagerObject);

            playerl = new LolPlayerLogic(mockLolManagerRepo.Object, mockLolTeamRepo.Object, mockLolPlayerRepo.Object);
            managerl = new LolManagerLogic(mockLolManagerRepo.Object, mockLolTeamRepo.Object, mockLolPlayerRepo.Object);
            teaml = new LolTeamLogic(mockLolTeamRepo.Object);
        }

        //CRUD

        [TestCase("TestName", 100, 100, true)]
        [TestCase("TestName", -100, 100, false)]
        [TestCase("", 100, 100, false)]
        public void CreateLolPlayerTest(string name, int age, int price, bool result)
        {
            if (result)
            {
                Assert.That(() => { playerl.Create(new LolPlayer() { Name = name, Age = age, Price = price }); }, Throws.Nothing);
            }
            else
            {
                Assert.That(() => { playerl.Create(new LolPlayer() { Name = name, Age = age, Price = price }); }, Throws.Exception);
            }
        }

        [TestCase("TestName", "TestOwner", true)]
        [TestCase("TestName123", "TestOwner123", false)]
        [TestCase("", "", false)]
        public void CreateLolTeamTest(string name, string owner, bool result)
        {
            if (result)
            {
                Assert.That(() => { teaml.Create(new LolTeam() { Name = name, Owner = owner }); }, Throws.Nothing);
            }
            else
            {
                Assert.That(() => { teaml.Create(new LolTeam() { Name = name, Owner = owner }); }, Throws.Exception);
            }
        }

        [TestCase("TestName", 100, true)]
        [TestCase("TestName123", 100, false)]
        [TestCase("", 100, false)]
        public void CreateLolManagerTest(string name, int employees, bool result)
        {
            if (result)
            {
                Assert.That(() => { managerl.Create(new LolManager() { Name = name, Employees = employees }); }, Throws.Nothing);
            }
            else
            {
                Assert.That(() => { managerl.Create(new LolManager() { Name = name, Employees = employees }); }, Throws.Exception);
            }
        }

        [TestCase(50)]
        [TestCase(100)]
        public void GetOneLolPlayer_ThrowsException_WhenIdIsTooBig(int idx)
        {
            Assert.That(() => playerl.Read(idx), Throws.TypeOf<IndexOutOfRangeException>());
        }

        [Test]
        public void GetOneLolPlayer_ReturnsCorrectInstance()
        {
            Assert.That(playerl.Read(1).Name, Is.EqualTo("Zeus"));
        }

        [Test]
        public void GetAllLolPlayer_ReturnsExactNumberOfInstances()
        {
            Assert.That(playerl.ReadAll().Count, Is.EqualTo(25));
        }

        //non-CRUD

        [Test]
        public void GetLolManagerWhereLolPlayerModelIsZeus_ReturnsCorrectInstance()
        {
            Assert.That(managerl.GetLolManagerWhereLolPlayerModelIsZeus().First().Name, Is.EqualTo("Maokai"));
        }

        [Test]
        public void GetLolPlayerWhereMoreThan28Employees_ReturnsCorrectInstance()
        {
            Assert.That(playerl.GetLolPlayerWhereMoreThan28Employees().Count(), Is.EqualTo(25));
        }

        [Test]
        public void GetLolPlayerWhereLolTeamOwnerIsBengi_ReturnsCorrectInstance()
        {
            Assert.That(playerl.GetLolPlayerWhereLolTeamOwnerIsBengi().Count(), Is.EqualTo(5));
        }

        [Test]
        public void GetLolManagerWhereLolPlayer18_ReturnsCorrectInstance()
        {
            Assert.That(managerl.GetLolManagerWhereLolPlayer18().First().Name, Is.EqualTo("Maokai"));
        }

        [Test]
        public void GetLolManagerWherePriceIs100_ReturnsCorrectInstance()
        {
            Assert.That(managerl.GetLolManagerWherePriceIs100().First().Name, Is.EqualTo("Maokai"));
        }

        private IQueryable<LolPlayer> FakeLolPlayerObject()
        {
            LolManager LolManager1 = new LolManager() { Id = 1, Name = "Bengi", Employees = 28 };
            LolManager LolManager2 = new LolManager() { Id = 2, Name = "Ocelote", Employees = 32 };
            LolManager LolManager3 = new LolManager() { Id = 3, Name = "AoD", Employees = 34 };
            LolManager LolManager4 = new LolManager() { Id = 4, Name = "Ssong", Employees = 33 };
            LolManager LolManager5 = new LolManager() { Id = 5, Name = "Maokai", Employees = 29 };

            LolManager1.LolTeams = new List<LolTeam>();
            LolManager2.LolTeams = new List<LolTeam>();
            LolManager3.LolTeams = new List<LolTeam>();
            LolManager4.LolTeams = new List<LolTeam>();
            LolManager5.LolTeams = new List<LolTeam>();



            LolTeam LolTeam1 = new LolTeam() { Id = 1, Name = "T1", Owner = "Bengi", LolManager_Id = 1 };
            LolTeam LolTeam2 = new LolTeam() { Id = 2, Name = "G2", Owner = "Ocelote", LolManager_Id = 2 };
            LolTeam LolTeam3 = new LolTeam() { Id = 3, Name = "Astralis", Owner = "AoD", LolManager_Id = 3 };
            LolTeam LolTeam4 = new LolTeam() { Id = 4, Name = "DRX", Owner = "Ssong", LolManager_Id = 4 };
            LolTeam LolTeam5 = new LolTeam() { Id = 5, Name = "EDG", Owner = "Maokai", LolManager_Id = 5 };

            LolTeam1.LolManager = LolManager1;
            LolTeam2.LolManager = LolManager2;
            LolTeam3.LolManager = LolManager3;
            LolTeam4.LolManager = LolManager4;
            LolTeam5.LolManager = LolManager5;

            LolTeam1.LolManager_Id = LolManager1.Id; LolManager1.LolTeams.Add(LolTeam1);
            LolTeam1.LolManager_Id = LolManager2.Id; LolManager2.LolTeams.Add(LolTeam2);
            LolTeam1.LolManager_Id = LolManager3.Id; LolManager3.LolTeams.Add(LolTeam3);
            LolTeam1.LolManager_Id = LolManager4.Id; LolManager4.LolTeams.Add(LolTeam4);
            LolTeam1.LolManager_Id = LolManager5.Id; LolManager5.LolTeams.Add(LolTeam5);

            LolTeam1.LolPlayers = new List<LolPlayer>();
            LolTeam2.LolPlayers = new List<LolPlayer>();
            LolTeam3.LolPlayers = new List<LolPlayer>();
            LolTeam4.LolPlayers = new List<LolPlayer>();
            LolTeam5.LolPlayers = new List<LolPlayer>();



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

            LolPlayer1.LolTeam = LolTeam1;
            LolPlayer2.LolTeam = LolTeam1;
            LolPlayer3.LolTeam = LolTeam1;
            LolPlayer4.LolTeam = LolTeam1;
            LolPlayer5.LolTeam = LolTeam1;
            LolPlayer6.LolTeam = LolTeam2;
            LolPlayer7.LolTeam = LolTeam2;
            LolPlayer8.LolTeam = LolTeam2;
            LolPlayer9.LolTeam = LolTeam2;
            LolPlayer10.LolTeam = LolTeam2;
            LolPlayer11.LolTeam = LolTeam3;
            LolPlayer12.LolTeam = LolTeam3;
            LolPlayer13.LolTeam = LolTeam3;
            LolPlayer14.LolTeam = LolTeam3;
            LolPlayer15.LolTeam = LolTeam3;
            LolPlayer16.LolTeam = LolTeam4;
            LolPlayer17.LolTeam = LolTeam4;
            LolPlayer18.LolTeam = LolTeam4;
            LolPlayer19.LolTeam = LolTeam4;
            LolPlayer20.LolTeam = LolTeam4;
            LolPlayer21.LolTeam = LolTeam5;
            LolPlayer22.LolTeam = LolTeam5;
            LolPlayer23.LolTeam = LolTeam5;
            LolPlayer24.LolTeam = LolTeam5;
            LolPlayer25.LolTeam = LolTeam5;

            LolPlayer1.LolTeam_Id = LolTeam1.Id; LolTeam1.LolPlayers.Add(LolPlayer1);
            LolPlayer2.LolTeam_Id = LolTeam1.Id; LolTeam1.LolPlayers.Add(LolPlayer2);
            LolPlayer3.LolTeam_Id = LolTeam1.Id; LolTeam1.LolPlayers.Add(LolPlayer3);
            LolPlayer4.LolTeam_Id = LolTeam1.Id; LolTeam1.LolPlayers.Add(LolPlayer4);
            LolPlayer5.LolTeam_Id = LolTeam1.Id; LolTeam1.LolPlayers.Add(LolPlayer5);
            LolPlayer6.LolTeam_Id = LolTeam2.Id; LolTeam2.LolPlayers.Add(LolPlayer6);
            LolPlayer7.LolTeam_Id = LolTeam2.Id; LolTeam2.LolPlayers.Add(LolPlayer7);
            LolPlayer8.LolTeam_Id = LolTeam2.Id; LolTeam2.LolPlayers.Add(LolPlayer8);
            LolPlayer9.LolTeam_Id = LolTeam2.Id; LolTeam2.LolPlayers.Add(LolPlayer9);
            LolPlayer10.LolTeam_Id = LolTeam2.Id; LolTeam2.LolPlayers.Add(LolPlayer10);
            LolPlayer11.LolTeam_Id = LolTeam3.Id; LolTeam3.LolPlayers.Add(LolPlayer11);
            LolPlayer12.LolTeam_Id = LolTeam3.Id; LolTeam3.LolPlayers.Add(LolPlayer12);
            LolPlayer13.LolTeam_Id = LolTeam3.Id; LolTeam3.LolPlayers.Add(LolPlayer13);
            LolPlayer14.LolTeam_Id = LolTeam3.Id; LolTeam3.LolPlayers.Add(LolPlayer14);
            LolPlayer15.LolTeam_Id = LolTeam3.Id; LolTeam3.LolPlayers.Add(LolPlayer15);
            LolPlayer16.LolTeam_Id = LolTeam4.Id; LolTeam4.LolPlayers.Add(LolPlayer16);
            LolPlayer17.LolTeam_Id = LolTeam4.Id; LolTeam4.LolPlayers.Add(LolPlayer17);
            LolPlayer18.LolTeam_Id = LolTeam4.Id; LolTeam4.LolPlayers.Add(LolPlayer18);
            LolPlayer19.LolTeam_Id = LolTeam4.Id; LolTeam4.LolPlayers.Add(LolPlayer19);
            LolPlayer20.LolTeam_Id = LolTeam4.Id; LolTeam4.LolPlayers.Add(LolPlayer20);
            LolPlayer21.LolTeam_Id = LolTeam5.Id; LolTeam5.LolPlayers.Add(LolPlayer21);
            LolPlayer22.LolTeam_Id = LolTeam5.Id; LolTeam5.LolPlayers.Add(LolPlayer22);
            LolPlayer23.LolTeam_Id = LolTeam5.Id; LolTeam5.LolPlayers.Add(LolPlayer23);
            LolPlayer24.LolTeam_Id = LolTeam5.Id; LolTeam5.LolPlayers.Add(LolPlayer24);
            LolPlayer25.LolTeam_Id = LolTeam5.Id; LolTeam5.LolPlayers.Add(LolPlayer25);




            List<LolPlayer> LolPlayer = new List<LolPlayer>();
            LolPlayer.Add(LolPlayer1);
            LolPlayer.Add(LolPlayer2);
            LolPlayer.Add(LolPlayer3);
            LolPlayer.Add(LolPlayer4);
            LolPlayer.Add(LolPlayer5);
            LolPlayer.Add(LolPlayer6);
            LolPlayer.Add(LolPlayer7);
            LolPlayer.Add(LolPlayer8);
            LolPlayer.Add(LolPlayer9);
            LolPlayer.Add(LolPlayer10);
            LolPlayer.Add(LolPlayer11);
            LolPlayer.Add(LolPlayer12);
            LolPlayer.Add(LolPlayer13);
            LolPlayer.Add(LolPlayer14);
            LolPlayer.Add(LolPlayer15);
            LolPlayer.Add(LolPlayer16);
            LolPlayer.Add(LolPlayer17);
            LolPlayer.Add(LolPlayer18);
            LolPlayer.Add(LolPlayer19);
            LolPlayer.Add(LolPlayer20);
            LolPlayer.Add(LolPlayer21);
            LolPlayer.Add(LolPlayer22);
            LolPlayer.Add(LolPlayer23);
            LolPlayer.Add(LolPlayer24);
            LolPlayer.Add(LolPlayer25);

            return LolPlayer.AsQueryable();
        }

        private IQueryable<LolTeam> FakeLolTeamObject()
        {
            LolManager LolManager1 = new LolManager() { Id = 1, Name = "Bengi", Employees = 28 };
            LolManager LolManager2 = new LolManager() { Id = 2, Name = "Ocelote", Employees = 32 };
            LolManager LolManager3 = new LolManager() { Id = 3, Name = "AoD", Employees = 34 };
            LolManager LolManager4 = new LolManager() { Id = 4, Name = "Ssong", Employees = 33 };
            LolManager LolManager5 = new LolManager() { Id = 5, Name = "Maokai", Employees = 29 };

            LolManager1.LolTeams = new List<LolTeam>();
            LolManager2.LolTeams = new List<LolTeam>();
            LolManager3.LolTeams = new List<LolTeam>();



            LolTeam LolTeam1 = new LolTeam() { Id = 1, Name = "T1", Owner = "Bengi", LolManager_Id = 1 };
            LolTeam LolTeam2 = new LolTeam() { Id = 2, Name = "G2", Owner = "Ocelote", LolManager_Id = 2 };
            LolTeam LolTeam3 = new LolTeam() { Id = 3, Name = "Astralis", Owner = "AoD", LolManager_Id = 3 };
            LolTeam LolTeam4 = new LolTeam() { Id = 4, Name = "DRX", Owner = "Ssong", LolManager_Id = 4 };
            LolTeam LolTeam5 = new LolTeam() { Id = 5, Name = "EDG", Owner = "Maokai", LolManager_Id = 5 };

            LolTeam1.LolManager = LolManager1;
            LolTeam2.LolManager = LolManager2;
            LolTeam3.LolManager = LolManager3;
            LolTeam4.LolManager = LolManager4;
            LolTeam5.LolManager = LolManager5;

            LolTeam1.LolManager_Id = LolManager1.Id; LolManager1.LolTeams.Add(LolTeam1);
            LolTeam1.LolManager_Id = LolManager2.Id; LolManager2.LolTeams.Add(LolTeam2);
            LolTeam1.LolManager_Id = LolManager3.Id; LolManager3.LolTeams.Add(LolTeam3);
            LolTeam1.LolManager_Id = LolManager4.Id; LolManager4.LolTeams.Add(LolTeam4);
            LolTeam1.LolManager_Id = LolManager5.Id; LolManager5.LolTeams.Add(LolTeam5);

            LolTeam1.LolPlayers = new List<LolPlayer>();
            LolTeam2.LolPlayers = new List<LolPlayer>();
            LolTeam3.LolPlayers = new List<LolPlayer>();
            LolTeam4.LolPlayers = new List<LolPlayer>();
            LolTeam5.LolPlayers = new List<LolPlayer>();



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

            LolPlayer1.LolTeam = LolTeam1;
            LolPlayer2.LolTeam = LolTeam1;
            LolPlayer3.LolTeam = LolTeam1;
            LolPlayer4.LolTeam = LolTeam1;
            LolPlayer5.LolTeam = LolTeam1;
            LolPlayer6.LolTeam = LolTeam2;
            LolPlayer7.LolTeam = LolTeam2;
            LolPlayer8.LolTeam = LolTeam2;
            LolPlayer9.LolTeam = LolTeam2;
            LolPlayer10.LolTeam = LolTeam2;
            LolPlayer11.LolTeam = LolTeam3;
            LolPlayer12.LolTeam = LolTeam3;
            LolPlayer13.LolTeam = LolTeam3;
            LolPlayer14.LolTeam = LolTeam3;
            LolPlayer15.LolTeam = LolTeam3;
            LolPlayer16.LolTeam = LolTeam4;
            LolPlayer17.LolTeam = LolTeam4;
            LolPlayer18.LolTeam = LolTeam4;
            LolPlayer19.LolTeam = LolTeam4;
            LolPlayer20.LolTeam = LolTeam4;
            LolPlayer21.LolTeam = LolTeam5;
            LolPlayer22.LolTeam = LolTeam5;
            LolPlayer23.LolTeam = LolTeam5;
            LolPlayer24.LolTeam = LolTeam5;
            LolPlayer25.LolTeam = LolTeam5;

            LolPlayer1.LolTeam_Id = LolTeam1.Id; LolTeam1.LolPlayers.Add(LolPlayer1);
            LolPlayer2.LolTeam_Id = LolTeam1.Id; LolTeam1.LolPlayers.Add(LolPlayer2);
            LolPlayer3.LolTeam_Id = LolTeam1.Id; LolTeam1.LolPlayers.Add(LolPlayer3);
            LolPlayer4.LolTeam_Id = LolTeam1.Id; LolTeam1.LolPlayers.Add(LolPlayer4);
            LolPlayer5.LolTeam_Id = LolTeam1.Id; LolTeam1.LolPlayers.Add(LolPlayer5);
            LolPlayer6.LolTeam_Id = LolTeam2.Id; LolTeam2.LolPlayers.Add(LolPlayer6);
            LolPlayer7.LolTeam_Id = LolTeam2.Id; LolTeam2.LolPlayers.Add(LolPlayer7);
            LolPlayer8.LolTeam_Id = LolTeam2.Id; LolTeam2.LolPlayers.Add(LolPlayer8);
            LolPlayer9.LolTeam_Id = LolTeam2.Id; LolTeam2.LolPlayers.Add(LolPlayer9);
            LolPlayer10.LolTeam_Id = LolTeam2.Id; LolTeam2.LolPlayers.Add(LolPlayer10);
            LolPlayer11.LolTeam_Id = LolTeam3.Id; LolTeam3.LolPlayers.Add(LolPlayer11);
            LolPlayer12.LolTeam_Id = LolTeam3.Id; LolTeam3.LolPlayers.Add(LolPlayer12);
            LolPlayer13.LolTeam_Id = LolTeam3.Id; LolTeam3.LolPlayers.Add(LolPlayer13);
            LolPlayer14.LolTeam_Id = LolTeam3.Id; LolTeam3.LolPlayers.Add(LolPlayer14);
            LolPlayer15.LolTeam_Id = LolTeam3.Id; LolTeam3.LolPlayers.Add(LolPlayer15);
            LolPlayer16.LolTeam_Id = LolTeam4.Id; LolTeam4.LolPlayers.Add(LolPlayer16);
            LolPlayer17.LolTeam_Id = LolTeam4.Id; LolTeam4.LolPlayers.Add(LolPlayer17);
            LolPlayer18.LolTeam_Id = LolTeam4.Id; LolTeam4.LolPlayers.Add(LolPlayer18);
            LolPlayer19.LolTeam_Id = LolTeam4.Id; LolTeam4.LolPlayers.Add(LolPlayer19);
            LolPlayer20.LolTeam_Id = LolTeam4.Id; LolTeam4.LolPlayers.Add(LolPlayer20);
            LolPlayer21.LolTeam_Id = LolTeam5.Id; LolTeam5.LolPlayers.Add(LolPlayer21);
            LolPlayer22.LolTeam_Id = LolTeam5.Id; LolTeam5.LolPlayers.Add(LolPlayer22);
            LolPlayer23.LolTeam_Id = LolTeam5.Id; LolTeam5.LolPlayers.Add(LolPlayer23);
            LolPlayer24.LolTeam_Id = LolTeam5.Id; LolTeam5.LolPlayers.Add(LolPlayer24);
            LolPlayer25.LolTeam_Id = LolTeam5.Id; LolTeam5.LolPlayers.Add(LolPlayer25);



            List<LolTeam> LolTeam = new List<LolTeam>();
            LolTeam.Add(LolTeam1);
            LolTeam.Add(LolTeam2);
            LolTeam.Add(LolTeam3);
            LolTeam.Add(LolTeam4);
            LolTeam.Add(LolTeam5);

            return LolTeam.AsQueryable();
        }

        private IQueryable<LolManager> FakeLolManagerObject()
        {
            LolManager LolManager1 = new LolManager() { Id = 1, Name = "Bengi", Employees = 28 };
            LolManager LolManager2 = new LolManager() { Id = 2, Name = "Ocelote", Employees = 32 };
            LolManager LolManager3 = new LolManager() { Id = 3, Name = "AoD", Employees = 34 };
            LolManager LolManager4 = new LolManager() { Id = 4, Name = "Ssong", Employees = 33 };
            LolManager LolManager5 = new LolManager() { Id = 5, Name = "Maokai", Employees = 29 };

            LolManager1.LolTeams = new List<LolTeam>();
            LolManager2.LolTeams = new List<LolTeam>();
            LolManager3.LolTeams = new List<LolTeam>();



            LolTeam LolTeam1 = new LolTeam() { Id = 1, Name = "T1", Owner = "Bengi", LolManager_Id = 1 };
            LolTeam LolTeam2 = new LolTeam() { Id = 2, Name = "G2", Owner = "Ocelote", LolManager_Id = 2 };
            LolTeam LolTeam3 = new LolTeam() { Id = 3, Name = "Astralis", Owner = "AoD", LolManager_Id = 3 };
            LolTeam LolTeam4 = new LolTeam() { Id = 4, Name = "DRX", Owner = "Ssong", LolManager_Id = 4 };
            LolTeam LolTeam5 = new LolTeam() { Id = 5, Name = "EDG", Owner = "Maokai", LolManager_Id = 5 };

            LolTeam1.LolManager = LolManager1;
            LolTeam2.LolManager = LolManager2;
            LolTeam3.LolManager = LolManager3;
            LolTeam4.LolManager = LolManager4;
            LolTeam5.LolManager = LolManager5;

            LolTeam1.LolManager_Id = LolManager1.Id; LolManager1.LolTeams.Add(LolTeam1);
            LolTeam1.LolManager_Id = LolManager2.Id; LolManager2.LolTeams.Add(LolTeam2);
            LolTeam1.LolManager_Id = LolManager3.Id; LolManager3.LolTeams.Add(LolTeam3);
            LolTeam1.LolManager_Id = LolManager4.Id; LolManager4.LolTeams.Add(LolTeam4);
            LolTeam1.LolManager_Id = LolManager5.Id; LolManager5.LolTeams.Add(LolTeam5);

            LolTeam1.LolPlayers = new List<LolPlayer>();
            LolTeam2.LolPlayers = new List<LolPlayer>();
            LolTeam3.LolPlayers = new List<LolPlayer>();
            LolTeam4.LolPlayers = new List<LolPlayer>();
            LolTeam5.LolPlayers = new List<LolPlayer>();

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

            LolPlayer1.LolTeam = LolTeam1;
            LolPlayer2.LolTeam = LolTeam1;
            LolPlayer3.LolTeam = LolTeam1;
            LolPlayer4.LolTeam = LolTeam1;
            LolPlayer5.LolTeam = LolTeam1;
            LolPlayer6.LolTeam = LolTeam2;
            LolPlayer7.LolTeam = LolTeam2;
            LolPlayer8.LolTeam = LolTeam2;
            LolPlayer9.LolTeam = LolTeam2;
            LolPlayer10.LolTeam = LolTeam2;
            LolPlayer11.LolTeam = LolTeam3;
            LolPlayer12.LolTeam = LolTeam3;
            LolPlayer13.LolTeam = LolTeam3;
            LolPlayer14.LolTeam = LolTeam3;
            LolPlayer15.LolTeam = LolTeam3;
            LolPlayer16.LolTeam = LolTeam4;
            LolPlayer17.LolTeam = LolTeam4;
            LolPlayer18.LolTeam = LolTeam4;
            LolPlayer19.LolTeam = LolTeam4;
            LolPlayer20.LolTeam = LolTeam4;
            LolPlayer21.LolTeam = LolTeam5;
            LolPlayer22.LolTeam = LolTeam5;
            LolPlayer23.LolTeam = LolTeam5;
            LolPlayer24.LolTeam = LolTeam5;
            LolPlayer25.LolTeam = LolTeam5;

            LolPlayer1.LolTeam_Id = LolTeam1.Id; LolTeam1.LolPlayers.Add(LolPlayer1);
            LolPlayer2.LolTeam_Id = LolTeam1.Id; LolTeam1.LolPlayers.Add(LolPlayer2);
            LolPlayer3.LolTeam_Id = LolTeam1.Id; LolTeam1.LolPlayers.Add(LolPlayer3);
            LolPlayer4.LolTeam_Id = LolTeam1.Id; LolTeam1.LolPlayers.Add(LolPlayer4);
            LolPlayer5.LolTeam_Id = LolTeam1.Id; LolTeam1.LolPlayers.Add(LolPlayer5);
            LolPlayer6.LolTeam_Id = LolTeam2.Id; LolTeam2.LolPlayers.Add(LolPlayer6);
            LolPlayer7.LolTeam_Id = LolTeam2.Id; LolTeam2.LolPlayers.Add(LolPlayer7);
            LolPlayer8.LolTeam_Id = LolTeam2.Id; LolTeam2.LolPlayers.Add(LolPlayer8);
            LolPlayer9.LolTeam_Id = LolTeam2.Id; LolTeam2.LolPlayers.Add(LolPlayer9);
            LolPlayer10.LolTeam_Id = LolTeam2.Id; LolTeam2.LolPlayers.Add(LolPlayer10);
            LolPlayer11.LolTeam_Id = LolTeam3.Id; LolTeam3.LolPlayers.Add(LolPlayer11);
            LolPlayer12.LolTeam_Id = LolTeam3.Id; LolTeam3.LolPlayers.Add(LolPlayer12);
            LolPlayer13.LolTeam_Id = LolTeam3.Id; LolTeam3.LolPlayers.Add(LolPlayer13);
            LolPlayer14.LolTeam_Id = LolTeam3.Id; LolTeam3.LolPlayers.Add(LolPlayer14);
            LolPlayer15.LolTeam_Id = LolTeam3.Id; LolTeam3.LolPlayers.Add(LolPlayer15);
            LolPlayer16.LolTeam_Id = LolTeam4.Id; LolTeam4.LolPlayers.Add(LolPlayer16);
            LolPlayer17.LolTeam_Id = LolTeam4.Id; LolTeam4.LolPlayers.Add(LolPlayer17);
            LolPlayer18.LolTeam_Id = LolTeam4.Id; LolTeam4.LolPlayers.Add(LolPlayer18);
            LolPlayer19.LolTeam_Id = LolTeam4.Id; LolTeam4.LolPlayers.Add(LolPlayer19);
            LolPlayer20.LolTeam_Id = LolTeam4.Id; LolTeam4.LolPlayers.Add(LolPlayer20);
            LolPlayer21.LolTeam_Id = LolTeam5.Id; LolTeam5.LolPlayers.Add(LolPlayer21);
            LolPlayer22.LolTeam_Id = LolTeam5.Id; LolTeam5.LolPlayers.Add(LolPlayer22);
            LolPlayer23.LolTeam_Id = LolTeam5.Id; LolTeam5.LolPlayers.Add(LolPlayer23);
            LolPlayer24.LolTeam_Id = LolTeam5.Id; LolTeam5.LolPlayers.Add(LolPlayer24);
            LolPlayer25.LolTeam_Id = LolTeam5.Id; LolTeam5.LolPlayers.Add(LolPlayer25);



            List<LolManager> LolManager = new List<LolManager>();
            LolManager.Add(LolManager1);
            LolManager.Add(LolManager2);
            LolManager.Add(LolManager3);
            LolManager.Add(LolManager4);
            LolManager.Add(LolManager5);


            return LolManager.AsQueryable();
        }
    }
}
