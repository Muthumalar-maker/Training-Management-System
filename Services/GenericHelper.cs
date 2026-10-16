
using System;
using System.Collections.Generic;

namespace TrainingManagementSystem.Services
{
    public class GenericHelper
    {
        public void DisplayItems<T>(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                Console.WriteLine(item);
            }
        }
    }
}
