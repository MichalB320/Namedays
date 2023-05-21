using System.Collections;

namespace JCNUloha2;

public record class NameCalendar : IEnumerable<Nameday>
{
    private List<Nameday> _nameDays;
    public int NameCount { get => _nameDays.Count() - 1; }
    public int DayCount { get => 365 - 3; } //365(pocet dni v roku) - 3(tri dni kde je ciarka)
    private DayMonth? this[string name]
    {
        get
        {
            if (_nameDays.Any(n => n.Name == name))
                return _nameDays.Find(n => n.Name == name).DayMonth;
            else
                return null;
        }
    }
    private string[] this[DayMonth dayMonth]
        => this[dayMonth.Day, dayMonth.Month];
    private string[] this[DateOnly date]
        => this[date.Day, date.Month];
    private string[] this[DateTime date]
        => this[date.Hour, date.Minute];
    private string[] this[int day, int month]
        => _nameDays.Where(n => n.DayMonth.Day == day && n.DayMonth.Month == month).Select(n => n.Name).ToArray();

    /// <summary>
    /// Initializes a new instance of the NameCalendar class.
    /// </summary>
    public NameCalendar()
    {
        _nameDays = new List<Nameday>();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the NameCalendar.
    /// </summary>
    /// <returns></returns>
    public IEnumerator<Nameday> GetEnumerator()
    {
        foreach (Nameday nameday in _nameDays)
            yield return nameday;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the NameCalendar.
    /// </summary>
    /// <returns></returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        foreach (Nameday nameday in _nameDays)
            yield return nameday;
    }

    /// <summary>
    /// Gets all the namedays in the NameCalendar.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Nameday> GetNamedays()
        => _nameDays;

    /// <summary>
    /// Gets all the namedays in the specified month.
    /// </summary>
    /// <param name="month"></param>
    /// <returns></returns>
    public IEnumerable<Nameday> GetNamedays(int month)
    {
        foreach (Nameday nameday in _nameDays)
        {
            if (nameday.DayMonth.Month == month)
                yield return nameday;
        }
    }

    /// <summary>
    /// Gets all the namedays with a specific name.
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public IEnumerable<Nameday> GetNamedays(string pattern)
    {
        foreach (Nameday nameday in _nameDays)
        {
            if (nameday.Name == pattern)
                yield return nameday;
        }
    }

    /// <summary>
    /// Adds a nameday to the NameCalendar.
    /// </summary>
    /// <param name="Nameday"></param>
    public void Add(Nameday Nameday)
        => _nameDays.Add(Nameday);

    /// <summary>
    /// Adds a nameday with the specified day, month, and names.
    /// </summary>
    /// <param name="day"></param>
    /// <param name="month"></param>
    /// <param name="names"></param>
    public void Add(int day, int month, params string[] names)
    {
        Nameday meniny = new Nameday(names[5], new DayMonth(day, month));
        _nameDays.Add(meniny);
    }

    /// <summary>
    /// Adds a nameday with the specified DayMonth and names.
    /// </summary>
    /// <param name="dayMonth"></param>
    /// <param name="names"></param>
    public void Add(DayMonth dayMonth, params string[] names)
    {
        Nameday meniny = new Nameday(names[5], dayMonth);
        _nameDays.Add(meniny);
    }

    /// <summary>
    /// Removes the nameday with the specified name from the NameCalendar.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool Remove(string name)
    {
        if (Contains(name))
        {
            foreach (Nameday nameday in _nameDays)
            {
                var rm = _nameDays.Find(nameday => nameday.Name == name);
                _nameDays.Remove(rm);
            }
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// Checks if the NameCalendar contains a nameday with the specified name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool Contains(string name)
    {
        foreach (var nameDay in _nameDays)
        {
            if (nameDay.Name.Equals(name)) return true;
        }
        return false;
    }

    /// <summary>
    /// Removes all namedays from the NameCalendar.
    /// </summary>
    public void Clear()
       => _nameDays.Clear();

    /// <summary>
    /// Loads namedays from a CSV file into the NameCalendar.
    /// </summary>
    /// <param name="csvFile"></param>
    public void Load(FileInfo csvFile)
    {
        using (var sr = new StreamReader(csvFile.FullName))
        {
            string? line;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                if (line is not null)
                {
                    string[] casti = line.Split(';');

                    string[] datum = casti[0].Split('.');

                    foreach (var cast in casti.Skip(1))
                    {
                        if (cast != "" && cast != "-")
                        {
                            DayMonth dayMonth = new DayMonth(Convert.ToInt32(datum[0]), Convert.ToInt32(datum[1]));
                            Nameday nameDay = new Nameday(cast, dayMonth);
                            this.Add(nameDay);
                        }
                    }
                }
            }
            sr.Close();
        }
    }

    /// <summary>
    /// Saves the namedays from the NameCalendar to a CSV file.
    /// </summary>
    /// <param name="csvFile"></param>
    public void Save(FileInfo csvFile)
    {
        using (var sw = new StreamWriter(csvFile.FullName))
        {
            foreach (Nameday nameday in _nameDays)
            {
                sw.WriteLine($"{nameday.DayMonth.Day}. {nameday.DayMonth.Month}.; {nameday.Name}");
            }

            sw.WriteLine("fs");
            sw.Close();
        }
    }
}
