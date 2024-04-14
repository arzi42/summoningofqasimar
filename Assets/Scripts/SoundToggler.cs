using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class SoundToggler : MonoBehaviour
    {
        
        [SerializeField] private List<Sprite> _sprites;
    
        private bool on = true;
        
        public void Toggle()
        {
            on = !on;

            AudioListener.pause = !on;

            GetComponent<Image>().sprite = _sprites[on ? 1 : 0];
        }
    }
}