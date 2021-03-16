using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocols.dto;

namespace MyServer.tool
{
    public class SkillInitialProperty
    {
        public static readonly Dictionary<int,SkillInitial>mapSkill=new Dictionary<int, SkillInitial>();
        public static readonly List<SkillInitial> skillInitials=new List<SkillInitial>();
        public SkillInitialProperty()
        {
            Create(1001, ModelName.LichModel, "重击", "造成前方范围内", "lichskill1", ApplyType.MultiTarget, ApplyProperty.Attack, ReleaseType.Enemy, "CFX_Hit_A Red", 4, GetSkillLevelData(0, 1, 5, 60, 130, 0, 20, 5, 1), GetSkillLevelData(1, 5, 5, 60, 130, 0, 20, 5, 1), GetSkillLevelData(2, 12, 4, 60, 150, 0, 30, 5, 1), GetSkillLevelData(3, 25, 3, 60, 200, 0, 20, 5, 1));
            Create(1002, ModelName.LichModel, "致命一击", "造成前方范围内", "lichskill2", ApplyType.MultiTarget, ApplyProperty.Attack, ReleaseType.Enemy, "CFX_Hit_A Red", 5, GetSkillLevelData(0, 1, 5, 60, 130, 0, 20, 5, 1), GetSkillLevelData(1, 5, 5, 60, 130, 0, 20, 5, 1), GetSkillLevelData(2, 12, 4, 60, 150, 0, 30, 5, 1), GetSkillLevelData(3, 25, 3, 60, 200, 0, 20, 5, 1));
            Create(1003, ModelName.LichModel, "全力打击", "造成前方范围内", "lichskill3", ApplyType.MultiTarget, ApplyProperty.Attack, ReleaseType.Enemy, "CFX_Hit_A Red", 6, GetSkillLevelData(0, 1, 5, 60, 130, 0, 20, 5, 1), GetSkillLevelData(1, 5, 5, 60, 130, 0, 20, 5, 1), GetSkillLevelData(2, 12, 4, 60, 150, 0, 30, 5, 1), GetSkillLevelData(3, 25, 3, 60, 200, 0, 20, 5, 1));

        }

        static SkillLevelDate GetSkillLevelData(int level, int nextLevel, int cold, float range, int applyValue, int applyTime, int mp, float jump, float back)
        {
            SkillLevelDate data = new SkillLevelDate( level, nextLevel, cold, range, applyValue, applyTime, mp,jump,back);
            return data;
        }
        void Create(int id,int code,  string name,  string info, string icon_name,
            ApplyType applyType, ApplyProperty applyProperty,  ReleaseType releaseType, string efx_name,int animName,params SkillLevelDate[] skillLevelDates)
        {
            SkillInitial skillInitial = new SkillInitial(id,code,  name, info, icon_name,
             applyType, applyProperty, releaseType, efx_name, animName, skillLevelDates);
            skillInitials.Add(skillInitial);
            mapSkill.Add(id,skillInitial);
        }
    }

    
    public class SkillInitial
    {
        public int id;
        public int code;//编码
        public string name;//技能名称
        public string info;//技能描述
        public string icon_name;
        public ApplyType applyType;
        public ApplyProperty applyProperty;
        public ReleaseType releaseType;
        public string efx_name;
        public int aniname;
        public SkillLevelDate[] SkillLevelDates;
        public SkillInitial() { }

        public SkillInitial(int id,int code, string name, string info, string icon_name,
            ApplyType applyType,ApplyProperty applyProperty,ReleaseType releaseType,string efx_name,int animName,SkillLevelDate[] skillLevelDates)
        {
            this.id = id;
            this.code = code;
            this.name = name;
            this.info = info;
            this.icon_name = icon_name;
            this.applyType = applyType;
            this.applyProperty = applyProperty;
            this.releaseType = releaseType;
            this.efx_name = efx_name;
            this.aniname = animName;
            this.SkillLevelDates = skillLevelDates;
        }
    }
}
