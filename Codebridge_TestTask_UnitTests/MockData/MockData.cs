using Codebridge_TestTask.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebridge_TestTask_UnitTests.MockData
{
    internal class ControllerMockData
    {
        public static List<DogDB> GetDogs()
        {
            return new List<DogDB>
            {
                new DogDB { Id = 1, Name = "Jason", Color = "orange", TailLength = 5, Weight = 4},
                new DogDB { Id = 1, Name = "Barney", Color = "white", TailLength = 3, Weight = 7},
                new DogDB { Id = 1, Name = "Baxter", Color = "black", TailLength = 2, Weight = 2},
                new DogDB { Id = 1, Name = "Bilbo", Color = "brown", TailLength = 4, Weight = 8}
            };
        }

        public static DogDB InitialDog()
        {
            return new DogDB { Id = 1, Name = "Doggy", Color = "orange", TailLength = 5, Weight = 4 };
        }

        public static DogPOST AddDog()
        {
            return new DogPOST { Name = "Doggy", Color = "orange", TailLength = 5, Weight = 4 };
        }

        public static DogPOST AddDogNegativeLength()
        {
            return new DogPOST { Name = "Doggy", Color = "orange", TailLength = -5, Weight = 4 };
        }

        public static DogPOST AddDogNegativeWeight()
        {
            return new DogPOST { Name = "Doggy", Color = "orange", TailLength = 5, Weight = -4 };
        }
    }
}
