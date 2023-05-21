namespace JCNUloha2;

public readonly record struct DayMonth
{
    public int Day { get; init; }
    public int Month { get; init; }

    /// <summary>
    /// Default constructor for the DayMonth struct.
    /// Initializes the Day property to 1 and the Month property to 1.
    /// </summary>
    public DayMonth()
    {
        Day = 1;
        Month = 1;
    }

    /// <summary>
    /// Constructor for the DayMonth struct that takes
    /// a day and month parameter.
    /// Initializes the Day and Month properties with
    /// the provided values.
    /// </summary>
    /// <param name="day"></param>
    /// <param name="month"></param>
    public DayMonth(int day, int month)
    {
        Day = day;
        Month = month;
    }

    /// <summary>
    /// Converts the DayMonth object to a DateTime object,
    /// using the current year and the Day and Month properties.
    /// </summary>
    /// <returns></returns>
    public DateTime ToDateTime()
        => new DateTime(DateTime.Now.Year, Month, Day);
}
