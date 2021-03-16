using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.logic;
using NetFrame;
using NetFrame.Auto;
using Protocols;

namespace MyServer
{
   public class HandlerCenter:AbsHandlerCenter
   {
       private int index=0;
       private HandlerInterface Accaount;
       private HandlerInterface User;
       private HandlerInterface Map;
       private HandlerInterface Pos;
       private HandlerInterface Skill;
       private HandlerInterface Inventory;
       public  HandlerCenter()
       {
           Accaount=new AccountHandler();
           User=new UserHandler();
           Map=new MapHandler();
           Pos=new PosHandler();
           Skill=new skillHandler();
           Inventory=new InventoryHandler();
       }
        public override void ClientConnect(UserToken token)
        {
            Console.WriteLine("有客户端连接了");
            index++;
            Console.WriteLine("当前连接人数:"+index);
        }

        public override void MessageReceive(UserToken token, object message)
        {
            SocketModel model = message as SocketModel;
            switch (model.type)
            {
                case Protocol.Accaount:
                    Accaount.MessageReceive(token,model);
                    break;
                case Protocol.User:
                    User.MessageReceive(token,model);
                    break;
                case Protocol.Map:
                    Map.MessageReceive(token,model);
                    break;
                case Protocol.Pos:
                    Pos.MessageReceive(token,model);
                    break;
                case Protocol.Skill:
                    Skill.MessageReceive(token,model);
                    break;
                case Protocol.Inventory:
                    Inventory.MessageReceive(token,model);
                    break;
            }
        }

        public override void ClientClose(UserToken token, string error)
        {
            Console.WriteLine("有客户端断开了");
            index--;
            Console.WriteLine("当前连接人数:" + index);
            Pos.ClientClose(token,error);
            Inventory.ClientClose(token,error);
            Skill.ClientClose(token,error);
            Map.ClientClose(token, error);
            User.ClientClose(token, error);
            Accaount.ClientClose(token,error);
        }
    }
}
