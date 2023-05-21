using CommunityToolkit.Mvvm.ComponentModel;

namespace EditorGuiApp.ViewModel
{
    internal class CalendarViewModel : ObservableObject
    {
        int age;


        public int Age
        {
            get => age;
            set
            {
                if (age != value)
                {
                    age = value;
                    OnPropertyChanged(nameof(Age));
                }
            }
            //set => setProperty(ref Age, value);
        }

        //public int ClickCommand;

        public CalendarViewModel()
        {

        }

    }
}
