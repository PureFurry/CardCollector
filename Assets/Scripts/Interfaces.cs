using UnityEngine;

public class Interfaces : MonoBehaviour
{
    
}
public interface IGetPower{
    public int GetPower();
}
public interface IGetHealth{
    public int GetHealth();
}
public interface ITakeDamage{
    public void TakeDamage(int _damage);
}
public interface IActionOnTurn{
    public void TurnAction(ref Card[] cards);
}