using UnityEngine;
using UnityEngine.UI;

namespace FitnessApp.UICore.MovableElementList
{
    public class ScrollableMovableElementList : MovableElementList
    {
        [SerializeField] private ScrollRect scrollRect;
        private RectTransform _scrollRectTransform;
        [SerializeField] private float scrollTriggerUpOffset;
        [SerializeField] private float scrollTriggerDownOffset;
        [SerializeField] private float scrollDownSensitivity;
        [SerializeField] private float scrollUpSensitivity;
        private float _minScrollViewYPos;
        private float _maxScrollViewYPos;
        
        private float MinScrollViewYPos
        {
            get
            {
                if (_minScrollViewYPos != 0f) return _minScrollViewYPos;
                
                var rectCorners = new Vector3[4];
                _scrollRectTransform.GetWorldCorners(rectCorners);
                _minScrollViewYPos = rectCorners[0].y;
                return _minScrollViewYPos;
            }
        }
        private float MaxScrollViewYPos
        {
            get
            {
                if (_maxScrollViewYPos != 0f) return _maxScrollViewYPos;
                
                var rectCorners = new Vector3[4];
                _scrollRectTransform.GetWorldCorners(rectCorners);
                _maxScrollViewYPos = rectCorners[1].y;
                return _maxScrollViewYPos;
            }
        }

        private void OnValidate()
        {
            scrollDownSensitivity = Mathf.Max(scrollDownSensitivity, 0f);
            scrollUpSensitivity = Mathf.Max(scrollUpSensitivity, 0f);
        }

        protected override void Setup()
        {
            base.Setup();
            scrollRect.Require(this);
            _scrollRectTransform = scrollRect.GetComponent<RectTransform>();
        }

        protected override void OnMove()
        {
            base.OnMove();
            var yVel = GetScrollRectYVel();
            if(yVel != 0f)
                scrollRect.velocity = new Vector2(scrollRect.velocity.x, yVel);
        }

        protected override int FindClosestSiblingIndexTo(float yPos)
        {
            float minDistance = float.MaxValue;
            int closestSibling = 0;
            
            for (int i = 0; i < transform.childCount; i++)
            {
                var childYPos = GetChildYPos(i);

                if (childYPos > MaxScrollViewYPos || childYPos < MinScrollViewYPos) continue;
                
                var childDistance = Mathf.Abs(yPos - childYPos);
                if (childDistance > minDistance) continue;
                
                minDistance = childDistance;
                closestSibling = i;
            }

            return closestSibling;
        }
        
        float GetScrollRectYVel()
        {
            if (currentElement == null) return 0f;
            
            var childCount = transform.childCount;
            if (childCount <= 0) return 0f;

            var yMinOrigin = MinScrollViewYPos - scrollTriggerDownOffset;
            var yMaxOrigin = MaxScrollViewYPos + scrollTriggerUpOffset;
            
            if (currentElement.YPos < yMinOrigin)
                return GetScrollAmount(yMinOrigin, scrollDownSensitivity);
            if (currentElement.YPos > yMaxOrigin)
                return -GetScrollAmount(yMaxOrigin, scrollUpSensitivity);

            return 0f;
        }

        float GetScrollAmount(float yOrigin, float sensitivity)
        {
            return sensitivity * Mathf.Abs(yOrigin - currentElement.YPos);
        }
    }
}