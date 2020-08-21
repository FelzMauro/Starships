using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StarShips.Models
{
    public class GridResultViewModel
    {
        public string MGLTView { get; set; }
        public string Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public virtual List<ResultDTO> ListResultDTO { get; set; }
    }
}