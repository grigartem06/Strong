using Strong.ViewModels;

namespace Strong.Pages.StudentsPages;

public partial class MeasurementsPage : ContentPage
{
	public MeasurementsPage(MeasurementsViewModel vm)
	{
		InitializeComponent(); BindingContext = vm;
	}
}