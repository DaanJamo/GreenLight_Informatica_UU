using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GreenLight
{
    public class DrivingLane
    {
        //Every road has a list of these DrivingLanes, a driving lane consists of a list of LanePoints
        //And for now an int that determines which type of road it is.
        //Each object from this class also has its own Draw feature, this draw feature
        //Draws a straight lane between all the points in the LanePoints list in order.
        //This is used for testing to see if our algorithm created a smooth road -- This will not be used in final release.


        public List<LanePoints> points;
        string dir;
        Bitmap Lane;
        Bitmap Verticallane;

        public DrivingLane(List<LanePoints> _points, string _dir)
        {
            this.points = _points;
            this.dir = _dir;
            Lane = new Bitmap(Properties.Resources.Lane);
            Verticallane = new Bitmap(Properties.Resources.Road_Verticaal);
        }

        public void Draw(Graphics g)
        {
            Pen p = new Pen(Color.Gray, 40);
            Pen pb = new Pen(Color.Black, 1);
            double rc, invrc;
            if (dir.Length > 1)
            {

            }
            else
            {
                g.DrawLine(p, points[0].cord, points[points.Count - 1].cord);
                rc = Math.Abs(points[0].cord.Y - points[points.Count - 1].cord.Y) / Math.Abs(points[0].cord.X - points[points.Count - 1].cord.X);
                invrc = -1 / rc;
                g.DrawLine(pb, Math.Min(points[0].cord.X, points[points.Count - 1].cord.X), points[0].cord.Y - 20, points[points.Count - 1].cord.X, points[points.Count - 1].cord.Y - 20);
            }

            Point[] _pointsarray = new Point[points.Count];
            int t = 0;
            foreach (LanePoints x in points)
            {
                _pointsarray[t] = x.cord;
                t++;
            }
            
            //g.DrawArc(p, _pointtemp.X, _pointtemp.Y - 20, Math.Abs(_pointtemp.X - x.cord.X), 40, 0, x.degree);
            //g.DrawCurve(p, _pointsarray);

            try
                {
                Point _pointtemp = points[0].cord;

                foreach (LanePoints x in points)
<<<<<<< HEAD
            {
                    //g.DrawRectangle(Pens.Black, _pointtemp.X, _pointtemp.Y - 20, Math.Abs(_pointtemp.X - x.cord.X), 40);
                    g.DrawImage(Lane, _pointtemp.X, _pointtemp.Y - 20, Math.Abs(_pointtemp.X - x.cord.X), 40);
                    
                    //g.DrawLine(Pens.Red, _pointtemp, x.cord);
                    // g.FillEllipse(Brushes.Gray, _pointtemp.X -20, _pointtemp.Y - 20, 40, 40);
                   // g.DrawRectangle(Pens.Red, _pointtemp.X, _pointtemp.Y, 1, 1);
                   
=======
                {
                    g.FillEllipse(Brushes.Gray, _pointtemp.X - 20, _pointtemp.Y - 20, 40, 40);
<<<<<<< Updated upstream
=======
>>>>>>> 5a1105c734ba0c2157ff68e3c6338f3a769d8d3e
>>>>>>> Stashed changes
                    _pointtemp = x.cord;
                   
                }
            }
           catch(Exception e) { }
        }

        public static Bitmap RotateImage(Bitmap b, float angle)
        {
            Bitmap returnBitmap = new Bitmap(b.Width, b.Height);
            using (Graphics g = Graphics.FromImage(returnBitmap))
            {
                g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
                g.DrawImage(b, new Point(0, 0));
            }
            return returnBitmap;
        }

        public void LogPoints()
        {
            foreach(LanePoints _point in this.points)
            {
                Console.WriteLine(_point.cord);
            }
        }
    }
}
