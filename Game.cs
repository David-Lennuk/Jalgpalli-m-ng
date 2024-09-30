namespace Jalgpalli
{
    public class Game
    {
        public Team HomeTeam { get; }
        public Team AwayTeam { get; }
        public Stadium Stadium { get; }
        public Ball Ball { get; private set; }

        public Game(Team homeTeam, Team awayTeam, Stadium stadium)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            Stadium = stadium;
        }

        public void Start()
        {
            Ball = new Ball(Stadium.Width / 2, Stadium.Height / 2, this);
            HomeTeam.StartGame(Stadium.Width, Stadium.Height);
            AwayTeam.StartGame(Stadium.Width, Stadium.Height);
        }

        private void CheckForGoals()
        {
            if (Ball.X < 0) // Левые ворота
            {
                AwayTeam.AddScore();
                ResetBall();
            }
            else if (Ball.X >= Stadium.Width) // Правые ворота
            {
                HomeTeam.AddScore();
                ResetBall();
            }
        }

        private void ResetBall()
        {
            Ball = new Ball(Stadium.Width / 2, Stadium.Height / 2, this);
        }

        public void Move()
        {
            HomeTeam.Move();
            AwayTeam.Move();
            Ball.Move();
            CheckForGoals();
        }
    }
}
