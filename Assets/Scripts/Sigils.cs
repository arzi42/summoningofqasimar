using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Sigils", menuName = "Sigils", order = 0)]
    
    public class Sigils : ScriptableObject
    {
        [SerializeField] private List<Sprite> _sigils;

        public Sprite Get(string name)
        {
            return _sigils.Find(s => s.name == name);
        }
    }
}