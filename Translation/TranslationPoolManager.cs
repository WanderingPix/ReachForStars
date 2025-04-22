using System;

namespace ReachForStars.Translation
{
  public static class TransManager
  {
    public static string GetTranslatedText(this TranslationPool pool)
    {
      if (TranslationController.Instance.currentLanguage.languageID == SupportedLangs.Spanish && pool.Spanish != null)
      {
        return pool.Spanish;
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
