using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.cache;
using MyServer.cache.accaount;
using MyServer.cache.model;

namespace MyServer.biz.accaount
{
    public class ModelBiz:IModelBiz
    {
        public IModelCache modelCache = cacheFactory.ModelCache;
        public IAccountBiz accountBiz = BizFactory.accountBiz;
       
        public void CreatModel(int id)
        {
            modelCache.AddModel(id);
        }

        public int Login(NetFrame.UserToken token, string account, string password)
        {
            if (account.Length < 3 || password.Length < 3) return -1;//账号密码格式错误

            if (!accountCache.HasAccaount(account)) return -2;//没有此账号

            if (!accountCache.MatchAccaount(account, password)) return -3;//密码不匹配

            if (accountCache.IsOnline(account)) return -4 ;//账号已登录

            accountCache.OnLine(token,account); return 1;//登录成功
        }
        public int Modify(NetFrame.UserToken token, string account, string oldPassword,string newPassword) 
        {
            if (account.Length < 3 || oldPassword.Length < 3||newPassword.Length<3) return -3;//账号密码格式错误
            return accountCache.ModifyPassword(account,oldPassword,newPassword);
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
