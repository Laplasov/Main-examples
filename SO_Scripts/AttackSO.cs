using UnityEngine;

[CreateAssetMenu(fileName = "AttackSO", menuName = "Scriptable Objects/AttackSO")]
public class AttackSO : ActionsSO
{
    [SerializeField]
    private int m_damage;
    [SerializeField]
    private int m_durability_Attack;

    public int Damage => m_damage;

    public int Durability_Attack
    {
        get
        {
            if (Destructible)
                return m_durability_Attack;
            else
                return -1;
        }
        set
        {
            m_durability_Attack = value;
            if (m_durability_Attack == 0 && Destructible)
                Destroy(this);
        }
    }
}