using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols.dto
{
    [Serializable]
    public class UserDTO:AbsRoleModel
    {

        public int accountId;
        public int gold;
        public int exp;
        public int diamond;
        public int pointTicket;
        public int map;
        public int mp;
        public int maxMp;
        public int skillPoint;
        public int propertyPoint;
        public int money;
        public int[] skillids;
        public int[] equips;
        public UserDTO(int id, int accountId,int gold, int level, int exp, string name, int modelName, int map,int attack,int def,int armour,
            int crit,int exemptCrit,int hp,int maxHp,int mp,int maxMp,float speed,int skillPoint,int propertyPoint,int money, int[] skillids,params int[] equips)
        {
            this.id = id;
            this.accountId = accountId;
            this.gold = gold;
            this.level = level;
            this.exp = exp;
            this.nikename = name;
            this.modelName = modelName;
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
        }

        public UserDTO()
        {
            
        }
            
    }      
}                   
                       