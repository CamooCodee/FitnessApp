using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
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
            
            if(context == null) Debug.LogError($"A '{typeof(T).Name}' is required somewhere!");
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

        public static int GetExerciseAmount(this WorkoutData target)
        {
            if(target == null) throw new Exception("Cannot get exercise amount on null object.");
            int amount = 0;
            
            foreach (var element in target.elements)
            {
                if (element is ExerciseData) amount++;
            }

            return amount;
        }
        
        public static int GetPauseAmount(this WorkoutData target)
        {
            if(target == null) throw new Exception("Cannot get pause amount on null object.");
            int amount = 0;
            
            foreach (var element in target.elements)
            {
                if (element is PauseData) amount++;
            }

            return amount;
        }

        public static float RoundToTwoDecimals(this float f)
        {
            return (float) Math.Round(f * 100f) / 100f;
        }
        
        public static bool TryParseTime(string s, out int length)
        {
            length = 0;
            
            if (s == "") return true;

            if (!CanBeParsedIntoTime(s)) return false;

            var split = s.Split(':');
            if (split.Length != 2) return false;
            string minutesS = split[0];
            string secondsS = split[1];

            int seconds = 0;
            bool result = int.TryParse(minutesS, out var minutes);
            result = result && int.TryParse(secondsS, out seconds);

            if (!result) return false;

            length = minutes * 60 + seconds;
            return true;
        }
        
        public static void TryParseTime(string lengthS, out string s)
        {
            if(!int.TryParse(lengthS, out int length))
                throw new ArgumentException($"The length has to be parsable into an integer. '{lengthS}'", nameof(lengthS));
            
            ParseTime(length, out s);
        }
        
        public static void ParseTime(int length, out string s)
        {
            int minutes = (int) Mathf.Floor(length / 60f);
            int seconds = length % 60;

            var minutesS = minutes.ToString();
            if (minutes == 0) minutesS = "00";
            else if (minutes < 10) minutesS = minutesS.Insert(0, "0");
            
            var secondsS = seconds.ToString();
            if (seconds == 0) secondsS = "00";
            else if (seconds < 10) secondsS = secondsS.Insert(0, "0");
            
            
            s = $"{minutesS}:{secondsS}";
        }

        public static bool CanBeParsedIntoTime(string s)
        {
            var format = new Regex(@"^[0-9]{2}:[0-9]{2}");
            return format.IsMatch(s);
        }
    }
}