using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Card : MonoBehaviour,IBeginDragHandler,IDropHandler,IDragHandler,IEndDragHandler
{
    public CardSO cardSO;
    public TMP_Text cardNameText;
    public TMP_Text cardValueText;
    public TMP_Text cardDamageText;
    public TMP_Text cardDefenseText;
    public Image cardImage;
    public Image cardRarityColor;
    public Vector2 oldPosition;
    public int currentDamage;
    public int currentDefense;
    public int currentValue;
    

    void Start()
    {
        currentDamage = cardSO.cardDamage;
        currentDefense = cardSO.cardDefense;
        currentValue = cardSO.cardValue;
        cardNameText.text = cardSO.cardName;
        cardDamageText.text = currentDamage.ToString();
        cardDefenseText.text = currentDefense.ToString();
        cardValueText.text = currentValue.ToString();
        cardImage.sprite = cardSO.cardImage;
        switch (cardSO.cardRarity)
        {
            case CardRarity.COMMON:
            cardRarityColor.color = new Color(255,255,255);
            break;
            case CardRarity.RARE:
            cardRarityColor.color = new Color(30,255,0);
            break;
            case CardRarity.VERYRARE:
            cardRarityColor.color = new Color(0,112,221);
            break;
            case CardRarity.EPIC:
            cardRarityColor.color = new Color(163,53,238); 
            break;
            case CardRarity.LEGENDARY:
            cardRarityColor.color = new Color(255,128,0);
            break;
        }
    }
    private void Update() {
        
    }
    public void UpdateCardData(){
        cardDamageText.text = currentDamage.ToString();
        cardDefenseText.text = currentDefense.ToString();
        cardValueText.text = currentValue.ToString();
    }
    public void LoadCardData(CardSO _loadedData){
        _loadedData = this.cardSO;
    }
    
    void ResetPosition(){
        transform.localPosition = oldPosition;
        this.transform.localScale = new Vector2(1,1);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        oldPosition = transform.localPosition;
        this.transform.localScale = new Vector2(2,2);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        this.transform.position = Input.mousePosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
        this.transform.localScale = new Vector2(1,1);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ResetPosition();
    }
}