using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArenaUIManager : MonoBehaviour
{
    [SerializeField] GameObject playerField;
    [SerializeField] GameObject enemyField;
    [SerializeField] TMP_Text playerPowerText;
    [SerializeField] TMP_Text enemyPowerText;
    [SerializeField] TMP_Text playerHealthText;
    [SerializeField] TMP_Text enemyHealthText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable() {
        SuscribeUpdatePlayerText();
    }
    private void OnDisable() {
        UnSuscribeUpdatePlayerText();
    }
    void SuscribeUpdatePlayerText(){
        GameManager.OnPlayerPowerUpdate += PlayerTextUpdate;
    }
    
    void UnSuscribeUpdatePlayerText(){
        GameManager.OnPlayerPowerUpdate -= PlayerTextUpdate;
    }


    private void PlayerTextUpdate(int powerAmount,int healthAmount)
    {
        playerPowerText.text = powerAmount.ToString();
        playerHealthText.text = healthAmount.ToString();
    }
}
