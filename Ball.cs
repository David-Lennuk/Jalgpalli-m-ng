using System;

namespace Jalgpalli
{
    public class Ball
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        private double _vx, _vy;
        private Game _game;

        public Ball(double x, double y, Game game)
        {
            _game = game;
            X = x;
            Y = y;
        }

        public void SetSpeed(double vx, double vy)
        {
            _vx = vx;
            _vy = vy;
        }

        public void Move()
        {
            double newX = X + _vx;
            double newY = Y + _vy;

            // Проверяем столкновение с границами
            if (newX < 0 || newX >= _game.Stadium.Width)
            {
                newX = _game.Stadium.Width / 2; // Сбрасываем на центр
                newY = _game.Stadium.Height / 2; // Сбрасываем на центр
                _vx = _vy = 0; // Останавливаем мяч
            }

            // Обновляем положение мяча
            X = newX;
            Y = newY;
        }
    }
}
