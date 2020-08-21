using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Entities
{
    public class GridDTO
    {
        public string count { get;set;}
        public string next { get; set; }
        public string previous { get; set; }
        public virtual List<StartshipDTO> results { get; set; }
    }
}