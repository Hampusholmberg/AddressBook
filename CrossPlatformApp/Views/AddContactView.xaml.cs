using CrossPlatformApp.ViewModels;

namespace CrossPlatformApp.Views;

public partial class AddContactView : ContentPage
{
	public AddContactView(AddContactViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}