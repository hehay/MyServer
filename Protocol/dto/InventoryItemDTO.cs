using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols.dto
{
    public enum InventoryType
    {
        Equip,
        Mat,
        Drug,
        Box
    }
    public enum EquipType
    {
        Helm=0,
        Cloth=1,
        Weapon=2,
        Shoes=3,
        Necklace=4,
        Bracelet=5,
        Ring=6,
        Wing=7,
        None=8
    }
    public enum InfoType
    {
        Hp,
        Mp,
        Exp,
        None
    }

    [Serializable]
    public class InventoryItemDTO
    {
        public int id;
        public int userId;
        public int shortcutid;
        public int inventoryGridId;
        public InventoryDto inventory;
        public int level;
        public int count;
        public bool IsDressed;
        public int starLevel;//星级
        public int quality;//品质
        public int attack;//伤害
        public int def;//伤害
        public int armour;
        public int crit;
        public int exemptCrit;
        public int mp;
        public float speed;
        public int hp ;//生命

    }
    [Serializable]
    public class InventoryDto
    {
        public int id;
        public string name;//名称
        public string icon;//在图集中的名称
        public InventoryType inventoryType;//物品类型
        public EquipType equipType;//装备类型
        public int modelName;//适合职业
        public int price = 0;//价格
        public int sell;//出售
        public InfoType infoType;//作用类型，表示作用在哪个属性之上
        public string info;//描述
        public int applyValue;//作用值

    }
}
