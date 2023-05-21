namespace JCNUloha2;

public readonly record struct Nameday
{
    public string Name { get; init; }
    public DayMonth DayMonth { get; init; }

    /// <summary>0
    /// Default constructor for the Nameday struct.
    /// Initializes the Name property to an empty string
    /// and the DayMonth property to a new DayMonth instance. 
    /// </summary>
    public Nameday()
    {
        Name = "";
        DayMonth = new DayMonth();
    }

    /// <summary>
    /// Constructor for the Nameday struct that takes
    /// a name and a dayMonth parameter.
    /// Initializes the Name and DayMonth properties with
    /// the provided values.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="dayMonth"></param>
    public Nameday(string name, DayMonth dayMonth)
    {
        Name = name;
        DayMonth = dayMonth;
    }
}
