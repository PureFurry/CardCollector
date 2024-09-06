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
    public Image cardImage;
    public Image cardRarityColor;
    public Image cardTypeImage;
    public Vector2 oldPosition;
    [SerializeField]public Image cardBack;
    public bool isCardOnBack, IsAttacked;
    [SerializeField]public Sprite fireIcon;
    [SerializeField]public Sprite rockIcon;
    [SerializeField]public Sprite waterIcon;
    [SerializeField]public Sprite electricIcon;
    [SerializeField]public Sprite darkIcon;
    [SerializeField]public Sprite lightIcon;
    public int currentAttack;
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
    public void AttackModifier(int modifierAmount){
        if (modifierAmount < 0)
        {
            currentAttack -= modifierAmount;
            cardDamageText.text = currentAttack.ToString();
        }
        else{
            currentAttack += modifierAmount;
            cardDamageText.text = currentAttack.ToString();
        }
    }

    void Start()
    {
        IsAttacked = false;
        currentAttack = cardSO.cardDamage;
        cardNameText.text = cardSO.cardName;
        cardValueText.text = cardSO.cardValue.ToString();
        cardImage.sprite = cardSO.cardImage;
        cardDamageText.text = currentAttack.ToString();
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
        switch (cardSO.cardType)
        {
            case CardType.FIRE:
            cardTypeImage.sprite = fireIcon;
            break;
            case CardType.ROCK:
            cardTypeImage.sprite = rockIcon;
            break;
            case CardType.WATER:
            cardTypeImage.sprite = waterIcon;
            break;
            case CardType.ELECTRIC:
            cardTypeImage.sprite = electricIcon;
            break;
            case CardType.DARK:
            cardTypeImage.sprite = darkIcon;
            break;
            case CardType.LIGHT:
            cardTypeImage.sprite = lightIcon;
            break;
        }
    }
    public void CardFlip(bool _cardBack){
        if (_cardBack == true) {
            cardBack.enabled = true;
            isCardOnBack = _cardBack;
        } 
        else {
            cardBack.enabled = false;
            isCardOnBack = _cardBack;
        } 
    }
    public void LoadCardData(ref CardSO _loadedData){
        _loadedData = this.cardSO;
    }
    
    void ResetPosition(){
        transform.localPosition = oldPosition;
        this.transform.localScale = new Vector2(1,1);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ResetPosition();
    }
}