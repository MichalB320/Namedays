using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

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

        public int ClickCommand;

        public CalendarViewModel()
        {
            
        }

    }
}
