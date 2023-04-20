using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaCL.Model
{
    public class Partiture
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
        public string WorkType { get; set; }
        public string Instrumentation { get; set; }
        public string Language { get; set; }
        public byte[] File { get; set; } 

    }
}
