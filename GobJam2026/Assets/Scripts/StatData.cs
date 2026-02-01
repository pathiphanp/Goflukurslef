using UnityEngine;

[CreateAssetMenu(fileName = "StatData", menuName = "Scriptable Objects/StatData")]
public class StatData : ScriptableObject
{
    public BaseStat data;
}

[System.Serializable]
public class BaseStat
{
    public float hp;
    public float atk;
    public float atkRange;
    public float speedMove;

    public BaseStat Clone()
    {
        return new BaseStat
        {
            hp = hp,
            atk = atk,
            atkRange = atkRange,
            speedMove = speedMove
        };
    }
}