using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols
{
    public class InventoryProtocol
    {
        public const int AddInventory_CREQ = 1;
        public const int AddInventory_SRES = 2;
        public const int GetInventory_CREQ = 3;
        public const int GetInventory_SRES = 4;
        public const int UpdateInventory_CREQ = 5;
        public const int UpdateInventory_SRES = 6;
        public const int DeleteInventory_CREQ = 7;
        public const int DeleteInventory_SRES = 8;
        public const int DeleteInventoryList_CREQ = 9;

    }
}
