using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols.dto
{
    [Serializable]
    public class DamageDTO
    {
        public int skill;
        public int userid;
        public int[][] targets;

    }
}
