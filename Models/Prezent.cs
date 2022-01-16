using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZadanieApi.Models
{
    public class Prezent
    {
        public int Id { get; set; }
        public string NazwaPrezentu { get; set; }
        public string KategoriaPrezentu { get; set; }
        public float CenaPrezentu { get; set; }
        public string WiekPrezentu { get; set; }

    }
    
}
