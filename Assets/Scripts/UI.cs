using System.Collections;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _hintText;
        
        private static UI _instance;

        protected void Awake()
        {
            _instance = this;
        }
        
        public static void ShowHint(string hint)
        {
            _instance.StartCoroutine(_instance.AnimateHint(hint));
        }

        private IEnumerator AnimateHint(string hint)
        {
            _hintText.gameObject.SetActive(true);

            _hintText.text = hint;

            yield return new WaitForSeconds(4f);
            
            _hintText.gameObject.SetActive(false);
        }
    }
}