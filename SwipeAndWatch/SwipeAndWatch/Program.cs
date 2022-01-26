using System.Linq;
using Bogus;
using MongoDB.Driver;
using SwipeAndWatch.Domain;
using SwipeAndWatch.Infastructure;
using System;
using System.Collections.Generic;

namespace SwipeAndWatch
{
    class Program
    {
        static void Main(string[] args)
        {
            //mongodb://127.0.0.1:27017
            var swipeAndWatchDb = new SwipeAndWatchDatabase("127.0.0.1", "SwipeAndWatch");
            swipeAndWatchDb.SeedDatabase();
        }

        
    }
}
