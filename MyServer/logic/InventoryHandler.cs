using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.biz;
using MyServer.biz.inventory;
using MyServer.cache;
using MyServer.cache.user;
using MyServer.dao;
using MyServer.tool;
using NetFrame;
using Protocols;
using Protocols.dto;

namespace MyServer.logic
{
    public class InventoryHandler:AbsOnceHandler,HandlerInterface
    {
        private IinventoryBiz inventoryBiz = BizFactory.inventoryBiz;
        private IUserCache userCache = cacheFactory.UserCache;
        public void ClientClose(NetFrame.UserToken token, string error)
        {

        }

        public void MessageReceive(NetFrame.UserToken token, NetFrame.Auto.SocketModel message)
        {
            switch (message.command)
            {
                case InventoryProtocol.AddInventory_CREQ:
                    AddInventory(token,message.GetMessage<int>());
                    break;
                case InventoryProtocol.GetInventory_CREQ:
                    GetInventory(token);
                    break;
                case InventoryProtocol.UpdateInventory_CREQ:
                    UpdateInventory(token, message.GetMessage<InventoryItemDTO>());
                    break;
                case InventoryProtocol.DeleteInventory_CREQ:
                    DeleteInventory(token, message.GetMessage<int>());
                    break;
                case InventoryProtocol.DeleteInventoryList_CREQ:
                    DeleteInventoryList(token);
                    break;

            }
        }

    

        void AddInventory(UserToken token,int inventoryId)
        {
            ExecutorPool.Instance.Executor(delegate
            {
               InventoryItemDTO itemDto= inventoryBiz.AddInventory(token, inventoryId);
               Write(token, InventoryProtocol.AddInventory_SRES, itemDto);
            });
        }

        void GetInventory(UserToken token)
        {
            ExecutorPool.Instance.Executor(delegate
            {
                List<InventoryItemDTO> itemDtos = inventoryBiz.GetInventoryList(token);
                Write(token,InventoryProtocol.GetInventory_SRES,itemDtos);
            });
        }

        void UpdateInventory(UserToken token, InventoryItemDTO _itemDto)
        {
            ExecutorPool.Instance.Executor(delegate
            {
                InventoryItemDTO itemDto = inventoryBiz.UpdateInventory(token, _itemDto);
                Write(token, InventoryProtocol.UpdateInventory_SRES, itemDto);

            });
        }

        void DeleteInventory(UserToken token,int inventoryId)
        {
            ExecutorPool.Instance.Executor(delegate
            {
                InventoryItemDTO itemDto = inventoryBiz.DeleteInventory(token, inventoryId);
                Write(token, InventoryProtocol.DeleteInventory_SRES, itemDto);
            });
        }
        void DeleteInventoryList(UserToken token)
        {
            ExecutorPool.Instance.Executor(delegate
            {
               inventoryBiz.DeleteInventoryList(token);
            });
        }

       
        public override byte GetType()
        {
            return Protocol.Inventory;
        }
    }
}
