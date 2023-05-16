using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCNUloha2;

public readonly record struct Nameday
{
    public string Name { get; init; }
    public DayMonth DayMonth { get; init; }

    public Nameday()
    {
        Name = "";
        DayMonth = new DayMonth();
    }

    public Nameday(string name, DayMonth dayMonth)
    {
        Name = name;
        DayMonth = dayMonth;
    }
}
