﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace GreenLight
{
    class Startup : Form
    {
        //Quick temporary form for testing purposes, on which a few driving cars are simulated

        bool simulate;
        public List<AI> driverList = new List<AI> { };
        public List<bool> listchoice = new List<bool> { };
        public Startup()
        {
            //createDriver();
            simulate = true;
            this.DoubleBuffered = true;
            this.Paint += teken;

            Thread run = new Thread(simulation);
            run.Start();
            Thread drivers = new Thread(createDriver);
            drivers.Start();
        }

        private void createDriver()
        {
            /*Vehicle v = new Vehicle(new VehicleStats("Auto", 1353, 4.77f, 100, 4223, 2, 2.65f), 10, 10);            
            AI driver = new AI(v, new DriverStats("new driver",250, 2, 0, 0));
            driverList.Add(driver);*/

            for (int n = 0; simulate && n < 40; n++)
            {
                Vehicle v = new Vehicle(new VehicleStats("Auto", 1353, 4.77f, 100, 4223/25, 2, 2.65f), 10, 10);
                AI driver = new AI(v, new DriverStats("new driver", 250, 2, 0, 0));
                driverList.Add(driver);
                listchoice.Add(true);
                Thread.Sleep(1000);
            }
        }
        private void simulation()
        {
            while (simulate)
            {
                Thread.Sleep(16);
                this.Invalidate();
            }
        }

        public void teken(object o, PaintEventArgs pea)
        {
            for (int t = 0; t < driverList.Count; t++)
            {
                if (listchoice[t] && driverList[t].v.frame <= 624)
                {
                    driverList[t].v.tekenAuto(pea.Graphics, driverList[t].location);
                    if (driverList[t].v.frame == 624)
                    {
                        listchoice[t] = false;
                        driverList[t].v.frame = 0;
                        /*Console.WriteLine("Switch naar lijst 2 van vehicle " + t + ".");*/
                    }
                }
                else if (!listchoice[t] && driverList[t].v.frame <= 624)
                {
                    driverList[t].v.tekenAuto(pea.Graphics, driverList[t].location2);
                    if (driverList[t].v.frame == 624)
                    {
                        listchoice[t] = true;
                        driverList[t].v.frame = 0;
                        /*Console.WriteLine("Switch naar lijst 1 van vehicle " + t + ".");*/
                    }
                }
            }
        }
    }
}