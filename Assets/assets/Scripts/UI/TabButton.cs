using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public TabGroup tabGroup;
    public UnityEvent onTabSelected, onTabDeselected;

    void Start() {
        if (tabGroup.tabButtons.Count == 0)
            tabGroup.OntabSelected(this);

        tabGroup.SubcribeButton(this);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData) {
        tabGroup.OntabSelected(this);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) {
        tabGroup.OntabEnter(this);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData){
        tabGroup.OntabExit();
    }

    public void Select() {
        if (onTabSelected != null)
            onTabSelected.Invoke();
    }
    public void Deselect() {
        if (onTabDeselected != null)
            onTabDeselected.Invoke();
               
    }
}
