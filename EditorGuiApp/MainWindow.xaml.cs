using EditorGuiApp.ViewModel;
using JCNUloha2;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EditorGuiApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private NameCalendar _calendar;
    public MainWindow()
    {
        InitializeComponent();
        _calendar = new NameCalendar();
        DataContext = new CalendarViewModel();

        ComboBox.Items.Add("Január");
        ComboBox.Items.Add("Február");
        ComboBox.Items.Add("Marec");
        ComboBox.Items.Add("Apríl");
        ComboBox.Items.Add("Máj");
        ComboBox.Items.Add("Jún");
        ComboBox.Items.Add("Júl");
        ComboBox.Items.Add("August");
        ComboBox.Items.Add("September");
        ComboBox.Items.Add("Október");
        ComboBox.Items.Add("November");
        ComboBox.Items.Add("December");
        //ComboBox.SelectedValue = ComboBox.Items.GetItemAt(0);

        ShowOnCalendarButton.IsEnabled = false;
    }
    private void OnNew(object sender, RoutedEventArgs e)
    {
        if (_calendar.NameCount > 0)
        {
            var buttnos = MessageBoxButton.YesNo;
            var result = MessageBox.Show("Chcete vynulovať kalendár mien?", "Nový", buttnos);
            if (result == MessageBoxResult.Yes)
            {
                _calendar.Clear();
            }
        }
    }

    private void OnOpen(object sender, RoutedEventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Filter = "CSV súbory (*.csv) | *.csv";
        ofd.ShowDialog();
        FileInfo subor = new FileInfo(ofd.FileName);
        _calendar.Load(subor);
    }
    private void OnSave(object sender, RoutedEventArgs e)
    {
        SaveFileDialog sfd = new SaveFileDialog();
        sfd.Filter = "CSV súbory (*.csv) | *.csv";
        sfd.ShowDialog();
        FileInfo subor = new FileInfo(sfd.FileName);
        _calendar.Save(subor);
    }

    private void OnExit(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void OnAbout(object sender, RoutedEventArgs e)
    {
        AboutWindow aboutWindow = new AboutWindow();
        aboutWindow.Show();
    }

    private void DoNothing()
    {

    }

    private void onTodayClick(object sender, RoutedEventArgs e)
    {
        VypisGui.Clear();
        Celebrates.Text = $"{DateTime.Now.ToString("dd.MM.yyyy")} celebretes: ";
        var filteredList = _calendar.GetNamedays().Where(n => n.DayMonth.Day == DateTime.Now.Day && n.DayMonth.Month == DateTime.Now.Month);
        foreach (var element in filteredList)
            VypisGui.AppendText($"{element.Name}\n");


        CalendarGui.SelectedDate = DateTime.Now;
    }

    private void OnCalDateChanged(object sender, MouseButtonEventArgs e)
    {
        VypisGui.Clear();

        DateTime calDate = CalendarGui.SelectedDate!.Value;
        Celebrates.Text = $"{calDate.ToString("dd.MM.yyyy")} celebretes: ";
        var filteredList = _calendar.GetNamedays().Where(n => n.DayMonth.Day == calDate.Day && n.DayMonth.Month == calDate.Month);
        foreach (var element in filteredList)
            VypisGui.AppendText($"{element.Name}\n");
    }

    private void OnClearFilterClick(object sender, RoutedEventArgs e)
    {
        ComboBox.SelectedValue = false;
        FilterTextBox.Clear();
        FilteredVypisGui.Clear();
        ListBox.Items.Clear();
    }

    private void onCahnge(object sender, TextChangedEventArgs e)
    {
        ListBox.Items.Clear();
        //FilteredVypisGui.Clear();
        //FilteredVypisGui.AppendText(FilterTextBox.Text);
        var mesiac = ComboBox.SelectedIndex;
        var filteredList = _calendar.GetNamedays().Where(n => n.DayMonth.Month == mesiac + 1 && n.Name.Contains(FilterTextBox.Text));
        foreach (var element in filteredList)
        {
            //FilteredVypisGui.AppendText($"{element.Name}\n");
            ListBox.Items.Add($"{element.DayMonth.Day}.{element.DayMonth.Month}. \n{element.Name}");
        }
    }

    private void OnComboBoxChanged(object sender, SelectionChangedEventArgs e)
    {
        //FilteredVypisGui.Clear();
        ListBox.Items.Clear();
        //FilteredVypisGui.AppendText(FilterTextBox.Text);
        var mesiac = ComboBox.SelectedIndex;
        var filteredList = _calendar.GetNamedays().Where(n => n.DayMonth.Month == mesiac + 1 && n.Name.Contains(FilterTextBox.Text));
        foreach (var element in filteredList)
        {
            //FilteredVypisGui.AppendText($"{element.Name}\n");
            ListBox.Items.Add($"{element.DayMonth.Day}.{element.DayMonth.Month}. \n{element.Name}");
            ListBox.Items.Add(element.DayMonth);
        }
    }

    private void OnAddClick(object sender, RoutedEventArgs e)
    {
        NewNameWindow newNameWindow = new NewNameWindow();
        newNameWindow.Show();
    }

    private void OnRemoveClick(object sender, RoutedEventArgs e)
    {
        var sprava = "Do you really want to remove selected nameday?"; //TODO (MENO)
        var buttons = MessageBoxButton.YesNo;
        var result = MessageBox.Show(sprava, "Remove nameday", buttons, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {

        }
        else
        {

        }
    }

    private void OnClicShowOnCalendar(object sender, RoutedEventArgs e)
    {
        string[] riadky = ListBox.SelectedItem.ToString()!.Split(' ');
        string[] datum = riadky[0].Split('.');

        int den = int.Parse(datum[0]);
        int mesiac = int.Parse(datum[1]);
        DateTime date = new DateTime(2001, mesiac, den, 0, 0, 0);

        CalendarGui.SelectedDate = date;
    }

    private void OnSelectionChange(object sender, SelectionChangedEventArgs e)
    {
        if (ListBox.SelectedItem == null || ListBox.Items.Count == 0)
            ShowOnCalendarButton.IsEnabled = false;
        else
            ShowOnCalendarButton.IsEnabled = true;
    }
}
