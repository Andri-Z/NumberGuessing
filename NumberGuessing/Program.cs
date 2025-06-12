using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Channels;
using System.Timers;
using System.Transactions;
using System.Xml.Serialization;

class Program
{
    static void Main(string[] args)
    {
        ShowMessages msg = new();
        Game game = new();

        msg.ShowWelcomeMessage();
        while (true)
        {
            game.GameEngine();

            if (Response() == 2)
                break;
        }
        if (game.attemptsList.Any() && game.elapsedList.Any())
            msg.ShowBests(game.attemptsList,game.elapsedList);
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
}
class Game
{
    private Random random = new();
    public List<int> attemptsList = new();
    public List<TimeSpan> elapsedList = new();
    public void GameEngine()
    {
        int secretNumber = random.Next(1, 101);
        var showMsg = new ShowMessages();
        var timer = new TimerControl();
        int maxAttemps = MaxAttempts();
        int input, count = 0;
        bool flag = false;

        while (count < maxAttemps)
        {
            timer.Start();
            count++;
            Console.WriteLine("\nEnter your guess:");
            while (!int.TryParse(Console.ReadLine(), out input) || input <= 0)
                Console.WriteLine("You must enter a valid number.");

            if (input == secretNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Congratulations! You guessed the correct number in {count} attempts.");
                Console.ForegroundColor = ConsoleColor.White;

                flag = true;
                timer.Stop();
                elapsedList.Add(timer.Elapsed());
                attemptsList.Add(count);
                Console.WriteLine($"Elapsed time:{timer.Elapsed()}");
                timer.Reset();
                break;
            }
            else
                Console.WriteLine(input > secretNumber ? "Incorrect! The number is less than {0}" :
                    "Incorrect! The number is greater than {0}", input);
        }
        if (flag == false)
            showMsg.ShowLossMessage(secretNumber);
    }
    int MaxAttempts()
    {
        int choice;
        Console.WriteLine("Please select the dificulty level:");
        Console.WriteLine(" 1. Easy (10 chances) \n 2. Medium (5 chances) \n 3. Hard (3 chances) \n");
        Thread.Sleep(1500);

        Console.WriteLine("Enter your choice:");
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
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
class ShowMessages
{
    public void ShowWelcomeMessage()
    {
        Console.WriteLine("Welcome to the Number Guessing Game!");
        Thread.Sleep(1500);
        Console.WriteLine("I'm thinking of a number between 1 and 100.");
        Thread.Sleep(1500);
        Console.WriteLine("You have 5 chances to guess the correct number.\n");
    }
    public void ShowBests(List<int> attemptsList, List<TimeSpan> timeList)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Best attemp: {attemptsList.Min()}");
        Console.WriteLine($"Best Time Elapsed: {timeList.Min()}");
        Console.ForegroundColor = ConsoleColor.White;
    }
    public void ShowLossMessage(int secretNumber)
    {
        var timer = new TimerControl();
        timer.Stop();
        
        Console.WriteLine($"Elapsed time:{timer.Elapsed}");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Game over! the number was {secretNumber}.");
        Console.ForegroundColor = ConsoleColor.White;

        timer.Reset();
    }
}
class TimerControl()
{
    Stopwatch timer = new();
    public void Start() =>
        timer.Start();

    public void Stop() =>
        timer.Stop();

    public void Reset() =>
        timer.Reset();

    public TimeSpan Elapsed() =>
        timer.Elapsed;
}