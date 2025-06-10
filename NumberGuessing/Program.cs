using System.Threading.Channels;
using System.Transactions;
using System.Xml.Serialization;

class Program
{
    static int number() =>
        new Random().Next(1, 100);
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Number Guessing Game!");
        //Thread.Sleep(1500); //Espera un segundo y medio.
        Console.WriteLine("I'm thinking of a number between 1 and 100.");
        //Thread.Sleep(1500); //Espera un segundo y medio.
        Console.WriteLine("You have 5 chances to guess the correct number.\n");

        var num = number();
        var choi = choice();
        int input = 0, count = 0;

        if (choi == 0)
            return;

        while (count < choi)
        {
            count++;
            Console.WriteLine("Enter your guess:");
            if (!int.TryParse(Console.ReadLine(), out input) && input > 0)
            {
                Console.WriteLine("You must enter a valid number.");
                return;
            }
            else
            {
                if (input == num)
                {
                    Console.WriteLine($"Congratulations! You guessed the correct number in {count} attempts.");
                    return;
                }
                Console.WriteLine(input > num ? "Incorrect! The number is less than {0}" :
                    "Incorrect! The number is greater than {0}", input);
            }
        }
        Console.WriteLine($"Game over! the was number is {num}.");
    }
    static int choice()
    {
        Console.WriteLine("Please select the dificulty level:");
        Console.WriteLine(" 1. Easy (10 chances) \n 2. Medium (5 chances) \n 3. Hard (3 chances) \n");

        Console.WriteLine("Enter your choice:");
        if (!int.TryParse(Console.ReadLine(), out int choice))
        {
            Console.WriteLine("Tiene que ingresar un numero entre 1 y 3.");
            return 0;
        }
        else
        {
            return choice switch
            {
                1 => 10,
                2 => 5,
                3 => 3,
                _ => 0
            };
        }
    }
    
}