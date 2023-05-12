using ConsoleTools;
using GBJ0CK_HFT_2021222.Models;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace GBJ0CK_HFT_2021222.Client
{
    internal class Program
    {
        public static RestService rserv = new RestService("http://localhost:21741");
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(8000);


            var menu = new ConsoleMenu()
               .Add("CRUD methods", () => CrudMenu())
               .Add("non-CRUD methods", () => NonCrudMenu())
               .Add("Exit", ConsoleMenu.Close);
            menu.Show();
        }

        private static void CrudMenu()
        {

            var menu = new ConsoleMenu()
                .Add("Create element", CreatePreMenu)
                .Add("Get one element", ReadPreMenu)
                .Add("Get all element", ReadAllPreMenu)
                .Add("Update element", UpdatePreMenu)
                .Add("Delete element", DeletePreMenu)
                .Add("Exit", ConsoleMenu.Close);
            menu.Show();
        }

        private static void NonCrudMenu()
        {
            var menu = new ConsoleMenu()
               .Add("Get LolPlayer(s) that are owned by Bengi", GetLolPlayerWhereLolTeamOwnerIsBengi)
               .Add("Get LolPlayer(s) that have more than 28 employees at their LolManagers", GetLolPlayerWhereMoreThan28Employees)
               .Add("Get LolManager(s) that have a LolPlayer with 18 age", GetLolManagerWhereLolPlayer18)
               .Add("Get LolManager(s) that have a LolPlayer model named Zeus", GetLolManagerWhereLolPlayerModelIsZeus)
               .Add("Get LolManager(s) that have a LolPlayer that costs 100", GetLolManagerWherePriceIs100)
               .Add("Exit", ConsoleMenu.Close);
            menu.Show();
        }

        private static void PreMenu(Action lolplayer, Action lolteam, Action lolmanager)
        {
            var menu = new ConsoleMenu()
                .Add("LolPlayer", lolplayer)
                .Add("LolTeam", lolteam)
                .Add("LolManager", lolmanager)
                .Add("Exit", ConsoleMenu.Close);
            menu.Show();
        }

        private static void CreatePreMenu()
        {
            PreMenu(CreateLolPlayer, CreateLolTeam, CreateLolManager);
        }

        private static void CreateLolManager()
        {
            Console.WriteLine("Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Number of employees:");
            int employees = int.Parse(Console.ReadLine());
            rserv.Post<LolManager>(new LolManager() { Name = name, Employees = employees }, "lolmanager");
        }

        private static void CreateLolTeam()
        {
            Console.WriteLine("Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Owner:");
            string owner = Console.ReadLine();
            Console.WriteLine("LolManager id: ");
            int lolmanagerid = int.Parse(Console.ReadLine());
            rserv.Post<LolTeam>(new LolTeam() { Name = name, Owner = owner, LolManager_Id = lolmanagerid }, "lolteam");
        }

        private static void CreateLolPlayer()
        {
            Console.WriteLine("Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Age: ");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("LolTeam id: ");
            int lolteamid = int.Parse(Console.ReadLine());
            rserv.Post<LolPlayer>(new LolPlayer() { Name = name, Age = age, LolTeam_Id = lolteamid }, "lolplayer");
        }

        private static void ReadPreMenu()
        {
            PreMenu(ReadLolPlayer, ReadLolTeam, ReadLolManager);
        }

        private static void ReadLolManager()
        {
            Console.WriteLine("Search for desired with an Id of: ");
            int id = int.Parse(Console.ReadLine());
            var getLolManager = rserv.Get<LolManager>(id, "lolmanager");
            Console.WriteLine($"Id: {getLolManager.Id}, Name: {getLolManager.Name}, Employees: {getLolManager.Employees}");
            Console.ReadLine();

        }

        private static void ReadLolTeam()
        {
            Console.WriteLine("Search for desired with an Id of: ");
            int id = int.Parse(Console.ReadLine());
            var getLolTeam = rserv.Get<LolTeam>(id, "lolteam");
            Console.WriteLine($"Id: {getLolTeam.Id}, Name: {getLolTeam.Name}, Owner: {getLolTeam.Owner}, LolManagerID: {getLolTeam.LolManager_Id}");
            Console.ReadLine();

        }

        private static void ReadLolPlayer()
        {
            Console.WriteLine("Search for desired with an Id of: ");
            int id = int.Parse(Console.ReadLine());
            var getLolPlayer = rserv.Get<LolPlayer>(id, "lolplayer");
            Console.WriteLine($"Id: {getLolPlayer.Id}, Name: {getLolPlayer.Name}, Age: {getLolPlayer.Age}, Price: {getLolPlayer.Price}, LolTeamID: {getLolPlayer.LolTeam_Id}");
            Console.ReadLine();

        }

        private static void ReadAllPreMenu()
        {
            PreMenu(PrintAllLolPlayers, PrintAllLolTeams, PrintAllLolManagers);
        }

        private static void PrintAllLolPlayers()
        {
            var LolPlayers = rserv.Get<LolPlayer>("lolplayer");
            Console.WriteLine("-------------LolPlayers-------------");
            LolPlayerToConsole(LolPlayers);
            Console.ReadLine();
        }

        private static void PrintAllLolTeams()
        {
            var LolTeams = rserv.Get<LolTeam>("lolteam");
            Console.WriteLine("-------------LolTeams-------------");
            LolTeamToConsole(LolTeams);
            Console.ReadLine();
        }

        private static void PrintAllLolManagers()
        {
            var LolManagers = rserv.Get<LolManager>("lolmanager");
            Console.WriteLine("-------------LolManagers-------------");
            LolManagerToConsole(LolManagers);
            Console.ReadLine();
        }

        private static void UpdatePreMenu()
        {
            PreMenu(UpdateLolPlayer, UpdateLolTeam, UpdateLolManager);
        }

        private static void UpdateLolManager()
        {
            Console.WriteLine("Id: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Employees:");
            int employees = int.Parse(Console.ReadLine());
            LolManager input = new LolManager() { Id = id, Name = name, Employees = employees };
            rserv.Put(input, "lolmanager");
        }

        private static void UpdateLolTeam()
        {
            Console.WriteLine("Id: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Owner:");
            string owner = Console.ReadLine();
            Console.WriteLine("LolManager id: ");
            int LolManagerid = int.Parse(Console.ReadLine());
            LolTeam input = new LolTeam() { Id = id, Name = name, Owner = owner, LolManager_Id = LolManagerid };
            rserv.Put(input, "lolteam");
        }

        private static void UpdateLolPlayer()
        {
            Console.WriteLine("Id: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Age:");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("Price:");
            int price = int.Parse(Console.ReadLine());
            Console.WriteLine("LolTeam id: ");
            int LolTeamid = int.Parse(Console.ReadLine());
            LolPlayer input = new LolPlayer() { Id = id, Name = name, Age = age, Price = price, LolTeam_Id = LolTeamid };
            rserv.Put(input, "lolplayer");
        }

        private static void DeletePreMenu()
        {
            PreMenu(DeleteLolPlayer, DeleteLolTeam, DeleteLolManager);
        }

        private static void DeleteLolManager()
        {
            Console.WriteLine("Delete element with an Id of: ");
            int id = int.Parse(Console.ReadLine());
            rserv.Delete(id, "lolmanager");
        }

        private static void DeleteLolTeam()
        {
            Console.WriteLine("Delete element with an Id of: ");
            int id = int.Parse(Console.ReadLine());
            rserv.Delete(id, "lolteam");
        }

        private static void DeleteLolPlayer()
        {
            Console.WriteLine("Delete element with an Id of: ");
            int id = int.Parse(Console.ReadLine());
            rserv.Delete(id, "lolplayer");
        }



        private static void GetLolManagerWhereLolPlayer18()
        {
            var output = rserv.Get<LolManager>("stat/GetLolManagerWhereLolPlayer18");
            LolManagerToConsole(output);
            Console.ReadLine();
        }
        private static void GetLolManagerWhereLolPlayerModelIsZeus()
        {
            var output = rserv.Get<LolManager>("stat/GetLolManagerWhereLolPlayerModelIsZeus");
            LolManagerToConsole(output);
            Console.ReadLine();
        }

        private static void GetLolManagerWherePriceIs100()
        {
            var output = rserv.Get<LolManager>("stat/GetLolManagerWherePriceIs100");
            LolManagerToConsole(output);
            Console.ReadLine();
        }
        private static void GetLolPlayerWhereMoreThan28Employees()
        {
            var output = rserv.Get<LolPlayer>("stat/GetLolPlayerWhereMoreThan28Employees");
            LolPlayerToConsole(output);
            Console.ReadLine();
        }
        private static void GetLolPlayerWhereLolTeamOwnerIsBengi()
        {
            var output = rserv.Get<LolPlayer>("stat/GetLolPlayerWhereLolTeamOwnerIsBengi");
            LolPlayerToConsole(output);
            Console.ReadLine();
        }



        private static void LolPlayerToConsole(IEnumerable<LolPlayer> input)
        {
            foreach (var item in input)
            {
                Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Age: {item.Age}, Price: {item.Price}, LolTeamID: {item.LolTeam_Id}");
            }
        }
        private static void LolTeamToConsole(IEnumerable<LolTeam> input)
        {
            foreach (var item in input)
            {
                Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Owner: {item.Owner}, LolManagerID: {item.LolManager_Id}");
            }
        }
        private static void LolManagerToConsole(IEnumerable<LolManager> input)
        {
            foreach (var item in input)
            {
                Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Employees: {item.Employees}");
            }
        }
    }
}
