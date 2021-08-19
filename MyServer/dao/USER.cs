using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.dao
{
    public class USER
    {
        private int id;
        private string skillIds;
        private string equips;
        private int gold;
        private int level;
        private int exp;
        private int diamond;
        private string nickname;
         
        private int modelName;
        private int accountId;
        private int map;
        private int attack;
        private int def;
        private int armour;
        private int crit;
        private int exemptCrit;
        private int hp;
        private int maxHp;
        private int mp;
        private int maxMp;
        private float speed;
        private int skillPoint;
        private int propertyPoint;
        private int money;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public int Gold 
        {
            set { gold = value; }
            get { return gold; }
        }
        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public int Exp
        {
            get { return exp; }
            set { exp = value; }
        }

        public int ModelName
        {
            get { return modelName; }
            set { modelName = value; }
        }

        public int AccountId
        {
            get { return accountId; }
            set { accountId = value; }
        }

        public string Nickname
        {
            get { return nickname; }
            set { nickname = value; }
        }
        public int Diamond 
        {
            get { return diamond; }
            set { diamond = value; }
        }
        public int Map
        {
            get { return map; }
            set { map = value; }
        }

        public int Attack
        {
            get { return attack; }
            set { attack = value; }
        }

        public int Def
        {
            get { return def; }
            set { def = value; }
        }

        public int Armour
        {
            get { return armour; }
            set { armour = value; }
        }

        public int Crit
        {
            get { return crit; }
            set { crit = value; }
        }

        public int ExemptCrit
        {
            get { return exemptCrit; }
            set { exemptCrit = value; }
        }

        public int Hp
        {
            get { return hp; }
            set { hp = value; }
        }

        public int MaxHp
        {
            get { return maxHp; }
            set { maxHp = value; }
        }

        public int Mp
        {
            get { return mp; }
            set { mp = value; }
        }

        public int MaxMp
        {
            get { return maxMp; }
            set { maxMp = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public int SkillPoint
        {
            get { return skillPoint; }
            set { skillPoint = value; }
        }

        public int PropertyPoint
        {
            get { return propertyPoint; }
            set { propertyPoint = value; }
        }

        public int Money
        {
            get { return money; }
            set { money = value; }
        }

        public string SkillIds
        {
            get { return skillIds; }
            set { skillIds = value; }
        }

        public string Equips
        {
            get { return equips; }
            set { equips = value; }
        }
    }
}
