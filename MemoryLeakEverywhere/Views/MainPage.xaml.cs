using MemoryLeakEverywhere.ViewModels;

namespace MemoryLeakEverywhere.Views;

public partial class MainPage : ContentPage
{
    private System.Timers.Timer aTimer;

    public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        // Create a timer with a two second interval.
        aTimer = new System.Timers.Timer(1000);
        // Hook up the Elapsed event for the timer. 
        aTimer.Elapsed += ATimer_Elapsed;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        aTimer.Start();
    }

    private void ATimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        aTimer.Stop();
        MainThread.BeginInvokeOnMainThread(async () => await ((MainViewModel)BindingContext).NavigateToCollectionViewSample());
    }
}