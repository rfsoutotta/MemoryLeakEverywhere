using MemoryLeakEverywhere.ViewModels;

namespace MemoryLeakEverywhere.Views;

[QueryProperty(nameof(Parameters), "parameters")]
public partial class CollectionViewSamplePage : ContentPage
{
    private System.Timers.Timer aTimer;

    public CollectionViewSamplePage(CollectionViewSampleViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    private Dictionary<string, object> _parameters;
    public Dictionary<string, object> Parameters
    {
        get => _parameters;
        set => _parameters = value;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((CollectionViewSampleViewModel)BindingContext).LoadData();

        // Create a timer with a two second interval.
        aTimer = new System.Timers.Timer(1000);
        // Hook up the Elapsed event for the timer. 
        aTimer.Elapsed += ATimer_Elapsed;
        aTimer.Start();
    }

    private void ATimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        aTimer.Stop();

        MainThread.BeginInvokeOnMainThread(() => Shell.Current.SendBackButtonPressed());
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }
}