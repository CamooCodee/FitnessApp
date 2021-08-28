using UnityEngine;
using UnityEngine.UI;

namespace FitnessApp.UICore
{
    public class RuntimeElementButtonReference : MonoBehaviour
    {
        [SerializeField] private Button button;

        private void Awake()
        {
            var result = button.Require(this);
            if(!result) Destroy(this);
        }

        public Button Button => button;
    }

}