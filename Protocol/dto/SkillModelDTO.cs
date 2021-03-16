using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols.dto
{
        [Serializable]
    public class SkillModelDTO
    {
        public int code;//编码
        public string name;//技能名称
        public string info;//技能描述
        public string icon_name;
        public ApplyType applyType;
        public ApplyProperty applyProperty;
        public ReleaseType releaseType;
        public string efx_name;
        public int aniname;

    }

    [Serializable]
    public class SkillLevelDate
    {
        public int level;//等级
        public int nextLevel;//学习需要角色等级
        public int coldTime;//冷却时间--ms
        public float range;//释放距离
        public int applyValue;
        public int applyTime;
        public int mp;
        public float dis;
        public float back;
        public SkillLevelDate() { }

        public SkillLevelDate(int level,int nextLevel,int cold,float range,int applyValue,int applyTime,int mp,float jump,float back)
        {
            this.level = level;
            this.nextLevel = nextLevel;
            this.coldTime = cold;
            this.range = range;
            this.applyValue = applyValue;
            this.applyTime = applyTime;
            this.mp = mp;
            this.dis = jump;
            this.back = back;
        }
    }
        //作用类型
        public enum ApplyType
        {
            Passive=1,
            Buff=2,
            SingleTarget=3,
            MultiTarget=4
        }
        //作用属性
        public enum ApplyProperty
        {
            Attack=1,
            Def=2,
            Speed=3,
            HP=4,
            MP=5
        }
        //释放类型
        public enum ReleaseType
        {
            Self,
            Enemy,
            Position
        }
}
