using Strong.ViewModels;

namespace Strong.Pages.StudentsPages;

public partial class StudentMainPage : ContentPage
{
	public StudentMainPage(StudentsMainPageviewModel vm)
	{
		InitializeComponent(); BindingContext = vm;
	}
}