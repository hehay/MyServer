using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.dao;

namespace MyServer.cache.inventory
{
    public class InventoryCache:IInventoryCache
    {
        public Dictionary<int, List<int>> useridToInventoryList = new Dictionary<int, List<int>>(); 
        public Dictionary< int,INVENTORY > IdToInventory=new Dictionary<int, INVENTORY>(); 
        public bool isInInventoryDic(int userid,int inventoryId)
        {
            if (useridToInventoryList.ContainsKey(userid))
            {
                List<int> inventorys = useridToInventoryList[userid];
                for (int i = 0; i < inventorys.Count; i++)
                {
                    if (IdToInventory[inventorys[i]].Inventoryid == inventoryId)
                    {
                        return true;
                        
                    }                    
                }
                return false;
            }
            else
            {
                return false;

            }
           
            
        }


        public INVENTORY AddInventory(int userid, INVENTORY inventory)
        {
            if (useridToInventoryList.ContainsKey(userid))
            {
                useridToInventoryList[userid].Add(inventory.Id);
                IdToInventory.Add(inventory.Id,inventory);
            }
            else
            {
                List<int> inventorys = new List<int>();
                inventorys.Add(inventory.Id);
                IdToInventory.Add(inventory.Id, inventory);
                useridToInventoryList.Add(userid,inventorys);
            }
            return inventory;
        }


        public INVENTORY GetInventory(int userid, int inventoryId)
        {
            if (useridToInventoryList.ContainsKey(userid))
            {
                List<int> inventorys = useridToInventoryList[userid];
                for (int i = 0; i < inventorys.Count; i++)
                {
                    if (IdToInventory[inventorys[i]].Inventoryid == inventoryId)
                    {
                        return IdToInventory[inventorys[i]];
                     
                    }
                }
                return null;
            }
            else
            {
                return null;

            }
        }

        public INVENTORY GetInventory(int id)
        {
            return IdToInventory[id];
        }

        public List<INVENTORY> GetInventoryList(int userid)
        {
            List<INVENTORY> inventorys=new List<INVENTORY>();
            if (useridToInventoryList.ContainsKey(userid))
            {
                for (int i = 0; i < useridToInventoryList[userid].Count; i++)
                {
                    inventorys.Add(IdToInventory[useridToInventoryList[userid][i]]); 
                }
                return inventorys;
            }
            return null;
        }

        public INVENTORY UpdateInventory(int userid, INVENTORY nowInventory)
        {

            if (IdToInventory.ContainsKey(nowInventory.Id))
            {
                IdToInventory[nowInventory.Id] = nowInventory;
                return nowInventory;
            }
            return null;
        }

        public INVENTORY DeleteInventory(int userid, int iventoryId)
        {
            if (useridToInventoryList.ContainsKey(userid))
            {
                INVENTORY inventory = IdToInventory[iventoryId];
                useridToInventoryList[userid].Remove(iventoryId);
                IdToInventory.Remove(iventoryId);
                return inventory;
            }
            return null;
        }

        public INVENTORY GetItem(int itemId)
        {
            if (IdToInventory.ContainsKey(itemId))
            {
                return IdToInventory[itemId];
            }
            return null;
        }

        public void DeleteInventoryList(int userid)
        {
            if (useridToInventoryList.ContainsKey(userid))
            {
                for (int i = 0; i < useridToInventoryList[userid].Count; i++)
                {
                    IdToInventory.Remove(useridToInventoryList[userid][i]);
                }
                useridToInventoryList.Remove(userid);
            }
        }
    }
}
