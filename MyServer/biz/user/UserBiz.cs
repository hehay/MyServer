using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.biz.accaount;
using MyServer.biz.pos;
using MyServer.cache;
using MyServer.cache.accaount;
using MyServer.cache.user;
using MyServer.dao;
using NetFrame;
using Protocols.dto;

namespace MyServer.biz.user
{
    public class UserBiz:IUserBiz
    {
        public IUserCache UserCache = cacheFactory.UserCache;
        public IAccountBiz AccountBiz = BizFactory.accountBiz;
        public IPosBiz PosBiz = BizFactory.PosBiz;
        public int CreatRole(UserToken token,string name,int modelName)
        {
            int accountId = AccountBiz.GetAccountId(token);//获取不到此用户
            if (accountId == -1) return -1;
            if (UserCache.GetRoleCount(accountId) >= 4) return -2;//角色达到限定数量

            if (UserCache.GetUserByName(name)) return -3;//已有角色名称存在

            UserCache.AddRole(accountId, name, modelName);
            return 1;//创建成功
        }
        public void GetMatchPlayer(UserToken token,int model, out List<MatchDTO> players, out List<UserToken> tokens)
        {
            int accountId = AccountBiz.GetAccountId(token);
            players= UserCache.GetMatchPlayer(accountId,model);
            if (players != null) 
            {
                List<UserToken> userTokens = new List<UserToken>();
                for (int i = 0; i < players.Count; i++)
                {
                    UserToken userToken = AccountBiz.GetToken(players[i].accountId);
                    userTokens.Add(userToken);
                }
                tokens = userTokens;
            }
            else tokens = null;

        }
        public void MatchConfirm(UserToken token, int model, out int confirmCount, out List<UserToken> tokens) 
        {
            int accountId = AccountBiz.GetAccountId(token);
            List<MatchDTO> players = UserCache.MatchConfirm(accountId, model);
            int count = 0;
            if (players != null)
            {
                List<UserToken> userTokens = new List<UserToken>();
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].hasConfirm) count++;
                    UserToken userToken = AccountBiz.GetToken(players[i].accountId);
                    userTokens.Add(userToken);
                }
                tokens = userTokens;
                confirmCount = count;
            }
            else
            {
                tokens = null;
                confirmCount = count;
            } 
        }
        public void MatchResult(UserToken token,int model,out int result, out List<UserToken> tokens)
        {
            int accountId = AccountBiz.GetAccountId(token);
            List<MatchDTO> players = UserCache.GetCompose(accountId,model);
            if (players != null)
            {
                List<UserToken> userTokens = new List<UserToken>();
                bool allConfirm = true;
                for (int i = 0; i < players.Count; i++)
                {
                    if (!players[i].hasConfirm) allConfirm = false;
                    UserToken userToken = AccountBiz.GetToken(players[i].accountId);
                    userTokens.Add(userToken);
                }
                tokens = userTokens;
                if (allConfirm) result = 1;
                else 
                {
                    result = 0;
                    UserCache.DestroyCompose(accountId,model);
                } 
            }
            else 
            {
                tokens = null;
                result = 0;
            }


        }
        public void StopMatchPlayer(UserToken token,int model)
        {
            int accountId = AccountBiz.GetAccountId(token);
            UserCache.StopMatchPlayer(accountId,model);
        }
        public UserDTO GetUserDtoByToken(UserToken token)
        {
            //USER user= UserCache.GetUserByToken(token);
            UserDTO dto = new UserDTO 
            { 
              gold=10
            };
            return dto;
        }
        public int DelectRole(UserToken token,  int roleId,string name)
        {
            int accountId = AccountBiz.GetAccountId(token);//获取不到此用户
            if (accountId == -1) return -1;
            if (UserCache.GetRoleCount(accountId) <=0) return -2;//此账号没有角色
            UserCache.DeleteRole(accountId, roleId, name);
            return 1;
        }

        public UserDTO OnLine(UserToken token, int userId)
        {
            int accountId = AccountBiz.GetAccountId(token);//获取不到此用户
            if (accountId == -1) return null ;
            if (UserCache.IsOnLine(userId)) return null;//角色已登录

           USER user= UserCache.OnLine(token, userId, accountId);
           return GetUserDtoToUSER(user);
        }

        public void OffLine(UserToken token)
        {
            int accountId = AccountBiz.GetAccountId(token);//获取不到此用户
            if (accountId == -1) return ;
            UserCache.OffLine(token,accountId);
        }
        public List<UserDTO> GetRoleList(UserToken token)
        {
            int accountId = AccountBiz.GetAccountId(token);//获取不到此用户
            if (accountId == -1) return null;
          List<USER> roleList=  UserCache.GetRoleList(accountId);
            List<UserDTO> roleDtos=new List<UserDTO>();
            if (roleList != null)
            {
                for (int i = 0; i < roleList.Count; i++)
                {
                    roleDtos.Add(GetUserDtoToUSER(roleList[i]));
                }
                return roleDtos;
            }
            else
            {
                return null;
            }
        }


       public  USER GetUserToUserDTO(UserDTO userDto)
        {
            USER user=new USER();
            user.Id = userDto.id;
            user.AccountId = userDto.accountId;
            user.Level = userDto.level;
            user.Exp = userDto.exp;
            user.Name = userDto.name;
            user.ModelName = userDto.modelName;
            user.Map = userDto.map;
            user.Attack = userDto.attack;
            user.Def = userDto.def;
            user.Armour = userDto.armour;
            user.Crit = userDto.crit;
            user.ExemptCrit = userDto.exemptCrit;
            user.Hp = userDto.hp;
            user.MaxHp = userDto.maxHp;
            user.Mp = userDto.mp;
            user.MaxHp = userDto.maxMp;
            user.Speed = userDto.speed;
            user.SkillPoint = userDto.skillPoint;
            user.PropertyPoint = userDto.propertyPoint;
            user.Money = userDto.money;
            for (int i = 0; i < userDto.skillids.Length; i++)
            {
                if (i == userDto.skillids.Length - 1)
                {
                    user.SkillIds += userDto.skillids[i].ToString();
                }
                else
                {
                    user.SkillIds += userDto.skillids[i] + ",";

                }
            }
            for (int i = 0; i < userDto.equips.Length; i++)
            {
                if (i == userDto.equips.Length - 1)
                {
                    user.Equips += userDto.equips[i].ToString();
                }
                else
                {
                    user.Equips += userDto.equips[i] + ",";

                }
            }
            return user;
        }


       public UserDTO GetUserDtoToUSER(USER user)
        {
            UserDTO userDto = new UserDTO();
            userDto.id = user.Id;
            userDto.accountId = user.AccountId;
            userDto.level = user.Level;
            userDto.exp = user.Exp;
            userDto.name = user.Name;
            userDto.modelName = user.ModelName;
            userDto.map = user.Map;
            userDto.attack = user.Attack;
            userDto.def = user.Def;
            userDto.armour = user.Armour;
            userDto.crit = user.Crit;
            userDto.exemptCrit = user.ExemptCrit;
            userDto.hp = user.Hp;
            userDto.maxHp = user.MaxHp;
            userDto.mp = user.Mp;
            userDto.maxMp = user.MaxMp;
            userDto.speed = user.Speed;
            userDto.skillPoint = user.SkillPoint;
            userDto.propertyPoint = user.PropertyPoint;
            userDto.money = user.Money;
            string[] skills = user.SkillIds.Split(',');
            List<int> skillList=new List<int>();
            for (int i = 0; i < skills.Length; i++)
            {
                skillList.Add(int.Parse(skills[i]));
            }
            userDto.skillids = skillList.ToArray();

            string[] equips = user.Equips.Split(',');
            List<int> equipList = new List<int>();
            for (int i = 0; i < equips.Length; i++)
            {
                equipList.Add(int.Parse(equips[i]));
            }
            userDto.equips = equipList.ToArray();
            return userDto;
        }
    }
}
