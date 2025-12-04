using Strong.ViewModels;

namespace Strong.Pages.StudentsPages;

public partial class AllTrainingsPage : ContentPage
{
	public AllTrainingsPage(AllTrainingsPageViewModel vm)
	{
		InitializeComponent(); BindingContext = vm;
	}
}