using System;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class ClickToAdvance : MonoBehaviour
    {
        [SerializeField] private GameObject _nextObject;

        [SerializeField] private UnityEvent _onAdvance;

        [SerializeField] private bool _hideThis = true;

        protected void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(_hideThis)
                    gameObject.SetActive(false);
                
                _onAdvance.Invoke();
                
                if(_nextObject != null)
                    _nextObject.SetActive(true);
            }
        }
    }
}