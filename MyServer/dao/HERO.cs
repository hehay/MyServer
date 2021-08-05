using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.dao
{
   public  class HERO
   {
       private int heroId;

       private string heroName;

       private string firstOccupation;

        private string secondOccupation;
       

       public int HeroId
       {
           set { heroId = value; }
           get { return heroId; }
       }
       public string HeroName
       {
           set { heroName = value; }
           get { return heroName; }
       }
        public string FirstOccupation 
        {
            get { return firstOccupation; }
            set { firstOccupation = value; }
        }
        public string SecondOccupation 
        {
            get { return secondOccupation; }
            set { secondOccupation = value; }
        }
   }
}
