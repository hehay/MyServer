using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.biz;
using MyServer.biz.inventory;
using MyServer.biz.pos;
using MyServer.biz.skill;
using MyServer.biz.user;
using MyServer.cache;
using MyServer.cache.accaount;
using MyServer.cache.inventory;
using MyServer.cache.user;
using MyServer.dao;
using MyServer.tool;
using NetFrame;
using Protocols;
using Protocols.dto;

namespace MyServer.logic
{
    public class MapRoom:AbsMulitHandler,HandlerInterface
    {
        Dictionary<int,AbsRoleModel> mapModel=new Dictionary<int, AbsRoleModel>(); 
        List<AbsRoleModel> models=new List<AbsRoleModel>(); 
        private IUserCache userCache = cacheFactory.UserCache;
        private UserBiz userBiz = new UserBiz();
        private ISkillBiz skillBiz = BizFactory.SkillBiz;
        private IinventoryBiz inventoryBiz = BizFactory.inventoryBiz;
        private IInventoryCache inventoryCache = cacheFactory.InventoryCache;
        public void ClientClose(NetFrame.UserToken token, string error)
        {
        LeaveMap(token);
        }

        public void MessageReceive(NetFrame.UserToken token, NetFrame.Auto.SocketModel message)
        {
            switch (message.command)
            {
                case MapProtocol.EnterMap_CREQ:
                    EnterMap(token, message.GetMessage<UserDTO>());
                    break;
                case MapProtocol.LeaveMap_CREQ:
                    LeaveMap(token);
                    break;
                case MapProtocol.Move_CREQ:
                    Move(token,message.GetMessage<MoveDto>());
                    break;
                case MapProtocol.Attack_CREQ:
                    Attack(token,message.GetMessage<int[]>());
                    break;
                case MapProtocol.Damage_CREQ:
                    Damage(token,message.GetMessage<DamageDTO>());
                    break;
                case MapProtocol.Skill_CREQ:
                    Skill(token,message.GetMessage<SkillAttackDTO>());
                    break;
                case MapProtocol.UseInventory_CREQ:
                    UseInventory(token,message.GetMessage<int>());
                    break;
                case MapProtocol.UnUseInventory_CREQ:
                    UnUseInventory(token, message.GetMessage<int>());
                    break;
                case MapProtocol.Talk_CREQ:
                    List<UserToken> userTokens = userBiz.GetCompose(token);
                    Talk(userTokens, token, message.GetMessage<TalkDTO>());
                    break;
            }
        }

        void Talk(List<UserToken> userTokens, UserToken token, TalkDTO talkDto)
        {
            talkDto.userid = userCache.GetUserId(token);
            talkDto.userName = userCache.GetUserById(talkDto.userid).Nickname;
            if (talkDto.receiverid >= 0&&talkDto.talkType==TalkType.One)
            {
                Write(token, MapProtocol.Talk_SRES, talkDto); //发送给自己
                Write(talkDto.receiverid, MapProtocol.Talk_SRES, talkDto); //发送给接收方
            }
            else
            {
                Brocast(userTokens, MapProtocol.Talk_BRO,talkDto);
            }
        }


        void UseInventory(UserToken token, int itemId)
        {
            USER user = userCache.GetUserById(userCache.GetUserId(token));
            UserDTO userDto = (UserDTO)GetRoleModel(user.Id);

            List<int> equips = new List<int>();
            string[] equipStrings = user.Equips.Split(',');
            for (int i = 0; i < equipStrings.Length; i++)
            {
                equips.Add(int.Parse(equipStrings[i]));
            }

            ExecutorPool.Instance.Executor(delegate
            {
                INVENTORY inventory = inventoryCache.GetInventory(itemId);
                InventoryItemDTO itemDto = inventoryBiz.UseInventory(token, itemId);
                if (itemDto != null)
                {
                    switch (itemDto.inventory.inventoryType)
                    {
                        case InventoryType.Equip:
                            inventory.IsDressed = true;
                            itemDto.IsDressed = true;
                            user.Attack += itemDto.attack;
                            user.Def += itemDto.def;
                            user.Armour += itemDto.armour;
                            user.Crit += itemDto.crit;
                            user.ExemptCrit += itemDto.exemptCrit;
                            user.MaxHp += itemDto.hp;
                            user.MaxMp += itemDto.mp;
                            user.Speed += itemDto.speed;
                            userDto.attack += itemDto.attack;
                            userDto.def += itemDto.def;
                            userDto.armour += itemDto.armour;
                            userDto.crit += itemDto.crit;
                            userDto.exemptCrit += itemDto.exemptCrit;
                            userDto.maxHp += itemDto.hp;
                            userDto.maxMp += itemDto.mp;
                            userDto.speed += itemDto.speed;
                            equips[(int)itemDto.inventory.equipType] = itemDto.id;
                            user.Equips = String.Empty;
                            userDto.equips = equips.ToArray();
                            for (int i = 0; i < equips.Count; i++)
                            {
                                if (i == equips.Count - 1)
                                {
                                    user.Equips += equips[i].ToString();
                                }
                                else
                                {
                                    user.Equips += equips[i] + ",";
                                }
                            }
                            itemDto.shortcutid = -1;
                            inventory.ShortcutId = -1;
                            break;
                        case InventoryType.Drug:
                            switch (itemDto.inventory.infoType)
                            {
                                case InfoType.Hp:
                                    user.Hp += itemDto.inventory.applyValue;
                                    userDto.hp += itemDto.inventory.applyValue;
                                    if (user.Hp >= user.MaxHp) user.Hp = user.MaxHp;
                                    if (userDto.hp >= userDto.maxHp) userDto.hp = userDto.maxHp;
                                    break;
                                case InfoType.Mp:
                                    user.Mp += itemDto.inventory.applyValue;
                                    if (user.Mp >= user.MaxMp) user.Mp = user.MaxMp;
                                    userDto.mp += itemDto.inventory.applyValue;
                                    if (userDto.mp >= userDto.maxMp) userDto.mp = userDto.maxMp;
                                    break;
                                case InfoType.Exp:
                                    user.Exp += itemDto.inventory.applyValue;
                                    int total_exp = 100 + user.Level * 30;
                                    while (user.Exp >= total_exp)
                                    {
                                        // 升级
                                        user.Level++;
                                        userDto.level++;
                                        user.Exp -= total_exp;
                                        userDto.exp = user.Exp;
                                        user.Hp += user.Level*50;
                                        user.MaxHp += user.Level * 50;
                                        user.Mp += user.Level*25;
                                        user.MaxMp += user.Level * 25;
                                        userDto.hp += userDto.level * 50;
                                        userDto.maxHp += userDto.level * 50;
                                        userDto.mp += userDto.level * 25;
                                        userDto.maxMp += userDto.level * 25;
                                        total_exp = 100 + user.Level * 30;
                                    }
                                    break;
                            }
                            itemDto.count -= 1;
                            if (itemDto.count <= 0)
                            {
                                inventoryBiz.DeleteInventory(token, itemDto.id);
                            }
                           
                            break;
                        case InventoryType.Box:
                            break;
                    }
                    List<UserToken> tokens = userBiz.GetCompose(token);
                    Brocast(tokens, MapProtocol.UseInventory_BRO,itemDto);
                }
            });
        }

        void UnUseInventory(UserToken token, int itemId)
        {
            USER user = userCache.GetUserById(userCache.GetUserId(token));
            UserDTO userDto = (UserDTO)GetRoleModel(user.Id);

            List<int> equips = new List<int>();
            string[] equipStrings = user.Equips.Split(',');
            for (int i = 0; i < equipStrings.Length; i++)
            {
                equips.Add(int.Parse(equipStrings[i]));
            }

            ExecutorPool.Instance.Executor(delegate
            {
                InventoryItemDTO itemDto = inventoryBiz.UseInventory(token, itemId);
                INVENTORY inventory = inventoryCache.GetInventory( itemId);

                if (itemDto != null)
                {
                    switch (itemDto.inventory.inventoryType)
                    {
                        case InventoryType.Equip:
                            inventory.IsDressed = false;
                            itemDto.IsDressed = false;
                             user.Attack -= itemDto.attack;
                            user.Def -= itemDto.def;
                            user.Armour -= itemDto.armour;
                            user.Crit -= itemDto.crit;
                            user.ExemptCrit -= itemDto.exemptCrit;
                            user.MaxHp -= itemDto.hp;
                            user.MaxMp -= itemDto.mp;
                            user.Speed -= itemDto.speed;
                            userDto.attack -= itemDto.attack;
                            userDto.def -= itemDto.def;
                            userDto.armour -= itemDto.armour;
                            userDto.crit -= itemDto.crit;
                            userDto.exemptCrit -= itemDto.exemptCrit;
                            userDto.maxHp -= itemDto.hp;
                            userDto.maxMp -= itemDto.mp;
                            userDto.speed -= itemDto.speed;
                            equips[(int)itemDto.inventory.equipType] = 0;
                            userDto.equips = equips.ToArray();
                            user.Equips = String.Empty;
                            for (int i = 0; i < equips.Count; i++)
                            {
                                if (i == equips.Count - 1)
                                {
                                    user.Equips += equips[i].ToString();
                                }
                                else
                                {
                                    user.Equips += equips[i] + ",";
                                }
                            }
                           
                            break;
                        default:
                          inventoryBiz.DeleteInventory(token, itemDto.id);
                            break;
                    }
                    List<UserToken> tokens = userBiz.GetCompose(token);
                    Brocast(tokens, MapProtocol.UnUseInventory_SRES, itemDto);
                }
            });

        }

        void Attack(UserToken token, int[] targetsId)
        {
            AttackDTO attackDto=new AttackDTO();
            int userId = userCache.GetUserId(token);
            attackDto.userId = userId;
            attackDto.targetsId = targetsId;
            List<UserToken> tokens = userBiz.GetCompose(token);
            Brocast(tokens,MapProtocol.Attack_BRO,attackDto);
        }

        void Skill(UserToken token, SkillAttackDTO skillAttackDto)
        {
            skillAttackDto.userId = userCache.GetUserId(token);
            USER user=userCache.GetUserById(userCache.GetUserId(token));
            if (user.Mp < skillAttackDto.skillDto.mp)
            {
                skillAttackDto = null;
            }
            if (skillAttackDto != null)
            {
                UserDTO userDto = (UserDTO)GetRoleModel(skillAttackDto.userId);
                userDto.mp -= skillAttackDto.skillDto.mp;
                if (userDto.mp <= 0) userDto.mp = 0;
                user.Mp -= skillAttackDto.skillDto.mp;
                if (user.Mp <= 0) user.Mp = 0;
                List<UserToken> tokens = userBiz.GetCompose(token);
                Brocast(tokens,MapProtocol.Skill_BRO, skillAttackDto);
            }
        }
        int[] inventorys = new[] { 1001, 1002, 1003, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008 };
        Random random = new Random();

        void Damage(UserToken token, DamageDTO damageDto)
        {
            List<UserToken> tokens = userBiz.GetCompose(token);
            int userId = userCache.GetUserId(token);
            AbsRoleModel attRoleModel = GetRoleModel(userId);;
            damageDto.userid = userId;

            List<int[]> damages = new List<int[]>();

            for (int i = 0; i < damageDto.targets.Length; i++)
            {
                AbsRoleModel targetRoleModel = GetRoleModel(damageDto.targets[i][0]);

                GetValue(token, userId, damageDto.skill, ref attRoleModel, ref targetRoleModel, ref damages);
              
                if (targetRoleModel.hp <= 0)
                {
                    string kill = "";
                    ExecutorPool.Instance.Executor(delegate
                    {
                        // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                     
                        if (random.Next(0,11) >4)
                        {
                            int index = random.Next(0, inventorys.Length - 1);
                            InventoryItemDTO itemDto = inventoryBiz.AddInventory(token, inventorys[index]);
                            Write(token, Protocol.Inventory, GetArea(), InventoryProtocol.AddInventory_SRES, itemDto);
                            kill = "并且获得斩杀奖励【" + itemDto.inventory.name + "】，别的小伙伴请继续努力！";
                        }
                    });//随机掉物品

                    targetRoleModel.hp = 0;
                    TalkDTO talkDto =new TalkDTO();
                    talkDto.userid = -1;
                    talkDto.talkType = TalkType.System;
                    talkDto.userName = "系统";
                    talkDto.text = attRoleModel.nikename + "杀死了" + targetRoleModel.nikename+"！"+kill;
                    
                    Brocast(tokens, MapProtocol.Talk_BRO, talkDto);//广播信息
                    UserDTO attUserDto = attRoleModel as UserDTO;
                    USER attUser= userCache.GetUserById(targetRoleModel.id);

                    if (attUserDto != null)
                    {
                        attUser.Money += attUser.Level * 100;
                        attUserDto.money += attUserDto.level * 100;
                        attUserDto.exp += 30 * (targetRoleModel.level / attUserDto.level) + attUserDto.level * 5;
                        attUser.Exp += 30 * (targetRoleModel.level / attUserDto.level) + attUserDto.level * 5;
                        int total_exp = 100 + attUser.Level * 30;
                        while (attUser.Exp >= total_exp)
                        {
                            // 升级
                            attUser.Level++;
                            attUserDto.level++;
                            attUser.Exp -= total_exp;
                            attUserDto.exp = attUser.Exp;
                            attUser.Hp += attUser.Level * 50;
                            attUser.MaxHp += attUser.Level * 50;
                            attUser.Mp += attUser.Level * 25;
                            attUser.MaxMp += attUser.Level * 25;
                            attUserDto.hp += attUserDto.level * 50;
                            attUserDto.maxHp += attUserDto.level * 50;
                            attUserDto.mp += attUserDto.level * 25;
                            attUserDto.maxMp += attUserDto.level * 25;
                            total_exp = 100 + attUser.Level * 30;
                        }
                        Brocast(tokens, MapProtocol.Killraward_BRO,attUserDto);//广播杀人
                    }
                    if (targetRoleModel.id >= 0)
                    {
                        // 开启计时任务复活
                        int timeEventId = ScheduleUtil.Instance.Schedule(delegate
                        {
                            UserDTO userDto=targetRoleModel as UserDTO;
                            userDto.hp = (int)(userDto.maxHp*0.5f);
                            userDto.mp = (int)(userDto.maxMp * 0.5f);

                            USER user = userCache.GetUserById(targetRoleModel.id);
                            user.Hp = (int)(user.MaxHp * 0.5);
                            user.Mp = (int)(user.MaxMp*0.5);
                            Brocast(tokens,MapProtocol.Revive_BRO,targetRoleModel.id);
                        }, 10);//10秒后复活玩家
                    }
                    else
                    {
                        //TODO 击杀NPC移除NPC数据
                    }
                }
            }
            damageDto.targets = damages.ToArray();
            Brocast(tokens, MapProtocol.Damage_BRO, damageDto);
        }


        void GetValue(UserToken token, int userid, int skill, ref AbsRoleModel att, ref AbsRoleModel target, ref List<int[]> damages)
        {
            int attValue = att.attack;
            int attArmour = att.armour;
            int attCrit = att.crit;
            int targetDef = target.def;
            int targetExeCrit = target.exemptCrit;
            //if (userid >= 0)//攻击者为真人
            //{
            //    UserDTO userDto = (UserDTO)att;

            //    for (int i = 0; i < userDto.equips.Length; i++)
            //    {
            //        if (userDto.equips[i] > 0)//获取拥有的装备，加上装备的属性
            //        {
            //          InventoryItemDTO itemDto=  inventoryBiz.GetItem(userDto.equips[i]);
            //            if (itemDto != null)
            //            {
            //                attValue += itemDto.attack;
            //                attArmour += itemDto.armour;
            //                attCrit += itemDto.crit;
            //            }
            //        }
            //    }
            //}
            //if (target.id >= 0)//被攻击者为真人
            //{
            //    UserDTO targetDto = (UserDTO)target;

            //    for (int i = 0; i < targetDto.equips.Length; i++)
            //    {
            //        if (targetDto.equips[i] > 0)//获取拥有的装备，加上装备的属性
            //        {
            //            InventoryItemDTO itemDto = inventoryBiz.GetItem(targetDto.equips[i]);
            //            targetDef += itemDto.def;
            //            targetExeCrit += itemDto.exemptCrit;
            //        }
            //    }
            //}

            if (skill > 0)//若此伤害为技能伤害
            {
                List<SkillDTO> skillDtos = skillBiz.GetSkillList(token);
                for (int i = 0; i < skillDtos.Count; i++)
                {
                    if (skillDtos[i].id == skill)//获取对应的技能伤害加成
                    {
                        attValue = (int)(attValue * ((float)skillDtos[i].applyValue / 100));
                        break;
                    }
                }
            }

            int value = (attValue - (targetDef - attArmour > 0 ? targetDef - attArmour : 0));
            int crit = 0;//是否暴击，0为没有，1为有触发暴击
            Random random = new Random(DateTime.Now.Millisecond);
            if (random.Next(1, 100) < (attCrit - targetExeCrit > 0 ? attCrit - targetExeCrit : 0))
            {
                value *= 2;
                crit = 1;
            }
            target.hp -= value;
            if (target.id >= 0)//被攻击者为真人则更新被攻击者数据
            {
                USER user = userCache.GetUserById(target.id);
                user.Hp = target.hp;
            }
            damages.Add(new[] { target.id, value, target.hp <= 0 ? 0 : 1 ,crit});//0为被攻击者id,1为收到伤害的值，2被攻击者是否死亡，3是否触发暴击
        }
    
        AbsRoleModel GetRoleModel(int id)
        {
            if (mapModel.ContainsKey(id))
            {
                return mapModel[id];
            }
            return null;
        }
        private void EnterMap(UserToken token, UserDTO userDto)
        {
            List<UserToken> tokens = userBiz.GetCompose(token);
            userDto.map = GetArea();
            USER user = userCache.GetUserById(userDto.id);
            user.Map = GetArea();
            mapModel.Add(userDto.id,userDto);
            models.Add(userDto);
            //base.Enter(token);
            //MapHandler.AllRoom.Enter(token);
            Write(token, MapProtocol.EnterMap_SRES, models);
            Brocast(tokens, MapProtocol.EnterMap_BRO,userDto,token);
        }


        private void LeaveMap(UserToken token)
        {
            List<UserToken> tokens = userBiz.GetCompose(token);
            //Leave(token);
            int id = userCache.GetUserId(token);
            if (mapModel.ContainsKey(id))
            {
                models.Remove(mapModel[id]);
                mapModel.Remove(id);             
            }
            Brocast(tokens, MapProtocol.LeaveMap_BRO,id);
            Write(token,MapProtocol.LeaveMap_SRES);
        }

        private void Move(UserToken token,MoveDto moveDto)
        {
            List<UserToken> tokens = userBiz.GetCompose(token);
            int id=userCache.GetUserId(token);
            moveDto.userId = id;
            Brocast(tokens, MapProtocol.Move_BRO,moveDto);
        }
        public override byte GetType()
        {
            return Protocol.Map;
        }

     
    }
}
