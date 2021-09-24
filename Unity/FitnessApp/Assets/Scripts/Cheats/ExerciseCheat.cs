using System.Collections.Generic;
using FitnessApp.Domain;
using FitnessAppAPI;
using UnityEngine;

namespace FitnessApp.Cheats
{
    public class ExerciseCheat : Cheat
    {
        private readonly MyFitnessDomain _domain;
        private readonly FitnessApiFacade _api;
        private readonly PerformanceComponentArgs[] _componentSet;
        private readonly string[] _nameSet;
        
        public ExerciseCheat(FitnessApiFacade api, MyFitnessDomain domain) : base(KeyCode.E)
        {
            _api = api;
            _domain = domain;
            _componentSet = new PerformanceComponentArgs[]
            {
                new WeightComponentArgs(Random.Range(1, 1000), "kg"),
                new RepsComponentArgs(Random.Range(1, 40)),
                new TimeComponentArgs(Random.Range(1, 120))
            };

            _nameSet = new[]
            {
                "Push Up",
                "Sit Up",
                "Bench Press",
                "Lat Pull Down",
                "Plank",
                "Bicep Curls"
            };
        }

        public override void Execute()
        {
            for (int i = 0; i < 10; i++)
            {
                _api.CreateNewExercise(GetRandomName(), GetRandomComponentArgs());
            }

            _domain.PerformSingleAction();
        }

        string GetRandomName()
        {
            return _nameSet[Random.Range(0, _nameSet.Length)];
        }
        PerformanceArgs GetRandomComponentArgs()
        {
            var args = new PerformanceArgs();
            float rand = Random.Range(0f, 1f);
            float probability = 0.9f;
            var alreadyAddedComponents = new List<int>();
            
            while (args.Count < 3 && rand < probability)
            {
                probability -= 0.2f;
                rand = Random.Range(0f, 1f);

                var index = Random.Range(0, _componentSet.Length);
                if(alreadyAddedComponents.Contains(index))
                    continue;
                
                alreadyAddedComponents.Add(index);
                args.AddArgs(_componentSet[index]);
            }

            return args;
        }
    }
}