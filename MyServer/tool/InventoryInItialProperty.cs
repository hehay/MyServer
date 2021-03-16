using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Protocols.dto;

namespace MyServer.tool
{
    public  class InventoryInItialProperty
    {

        public static readonly Dictionary<int,InventoryInitial>idToInventoryInitial=new Dictionary<int, InventoryInitial>();

        public InventoryInItialProperty()
        {
            Create(1001, "血药", "icon_1001", InventoryType.Drug, EquipType.None, ModelName.Command, 60, 50, InfoType.Hp, "能恢复你的生命值，人在江湖飘哪能不挨刀，你值得拥有！",60,0,0,0,0,0,0,0,0);
            Create(1002, "蓝药", "icon_1002", InventoryType.Drug, EquipType.None, ModelName.Command, 60, 50, InfoType.Mp, "能恢复你的蓝量值，杀人放火必备，你值得拥有！", 40,0,0,0,0,0,0,0,0);
            Create(1003, "经验书", "icon_1003", InventoryType.Drug, EquipType.None, ModelName.Command, 80, 60, InfoType.Exp, "能快速提高你的经验，你值得拥有！", 100, 0, 0, 0, 0, 0, 0, 0, 0);
            Create(1004, "大瓶药水", "icon_1004", InventoryType.Drug, EquipType.None, ModelName.Command, 120, 70, InfoType.Hp, "不管你是不是女的。当大量缺血的时候，你肯定需要我！", 150, 0, 0, 0, 0, 0, 0, 0, 0);
            Create(2001, "寂寞斧子", "icon_2001", InventoryType.Equip, EquipType.Weapon, ModelName.LichModel, 60, 50, InfoType.None, "寂寞的人用寂寞武器，即珍贵又实用，重点还能装逼！", 40,15,0,5,5,0,0,0,0);//攻击，防御，破甲，暴击，免暴，mp，速度，hp
            Create(2002, "寂寞手镯", "icon_2002", InventoryType.Equip, EquipType.Bracelet, ModelName.LichModel, 60, 50, InfoType.None, "电视上先祖遗传的东西！", 40, 3, 5, 0, 5, 5, 0, 0, 50);
            Create(2003, "寂寞铠甲", "icon_2003", InventoryType.Equip, EquipType.Cloth, ModelName.LichModel, 60, 50, InfoType.None, "虽然好用，但完全就不性感了！", 40, 0,10, 0, 0, 10, 0, 0, 100);
            Create(2004, "寂寞头盔", "icon_2004", InventoryType.Equip, EquipType.Helm, ModelName.LichModel, 60, 50, InfoType.None, "跟天朝的摩托头盔一样好！", 40, 0, 5, 0, 0, 8, 0, 0, 0);
            Create(2005, "寂寞项链", "icon_2005", InventoryType.Equip, EquipType.Necklace, ModelName.LichModel, 60, 50, InfoType.None, "小心打架的时候不要被勒住啦，会死人的！", 40, 0, 5, 5, 0, 0, 50, 0, 25);
            Create(2006, "寂寞戒指", "icon_2006", InventoryType.Equip, EquipType.Ring, ModelName.LichModel, 60, 50, InfoType.None, "这可不是结婚用的！当然你要用也是可以—.—", 40, 3, 5, 0, 5, 5, 0, 0, 0);
            Create(2007, "寂寞鞋", "icon_2007", InventoryType.Equip, EquipType.Shoes, ModelName.LichModel, 60, 50, InfoType.None, "装了逼还能跑！就是爽！", 40, 0, 0, 0, 3, 0, 0, 0.5f, 0);
            Create(2008, "寂寞翅膀", "icon_2008", InventoryType.Equip, EquipType.Wing, ModelName.LichModel, 60, 50, InfoType.None, "带你飞，带你翱翔世界！", 40, 10, 10, 5, 5, 5, 0, 1f, 0);
        }

        void Create(int id, string name, string icon, InventoryType inventoryType, EquipType equipType, int modelName, int price, int sell, InfoType infoType, string info, int applyValue,
                    int attack, int def, int armour, int crit, int exemptCrit, int mp, float speed, int hp)
        {
            InventoryInitial inventoryInitial=new InventoryInitial( id, name, icon, inventoryType, equipType, modelName, price, sell, infoType, info, applyValue, attack,  def,  armour,  crit,  exemptCrit,  mp,  speed,  hp);
            idToInventoryInitial.Add(id,inventoryInitial);
        }
    }

    public class InventoryInitial
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

        public int attack;//伤害
        public int def;//伤害
        public int armour;
        public int crit;
        public int exemptCrit;
        public int mp;
        public float speed;
        public int hp;//生命

        public InventoryInitial() { }

        public InventoryInitial(int id,string name,string icon,InventoryType inventoryType,EquipType equipType,int modelName,int price,int sell,InfoType infoType,string info,int applyValue,
                                 int attack, int def, int armour, int crit, int exemptCrit, int mp, float speed, int hp)
        {
            this.id = id;
            this.name = name;
            this.icon = icon;
            this.inventoryType = inventoryType;
            this.equipType = equipType;
            this.modelName = modelName;
            this.price = price;
            this.sell = sell;
            this.infoType = infoType;
            this.info = info;
            this.applyValue = applyValue;


            this.attack = attack;
            this.def = def;
            this.armour = armour;
            this.crit = crit;
            this.exemptCrit = exemptCrit;
            this.mp = mp;
            this.speed = speed;
            this.hp = hp;
        }

    }
}
