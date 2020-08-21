using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Runner
    {
        private readonly ICalculator _calculator;

        private readonly IApiCaller _apiCaller;

        private List<StartshipDTO> starships;

        public int CountStarShips => starships.Count;

        public Runner(ICalculator calculator, IApiCaller apiCaller)
        {

            _calculator = calculator;
            _apiCaller = apiCaller;
        }

        public void Init()
        {
            starships = _apiCaller.GetAll();
        }
        
        /// <summary>
        /// Run all Starships and return how many stop each starship needs by distance
        /// </summary>
        /// <param name="distance"></param>
        /// <returns>listResultDTO</returns>
        public List<ResultDTO> Run(long distance)
        {
            var resultList = new List<ResultDTO>();
            if (starships == null)
                throw new ArgumentNullException(nameof(starships));

            foreach (var starship in starships)
            {
                ResultDTO resultDTO = new ResultDTO();

                int MGLT = 0;
                if (starship.MGLT != "unknown")
                    MGLT = Convert.ToInt32(starship.MGLT);

                string[] consumablesSplit = starship.consumables.Split(' ');
                string stops;
                if (consumablesSplit.Length == 2 && MGLT > 0)
                {
                    // Calculate total number of hours that the consumables can last.
                    long hours = _calculator.ToHours(Convert.ToInt32(consumablesSplit[0]), consumablesSplit[1]);
                    // finaly calculate total number of stops required based on the distance, speed and time
                    stops = _calculator.ToStops(Convert.ToInt32(distance), MGLT, hours).ToString();
                }
                else
                {
                    stops = "unknown";
                }

                resultDTO.Starship = starship;
                resultDTO.Stop = stops;
                resultList.Add(resultDTO);
            }

            return resultList;
        }

        /// <summary>
        /// Run a single Starship by Name
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="name"></param>
        /// <returns>ResultDTO</returns>
        public ResultDTO RunByName(long distance, string name)
        {
            int MGLT = 0;
            string stops = string.Empty;
            if (starships == null)
                throw new ArgumentNullException(nameof(starships));

            var starship = starships.FirstOrDefault(x => x.name == name);

            if (starship == null)
                throw new ArgumentNullException(nameof(starships));
            if (starship.MGLT != "unknown")
                MGLT = Convert.ToInt32(starship.MGLT);
            string[] consumablesSplit = starship.consumables.Split(' ');
            if (consumablesSplit.Length == 2 && MGLT != 0)
            {
                // Calculate total number of hours that the consumables can last.
                long hours = _calculator.ToHours(Convert.ToInt32(consumablesSplit[0]), consumablesSplit[1]);
                // finaly calculate total number of stops required based on the distance, speed and time
                stops = _calculator.ToStops(Convert.ToInt32(distance), MGLT, hours).ToString();
            }
            else
            {
                stops = "unknown";
            }
            return new ResultDTO { Starship = starship, Stop = stops };
        }
        
    }
}
