using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.biz;
using MyServer.biz.user;
using MyServer.tool;
using NetFrame;
using Protocols;
using Protocols.dto;

namespace MyServer.logic
{
    public  class UserHandler:AbsOnceHandler,HandlerInterface
    {
        public IUserBiz UserBiz=BizFactory.userBiz;

       
        public void ClientClose(NetFrame.UserToken token, string error)
        {
            UserBiz.OffLine(token);
        }

        public void MessageReceive(NetFrame.UserToken token, NetFrame.Auto.SocketModel message)
        {
          
            switch (message.command)
            {
                case UserProtocol.CreateRole_CREQ:
                    UserDTO userDto1 = message.GetMessage<UserDTO>();
                    CreateRole(token, userDto1.name, userDto1.modelName);
                    break;
                case UserProtocol.OnLine_CREQ:
                    UserDTO userDto2 = message.GetMessage<UserDTO>();
                    OnLine(token, userDto2.id);
                    break;
                case UserProtocol.OffLine_CREQ:
                    OffLine(token);
                    break;
                case UserProtocol.DeleteRole_CREQ:
                    UserDTO userDto3 = message.GetMessage<UserDTO>();
                    DelectRole(token, userDto3.id, userDto3.name);
                    break;
                case UserProtocol.GetRoleList_CREQ:
                    GetRoleList(token);
                    break;
            }

        }

        void CreateRole(NetFrame.UserToken token,string name,int modelName )
        {
            ExecutorPool.Instance.Executor(delegate
            {
                int i = UserBiz.CreatRole(token, name, modelName);//返回结果
                Write(token,UserProtocol.CreateRole_SRES,i);
            });
        }

        void OnLine(UserToken token,int roleId)
        {
            ExecutorPool.Instance.Executor(delegate
            {
               UserDTO i= UserBiz.OnLine(token,roleId);
                Write(token,UserProtocol.OnLine_SRES,i);

            });
        }

        void OffLine(UserToken token)
        {
            ExecutorPool.Instance.Executor(delegate
            {
                 UserBiz.OffLine(token);
            });
        }

        void DelectRole(UserToken token,int roleId,string name)
        {
            ExecutorPool.Instance.Executor(delegate
            {
                int i=UserBiz.DelectRole(token, roleId, name);
                Write(token,UserProtocol.DeleteRole_SRES,i);
            });
        }

        void GetRoleList(UserToken token)
        {
            ExecutorPool.Instance.Executor(delegate
            {
                List<UserDTO> userDtos = UserBiz.GetRoleList(token);
                Write(token,UserProtocol.GetRoleList_SRES,userDtos);
            });
        }
        public override byte GetType()
        {
            return Protocol.User;
        }
    }
}
