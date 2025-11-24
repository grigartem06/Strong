using Strong.Pages;
using Strong.Pages.TrenersPages;

namespace Strong
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("//Pages/InputPage", typeof(InputPage));
            Routing.RegisterRoute("//RegistrationPage", typeof(MainPage));
            Routing.RegisterRoute("//Pages/TrenersPages/TrenersMainPage", typeof(TrenersMainPage));
        }
    
    }
}
