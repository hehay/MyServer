using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.dao;
using NetFrame;
using Protocols.dto;

namespace MyServer.biz.user
{
    public interface IUserBiz
    {
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="token"></param>
        /// <param name="name"></param>
        /// <param name="accountId"></param>
        int CreatRole(UserToken token, string name,int modelName);

        ///// <summary>
        ///// 更改角色
        ///// </summary>
        ///// <param name="token"></param>
        ///// <param name="accountId"></param>
        ///// <param name="roleId"></param>
        //void SelectRole(UserToken token, int roleId);


        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="token"></param>
        /// <param name="accountId"></param>
        /// <param name="roleId"></param>
        int DelectRole(UserToken token, int roleId,string name);

        UserDTO OnLine(UserToken token, int userId);
        void OffLine(UserToken token);
        List<UserDTO> GetRoleList(UserToken token);
        ///// <summary>
        ///// 获取链接对象
        ///// </summary>
        ///// <param name="token"></param>
        //void GetToken(UserToken token);
    }
}
