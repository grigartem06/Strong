using Strong.ViewModels;

namespace Strong.Pages;

public partial class InputPage : ContentPage
{
	public InputPage()
	{
		InitializeComponent(); BindingContext = new InputPageViewModel();

    }
}