using Strong.ViewModels;

namespace Strong.Pages.StudentsPages;

public partial class NewTrainingPage : ContentPage
{
	public NewTrainingPage(NewTrainingViewModel VM)
	{
		InitializeComponent();BindingContext = VM;
	}

    

}