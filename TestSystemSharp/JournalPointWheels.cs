using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystemSharp
{
    class JournalPointWheels
    {
        public List<JournalEntry> ArrEntry { get; private set; }

        public JournalPointWheels()
        {
            ArrEntry = new List<JournalEntry>();
        }

        public void Add(object source, WheelsEventArgs args)
        {
            ArrEntry.Add(new JournalEntry(args));
        }

        public override string ToString()
        {
            string rez = "";
            foreach (JournalEntry i in ArrEntry) rez += i.ToString() + '\n';
            return rez;
        }
    }
}
