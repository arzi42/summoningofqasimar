using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class Book : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _pages;
        [SerializeField] private List<GameObject> _additionalPages;
        [SerializeField] private DemonPage _demonPage;

        [SerializeField] private UnityEvent _onOpen;
        [SerializeField] private UnityEvent _onClose;

        private Animator _animator;
        
        private int _currentPage;

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void AddPage()
        {
            var page = _additionalPages[0];
            
            _additionalPages.RemoveAt(0);
            
            _pages.Add(page);
        }
        

        public void Open()
        {
            _currentPage = 0;
            
            _pages[_currentPage].SetActive(true);
            
            _animator.SetTrigger("Open");
            
            _onOpen.Invoke();
        }
        
        
        
        public void NextPage()
        {
            Sounds.Play("page");
            
            _currentPage++;
            
            if (_currentPage >= _pages.Count)
            {
                int over = _currentPage - _pages.Count;

                if (Grimoire.Count > over)
                {
                    _pages[^1].SetActive(false);
                    
                    _demonPage.gameObject.SetActive(true);
                    
                    _demonPage.SetDemon(Grimoire.Get(over));
                }
                else
                {
                    _demonPage.gameObject.SetActive(false);
                    
                    _pages[^1].SetActive(false);
                    
                    _currentPage = 0;
                
                    _animator.SetTrigger("Close");
                
                    _onClose.Invoke();
                }
                
                
            }
            else
            {
                _pages[_currentPage-1].SetActive(false);
                _pages[_currentPage].SetActive(true);
            }
        }
        
    }
}