using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FitnessApp.UICore
{
    public class UIRebuilder : MonoBehaviour
    {
        public void RebuildUILayout(RectTransform layoutRoot)
        {
            if (layoutRoot == null)
            {
                Debug.LogWarning("The layout root for a rebuild cannot be null.");
                return;
            }
            StartCoroutine(RebuildCoroutine(layoutRoot));
        }

        IEnumerator RebuildCoroutine(RectTransform layoutRoot)
        {
            yield return null;

            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutRoot);
        }
    }
}