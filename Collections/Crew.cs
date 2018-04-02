using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    public class Crew : List<Worker>
    {
        public void SortByWorkingPosition() => Sort(new WorkerComparer());
    }
}
