using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.dao;
using Protocols.dto;

namespace MyServer.cache.inventory
{
    public interface IInventoryCache
    {

        bool isInInventoryDic(int userid,int inventoryId);

        INVENTORY AddInventory(int userid, INVENTORY inventory);

        INVENTORY GetInventory(int userid, int inventoryId);
        INVENTORY GetInventory(int id);
        List<INVENTORY> GetInventoryList(int userid);

        INVENTORY UpdateInventory(int userid, INVENTORY nowInventory);

        INVENTORY DeleteInventory(int userid, int iventoryId);

        void DeleteInventoryList(int userid);

        INVENTORY GetItem(int itemId);
    }
}
