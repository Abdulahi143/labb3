namespace labb3;

public class TemperatureCalculator
{

    private readonly List<TemperatureData> _temperatureData;

    public TemperatureCalculator(List<TemperatureData> temperatureData)
    {
        _temperatureData = temperatureData;
    }
    
    public double CalculateAverageTemperature()
    {
        if (_temperatureData == null || _temperatureData.Count == 0) 
            throw new InvalidOperationException("Inga temperatur data hittades för att beräkna medeltemperaturen!");

        return _temperatureData.Average(t => t.Temperature);
    }
    
    public double CalculateMiddleTemperature()
    {
        if (_temperatureData == null || _temperatureData.Count == 0)
            throw new InvalidOperationException("Inga temperatur data hittades för att beräkna medianen!");


        /*Eftersom listan är nollindexerad så måste värdet måste ligga på 15 som är 16:e dagen*/
        /*Och innan måste man sortera i storleksordning dvs minsta till största och används lambda expression för att jämföra(t1, t2) => t1.Temperature.CompareTo(t2.Temperature)*/
        _temperatureData.Sort((t1, t2) => t1.Temperature.CompareTo(t2.Temperature));
        int middleIndex = 15;
        return _temperatureData[middleIndex].Temperature;
    }

    public string CalculateMaxTemperature()
    {
        if (_temperatureData == null || _temperatureData.Count == 0)
            throw new InvalidOperationException("Inga temperatur data hittades för att beräkna högsta temperaturen!");

        double maxTemp = _temperatureData.Max(t => t.Temperature);
        var maxTempData = _temperatureData.First(t => t.Temperature == maxTemp);
        var weekDays = maxTempData.weekDay; 
        string day = maxTempData.Day.ToString();
        return $"Varmaste dagen i maj är {weekDays} den {day}:e med temperaturen {maxTemp:F1}°C.";
    }
    
    public string CalculateMinTemperature()
    {
        if (_temperatureData == null || _temperatureData.Count == 0)
            throw new InvalidOperationException("Inga temperatur data hittades för att beräkna lägsta temperaturen!");

        double minTemp = _temperatureData.Min(t => t.Temperature);
        var minTempData = _temperatureData.First(t => t.Temperature == minTemp);
        var weekDays = minTempData.weekDay; 
        string day = minTempData.Day.ToString();
        return $"Kallaste dagen i maj är {weekDays} den {day} med temperaturen {minTemp:F1}°C.";
    }

    public void SortTemperatures()
    {
        if (_temperatureData == null || _temperatureData.Count == 0)
            throw new InvalidOperationException("Inga temperatur data hittades för att sortera de!");

        var sortedTemperatures = _temperatureData.OrderBy(t => t.Temperature).ToList();
        foreach (var tempData in sortedTemperatures)
        {
            Console.Write("\t"); // Print a tab for indentation
            Console.WriteLine($"Day {tempData.Day}: {tempData.Temperature:F1}°C");
        }
    }


    public List<string[]> AdayInMay(int day)
    {
        // Om användaren skriver t.ex 25 maj då vill jag se hur temperaturen var dagen innan och dagen efter dvs 24,25,26 maj
        if (_temperatureData == null || _temperatureData.Count == 0)
            throw new InvalidOperationException("Inga temperatur data hittades för den dagen du angav!");
        if (day < 1 || day > 31)
            throw new ArgumentOutOfRangeException(nameof(day), "Dagen måste vara mellan 1 och 31!");

        List<string[]> result = new ();

        var inputtedDay = _temperatureData.FirstOrDefault(t => t.Day == day);
        if (inputtedDay != null)
        {
            result.Add(new []{$"Temperaturen i {inputtedDay.Day} maj, var {inputtedDay.Temperature:F1}°C" });
        }
        
        var previousDay = _temperatureData.FirstOrDefault(t => t.Day == day - 1);
        if (inputtedDay != null)
        {
            result.Add(new []{$"Temperaturen i {previousDay.Day} maj, var {previousDay.Temperature:F1}°C" });
        }

        if (day < 31)
        {
            var nextDay = _temperatureData.FirstOrDefault(t => t.Day == day + 1);
            if (inputtedDay != null)
            {
                result.Add(new []{$"Temperaturen i {nextDay.Day} maj, var {nextDay.Temperature:F1}°C" });
            }
        }

        return result;
    }

    public void RepeatedTempsOn3Days()
    {
        //Jag vill loopa igenom alla genererade temperaturer och se vilka temperaturer som kommer minst 3 ggr
        if (_temperatureData == null || _temperatureData.Count == 0)
            throw new InvalidOperationException("Inga temperatur data hittades för att sortera de!");

        Dictionary<double, int> tempCounts = new();

        foreach (var tempData in _temperatureData)
        {
            double roundedTemp = Math.Round(tempData.Temperature, 1);
            if (tempCounts.ContainsKey(roundedTemp))
            {
                tempCounts[roundedTemp]++;
            }
            else
            {
                tempCounts[roundedTemp] = 1;
            }
            
        }
        
        var repeatedTemps = tempCounts.Where(repeats => repeats.Value >= 3);
        if (!repeatedTemps.Any())
        {
            Console.WriteLine("Inga temperaturer förekommer minst 3 gånger.");
        }
        else
        {
            Console.WriteLine("De dag som hade samma temperatur var:");
            foreach (var temp in repeatedTemps)
            {
                Console.WriteLine($"\t Temperatur: {temp.Key:F1}°C, Förekomster: {temp.Value}");
            }
        }
    }

    public void HigerTemperatureThan(double threshold)
    {
        /*Här sorteras alla temperatur som överstiger 18 grader celcius och visar vilka de är*/
        if (_temperatureData == null || _temperatureData.Count == 0)
            throw new InvalidOperationException("Inga temperaturdata hittades för att sortera de!");

        var higherTemps = _temperatureData.Where(t => t.Temperature > threshold).ToList();

        if (!higherTemps.Any())
        {
            Console.WriteLine($"Inga temperaturer överstiger {threshold:F1}°C");
        }
        else
        {
            Console.WriteLine($"Temperaturer som överstiger {threshold:F1}°C är:");
            foreach (TemperatureData tempData in higherTemps)
            {
                Console.WriteLine($"\tDag {tempData.Day}: {tempData.Temperature:F1}°C");
            }
        }
    }
}