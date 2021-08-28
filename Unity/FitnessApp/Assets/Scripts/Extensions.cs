using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FitnessApp.UICore;
using FitnessAppAPI;
using UnityEngine;
using Object = UnityEngine.Object;

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

        public static PerformanceType GetPerformanceTypeWithTagComponent(this GameObject target)
        {
            if(target == null) throw new ArgumentException("Argument was null", "target");
            var component = target.GetComponent<PerformanceTypeTag>();
            if(component == null)
                throw new Exception($"Given target didn't have a '{nameof(PerformanceTypeTag)}' component attached.");
            return component.GetPerformanceType();
        }

        public static bool IsDigit(this char target)
        {
            return target >= '0' && target <= '9';
        }

        public static int GetLastIndex<T>(this IEnumerable<T> collection)
        {
            int count = collection.Count();
            if (count == 0) return 0;
            return count - 1;
        }
    }
}