using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.cache;
using MyServer.cache.inventory;
using MyServer.cache.user;
using MyServer.dao;
using MyServer.tool;
using Protocols.dto;

namespace MyServer.biz.inventory
{
    public class InventoryBiz:IinventoryBiz
    {
        private IUserCache userCache = cacheFactory.UserCache;
        private IInventoryCache InventoryCache = cacheFactory.InventoryCache;
        private int index = 1;
        public InventoryItemDTO AddInventory(NetFrame.UserToken userToken, int inventoryId)
        {
            int userid = userCache.GetUserId(userToken);

       
            InventoryInitial inventoryInitial = InventoryInItialProperty.idToInventoryInitial[inventoryId];
            if (inventoryInitial.inventoryType == InventoryType.Equip)//是装备类型，不管什么情况都增加一条记录
            {
                INVENTORY inventory = new INVENTORY();
                inventory.Id = index;
                index++;
                inventory.Inventoryid = inventoryId;
                inventory.UserId = userid;
                inventory.ShortcutId = 0;
                inventory.InventoryGridId = -1;
                inventory.Level = 1;
                inventory.Count = 1;
                inventory.IsDressed = false;
                long tick = DateTime.Now.Ticks;
                Random random1 = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 50));
                Random random2 = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
                Random random3 = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 42));
                Random random4 = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 82));
                Random random5 = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 45));
                Random random6 = new Random((int) (tick & 0xffffffffL) | (int) (tick >> 16));
                Random random7 = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 26));
                Random random8 = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 106));

                inventory.StarLevel = random6.Next(1, 5);
                inventory.Quality = random8.Next(1, 5);
                inventory.Attack = (int)GetValue(random1,inventoryInitial.attack, inventory.StarLevel, inventory.Quality);
                inventory.Armour = (int)GetValue(random2,inventoryInitial.armour, inventory.StarLevel, inventory.Quality);
                inventory.Def = (int)GetValue(random3,inventoryInitial.def, inventory.StarLevel, inventory.Quality);
                inventory.Crit = (int)GetValue(random4,inventoryInitial.crit, inventory.StarLevel, inventory.Quality);
                inventory.ExemptCrit = (int)GetValue(random5,inventoryInitial.exemptCrit, inventory.StarLevel, inventory.Quality);
                inventory.Mp = (int)GetValue(random7,inventoryInitial.mp, inventory.StarLevel, inventory.Quality);
                inventory.Speed = 0;
                if (inventoryInitial.speed > 0)
                {
                    float max = ((inventoryInitial.speed + ((inventory.StarLevel + inventory.Quality) * 0.05f)));
                    float min = max * 0.5f;
                    double speed = random6.NextDouble() * (max - min) + min;
                    inventory.Speed = (float)Math.Round(speed, 1);
                }
              
                inventory.Hp = (int)GetValue(random8,inventoryInitial.hp, inventory.StarLevel, inventory.Quality);
     
               return GetInventoryItem(InventoryCache.AddInventory(userid, inventory));
            }
            else//非装备类型
            {
                INVENTORY inventory;
                if (InventoryCache.isInInventoryDic(userid, inventoryId))//判断是否有相同类型的物品
                {
                    inventory = GetInventoryByItem(GetInventory(userid, inventoryId));//获取相同的物品使数量增加
                    if (inventory != null)
                    {
                        inventory.Count++;
                        return GetInventoryItem(inventory);
                    }
                  
                }
                    inventory = new INVENTORY();//没有相同物品，则自己增加一条记录
                    inventory.Id = index;
                    index++;
                    inventory.Inventoryid = inventoryId;
                    inventory.UserId = userid;
                    inventory.ShortcutId = 0;
                    inventory.InventoryGridId = -1;
                    inventory.Count = 1;
                    inventory.Level = 1;
                    inventory.IsDressed = false;
                    inventory.StarLevel = 0;
                    inventory.Quality = 0;
                    inventory.Attack = 0;
                    inventory.Armour = 0;
                    inventory.Def = 0;
                    inventory.Crit = 0;
                    inventory.ExemptCrit = 0;
                    inventory.Mp = 0;
                    inventory.Speed = 0;
                    inventory.Hp = 0;
                    return GetInventoryItem(InventoryCache.AddInventory(userid, inventory));
                }

        }


        float GetValue( Random random, float value, int starlevel, int quality)
        {
            return random.Next((int) (value+((starlevel + quality)*2)), (int) (value+(starlevel + quality)*4));
        }

        public InventoryItemDTO GetInventory(int userid, int inventoryId)
        {
            if (InventoryCache.isInInventoryDic(userid, inventoryId))
            {
                InventoryCache.GetInventory( userid,inventoryId);
                return GetInventoryItem(InventoryCache.GetInventory(userid,inventoryId));
            }
            return null;
        }

        public InventoryItemDTO GetItem( int itemID)
        {
            if (InventoryCache.GetItem(itemID) != null)
            {
                return GetInventoryItem(InventoryCache.GetItem(itemID));
            }
            return null;
        }

        public List<InventoryItemDTO> GetInventoryList(NetFrame.UserToken userToken)
        {
            int userid = userCache.GetUserId(userToken);
            List<INVENTORY> inventorys = InventoryCache.GetInventoryList(userid);
            if (inventorys!=null)  return GetInventoryItemList(inventorys);
            return null;
        }

        public InventoryItemDTO UpdateInventory(NetFrame.UserToken token, InventoryItemDTO nowItem)
        {
            int userid = userCache.GetUserId(token);

            InventoryCache.UpdateInventory(userid, GetInventoryByItem(nowItem));
            return nowItem;
        }

        public InventoryItemDTO DeleteInventory(NetFrame.UserToken token, int inventoryId)
        {
            int userid = userCache.GetUserId(token);
            return GetInventoryItem(InventoryCache.DeleteInventory(userid, inventoryId));
        }

        public void DeleteInventoryList(NetFrame.UserToken token)
        {
            int userid = userCache.GetUserId(token);
            InventoryCache.DeleteInventoryList(userid);
        }


        INVENTORY GetInventoryByItem(InventoryItemDTO itemDto)
        {
            INVENTORY inventory=new INVENTORY();
            inventory.Id = itemDto.id;
            inventory.Inventoryid = itemDto.inventory.id;
            inventory.UserId = itemDto.userId;
            inventory.ShortcutId = itemDto.shortcutid;
            inventory.InventoryGridId = itemDto.inventoryGridId;
            inventory.Level = itemDto.level;
            inventory.Count = itemDto.count;
            inventory.IsDressed = itemDto.IsDressed;
            inventory.StarLevel = itemDto.starLevel;
            inventory.Quality = itemDto.quality;
            inventory.Attack = itemDto.attack;
            inventory.Armour = itemDto.armour;
            inventory.Def = itemDto.def;
            inventory.Crit = itemDto.crit;
            inventory.ExemptCrit = itemDto.exemptCrit;
            inventory.Mp = itemDto.mp;
            inventory.Speed = itemDto.speed;
            inventory.Hp = itemDto.hp;
            return inventory;
        }

        InventoryItemDTO GetInventoryItem(INVENTORY inventory)
        {
            InventoryInitial inventoryInitial = InventoryInItialProperty.idToInventoryInitial[inventory.Inventoryid];

            InventoryItemDTO itemDto=new InventoryItemDTO();
            InventoryDto inventoryDto = new InventoryDto();
            inventoryDto.id = inventory.Inventoryid;
            inventoryDto.applyValue = inventoryInitial.applyValue;
            inventoryDto.equipType = inventoryInitial.equipType;
            inventoryDto.icon = inventoryInitial.icon;
            inventoryDto.info = inventoryInitial.info;
            inventoryDto.infoType = inventoryInitial.infoType;
            inventoryDto.inventoryType = inventoryInitial.inventoryType;
            inventoryDto.modelName = inventoryInitial.modelName;
            inventoryDto.name = inventoryInitial.name;
            inventoryDto.price = inventoryInitial.price;
            inventoryDto.sell = inventoryInitial.sell;
            itemDto.inventory = inventoryDto;
            itemDto.id = inventory.Id;
            itemDto.userId = inventory.UserId;
            itemDto.shortcutid = inventory.ShortcutId;
            itemDto.inventoryGridId = inventory.InventoryGridId;
            itemDto.level = inventory.Level;
            itemDto.count = inventory.Count;
            itemDto.IsDressed = inventory.IsDressed;
            itemDto.starLevel = inventory.StarLevel;
            itemDto.quality = inventory.Quality;
            itemDto.attack = inventory.Attack;
            itemDto.armour = inventory.Armour;
            itemDto.def = inventory.Def;
            itemDto.crit = inventory.Crit;
            itemDto.exemptCrit = inventory.ExemptCrit;
            itemDto.mp = inventory.Mp;
            itemDto.speed = inventory.Speed;
            itemDto.hp = inventory.Hp;
            return itemDto;
        }

        List<InventoryItemDTO> GetInventoryItemList(List<INVENTORY> inventorys)
        {

            List<InventoryItemDTO> inventoryItemDtos=new List<InventoryItemDTO>();
            for (int i = 0; i < inventorys.Count; i++)
            {
                InventoryInitial inventoryInitial = InventoryInItialProperty.idToInventoryInitial[inventorys[i].Inventoryid];
                InventoryItemDTO itemDto = new InventoryItemDTO();
                InventoryDto inventoryDto = new InventoryDto();
                inventoryDto.id = inventorys[i].Inventoryid;
                inventoryDto.applyValue = inventoryInitial.applyValue;
                inventoryDto.equipType = inventoryInitial.equipType;
                inventoryDto.icon = inventoryInitial.icon;
                inventoryDto.info = inventoryInitial.info;
                inventoryDto.infoType = inventoryInitial.infoType;
                inventoryDto.inventoryType = inventoryInitial.inventoryType;
                inventoryDto.modelName = inventoryInitial.modelName;
                inventoryDto.name = inventoryInitial.name;
                inventoryDto.price = inventoryInitial.price;
                inventoryDto.sell = inventoryInitial.sell;
                itemDto.inventory = inventoryDto;
                itemDto.id = inventorys[i].Id;
                itemDto.userId = inventorys[i].UserId;
                itemDto.shortcutid = inventorys[i].ShortcutId;
                itemDto.inventoryGridId = inventorys[i].InventoryGridId;
                itemDto.level = inventorys[i].Level;
                itemDto.count = inventorys[i].Count;
                itemDto.IsDressed = inventorys[i].IsDressed;
                itemDto.starLevel = inventorys[i].StarLevel;
                itemDto.quality = inventorys[i].Quality;
                itemDto.attack = inventorys[i].Attack;
                itemDto.armour = inventorys[i].Armour;
                itemDto.def = inventorys[i].Def;
                itemDto.crit = inventorys[i].Crit;
                itemDto.exemptCrit = inventorys[i].ExemptCrit;
                itemDto.mp = inventorys[i].Mp;
                itemDto.speed = inventorys[i].Speed;
                itemDto.hp = inventorys[i].Hp;
                inventoryItemDtos.Add(itemDto);
            }
            return inventoryItemDtos;
        }



        public InventoryItemDTO UseInventory(NetFrame.UserToken token, int itemId)
        {
            if (InventoryCache.GetItem(itemId) != null)
            {
                return GetInventoryItem(InventoryCache.GetItem(itemId));
            }
            return null;
        }
    }
}
