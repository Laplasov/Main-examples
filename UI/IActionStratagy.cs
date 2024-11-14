using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UIElements;

public interface IActionStrategy
{
    void HandleAction(ActionsSO ActionSO, ScrollView scrollViewToAdd, VisualElement Destroid, Label Broken, string ExitSymbol, string Durability, VisualElement actionElement);
    public ActionContext Context { get; set; }

}
public struct ActionContext
{
    public ScrollView ScrollViewActions { get; set; }
    public ScrollView ScrollViewStash { get; set; }
    public List<ActionsSO> ActionsList { get; set; }
    public List<ActionsSO> StashList { get; set; }
    public VisualTreeAsset AttackPlaceHolder { get; set; }
    public VisualTreeAsset DeffencePlaceHolder { get; set; }
    public VisualTreeAsset SkillsPlaceHolder { get; set; }

}

public class ActionInventoryStrategy : IActionStrategy
{
    public ActionContext Context { get; set; }
    public void HandleAction(ActionsSO ActionSO, ScrollView scrollViewToAdd, VisualElement Destroid, Label Broken, string ExitSymbol, string Durability, VisualElement actionElement)
    {
        if (ActionSO.Destructible)
        {
            Destroid.RegisterCallback<ClickEvent>(evt => UISetter.OnRefreshInventiry(
                ActionSO,
                scrollViewToAdd,
                Context.ActionsList,
                this
                ));
        }
        else
        {
            Destroid.style.display = DisplayStyle.None;
        }

        if (ActionSO is not SkillSO)
        {
            if (Broken != null)
            {
                Broken.text = ActionSO.Destructible ? Durability : ExitSymbol;
            }
        }
    }
}
public class ActionStashStrategy : IActionStrategy
{
    public ActionContext Context { get; set; }
    public void HandleAction(ActionsSO ActionSO, ScrollView scrollViewToAdd, VisualElement Destroid, Label Broken, string ExitSymbol, string Durability, VisualElement actionElement)
    {
        if (ActionSO.Destructible)
        {
            Destroid.RegisterCallback<ClickEvent>(evt => UISetter.OnRemoveFromStash(
            ActionSO,
            scrollViewToAdd,
            Context.StashList,
            this
            ));
        }
        else
        {
            Destroid.style.display = DisplayStyle.None;
        }

        if (ActionSO is not SkillSO)
        {
            if (Broken != null)
            {
                Broken.text = ActionSO.Destructible ? Durability : ExitSymbol;
            }
        }

        actionElement.RegisterCallback<ClickEvent>(evt => UISetter.AddToInventory(
            ActionSO,
            scrollViewToAdd,
            Context.StashList,
            this
        ));

    }
}
public class ButtleActionStrategy : IActionStrategy
{
    public ActionContext Context { get; set; }

    public void HandleAction(ActionsSO ActionSO, ScrollView scrollViewToAdd, VisualElement Destroid, Label Broken, string ExitSymbol, string Durability, VisualElement actionElement)
    {
        Destroid.style.display = DisplayStyle.None;
    }
}