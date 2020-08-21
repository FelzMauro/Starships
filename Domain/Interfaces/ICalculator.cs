using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface ICalculator
    {
        int ToStops(long distance, long mglt, long hours);

        long ToHours(int time, string type);
    }
}
