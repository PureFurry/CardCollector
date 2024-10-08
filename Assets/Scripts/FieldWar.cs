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
            if (this.GetComponentsInChildren<Card>() != null)
            {
                // Array.Clear(cardPool,0,cardPool.Length);
            cardPool = FetchCardPool();
            foreach (Card item in cardPool)
            {
                Debug.Log(item.cardSO.cardName);
            }
            Card[] destroyCard = CardDestroyPool(ref cardPool,_damage);
            Debug.Log(destroyCard);
            Destroy(destroyCard[UnityEngine.Random.Range(0,destroyCard.Length)].gameObject);
            Debug.Log("Damage İçinde");
            this.GetPower();
            this.UpdatePowerText();
            }
            else this.fieldHealth -= _damage;
        }
        else if (this.fieldHealth <= 0)
        {
            this.enabled = false;
        }
        
    }
    private void Update() {
        UpdatePowerText();
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
        
        // Vurma şansı hesaplanıyor
        int hitChance = UnityEngine.Random.Range(1, 12);

        // Vuruş hesaplaması yapılıyor
        if (hitChance + GameManager.Instance.globalWeath > missChance)
        {
            // Child kartların sayısını kontrol ediyoruz
            Card[] rivalCards = rivalField.GetComponentsInChildren<Card>();
            if (rivalCards.Length > 0)
            {
                // FieldWar component'ini alıyoruz ve null olmadığından emin oluyoruz
                FieldWar fieldWar = rivalField.GetComponent<FieldWar>();
                if (fieldWar != null)
                {
                    fieldWar.TakeDamage(GetPower());
                    Debug.Log("Action performed successfully");
                    UpdatePowerText();
                }
                else
                {
                    Debug.LogWarning("FieldWar component not found on rivalField");
                }
            }
            else
            {
                this.fieldHealth -= GetPower();
                Debug.Log("No cards found in rivalField");
            }
        }
        else
        {
            Debug.Log("Missed the target");
        }
        
        Debug.Log("Turn Action ended");
    }
    catch (Exception ex)
    {
        Debug.LogError($"Error in TurnAction: {ex.Message}");
    }
    }

}
