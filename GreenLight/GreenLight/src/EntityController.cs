using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLight
{
    public abstract class EntityController : AbstractController
    {
        public List<VehicleOriginPoint> originPoints = new List<VehicleOriginPoint> { };
        static Random random = new Random();
        public int Totalweight
        {
            get
            {
                int totalweight = 0;
                for (int t = 0; t < originPoints.Count; t++)
                {
                    totalweight += originPoints[t].weight;
                }
                Console.WriteLine("Calculated totalweight for originpoints.");
                return totalweight;
            }
        }
        public Point chooseOriginPoint()
        {
            int index = random.Next(originPoints.Count);
            return originPoints[index].Cords;
        }
    }
}