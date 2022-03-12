using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DroneWebApi.Data
{
    public static class Model
    {
        public static readonly string Lightweight = "Lightweight";
        public static readonly string Middleweight = "Middleweight";
        public static readonly string Cruiserweight = "Cruiserweight";
        public static readonly string Heavyweight = "Heavyweight";
    }

    public static class State
    {
        public static readonly string IDLE = "IDLE";
        public static readonly string LOADING = "LOADING";
        public static readonly string LOADED = "LOADED";
        public static readonly string DELIVERING = "DELIVERING";
        public static readonly string DELIVERED = "DELIVERED";
        public static readonly string RETURNING = "RETURNING";

    }
}
