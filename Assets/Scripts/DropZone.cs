using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class DropZone : MonoBehaviour,IDropHandler,IGetPower
{
    [SerializeField] protected TMP_Text fieldPowerText;
    protected Card[] cardPool;

    private void Start()
    {
        UpdatePowerText();
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        Card droppedCard = eventData.pointerDrag.GetComponent<Card>();
        if (droppedCard != null && droppedCard.currentValue <= GameManager.Instance.playerSupply && GameManager.Instance.currentPlayerSupply > 0)
        {
            droppedCard.transform.SetParent(transform);
            droppedCard.transform.DOMove(transform.position, 0.2f).OnComplete(()=> droppedCard.transform.DOScale(new Vector2(droppedCard.transform.localScale.x + 1,droppedCard.transform.localScale.y + 1),0.2f).OnComplete(()=> droppedCard.transform.DOScale(new Vector2(droppedCard.transform.localScale.x - 1,droppedCard.transform.localScale.y - 1),0.2f)));
            GameManager.Instance.currentPlayerSupply -= droppedCard.currentValue;
            GameManager.Instance.UpdatePlayerSupply();
            UpdatePowerText();
        }
        else{
            GameObject notEnoughSupplyText = Instantiate(Resources.Load<GameObject>("Resources/NotEnoughSupplyText.prefab"),droppedCard.transform.position,Quaternion.identity);
            Destroy(notEnoughSupplyText,1f);
        }
    }

    public int GetPower()
    {
        int totalPower = 0;
        cardPool = GetComponentsInChildren<Card>();
        if (cardPool != null)
        {
            foreach (var card in cardPool)
            {
                totalPower += card.cardSO.cardDamage;
            }
        }
        Debug.Log(totalPower);
        return totalPower;
    }

    public void UpdatePowerText()
    {
        Debug.Log("Text Update");
        fieldPowerText.text = GetPower().ToString();
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
    public Card[] FetchCardPool(){
        return this.GetComponentsInChildren<Card>();
    }
}
