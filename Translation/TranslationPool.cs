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
     public TranslationPool(string english, string spanish, string portuguese, string french)
     {
       English = english;
       Spanish = spanish;
       Portuguese = portuguese;
       French = french;
     }
   }
}
