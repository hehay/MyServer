using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols.dto
{
    [Serializable]
    public  class AbsRoleModel
    {
        public int id;
        public string name;
        public int modelName;
        public int attack;
        public int def;
        public int armour;
        public int crit;
        public int exemptCrit;
        public int hp;
        public int maxHp;
        public float speed;
        public int level;

    }

    [Serializable]
    public class ModelName
    {
        public const int Command = 0;
        public const int LichModel = 1;
    }
}
