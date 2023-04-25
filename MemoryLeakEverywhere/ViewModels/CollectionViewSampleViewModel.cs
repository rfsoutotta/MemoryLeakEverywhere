using CommunityToolkit.Mvvm.ComponentModel;
using MemoryLeakEverywhere.Models;
using System.Collections.ObjectModel;
using System.Timers;

namespace MemoryLeakEverywhere.ViewModels
{
    public partial class CollectionViewSampleViewModel : BaseViewModel
    {
        private System.Timers.Timer timer;

        [ObservableProperty]
        public DateTime? currentTime;

        public CollectionViewSampleViewModel()
        {
        }

        private ObservableCollection<RandomItem> _randomItems = new ObservableCollection<RandomItem>();
        public ObservableCollection<RandomItem> RandomItems
        {
            get => _randomItems;
            set
            {
                _randomItems = value;
                RaiseOnPropertyChanged();
            }
        }

        public void LoadData()
        {
            RandomItems.Clear();

            var tempRandomItems = new ObservableCollection<RandomItem>();

            for (var i = 0; i < 50; i++)
            {
                tempRandomItems.Add(new RandomItem
                {
                    Name = $"Item {i}: {new Random().Next(1, 1000)}"
                });
            }

            RandomItems = tempRandomItems;
        }

        public void OnAppearing()
        {
            LoadData();
            InitTimer();
        }

        protected virtual void InitTimer()
        {
            CurrentTime = null;

            timer = new System.Timers.Timer(1000)
            {
                AutoReset = true,
                Enabled = true,
            };

            timer.Elapsed += DoRunTimer;
            timer.Start();
        }

        protected virtual void StopTimer()
        {
            CurrentTime = null;
            if (timer != null)
            {
                timer.Elapsed -= DoRunTimer;
                timer.Stop();
                timer = null;
            }
        }

        private async void DoRunTimer(object sender, ElapsedEventArgs e)
        {
            if (!CurrentTime.HasValue)
            {
                CurrentTime = new DateTime(2001, 1, 1, 0, 0, 0).AddSeconds(1);
            }
            else if (CurrentTime.Value.Minute == 0 && CurrentTime.Value.Second == 0)
            {
                StopTimer();
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Console.WriteLine("Navigating back...");
                    Shell.Current.SendBackButtonPressed();
                    Console.WriteLine("Navigating back ends");
                });
            }
            else
            {
                CurrentTime = CurrentTime.Value.AddSeconds(-1);
            }
        }
    }
}