using SystemOfManagingCollections.Views;

namespace SystemOfManagingCollections
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemCollectionPage), typeof(ItemCollectionPage));
            Routing.RegisterRoute(nameof(EditItemPage), typeof(EditItemPage));
        }
    }
}
