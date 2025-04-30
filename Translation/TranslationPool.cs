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
     }
   }
}
