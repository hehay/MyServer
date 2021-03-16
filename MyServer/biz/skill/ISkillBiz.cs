using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.dao;
using NetFrame;
using Protocols.dto;

namespace MyServer.biz.skill
{
    public interface ISkillBiz
    {
        List<SkillDTO> AddSkill(UserToken userToken);

        List<SkillDTO> GetSkillList(UserToken token);

        void DeleteSkill(UserToken token);
        SkillDTO UpdateSkill(UserToken token, SkillDTO nowskill);
        SkillDTO SkillUp(UserToken token, int code);
    }
}
