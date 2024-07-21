using UnityEngine;

public class FightManager : MonoBehaviour
{
    public static FightManager Instance;
    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public enum FightState
    {
        PLAYER,
        ENEMY
    }
}
