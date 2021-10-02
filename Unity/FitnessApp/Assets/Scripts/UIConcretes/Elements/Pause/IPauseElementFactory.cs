using FitnessAppAPI;
using UnityEngine;

namespace UIConcretes.Elements.Pause
{
    public interface IPauseElementFactory
    {
        public PauseElement InstantiateElement(PauseData data, Transform elementContainer);
    }
}