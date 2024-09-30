using System;

namespace Jalgpalli
{
    public class Player
    {
        public string Name { get; }
        public double X { get; private set; }
        public double Y { get; private set; }
        public Team? Team { get; set; } = null;

        private const double MaxSpeed = 2; // Уменьшенная скорость
        private const double MaxKickSpeed = 5; // Уменьшенная сила удара
        private const double BallKickDistance = 2; // Ближайшее расстояние для удара
        private Random _random = new Random();

        public Player(string name)
        {
            Name = name;
        }

        public void SetPosition(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double GetDistanceToBall()
        {
            var ballPosition = Team!.Game.Ball;
            var dx = ballPosition.X - X;
            var dy = ballPosition.Y - Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public void Move()
        {
            var ballPosition = Team!.Game.Ball;
            var distanceToBall = GetDistanceToBall();

            if (distanceToBall < BallKickDistance)
            {
                // Удар по мячу
                Team.Game.Ball.SetSpeed(MaxKickSpeed * _random.NextDouble(), MaxKickSpeed * (_random.NextDouble() - 0.5));
            }
            MoveTowardsBall();
        }

        public void MoveTowardsBall()
        {
            var ballPosition = Team!.Game.Ball;
            var dx = ballPosition.X - X;
            var dy = ballPosition.Y - Y;

            if (dx == 0 && dy == 0) return; // Если уже на мяче, ничего не делать

            var ratio = Math.Sqrt(dx * dx + dy * dy) / MaxSpeed;
            if (ratio > 1)
            {
                dx /= ratio;
                dy /= ratio;
            }

            X += dx;
            Y += dy;

            // Проверяем границы поля
            if (!Team.Game.Stadium.IsIn(X, Y))
            {
                X = Math.Max(0, Math.Min(X, Team.Game.Stadium.Width - 1));
                Y = Math.Max(0, Math.Min(Y, Team.Game.Stadium.Height - 1));
            }
        }
    }
}
