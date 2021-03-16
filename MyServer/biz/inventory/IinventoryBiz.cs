using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFrame;
using Protocols.dto;

namespace MyServer.biz.inventory
{
    public interface IinventoryBiz
    {
        InventoryItemDTO AddInventory(UserToken userToken, int inventoryId);
        List<InventoryItemDTO> GetInventoryList(UserToken userToken);

        InventoryItemDTO GetInventory(int userid, int inventoryId);

        InventoryItemDTO GetItem( int itemID);

        InventoryItemDTO UpdateInventory(UserToken token, InventoryItemDTO nowItem);
        InventoryItemDTO DeleteInventory(UserToken token, int inventoryId);
        void DeleteInventoryList(UserToken token);

        InventoryItemDTO UseInventory(UserToken token, int itemId);
    }
}
