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
        public const int FIELD_WIDTH = 25;
        public const int FIELD_HEIGHT = 10;
        public int[,] field { get; set; }
        int fieldXPosition, fieldYPosition;
        public int[,] canva { get; set; }

        public Screen()
        {
            fieldXPosition = 1;
            fieldYPosition = 1;
            field = new int[FIELD_HEIGHT, FIELD_WIDTH];
            canva = new int[FIELD_HEIGHT + 2, FIELD_WIDTH + 2];
            InitializeCanva();
        }

        public void DrawCanva()
        {
            PlaceFieldInCanva();
            ColoredDraw();
            //Console.Write(sb);;
        }


        internal void SimpleDraw()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < canva.GetLength(0); i++)
            {
                for (int j = 0; j < canva.GetLength(1); j++)
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
                        case 3:
                            sb.Append('[');
                            sb.Append(']');

                            break;
                        default:
                            sb.Append('╬');
                            sb.Append('╬');

                            break;
                    }
                }
                sb.Append("\n");
            }
            Console.Write(sb);
        }

        internal void PlaceFieldInCanva()
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    canva[i + fieldYPosition, j + fieldXPosition] = field[i, j];
                }
            }
        }

        internal void ColoredDraw()
        {
            for (int i = 0; i < canva.GetLength(0); i++)
            {
                for (int j = 0; j < canva.GetLength(1); j++)
                {
                    switch (canva[i, j])
                    {
                        case 0:
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("░░");
                            break;
                        case 1:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("██");
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("██");
                            break;
                        case -1:
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write("╬╬");
                            break;
                    }
                }
                Console.WriteLine();
            }
        }

        internal void InitializeCanva()
        {
            for (int i = 0; i < canva.GetLength(0); i++)
            {
                for (int j = 0; j < canva.GetLength(1); j++)
                {
                    canva[i, j] = -1;
                }
            }
        }

    }
}
