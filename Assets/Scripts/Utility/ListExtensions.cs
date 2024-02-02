using System;
using System.Collections.Generic;

namespace Utility
{
    public static class ListExtensions
    {
        private static readonly Random Rng = new();  

        public static void RandomSort<T>(this IList<T> list)  
        {  
            int n = list.Count;  
            for(int i = 0; i < n; i++) {  
                int k = Rng.Next(n);  
                (list[k], list[i]) = (list[i], list[k]);
            }  
        }
        
        public static T RandomPick<T>(this IList<T> list)  
        {  
            int n = list.Count;  
            int k = Rng.Next(0, n);  
            return list[k];
        }
    }
}