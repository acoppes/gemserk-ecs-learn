using UnityEngine;

namespace Gemserk.Ecs.Models
{
    public class UnitySpriteAnimator : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
    
        private float _speed = 1.0f;
    
        private UnitAnimationDefinition _currentAnimation;
        private int _currentFrame;

        private float _currentTime;

        private const float _frameTime = 1.0f / 30.0f;

        private bool _isPlaying;
        private bool _loop;

        public float GetDuration(UnitAnimationDefinition animation, float speed)
        {
            return animation.frames.Length * _frameTime / speed;
        }

        public void Play(UnitAnimationDefinition animation, float speed, bool loop)
        {
            // already playing animation
            if (_currentAnimation == animation && _isPlaying)
                return;

            _loop = loop;
            _speed = speed;
            _currentFrame = 0;
            // _currentAnimation = _animations.Find(a => a.name.Equals(name));
            _currentAnimation = animation;
            _spriteRenderer.sprite = _currentAnimation.frames[_currentFrame];
            _isPlaying = true;
            _currentTime = 0;
        }

        private void LateUpdate()
        {
            if (_currentAnimation == null || !_isPlaying)
                return;

            // if playing...
            _currentTime += Time.deltaTime * _speed;

            while (_currentTime >= _frameTime && _isPlaying)
            {
                _currentTime -= _frameTime;

                _currentFrame++;
            
                if (!_loop && _currentFrame >= _currentAnimation.frames.Length)
                {
                    _currentFrame = _currentAnimation.frames.Length - 1;
                    _isPlaying = false;               
                }
                else
                {
                    _currentFrame = _currentFrame % _currentAnimation.frames.Length;
                }
            
                _spriteRenderer.sprite = _currentAnimation.frames[_currentFrame];
                // Debug.LogFormat("updating sprite: {0}", _spriteRenderer.sprite.name);        
            }
        }

        /*
    public void SetFrame(int frame)
    {
        frame = Mathf.Clamp(frame, 0, _currentAnimation.frames.Length - 1);
        _currentFrame = frame;
        _spriteRenderer.sprite = _currentAnimation.frames[_currentFrame];
    }
    */
    }
}