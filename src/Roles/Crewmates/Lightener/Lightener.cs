using MiraAPI.Hud;
using MiraAPI.Roles;
using PowerTools;
using ReachForStars.Components;
using ReachForStars.Translation;
using Reactor.Utilities.Extensions;
using UnityEngine;

namespace ReachForStars.Roles.Crewmates.Lightener;

public class LightenerRole : CrewmateGhostRole, ICustomRole
{
    private SwingingLantern lantern;

    public TranslationPool RoleDescLong = new(
        "Help the crew by placing lanterns to light up dim areas!",
        french: "",
        spanish: "",
        russian: ""
    );

    public TranslationPool RoleDescShort = new(
        "Let there be light!",
        french: "",
        spanish: "",
        russian: ""
    );

    public TranslationPool Rolename = new(
        "Lightener",
        "",
        "",
        ""
    );

    public override bool IsAffectedByComms => false;

    public void Start()
    {
        if (Player == null) return;

        lantern = new GameObject("HandAnimation").AddComponent<SwingingLantern>();
        lantern.gameObject.layer = LayerMask.NameToLayer("Players");
        lantern.gameObject.AddComponent<Animator>();
        lantern._animator = lantern.gameObject.AddComponent<SpriteAnim>();
        lantern._renderer = lantern.gameObject.AddComponent<SpriteRenderer>();
        lantern._renderer.material = new Material(Shader.Find("Unlit/PlayerShader"));
        PlayerMaterial.SetColors(Player.cosmetics.ColorId, lantern._renderer);
        lantern.transform.SetParent(Player.MyPhysics.Animations.transform);
        lantern.transform.localPosition = new Vector3(0f, -0.2f, -10f);
        lantern.transform.localScale = new Vector3(0.5f, 0.5f, 10f);
        lantern.Player = Player;

        if (PlayerControl.LocalPlayer == Player) CustomButtonSingleton<LightUp>.Instance.SetActive(true, this);
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
        HideSettings = false
    };

    public override void Deinitialize(PlayerControl targetPlayer)
    {
        lantern.gameObject.DestroyImmediate();
    }
}