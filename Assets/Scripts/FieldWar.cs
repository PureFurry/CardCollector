using System;
using UnityEngine;

public class FieldWar : DropZone,ITakeDamage
{
    public int fieldHealth;
    [SerializeField]GameObject rivalField;
    public int missChance;
    public void TakeDamage(int _damage)
    {
        if (fieldHealth > 0)
        {
            // Array.Clear(cardPool,0,cardPool.Length);
            cardPool = FetCardPool();
            foreach (Card item in cardPool)
            {
                Debug.Log(item.cardSO.cardName);
            }
            Card[] destroyCard = CardDestroyPool(ref cardPool,_damage);
            Debug.Log(destroyCard);
            Destroy(destroyCard[UnityEngine.Random.Range(0,destroyCard.Length)].gameObject);
        }
        else if (this.fieldHealth <= 0)
        {
            
        }
        this.fieldHealth -= _damage;
    }
    private void OnEnable() {
        SuscribeTurnAction();
    }
    private void OnDisable() {
        UnSuscribeTurnAction();
    }
    void SuscribeTurnAction(){
        GameManager.OnEndTurn += TurnAction;
    }
    void UnSuscribeTurnAction(){
        GameManager.OnEndTurn -= TurnAction;
    }
    void TurnAction()
    {
        try
        {
            Debug.Log("Turn Action started");
            int hitChance = UnityEngine.Random.Range(1,12);
            if (hitChance + GameManager.Instance.globalWeath > missChance)
            {
                rivalField.GetComponent<FieldWar>().TakeDamage(GetPower());
            }
            else
            {
                Debug.Log("Missed");
            }
            Debug.Log("Turn Action ended");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error in TurnAction: {ex.Message}");
        }
    }

}
