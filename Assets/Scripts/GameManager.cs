using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public static event Action<int> OnPlayerPowerUpdate;
    public static event Action<int> OnEnemyStatsUpdate;
    public static event Action<int> OnPlayerHealthUpdate;
    public static event Action<int> OnEnemyHealthUpdate;
    public static event Action<int> PlayerComboUpdate;
    public static event Action<int> EnemyComboUpdate;
    public int playerPower,enemyPower,playerHealth,enemyHealth, currentPlayerHealth;
    public bool isPlayerAttacking, isEnemyAttacking, isItFirstTurn, playerAttackable = true, enemyAttackable = true;
    public Dictionary<int, int> combo;
    public int playerCombo,enemyCombo;
    public int playerAttackCount, enemyAttackCount, playerComboCounter, enemyComboCounter;
    public Enemy enemy;
    public Player player;
    public GameStates gameStates;
    Card[] playerCardonField;
    Card[] enemyCardsonField;
    private void Awake() {
        if (Instance == null)
        {
            Instance = this;            
        }
    }
    private void Start() {
        gameStates = GameStates.START;
        player = GameObject.Find("Player Hand").GetComponent<Player>();
        enemy = GameObject.Find("Enemy Hand").GetComponent<Enemy>();
        combo = new Dictionary<int, int>();
    }
    private void Update() {
        switch (gameStates)
        {
            case GameStates.START:
            isItFirstTurn = true;
            playerHealth = 20;
            enemyHealth = 20;
            OnPlayerHealthUpdate?.Invoke(playerHealth);
            OnEnemyHealthUpdate?.Invoke(enemyHealth);
            combo.Add(1,1);
            combo.Add(2,1);
            combo.Add(3,1);
            combo.Add(4,2);
            combo.Add(5,2);
            combo.Add(6,3);
            playerComboCounter = 1;
            enemyComboCounter = 1;
            playerCombo = 1;
            enemyCombo = 1;
            PlayerComboUpdate?.Invoke(playerCombo);
            EnemyComboUpdate?.Invoke(enemyCombo);
            FetchCardField(ref playerCardonField,"PlayerField");
            FetchCardField(ref enemyCardsonField,"EnemyField");
            CardAttackReset(playerCardonField);
            CardAttackReset(enemyCardsonField);
            gameStates = GameStates.PLAYERTURN;
            break;
            case GameStates.RESET:
            playerPower = 0;
            enemyPower = 0;
            gameStates = GameStates.PLAYERTURN;
            break;
            case GameStates.PLAYERTURN:
            FetchCardField(ref playerCardonField,"PlayerField");
            FetchCardField(ref enemyCardsonField,"EnemyField");
            CardAttackReset(playerCardonField);
            enemy.isTurn = false;
            player.isTurn = true;
            break;
            case GameStates.ENEMYTURN:
            FetchCardField(ref playerCardonField,"PlayerField");
            FetchCardField(ref enemyCardsonField,"EnemyField");
            enemy.isTurn = true;
            player.isTurn = false;
            break;
            case GameStates.END:
            gameStates = GameStates.RESET;
            break;
        }
    }
    public void UpgradePlayerPower(int _playerPower){
        OnPlayerPowerUpdate?.Invoke(_playerPower);
        playerPower = _playerPower;
    }
    public void UpgradeEnemyStats(int _enemyPower){
        OnEnemyStatsUpdate?.Invoke(_enemyPower);
        enemyPower = _enemyPower;
    }
    public void ResetState(){
        gameStates = GameStates.RESET;
    }
    public void GiveTurn(){
        Debug.Log(playerHealth);
        Debug.Log(enemyHealth);
        if (GameObject.FindGameObjectWithTag("AskPanel") == null)
        {
            if (player.isTurn == true)
            {
                isPlayerAttacking = GameObject.Find("Player Attack").GetComponent<Toggle>().isOn;
                //player attackable tru ile başlamalı
                if (isPlayerAttacking && playerAttackable)
                {
                    // Debug.Log("Work");
                    FetchCardField(ref playerCardonField,"PlayerField");
                    FetchCardField(ref enemyCardsonField,"EnemyField");
                    if (isItFirstTurn)
                    {
                        isItFirstTurn = false;
                    }
                    // Debug.Log(isItFirstTurn);
                    // Debug.Log(enemyCardsonField != null);
                    // Debug.Log(playerPower > enemyPower);
                    //    && enemyCardsonField != null // sıkıntı düşmanfieldını null çekiyor
                    if (playerPower > enemyPower && isItFirstTurn == false)
                    {
                        if (enemyCardsonField != null)
                        {
                            for (int i = 0; i < enemyCardsonField.Length; i++)
                            {
                                if (enemyCardsonField[i].isCardOnBack == true)
                                {
                                    enemyCardsonField[i]?.CardFlip(false);
                                    UpgradeEnemyStats(enemy.GetPower());
                                }
                            }
                            for (int i = 0; i < enemyCardsonField.Length; i++)
                            {
                                for (int j = 0; j < playerCardonField.Length; j++)
                                {
                                    Debug.Log(playerCardonField[j].IsAttacked);
                                    if (playerCardonField[j].cardSO.cardValue >= enemyCardsonField[i].cardSO.cardValue && playerCardonField[j].cardSO.cardDamage > enemyCardsonField[i].cardSO.cardDamage && playerCardonField[j].IsAttacked == false && enemyCardsonField!= null)
                                    {
                                        playerCardonField[i].IsAttacked = true;
                                        playerComboCounter++;
                                        playerCombo = combo[playerComboCounter];
                                        PlayerComboUpdate?.Invoke(playerCombo);
                                        Destroy(enemyCardsonField[i].gameObject); 
                                    }
                                    if (enemyCardsonField == null)
                                    {
                                        HitToEnemy(playerCardonField[j]);
                                    }
                                }
                            }
                            gameStates = GameStates.ENEMYTURN;
                        }
                        
                    }
                    if(enemyCardsonField == null && !isItFirstTurn)
                    {
                        Debug.Log("Boş kısmında çalıştı");
                        for (int i = 0; i < playerCardonField.Length; i++)
                        {
                            HitToEnemy(playerCardonField[i]);
                        }
                        OnEnemyHealthUpdate(enemyHealth);
                        gameStates = GameStates.ENEMYTURN;
                    }
                }
                else
                {
                    AttackableChange(ref playerAttackable);
                    gameStates = GameStates.ENEMYTURN;
                } 
            }
            if (enemy.isTurn == true)
            {
                isEnemyAttacking = GameObject.Find("Enemy Attack").GetComponent<Toggle>().isOn;
                if (isEnemyAttacking)
                {
                    playerHealth -= enemyPower;
                    gameStates = GameStates.PLAYERTURN;
                }
                else
                {
                    gameStates = GameStates.PLAYERTURN;
                }
            }
        }
    }
    void AttackableChange(ref bool _isAttackable){
        if (_isAttackable == true) {_isAttackable = false;}
        else {_isAttackable = true;}
    }
    void HitToEnemy(Card attackCard){
            Debug.Log("asdşasşdlasişdasdlşasd");
            enemyHealth-= attackCard.cardSO.cardDamage;
            OnEnemyHealthUpdate(enemyHealth);
    }
    void CardAttackReset(Card[] cardList){
        if (cardList != null)
        {
            for (int i = 0; i < cardList.Length ; i++)
            {
                cardList[i].IsAttacked = false;
            }
        }  
    }
    Card[] FetchCardField(ref Card[] _cardList,string fieldName){
        if (_cardList != null)
        {
            Array.Clear(_cardList, 0, _cardList.Length);
        }
       return _cardList = GameObject.FindGameObjectWithTag(fieldName).GetComponentsInChildren<Card>();
    }
}
public enum GameStates
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    RESET,
    END
}
