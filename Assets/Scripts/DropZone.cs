using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour,IDropHandler,IGetPower,IGetHealth
{
    public int GetHealth()
    {
        int tempHealth = 0;
        Card[] temp = transform.GetComponentsInChildren<Card>();
        for (int i = 0; i < temp.Length; i++)
        {
            tempHealth += temp[i].cardSO.cardHealth;
        }
        return tempHealth;
    }

    public int GetPower()
    {
        int tempPower = 0;
        Card[] temp = transform.GetComponentsInChildren<Card>();
        for (int i = 0; i < temp.Length; i++)
        {
            tempPower += temp[i].cardSO.cardDamage;
        }
        return tempPower;
    }

    public virtual void OnDrop(PointerEventData eventData)
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
