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
        void CreateUser(int accountId, string nickname);
        UserDTO OnLine(UserToken token, int userId);
        void OffLine(UserToken token);
        List<UserDTO> GetRoleList(UserToken token);
        ///// <summary>
        ///// 获取链接对象
        ///// </summary>
        ///// <param name="token"></param>
        //void GetToken(UserToken token);
        UserDTO GetUserDtoByToken(UserToken token);
        void GetMatchPlayer(UserToken token, int model,out List<MatchDTO> players,out List<UserToken> tokens);
        void MatchConfirm(UserToken token, int model, out int confirmCount, out List<UserToken> tokens);
        void MatchResult(UserToken token, int model, out int result, out List<UserToken> tokens);
        void StopMatchPlayer(UserToken token,int model);
        void SelectHero(UserToken token, MatchDTO matchDTO, out List<UserToken> tokens, out List<MatchDTO> matchDTOs);
        List<UserToken> GetCompose(UserToken token);
    }
}
