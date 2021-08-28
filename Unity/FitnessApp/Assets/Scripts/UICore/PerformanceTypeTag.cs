using FitnessAppAPI;
using UnityEngine;

namespace FitnessApp.UICore
{
    public class PerformanceTypeTag : MonoBehaviour
    {
        [SerializeField] private PerformanceType performanceTag;

        public PerformanceType GetPerformanceType()
        {
            return performanceTag;
        }
    }

}