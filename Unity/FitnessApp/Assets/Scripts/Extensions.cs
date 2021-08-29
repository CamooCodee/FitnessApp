using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using FitnessApp.UICore;
using FitnessAppAPI;
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
        
        public static T Clone<T>(this T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", nameof(source));
            }

            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null)) return default;

            using var stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, source);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(stream);
        }
    }
}