using JCNUloha2;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewerConsoleApp;

public class IS
{
    private NameCalendar calendar;

    public IS(string cesta)
    {
        calendar = new NameCalendar();

        if (cesta is null || cesta is "")
            Console.WriteLine("Nespravna cesta");
        else
        {
            FileInfo subor = new FileInfo(cesta);

            if (!subor.Exists)
                Console.WriteLine($"Zadaný súbor {subor.Name} neexistuje!");

            if (subor.Extension.ToLower() != ".csv")
                Console.WriteLine($"Zadaný súbor {subor.Name} nie je typu CSV!");

            if (subor.Exists && subor.Extension.ToLower() == ".csv")
            {
                calendar.Load(subor);
                Console.WriteLine("Súbor kalendára bol načítaný.");
            }
        }

        Console.WriteLine("KALENDÁR MIEN");
        Console.WriteLine($"DNES {DateTime.Now.ToString("d.M.yyyy")} {string.Join(", ", calendar.GetNamedays().Where(n => n.DayMonth.Day == DateTime.Now.Day && n.DayMonth.Month == DateTime.Now.Month).Select(n => n.Name))}");
        Console.WriteLine($"Zajtra má meniny: {string.Join(", ", calendar.GetNamedays().Where(n => n.DayMonth.Day == DateTime.Now.AddDays(1).Day && n.DayMonth.Month == DateTime.Now.AddDays(1).Month).Select(n => n.Name))}");
        Console.WriteLine("");
    }

    public void run()
    {
        bool koniec = false;
        while (!koniec)
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1 - načítať kalendár");
            Console.WriteLine("2 - zobraziť štatistiku");
            Console.WriteLine("3 - vyhľadať mená");
            Console.WriteLine("4 - vyhľadať mená podľa dátumu");
            Console.WriteLine("5 - zobraziť kalendár mien v mesiaci");
            Console.WriteLine("6 | Escape - koniec");
            Console.Write("Vaša voľba: ");
            string? volba = Console.ReadLine();
            int parsed;
            if (int.TryParse(volba, out parsed))
            {
                switch (parsed)
                {
                    case 1:
                        NacitajKalendar();
                        break;
                    case 2:
                        ZobrazStatistiku();
                        break;
                    case 3:
                        VyhladajMena();
                        break;
                    case 4:
                        VyhladajMenaPodlaDatumu();
                        break;
                    case 5:
                        ZobrazKalendarMienVMesiaci();
                        break;
                    case 6:
                        koniec = true;
                        break;
                    default:
                        Console.WriteLine("Nespravna volba");
                        break;
                }
            }
        }
    }

    private void NacitajKalendar()
    {
        Console.Clear();
        //FileInfo subor = new FileInfo("C:\\Users\\micha\\Downloads\\namedays-sk.txt");
        Console.WriteLine("OTVORENIE");
        while (true)
        {
            Console.WriteLine("Zadajte cestu k súboru kalendára mien alebo stlačte Enter pre ukončenie.");
            Console.Write("Zadajte cestu k CSV súboru: ");
            string? cesta = Console.ReadLine();

            if (cesta is null || cesta is "")
                break;

            FileInfo subor = new FileInfo(cesta);
            if (!subor.Exists)
                Console.WriteLine($"Zadaný súbor {subor.Name} neexistuje!");

            if (subor.Extension.ToLower() != ".csv")
                Console.WriteLine($"Zadaný súbor {subor.Name} nie je typu CSV!");

            if (subor.Exists && subor.Extension.ToLower() == ".csv")
            {
                calendar.Load(subor);
                Console.WriteLine("Súbor kalendára bol načítaný.");

                Console.WriteLine("Pre pokračovanie stlačte Enter.");
                string? vstup = Console.ReadLine();
                break;
            }
        }
    }

    private void ZobrazStatistiku()
    {
        Console.Clear();
        string[] mesiace = { "január", "február", "marec", "apríl", "máj", "jún", "júl", "august", "september", "október", "november", "december" };
        Console.WriteLine("ŠTATISTIKA");
        Console.WriteLine($"Celkový počet mien v kalendári: {calendar.NameCount}");
        Console.WriteLine($"Celkový počet dní obsahujúcimená v kalendári: {calendar.DayCount}");
        Console.WriteLine("Celkový počet mien v jednotlivých mesiacoch: ");
        int index = 1;
        foreach (string mesiac in mesiace)
        {
            var list = calendar.GetNamedays();
            var pocet = list.Where(n => n.DayMonth.Month == index).Select(n => n.Name).Count();
            Console.WriteLine($"\t{mesiac}: {pocet}");
            index++;
        }
        Console.WriteLine("Počet mien podľa začiatočných písmen: ");
        var filter = calendar.GetNamedays().GroupBy(n => n.Name.First()).OrderBy(n => n.Key);
        //Console.WriteLine(string.Join(", ", filter.Select(n => n.Count())));
        Console.WriteLine("Počet mien podľa dĺžky: ");
        for (int i = 0; i < 15; i++)
        {
            var pocetLudi = calendar.GetNamedays().Where(n => n.Name.Length == i).Count();
            if (pocetLudi > 2)
                Console.WriteLine($"{i}: {pocetLudi}");
        }
        Console.WriteLine("Po skončení stlašte Enter.");
        string? vstup = Console.ReadLine();
    }

    private void VyhladajMena()
    {
        Console.Clear();
        Console.WriteLine("VYHĽADÁVANIE MIEN");
        Console.WriteLine("Pre ukončenie stlačte Enter.");
        while (true)
        {
            Console.Write("Zadajte meno (regulárny výraz): ");
            string vstup = Console.ReadLine()!;
            if (vstup is null || vstup.Equals(""))
                break;

            var list = calendar.GetNamedays();

            bool prazdne = true;
            int index = 1;
            foreach (var nameDay in list)
            {
                if (nameDay.Name.Contains(vstup))
                {
                    Console.WriteLine($"\t{index}. {nameDay.Name} ({nameDay.DayMonth.Day}. {nameDay.DayMonth.Month}.)");
                    index++;
                    prazdne = false;
                }
            }
            if (prazdne)
                Console.WriteLine("Neboli nájdené žiadné mená.");
            index = 0;
        }
    }

    private void VyhladajMenaPodlaDatumu()
    {
        Console.Clear();
        Console.WriteLine("VYHĽADÁVANIE MIEN PODĽA DÁTUMU");
        Console.WriteLine("Pre ukončenie stlačte Enter.");
        while (true)
        {
            Console.Write("Zadajte deň a mesiac: ");
            string? vstup = Console.ReadLine();
            if (vstup is null || vstup == "")
                break;

            var list = calendar.GetNamedays();

            bool prazdne = true;
            int index = 1;

            string[] vstupy = vstup.Split('.');

            var filtrovanyList = list.Where(n => n.DayMonth.Day == Convert.ToInt32(vstupy[0]) && n.DayMonth.Month == Convert.ToInt32(vstupy[1]));

            foreach (var nameday in filtrovanyList)
            {
                Console.WriteLine($"{index}. {nameday.Name}");
                prazdne = false;
                index++;
            }
            if (prazdne)
                Console.WriteLine("Neboli nájdené žiadné mená.");

            index = 0;
        }
    }

    private void ZobrazKalendarMienVMesiaci()
    {
        Console.Clear();
        Console.WriteLine("KALENDÁR MENÍN");
        Console.WriteLine($"{DateTime.Now.ToString("MMMM yyyy")}: ");
      
        var filteredList = calendar.GetNamedays().Where(n => n.DayMonth.Month == DateTime.Now.Month);
        foreach (var nameDay in filteredList)
        {
            Console.WriteLine($"{nameDay.DayMonth.Day}.{nameDay.DayMonth.Month} {nameDay.Name}");
        }
    }
}
