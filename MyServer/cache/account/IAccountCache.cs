using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFrame;
using Protocols.dto;

namespace MyServer.cache.accaount
{
    public interface IAccountCache
    {
        /// <summary>
        /// 是否已有此账号
        /// </summary>
        /// <returns></returns>
        bool HasAccaount(string account);
        /// <summary>
        /// 账号密码是否匹配
        /// </summary>
        /// <returns></returns>
        bool MatchAccaount(string account,string password);

        int ModifyPassword(string account,string oldPassword,string newPassword);
        /// <summary>
        /// 是否在线
        /// </summary>
        /// <returns></returns>
        bool IsOnline(string account);
        /// <summary>
        /// 上线
        /// </summary>
        /// <param name="token"></param>
        /// <param name="account"></param>
        void OnLine(UserToken token, string account);
        /// <summary>
        /// 下线
        /// </summary>
        /// <param name="token"></param>
        void OffLine(UserToken token);
        /// <summary>
        /// 获取链接对象Id
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        int GetAccountId(UserToken token);
        /// <summary>
        /// 添加账号
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        void AddAccaount(string account, string password);
    }
}
