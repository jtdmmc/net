using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MT.PrismApp.Resources.DrawingTool
{
    public static class RayCastingAlgorithm
    {
        public static bool IsWithin(Point pt, IList<Point> polygon, bool noneZeroMode = false)
        {
            int ptNum = polygon.Count();
            if (ptNum < 3)
            {
                return false;
            }
            int j = ptNum - 1;
            bool oddNodes = false;
            int zeroState = 0;
            for (int k = 0; k < ptNum; k++)
            {
                Point ptK = polygon[k];
                Point ptJ = polygon[j];
                if (((ptK.Y > pt.Y) != (ptJ.Y > pt.Y)) && (pt.X < (ptJ.X - ptK.X) * (pt.Y - ptK.Y) / (ptJ.Y - ptK.Y) + ptK.X))
                {
                    oddNodes = !oddNodes;
                    if (ptK.Y > ptJ.Y)
                    {
                        zeroState++;
                    }
                    else
                    {
                        zeroState--;
                    }
                }
                j = k;
            }
            return noneZeroMode ? zeroState != 0 : oddNodes;
        }
    }
}
