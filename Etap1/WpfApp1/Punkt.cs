using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
   public class Punkt : IComparable<Punkt>
    {
        private double x;
        private double y;

        public double X
        {
            get { return this.x; }
            set { this.x = value; }
        }
        public double Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public Punkt(double x, double y)
        {
            this.x = x;
            this.y = y;
        }


        public int CompareTo(Punkt other)
        {
            if (this.y < other.y) return -1;
            if (this.y == other.y) return 0;
            return 1;
        }
    }
}
