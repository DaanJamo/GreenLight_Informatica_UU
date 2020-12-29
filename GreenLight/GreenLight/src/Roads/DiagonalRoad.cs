﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GreenLight
{
    public class DiagonalRoad : AbstractRoad
    {
        //Similar to CurvedRoad, but now math for diagnol

        private string dir;

        public DiagonalRoad(Point _point1, Point _point2, int _lanes, string _dir) : base(_point1, _point2, _lanes)
        {
            this.dir = _dir;

            for (int x = 1; x <= this.lanes; x++)
            {
                this.Drivinglanes.Add(CalculateLanes(_point1, _point2, x));
            }
        }

        protected override DrivingLane CalculateDrivingLane(Point _point1, Point _point2, int _thisLane)
        {
            List<LanePoints>_lanePoints = new List<LanePoints>();
            Point _normpoint1 = _point1; Point _normpoint2 = _point2;
            double _rc;
            Point _prev = _normpoint1;
            bool divByZero = false;
           
            _rc = (double)(_point2.Y - _point1.Y) / (double)(_point2.X - _point1.X);
            if (_point2.X - _point1.X == 0)
            { 
                _rc = 0;
                int _vertical;
                divByZero = true;
                if (_point1.Y > _point2.Y)
                    _vertical = -1;
                else
                    _vertical = 1;

                for (int y = 0 ; y <= Math.Abs(_point1.Y - _point2.Y); y++)
                {
                    _normpoint1 = new Point(_point1.X, (int)(_point1.Y + y * _vertical));
                    _lanePoints.Add(new LanePoints(_normpoint1, AbstractRoad.CalculateAngle(_prev, _normpoint1)));

                    _prev = _normpoint1;
                }
            }
            Console.WriteLine(_rc);
            Console.WriteLine(_thisLane);

            int _dir = GetDirection(_point1, _point2);

            /*if (_rc >= 0.5 || _point2.X - _point1.X == 0)
            {
                for (int y = 0; y <= Math.Abs(_point1.Y - _point2.Y); y++)
                {
                    _normpoint1 = new Point(_point1.X + y/_rc * _dir, (int)(_point1.Y + y * _rc * _dir));
                    _lanePoints.Add(new LanePoints(_normpoint1, AbstractRoad.CalculateAngle(_prev, _normpoint1)));

                    _prev = _normpoint1;
                }
            }*/

            for (int x = 0; x <= Math.Abs(_point1.X - _point2.X) && !divByZero; x++)
            {
                _normpoint1 = new Point(_point1.X + x * _dir, (int)(_point1.Y + x * _rc * _dir));
                _lanePoints.Add(new LanePoints(_normpoint1, AbstractRoad.CalculateAngle(_prev, _normpoint1)));

                _prev = _normpoint1;
            }
            

            foreach (LanePoints x in _lanePoints)
            {
                Console.WriteLine(x.ToString());
            }

            return new DrivingLane(_lanePoints, this.dir, lanes, _thisLane); 
        }

        private int GetDirection(Point _point1, Point _point2)
        {
            if (_point2.X >= _point1.X)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        private DrivingLane CalculateLanes(Point _firstPoint, Point _secondPoint, int t)
        {
            int drivingLaneDistance = 40;
            double slp, slpPer, oneX, amountX;

            if (_firstPoint.X != _secondPoint.X && _firstPoint.Y != _secondPoint.Y)
            {
                slp = (double)(_firstPoint.Y - _secondPoint.Y) / (double)(_secondPoint.X - _firstPoint.X);
                slpPer = -1 / slp;
                oneX = Math.Abs(Math.Sqrt(1 + Math.Pow(slpPer, 2)));
                amountX = drivingLaneDistance / oneX;

                if (lanes % 2 == 0)
                {
                    if (t % 2 == 0)
                    {
                        _firstPoint.X += (int)(amountX / 2 + amountX * (t / 2 - 1));
                        _firstPoint.Y -= (int)(slpPer * amountX / 2 + slpPer * amountX * (t / 2 - 1));
                        _secondPoint.X += (int)(amountX / 2 + amountX * (t / 2 - 1));
                        _secondPoint.Y -= (int)(slpPer * amountX / 2 + slpPer * amountX * (t / 2 - 1));
                    }
                    else
                    {
                        _firstPoint.X -= (int)(amountX / 2 + amountX * (t - 1) / 2);
                        _firstPoint.Y += (int)(slpPer * amountX / 2 + slpPer * amountX * (t - 1) / 2);
                        _secondPoint.X -= (int)(amountX / 2 + amountX * (t - 1) / 2);
                        _secondPoint.Y += (int)(slpPer * amountX / 2 + slpPer * amountX * (t - 1) / 2);
                    }
                }
                else // (lanes % 2 == 1)
                {
                    if (t % 2 == 0)
                    {
                        _firstPoint.X -= (int)(amountX * t / 2);
                        _firstPoint.Y += (int)(slpPer * amountX * t / 2);
                        _secondPoint.X -= (int)(amountX * t / 2);
                        _secondPoint.Y += (int)(slpPer * amountX * t / 2);
                    }
                    else if (t % 2 == 1 && t != 1)
                    {
                        _firstPoint.X += (int)(amountX * (t-1) / 2);
                        _firstPoint.Y -= (int)(slpPer * amountX * (t-1) / 2);
                        _secondPoint.X += (int)(amountX * (t-1) / 2);
                        _secondPoint.Y -= (int)(slpPer * amountX * (t-1) / 2);

                    }
                }
            }
            else if (_firstPoint.X == _secondPoint.X)
            {
                if (lanes % 2 == 0)
                {
                    if (t % 2 == 0)
                    {
                        _firstPoint.X += (t / 2 - 1) * drivingLaneDistance + drivingLaneDistance / 2;
                        _secondPoint.X += (t / 2 - 1) * drivingLaneDistance + drivingLaneDistance / 2;
                    }
                    else
                    {
                        _firstPoint.X -= (t - 1) / 2 * drivingLaneDistance + drivingLaneDistance / 2;
                        _secondPoint.X -= (t - 1) / 2 * drivingLaneDistance + drivingLaneDistance / 2;
                    }
                }
                else //if (lanes%2 !=0)
                {
                    if (t % 2 == 0)
                    {
                        _firstPoint.X -= t / 2 * drivingLaneDistance;
                        _secondPoint.X -= t / 2 * drivingLaneDistance;
                    }
                    else if (t % 2 == 1 && t != 1)
                    {
                        _firstPoint.X += (t - 1) / 2 * drivingLaneDistance;
                        _secondPoint.X += (t - 1) / 2 * drivingLaneDistance;
                    }
                }
            }
            else //if(_fistrPoint.Y == _secondPoint.Y)
            {
                if (lanes % 2 == 0)
                {
                    if (t % 2 == 0)
                    {
                        _firstPoint.Y += (t / 2 - 1) * drivingLaneDistance + drivingLaneDistance / 2;
                        _secondPoint.Y += (t / 2 - 1)* drivingLaneDistance + drivingLaneDistance / 2;
                    }
                    else
                    {
                        _firstPoint.Y -= (t - 1) / 2 * drivingLaneDistance + drivingLaneDistance / 2;
                        _secondPoint.Y -= (t - 1) / 2 * drivingLaneDistance + drivingLaneDistance / 2;
                    }

                }
                else // (lanes % 2 == 1)
                {
                    if (t % 2 == 0)
                    {
                        _firstPoint.Y -= t / 2 * drivingLaneDistance;
                        _secondPoint.Y -= t / 2 * drivingLaneDistance;
                    }
                    else if (t % 2 == 1 && t != 1)
                    {
                        _firstPoint.Y += (t - 1) / 2 * drivingLaneDistance;
                        _secondPoint.Y += (t - 1) / 2 * drivingLaneDistance;
                    }
                }
            }

            return CalculateDrivingLane(_firstPoint, _secondPoint, t);
        }

    }   
}
