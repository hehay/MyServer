using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.biz;
using MyServer.biz.accaount;
using MyServer.tool;
using NetFrame;
using NetFrame.Auto;
using Protocols;
using Protocols.dto;


namespace MyServer.logic
{
    public class AccountHandler:AbsOnceHandler,HandlerInterface
    {
        private IAccountBiz accaount = BizFactory.accountBiz;
        public void ClientClose(UserToken token, string error)
        {
            ExecutorPool.Instance.Executor(
                    delegate()
                    {
                        accaount.Close(token);
                    }
                    );
        }

        public void MessageReceive(UserToken token,SocketModel message)
        {
            switch (message.command)
            {
                case AccountProtocol.Login_CREQ:
                    Login(token,message.GetMessage<AccountDTO>());
                    break;
                case AccountProtocol.Reg_CREQ:
                    Creat(token,message.GetMessage<AccountDTO>());
                    break;
                case AccountProtocol.Modify_CREQ:
                    Modify(token,message.GetMessage<ModifyDTO>());
                    break;
                case AccountProtocol.SetModel_CREQ:
                    SetModel(token, message.GetMessage<int>());
                    break;
            }
        }
        public void SetModel(UserToken token,int model) 
        {
            accaount.SetModel(token, model);
             
        }

        void Login(UserToken token, AccountDTO dto)
        {
            ExecutorPool.Instance.Executor(delegate
            {
                int returnNum = accaount.Login(token, dto.account, dto.password);
                // 返回客户端
                Write(token,AccountProtocol.Login_SRES,returnNum);
            });

        }

        void Creat(UserToken token, AccountDTO dto)
        {
            ExecutorPool.Instance.Executor(delegate
            {
                int returnNum = accaount.Creat(token, dto.account, dto.password);
                //返回客户端
                Write(token, AccountProtocol.Reg_SRES, returnNum);
            });
        }
        void Modify(UserToken token,ModifyDTO dto) 
        {
            ExecutorPool.Instance.Executor(delegate 
            {
                int returnNum = accaount.Modify(token, dto.account, dto.oldPassword,dto.newPassword);
                Write(token,AccountProtocol.Modify_SRES ,returnNum);
            }
                
                
                );
        }
        public override byte GetType()
        {
            return Protocol.Account;
        }
        
    }
}
