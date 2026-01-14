using Strong.ViewModels;

namespace Strong.Pages.StudentsPages;

public partial class AboutTrainingPage : ContentPage
{
	public AboutTrainingPage(AboutTrainingPageViewModel vm)
	{
		InitializeComponent(); BindingContext = vm;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is AboutTrainingPageViewModel vm)
        {
            await vm.InitializeAsync(); // Вызываем инициализацию при отображении страницы
        }
    }


}