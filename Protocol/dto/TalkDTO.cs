using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols.dto
{
    [Serializable]
   public class TalkDTO
    {
        public int userid;
        public string userName;
        public int receiverid;
        public string text;
        public TalkType talkType;
    }
    [Serializable]
    public enum TalkType
    {
        Word=1,
        Scene=2,
        One=3,
        System=4
    }
}
