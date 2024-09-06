using System;
using System.Globalization;
using UnityEngine;

public class GameActions : MonoBehaviour
{
    public static GameActions Instance;
    private void Awake() {
        if (this == null)
        {
            Instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Attack(ref Card[] attackSideField,ref Card[] rivalSideField,ref GameStates gameStates,ref int combo){
        // Debug.Log("Work");
                    FetchCardField(ref attackSideField,"PlayerField");
                    FetchCardField(ref rivalSideField,"EnemyField");
                    //bu kısım gamemanagerda kalabilir
                    // if (isItFirstTurn)
                    // {
                    //     isItFirstTurn = false;
                    // }


                        if (rivalSideField != null)
                        {
                            for (int i = 0; i < rivalSideField.Length; i++)
                            {
                                if (rivalSideField[i].isCardOnBack == true)
                                {
                                    rivalSideField[i]?.CardFlip(false);
                                    // GameManager.Instance.UpgradeEnemyStats(GameManager.Instance.enemy.GetPower());
                                }
                            }
                            for (int i = 0; i < rivalSideField.Length; i++)
                            {
                                for (int j = 0; j < attackSideField.Length; j++)
                                {
                                    Debug.Log(attackSideField[j].IsAttacked);
                                    if (attackSideField[j].cardSO.cardValue >= rivalSideField[i].cardSO.cardValue && attackSideField[j].cardSO.cardDamage > rivalSideField[i].cardSO.cardDamage && attackSideField[j].IsAttacked == false && rivalSideField!= null && combo == attackSideField[j].cardSO.cardValue)
                                    {
                                        attackSideField[i].IsAttacked = true;
                                        GameManager.Instance.PlayerComboUpgrade();
                                        Destroy(rivalSideField[i].gameObject); 
                                    }
                                    if (rivalSideField == null)
                                    {
                                        HitToEnemy(attackSideField[j],ref GameManager.Instance.enemyHealth);
                                    }
                                }
                            }
                            gameStates = GameStates.ENEMYTURN;
                        }
    }
    public void HitRivalHealth(ref Card[] rivalSideField, ref GameStates gameStates){
        Debug.Log("Boş kısmında çalıştı");
        for (int i = 0; i < rivalSideField.Length; i++)
        {
            HitToEnemy(rivalSideField[i],ref GameManager.Instance.enemyHealth);
        }
        gameStates = GameStates.ENEMYTURN;
    }
    Card[] FetchCardField(ref Card[] _cardList,string fieldName){
        if (_cardList != null)
        {
            Array.Clear(_cardList, 0, _cardList.Length);
        }
       return _cardList = GameObject.FindGameObjectWithTag(fieldName).GetComponentsInChildren<Card>();
    }
    void HitToEnemy(Card attackCard,ref int changedHealth){
            Debug.Log("asdşasşdlasişdasdlşasd");
            changedHealth -= attackCard.cardSO.cardDamage;
    }
}
