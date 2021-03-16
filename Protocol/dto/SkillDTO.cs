using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols.dto
{
    [Serializable]
    public class SkillDTO
    {
        public int id;
        public int UserId;
        public int skillId;
        public int shortcutId;
        public int level;//等级
        public int nextLevel;//学习需要角色等级
        public int coldTime;//冷却时间--ms
        public float range;//释放距离
        public int applyValue;
        public int applyTime;
        public int mp;
        public float dis;
        public float back;
        public SkillModelDTO SkillModelDto;
    }
}
