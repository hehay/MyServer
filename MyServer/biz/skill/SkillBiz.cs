using System;
using System.Collections.Generic;
using MyServer.cache;
using MyServer.cache.skill;
using MyServer.cache.user;
using MyServer.dao;
using MyServer.tool;
using NetFrame;
using Protocols.dto;

namespace MyServer.biz.skill
{
    public class SkillBiz:ISkillBiz
    {
        public IUserCache UserCache = cacheFactory.UserCache;
        public ISkillCache SkillCache = cacheFactory.SkillCache;
        public List<SkillDTO> AddSkill(UserToken userToken)
        {
            int userId = UserCache.GetUserId(userToken);
            USER user = UserCache.GetUserById(userId);
            string[] skillids = user.SkillIds.Split(',');
            List<int> skillidlist = new List<int>();
            for (int i = 0; i < skillids.Length; i++)
            {
                skillidlist.Add(int.Parse(skillids[i]));
            }
            if (!SkillCache.IsInSkillDic(userId))
            {
                List<SKILL> skills = new List<SKILL>();

                for (int i = 0; i < skillidlist.Count; i++)
                {
                    SkillInitial skillInitial = SkillInitialProperty.mapSkill[skillidlist[i]];
                    if (skillInitial.code == user.ModelName)
                    {
                        SKILL skill=new SKILL();
                        skill.Id = skillInitial.id;
                        skill.UserId = userId;
                        skill.SkillId = skillInitial.id;
                        skill.ShortcutId = -1;
                        skill.Code = skillInitial.code;
                        skill.Level = skillInitial.SkillLevelDates[0].level;
                        skill.NextLevel = skillInitial.SkillLevelDates[0].nextLevel;
                        skill.ColdTime = skillInitial.SkillLevelDates[0].coldTime;
                        skill.Name = skillInitial.name;
                        skill.Range = skillInitial.SkillLevelDates[0].range;
                        skill.ApplyValue = skillInitial.SkillLevelDates[0].applyValue;
                        skill.ApplyTime = skillInitial.SkillLevelDates[0].applyTime;
                        skill.Dis = skillInitial.SkillLevelDates[0].dis;
                        skill.Back = skillInitial.SkillLevelDates[0].back;
                        skill.Mp = skillInitial.SkillLevelDates[0].mp;
                        skills.Add(skill);
                    }
                }

                List<SKILL> skillList=SkillCache.AddSkill(userId, skills);
                return GetSkillDtoList(skillList);

            }
            return null;
        }


        public List<SkillDTO> GetSkillList(NetFrame.UserToken token)
        {
            int userId = UserCache.GetUserId(token);
            if (SkillCache.IsInSkillDic(userId))
            {
                List<SKILL> skillList = SkillCache.GetSkillList(userId);
                return GetSkillDtoList(skillList);
            }
            else
            {
                return AddSkill(token);
            }
        }

        public SkillDTO UpdateSkill(NetFrame.UserToken token, SkillDTO nowSkill)
        {
            int userId = UserCache.GetUserId(token);
            return GetSkillDto(SkillCache.UpdateSkill(userId, nowSkill.id, GetSkill(nowSkill)));
        }
        public void DeleteSkill(NetFrame.UserToken token)
        {
            int id = UserCache.GetUserId(token);
            SkillCache.DeleteSkillList(id);
        }
        public SkillDTO SkillUp(UserToken token, int code)
        {
            USER user = UserCache.GetUserById(UserCache.GetUserId(token));
            List<SkillDTO> skills = GetSkillList(token);
            for (int i = 0; i < skills.Count; i++)//遍历是否角色拥有此技能
            {
                if (skills[i].id == code)
                {
                    if (skills[i].nextLevel <= user.Level)//判断玩家等级能否升级
                    {
                        //user.SkillPoint -= 1;
                        int level = skills[i].level + 1;
                        SkillLevelDate date = SkillInitialProperty.mapSkill[code].SkillLevelDates[level];
                        skills[i].nextLevel = date.nextLevel;
                        skills[i].level = date.level;
                        skills[i].applyValue = date.applyValue;
                        skills[i].applyTime = date.applyTime;
                        skills[i].range = date.range;
                        skills[i].coldTime = date.coldTime;
                        skills[i].mp = date.mp;
                        skills[i].dis = date.dis;
                        skills[i].back = date.back;
                        return skills[i];
                    }
                }
            }
            return null;
        }
        List<SkillDTO> GetSkillDtoList(List<SKILL> skillList)
        {
            List<SkillDTO> skillDtos = new List<SkillDTO>();
            for (int i = 0; i < skillList.Count; i++)
            {
                SkillDTO skillDto = new SkillDTO();
                skillDto.id = skillList[i].Id;
                skillDto.UserId = skillList[i].UserId;
                skillDto.shortcutId = skillList[i].ShortcutId;
                skillDto.level = skillList[i].Level;
                skillDto.nextLevel = skillList[i].NextLevel;
                skillDto.coldTime = skillList[i].ColdTime;
                skillDto.range = skillList[i].Range;
                skillDto.applyValue = skillList[i].ApplyValue;
                skillDto.applyTime = skillList[i].ApplyTime;
                skillDto.mp = skillList[i].Mp;
                skillDto.dis = skillList[i].Dis;
                skillDto.back = skillList[i].Back;
                skillDto.skillId = skillList[i].SkillId;
                SkillModelDTO skillModelDto = new SkillModelDTO();
                skillModelDto.code = skillList[i].Code;
                skillModelDto.name = skillList[i].Name;
                SkillInitial skillInitial = SkillInitialProperty.mapSkill[skillList[i].SkillId];
                skillModelDto.info = skillInitial.info;
                skillModelDto.icon_name = skillInitial.icon_name;
                skillModelDto.applyType = skillInitial.applyType;
                skillModelDto.applyProperty = skillInitial.applyProperty;
                skillModelDto.releaseType = skillInitial.releaseType;
                skillModelDto.efx_name = skillInitial.efx_name;
                skillModelDto.aniname = skillInitial.aniname;

                skillDto.SkillModelDto = skillModelDto;
                skillDtos.Add(skillDto);
            }
            return skillDtos;
        }

        SKILL GetSkill(SkillDTO skillDto)
        {
            SKILL skill = new SKILL();
            skill.Id = skillDto.id;
            skill.UserId = skillDto.UserId;
            skill.SkillId = skillDto.skillId;
            skill.ShortcutId = skillDto.shortcutId;
            skill.Code = skillDto.SkillModelDto.code;
            skill.Level = skillDto.level;
            skill.NextLevel = skillDto.nextLevel;
            skill.ColdTime = skillDto.coldTime;
            skill.Name = skillDto.SkillModelDto.name;
            skill.Range = skillDto.range;
            skill.ApplyValue = skillDto.applyValue;
            skill.ApplyTime = skillDto.applyTime;
            skill.Mp = skillDto.mp;
            skill.Dis = skillDto.dis;
            skill.Back = skillDto.back;

            return skill;
        }

        SkillDTO GetSkillDto(SKILL skill)
        {
            SkillDTO skillDto = new SkillDTO();
            skillDto.id = skill.Id;
            skillDto.UserId = skill.UserId;
            skillDto.shortcutId = skill.ShortcutId;
            skillDto.nextLevel = skill.NextLevel;
            skillDto.coldTime = skill.ColdTime;
            skillDto.range = skill.Range;
            skillDto.applyValue = skill.ApplyValue;
            skillDto.applyTime = skill.ApplyTime;
            skillDto.mp = skill.Mp;
            skillDto.dis = skill.Dis;
            skillDto.back = skill.Back;
            skillDto.skillId = skill.SkillId;
            SkillModelDTO skillModelDto = new SkillModelDTO();
            skillModelDto.code = skill.Code;
            skillModelDto.name = skill.Name;
            SkillInitial skillInitial = SkillInitialProperty.mapSkill[skillDto.skillId];
            skillModelDto.info = skillInitial.info;
            skillModelDto.icon_name = skillInitial.icon_name;
            skillModelDto.applyType = skillInitial.applyType;
            skillModelDto.applyProperty = skillInitial.applyProperty;
            skillModelDto.releaseType = skillInitial.releaseType;
            skillModelDto.efx_name = skillInitial.efx_name;
            skillModelDto.aniname = skillInitial.aniname;

            skillDto.SkillModelDto = skillModelDto;
            return skillDto;
        }

     
    }
}
