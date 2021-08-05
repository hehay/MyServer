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
        public const int GetUserDt_CREQ = 11;
        public const int GetUserDt_SRES = 12;
        public const int StartMatch_CREQ = 13;
        public const int StartMatch_SRES = 14;
        public const int MatchConfirm_CREQ = 15;
        public const int MatchConfirm_SRES = 16;
        public const int StopMatch_CREQ = 17;
        public const int StopMatch_SRES = 18;
        public const int MatchResult_CREQ = 19;
        public const int MatchResult_SRES = 20;
        public const int GetHeroList_CREQ = 21;
        public const int GetHeroList_SRES=22;
   }
}
