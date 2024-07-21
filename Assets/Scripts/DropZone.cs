using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour,IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<Card>().oldPosition = this.transform.localPosition;
        eventData.pointerDrag.GetComponent<Card>().transform.parent = this.transform;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
