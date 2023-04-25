using CommunityToolkit.Mvvm.ComponentModel;
using MemoryLeakEverywhere.Models;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Input;

namespace MemoryLeakEverywhere.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private System.Timers.Timer timer;

        [ObservableProperty]
        public DateTime? currentTime;

        public MainViewModel()
        {
        }

        public ICommand NavigateToCollectionViewSampleCommand => new Command(async () => await NavigateToCollectionViewSample());

        public async Task NavigateToCollectionViewSample()
        {
            var parameters = new Dictionary<string, object>()
            {
                { "Sample", "Hi" },
                { "Test", 1 },
            };
            
            await Shell.Current.GoToAsync("CollectionViewSamplePage", false, new Dictionary<string, object>
            {
                { "parameters", parameters }
            });
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

        public void OnAppearing()
        {
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
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    Console.WriteLine("Navigating to CollectionViewSample...");
                    await NavigateToCollectionViewSample();
                    Console.WriteLine("Navigating to CollectionViewSample ends");
                });
            }
            else
            {
                CurrentTime = CurrentTime.Value.AddSeconds(-1);
            }
        }
    }
}