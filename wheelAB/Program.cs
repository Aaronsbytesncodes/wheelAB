class Program
{
    static string[] wheelOptions = {
        "$100", "$200", "$300", "$400", "$500",
        "$600", "$700", "$800", "$900",  "Lose a Turn","1000","$100", "$200", "$300", "$400", "$500","Bankrupt","$600", "$700",
    };

    static Random random = new Random();

    static void Main()
    {
        Console.CursorVisible = false;
        Console.WriteLine("🎡 Welcome to the Wheel!");
        Console.WriteLine("Press [Enter] to spin...");
        Console.ReadLine();

        SpinWheel();
    }

    static void SpinWheel()
    {
        int wheelSize = wheelOptions.Length;
        int spins = random.Next(30, 60);
        int current = random.Next(wheelSize);
        int slowdownDelay = 30;

        for (int i = 0; i < spins; i++)
        {
            Console.Clear();
            DrawWheel(current);
            current = (current + 1) % wheelSize;

            // Play a clicking sound
            Console.Beep(300, 50); // Frequency: 300 Hz, Duration: 50 ms

            Thread.Sleep(slowdownDelay);

            // Slow down after halfway
            if (i > spins / 2)
                slowdownDelay += 5;
            if (i > spins * 3 / 4)
                slowdownDelay += 10;
        }

        Console.Clear();
        int resultIndex = (current + wheelSize - 1) % wheelSize;
        string result = wheelOptions[resultIndex];
        DrawWheel(resultIndex, result);

        // Center the "wheel stopped" and result message
        int centerX = Console.WindowWidth / 2;
        int centerY = Console.WindowHeight / 2;

        string stoppedMessage = " The wheel stopped!";
        string resultMessage = $" Result: {result}";
        string exitMessage = "Press any key to exit...";

        Console.ForegroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(centerX - stoppedMessage.Length / 2, centerY + 1);
        Console.WriteLine(stoppedMessage);

        Console.SetCursorPosition(centerX - resultMessage.Length / 2, centerY + 2);
        Console.WriteLine(resultMessage);

        Console.SetCursorPosition(centerX - exitMessage.Length / 2, centerY + 4);
        Console.WriteLine(exitMessage);

        Console.ReadKey();
    }
    // wheel
    static void DrawWheel(int highlightIndex, string result = null)
    {
        int centerX = Console.WindowWidth / 2;
        int centerY = Console.WindowHeight / 2;
        double radiusX = (Console.WindowWidth - 10) / 2.0;
        double radiusY = (Console.WindowHeight - 5) / 2.0;

        for (int i = 0; i < wheelOptions.Length; i++)
        {
            double angle = (2 * Math.PI / wheelOptions.Length) * i;
            int x = centerX + (int)(radiusX * Math.Cos(angle));
            int y = centerY + (int)(radiusY * Math.Sin(angle));

            if (x < 0 || y < 0 || x >= Console.WindowWidth || y >= Console.WindowHeight)
                continue; // keeps the wheel inbounds of window

            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Green;
            if (i == highlightIndex)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"[{wheelOptions[i]}]");
            }
            else
            {
                Console.Write(wheelOptions[i]);
            }
        }

        // Displays "WHEEL OF FORTUNE" in the center during the spin
        if (string.IsNullOrEmpty(result))
        {
            string boldText = "WHEEL OF FORTUNE";
            Console.SetCursorPosition(centerX - boldText.Length / 2, centerY);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(boldText);
        }
        // Displays the result of the wheel spin in the center
        else
        {
            Console.SetCursorPosition(centerX - result.Length / 2, centerY);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(result);
        }
        Console.ResetColor();
    }
}