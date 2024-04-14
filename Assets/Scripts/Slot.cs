using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sigil Sigil => _currentSigil;
    
    private Sigil _currentSigil;
    
    public void OnDrop(PointerEventData eventData)
    {
        var sigil = eventData.pointerDrag.GetComponent<Sigil>();
        
        sigil.OnSlotted(this);
        
        SetAlpha(0.3f);

        _currentSigil = sigil;
    }

    private void SetAlpha(float a)
    {
        var image = GetComponent<Image>();

        var colour = image.color;
        colour.a = a;
        image.color = colour;
    }

    public void RemoveSigil()
    {
        SetAlpha(1);
        _currentSigil = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void EjectSigil()
    {
        if (Sigil != null)
        {
            Sigil.Eject();
            RemoveSigil();
        }
    }

    public void Reveal()
    {
        var tmp = GetComponentInChildren<TextMeshProUGUI>(true);

        tmp.text = name.ToUpper();
        
        tmp.gameObject.SetActive(true);
    }
}
