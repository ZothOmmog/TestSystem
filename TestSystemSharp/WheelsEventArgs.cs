using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystemSharp
{
    class WheelsEventArgs
    {
        public Point LeftPoint { get; private set; }
        public Point Center { get; private set; }
        public Point RightPoint { get; private set; }

        public Point AnotherWheel { get; private set; }
        public Vector Perpendicular { get; private set; }

        public WheelsEventArgs(Point LeftPoint, Point Center, Point RightPoint, Point AnotherWheel, Vector Perpendicular)
        {
            Init(LeftPoint, Center, RightPoint, AnotherWheel, Perpendicular);
        }

        private void Init(Point LeftPoint, Point Center, Point RightPoint, Point AnotherWheel, Vector Perpendicular)
        {
            this.LeftPoint = LeftPoint;
            this.Center = Center;
            this.RightPoint = RightPoint;
            this.Perpendicular = Perpendicular;
            this.AnotherWheel = AnotherWheel;
        }
    }
}
