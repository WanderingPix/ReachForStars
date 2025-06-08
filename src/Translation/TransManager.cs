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
       ///I live in your walls  ^_^  -Pengun
       public static string GetTranslatedText(this TranslationPool pool)
       {
       if (TranslationController.Instance.currentLanguage.languageID == SupportedLangs.Spanish)
         {
         return pool.Spanish; //Spanish translator: TBD
         }
         else if (TranslationController.Instance.currentLanguage.languageID == SupportedLangs.Russian && pool.Russian != null)
         {
         return pool.Russian; //Russian translator: lime
         }
         else if (TranslationController.Instance.currentLanguage.languageID == SupportedLangs.French && pool.French != null)
         {
           return pool.French; //french translator: WanderingPix
         }
         else if (TranslationController.Instance.currentLanguage.languageID == SupportedLangs.Italian && pool.Italian != null)
         {
           return pool.Italian; //italian translator: pengun
         }
         else return pool.English;
       }
    }
}
