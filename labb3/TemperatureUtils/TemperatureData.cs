namespace labb3;

public class TemperatureData
{
    public int Day { get; private set; }
    private string Month { get; } = "May";
    public DayOfWeekSwedish weekDay { get; private set; }
    public double Temperature { get; private set; }

    /*Här har vi en konstruktör som gör möjligt att data kan accessas utifån klassen*/
    public TemperatureData(int day, DayOfWeekSwedish weekDay, double temp)
    {
        this.Day = day;
        this.weekDay = weekDay;
        this.Temperature = temp;
    }
        
    public override string ToString()
    {
        return $"Dag: {Day}, {weekDay}, Temperaturen: {Temperature:F1}°C";
    }
    /*Den här metoden/funktionen är här för att generera random temperatur från 0 till 28 grader som är det vanligast temperatur för maj månad
     enligt https://sv.climate-data.org/europa/sverige-105/c/maj-5/
     som hänger ihop med datumet dvs 31 dagars temperatur*/
    public static TemperatureData[] GenerateMayTemperatures()
    {
        Random random = new Random();
        List<TemperatureData> temperatures = new();

        for (int day = 1; day <= 31; day++)
        {
            double temp = random.NextDouble() * 28;
            DateOnly date = new DateOnly(2024, 5, day); // Create DateOnly instance
            DayOfWeek dayOfWeek = date.DayOfWeek;// Get the DayOfWeek for the date
            DayOfWeekSwedish dayOfWeekSwedish = MapToSwedishDayOfWeek(dayOfWeek); // Map to Swedish DayOfWeek

            temperatures.Add(new TemperatureData(day, dayOfWeekSwedish, temp)); // Pass explicitly
        }

        return temperatures.ToArray();
    }


        
        /*Den här metoden/funktionen är här för att loopa genom alla random genereade temperatur data och visa en efter en*/
        public static void PrintTemperatures(List<TemperatureData> temperatures)
        {
            foreach (var tempData in temperatures)
            {
                Console.WriteLine(tempData);
            }
        }

        private static DayOfWeekSwedish MapToSwedishDayOfWeek(DayOfWeek dayOfWeek)
        {
            return dayOfWeek switch
            {
                DayOfWeek.Monday => DayOfWeekSwedish.Måndag,
                DayOfWeek.Tuesday => DayOfWeekSwedish.Tisdag,
                DayOfWeek.Wednesday => DayOfWeekSwedish.Onsdag,
                DayOfWeek.Thursday => DayOfWeekSwedish.Torsdag,
                DayOfWeek.Friday => DayOfWeekSwedish.Fredag,
                DayOfWeek.Saturday => DayOfWeekSwedish.Lördag,
                DayOfWeek.Sunday => DayOfWeekSwedish.Söndag,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
}