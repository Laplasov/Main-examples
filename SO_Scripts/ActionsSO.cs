using UnityEngine;

public abstract class ActionsSO : ScriptableObject
{
    [SerializeField]
    private string m_Actions_Name;
    [SerializeField]
    private string m_Description;
    [SerializeField]
    private int m_SP_Cost;
    [SerializeField]
    private bool m_destructible;

    public string Name => m_Actions_Name;
    public string Description => m_Description;
    public int SP_Cost => m_SP_Cost;
    public bool Destructible => m_destructible;

    public virtual ActionsSO GetInstansSO() => this;
}