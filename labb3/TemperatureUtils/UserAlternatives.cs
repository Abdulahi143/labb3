namespace labb3;

public static class UserAlternatives
{
    private static List<TemperatureData> mayTemperatures;  // Keep this as List<TemperatureData>
    private static TemperatureCalculator calculator;

    private static readonly List<string> Choices = new List<string>
    {
        "1. Visa temperaturer för maj månad.",
        "2. Visa medeltemperaturen.",
        "3. Visa mediantemperaturen.",
        "4. Visa den högsta temperaturen.",
        "5. Visa den lägsta temperaturen.",
        "6. Sortera temperaturerna i stigande ordning.",
        "7. Visa de dagar som är varmare än 18°C.",
        "8. Visa temperaturen för en dag samt dagen innan och dagen efter.",
        "9. Visa de mest förekommande temperaturerna i maj.",
        "10. Avsluta programmet!"
        
    };

    public static void DisplayChoices(List<int> availableChoices)
    {
        
        foreach (int choice in availableChoices)
        {
            if (choice >= 1 && choice <= Choices.Count)
            {
                Console.WriteLine(Choices[choice - 1]);
            }
        }
    }

    public static void HandleChoice(int choice)
    {
        
        //Här visas bara om det finns data i listan det vill säga om det är redan genererad maj temperatur
        if (mayTemperatures == null)
        {
            var temperaturesArray = TemperatureData.GenerateMayTemperatures().ToArray(); 
            mayTemperatures = temperaturesArray.ToList();  
            calculator = new TemperatureCalculator(mayTemperatures);
        }
        
        switch (choice)
        {
            case 1:
                TemperatureData.PrintTemperatures(mayTemperatures);
                break;
            case 2:
                Console.WriteLine($"Medeltemperaturen är: {calculator.CalculateAverageTemperature():F1}°C");
                break;
            case 3:
                Console.WriteLine($"Mediantemperaturen är: {calculator.CalculateMiddleTemperature():F1}°C");
                break;
            case 4:
                Console.WriteLine(calculator.CalculateMaxTemperature());
                break;
            case 5:
                Console.WriteLine(calculator.CalculateMinTemperature());
                break;
            case 6:
                Console.WriteLine("Sorterad från lägsta temperatur till högsta: ");
                calculator.SortTemperatures();
                break;
            case 7:
                calculator.HigerTemperatureThan(18);
                break;
            case 8:
                Console.WriteLine("Vill du leta efter en dag i maj temperatur?");
                Console.Write("Skriv bara dagen siffra ex, 3: ");
                int inputtedDay = Int32.Parse(Console.ReadLine());

                var threeDaysTemp = calculator.AdayInMay(inputtedDay);

                foreach (var day in threeDaysTemp)
                {
                    Console.WriteLine(day[0]);
                }

                break;
            case 9:
                calculator.RepeatedTempsOn3Days();
                break;
            case 10:
                break;
            default:
                Console.WriteLine("Ogiltigt val.");
                break;
        }
    }
}
