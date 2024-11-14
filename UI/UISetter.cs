using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class UISetter
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

    public static Action<ActionsSO> OnRefreshInventiryDeligat;
    public static void AddActions<T>(
        ScrollView scrollViewToAdd,
        List<ActionsSO> SOListToAdd,
        IActionStrategy Strategy)
        where T : ActionsSO
    {
        scrollViewToAdd.Clear();
        foreach (ActionsSO item in SOListToAdd)
        {
            if (item is T actionSO)
            {
                VisualElement actionElement;
                VisualTreeAsset placeHolder;

                if (item is AttackSO)
                {
                    actionElement = Strategy.Context.AttackPlaceHolder.CloneTree();
                    placeHolder = Strategy.Context.AttackPlaceHolder;
                }
                else if (item is DefenceSO)
                {
                    actionElement = Strategy.Context.DeffencePlaceHolder.CloneTree();
                    placeHolder = Strategy.Context.DeffencePlaceHolder;
                }
                else if (item is SkillSO)
                {
                    actionElement = Strategy.Context.SkillsPlaceHolder.CloneTree();
                    placeHolder = Strategy.Context.SkillsPlaceHolder;
                }
                else return;

                var (broken, destroid, durability) = SetElements(actionSO, actionElement);

                Strategy.HandleAction(actionSO, scrollViewToAdd, destroid, broken, Exit_simbol, durability, actionElement);

                scrollViewToAdd.Add(actionElement);

            }

        }

    }

    private static (Label broken, VisualElement destroid, string durability) SetElements(ActionsSO actionSO, VisualElement actionElement)
    {
        if (actionSO is AttackSO attackSO)
        {
            var label = actionElement.Q<Label>(Attack_Name);
            var attack = actionElement.Q<Label>(Attack_number);
            var SP = actionElement.Q<Label>(SP_number);
            var broken = actionElement.Q<Label>(Broken_number);
            var destroid = actionElement.Q<VisualElement>(Destroid_button);
            string durability = attackSO.Durability_Attack.ToString();

            label.text = attackSO.Name;
            attack.text = attackSO.Damage.ToString();
            SP.text = attackSO.SP_Cost.ToString();

            return (broken, destroid, durability);
        }
        if (actionSO is DefenceSO defenceSO)
        {
            var label = actionElement.Q<Label>(Defence_Name);
            var attack = actionElement.Q<Label>(Defence_number);
            var SP = actionElement.Q<Label>(SP_number);
            var broken = actionElement.Q<Label>(Broken_number);
            var destroid = actionElement.Q<VisualElement>(Destroid_button);
            string durability = defenceSO.Durability_Defence.ToString();

            label.text = defenceSO.Name;
            attack.text = defenceSO.Defence.ToString();
            SP.text = defenceSO.SP_Cost.ToString();

            return (broken, destroid, durability);
        }
        if (actionSO is SkillSO skillSO)
        {
            var label = actionElement.Q<Label>(Skill_Name);
            var SP = actionElement.Q<Label>(SP_number);
            var Cooldown = actionElement.Q<Label>(Cooldown_number);
            var destroid = actionElement.Q<VisualElement>(Destroid_button);
            Label broken = null;
            string durability = "";

            label.text = skillSO.Name;
            SP.text = skillSO.SP_Cost.ToString();
            Cooldown.text = skillSO.Cooldown.ToString();
            return (broken, destroid, durability);
        }
        return (null, null, null);
    }

    public static void OnRefreshInventiry(
        ActionsSO item,
        ScrollView scrollViewToAdd,
        List<ActionsSO> SOListToAdd,
        IActionStrategy Strategy)
    {
        SOListToAdd.Remove(item);

        if (item is AttackSO)
            AddActions<AttackSO>(scrollViewToAdd, SOListToAdd, Strategy);
        if (item is DefenceSO)
            AddActions<DefenceSO>(scrollViewToAdd, SOListToAdd, Strategy);
        if (item is SkillSO)
            AddActions<SkillSO>(scrollViewToAdd, SOListToAdd, Strategy);
    }
    public static void AddToInventory(
        ActionsSO item,
        ScrollView scrollViewToAdd,
        List<ActionsSO> SOListToAdd,
        IActionStrategy Strategy)
    {

        Strategy.Context.ActionsList.Add(item);
        OnRemoveFromStash(item, scrollViewToAdd, SOListToAdd, Strategy);

        OnRefreshInventiryDeligat?.Invoke(item);
    }

    public static void OnRemoveFromStash(
        ActionsSO item,
        ScrollView scrollViewToAdd,
        List<ActionsSO> SOListToAdd,
        IActionStrategy Strategy)
    {
        SOListToAdd.Remove(item);
        AddActions<ActionsSO>(scrollViewToAdd, SOListToAdd, Strategy);
    }

    public static void PutInConsole(List<string> list, ScrollView ScrollViewConsole, VisualTreeAsset ConsolePlaceHolder)
    {
        //ScrollViewConsole.Clear();

        foreach (var text in list)
        {
            VisualElement ConsoleElement = ConsolePlaceHolder.CloneTree();
            Label label = ConsoleElement.Q<Label>("Console_content_placeholder");
            label.text = text;
            label.style.color = Color.white;
            label.RegisterCallback<MouseEnterEvent>(evt => {label.style.color = Color.yellow;});
            label.RegisterCallback<MouseLeaveEvent>(evt => {label.style.color = Color.white;});

            ScrollViewConsole.Add(label);
        }
    }
}
