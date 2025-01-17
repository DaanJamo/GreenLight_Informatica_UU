﻿using System;
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
        int roadLanes;
        int thisLane;
        Bitmap Lane;
        Bitmap Verticallane;

        public DrivingLane(List<LanePoints> _points, string _dir, int _roadLanes, int _thisLane)
        {
            this.points = _points;
            this.dir = _dir;
            this.roadLanes = _roadLanes;
            this.thisLane = _thisLane;
            Lane = new Bitmap(Properties.Resources.Lane);
            Verticallane = new Bitmap(Properties.Resources.Road_Verticaal);
        }

        public Pen getPen(int _side)
        {
            Pen p = new Pen(Color.FromArgb(248,185,0), 3);

            if (thisLane <= roadLanes - 2)
            {
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            }
            else if (roadLanes == 1)
            {
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                p.Width = 7;
                p.Color = Color.White;
            }
            else if (thisLane == roadLanes - 1)
            {
                if (_side == 1)
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    p.Width = 7;
                    p.Color = Color.White;
                }
                else
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                }
            }
            else if (thisLane == roadLanes)
            {
                if (_side == 1)
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                }
                else
                {
                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    p.Width = 7;
                    p.Color = Color.White;
                }
            }
            return p;
        }

        public void Draw(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Pen p = new Pen(Color.FromArgb(21,21,21), 40);
            int drivingLaneDistance = 40; 
            double slp, slpPer, oneX, amountX;
            int startAngle = 0, sweepAngle = 90;
            Rectangle rect = new Rectangle();
            Size size;
            Rectangle outer, inner;
            int side1 = 0, side2 = 0;

            if (dir.Length > 1) //CurvedRoad
            {
                size = new Size(Math.Abs(points[points.Count - 1].cord.X - points[0].cord.X) * 2, Math.Abs(points[points.Count - 1].cord.Y - points[0].cord.Y) * 2);
                switch (dir)
                {
                    case "SE":
                        {
                            startAngle = 180;
                            rect = new Rectangle(new Point(Math.Min(points[0].cord.X, points[points.Count - 1].cord.X), Math.Min(points[0].cord.Y, points[points.Count - 1].cord.Y)), size);
                            side1 = 2;
                            side2 = 1;
                        }
                        break;
                    case "SW":
                        {
                            startAngle = 270;
                            rect = new Rectangle(new Point(Math.Max(points[0].cord.X, points[points.Count - 1].cord.X) - size.Width, Math.Min(points[0].cord.Y, points[points.Count - 1].cord.Y)), size);
                            side1 = 1;
                            side2 = 2;
                        }
                        break;
                    case "NW":
                        {
                            startAngle = 0;
                            rect = new Rectangle(new Point(Math.Max(points[0].cord.X, points[points.Count - 1].cord.X) - size.Width, Math.Max(points[0].cord.Y, points[points.Count - 1].cord.Y) - size.Height), size);
                            side1 = 1;
                            side2 = 2;
                        }
                        break;
                    case "NE":
                        {
                            startAngle = 90;
                            rect = new Rectangle(new Point(Math.Min(points[0].cord.X, points[points.Count - 1].cord.X), Math.Max(points[0].cord.Y, points[points.Count - 1].cord.Y) - size.Height), size);
                            side1 = 2;
                            side2 = 1;
                        }
                        break;
                }
                Console.WriteLine(dir);
                g.DrawArc(p, rect, startAngle, sweepAngle);

                
                outer = new Rectangle(new Point(rect.Location.X - drivingLaneDistance / 2, rect.Location.Y - drivingLaneDistance / 2), new Size(rect.Width + drivingLaneDistance, rect.Height + drivingLaneDistance));
                inner = new Rectangle(new Point(rect.Location.X + drivingLaneDistance / 2, rect.Location.Y + drivingLaneDistance / 2), new Size(rect.Width - drivingLaneDistance, rect.Height - drivingLaneDistance));
                
                g.DrawArc(getPen(side1), outer, startAngle, sweepAngle);
                g.DrawArc(getPen(side2), inner, startAngle, sweepAngle);

            }
            else
            {
                g.DrawLine(p, points[0].cord, points[points.Count - 1].cord);

                if (dir == "D")  //DiagonalRoad
                {
                    if (points[0].cord.X != points[points.Count - 1].cord.X && points[0].cord.Y != points[points.Count - 1].cord.Y)
                    {
                        slp = (double)(points[0].cord.Y - points[points.Count - 1].cord.Y) / (double)(points[points.Count - 1].cord.X - points[0].cord.X);
                        slpPer = -1 / slp;
                        oneX = Math.Abs(Math.Sqrt(1 + Math.Pow(slpPer, 2)));
                        amountX = (drivingLaneDistance / 2) / oneX;

                        g.DrawLine(getPen(1), new Point(points[0].cord.X - (int)amountX, points[0].cord.Y + (int)(slpPer * amountX)), new Point(points[points.Count - 1].cord.X - (int)amountX, points[points.Count - 1].cord.Y + (int)(slpPer * amountX)));
                        g.DrawLine(getPen(2), new Point(points[0].cord.X + (int)amountX, points[0].cord.Y - (int)(slpPer * amountX)), new Point(points[points.Count - 1].cord.X + (int)amountX, points[points.Count - 1].cord.Y - (int)(slpPer * amountX)));
                    }
                    else if (points[0].cord.X == points[points.Count - 1].cord.X)
                    {
                        g.DrawLine(getPen(1), new Point(points[0].cord.X - drivingLaneDistance / 2, points[0].cord.Y), new Point(points[points.Count - 1].cord.X - drivingLaneDistance / 2, points[points.Count - 1].cord.Y));
                        g.DrawLine(getPen(2), new Point(points[0].cord.X + drivingLaneDistance / 2, points[0].cord.Y), new Point(points[points.Count - 1].cord.X + drivingLaneDistance / 2, points[points.Count - 1].cord.Y));
                    }
                    else
                    {
                        g.DrawLine(getPen(1), new Point(points[0].cord.X, points[0].cord.Y - drivingLaneDistance / 2), new Point(points[points.Count - 1].cord.X, points[points.Count - 1].cord.Y - drivingLaneDistance / 2));
                        g.DrawLine(getPen(2), new Point(points[0].cord.X, points[0].cord.Y + drivingLaneDistance / 2), new Point(points[points.Count - 1].cord.X, points[points.Count - 1].cord.Y + drivingLaneDistance / 2));
                    }

                }
                else //StraightRoad
                {
                    if (points[0].cord.X == points[points.Count - 1].cord.X)
                    {
                        g.DrawLine(getPen(1), new Point(points[0].cord.X - drivingLaneDistance / 2, points[0].cord.Y), new Point(points[points.Count - 1].cord.X - drivingLaneDistance / 2, points[points.Count - 1].cord.Y));
                        g.DrawLine(getPen(2), new Point(points[0].cord.X + drivingLaneDistance / 2, points[0].cord.Y), new Point(points[points.Count - 1].cord.X + drivingLaneDistance / 2, points[points.Count - 1].cord.Y));
                    }
                    else
                    {
                        g.DrawLine(getPen(1), new Point(points[0].cord.X, points[0].cord.Y - drivingLaneDistance / 2), new Point(points[points.Count - 1].cord.X, points[points.Count - 1].cord.Y - drivingLaneDistance / 2));
                        g.DrawLine(getPen(2), new Point(points[0].cord.X, points[0].cord.Y + drivingLaneDistance / 2), new Point(points[points.Count - 1].cord.X, points[points.Count - 1].cord.Y + drivingLaneDistance / 2));
                    }
                }
            }
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
