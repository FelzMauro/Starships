using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IApiCaller
    {
        T Deserialize<T>(string input);
        List<StartshipDTO> GetAll();
        StartshipDTO GetStarShipByName(string name);
    }
}
