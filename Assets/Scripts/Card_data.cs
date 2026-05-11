using UnityEngine;

[CreateAssetMenu(fileName = "Card_data", menuName = "Cards/Card_data", order = 1)]
public class Card_data : ScriptableObject
{
    public string card_name;
    public string description;
    public enum Origin
    {
        Connected,
        Disconnected
    }
    public Origin originateFrom;
    public enum aoeType
    {
        Curve,
        Line,
        Round
    }
    public aoeType aoe;
    public int damage;
    public int range;
    public Sprite sprite;
    public enum Card_type 
    {
        Damage,
        Buff,
        Debuff
    }

    public Card_type type;
}
