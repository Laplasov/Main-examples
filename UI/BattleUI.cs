using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleUI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_player;

    private List<ActionsSO> m_actionsSO;

    [SerializeField]
    private Battle_Console battle_ConsoleSO;

    private List<string> _consol_text_list;

    [SerializeField]
    private VisualTreeAsset AttackPlaceHolder;
    [SerializeField]
    private VisualTreeAsset DeffencePlaceHolder;
    [SerializeField]
    private VisualTreeAsset SkillsPlaceHolder;
    [SerializeField]
    private VisualTreeAsset ConsolePlaceHolder;

    Label Console_title;
    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var ScrollViewConsole = root.Q<ScrollView>("ScrollViewConsole");
        var ScrollViewActions = root.Q<ScrollView>("ScrollViewActions");

        var Attack_button = root.Q<Button>("Attack_button");
        var Defence_button = root.Q<Button>("Defence_button");
        var Skill_button = root.Q<Button>("Skill_button"); 
        var Run_Button = root.Q<Button>("Run_Button");

        Console_title = root.Q<Label>("Console_content_placeholder");

        SetConsoleTitle(battle_ConsoleSO.ConsoleTitle);
        _consol_text_list = battle_ConsoleSO.ConsoleList;

        m_actionsSO = m_player.GetComponent<Inventory>().ActionsSO;

        var context = new ActionContext
        {
            ActionsList = m_actionsSO,
            StashList = null,
            AttackPlaceHolder = AttackPlaceHolder,
            DeffencePlaceHolder = DeffencePlaceHolder,
            SkillsPlaceHolder = SkillsPlaceHolder
        };

        var buttleActionStrategy = new ButtleActionStrategy();
        buttleActionStrategy.Context = context;

        Attack_button.RegisterCallback<ClickEvent>(evt => UISetter.AddActions<AttackSO>(ScrollViewActions, m_actionsSO, buttleActionStrategy));
        Defence_button.RegisterCallback<ClickEvent>(evt => UISetter.AddActions<DefenceSO>(ScrollViewActions, m_actionsSO, buttleActionStrategy));
        Skill_button.RegisterCallback<ClickEvent>(evt => UISetter.AddActions<SkillSO>(ScrollViewActions, m_actionsSO, buttleActionStrategy));
        Run_Button.RegisterCallback<ClickEvent>(evt => ExitButtle());

        UISetter.PutInConsole(_consol_text_list, ScrollViewConsole, ConsolePlaceHolder);
    }

    private void SetConsoleTitle(string title)
    {
        Console_title.text = title;
    }
    private void ExitButtle()
    {
        Debug.Log("Exit");
    }
}
