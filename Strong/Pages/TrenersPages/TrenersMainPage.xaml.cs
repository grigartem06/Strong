using Strong.ViewModels;

namespace Strong.Pages.TrenersPages;

public partial class TrenersMainPage : ContentPage
{
	public TrenersMainPage(TrenersMainPageViewModel vm)	{InitializeComponent(); BindingContext = vm;}
}