using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    public class Worker : IEquatable<Worker>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WorkingPosition { get; set; }

        public bool Equals(Worker other) => FirstName == other.FirstName && LastName == other.LastName;
    }
}
