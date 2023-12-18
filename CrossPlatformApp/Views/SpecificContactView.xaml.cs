using CrossPlatformApp.ViewModels;

namespace CrossPlatformApp.Views;

public partial class SpecificContactView : ContentPage
{
	public SpecificContactView(SpecificContactViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}