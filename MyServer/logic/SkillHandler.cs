using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.biz;
using MyServer.biz.skill;
using NetFrame;
using Protocols;
using Protocols.dto;

namespace MyServer.logic
{
    public class skillHandler:AbsOnceHandler,HandlerInterface
    {
        public ISkillBiz Skill = BizFactory.SkillBiz;
        public void ClientClose(NetFrame.UserToken token, string error)
        {
           
        }

        public void MessageReceive(NetFrame.UserToken token, NetFrame.Auto.SocketModel message)
        {
            switch (message.command)
            {
                case SkillProtocol.GetskillList_CREQ:
                    GetSkillLit(token);
                    break;
                case SkillProtocol.Deleteskill_CREQ:
                    DeleteSkill(token);
                    break;
                case SkillProtocol.SkillUp_CREQ:
                    SkillUp(token, message.GetMessage<int>());
                    break;
                case SkillProtocol.Updateskill_CREQ:
                    UpDateSkill(token,message.GetMessage<SkillDTO>());
                    break;
            }
        }

        void SkillUp(UserToken token, int code)
        {
            SkillDTO skillDto=Skill.SkillUp(token, code);
            Write(token, SkillProtocol.SkillUp_SRES, skillDto);
        }

        void UpDateSkill(UserToken token, SkillDTO skillDto)
        {
            Skill.UpdateSkill(token, skillDto);
        }
        void GetSkillLit(UserToken token)
        {
         List<SkillDTO> skillDtos=   Skill.GetSkillList(token);
            Write(token,SkillProtocol.GetskillList_SRES,skillDtos);
        }

        void DeleteSkill(UserToken token)
        {
            Skill.DeleteSkill(token);
        }
        public override byte GetType()
        {
            return Protocol.Skill;
        }
    }
}
