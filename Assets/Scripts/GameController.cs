using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace DefaultNamespace
{
    public struct InterrogationText
    {
        public string Sigil;
        public string Slot;
        public string Text;
    }
    
    
    public class GameController : MonoBehaviour
    {
        [SerializeField] private InterrogationView _interrogationView;
        [SerializeField] private TextMeshProUGUI _answerText;
        [SerializeField] private GameObject _winScreen;
        [SerializeField] private GameObject _ritual;
        [SerializeField] private Sigils _sigils;
        [SerializeField] private Book _book;
        
        private static GameController _instance;

        private Demon _currentDemon;

        private List<InterrogationText> _currentTexts;
        
        private Slot[] _slots;

        private bool _isNewDemon;

        public static Sigils Sigils => _instance._sigils;
        

        private static Dictionary<string, List<string>> _crypticAnswers = new Dictionary<string, List<string>>
        {
            {
                "salt", new List<string> { "with the holy" }
            },
            {
                "silver", new List<string> { "with the leader of all" }
            },
            {
                "sand", new List<string> { "in the desert of set" }
            },
            {
                "stone", new List<string> { "with those who can not speak" }
            },
            {
                "soil", new List<string> { "in the ground with the mourned" }
            }
        };
        
        private static Dictionary<string, List<string>> _crypticQuestions = new Dictionary<string, List<string>>
        {
            {
                "sin", new List<string> { "where do you sin?" }
            },
            {
                "see", new List<string> { "who sees you?" }
            },
            {
                "drink", new List<string> { "what do you drink?" }
            },
            {
                "laugh", new List<string> { "who do you share laughter with?" }
            },
            {
                "birth", new List<string> { "where were you born?" }
            },
            {
                "walk", new List<string> { "where do you walk?" }
            },
            {
                "weep", new List<string> { "where are your tears?" }
            },
            {
                "die", new List<string> { "where do you die?" }
            },
            {
                "hear", new List<string> { "what do you hear?" }
            },
        };

        private Dictionary<string, string> _correctSigils = new Dictionary<string, string>
        {
            { "sin", "salt" },
            { "see", "silver" },
            { "drink", "sand" },
            { "laugh", "stone" },
            { "birth", "soil" }
        };

        protected void Awake()
        {
            _instance = this;
            
            _slots = FindObjectsByType<Slot>(FindObjectsSortMode.None);
            
            Debug.Log($"Got {_slots.Length} slots");
        }
        
        public void DoRitual()
        {
            var strings = new List<string>();

            bool isCorrect = true;

            _currentTexts = new List<InterrogationText>();

            bool _oldDemon = false;

            foreach (var slot in _slots)
            {
                if (slot.Sigil == null)
                {
                    UI.ShowHint("Place ALL sigils before the ritual");
                    return;
                }

                var text = _crypticQuestions[slot.Sigil.name][0];

                if (Grimoire.HasWithSigilAndSlot(slot.Sigil.name, slot.name))
                {
                    _currentDemon = Grimoire.GetWithSigilAndSlot(slot.Sigil.name, slot.name);
                    _oldDemon = true;
                }
                
                strings.Add(text);
                
                _currentTexts.Add(new InterrogationText { Sigil = slot.Sigil.name, Slot = slot.name, Text = text });

                Debug.Log($"{slot.Sigil.name} != {slot.name}");

                if (!_correctSigils.ContainsKey(slot.Sigil.name) || _correctSigils[slot.Sigil.name] != slot.name)
                {
                    Debug.Log($"{slot.Sigil.name} != {slot.name}");
                    isCorrect = false;
                }
            }
            
            _interrogationView.SetButtonTexts(strings);
            
            _ritual.SetActive(false);

            if (!_oldDemon)
            {
                _currentDemon = new Demon();
                _currentDemon.Name = DemonNameGenerator.Generate();
            }
            
            Summoning.Summon(_currentDemon.Name, isCorrect ? Win : Interrogate);
        }

        private void Win()
        {
            _winScreen.SetActive(true);
        }

        private void Interrogate()
        {
            _interrogationView.gameObject.SetActive(true);
        }

        public static void OnAnswer(int i)
        {
            var tmp = _instance._answerText;
            
            tmp.gameObject.SetActive(true);
            tmp.text = _crypticAnswers[_instance._slots[i].name][0];
            
            _instance.Invoke("HideAnswer", 2f);

            if (!Grimoire.Has(_instance._currentDemon.Name))
            {
                _instance._currentTexts.RemoveAt(i);

                var text = _instance._currentTexts[Random.Range(0, _instance._currentTexts.Count)];

                _instance._currentDemon.Sigil = text.Sigil;
                _instance._currentDemon.SlotText = $"I {text.Sigil} {_crypticAnswers[text.Slot][0]}";
                _instance._currentDemon.Slot = text.Slot;
            
                Grimoire.Add(_instance._currentDemon);
                
                UI.ShowHint($"{_instance._currentDemon.Name} added to the grimoire");

                if (Grimoire.Count == 3)
                {
                    _instance._book.AddPage();
                }
            }
            else
            {
                _instance.RevealSlot(_instance._currentDemon);
            }

            foreach (var slot in _instance._slots)
            {
                slot.EjectSigil();
            }
            
        }

        private void RevealSlot(Demon demon)
        {
            foreach (var slot in _slots)
            {
                if (slot.name == demon.Slot)
                {
                    slot.Reveal();
                    return;
                }
                        
            }

        }

        private void HideAnswer()
        {
            _answerText.gameObject.SetActive(false);
            _ritual.SetActive(true);
        }
        
    }
}