using System;
using System.Threading;

namespace Jalgpalli
{
    class Program
    {
        static void Main(string[] args)
        {
            var stadium = new Stadium(30, 15);
            var homeTeam = new Team("Home Team");
            var awayTeam = new Team("Away Team");

            for (int i = 0; i < 5; i++)
            {
                homeTeam.AddPlayer(new Player($"H{i + 1}"));
                awayTeam.AddPlayer(new Player($"A{i + 1}"));
            }

            var game = new Game(homeTeam, awayTeam, stadium);
            game.Start();

            Console.WriteLine("Игра началась! Нажмите любую клавишу для остановки.");

            while (!Console.KeyAvailable)
            {
                Console.Clear();
                game.Move();
                DrawGame(stadium, game);
                DrawScore(homeTeam, awayTeam);
                Thread.Sleep(500);
            }

            Console.ReadKey();
            Console.WriteLine("Игра остановлена.");
        }

        static void DrawGame(Stadium stadium, Game game)
        {
            for (int y = 0; y < stadium.Height; y++)
            {
                for (int x = 0; x < stadium.Width; x++)
                {
                    bool playerDrawn = false;

                    foreach (var player in game.HomeTeam.Players)
                    {
                        if ((int)player.X == x && (int)player.Y == y)
                        {
                            Console.Write("H "); // H для хозяев
                            playerDrawn = true;
                        }
                    }

                    foreach (var player in game.AwayTeam.Players)
                    {
                        if ((int)player.X == x && (int)player.Y == y)
                        {
                            Console.Write("A "); // A для гостей
                            playerDrawn = true;
                        }
                    }

                    if (!playerDrawn)
                    {
                        if ((int)game.Ball.X == x && (int)game.Ball.Y == y)
                        {
                            Console.Write("O "); // O для мяча
                        }
                        else
                        {
                            Console.Write(". "); // Пустое пространство
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        static void DrawScore(Team homeTeam, Team awayTeam)
        {
            Console.WriteLine($"Счет: {homeTeam.Name} {homeTeam.Score} - {awayTeam.Score} {awayTeam.Name}");
        }
    }
}
