using System;

namespace ReachForStars.Translation
{
   public struct TranslationPool
   {
     //Expandable 
     public string English;
     public string Spanish;
     public string French;
     public string Portuguese;
     public string Russian;
     public string Italian;
      
     public TranslationPool(string english, string spanish = null, string portuguese = null, string french = null, string russian = null, string italian = null)
     {
       English = english;
       Spanish = spanish;
       Portuguese = portuguese;
       French = french;
       Russian = russian;
       Italian = italian;
        
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
         else if (TranslationController.Instance.currentLanguage.languageID == SupportedLangs.Portuguese && pool.Portuguese != null)
         {
           return pool.Portuguese; //Portuguese translator: TBD
         }
         else if (TranslationController.Instance.currentLanguage.languageID == SupportedLangs.French && pool.French != null)
         {
           return pool.French; //french translator: WanderingPix
         }
         else if (TranslationController.Instance.currentLanguage.languageID == SupportedLangs.Italian && pool.Italian != null)
         {
           return pool.Italian; //french translator: pengun
         }
         else return pool.English;
      }
    }
  }
