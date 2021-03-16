using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp.RuntimeBinder;
using Protocols.dto;

namespace MyServer.tool
{
    public class ModelInitialProperty
    {
        public Dictionary<int, ModelInitial> modelToInitial = new Dictionary<int, ModelInitial>();

        public ModelInitialProperty()
        {
            int[] equips = new[] {0, 0, 0, 0, 0, 0, 0, 0};
            Create(ModelName.LichModel, 75, 50, 0, 0, 0, 500, 500, 300, 300, 2f,  new[] {1001, 1002, 1003}, equips);
        }

        private void Create(int modelname, int attack, int def, int armour,
            int crit, int exemptCrit, int hp, int maxHp, int mp, int maxMp, float speed, int[] skillids, int[] equips)
        {
            ModelInitial modelInitial = new ModelInitial(modelname, attack, def, armour,
                crit, exemptCrit, hp, maxHp, mp, maxMp, speed, skillids, equips);
            modelToInitial.Add(modelname, modelInitial);
        }
    }

    public class ModelInitial
    {
        public int name;
        public int attack;
        public int def;
        public int armour;
        public int crit;
        public int exemptCrit;
        public int hp;
        public int maxHp;
        public int mp;
        public int maxMp;
        public float speed;
        public int[] skillIDs;
        public int[] equips;

        public ModelInitial()
        {
        }

        public ModelInitial(int name, int attack, int def, int armour,
            int crit, int exemptCrit, int hp, int maxHp, int mp, int maxMp, float speed, int[] skillIds, int[] equips)
        {
            this.name = name;
            this.attack = attack;
            this.def = def;
            this.armour = armour;
            this.crit = crit;
            this.exemptCrit = exemptCrit;
            this.hp = hp;
            this.maxHp = maxHp;
            this.mp = mp;
            this.maxMp = maxMp;
            this.speed = speed;
            skillIDs = skillIds;
            this.equips = equips;
        }
    }
}
