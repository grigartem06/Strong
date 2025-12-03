using Microsoft.Extensions.Logging;
using Strong.Pages.StudentsPages;
using Strong.Pages.TrenersPages;
using Strong.ViewModels;

namespace Strong
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<TrenersMainPage>();
            builder.Services.AddSingleton<TrenersMainPageViewModel>();

            builder.Services.AddSingleton<AddStudentToListPage>();
            builder.Services.AddSingleton<AddStudentToListViewModel>();

            builder.Services.AddSingleton<StudentsMainPageviewModel>();
            builder.Services.AddSingleton<StudentMainPage>();

            builder.Services.AddSingleton<ExercisePage>();
            builder.Services.AddSingleton<ExercisePageViewModel>();
            
            builder.Services.AddSingleton<MeasurementsPage>();
            builder.Services.AddSingleton<MeasurementsViewModel>();

            builder.Services.AddSingleton<NewTrainingPage>();
            builder.Services.AddSingleton<NewTrainingViewModel>();

            


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
