using System;

namespace ReachForStars.Translation
{
  public static class TransManager
  {
    ///Could I use a switch case here?
    ///Yes.
    ///Will I?
    ///No.
    ///Why?
    ///Because I'm a lazy cunt
    public static string GetTranslatedText(this TranslationPool pool)
    {
      if (TranslationController.Instance.currentLanguage.languageID == SupportedLangs.Spanish && pool.Spanish != null)
      {
        return pool.Spanish;
      }
      else if (TranslationController.Instance.currentLanguage.languageID == SupportedLangs.Russian && pool.Spanish != null)
      {
        return pool.Russian;
      }
      else if (TranslationController.Instance.currentLanguage.languageID == SupportedLangs.Portuguese && pool.Portuguese != null)
      {
        return pool.Portuguese;
      }
      else if (TranslationController.Instance.currentLanguage.languageID == SupportedLangs.French && pool.French != null)
      {
        return pool.French;
      }
      else return pool.English;
    }
  }
}
