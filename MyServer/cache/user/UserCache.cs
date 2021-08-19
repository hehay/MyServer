using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
        public Dictionary<int, USER> accountIdAndUser = new Dictionary<int, USER>();
        public Dictionary<string,USER> nameToUser=new Dictionary<string, USER>(); 

       //上线才会存在的字典
        public Dictionary<int, UserToken> IdToToken = new Dictionary<int, UserToken>();
        public Dictionary<UserToken, int> tokenToId = new Dictionary<UserToken, int>();
        public Dictionary<int,USER> accIdToUser=new Dictionary<int, USER>();
        public List<List<MatchDTO>> singlePool = new List<List<MatchDTO>>();
        public List<List<MatchDTO>> multiPool = new List<List<MatchDTO>>();
        public List<List<MatchDTO>> fivePool = new List<List<MatchDTO>>();
        public List<HERO> heros = new List<HERO>();

        private string _path;
        private XmlDocument heroFile=new XmlDocument();

        public UserCache(string path) 
        {
            _path = path;
            ReadHeroFile();
            
            List<MatchDTO> compose = new List<MatchDTO>();
            MatchDTO matchDto1 = new MatchDTO
            {
                accountId = 1,
                index = 0,
                hasConfirm = true

            };
            MatchDTO matchDto2 = new MatchDTO
            {
                accountId = 2,
                index = 1,
                hasConfirm = true
            };
            compose.Add(matchDto1);
            compose.Add(matchDto2);
            singlePool.Add(compose);
        }
        public void ReadHeroFile()
        {
            if (!File.Exists(_path))
            {
                XmlElement root = heroFile.CreateElement("Root");
                heroFile.AppendChild(root);
                XmlElement hero1 = heroFile.CreateElement("Hero");
                hero1.SetAttribute("HeroId", "100");
                hero1.SetAttribute("HeroName", "Ake");
                hero1.SetAttribute("第一职业", "刺客");
                hero1.SetAttribute("第二职业","");
                //account.SetAttribute("密码", "1234");
                root.AppendChild(hero1);
                //XmlElement account1 = AccountDoc.CreateElement("Account");
                //account1.SetAttribute("id", "002");
                //account1.SetAttribute("账号", "0002");
                //account1.SetAttribute("密码", "1234");
                //root.AppendChild(account1);
                heroFile.Save(_path);

            }
            heroFile.Load(_path);
            XmlNodeList nodeList = heroFile.SelectSingleNode("Root").ChildNodes;
            foreach (XmlElement node in nodeList)
            {
                HERO hero = new HERO();
                hero.HeroId = int.Parse(node.GetAttribute("HeroId"));
                hero.HeroName = node.GetAttribute("HeroName");
                hero.FirstOccupation = node.GetAttribute("第一职业");
                hero.SecondOccupation = node.GetAttribute("第二职业");
                heros.Add(hero);
            }
        }
        public void WriteHeroFile()
        {
            heroFile.RemoveAll();
            XmlElement root = heroFile.CreateElement("Root");
            heroFile.AppendChild(root);
            foreach (var a in heros)
            {
                XmlElement hero = heroFile.CreateElement("Hero");
                hero.SetAttribute("HeroId", a.HeroId.ToString());
                hero.SetAttribute("HeroName", a.HeroName);
                root.AppendChild(hero);
            }
            heroFile.Save(_path);
        }

        public List<MatchDTO> SelectHero(int accountId, int model, MatchDTO matchDTO) 
        {
            switch (model) 
            {
                case 0:
                    for (int i = 0; i < singlePool.Count; i++)
                    {
                        List<MatchDTO> players = singlePool[i];
                        for (int j = 0; j < 2; j++)
                        {
                            MatchDTO matchDto = players[j];
                            if (matchDto.accountId == accountId)
                            {
                                matchDto.heroId = matchDTO.heroId;
                                return singlePool[i];
                            }
                           
                        }
                    }
                    return null;
                case 1:
                    for (int i = 0; i < multiPool.Count; i++)
                    {
                        List<MatchDTO> players = multiPool[i];
                        for (int j = 0; j < 6; j++)
                        {
                            MatchDTO matchDto = players[j];
                            if (matchDto.accountId == 0)
                            {
                                matchDto.heroId = matchDTO.heroId;
                                return singlePool[i];
                            }
                        }
                    }
                    return null;
                default:
                    return null;
            }
        
        }
        public List<MatchDTO> GetMatchPlayer(int accountId,int model) 
        {
            
            switch (model) 
            {
                case 0:
                    for (int i = 0; i < singlePool.Count; i++)
                    {
                        List<MatchDTO> players = singlePool[i];
                        for (int j = 0; j < 2; j++)
                        {
                            MatchDTO matchDto = players[j];
                            if (matchDto.accountId==accountId) 
                            {
                                return null;
                                //断线重连todo
                            }
                            if (matchDto.accountId== 0) 
                            {
                                matchDto.accountId = accountId;
                                if (j == 1)
                                {
                                    return players;
                                }
                                else return null;
                            }
                        }
                    }
                    List<MatchDTO> compose = new List<MatchDTO>();
                    for (int i = 0; i < 2; i++)
                    {
                        MatchDTO matchDto = new MatchDTO
                        {
                            accountId = 0,
                            index = i,
                            hasConfirm=false
                 
                        };
                        compose.Add(matchDto);
                    }
                    compose[0].accountId = accountId;
                    singlePool.Add(compose);
                    return null;
                case 1:
                    for (int i = 0; i < multiPool.Count; i++)
                    {
                        List<MatchDTO> players = multiPool[i];
                        for (int j = 0; j < 6; j++)
                        {
                            MatchDTO matchDto = players[j];
                            if (matchDto.accountId == 0)
                            {
                                matchDto.accountId = accountId;
                                if (j == 5)
                                {
                                    return players;
                                }
                                else return null;
                            }
                        }
                    }
                    compose = new List<MatchDTO>();
                    for (int i = 0; i < 6; i++)
                    {
                        MatchDTO matchDto = new MatchDTO
                        {
                            accountId = 0,
                            index = i,
                            hasConfirm = false

                        };
                    }
                    compose[0].accountId = accountId;
                    multiPool.Add(compose);
                    return null;
                default:
                    return null;
            }
        }
        public List<MatchDTO> GetCompose(int accountId,int model)
        {
            switch (model)
            {
                case 0:
                    for (int i = 0; i < singlePool.Count; i++)
                    {
                        List<MatchDTO> players = singlePool[i];
                        for (int j = 0; j < 2; j++)
                        {
                            MatchDTO matchDto = players[j];
                            if (matchDto.accountId == accountId)
                            {
                                return players;
                            }
                        }
                    }
                    return null;
                case 1:
                    for (int i = 0; i < multiPool.Count; i++)
                    {
                        List<MatchDTO> players = multiPool[i];
                        for (int j = 0; j < 6; j++)
                        {
                            MatchDTO matchDto = players[j];
                            if (matchDto.accountId == accountId)
                            {
                                return players;
                            }
                        }
                    }
                    return null;
                default:
                    return null;
            }
        }
        public void DestroyCompose(int accountId ,int model)
        {
            bool hasGet = false;
            switch (model)
            {
                case 0:
                    
                    for (int i = 0; i < singlePool.Count; i++)
                    {
                        List<MatchDTO> players = singlePool[i];
                        for (int j = 0; j < 2; j++)
                        {
                            MatchDTO matchDto = players[j];
                            if (matchDto.accountId == accountId)
                            {
                                hasGet = true;
                                players.Clear();
                                break;
                            }
                        }
                        if (hasGet) 
                        {
                            singlePool.Remove(players);
                            break;
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < multiPool.Count; i++)
                    {
                        List<MatchDTO> players = multiPool[i];
                        for (int j = 0; j < 6; j++)
                        {
                            MatchDTO matchDto = players[j];
                            if (matchDto.accountId == accountId)
                            {
                                hasGet = true;
                                players.Clear();
                                break;
                            }
                        }
                        if (hasGet)
                        {
                            multiPool.Remove(players);
                        }
                    }
                    break;
                default:
                    break;
            }

        }
        public List<MatchDTO> MatchConfirm(int accountId, int model) 
        {
            switch (model)
            {
                case 0:
                    for (int i = 0; i < singlePool.Count; i++)
                    {
                        List<MatchDTO> players = singlePool[i];
                        for (int j = 0; j < 2; j++)
                        {
                            MatchDTO matchDto = players[j];
                            if (matchDto.accountId == accountId)
                            {
                                matchDto.hasConfirm = true;
                                return players;
                            }
                        }
                    }
                    return null;
                case 1:
                    for (int i = 0; i < multiPool.Count; i++)
                    {
                        List<MatchDTO> players = multiPool[i];
                        for (int j = 0; j < 6; j++)
                        {
                            MatchDTO matchDto = players[j];
                            if (matchDto.accountId == accountId)
                            {
                                matchDto.hasConfirm = true;
                                return players;
                            }
                        }
                    }
                    return null;
                default:
                    return null;
            }

        }
        public void StopMatchPlayer(int accountId,int model) 
        {
            switch (model)
            {
                case 0:
                    for (int i = 0; i < singlePool.Count; i++)
                    {
                        List<MatchDTO> players = singlePool[i];
                        for (int j = 0; j < 2; j++)
                        {
                            MatchDTO matchDto = players[j];
                            if (matchDto.accountId == accountId)
                            {
                                matchDto.accountId = 0;
                                matchDto.hasConfirm = false;
                                return;
                            }
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < multiPool.Count; i++)
                    {
                        List<MatchDTO> players = multiPool[i];
                        for (int j = 0; j < 6; j++)
                        {
                            MatchDTO matchDto = players[j];
                            if (matchDto.accountId == accountId)
                            {
                                matchDto.accountId = 0;
                                matchDto.hasConfirm = false;
                                return;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

        }
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
           if (accountIdAndUser.ContainsKey(id))
           {
               return accountIdAndUser[id];
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
                    userList.Add(accountIdAndUser[roleList[i]]);
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
        public void CreateUser(int accountId,string nickname) 
        {
            USER user = new USER
            {
                
                AccountId = accountId,
                Gold = 20000, 
                Exp = 0,
                Nickname = nickname,
                diamond
            this.map = map;
            this.attack = attack;
            this.def = def;
            this.armour = armour;
            this.crit = crit;
            this.exemptCrit = exemptCrit;
            this.hp = hp;
            this.maxHp = maxHp;
            this.mp = mp;
            this.maxMp = maxMp;
            this.speed = speed;
            this.skillPoint = skillPoint;
            this.propertyPoint = propertyPoint;
            this.money = money;
            this.skillids = skillids;
            this.equips = equips;
        };
        }
        

        public bool AddRole(int accountId,string name, int modelName)
        {
            ModelInitialProperty modelInitialProperty = new ModelInitialProperty();
            ModelInitial modelInitial = modelInitialProperty.modelToInitial[modelName];//模型初始属性
            USER user = new USER();
            user.Id = index;
            user.AccountId = accountId;
            user.Exp = 0;
            user.Level = 1;
            user.Nickname = name;
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
            accountIdAndUser.Add(user.Id, user);
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

            accountIdAndUser.Remove(roleId);
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
            accIdToUser.Add(accountId,accountIdAndUser[userId]);
            return accountIdAndUser[userId];
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
