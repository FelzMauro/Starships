using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Domain.Entities
{
    public class ResultDTO
    {
        public StartshipDTO Starship { get; set; }
        public string Stop { get; set; }
        public string Name => Starship.name;
        public string StarshipDetails => "Name: " + Starship.name + ", Crew: " + Starship.crew + ", MGLT: " + Starship.MGLT + ", Consumables: " + Starship.consumables + ", Stops: " + Stop;
    }
}