using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Input;
using MemoryLeakEverywhere.Models;

namespace MemoryLeakEverywhere.ViewModels
{
    public class CollectionViewSampleViewModel : BaseViewModel
    {
        private System.Timers.Timer timer;

        public DateTime? CurrentTime;

        private bool randomBool = true;
        public CollectionViewSampleViewModel()
        {
            LoadData();

        }

        public ICommand ChangeCategoryCommand => new Command(() => ChangeCategory());

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

        private ObservableCollection<Category> _categories = new ObservableCollection<Category>
        {
            new Category
            {
                Name = "Refresh ItemsSource"
            }
        };
        public ObservableCollection<Category> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                RaiseOnPropertyChanged();
            }
        }

        private void ChangeCategory()
        {
            RandomItems.Clear();
            LoadData();
        }

        private void LoadData()
        {
            var rnd = new Random();
            var itemsNumber = rnd.Next(25, 50);
            ObservableCollection<RandomItem> newItems = new ObservableCollection<RandomItem>();

            for (var i = 0; i < itemsNumber; i++)
            {
                randomBool = !randomBool;
                var randomImage = i % 2 == 0 && randomBool ? "dotnet_bot" : "monkey";
                newItems.Add(new RandomItem
                {
                    Name = $"Item {i}",
                    Image = randomImage
                });
            }

            RandomItems = new ObservableCollection<RandomItem>(newItems);
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
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Console.WriteLine("Refreshing...");
                    ChangeCategory();
                    Console.WriteLine("Refresh ends");
                });
            }
            else
            {
                CurrentTime = CurrentTime.Value.AddSeconds(-1);
            }
        }
    }
}