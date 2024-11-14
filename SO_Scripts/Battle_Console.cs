using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Battle_Console", menuName = "Scriptable Objects/Battle_Console")]
public class Battle_Console : ScriptableObject
{
    [SerializeField]
    private string m_console_title;
    [SerializeField]
    private List<string> m_console_list;
    public string ConsoleTitle => m_console_title;
    public List<string> ConsoleList => m_console_list;

}


