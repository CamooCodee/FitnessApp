using UnityEngine;

namespace DefaultNamespace
{
    [DefaultExecutionOrder(-1)]
    public class ParentInitializer : MonoBehaviour
    {
        [SerializeField] private int preferredSiblingIndex = 0;
    
        private void Awake()
        {
            transform.SetParent(transform.parent);
            transform.SetSiblingIndex(preferredSiblingIndex);
        }
    }
}