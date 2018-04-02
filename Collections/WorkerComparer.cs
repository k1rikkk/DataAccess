using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    public class WorkerComparer : IComparer<Worker>
    {
        public int Compare(Worker x, Worker y) => string.Compare(x.WorkingPosition, y.WorkingPosition);
    }
}
