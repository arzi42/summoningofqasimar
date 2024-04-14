using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Sigil : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private Image _image;

    private Vector3 _startPosition;

    private Slot _currentSlot;
    
    protected void Awake()
    {
        _image = GetComponent<Image>();

        _startPosition = transform.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 vec = Camera.main.WorldToScreenPoint(transform.position);
        
        vec.x += eventData.delta.x;
        vec.y += eventData.delta.y;
        
        transform.position = Camera.main.ScreenToWorldPoint(vec);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.raycastTarget = true;

        if (_currentSlot == null)
        {
            transform.position = _startPosition;
            Sounds.Play("pick");
        }
        else transform.position = _currentSlot.transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.raycastTarget = false;
        _currentSlot = null;
        
        Sounds.Play("pick");
    }

    public void OnSlotted(Slot slot)
    {
        _currentSlot = slot;
    }

    public void Eject()
    {
        _currentSlot = null;
        transform.position = _startPosition;
    }
}
