using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.dao;
using NetFrame;
using Protocols.dto;

namespace MyServer.cache.skill
{
    public interface ISkillCache
    {
        List<SKILL> AddSkill(int userId, List<SKILL> _skills);

        List<SKILL> GetSkillList(int id);

        SKILL UpdateSkill(int userid, int skillid, SKILL nowSkill);
        void DeleteSkillList(int userId);
        bool IsInSkillDic(int id);
    }
}
