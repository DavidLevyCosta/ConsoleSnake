using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Vector
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Vector v = (Vector)obj;
            return (X == v.X) && (Y == v.Y);
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }
    }
}
