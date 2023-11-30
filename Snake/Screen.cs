using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Snake
{
    class Screen
    {
        public const int WIDTH = 25;
        public const int HEIGHT = 10;
        private Cobra cobra;
        public int[,] canva { get; set; }

        public Screen (Cobra cobra)
        {
            this.cobra = cobra;
            canva = new int[HEIGHT, WIDTH];
        }

        public void DrawCanva()
        {

            SimpleDraw();

            //Console.Write(sb);
            DebugPerFrame();
        }

        public void DebugPerFrame()
        {

            

            //sb.Append("next head position: ");
            //sb.Append(cobra.nextHeadPosition.X);
            //sb.Append('/');
            //sb.Append(cobra.nextHeadPosition.Y);
            //sb.Append("    ");
            //sb.Append("\n");
            //for (int i = 0; i < cobra.tailSize + 1; i++)
            //{
            //    sb.Append(i);
            //    sb.Append(": ");
            //    sb.Append(cobra.segments[i].X);
            //    sb.Append('/');
            //    sb.Append(cobra.segments[i].Y);
            //    sb.Append("    ");
            //    sb.Append("\n");
            //}
            //Console.Write(sb);
        }

        internal void SimpleDraw()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    switch (canva[i, j])
                    {
                        case 0:
                            sb.Append('░');
                            sb.Append('░');

                            break;
                        case 1:
                            sb.Append('█');
                            sb.Append('█');

                            break;
                        case 2:
                            sb.Append('▓');
                            sb.Append('▓');

                            break;
                        default:
                            sb.Append(' ');
                            sb.Append(' ');

                            break;
                    }
                }
                sb.Append("\n");
            }
            Console.Write(sb);
        }

        internal void ColoredDraw()
        {
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    switch (canva[i, j])
                    {
                        case 0:
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("██");
                            break;
                        case 1:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("██");
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("██");
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("██");
                            break;
                    }
                }
                Console.WriteLine();
            }
        }

    }
}
