using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public static event Action<int,int> OnPlayerPowerUpdate;
    public static event Action<int,int> OnEnemyStatsUpdate;
    public int playerPower,enemyPower,playerHealth,enemyHealth;
    public bool isPlayerAttacking, isEnemyAttacking;
    public Enemy enemy;
    public Player player;
    public GameStates gameStates;
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
        ResetState();
    }
    private void Update() {
        switch (gameStates)
        {
            case GameStates.RESET:
            playerPower = 0;
            enemyPower = 0;
            gameStates = GameStates.PLAYERTURN;
            break;
            case GameStates.PLAYERTURN:
            enemy.isTurn = false;
            player.isTurn = true;
            break;
            case GameStates.ENEMYTURN:
            enemy.isTurn = true;
            player.isTurn = false;
            break;
            case GameStates.END:
            int tempPlayerPower = playerPower;
            int tempEnemyPower = enemyPower;
            playerHealth -= tempEnemyPower;
            enemyHealth -=  tempPlayerPower;
            gameStates = GameStates.RESET;
            break;

        }
    }
    public void UpgradePlayerPower(int _playerPower,int _playerHealth){
        
        OnPlayerPowerUpdate?.Invoke(_playerPower,_playerHealth);
        playerPower = _playerPower;
        playerHealth = _playerHealth;
    }
    public void UpgradeEnemyStats(int _enemyPower,int _enemyHealth){
        OnEnemyStatsUpdate?.Invoke(_enemyPower, _enemyHealth);
        enemyPower = _enemyPower;
        enemyHealth = _enemyHealth;
    }
    public void ResetState(){
        gameStates = GameStates.RESET;
    }
    public void GiveTurn(){
        if (GameObject.FindGameObjectWithTag("AskPanel") == null)
        {
            if (player.isTurn == true)
            {
                isPlayerAttacking = GameObject.Find("Player Attack").GetComponent<Toggle>().isOn;
                if (isPlayerAttacking)
                {
                    
                    if (playerPower > enemyPower)
                    {
                        int multiplier = Mathf.RoundToInt(playerPower / 3);
                        if (UnityEngine.Random.Range(0,12) > 6)
                        {
                            enemyHealth -= UnityEngine.Random.Range(0,6) * multiplier;
                            Card[] destroyPool = GameObject.Find("Enemy Field").GetComponentsInChildren<Card>();
                            int randomDestroy = UnityEngine.Random.Range(0,destroyPool.Length - 1);
                            Destroy(destroyPool[randomDestroy].gameObject);
                            gameStates = GameStates.ENEMYTURN;
                        } 
                    }
                    else
                    {
                        playerHealth -= playerPower /3 +2;
                        gameStates = GameStates.ENEMYTURN;
                    }
                }
                else
                {
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
}
public enum GameStates
{
    START,
    PLAYERTURN,
    ENEMYTURN,
    RESET,
    END
}
