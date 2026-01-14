using SQLite;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Strong.Models
{
    public class SetsTable : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int sets_id { get; set; }

        [ForeignKey(nameof(ExerciseTable))]
        public int exercise_id { get; set; }

        private double _exercise_weight;
        public double exercise_weight
        {
            get => _exercise_weight;
            set
            {
                if (_exercise_weight != value)
                {
                    _exercise_weight = value;
                    OnPropertyChanged(nameof(exercise_weight));
                    ValueChanged?.Invoke(this);
                }
            }
        }

        private double _exercise_reps;
        public double exercise_reps
        {
            get => _exercise_reps;
            set
            {
                if (_exercise_reps != value)
                {
                    _exercise_reps = value;
                    OnPropertyChanged(nameof(exercise_reps));
                    ValueChanged?.Invoke(this);
                }
            }
        }

        [ForeignKey(nameof(TrainingTable))]
        public int training_id { get; set; }

        private double _rest_time;
        public double rest_time
        {
            get => _rest_time;
            set
            {
                if (_rest_time != value)
                {
                    _rest_time = value;
                    OnPropertyChanged(nameof(rest_time));
                    ValueChanged?.Invoke(this);
                }
            }
        }

        public event Action<SetsTable> ValueChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}