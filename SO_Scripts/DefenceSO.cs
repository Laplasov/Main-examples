using UnityEngine;

[CreateAssetMenu(fileName = "DefenceSO", menuName = "Scriptable Objects/DefenceSO")]
public class DefenceSO : ActionsSO
{

    [SerializeField]
    private int m_defence;
    public int Defence => m_defence;

    [SerializeField]
    private int m_durability_defence;
    public int Durability_Defence
    {
        get
        {
            if (Destructible)
                return m_durability_defence;
            else
                return -1;
        }
        set
        {
            m_durability_defence = value;
            if (m_durability_defence == 0 && Destructible)
                Destroy(this);
        }
    }
}
