using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FitnessApp
{
    public static class Extensions
    {
        public static bool IsNullOrWhitespace(this string s)
        {
            return String.IsNullOrWhiteSpace(s);
        }

        public static bool IsValidIndexFor<T>(this int index, IEnumerable<T> collection)
        {
            return index >= 0 && index < collection.Count();
        }

        public static bool Require<T>(this T required, UnityEngine.Object context = null, bool logError = true)
        {
            if(required != null) return true;
            if (!logError) return false;
            
            if(context == null) Debug.LogError($"A '{typeof(T).Name} is required somewhere!");
            else Debug.LogError($"A '{typeof(T).Name}' is required on '{context.name}'!", context);
            return false;
        }
    }
}