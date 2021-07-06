using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.cache;
using MyServer.cache.accaount;

namespace MyServer.biz.accaount
{
    public class AccountBiz:IAccountBiz
    {
        private IAccountCache accountCache = cacheFactory.AccaountCache;
        /// <summary>
        /// 创建账号
        /// </summary>
        /// <param name="token"></param>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int Creat(NetFrame.UserToken token, string account, string password)
        {
            if (account.Length < 3 || password.Length < 3) return -1;//账号或密码长度不够返回-1
            
            if (accountCache.HasAccaount(account)) return -2;//已有此账号

            accountCache.AddAccaount(account,password); return 1;//创建成功
        }

        public int Login(NetFrame.UserToken token, string account, string password)
        {
            if (account.Length < 3 || password.Length < 3) return -1;//账号密码格式错误

            if (!accountCache.HasAccaount(account)) return -2;//没有此账号

            if (!accountCache.MatchAccaount(account, password)) return -3;//密码不匹配

            if (accountCache.IsOnline(account)) return -4 ;//账号已登录

            accountCache.OnLine(token,account); return 1;//登录成功
        }
        public int Modify(NetFrame.UserToken token, string account, string password) 
        {
            if (account.Length < 3 || password.Length < 3) return -1;//账号密码格式错误
            if (!accountCache.HasAccaount(account)) return -2;//没有此账号
            if (!accountCache.ModifyPassword(account, password)) return -3;//修改失败
            return 1;//密码修改成功
        
        }
        public void Close(NetFrame.UserToken token)
        {
            accountCache.OffLine(token);
        }

        public int GetAccountId(NetFrame.UserToken token)
        {
            return accountCache.GetAccountId(token);
        }
    }
}
