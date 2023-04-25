using MemoryLeakEverywhere.Models;
using System.Collections.ObjectModel;

namespace MemoryLeakEverywhere.ViewModels
{
    public class CollectionViewSampleViewModel : BaseViewModel
    {
        public CollectionViewSampleViewModel()
        {
            LoadData();
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
            for (var i = 0; i < 50; i++)
            {
                RandomItems.Add(new RandomItem
                {
                    Name = $"Item {i}"
                });
            }
        }

    }
}