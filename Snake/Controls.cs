using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Snake
{
    public enum Key
    {
        RightArrow,
        DownArrow,
        LeftArrow,
        UpArrow
    }

    class Controls
    {
        public ConcurrentQueue<Key> keyQueue = new ConcurrentQueue<Key>();

        public Controls()
        {
            keyQueue.Enqueue(Key.RightArrow);
        }

        public void ReadKeys(ConsoleKeyInfo keyInfo)
        {
            // Verifica qual tecla foi pressionada e adiciona à fila
            switch (keyInfo.Key)
            {
                case ConsoleKey.RightArrow:
                    if (keyQueue.Count == 0 || keyQueue.Last() != Key.RightArrow)
                        keyQueue.Enqueue(Key.RightArrow);
                    break;
                case ConsoleKey.LeftArrow:
                    if (keyQueue.Count == 0 || keyQueue.Last() != Key.LeftArrow)
                        keyQueue.Enqueue(Key.LeftArrow);
                    break;
                case ConsoleKey.UpArrow:
                    if (keyQueue.Count == 0 || keyQueue.Last() != Key.UpArrow)
                        keyQueue.Enqueue(Key.UpArrow);
                    break;
                case ConsoleKey.DownArrow:
                    if (keyQueue.Count == 0 || keyQueue.Last() != Key.DownArrow)
                        keyQueue.Enqueue(Key.DownArrow);
                    break;
            }
        }
    }
}

