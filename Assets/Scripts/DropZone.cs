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
            if (temp[i].isCardOnBack == false)
            {
                Debug.Log(temp[i].isCardOnBack);
                tempHealth += temp[i].cardSO.cardHealth;
            }
        }
        return tempHealth;
    }

    public int GetPower()
    {
        int tempPower = 0;
        Card[] temp = transform.GetComponentsInChildren<Card>();
        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].isCardOnBack == false)
            {
                tempPower += temp[i].cardSO.cardDamage;
            }
        }
        return tempPower;
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<Card>().oldPosition = this.transform.localPosition;
        eventData.pointerDrag.gameObject.transform.localScale = new Vector2(1,1);

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
