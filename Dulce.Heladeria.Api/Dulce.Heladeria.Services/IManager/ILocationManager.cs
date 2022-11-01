using Dulce.Heladeria.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dulce.Heladeria.Services.IManager
{
    public interface ILocationManager
    {
        //Task<List<GetLocationDto>> GetAllLocations(); 
        Task<bool> InsertLocation(LocationDto location);
    }
}
