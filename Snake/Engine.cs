using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Snake
{

    class Engine
    {
        Thread gameThread;
        Thread inputThread;
        Cobra cobra;
        Screen screen;
        Controls controls;
        Random rand;

        bool turnColided;

        private Vector fruta;

        byte desiredFps;

        public Engine()
        {
            turnColided = false;
            cobra = new Cobra();
            screen = new Screen();
            controls = new Controls();
            rand = new Random();
            fruta = new Vector(0, 0);
            fruta.X = rand.Next(4, (Screen.FIELD_WIDTH - 1));
            fruta.Y = rand.Next(4, (Screen.FIELD_HEIGHT - 1));

            desiredFps = 15;
            gameThread = new Thread(onFrame);
            gameThread.IsBackground = false;
            inputThread = new Thread(InputRead);
            inputThread.IsBackground = true;
        }

        public void Start()
        {
            gameThread.Start();
            inputThread.Start();
        }

        internal void onFrame()
        {
            while (true)
            {
                Update();
                Draw();

                Thread.Sleep(1000 / desiredFps);
            }
        }

        public void Update()
        {
            if (!Colided() && !turnColided)
            {
                ChangeDirection();
                cobra.Move();
                UpdateField();
            }
            else
            {
                Console.WriteLine("Game Over!");
            }
        }

        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
            screen.DrawCanva();
        }

        // Movment

        internal void ChangeDirection()
        {
            Key nextKeyInQueue;
            if (controls.keyQueue.TryDequeue(out nextKeyInQueue))
            {
                if (nextKeyInQueue == Key.RightArrow && cobra.direcao != Direction.Left)
                {
                    if (cobra.direcao == Direction.Down || cobra.direcao == Direction.Up)
                    {
                        TurnColided(cobra.segments[0].X + 1, cobra.segments[0].Y);
                    }
                    cobra.direcao = Direction.Right;
                }
                if (nextKeyInQueue == Key.DownArrow && cobra.direcao != Direction.Up)
                {
                    if (cobra.direcao == Direction.Right || cobra.direcao == Direction.Left)
                    {
                        TurnColided(cobra.segments[0].X, cobra.segments[0].Y + 1);
                    }
                    cobra.direcao = Direction.Down;
                }
                if (nextKeyInQueue == Key.LeftArrow && cobra.direcao != Direction.Right)
                {
                    if (cobra.direcao == Direction.Up || cobra.direcao == Direction.Down)
                    {
                        TurnColided(cobra.segments[0].X - 1, cobra.segments[0].Y);
                    }
                    cobra.direcao = Direction.Left;
                }
                if (nextKeyInQueue == Key.UpArrow && cobra.direcao != Direction.Down)
                {
                    if (cobra.direcao == Direction.Right || cobra.direcao == Direction.Left)
                    {
                        TurnColided(cobra.segments[0].X, cobra.segments[0].Y - 1);
                    }
                    cobra.direcao = Direction.Up;
                }
            }
        }

        internal void InputRead()
        {
            while (true)
            {
                try
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    controls.ReadKeys(keyInfo);
                    //Debug.WriteIf(controls.keyQueue.Count != 0, string.Join(", ", controls.keyQueue) + "\n");
                }
                catch (Exception e)
                {
                    Debug.Write("falha ao ler input!\n" + e);
                }
            }

        }

        internal void NewFrutaPosition()
        {
            Vector target;
            do
            {
                target = new Vector(rand.Next(0, (Screen.FIELD_WIDTH - 1)), rand.Next(0, (Screen.FIELD_HEIGHT - 1)));

            } while (cobra.segments.Contains(target));

            fruta.X = target.X;
            fruta.Y = target.Y;
        }


        public bool Colided()
        {
            List<Vector> tail = cobra.segments.GetRange(1, cobra.segments.Count - 1);
            return tail.Contains(cobra.frontPosition);
        }
        public void TurnColided(int X, int Y)
        {
            Vector turnColision = new Vector(X, Y);
            List<Vector> tail = cobra.segments.GetRange(1, cobra.segments.Count - 1);
            turnColided = tail.Contains(turnColision) && cobra.tailSize > 3;
        }
        public void UpdateField()
        {
            for (int i = 0; i < Screen.FIELD_HEIGHT; i++)
            {
                for (int j = 0; j < Screen.FIELD_WIDTH; j++)
                {

                    if (cobra.segments[0].Y == fruta.Y && cobra.segments[0].X == fruta.X)
                    {
                        NewFrutaPosition();
                        cobra.tailSize++;
                        cobra.AddTail();
                    }

                    if (i == cobra.segments[0].Y && j == cobra.segments[0].X)
                    {
                        screen.field[i, j] = 1;
                    }
                    else if (i == fruta.Y && j == fruta.X)
                    {
                        screen.field[i, j] = 2;
                    }
                    else screen.field[i, j] = 0;

                    for (int k = 0; k < cobra.tailSize + 1; k++)
                    {
                        if (i == cobra.segments[k].Y && j == cobra.segments[k].X)
                        {
                            screen.field[i, j] = 1;
                            break;
                        }
                    }
                }
            }
        }
    }
}
