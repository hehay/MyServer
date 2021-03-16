using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols.dto
{
    [Serializable]
    public class SkillAttackDTO
    {
        public int userId;
        public int[] targetsId;
        public SkillDTO skillDto;
    }
}
