using MiraAPI.Roles;
using ReachForStars.Components;
using ReachForStars.Translation;
using ReachForStars.Utilities;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Blessed;

public class LightenerRole : CrewmateGhostRole, ICustomRole
{
    public AuraEffect Aura;

    public TranslationPool RoleDescLong = new(
        "Finish your tasks to revive yourself!",
        french: "",
        spanish: "",
        russian: ""
    );

    public TranslationPool RoleDescShort = new(
        "Finish your tasks to revive yourself!",
        french: "",
        spanish: "",
        russian: ""
    );

    public TranslationPool Rolename = new(
        "Blessed",
        "",
        "",
        ""
    );

    public override bool IsAffectedByComms => false;

    public void Start()
    {
        if (Player == null) return;

        Aura = new GameObject("HandAnimation").AddComponent<AuraEffect>();
        Aura.gameObject.layer = LayerMask.NameToLayer("Players");
        Aura._renderer = Aura.gameObject.AddComponent<SpriteRenderer>();
        Aura._renderer.sprite = Assets.PlaceHolder.LoadAsset();
        Aura._renderer.material = new Material(Shader.Find("Unlit/PlayerShader"));
        PlayerMaterial.SetColors(Player.cosmetics.ColorId, Aura._renderer);
        Aura.transform.SetParent(Player.MyPhysics.Animations.transform);
        Aura.transform.localPosition = new Vector3(0f, -0.2f, -10f);
        Aura.transform.localScale = new Vector3(0.5f, 0.5f, 10f);
        Aura.Player = Player;

        if (Player != PlayerControl.LocalPlayer) return;

        Player.RegenerateTasks();
        Player.gameObject.layer = LayerMask.NameToLayer("Players");

        //TBD Custom Player Model :3
    }

    public string RoleName => Rolename.GetTranslatedText();
    public string RoleLongDescription => RoleDescLong.GetTranslatedText();
    public string RoleDescription => RoleDescShort.GetTranslatedText();
    public Color RoleColor => Palette.CrewmateRoleHeaderBlue;
    public ModdedRoleTeams Team => ModdedRoleTeams.Crewmate;

    public Color OptionsMenuColor => Palette.CrewmateRoleHeaderBlue;

    public CustomRoleConfiguration Configuration => new(this)
    {
        Icon = Assets.SheriffIcon,
        ShowInFreeplay = true,
        HideSettings = false,
        TasksCountForProgress = false
    };

    public override void Deinitialize(PlayerControl targetPlayer)
    {
        //TBD Custom Player Model :3
    }
}