using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Strong.DataBase;
using Strong.Models;
using Strong.Pages.TrenersPages;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static Strong.DataBase.ToDoDataBase;


namespace Strong.ViewModels
{
    public partial class TrenersMainPageViewModel : ObservableObject
    {
        private readonly ToDoDataBase _database;

        [ObservableProperty]
        private TrainerTable trainer;

        [ObservableProperty]
        private ObservableCollection<StudentTable> studentsList;
        public int id;
        public TrenersMainPageViewModel()
        {
            _database = new ToDoDataBase();
            LoadStudents();
        }
        private async Task GetSecret()
        {
            string x = await SecureStorage.Default.GetAsync("trenerID");
            id = Convert.ToInt32(x);
        }

        [RelayCommand]
        public async Task AddStudentToList() 
        {
            await Shell.Current.GoToAsync("//Pages/TrenersPages/AddStudentToListPage");
        }

        [RelayCommand]
        public async Task LoadStudents()
        {
            await GetSecret();
            var students = await _database.GetStudentsWithTrener(id);
            StudentsList = new ObservableCollection<StudentTable>(students);
        }
    }
}
