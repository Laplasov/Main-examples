using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUIWorld : MonoBehaviour
{
    #region CONSTANTS
    private const string Console_Holder = "Console-Holder";
    private const string Inventory_field = "Inventory_field";
    private const string Stash_Holder = "Stash-Holder";
    private const string Destroid_button_stash = "Destroid_button_stash";
    private const string ScrollViewActions = "ScrollViewActions";
    private const string ScrollViewStash = "ScrollViewStash";

    private const string Attack_button = "Attack-button";
    private const string Defence_button = "Defence-button";
    private const string Skill_button = "Skill-button";

    private const string Attack_Name = "Attack_Name";
    private const string Attack_number = "Attack_number";
    private const string SP_number = "SP_number";
    private const string Broken_number = "Broken_number";
    private const string Destroid_button = "Destroid_button";

    private const string Defence_Name = "Defence_Name";
    private const string Defence_number = "Defence_number";

    private const string Skill_Name = "Skill_Name";
    private const string Cooldown_number = "Cooldown_number";

    private const string Exit_simbol = "X";
    #endregion

    #region HOLDERS
    private VisualElement _console_Holder;
    private VisualElement _inventory_field;
    private VisualElement _stash_Holder;
    private VisualElement _destroid_button_stash;
    private ScrollView _ScrollViewActions;
    private ScrollView _ScrollViewStash;

    private VisualElement _attack_place_holder;
    private VisualElement _deffence_place_holder;
    private VisualElement _skills_place_holder;

    private Button _attack_button;
    private Button _defence_button;
    private Button _skill_button;

    private List<ActionsSO> m_actionsSO;

    [SerializeField]
    private List<ActionsSO> m_stashSO;

    [SerializeField]
    private GameObject m_player;
    [SerializeField]
    private PlayerStatsOS m_playerStatsOS;

    [SerializeField]
    private VisualTreeAsset AttackPlaceHolder;
    [SerializeField]
    private VisualTreeAsset DeffencePlaceHolder;
    [SerializeField]
    private VisualTreeAsset SkillsPlaceHolder;

    public GameObject Player => m_player;
    IActionStrategy inventoryStrategy;
    IActionStrategy stashStrategy;
    #endregion

    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        m_actionsSO = m_player.GetComponent<Inventory>().ActionsSO;

        _console_Holder = root.Q<VisualElement>(Console_Holder);
        _inventory_field = root.Q<VisualElement>(Inventory_field);
        _stash_Holder = root.Q<VisualElement>(Stash_Holder);
        _destroid_button_stash = root.Q<VisualElement>(Destroid_button_stash);

        _attack_button = root.Q<Button>(Attack_button);
        _defence_button = root.Q<Button>(Defence_button);
        _skill_button = root.Q<Button>(Skill_button);

        _ScrollViewActions = root.Q<ScrollView>(ScrollViewActions);
        _ScrollViewStash = root.Q<ScrollView>(ScrollViewStash);

        var context = new ActionContext
        {
            ActionsList = m_actionsSO,
            StashList = m_stashSO,
            AttackPlaceHolder = AttackPlaceHolder,
            DeffencePlaceHolder = DeffencePlaceHolder,
            SkillsPlaceHolder = SkillsPlaceHolder
        };

        inventoryStrategy = new ActionInventoryStrategy();
        stashStrategy = new ActionStashStrategy();
        inventoryStrategy.Context = context;
        stashStrategy.Context = context;

        //_stash_Holder.style.display = DisplayStyle.None;

        _attack_button.RegisterCallback<ClickEvent>(evt => UISetter.AddActions<AttackSO>(_ScrollViewActions, m_actionsSO, inventoryStrategy));
        _defence_button.RegisterCallback<ClickEvent>(evt => UISetter.AddActions<DefenceSO>(_ScrollViewActions, m_actionsSO, inventoryStrategy));
        _skill_button.RegisterCallback<ClickEvent>(evt => UISetter.AddActions<SkillSO>(_ScrollViewActions, m_actionsSO, inventoryStrategy));

        _destroid_button_stash.RegisterCallback<ClickEvent>(ExitStash);

        _ScrollViewActions.Clear();
        _ScrollViewStash.Clear();

        UISetter.AddActions<AttackSO>(_ScrollViewActions, m_actionsSO, inventoryStrategy);
        UISetter.AddActions<ActionsSO>(_ScrollViewStash, m_stashSO, stashStrategy);
        UISetter.OnRefreshInventiryDeligat += OnRefreshInventiryDeligat;
        //HideUI();
    }
    private void HideUI()
    {
        _console_Holder.style.display = DisplayStyle.None;
        _inventory_field.style.display = DisplayStyle.None;
    }
    private void ShowUI()
    {
        _console_Holder.style.display = DisplayStyle.Flex;
        _inventory_field.style.display = DisplayStyle.Flex;
    }
    private void ExitStash(ClickEvent evt)
    {
        _stash_Holder.style.display = DisplayStyle.None;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            ShowUI();
        if (Input.GetKeyUp(KeyCode.Tab))
            HideUI();
    }

    private void OnRefreshInventiryDeligat(ActionsSO item)
    {
        if (item is AttackSO)
            UISetter.AddActions<AttackSO>(_ScrollViewActions, m_actionsSO, inventoryStrategy);
        if (item is DefenceSO)
            UISetter.AddActions<DefenceSO>(_ScrollViewActions, m_actionsSO, inventoryStrategy);
        if (item is SkillSO)
            UISetter.AddActions<SkillSO>(_ScrollViewActions, m_actionsSO, inventoryStrategy);
    }
    private void OnDestroy() => UISetter.OnRefreshInventiryDeligat -= OnRefreshInventiryDeligat;
}
