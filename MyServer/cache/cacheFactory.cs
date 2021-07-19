using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.cache.accaount;
using MyServer.cache.inventory;
using MyServer.cache.pos;
using MyServer.cache.skill;
using MyServer.cache.user;

namespace MyServer.cache
{
  public class cacheFactory
  {
      //private static Dictionary<string, string> NameAndPath = new Dictionary<string, string>();
      public static readonly IAccountCache AccaountCache;
      public static readonly IUserCache UserCache;
      public static readonly IPosCache PosCache;
      public static readonly ISkillCache SkillCache;
      public static readonly IInventoryCache InventoryCache;
      private static string _path = "F:/CurDesign/MyServer/MyServer/file/";
        static cacheFactory()
      {
          // CreatePath();
           
           AccaountCache =new AccountCache(_path+ "AccountFile.xml");
           UserCache=new UserCache();
           PosCache=new PosCache();
           SkillCache = new SkillCache();
           InventoryCache=new InventoryCache();
      }
        //private List<ACCOUNT> AllAccount = new List<ACCOUNT>();
        
        
      /*
        private static void CreatePath()
        {
            NameAndPath.Add("AccountFile", _path + "AccountFile.xml");
            NameAndPath.Add("SkillFile", _path + "SkillFile.xml");
        }
    */
        
    }
}
