using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snake;
using System.Diagnostics;

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

        private Vector fruta;

        byte desiredFps;

        public Engine() {
            
            cobra = new Cobra();
            screen = new Screen(cobra);
            controls = new Controls();
            rand = new Random();
            fruta = new Vector(0, 0);
            fruta.X = rand.Next(4, (Screen.WIDTH - 1));
            fruta.Y = rand.Next(4, (Screen.HEIGHT - 1));

            desiredFps = 6;
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
            if (!Colided()) { 
                ChangeDirection();
                cobra.Move();
                for (int i = 0; i < Screen.HEIGHT; i++)
                {
                    for (int j = 0; j < Screen.WIDTH; j++)
                    {

                        if (cobra.segments[0].Y == fruta.Y && cobra.segments[0].X == fruta.X)
                        {
                            NewFrutaPosition();
                            cobra.tailSize++;
                            cobra.AddTail();

                        }

                        if (i == cobra.segments[0].Y && j == cobra.segments[0].X)
                        {
                            screen.canva[i, j] = 1;
                        }
                        else if (i == fruta.Y && j == fruta.X) {
                            screen.canva[i, j] = 2;
                        }
                        else screen.canva[i, j] = 0;

                        for (int k = 0; k < cobra.tailSize + 1; k++)
                        {

                            if (i == cobra.segments[k].Y && j == cobra.segments[k].X)
                            {
                                screen.canva[i, j] = 1;
                                break;
                            }
                        }

                    }
                }
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
                if (nextKeyInQueue == Key.Right && cobra.direcao != 2) cobra.direcao = 0;
                if (nextKeyInQueue == Key.Down && cobra.direcao != 3) cobra.direcao = 1;
                if (nextKeyInQueue == Key.Left && cobra.direcao != 0) cobra.direcao = 2;
                if (nextKeyInQueue == Key.Up && cobra.direcao != 1) cobra.direcao = 3;
            }
        }

        internal void InputRead()
        {
            while (true)
            {
                controls.ReadKeys();
                Debug.WriteIf(controls.keyQueue.Count != 0, string.Join(", ", controls.keyQueue) + "\n");
            }

        }

        internal void NewFrutaPosition()
        {
            Vector target;
            do
            {
                target = new Vector(rand.Next(0, (Screen.WIDTH - 1)), rand.Next(0, (Screen.HEIGHT - 1)));

            } while (cobra.segments.Contains(target));

            fruta.X = target.X;
            fruta.Y = target.Y;
        }

        internal bool Colided()
        {
            List<Vector> tail = cobra.segments.GetRange(1, cobra.segments.Count - 1);
            return tail.Contains(cobra.nextHeadPosition);
        }
    }
}
