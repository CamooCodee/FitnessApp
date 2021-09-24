using System.Collections.Generic;
using FitnessApp.Domain;
using FitnessAppAPI;
using UnityEngine;

namespace FitnessApp.Cheats
{
    public class CheatsManager : FitnessAppMonoBehaviour
    {
        private static bool PressingCheatCode => Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift);

        [SerializeField] private MyFitnessDomain domain;
        [Space(15f)]
        [SerializeField] private bool exerciseCheat = false;

        private readonly List<Cheat> _cheats = new List<Cheat>();
        
        private void Awake()
        {
#if !UNITY_EDITOR
            Destroy(this);
            return;
#endif

            domain.Require(this);
            if(exerciseCheat) _cheats.Add(new ExerciseCheat(AppAPI, domain));
        }

        private void Update()
        {
            if(!PressingCheatCode) return;

            for (var i = 0; i < _cheats.Count; i++)
            {
                if(Input.GetKeyDown(_cheats[i].cheatKey)) _cheats[i].Execute();
            }
        }
    }
}