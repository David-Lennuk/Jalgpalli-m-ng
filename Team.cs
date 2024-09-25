using System;
using System.Collections.Generic;

namespace Jalgpalli;

public class Team
    {
        public List<Player> Players { get; } = new List<Player>();
        public string Name { get; private set; }
        public Game Game { get; set; } //движение к мячу

    public Team(string name) //название команды
    {
            Name = name;
        }

        public void StartGame(int width, int height) //выдача кахдому игроку пазицию
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

        public void AddPlayer(Player player)  //добавление играков 
    {
            if (player.Team != null) return;
            Players.Add(player);
            player.Team = this;
        }

        public (double, double) GetBallPosition() //позиция мяча 
    {
            return Game.GetBallPositionForTeam(this);
        }

        public void SetBallSpeed(double vx, double vy) //скорость мяча 
    {
            Game.SetBallSpeedForTeam(this, vx, vy);
        }

        public Player GetClosestPlayerToBall()
        {
            Player closestPlayer = Players[0];
            double bestDistance = Double.MaxValue;
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

        public void Move()
        {
            GetClosestPlayerToBall().MoveTowardsBall();
            Players.ForEach(player => player.Move());
        }
    }
