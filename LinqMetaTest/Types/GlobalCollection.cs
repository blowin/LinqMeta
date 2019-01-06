using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqMetaTest.Types
{
    public static class GlobalCollection
    {
        public static int[] Arr = {1, 1, 1, 1, 1, 2, 8, 3, -2, 4, 1, 6, -2, -2, 4, 8, 8, 9, 10, 6, -2, -3, 1};

        public static string[] Cars = { "Alfa Romeo", "Aston Martin", "Audi", "Nissan", "Chevrolet",  "Chrysler", "Dodge", "BMW", 
            "Ferrari",  "Bentley", "Ford", "Lexus", "Mercedes", "Toyota", "Volvo", "Subaru" };

        public static string[] CarsFirst = Cars.Take(5).ToArray();
        public static string[] CarsSecond = Cars.Skip(4).ToArray();
        
        public static Stack<int> Stack = new Stack<int>(Enumerable.Range(0, 10));
        
        public static Dictionary<int, string> Dictionary = Enumerable
            .Repeat("Dima", 10)
            .Select((s, i) => new {Key = i, Name = s + i})
            .ToDictionary(arg => arg.Key, arg => arg.Name);
        
        public static object[] HeterogeneousArray = {"Test", 12, "Home", "Hello", 2.222, 12u, new object(), new Random(), };

    }
}