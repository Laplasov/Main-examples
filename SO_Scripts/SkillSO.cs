using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "Scriptable Objects/SkillSO")]
public class SkillSO : ActionsSO
{
    [SerializeField]
    private int m_cooldown;
    public int Cooldown => m_cooldown;
}