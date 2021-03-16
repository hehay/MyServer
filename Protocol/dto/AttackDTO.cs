using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols.dto
{
    [Serializable]
    public class AttackDTO
    {
        public int userId;
        public int[] targetsId;
    }
}
