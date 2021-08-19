using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.biz;
using MyServer.biz.accaount;
using MyServer.biz.user;
using MyServer.tool;
using NetFrame;
using Protocols;
using Protocols.dto;

namespace MyServer.logic
{
    public  class UserHandler:AbsMulitHandler,HandlerInterface
    {
        public IUserBiz UserBiz=BizFactory.userBiz;
        public IAccountBiz accountBiz = BizFactory.accountBiz;
       
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
                    CreateRole(token, userDto1.nikename, userDto1.modelName);
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
                    DelectRole(token, userDto3.id, userDto3.nikename);
                    break;
                case UserProtocol.GetRoleList_CREQ:
                    GetRoleList(token);
                    break;
                case UserProtocol.GetUserDt_CREQ:
                    GetUserDt(token);
                    break;
                case UserProtocol.StartMatch_CREQ:
                    int model = message.GetMessage<int>();
                    GetMatchPlayer(token,model);
                    break;
                case UserProtocol.StopMatch_CREQ:
                    model = message.GetMessage<int>();
                    StopMatchPlayer(token,model);
                    break;
                case UserProtocol.MatchConfirm_CREQ:
                    model = message.GetMessage<int>();
                    MatchConfirm(token,model);
                    break;
                case UserProtocol.MatchResult_CREQ:
                    model = message.GetMessage<int>();
                    MatchResult(token, model);
                    break;
                case UserProtocol.GetHeroList_CREQ:
                    GetHeroList(token);
                    break;
                case UserProtocol.SelectHero_CREQ:
                    MatchDTO matchDto = message.GetMessage<MatchDTO>();
                    SelectHero(token,matchDto);
                    break;
            }

        }
        void SelectHero(UserToken token,MatchDTO matchDto) 
        {
            ExecutorPool.Instance.Executor(delegate
            {
                List<UserToken> tokens = new List<UserToken>();
                List<MatchDTO> matchDTOs = new List<MatchDTO>();
                UserBiz.SelectHero(token, matchDto, out tokens,out matchDTOs);
                Brocast(tokens, UserProtocol.SelectHero_BRO, matchDTOs);
            });

        }
        void GetHeroList(UserToken token) 
        {
            ExecutorPool.Instance.Executor(delegate 
            {
              
            });
        }
        void MatchConfirm(UserToken token, int model) 
        {
            ExecutorPool.Instance.Executor(delegate
            {
                int confirmCount = 0;
                List<UserToken> tokens = new List<UserToken>();
                UserBiz.MatchConfirm(token, model, out confirmCount, out tokens);
                for (int i = 0; i < tokens.Count; i++)
                {
                    Write(tokens[i], UserProtocol.MatchConfirm_SRES, confirmCount);
                    switch (model) 
                    {
                        case 0:
                            if (confirmCount == 2) Write(tokens[i], UserProtocol.MatchResult_SRES, 1);
                            break;
                        case 1:
                            if (confirmCount == 6) Write(tokens[i],UserProtocol.MatchResult_SRES,1);
                            break;
                        case 2:
                            if (confirmCount == 10) Write(tokens[i], UserProtocol.MatchResult_SRES, 1);
                            break;
                    }
                }
            });
        }
        void MatchResult(UserToken token, int model) 
        {
            ExecutorPool.Instance.Executor(delegate
            {
                List<UserToken> tokens = new List<UserToken>();
                int result = 0;
                UserBiz.MatchResult(token, model,out result, out tokens);
                if (tokens != null)
                {
                    for (int i = 0; i < tokens.Count; i++)
                    {
                        Write(tokens[i], UserProtocol.MatchResult_SRES, result);
                    }
                }
            });
        }
        void CreateRole(NetFrame.UserToken token,string name,int modelName )
        {
            ExecutorPool.Instance.Executor(delegate
            {
                int i = UserBiz.CreatRole(token, name, modelName);//返回结果
                Write(token,UserProtocol.CreateRole_SRES,i);
            });
        }
        void GetMatchPlayer(UserToken token,int model) 
        {
            ExecutorPool.Instance.Executor(delegate 
            {
                List<MatchDTO> players = new List<MatchDTO>();
                List<UserToken> tokens = new List<UserToken>();
                UserBiz.GetMatchPlayer(token, model,out players,out tokens);
                if (players != null) 
                {
                    for (int i = 0; i < tokens.Count; i++)
                    {
                        Write(tokens[i], UserProtocol.StartMatch_SRES, 1);
                    }
                    
                }
            });
           
        }
        void StopMatchPlayer(UserToken token,int model) 
        {
            UserBiz.StopMatchPlayer(token,model);
        }
        void OnLine(UserToken token,int roleId)
        {
            ExecutorPool.Instance.Executor(delegate
            {
               UserDTO userDTO= UserBiz.OnLine(token,roleId);
                Write(token,UserProtocol.OnLine_SRES,userDTO);

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
        void GetUserDt(UserToken token)
        {
            ExecutorPool.Instance.Executor(delegate 
            {
                UserDTO userDto = UserBiz.GetUserDtoByToken(token);
                Write(token, UserProtocol.GetUserDt_SRES, userDto);
            
            });
        }
    }
}
