using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols.dto
{
    [Serializable]
    public class MatchDTO
    {
        //public string account;
        //public int total;
        public int accountId;
        public int index;
        public bool hasConfirm;
        public int heroId;
    }

}
