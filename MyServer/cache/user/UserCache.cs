using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.dao;
using MyServer.tool;
using NetFrame;
using Protocols.dto;

namespace MyServer.cache.user
{
    public class UserCache : IUserCache
    {
        //注册就会存在的字典
        public int index = 1;
        public Dictionary<int, List<int>> accIdToRoleListId = new Dictionary<int, List<int>>();
        public Dictionary<int, USER> idToUser = new Dictionary<int, USER>();
        public Dictionary<string,USER> nameToUser=new Dictionary<string, USER>(); 

       //上线才会存在的字典
        public Dictionary<int, UserToken> IdToToken = new Dictionary<int, UserToken>();
        public Dictionary<UserToken, int> tokenToId = new Dictionary<UserToken, int>();
        public Dictionary<int,USER> accIdToUser=new Dictionary<int, USER>();


        public int GetUserId(UserToken token)
        {
            if(tokenToId.ContainsKey(token))
            return tokenToId[token];
            return -1;
        }

        /// <summary>
        /// 根据用户id获取链接对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserToken GeTokenById(int id)
        {
            return IdToToken[id];
        }
        /// <summary>
        /// 根据id获取角色对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       public USER GetUserByToken(UserToken token) 
        {
            int id = tokenToId[token];
            USER user= GetUserById(id);
            return user;
        
        }
       public USER GetUserById(int id)
        {
           if (idToUser.ContainsKey(id))
           {
               return idToUser[id];
           }
           return null;
        }
        /// <summary>
        /// 根据账号id获取角色数量
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public int GetRoleCount( int accountId)
        {
            List<int> roleList;
            accIdToRoleListId.TryGetValue(accountId, out roleList);
            if (roleList != null)
            {
                return roleList.Count;

            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据账号id获取角色列表
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public List<USER> GetRoleList(int accountId)
        {
            List<int> roleList;
            List<USER> userList=new List<USER>();
            accIdToRoleListId.TryGetValue(accountId,out roleList);
            if (roleList != null)
            {
                for (int i = 0; i < roleList.Count; i++)
                {
                    userList.Add(idToUser[roleList[i]]);
                }
                return userList;
            }
            return null;
        }

        /// <summary>
        /// 根据用户名字获取用户，返回是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool GetUserByName(string name)
        {
            return nameToUser.ContainsKey(name);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="accountId">绑定的账号id</param>
        /// <param name="name">角色名字</param>
        /// <param name="modelName">模型名</param>
        /// <returns></returns>
        public bool AddRole(int accountId,string name, int modelName)
        {
            ModelInitialProperty modelInitialProperty = new ModelInitialProperty();
            ModelInitial modelInitial = modelInitialProperty.modelToInitial[modelName];//模型初始属性
            USER user = new USER();
            user.Id = index;
            user.AccountId = accountId;
            user.Exp = 0;
            user.Level = 1;
            user.Name = name;
            user.ModelName = modelName;
            user.Map = 3;
            user.Attack = modelInitial.attack;
            user.Def = modelInitial.def;
            user.Armour = modelInitial.armour;
            user.Crit = modelInitial.crit;
            user.ExemptCrit = modelInitial.exemptCrit;
            user.Hp = modelInitial.hp;
            user.MaxHp = modelInitial.maxHp;
            user.Mp = modelInitial.mp;
            user.MaxMp = modelInitial.maxMp;
            user.Speed = modelInitial.speed;
            user.SkillPoint = 0;
            user.PropertyPoint = 0;
            user.Money = 0;
            for (int i = 0; i < modelInitial.skillIDs.Length; i++)
            {
                if (i == modelInitial.skillIDs.Length - 1)
                {
                    user.SkillIds += modelInitial.skillIDs[i].ToString();
                }
                else
                {
                    user.SkillIds += modelInitial.skillIDs[i] + ",";

                }
            }
            for (int i = 0; i < modelInitial.equips.Length; i++)
            {
                if (i == modelInitial.equips.Length - 1)
                {
                    user.Equips += modelInitial.equips[i].ToString();
                }
                else
                {
                    user.Equips += modelInitial.equips[i] + ",";

                }
            }
            index++;
            idToUser.Add(user.Id, user);
            nameToUser.Add(name,user);
            List<int> roleList;
            accIdToRoleListId.TryGetValue(accountId, out roleList);
            if (roleList != null)
            {
                roleList.Add(user.Id);
            }
            else
            {
                List<int> roles = new List<int>();
                roles.Add(user.Id);
                accIdToRoleListId.Add(accountId, roles);
            }
            return true;
        }



        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="roleId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool DeleteRole(int accountId, int roleId,string name)
        {
            List<int> roleList = new List<int>();
            accIdToRoleListId.TryGetValue(accountId, out roleList);
            if (roleList != null) roleList.Remove(roleId);

            idToUser.Remove(roleId);
            nameToUser.Remove(name);
            return true;
        }

        /// <summary>
        /// 获取当前账号id上线的角色 
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public USER GetUserByAccId(int accountId)
        {
            if(accIdToUser.ContainsKey(accountId)) return accIdToUser[accountId];
            return null;
        }

        /// <summary>
        /// 角色是否上线
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsOnLine(int userId)
        {
            return IdToToken.ContainsKey(userId);
        }

        /// <summary>
        /// 角色上线
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public USER OnLine(UserToken token,int userId,int accountId)
        {
            if (IdToToken.ContainsKey(userId))
                IdToToken.Remove(userId);
            if (tokenToId.ContainsKey(token))
                tokenToId.Remove(token);
            if (accIdToUser.ContainsKey(accountId))
                accIdToUser.Remove(accountId);

            IdToToken.Add(userId, token);
            tokenToId.Add(token, userId);
            accIdToUser.Add(accountId,idToUser[userId]);
            return idToUser[userId];
        }

        /// <summary>
        /// 角色下线
        /// </summary>
        /// <param name="token"></param>
        /// <param name="accountId"></param>
        public void OffLine(UserToken token, int accountId)
        {
            if (tokenToId.ContainsKey(token))
            {
                if (IdToToken.ContainsKey(tokenToId[token]))
                {
                    IdToToken.Remove(tokenToId[token]);
                }
                tokenToId.Remove(token);
            }
            if (accIdToUser.ContainsKey(accountId))
                accIdToUser.Remove(accountId);
        }
    }
}
