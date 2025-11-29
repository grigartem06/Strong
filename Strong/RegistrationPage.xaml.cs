using Strong.ViewModels;

namespace Strong
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent(); BindingContext = new RegistrationPageViewModel();
        }
    }
}
