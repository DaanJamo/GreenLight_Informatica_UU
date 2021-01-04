using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace GreenLight
{
    public class VehicleOriginPoint : ScreenObject
    {
        Point p;
        public int weight;
        public VehicleOriginPoint(Point p) : base(p, new Size(10, 10))
        {
            this.p = p;
            this.weight = 10;
        }
        public void changeweight (int newweight)
        {
            weight = newweight;
        }
    }
}