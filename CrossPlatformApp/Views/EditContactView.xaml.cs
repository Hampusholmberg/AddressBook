using CrossPlatformApp.ViewModels;

namespace CrossPlatformApp.Views;

public partial class EditContactView : ContentPage
{
	public EditContactView(EditContactViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}