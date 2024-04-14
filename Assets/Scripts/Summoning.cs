using System.Collections;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class Summoning : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _demonName;
        [SerializeField] private SpriteRenderer _demonSprite;
        [SerializeField] private GameObject _room;
        [SerializeField] private ParticleSystem _summonParticles;
        [SerializeField] private AudioSource _audio;
        
        private static Summoning _instance;

        protected void Awake()
        {
            _instance = this;
        }
        
        public static void Summon(string demonName, System.Action onCompleted)
        {
            _instance._demonName.text = demonName;
            
            _instance.StartCoroutine(_instance.Ritual(onCompleted));
        }

        private IEnumerator Ritual(System.Action onCompleted)
        {
            _room.SetActive(true);
            
            _audio.Play();


            _demonSprite.color = Color.clear;

            yield return new WaitForSeconds(1f);
            
            _summonParticles.Play(true);

            yield return new WaitForSeconds(8f);

            for (float t = 0; t < 1f; t += Time.deltaTime / 3f)
            {
                _demonSprite.color = Color.Lerp(Color.clear, Color.white, t);
                yield return null;
            }

            _demonSprite.color = Color.white;
            
            _demonName.gameObject.SetActive(true);
            
            yield return new WaitForSeconds(2f);
            
            _demonName.gameObject.SetActive(false);
            
            _room.SetActive(false);

            onCompleted();
        }
    }
}