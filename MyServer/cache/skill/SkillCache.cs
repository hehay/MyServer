using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.dao;

namespace MyServer.cache.skill
{
    public class SkillCache:ISkillCache
    {
        public Dictionary<int, List<SKILL>> UserIdToskill = new Dictionary<int, List<SKILL>>();
        public List<SKILL> AddSkill(int userId, List<SKILL> skills)
        {
            UserIdToskill.Add(userId, skills);
            return skills;
        }

        public SKILL UpdateSkill(int userid, int skillid, SKILL nowSkill)
        {
            List<SKILL> skills = UserIdToskill[userid];
            for (int i = 0; i < skills.Count; i++)
            {
                if (skills[i].Id == skillid)
                {
                    skills[i] = nowSkill;
                }
            }
            return nowSkill;
        }

        public List<SKILL> GetSkillList(int id)
        {
            return UserIdToskill[id];
        }

        public void DeleteSkillList(int userId)
        {
            UserIdToskill.Remove(userId);
        }
        public bool IsInSkillDic(int id)
        {
            if (UserIdToskill.ContainsKey(id)) return true;
            return false;
        }
    }
}
