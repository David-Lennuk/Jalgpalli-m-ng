using System;
using System.Collections.Generic;

namespace Jalgpalli
{
    public class Team
    {
        public List<Player> Players { get; } = new List<Player>();
        public string Name { get; private set; }
        public Game Game { get; set; }
        public int Score { get; private set; } = 0;

        public Team(string name)
        {
            Name = name;
        }

        public void AddScore()
        {
            Score++;
        }

        public void StartGame(int width, int height)
        {
            Random rnd = new Random();
            foreach (var player in Players)
            {
                player.SetPosition(
                    rnd.NextDouble() * width,
                    rnd.NextDouble() * height
                );
            }
        }

        public void AddPlayer(Player player)
        {
            if (player.Team != null) return;
            Players.Add(player);
            player.Team = this;
        }

        public void Move()
        {
            Player closestPlayer = GetClosestPlayerToBall();
            closestPlayer.Move();
        }

        public Player GetClosestPlayerToBall()
        {
            Player closestPlayer = Players[0];
            double bestDistance = double.MaxValue;
            foreach (var player in Players)
            {
                var distance = player.GetDistanceToBall();
                if (distance < bestDistance)
                {
                    closestPlayer = player;
                    bestDistance = distance;
                }
            }
            return closestPlayer;
        }
    }
}
