using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Linq
{
    public class Animal : Entity
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public int SpeciesId { get; set; }
    }
}
