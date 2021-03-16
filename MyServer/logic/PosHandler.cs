using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.biz;
using MyServer.biz.pos;
using NetFrame;
using Protocols;
using Protocols.dto;

namespace MyServer.logic
{
    public class PosHandler:AbsOnceHandler,HandlerInterface
    {
        public IPosBiz PosBiz = BizFactory.PosBiz;
        public void ClientClose(NetFrame.UserToken token, string error)
        {
           
        }

        public void MessageReceive(NetFrame.UserToken token, NetFrame.Auto.SocketModel message)
        {
            switch (message.command)
            {
                case PosProtocol.GetPos_CREQ:
                    GetPos(token);
                    break;
                case PosProtocol.UpdatePos_CREQ:
                    UpdatePos(token,message.GetMessage<PosDto>());
                    break;
                case PosProtocol.DeletePos_CREQ:
                    DeletePos(token);
                    break;
            }
        }

        void GetPos(UserToken token)
        {
           PosDto posDto= PosBiz.GetPos(token);
            Write(token,PosProtocol.GetPos_SRES,posDto);
        }

        void UpdatePos(UserToken token,PosDto posDto)
        {
            PosBiz.UpdatePos(token,posDto);            
        }

        void DeletePos(UserToken token)
        {
            PosBiz.DeletePos(token);
        }
        public override byte GetType()
        {
            return Protocol.Pos;
        }
    }
}
