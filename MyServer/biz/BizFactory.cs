using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.biz.accaount;
using MyServer.biz.inventory;
using MyServer.biz.pos;
using MyServer.biz.skill;
using MyServer.biz.user;

namespace MyServer.biz
{
    public class BizFactory
    {
        public static readonly IAccountBiz accountBiz;
        public static readonly IUserBiz userBiz;
        public static readonly IPosBiz PosBiz;
        public static readonly ISkillBiz SkillBiz;
        public static readonly IinventoryBiz inventoryBiz;
        public static readonly IModelBiz modelBiz;
        static BizFactory()
        {
            accountBiz = new AccountBiz();
            userBiz=new UserBiz();
            PosBiz=new PosBiz();
            SkillBiz=new SkillBiz();
            inventoryBiz=new InventoryBiz();
            modelBiz = new ModelBiz();
        }
    }
}
