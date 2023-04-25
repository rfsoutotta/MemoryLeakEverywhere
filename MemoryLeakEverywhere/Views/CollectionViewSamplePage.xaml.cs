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
        ((CollectionViewSampleViewModel)BindingContext).OnAppearing();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }
}