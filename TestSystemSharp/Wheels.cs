using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystemSharp
{
    class Wheels
    {
        public double WheelsDistance { get; private set; }
        public Point LeftWheel { get; private set; }
        public Point RightWheel { get; private set; }
        public Point Center { get { return new Point((LeftWheel.X + RightWheel.X) / 2, (LeftWheel.Y + RightWheel.Y) / 2); } }

        public Wheels(double WheelsDistance)
        {
            this.WheelsDistance = WheelsDistance;
            this.LeftWheel = new Point(-WheelsDistance / 2, 0);
            this.RightWheel = new Point(WheelsDistance / 2, 0);
        }
         
        public void ChangePosition(double speed_left, double speed_right)
        {
            Vector left_right = new Vector(LeftWheel, RightWheel);

            Vector new_position_left = new Vector(LeftWheel, speed_left, left_right);
            Vector new_position_right = new Vector(RightWheel, speed_right, left_right);

            RightWheel = new_position_right.PointEnd;
            LeftWheel = new_position_left.PointEnd;

            if (speed_left > speed_right) TurnRight();
            else if (speed_right > speed_left) TurnLeft();
        }

        private void TurnLeft()
        {
            Vector new_position_right = new Vector(new Vector(LeftWheel, RightWheel), WheelsDistance);
            RightWheel = new_position_right.PointEnd;
        }

        private void TurnRight()
        {
            Vector new_position_left = new Vector(new Vector(RightWheel, LeftWheel), WheelsDistance);
            LeftWheel = new_position_left.PointEnd;
        }

        public override string ToString()
        {
            return Center.ToString()+ " " + LeftWheel.ToString() + " " + RightWheel.ToString();
        }
    }
}