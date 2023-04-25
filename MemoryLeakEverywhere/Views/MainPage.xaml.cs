using MemoryLeakEverywhere.ViewModels;

namespace MemoryLeakEverywhere.Views;

public partial class MainPage : ContentPage
{
    private System.Timers.Timer aTimer;

    public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((MainViewModel)BindingContext).OnAppearing();
    }
}