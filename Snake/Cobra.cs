using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Text;

namespace Snake
{

    class Cobra
    {
        private Controls controls;
        public int tailSize { get; set; }
        public byte direcao;
        internal int nextX;
        internal int nextY;

        public Vector nextHeadPosition;
        public List<Vector> segments;

        public Cobra ()
        {
            nextX = -1;
            nextY = -1;
            nextHeadPosition = new Vector(-1, -1);
            controls = new Controls();
            segments = new List<Vector>{new Vector(0, 0)};
            tailSize = 0;
            direcao = 0;

        }

        internal void Move()
        {

            UpdateTail();   
            switch (direcao)
            {
                case 0:
                    if (segments[0].X == (Screen.WIDTH - 1)) segments[0].X = 0;
                    else
                    {
                        segments[0].X++;
                        nextX = segments[0].X + 1;
                        nextY = segments[0].Y;
                    }
                    break;
                case 1:
                    if (segments[0].Y == (Screen.HEIGHT - 1)) segments[0].Y = 0;
                    else
                    {
                        segments[0].Y++;
                        nextX = segments[0].X;
                        nextY = segments[0].Y + 1;
                    }
                    break;
                case 2:
                    if (segments[0].X == 0) segments[0].X = (Screen.WIDTH - 1);
                    else
                    {
                        segments[0].X--;
                        nextX = segments[0].X - 1;
                        nextY = segments[0].Y;
                    }
                    break;
                case 3:
                    if (segments[0].Y == 0) segments[0].Y = (Screen.HEIGHT - 1);
                    else
                    {
                        segments[0].Y--;
                        nextX = segments[0].X;
                        nextY = segments[0].Y - 1;
                    }
                    break;
            }

            nextHeadPosition = new Vector(nextX, nextY);


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
