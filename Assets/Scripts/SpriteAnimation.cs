using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Sprite Animation", menuName = "Sprite Animation", order = 0)]
    public class SpriteAnimation : ScriptableObject
    {
        public List<Sprite> Frames;
    }
}