using UnityEngine;

namespace FitnessApp.Cheats
{
    public abstract class Cheat
    {
        public readonly KeyCode cheatKey;

        protected Cheat(KeyCode cheatKey)
        {
            this.cheatKey = cheatKey;
        }

        public abstract void Execute();
    }
}