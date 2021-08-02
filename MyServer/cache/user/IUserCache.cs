using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.dao;
using NetFrame;
using Protocols.dto;

namespace MyServer.cache.user
{
   public interface IUserCache
   {
       /// <summary>
       /// 获取角色数量
       /// </summary>
       /// <param name="accountId"></param>
       /// <returns></returns>
       int GetRoleCount(int accountId);


       /// <summary>
       /// 获取全部角色
       /// </summary>
       /// <param name="accountId"></param>
       /// <returns></returns>
       List<USER> GetRoleList(int accountId); 
       /// <summary>
       /// 根据名字获取角色
       /// </summary>
       /// <param name="name"></param>
       /// <returns></returns>
       bool GetUserByName(string name);
       /// <summary>
       /// 增加角色
       /// </summary>
       /// <param name="accountId"></param>
       /// <param name="roleId"></param>
       /// <param name="name"></param>
       /// <param name="modelName"></param>
       /// <returns></returns>
       bool AddRole(int accountId,string name,int modelName);
       ///// <summary>
       ///// 选择角色
       ///// </summary>
       ///// <param name="accountId"></param>
       ///// <param name="roleId"></param>
       ///// <returns></returns>
       //USER SelectRole(int accountId, int roleId);
       /// <summary>
       /// 删除角色
       /// </summary>
       /// <param name="accountId"></param>
       /// <param name="roleId"></param>
       /// <param name="name"></param>
       /// <returns></returns>
       bool DeleteRole(int accountId, int roleId,string name);

       USER GetUserByAccId(int accountId);

       USER GetUserById(int id);
       bool IsOnLine(int userId);
       USER OnLine(UserToken token, int userId, int accountId);

       void OffLine(UserToken token, int accountId);

       int GetUserId(UserToken token);
       UserToken GeTokenById(int id);
       USER GetUserByToken(UserToken token);
       List<MatchDTO> GetMatchPlayer(int accountId, int model);
   }
}
