using UnityEngine;
using UnityEngine.UI;

namespace FitnessApp.UICore
{
    public class KeepOutsideScreen : MonoBehaviour
    {
        [SerializeField] private Transform canvas;
        
        
        private void Start()
        {
            var rectT = GetComponent<RectTransform>();
            rectT.anchoredPosition = new Vector2(Screen.width / canvas.localScale.x, 0);
        }
    }
}