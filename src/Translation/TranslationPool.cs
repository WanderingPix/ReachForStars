using System;

namespace ReachForStars.Translation
{
   public struct TranslationPool
   {
     //Expandable 
     public string English;
     public string Spanish;
     public string French;
     public string Russian;
     public string Italian;
      
     public TranslationPool(string english, string spanish = null, string french = null, string russian = null, string italian = null)
     {
       English = english;
       Spanish = spanish;
       French = french;
       Russian = russian;
       Italian = italian;
     }
    }
  }
