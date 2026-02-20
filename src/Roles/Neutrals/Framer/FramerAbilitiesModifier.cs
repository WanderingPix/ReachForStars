using MiraAPI.Hud;
using MiraAPI.Modifiers.Types;

namespace ReachForStars.Roles.Neutrals.Framer;

public class FramerAbilitiesModifier : TimedModifier
{
    public override string ModifierName => "Disguised";

    public override float Duration => 30;

    public override bool ShowInFreeplay => false;
    
    public PlayerControl Target;

    public override void OnActivate()
    {
        Player.Shapeshift(Target, true);
        if (Player.AmOwner)
        {
            var ventbtn = CustomButtonSingleton<FramerVentButton>.Instance;
            ventbtn.Button?.Show();
            ventbtn.ResetCooldownAndOrEffect();
            ventbtn.Timer = 0;
            var killbtn = CustomButtonSingleton<FramerKillButton>.Instance;
            killbtn.Button?.Show();
            killbtn.ResetCooldownAndOrEffect();
            killbtn.Timer = 0;
        }
    }
    
    public override void OnDeactivate()
    {
        Player.RpcRejectShapeshift();
        if (Player.AmOwner)
        {
            CustomButtonSingleton<FramerVentButton>.Instance.Button?.Hide();
            CustomButtonSingleton<FramerKillButton>.Instance.Button?.Hide();
        }
    }

    public FramerAbilitiesModifier(PlayerControl playerControl)
    {
        Target = playerControl;
    }
}