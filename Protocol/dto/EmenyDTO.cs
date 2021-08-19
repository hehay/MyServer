using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols.dto
{
    [Serializable]
    public class EmenyDTO:AbsRoleModel
    {
        public EmenyDTO() { }

        public EmenyDTO(int id,  string name, int modelName, int attack, int def, int armour,
            int crit, int exemptCrit, int hp, int maxHp,  float speed)
        {
            this.id = id;
            this.nikename = name;
            this.modelName = modelName;
            this.attack = attack;
            this.def = def;
            this.armour = armour;
            this.crit = crit;
            this.exemptCrit = exemptCrit;
            this.hp = hp;
            this.maxHp = maxHp;
            this.speed = speed;
        }
    }
}
