﻿using MemoryLeakEverywhere.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MemoryLeakEverywhere.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            LoadData();
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

        private void LoadData()
        {
            
        }
    }
}