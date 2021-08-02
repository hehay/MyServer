using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFrame;

namespace MyServer.biz.accaount
{
   public interface IAccountBiz
    {
        /// <summary>
        /// 创建账号
        /// </summary>
        /// <param name="token">链接对象</param>
        /// <param name="accaount">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        int Creat(UserToken token, string account, string password);
        /// <summary>
        /// 登录账号
        /// </summary>
        /// <param name="token"></param>
        /// <param name="accaount"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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
        UserToken GetToken(int accountId);
    }
}
