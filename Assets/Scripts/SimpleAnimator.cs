using UnityEngine;

namespace DefaultNamespace
{
    public class SimpleAnimator : MonoBehaviour
    {
        [SerializeField] private float _frameTime = 1f;
        
        private SpriteAnimation _spriteAnimation;
        private SpriteRenderer _spriteRenderer;

        private float _timeElapsed;

        private int _frame;

        protected void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected void Update()
        {
            if (_spriteAnimation == null)
                return;

            _timeElapsed += Time.deltaTime;

            if (_timeElapsed > _frameTime)
            {
                _frameTime = _timeElapsed;

                _frame++;

                if (_frame >= _spriteAnimation.Frames.Count)
                    _frame = 0;

                _spriteRenderer.sprite = _spriteAnimation.Frames[_frame];

            }
        }

        public void SetAnimation(SpriteAnimation animation)
        {
            _spriteAnimation = animation;
        }
    }
}