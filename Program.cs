using System;
using System.Collections.Generic;
using System.Numerics;

namespace Jalgpali
{
    class Program
    {
        static void Main(string[] args)
        {
            Stadium stadium = new Stadium(40, 20); // Увеличиваем размер стадиона


            Team homeTeam = new Team("Home");
            Team awayTeam = new Team("Away");


            for (int i = 0; i < 11; i++)// Добавляем игроков в команды
            {
                homeTeam.AddPlayer(new Player($"HomePlayer{i + 1}"));
                awayTeam.AddPlayer(new Player($"AwayPlayer{i + 1}"));
            }


            Game game = new Game(homeTeam, awayTeam, stadium);
            game.Start();


            while (true)// Бесконечный игровой цикл
            {
                game.Move();
                PrintGameState(game);
                System.Threading.Thread.Sleep(500); // Задержка для визуализации


                if (Console.KeyAvailable)// Проверка нажатия клавиши для выхода
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Escape) // Выход по нажатию ESC
                    {
                        break;
                    }
                }
            }
        }

        static void PrintGameState(Game game)
        {
            Console.Clear();

            int width = game.Stadium.Width;
            int height = game.Stadium.Height;
            char[,] field = new char[height, width];


            for (int y = 0; y < height; y++)//создаём поле
            {
                for (int x = 0; x < width; x++)
                {
                    field[y, x] = '.'; //пустота
                }
            }


            foreach (var player in game.HomeTeam.Players)//игроки домашней команды
            {
                int playerX = (int)player.X;
                int playerY = (int)player.Y;
                if (playerX >= 0 && playerX < width && playerY >= 0 && playerY < height)
                {
                    field[playerY, playerX] = 'H';
                }
            }


            foreach (var player in game.AwayTeam.Players)//игроки выездной команды
            {
                int playerX = (int)player.X;
                int playerY = (int)player.Y;
                if (playerX >= 0 && playerX < width && playerY >= 0 && playerY < height)
                {
                    field[playerY, playerX] = 'A';
                }
            }


            int ballX = (int)game.Ball.X;//показываем мяч
            int ballY = (int)game.Ball.Y;
            if (ballX >= 0 && ballX < width && ballY >= 0 && ballY < height)
            {
                field[ballY, ballX] = 'O';
            }

            // Отображаем ворота
            field[height / 2 - 1, 0] = 'G'; //ворота домашней
            field[height / 2, 0] = 'G';
            field[height / 2 + 1, 0] = 'G';
            field[height / 2 - 1, width - 1] = 'G'; //ворота выездной
            field[height / 2, width - 1] = 'G';
            field[height / 2 + 1, width - 1] = 'G';

            // Выводим поле
            Console.WriteLine($"Score: {game.HomeTeam.Name} {game.HomeTeam.Score} - {game.AwayTeam.Score} {game.AwayTeam.Name}\n");
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(field[y, x] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
