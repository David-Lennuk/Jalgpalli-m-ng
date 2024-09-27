using System;

namespace Jalgpalli;

public class Program
{
    public static void Main(string[] args)
    {
        // Create stadium with boundaries
        Stadium stadium = new Stadium(100, 50);

        // Create teams
        Team homeTeam = new Team("Team A");
        Team awayTeam = new Team("Team B");

        // Add players to teams
        for (int i = 0; i < 5; i++)
        {
            homeTeam.AddPlayer(new Player($"Player A{i + 1}"));
            awayTeam.AddPlayer(new Player($"Player B{i + 1}"));
        }

        // Create the game
        Game game = new Game(homeTeam, awayTeam, stadium);
        game.Start();

        // Game loop simulation
        for (int tick = 0; tick < 100; tick++) // Simulate 100 ticks
        {
            game.Move();
            PrintGameState(game);
            System.Threading.Thread.Sleep(500); // Wait for half a second for visibility
        }
    }

    private static void PrintGameState(Game game)
    {
        Console.Clear();
        Console.WriteLine("Game State:");
        Console.WriteLine("Stadium:");
        for (int y = 0; y < game.Stadium.Height; y++)
        {
            for (int x = 0; x < game.Stadium.Width; x++)
            {
                // Check if the ball is at this position
                if (Math.Abs(game.Ball.X - x) < 0.5 && Math.Abs(game.Ball.Y - y) < 0.5)
                {
                    Console.Write(" O "); // Ball
                }
                else
                {
                    // Check for players from Team A
                    Player playerA = game.HomeTeam.Players.Find(p => Math.Abs(p.X - x) < 0.5 && Math.Abs(p.Y - y) < 0.5);
                    if (playerA != null)
                    {
                        Console.Write(" A "); // Team A
                    }
                    else
                    {
                        // Check for players from Team B
                        Player playerB = game.AwayTeam.Players.Find(p => Math.Abs(p.X - x) < 0.5 && Math.Abs(p.Y - y) < 0.5);
                        if (playerB != null)
                        {
                            Console.Write(" B "); // Team B
                        }
                        else
                        {
                            Console.Write("   "); // Empty space
                        }
                    }
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
