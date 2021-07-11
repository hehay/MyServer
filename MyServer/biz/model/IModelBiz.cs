using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFrame;

namespace MyServer.biz.accaount
{
   public interface IModelBiz
    {
        /// <summary>
        /// 创建账号
        /// </summary>
        /// <param name="token">链接对象</param>
        /// <param name="accaount">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        void CreatModel(int id);
        
        int Login(UserToken token, string account, string password);

        int Modify(UserToken token, string account, string oldPassword,string newPassword);
        /// <summary>
        /// 用户断开
        /// </summary>
        /// <param name="token"></param>
        void Close(UserToken token);
        /// <summary>
        /// 获取链接对象
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        int GetAccountId(UserToken token);
    }
}
