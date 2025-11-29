using Strong.ViewModels;

namespace Strong.Pages.TrenersPages;

public partial class AddStudentToListPage : ContentPage
{
	public AddStudentToListPage(AddStudentToListViewModel vm)
	{
		InitializeComponent(); BindingContext = vm;

//            public TrenersMainPage(TrenersMainPageViewModel vm) { InitializeComponent(); BindingContext = vm; }

} 
}