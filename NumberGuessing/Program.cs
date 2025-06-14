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
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            Console.Clear();
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
    ShowMessages showMsg = new ShowMessages();
    public void GameEngine()
    {
        int secretNumber = random.Next(0, 101);
        var timer = new TimerControl();
        int attempts = Attempts();
        int input, count = 0;
        bool flag = false;

        while (true)
        {
            timer.Start();
            count++;
            Console.WriteLine("\nEnter your guess:");
            input = ValidateNumber();

            if (input == secretNumber)
            {
                showMsg.ShowWinMessage(count);
                flag = true;
                timer.Stop();
                elapsedList.Add(timer.Elapsed());
                attemptsList.Add(count);
                Console.WriteLine($"Elapsed time:{timer.Elapsed()}");
                timer.Reset();
                break;
            }
            else if (count == attempts)
                break;
            else
                Console.WriteLine(input > secretNumber ? $"Incorrect! The number is less than {input}" :
                    $"Incorrect! The number is greater than {input}");
                
        }
        if (flag == false)
        {
            timer.Stop();
            showMsg.ShowLossMessage(secretNumber);
            timer.Reset();
        }  
    }
    int Attempts()
    {
        showMsg.ShowDificultyLevel();
        int attempts = ValidateNumber();
        while (attempts < 1 || attempts > 3)
        {
            Console.WriteLine("Invalid input. Please enter number between 1 and 3:");
            attempts = ValidateNumber();
        }
        return attempts switch
        {
            1 => 10,
            2 => 5,
            3 => 3,
            _ => 0
        };
    }
    int ValidateNumber()
    {
        int number;
        while (!int.TryParse(Console.ReadLine(), out number))
            Console.WriteLine("You must enter a valid number:");
        return number;
    }
}
class ShowMessages
{
    public void ShowWinMessage(int attempts)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"Congratulations! You guessed the correct number in {attempts} attempts.");
        Console.ForegroundColor = ConsoleColor.White;
    }
    public void ShowDificultyLevel()
    {
        Console.WriteLine("Please select the dificulty level:");
        Console.WriteLine(" 1. Easy (10 chances) \n 2. Medium (5 chances) \n 3. Hard (3 chances) \n");
        Thread.Sleep(500);
        Console.WriteLine("Enter your choice:");
    }
    public void ShowWelcomeMessage()
    {
        Console.WriteLine("Welcome to the Number Guessing Game!");
        Console.WriteLine("I'm thinking of a number between 1 and 100.\n");
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
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"You lose! the number was {secretNumber}.");
        Console.ForegroundColor = ConsoleColor.White;
    }
}
class TimerControl
{
    private Stopwatch timer = new();
    public void Start() =>
        timer.Start();

    public void Stop() =>
        timer.Stop();

    public void Reset() =>
        timer.Reset();

    public TimeSpan Elapsed() =>
        timer.Elapsed;
}