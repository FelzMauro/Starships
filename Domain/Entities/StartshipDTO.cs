using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Entities
{
    public class StartshipDTO
    {
        public string name { get; set; }  // The name of this starship.The common name, such as "Death Star".
        public string model { get; set; }  // The model or official name of this starship.Such as "T-65 X-wing" or "DS-1 Orbital Battle Station".
        public string manufacturer { get; set; }  // The manufacturer of this starship.Comma separated if more than one.
        public string cost_in_credits { get; set; }  // The cost of this starship new, in galactic credits.
        public string length { get; set; }  // The length of this starship in meters.
        public string max_atmosphering_speed { get; set; }  // The maximum speed of this starship in the atmosphere. "N/A" if this starship is incapable of atmospheric flight.
        public string crew { get; set; }  // The number of personnel needed to run or pilot this starship.
        public string passengers { get; set; }  // The number of non-essential people this starship can transport.
        public string cargo_capacity { get; set; }  // The maximum number of kilograms that this starship can transport.
        public string consumables { get; set; } //public string The maximum length of time that this starship can provide consumables for its entire crew without having to resupply.
        public string hyperdrive_rating { get; set; }  // The class of this starships hyperdrive.
        public string MGLT { get; set; }  // The Maximum number of Megalights this starship can travel in a standard hour.A "Megalight" is a standard unit of distance and has never been defined before within the Star Wars universe. This figure is only really useful for measuring the difference in speed of starships.We can assume it is similar to AU, the distance between our Sun (Sol) and Earth.
        public string starship_class { get; set; }  // The class of this starship, such as "Starfighter" or "Deep Space Mobile Battlestation"
        public string[] pilots { get; set; } // An array of People URL Resources that this starship has been piloted by.
        public string[] films { get; set; } // An array of Film URL Resources that this starship has appeared in.
        public string created { get; set; }  // the ISO 8601 date format of the time that this resource was created.
        public string edited { get; set; }  // the ISO 8601 date format of the time that this resource was edited.
        public string url { get; set; }  // the hypermedia URL of this resource.
    }
}