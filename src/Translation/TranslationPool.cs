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
      
     public TranslationPool(string english, string spanish = null, string french = null, string russian = null)
     {
       English = english;
       Spanish = spanish;
       French = french;
       Russian = russian;
     }
    }
  }
