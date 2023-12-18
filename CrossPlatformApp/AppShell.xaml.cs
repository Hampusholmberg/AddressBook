using CrossPlatformApp.Views;

namespace CrossPlatformApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ContactListView), typeof(ContactListView));

            Routing.RegisterRoute(nameof(AddContactView), typeof(AddContactView));

            Routing.RegisterRoute(nameof(EditContactView), typeof(EditContactView));

            Routing.RegisterRoute(nameof(SpecificContactView), typeof(SpecificContactView));
        }
    }
}
