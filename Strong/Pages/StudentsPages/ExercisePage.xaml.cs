using Strong.ViewModels;

namespace Strong.Pages.StudentsPages;

public partial class ExercisePage : ContentPage
{
	public ExercisePage(ExercisePageViewModel vm)
	{
		InitializeComponent();BindingContext = vm;
	}
}