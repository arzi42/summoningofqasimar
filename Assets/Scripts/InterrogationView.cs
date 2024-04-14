using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class InterrogationView : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _buttons;

        public void SetButtonTexts(List<string> texts)
        {
            for (int i = 0; i < texts.Count; i++)
            {
                Debug.Log(texts[i]);
                
                _buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = texts[i];
            }
        }

        public void OnButtonClicked(int i)
        {
            GameController.OnAnswer(i);
            
            gameObject.SetActive(false);
        }
    }
}