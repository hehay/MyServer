using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocols.dto;

namespace MyServer.dao
{
    public class SKILL
    {
        private int id;
        private int userId;
        private int skillId;
        private int shortcutId;
        private int code;//编码
        private int level;//等级
        private int nextLevel;//学习需要角色等级
        private int coldTime;//冷却时间--ms
        private string name;//技能名称
        private float range;//释放距离
        private int applyValue;
        private int applyTime;
        private int mp;
        private float dis;
        private float back;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public int NextLevel
        {
            get { return nextLevel; }
            set { nextLevel = value; }
        }

        public int ColdTime
        {
            get { return coldTime; }
            set { coldTime = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public float Range
        {
            get { return range; }
            set { range = value; }
        }


        public int ApplyValue
        {
            get { return applyValue; }
            set { applyValue = value; }
        }

        public int ApplyTime
        {
            get { return applyTime; }
            set { applyTime = value; }
        }

        public int Mp
        {
            get { return mp; }
            set { mp = value; }
        }



        public int ShortcutId
        {
            get { return shortcutId; }
            set { shortcutId = value; }
        }

        public float Dis
        {
            get { return dis; }
            set { dis = value; }
        }

        public float Back
        {
            get { return back; }
            set { back = value; }
        }

        public int SkillId
        {
            get { return skillId; }
            set { skillId = value; }
        }
    }
}
