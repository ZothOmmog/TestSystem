using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystemSharp
{
    class JournalEntry
    {
        public string LeftWheel { get; private set; }
        public string Center { get; private set; }
        public string RightWheel { get; private set; }
        public string PointPreBegPerp { get; private set; }
        public string PointBegPerp { get; private set; }
        public string PointEndPerp { get; private set; }


        public JournalEntry(WheelsEventArgs args)
        {
            Init(args);
        }

        private void Init(WheelsEventArgs args)
        {
            this.LeftWheel = args.LeftPoint.ToString();
            this.Center = args.Center.ToString();
            this.RightWheel = args.RightPoint.ToString();
            this.PointPreBegPerp = args.AnotherWheel.ToString();
            this.PointBegPerp = args.Perpendicular.PointBegin.ToString();
            this.PointEndPerp = args.Perpendicular.PointEnd.ToString();
        }

        public override string ToString()
        {
            string rez = "#\n" + PointPreBegPerp + '\t' + LeftWheel + '\t' + Center + '\t' + RightWheel + '\n' +
                                 PointBegPerp + '\n' + PointEndPerp;
            return rez.Replace(',', '.');
        }
    }
}
