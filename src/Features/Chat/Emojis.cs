namespace ReachForStars.Features;

public class Emojis
{
    /// <summary>
    ///     Executed by Patches/OnCutsceneBreak.cs
    /// </summary>
    public static string ReformatForEmojis(string text)
    {
        var FinalText = text;

        if (FinalText.ToLower().Contains(":shrug:")) FinalText = FinalText.Replace(":shrug:", "<sprite=0>");
        if (FinalText.ToLower().Contains(":heart:")) FinalText = FinalText.Replace(":heart:", "<sprite=1>");
        if (FinalText.ToLower().Contains(":heh:")) FinalText = FinalText.Replace(":heh:", "<sprite=2>");
        if (FinalText.ToLower().Contains(":fire:")) FinalText = FinalText.Replace(":fire:", "<sprite=3>");
        if (FinalText.ToLower().Contains(":sob:")) FinalText = FinalText.Replace(":sob:", "<sprite=4>");

        return FinalText;
    }
}