using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.dao
{
    public class MODEL
    {
        private int id;
        private int gold;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int Gold 
        {
            get { return gold; }
            set { gold = value; }
        }
        
    }
}
