using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Shared.BaseModels;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace Burger_Dojo3.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        private Simulator sim;
        private List<ItemVm> modelItems = new List<ItemVm>();
        public ObservableCollection<ItemVm> SensorList { get; set; }
        public ObservableCollection<ItemVm> ActorList { get; set; }

        private string currTime = DateTime.Now.ToLocalTime().ToShortTimeString();
        private string currDate = DateTime.Now.ToLocalTime().ToShortDateString();

        public ObservableCollection<string> ActorModeSelectionList { get; private set; }
        public ObservableCollection<string> SensorModeSelectionList { get; private set; }

        public string CurrTime
        {
            get { return currTime; }
            set { currTime = value; RaisePropertyChanged(); }
        }

        public string CurrDate
        {
            get { return currDate; }
            set { currDate = value; RaisePropertyChanged(); }
        }

        public MainViewModel()
        {
            //Splitted, so that only the modes available for either Actors or Sensors are chooseable
            ActorModeSelectionList = new ObservableCollection<string>();
            SensorModeSelectionList = new ObservableCollection<string>();

            SensorList = new ObservableCollection<ItemVm>();
            ActorList = new ObservableCollection<ItemVm>();

            //Fill ModesList for Sensors
            foreach (var item in Enum.GetNames(typeof(SensorModeType)))
            {
                SensorModeSelectionList.Add(item);
            }

            //Fill ModesList for Actors
            foreach(var item in Enum.GetNames(typeof(ModeType)))
            {
                ActorModeSelectionList.Add(item);
            }

            //Update time and date
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 20);
            timer.Tick += UpdateTime;

            if (!IsInDesignMode)
            {
                LoadData();
                timer.Start();
            }
        }

        private void LoadData()
        {
            Simulator sim = new Simulator(modelItems);

            foreach(var item in sim.Items)
            {
                if (item.ItemType.Equals(typeof(ISensor)))
                    SensorList.Add(item);
                else if (item.ItemType.Equals(typeof(IActuator)))
                    ActorList.Add(item);


            }
        }

        private void UpdateTime(object sender, EventArgs e)
        {
            CurrTime = DateTime.Now.ToLocalTime().ToShortTimeString();
            CurrDate = DateTime.Now.ToLocalTime().ToShortDateString();
        }

    }
}