using System.Diagnostics;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Channels;
using System.Timers;
using System.Transactions;
using System.Xml.Serialization;

class Program
{
    static Random random = new();
    static Stopwatch timer = new();
    static void Main(string[] args)
    {
        WelcomeMessage(); //Show Welcome Messages.
        List<int> attemptsList = new List<int>();
        List<TimeSpan> timeList = [];

        while (true)
        {
            int input, count = 0;
            var secretNumber = random.Next(1, 101);
            var maxAttemps = MaxAttempts();
            bool flag = false;

            while (count < maxAttemps)
            {
                timer.Start();
                count++;
                Console.WriteLine("\nEnter your guess:");
                while (!int.TryParse(Console.ReadLine(), out input) || input <= 0)
                {
                    Console.WriteLine("You must enter a valid number.");
                }
                    if(input == secretNumber)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"Congratulations! You guessed the correct number in {count} attempts.");
                        Console.ForegroundColor = ConsoleColor.White;

                        attemptsList.Add(count);
                        flag = true; 
                        
                        timer.Stop(); 
                        timeList.Add(timer.Elapsed); 
                        Console.WriteLine($"Tiempo transcurrido:{timer.Elapsed}"); 
                        timer.Reset();
                        break; 
                    }
                    else
                    {
                        Console.WriteLine(input > secretNumber ? "Incorrect! The number is less than {0}" :
                        "Incorrect! The number is greater than {0}", input);
                    }
            }
            ShowLossMessage(flag, secretNumber); 
            if (Response() == 2)
                break;
        }
        if (attemptsList.Any() && timeList.Any())
            ShowBests(attemptsList,timeList);
    }
    static void WelcomeMessage()
    {
        Console.WriteLine("Welcome to the Number Guessing Game!");
        Thread.Sleep(1500); 
        Console.WriteLine("I'm thinking of a number between 1 and 100.");
        Thread.Sleep(1500); 
        Console.WriteLine("You have 5 chances to guess the correct number.\n");
    }
    static void ShowBests(List<int> attemptsList, List<TimeSpan> timeList)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Best attemp: {attemptsList.Min()}");
        Console.WriteLine($"Best Time Elapsed: {timeList.Min()}");
        Console.ForegroundColor = ConsoleColor.White;
    }
    static void ShowLossMessage (bool flag, int secretNumber) 
    {
        if(flag != true)
        {
            timer.Stop();

            Console.WriteLine($"Elapsed time:{timer.Elapsed}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Game over! the number was {secretNumber}.");
            Console.ForegroundColor = ConsoleColor.White;

            timer.Reset();
        }
    }
    static int Response()
    {
        int response;
        Console.WriteLine("1. Continue the game. \n2. Exit.");
        while (!int.TryParse(Console.ReadLine(), out response) || response < 1 || response > 2)
        {
            Console.WriteLine("Invalid input. Please enter 1 or 2:");
        }
        return response;
    }
    static int MaxAttempts()
    {
        int choice;
        Console.WriteLine("Please select the dificulty level:");
        Console.WriteLine(" 1. Easy (10 chances) \n 2. Medium (5 chances) \n 3. Hard (3 chances) \n");
        Thread.Sleep(1500);

        Console.WriteLine("Enter your choice:");
        while (!int.TryParse(Console.ReadLine(), out choice) || choice <1 || choice >3)
        {
            Console.WriteLine("Invalid input. Please enter number between 1 and 3:");
        }
        return choice switch
        {
            1 => 10,
            2 => 5,
            3 => 3,
            _ => 0
        };
    }
}