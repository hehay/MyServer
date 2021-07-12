using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols.dto
{
    [Serializable]
    public class ModelDTO
    {
        private int _id;
        private int gold;
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        public int Gold
        {
            set { gold = value; }
            get { return _id; }
        }
        
            

    }      
}                   
                       