using CrossPlatformApp.ViewModels;

namespace CrossPlatformApp.Views;

public partial class ContactListView : ContentPage
{
    public ContactListView(ContactListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}