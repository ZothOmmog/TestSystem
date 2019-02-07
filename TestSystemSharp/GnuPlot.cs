using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.IO;

namespace TestSystemSharp
{
    static class GnuPlot
    {
        static public void CreateScript(string data, string script, int kol_graph, string[] titles)
        {
            StreamWriter sw = new StreamWriter(new FileStream(script, FileMode.Create));
            sw.Write("set size ratio -1\n" +
                "set grid\n" +
                "plot [-10:20][0:30]");

            for (int i = 0, j = 1; i < kol_graph; i++, j += 2)
            {
                sw.Write(" '{0}' u {1}:{2} w l title '{3}'", data, j, j + 1, titles[i]);
                if (i < kol_graph - 1) sw.Write(',');
            }

            sw.Write("\nwhile(1){}");

            sw.Close();
        }

        static public void RunScript(string script)
        {
            Process prc = null;

            try
            {
                prc = new Process();
                prc.StartInfo.FileName = script;
                
                prc.Start();
                
                prc.WaitForExit();
            }
            finally
            {
                if (prc != null) prc.Close();
            }
        }
    }
}
