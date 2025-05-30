namespace ReachForStars.Networking
{
    public enum RPC : uint
    {
        // for the Yeehaw sfx 
        Yeehaw,
        // for the Seeker spawn sfx
        SeekerScream,
        // for changing the body type of a player
        ChangeBodyType,
        // for placing vents as mole
        PlaceDaVent,
        // for resizing players, be careful with values tho!
        ResizePlayer,
        // Do I need to explain this one?
        DestroyObj,
        // for reviving players
        Revive,
        // For freezing dead bodies as chiller
        FreezeBody,
        // For unfreezing dead bodies
        DamageFrozenBody,
        // for possessing as Cursed soul
        Possess,
        // for placing cobwebs as arachnid
        PlaceCobweb,
        // for setting traps as trapper
        PlaceTrap,
    }
}
