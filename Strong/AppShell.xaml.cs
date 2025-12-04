using Strong.Pages;
using Strong.Pages.StudentsPages;
using Strong.Pages.TrenersPages;
using Strong.ViewModels;

namespace Strong
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("//Pages/InputPage", typeof(InputPage));
            Routing.RegisterRoute("//RegistrationPage", typeof(MainPage));

            Routing.RegisterRoute("//Pages/TrenersPages/TrenersMainPage", typeof(TrenersMainPage));
            Routing.RegisterRoute("//Pages/TrenersPages/AddStudentToListPage", typeof(AddStudentToListPage));

            Routing.RegisterRoute("//Pages/StudentsPages/StudentMainPage", typeof(StudentMainPage));
            Routing.RegisterRoute("//Pages/StudentsPages/ExercisePage", typeof(ExercisePage));

            Routing.RegisterRoute("//Pages/StudentsPages/MeasurementsPage", typeof(MeasurementsPage));
            Routing.RegisterRoute("//Pages/StudentsPages/NewTrainingPage", typeof(NewTrainingPage));

            Routing.RegisterRoute("//Pages/StudentsPages/AllTrainingsPage", typeof(AllTrainingsPage));
            Routing.RegisterRoute("//Pages/StudentsPages/AboutTrainingPage", typeof(AboutTrainingPage));


        }
    
    }
}
