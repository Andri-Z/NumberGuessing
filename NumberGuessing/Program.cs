class Program
{
    public void adivinar()
    {
        var num = numero();
        int entrada = 0;
        do
        {
            if (int.TryParse(Console.ReadLine(), out entrada))
            {

            }
            else
            {

            }
        }
        while (num != entrada);
    }
    int numero()
    {
        var num = new Random().Next(1, 100);
        return num;
    }
}