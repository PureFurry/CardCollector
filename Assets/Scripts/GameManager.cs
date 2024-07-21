using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    public static event Action OnPlayerPowerUpdate;
    public int playerPower,enemyPower;
    private void Awake() {
        if (Instance == null)
        {
            Instance = this;            
        }
    }
    private void OnEnable() {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpgradePlayerPower(){
        Card[] temp = transform.GetComponentsInChildren<Card>();
        for (int i = 0; i < temp.Length; i++)
        {
            playerPower += temp[i].cardSO.cardDamage;
        }
        OnPlayerPowerUpdate?.Invoke();
    }
}
