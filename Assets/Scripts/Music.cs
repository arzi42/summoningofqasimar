using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class Music : MonoBehaviour
    {
        private static Music _instance;
        
        private AudioSource _audio;

        private float _volume;

        protected void Awake()
        {
            _audio = GetComponent<AudioSource>();
            
            _instance = this;

            _volume = _audio.volume;
        }

        public static void FadeIn(float duration)
        {
            _instance.StartCoroutine( _instance.RunFade(0, _instance._volume, duration));
        }
        
        public static void FadeOut(float duration)
        {
            _instance.StartCoroutine(_instance.RunFade(_instance._volume, 0, duration));
        }

        private IEnumerator RunFade(float start, float end, float duration)
        {
            for (float t = 0; t < 1f; t += Time.deltaTime / duration)
            {
                _audio.volume = Mathf.Lerp(start, end, t);
                yield return null;
            }
            
            _audio.volume = end;
        }
    }
}