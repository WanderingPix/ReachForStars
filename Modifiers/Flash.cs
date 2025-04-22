using MiraAPI.Modifiers;
using MiraAPI.Modifiers.Types;
using ReachForStars.Utilities;

namespace ReachForStars.Addons.Flash;

public class FlashModifier : GameModifier
{
    public override string ModifierName => "Flash";

    public override int GetAmountPerGame()
    {
        return 15;
    }

    public override int GetAssignmentChance()
    {
        return 100;
    }
    public float SpeedMultiplier = 1f;
    float TasksDone = 0f;
    public override void OnActivate()
    {
        if (Player.HasModifier<FlashModifier>())
        {
            HudManager.Instance.DangerMeter.gameObject.SetActive(true);
            SpeedMultiplier = 1f;
            HudManager.Instance.DangerMeter.SetDangerValue(0f, 0f);
            HudManager.Instance.DangerMeter.crewmateSeekerSprite.sprite = Assets.SpeedOMeter.LoadAsset();
        }   
    }
    public override void OnDeactivate()
    {
        HudManager.Instance.DangerMeter.gameObject.SetActive(false);
        SpeedMultiplier = 1f;
    }
    
    public void IncreaseSpeed()
    {
        TasksDone++;
        SpeedMultiplier *= 1.75f; //replace 1.75f with a setting in the modifier settings
        
        HudManager.Instance.DangerMeter.SetDangerValue(TasksDone/5, TasksDone/5);
        HudManager.Instance.DangerMeter.SetFirstNBarColors((int)TasksDone, new UnityEngine.Color(1f, 1f, 0f, 1f));
    }
}
