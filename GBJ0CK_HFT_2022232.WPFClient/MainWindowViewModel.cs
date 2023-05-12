using GBJ0CK_HFT_2021222.Endpoint;
using GBJ0CK_HFT_2021222.Logic;
using GBJ0CK_HFT_2021222.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GBJ0CK_HFT_2022232.WPFClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<LolManager> LolManagers { get; set; }
        public RestCollection<LolTeam> LolTeams { get; set; }
        public RestCollection<LolPlayer> LolPlayers { get; set; }

        public RestService GetLolPlayerWhereMoreThan28Employeess;
        public RestService GetLolPlayerWhereLolTeamOwnerIsBengis;
        public RestService GetLolManagerWhereLolPlayer18s;
        public RestService GetLolManagerWhereLolPlayerModelIsZeuss;
        public RestService GetLolManagerWherePriceIs100s;

        private LolManager selectedLolManager;
        private LolTeam selectedLolTeam;
        private LolPlayer selectedLolPlayer;

        public LolManager SelectedLolManager
        {
            get { return selectedLolManager; }
            set
            {
                if (value != null)
                {
                    selectedLolManager = new LolManager()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        Employees = value.Employees


                    };
                    OnPropertyChanged();
                    (DeleteLolManager as RelayCommand).NotifyCanExecuteChanged();
                    //(UpdateLolManager as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        public LolTeam SelectedLolTeam
        {
            get => selectedLolTeam;
            set
            {
                if (value != null)
                {
                    selectedLolTeam = new LolTeam()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        Owner = value.Owner,
                        LolManager_Id = value.LolManager_Id


                    };
                    OnPropertyChanged();
                    (DeleteLolTeam as RelayCommand).NotifyCanExecuteChanged();
                    //(UpdateLolTeam as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        public LolPlayer SelectedLolPlayer
        {
            get => selectedLolPlayer;
            set
            {
                if (value != null)
                {
                    selectedLolPlayer = new LolPlayer()
                    {
                        Id = value.Id,
                        Name = value.Name,
                        Age = value.Age,
                        LolTeam_Id = value.LolTeam_Id

                    };
                    OnPropertyChanged();
                    (DeleteLolPlayer as RelayCommand).NotifyCanExecuteChanged();
                    //(UpdateLolPlayer as RelayCommand).NotifyCanExecuteChanged();

                }
            }
        }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public ICommand CreateLolManager { get; set; }
        public ICommand UpdateLolManager { get; set; }
        public ICommand DeleteLolManager { get; set; }

        public ICommand CreateLolTeam { get; set; }
        public ICommand UpdateLolTeam { get; set; }
        public ICommand DeleteLolTeam { get; set; }

        public ICommand CreateLolPlayer { get; set; }
        public ICommand UpdateLolPlayer { get; set; }
        public ICommand DeleteLolPlayer { get; set; }

        public List<LolPlayer> GetLolPlayerWhereMoreThan28Employees
        {
            get { return GetLolPlayerWhereMoreThan28Employeess.Get<LolPlayer>("stat/GetLolPlayerWhereMoreThan28Employees"); }
        }
        public List<LolPlayer> GetLolPlayerWhereLolTeamOwnerIsBengi
        {
            get { return GetLolPlayerWhereLolTeamOwnerIsBengis.Get<LolPlayer>("stat/GetLolPlayerWhereLolTeamOwnerIsBengi"); }
        }
        public List<LolManager> GetLolManagerWhereLolPlayer18
        {
            get { return GetLolManagerWhereLolPlayer18s.Get<LolManager>("stat/GetLolManagerWhereLolPlayer18"); }
        }
        public List<LolManager> GetLolManagerWhereLolPlayerModelIsZeus
        {
            get { return GetLolManagerWhereLolPlayerModelIsZeuss.Get<LolManager>("stat/GetLolManagerWhereLolPlayerModelIsZeus"); }
        }
        public List<LolManager> GetLolManagerWherePriceIs100
        {
            get { return GetLolManagerWherePriceIs100s.Get<LolManager>("stat/GetLolManagerWherePriceIs100"); }
        }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                GetLolPlayerWhereMoreThan28Employeess = new RestService("http://localhost:21741/", "stat/GetLolPlayerWhereMoreThan28Employees");
                GetLolPlayerWhereLolTeamOwnerIsBengis = new RestService("http://localhost:21741/", "stat/GetLolPlayerWhereLolTeamOwnerIsBengi");
                GetLolManagerWhereLolPlayer18s = new RestService("http://localhost:21741/", "stat/GetLolManagerWhereLolPlayer18");
                GetLolManagerWhereLolPlayerModelIsZeuss = new RestService("http://localhost:21741/", "stat/GetLolManagerWhereLolPlayerModelIsZeus");
                GetLolManagerWherePriceIs100s = new RestService("http://localhost:21741/", "stat/GetLolManagerWherePriceIs100");

                LolManagers = new RestCollection<LolManager>("http://localhost:21741/", "lolmanager","hub");
                CreateLolManager = new RelayCommand(() =>
                {
                    LolManagers.Add(new LolManager()
                    {

                        Name = SelectedLolManager.Name,
                        Employees = SelectedLolManager.Employees

                    });
                });
                UpdateLolManager = new RelayCommand(() => LolManagers.Update(SelectedLolManager));
                DeleteLolManager = new RelayCommand(() => LolManagers.Delete(SelectedLolManager.Id), () => SelectedLolManager != null);
                SelectedLolManager = new LolManager();
                
                //---------------------------------------------------------------
                LolTeams = new RestCollection<LolTeam>("http://localhost:21741/", "lolteam","hub");
                CreateLolTeam = new RelayCommand(() =>
                {
                    LolTeams.Add(new LolTeam()
                    {


                        Name = SelectedLolTeam.Name,
                        Owner = SelectedLolTeam.Owner,
                        LolManager_Id = SelectedLolTeam.LolManager_Id

                    });
                });
                UpdateLolTeam = new RelayCommand(() => LolTeams.Update(SelectedLolTeam));
                DeleteLolTeam = new RelayCommand(() => LolTeams.Delete(SelectedLolTeam.Id), () => SelectedLolTeam != null);
                SelectedLolTeam = new LolTeam();

                //---------------------------------------------------------------
                LolPlayers = new RestCollection<LolPlayer>("http://localhost:21741/", "lolplayer","hub");
                CreateLolPlayer = new RelayCommand(() =>
                {
                    LolPlayers.Add(new LolPlayer()
                    {

                        Name = SelectedLolPlayer.Name,
                        Age = SelectedLolPlayer.Age,
                        Price = SelectedLolPlayer.Price,
                        LolTeam_Id = SelectedLolPlayer.LolTeam_Id

                    });
                });
                UpdateLolPlayer = new RelayCommand(() => LolPlayers.Update(SelectedLolPlayer));
                DeleteLolPlayer = new RelayCommand(() => LolPlayers.Delete(SelectedLolPlayer.Id), () => SelectedLolPlayer != null);
                SelectedLolPlayer = new LolPlayer();
                ;
            }
            ;

        }
        

    }
}
