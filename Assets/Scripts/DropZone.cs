using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class DropZone : MonoBehaviour,IDropHandler,IGetPower
{
    protected Card[] cardPool;
    [SerializeField] protected TMP_Text fieldPowerText;

    //Old Function
    // public int GetHealth()
    // {
    //     int tempHealth = 0;
    //     Card[] temp = transform.GetComponentsInChildren<Card>();
    //     for (int i = 0; i < temp.Length; i++)
    //     {
    //             tempHealth += temp[i].cardSO.cardHealth;
    //     }
    //     return tempHealth;
    // }

    private void Start() {
        cardPool = FetCardPool();
        UpdatePowerText();
    }

    //For fetch all card power
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

    //When you drop card to field
    public virtual void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.GetComponent<Card>().currentValue <= GameManager.Instance.playerSupply)
        {
            eventData.pointerDrag.GetComponent<Card>().oldPosition = this.transform.localPosition;
            eventData.pointerDrag.gameObject.transform.localScale = new Vector2(1,1);
            eventData.pointerDrag.GetComponent<Card>().transform.SetParent(this.transform);
            GameManager.Instance.currentPlayerSupply -= eventData.pointerDrag.GetComponent<Card>().currentValue;
            GameManager.Instance.UpdatePlayerSupply();
            UpdatePowerText(); 
        }
        
    }

    //For Fetch when you card destroy
    public Card[] CardDestroyPool(ref Card[] _cardlist,int _power){
        List<Card> tempDestroyCardList = new List<Card>();
        foreach (Card card in _cardlist)
        {
            if (card.currentDefense < _power)
            {
                tempDestroyCardList.Add(card);
            }
        }
        return tempDestroyCardList.ToArray<Card>();
    }

    //Fetch Card Pool
    public Card[] FetCardPool(){
        return this.GetComponentsInChildren<Card>();
    }
    public void UpdatePowerText(){
        fieldPowerText.text = GetPower().ToString();
    }

}
