namespace labb3;

class Program
{
    static void Main(string[] args)
    {
       
        Console.WriteLine("*********************************************************");
        Console.WriteLine("************ Temperatur i maj månad *********************");
        Console.WriteLine();

        List<int> availableChoices = Enumerable.Range(1, 10).ToList();
        bool continueProgram = true;

        while (continueProgram)
        {
            UserAlternatives.DisplayChoices(availableChoices);
            Console.Write("Välj ett alternativ:");

            if (int.TryParse(Console.ReadLine(), out int choice) && availableChoices.Contains(choice))
            {
                UserAlternatives.HandleChoice(choice);
                // Om användaren väljer 10 då avslutas programmet
                if (choice == 10)
                {
                    Console.WriteLine("Programmet avslutas, hejdå!");
                    break;  
                }
     
                // Här vi tar bort det alternativ har användaren redan valt
                availableChoices.Remove(choice);
                if (availableChoices.Count > 0)
                {
                    // Om användaren vill inte fortsätt och det finns alternativ kvar då avslutas också program
                    Console.Write("Vill du fortsätta? Tryck y för ja eller valfritt knapp för att avsluta: ");
                    string userResponse = Console.ReadLine()?.Trim().ToLower();

                    if (userResponse != "y" || choice == 10)
                    {
                        Console.WriteLine("Programmet avslutas, hejdå!");
                        continueProgram = false;
                    }
                }
                else
                {
                    Console.WriteLine("Inga fler val finns tillgängliga. Programmet avslutas, hejdå!");
                    continueProgram = false;
                }
            }
            else
            {
                Console.WriteLine("Ogiltigt val. Ange ett nummer från listan.");
            }

            Console.WriteLine(); // lägger tom rad här för läsbarheten
        }
    }
}