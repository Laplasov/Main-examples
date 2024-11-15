using UnityEngine;

[CreateAssetMenu(fileName = "StaminaSO", menuName = "Scriptable Objects/StaminaSO")]
public class StaminaSO : ScriptableObject
{
    [SerializeField]
    private float m_stamina = 5f;
    [SerializeField]
    private float m_staminaDepletionRate = 1f;
    [SerializeField]
    private float m_staminaRecoveryRate = 0.5f;
    [SerializeField]
    private float m_staminaInit = 5f;

    public float Stamina 
    { 
        get => m_stamina;
        set => m_stamina = value;
    }
    public float StaminaDepletionRate
    {
        get => m_staminaDepletionRate;
        set => m_staminaDepletionRate = value;
    }
    public float StaminaRecoveryRate
    {
        get => m_staminaRecoveryRate;
        set => m_staminaRecoveryRate = value;
    }
    public float StaminaInit
    {
        get => m_staminaInit;
        set => m_staminaInit = value;
    }
}
