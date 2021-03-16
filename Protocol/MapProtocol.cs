using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols
{

    public class MapProtocol
    {
        public const int EnterMap_CREQ = 1;
        public const int EnterMap_SRES = 2;
        public const int EnterMap_BRO = 3;
        public const int LeaveMap_CREQ = 4;
        public const int LeaveMap_SRES = 5;
        public const int LeaveMap_BRO = 6;
        public const int Move_CREQ = 7;
        public const int Move_BRO = 8;
        public const int GetPosList_SRES = 9;
        public const int GetPosList_CRES = 10;
        public const int Attack_CREQ = 11;
        public const int Attack_BRO = 12;
        public const int Damage_CREQ = 13;
        public const int Damage_BRO = 14;
        public const int Skill_CREQ = 15;
        public const int Skill_BRO = 16;
        public const int UseInventory_CREQ = 17;
        public const int UseInventory_BRO = 18;
        public const int Talk_CREQ = 19;
        public const int Talk_SRES = 20;
        public const int Talk_BRO = 21;
        public const int UnUseInventory_CREQ = 22;
        public const int UnUseInventory_SRES = 23;
        public const int Revive_BRO = 24;
        public const int Killraward_BRO = 25;
    }
}
