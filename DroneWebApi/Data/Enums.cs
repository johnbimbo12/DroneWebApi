using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneWebApi.Data
{
    public class Enums
    {
        public enum Model
        {
            Lightweight,
            Middleweight,
            Cruiserweight,
            Heavyweight
        }

        public enum State
        {
            IDLE,
            LOADING,
            LOADED,
            DELIVERING,
            DELIVERED,
            RETURNING
        }
    }
}
