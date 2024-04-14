using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Sounds : MonoBehaviour
    {
        
        [SerializeField] private List<AudioClip> _sounds;

        private static Sounds _instance;

        private AudioSource _audio;

        protected void Awake()
        {
            _instance = this;
            
            _audio = GetComponent<AudioSource>();
        }

        public static void Play(string id)
        {
            _instance._audio.PlayOneShot(_instance._sounds.Find(c => c.name == id));
        }

        public void Button()
        {
            Play("button");
        }
    }
}