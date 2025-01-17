﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GreenLight
{
    public struct LanePoints
    {
        //lanepoints are very simple put, just a point with a corresponding degree, so every car can drive from points to point
        //and angle itself accordingly between the 2 to create a smooth illusion of driving.

        public Point cord { get; }
        public int degree { get; }

        public LanePoints(Point _cord, int _degree)
        {
            this.cord = _cord;
            this.degree = _degree;
        }

        public override string ToString()
        {
            string _temp = "CORDS: "+ this.cord + "  -  DEGREE: " + this.degree;
            return _temp;
        }
    }
}
