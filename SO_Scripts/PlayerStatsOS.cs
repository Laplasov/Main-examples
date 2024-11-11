using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "PlayerStatsOS", menuName = "Scriptable Objects/PlayerStatsOS")]
public class PlayerStatsOS : ScriptableObject
{
    [SerializeField]
    private string m_player_name;
    [SerializeField]
    private int m_level;
    [SerializeField]
    private int m_healthPoints;
    [SerializeField]
    private int m_currentHealthPoints;
    [SerializeField]
    private int m_attack;
    [SerializeField]
    private int m_specialPoints;
    [SerializeField]
    private int m_defence;

    public void SetStatsToZero()
    {
        m_player_name = "Player";
        m_level = 0;
        m_healthPoints = 0;
        m_attack = 0;
        m_specialPoints = 0;
        m_defence = 0;
    }
}
