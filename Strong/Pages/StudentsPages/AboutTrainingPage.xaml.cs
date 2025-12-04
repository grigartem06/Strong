using Strong.ViewModels;

namespace Strong.Pages.StudentsPages;

public partial class AboutTrainingPage : ContentPage
{
	public AboutTrainingPage(AboutTrainingPageViewModel vm)
	{
		InitializeComponent(); BindingContext = vm;
	}
}