using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols
{

   public class UserProtocol
   {
       public const int CreateRole_CREQ = 1;//创建
       public const int CreateRole_SRES = 2;
       public const int DeleteRole_CREQ = 3;//删除
       public const int DeleteRole_SRES = 4;
       public const int GetRoleList_CREQ = 5;
       public const int GetRoleList_SRES = 6;
       public const int OnLine_CREQ = 7;
       public const int OnLine_SRES = 8;
       public const int OffLine_CREQ = 9;
       public const int OffLine_SRES = 10;

   }
}
