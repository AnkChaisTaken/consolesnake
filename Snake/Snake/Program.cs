using System;
using System.Collections.Generic;
using System.Threading;

namespace Snake
{
    class Program
    {
        const int WAIT_MS = 250;
        static List<SnakePart> snakeParts = new List<SnakePart>();

        static int direction = 1;

        static int appleX;
        static int appleY;

        static void Main(string[] args)
        {
            Console.Title = "Snake!";
            Console.CursorVisible = false;

            Random random = new Random();

            SnakePart tempSP = new SnakePart();
            tempSP.x = random.Next(16);
            tempSP.y = random.Next(32);
            snakeParts.Add(tempSP);

            appleX = random.Next(16);
            appleY = random.Next(32);
            DrawPixel(appleX, appleY, "*");

            Thread t = new Thread(new ThreadStart(ReadKeys));
            t.Start();

            UpdateMethod();
            
        }

        static void ReadKeys()
        {
            while(true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            direction = 0;
                            break;

                        case ConsoleKey.DownArrow:
                            direction = 1;
                            break;

                        case ConsoleKey.RightArrow:
                            direction = 2;
                            break;

                        case ConsoleKey.LeftArrow:
                            direction = 3;
                            break;

                        case ConsoleKey.Enter:
                            SnakePart tempSP = new SnakePart();
                            tempSP.x = snakeParts[snakeParts.Count - 1].x;
                            tempSP.y = snakeParts[snakeParts.Count - 1].x;
                            snakeParts.Add(tempSP);
                            break;
                    }
                }
            }
        }

        static void UpdateMethod()
        {
            bool x = false;
            for(int i = snakeParts.Count - 1; i >= 1; i--)
            {
                if(!x)
                {
                    DrawPixel(snakeParts[i].x, snakeParts[i].y, " ");
                }
                x = true;

                snakeParts[i].x = snakeParts[i - 1].x;
                snakeParts[i].y = snakeParts[i - 1].y;
                DrawPixel(snakeParts[i].x, snakeParts[i].y, "#");
            }

            if (snakeParts.Count == 1)
            {
                DrawPixel(snakeParts[0].x, snakeParts[0].y, " ");
            }

            switch (direction)
            {
                case 0:
                    snakeParts[0].y--;
                    DrawPixel(snakeParts[0].x, snakeParts[0].y, "#");
                    break;

                case 1:
                    snakeParts[0].y++;
                    DrawPixel(snakeParts[0].x, snakeParts[0].y, "#");
                    break;

                case 2:
                    snakeParts[0].x++;
                    DrawPixel(snakeParts[0].x, snakeParts[0].y, "#");
                    break;

                case 3:
                    snakeParts[0].x--;
                    DrawPixel(snakeParts[0].x, snakeParts[0].y, "#");
                    break;

                default:
                    break;
            }

            if(snakeParts[0].x == appleX && snakeParts[0].y == appleY)
            {
                SnakePart tempSP = new SnakePart();
                tempSP.x = snakeParts[snakeParts.Count - 1].x;
                tempSP.y = snakeParts[snakeParts.Count - 1].x;
                snakeParts.Add(tempSP);

                Random random = new Random();
                appleX = random.Next(32);
                appleY = random.Next(32);
                DrawPixel(appleX, appleY, "*");
            }

            Thread.Sleep(WAIT_MS);
            UpdateMethod();
        }

        public static void DrawPixel(int x, int y, string symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }
    }
    
    class SnakePart
    {
        public int x = 0;
        public int y = 0;
    }
}
