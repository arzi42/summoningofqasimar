using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class DemonPage : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private Image _sigil;
        [SerializeField] private TextMeshProUGUI _slotText;
        
        public void SetDemon(Demon demon)
        {
            _name.text = demon.Name;
            _sigil.sprite = GameController.Sigils.Get(demon.Sigil);
            _slotText.text = demon.SlotText;
        }
    }
}