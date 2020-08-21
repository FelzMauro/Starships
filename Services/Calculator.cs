using Domain.Interfaces;

namespace Services
{
    public class Calculator : ICalculator
    {
        public long ToHours(int time, string type)
        {
            return type switch
            {
                "day" => 24 * time,
                "days" => 24 * time,
                "week" => 168 * time,
                "weeks" => 168 * time,
                "month" => 730 * time,
                "months" => 730 * time,
                "year" => 8760 * time,
                "years" => 8760 * time,
                _ => 0,
            };
        }

        public int ToStops(long distance, long mglt, long hours)
        {
            return (int)((double)distance / (hours * mglt));
        }
    }
}
