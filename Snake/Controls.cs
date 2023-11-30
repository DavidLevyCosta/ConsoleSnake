using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;

namespace Snake
{
    public enum Key
    {
        Right,
        Down,
        Left,
        Up
    }

    class Controls
    {
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(ConsoleKey vKey);

        public Key KeyPressed { get; private set; }
        public ConcurrentQueue<Key> keyQueue = new ConcurrentQueue<Key>();

        public Controls()
        {
            keyQueue.Enqueue(Key.Right);
        }

        public void ReadKeys()
        {
            bool isRightKeyPressed = IsKeyDown(ConsoleKey.RightArrow);
            bool isLeftKeyPressed = IsKeyDown(ConsoleKey.LeftArrow);
            bool isUpKeyPressed = IsKeyDown(ConsoleKey.UpArrow);
            bool isDownKeyPressed = IsKeyDown(ConsoleKey.DownArrow);

            bool onlyRightKeyPressed = isRightKeyPressed && !isDownKeyPressed && !isLeftKeyPressed && !isUpKeyPressed;
            bool onlyDownKeyPressed = isDownKeyPressed && !isRightKeyPressed && !isLeftKeyPressed && !isUpKeyPressed;
            bool onlyLeftKeyPressed = isLeftKeyPressed && !isDownKeyPressed && !isRightKeyPressed && !isUpKeyPressed;
            bool onlyUptKeyPressed = isUpKeyPressed && !isDownKeyPressed && !isLeftKeyPressed && !isRightKeyPressed;



            // -----------------Right------------------------

            if (onlyRightKeyPressed && (keyQueue.Count == 0 || keyQueue.Last() != Key.Right))
            {
                // A tecla direita foi pressionada
                keyQueue.Enqueue(Key.Right);

            }

            // ------------------Left-----------------------

            if (onlyLeftKeyPressed && (keyQueue.Count == 0 || keyQueue.Last() != Key.Left))
            {
                // A tecla esquerda foi pressionada
                keyQueue.Enqueue(Key.Left);
            }

            // -------------------Up-----------------------

            if (onlyUptKeyPressed && (keyQueue.Count == 0 || keyQueue.Last() != Key.Up))
            {
                // A tecla direita foi pressionada
                keyQueue.Enqueue(Key.Up);
            }

            // ------------------Down----------------------

            if (onlyDownKeyPressed && (keyQueue.Count == 0 || keyQueue.Last() != Key.Down))
            {
                // A tecla esquerda foi pressionada
                keyQueue.Enqueue(Key.Down);
            }

        }

        private bool IsKeyDown(ConsoleKey key)
        {
            return (GetAsyncKeyState(key) < 0);
        }



    }
}
