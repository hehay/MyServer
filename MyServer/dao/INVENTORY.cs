using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.dao
{
    public class INVENTORY
    {
        private int id;
        private int userId;
        private int inventoryid;
        private int shortcutId;
        private int inventoryGridId;
        private int level;
        private int count;
        private bool isDressed;
        private int starLevel;//星级
        private int quality;//品质
        private int attack;//伤害
        private int def;//伤害
        private int armour;
        private int crit;
        private int exemptCrit;
        private int mp;
        private float speed;
        private int hp;//生命

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public int Inventoryid
        {
            get { return inventoryid; }
            set { inventoryid = value; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        public bool IsDressed
        {
            get { return isDressed; }
            set { isDressed = value; }
        }

        public int StarLevel
        {
            get { return starLevel; }
            set { starLevel = value; }
        }

        public int Quality
        {
            get { return quality; }
            set { quality = value; }
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

        public int Mp
        {
            get { return mp; }
            set { mp = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public int Hp
        {
            get { return hp; }
            set { hp = value; }
        }

        public int ShortcutId
        {
            get { return shortcutId; }
            set { shortcutId = value; }
        }

        public int InventoryGridId
        {
            get { return inventoryGridId; }
            set { inventoryGridId = value; }
        }
    }
}
