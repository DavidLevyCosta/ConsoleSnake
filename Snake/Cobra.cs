using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Text;

namespace Snake
{
    public enum Direction
    {
        Up, Down, Left, Right
    }

    class Cobra
    {
        private Controls controls;
        public int tailSize { get; set; }
        public Direction direcao;
        internal int nextFrontX;
        internal int nextFrontY;

        public Vector frontPosition;
        public Vector turnPosition;
        public List<Vector> segments;

        public Cobra ()
        {
            nextFrontX = -1;
            nextFrontY = -1;
            frontPosition = new Vector(-1, -1);
            turnPosition = new Vector(-1, -1);
            controls = new Controls();
            segments = new List<Vector>{new Vector(0, 0)};
            tailSize = 0;
            direcao = Direction.Right;

        }

        internal void Move()
        {

            UpdateTail();   
            switch (direcao)
            {
                case Direction.Right:
                    if (segments[0].X == (Screen.WIDTH - 1)) segments[0].X = 0;
                    else
                    {
                        segments[0].X++;

                        nextFrontX = segments[0].X + 1;
                        nextFrontY = segments[0].Y;
                    }
                    break;
                case Direction.Down:
                    if (segments[0].Y == (Screen.HEIGHT - 1)) segments[0].Y = 0;
                    else
                    {
                        segments[0].Y++;

                        nextFrontX = segments[0].X;
                        nextFrontY = segments[0].Y + 1;
                    }
                    break;
                case Direction.Left:
                    if (segments[0].X == 0) segments[0].X = (Screen.WIDTH - 1);
                    else
                    {
                        segments[0].X--;

                        nextFrontX = segments[0].X - 1;
                        nextFrontY = segments[0].Y;
                    }
                    break;
                case Direction.Up:
                    if (segments[0].Y == 0) segments[0].Y = (Screen.HEIGHT - 1);
                    else
                    {
                        segments[0].Y--;

                        nextFrontX = segments[0].X;
                        nextFrontY = segments[0].Y - 1;
                    }
                    break;
            }
            frontPosition = new Vector(nextFrontX, nextFrontY);
        }

        public void AddTail()
        {
            segments.Add(new Vector(segments[segments.Count - 1].X, segments[segments.Count - 1].Y));
        }
        public void UpdateTail()
        {
            for (int i = tailSize; i > 0; i--)
            {
                segments[i] = new Vector(segments[i - 1].X, segments[i - 1].Y); ;
            }
        }
    }
}
