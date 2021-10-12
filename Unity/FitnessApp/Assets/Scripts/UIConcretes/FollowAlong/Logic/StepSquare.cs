using UnityEngine;
using UnityEngine.UI;

namespace FitnessApp.UIConcretes.FollowAlong.Logic
{
    public class StepSquare : MonoBehaviour
    {
        private Image _border;
        private Color _borderOrigin;
        private Color _borderTarget;
        private Image _icon;
        private Color _iconOrigin;
        private Color _iconTarget;
        private Image _fill;
        private Color _fillOrigin;
        private Color _fillTarget;

        private bool _isLerping;
        private float _timeLeft;
        private float _maxTime = 0.35f;
        
        private void Awake()
        {
            _border = GetComponent<Image>();
            _icon = transform.GetChild(1).GetComponent<Image>();
            _fill = transform.GetChild(0).GetComponent<Image>();
        }

        void Update()
        {
            if(!_isLerping) return;

            if (_timeLeft > 0f)
            {
                var t = _timeLeft / _maxTime;
                _border.color = Color.Lerp(_borderTarget, _borderOrigin, t);
                _icon.color = Color.Lerp(_iconTarget, _iconOrigin, t);
                _fill.color = Color.Lerp(_fillTarget, _fillOrigin, t);
                _timeLeft -= Time.deltaTime;
            }
            else
            {
                _border.color = _borderTarget;
                _icon.color = _iconTarget;
                _fill.color = _fillTarget;
                _timeLeft = 0f;
                _isLerping = false;
            }
        }
        
        public void SetSquareColorByPrefab(GameObject stepSquarePrefab, float animLength)
        {
            GetColorsBySquareGameObject(stepSquarePrefab, out var border, out var icon, out var fill);
            LerpColor(border, icon, fill, animLength);
        }

        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }

        void LerpColor(Color border, Color icon, Color fill, float animLength)
        {
            _timeLeft = _maxTime = animLength;
            _isLerping = true;
            _borderTarget = border;
            _iconTarget = icon;
            _fillTarget = fill;
            _borderOrigin = _border.color;
            _iconOrigin = _icon.color;
            _fillOrigin = _fill.color;
        }

        void GetColorsBySquareGameObject(GameObject o, out Color border, out Color icon, out Color fill)
        {
            border = o.GetComponent<Image>().color;
            icon = o.transform.GetChild(1).GetComponent<Graphic>().color;
            fill = o.transform.GetChild(0).GetComponent<Image>().color;
        }
    }
}