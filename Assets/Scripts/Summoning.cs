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

        [SerializeField] private GameObject _win;
        
        private static Summoning _instance;

        protected void Awake()
        {
            _instance = this;
        }
        
        public static void Summon(Demon demon, System.Action onCompleted)
        {
            _instance._demonName.text = demon.Name;
            
            _instance._demonSprite.GetComponent<SimpleAnimator>().SetAnimation(demon.Animation);
            
            _instance.StartCoroutine(_instance.Ritual(onCompleted));
        }
        
        public static void Win()
        {
            _instance.StartCoroutine(_instance.WinRoutine());
        }

        public static void Dismiss()
        {
            _instance._room.SetActive(false);
            
            Music.FadeIn(2f);
        }
        
        private IEnumerator WinRoutine()
        {
            _room.SetActive(true);
            
            _demonSprite.color = Color.clear;
            
            Music.FadeOut(1f);

            yield return new WaitForSeconds(0.5f);
            
            _audio.Play();

            yield return new WaitForSeconds(1f);
            
            _summonParticles.Play(true);

            yield return new WaitForSeconds(8f);
            
            _win.SetActive(true);
        }

        private IEnumerator Ritual(System.Action onCompleted)
        {
            _room.SetActive(true);
            
            _demonSprite.color = Color.clear;
            
            Music.FadeOut(1f);

            yield return new WaitForSeconds(0.5f);
            
            _audio.Play();

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
            
            yield return new WaitForSeconds(4f);
            
            _demonName.gameObject.SetActive(false);
            
            onCompleted();
        }
    }
}