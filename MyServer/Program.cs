using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.tool;
using NetFrame;
using NetFrame.Auto;

using MyServer.dao;

namespace MyServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerStart serverStart = new ServerStart(10);
            serverStart.LD = LengthEncoding.decode;
            serverStart.LE = LengthEncoding.encode;
            serverStart.encode = MessageEncoding.encode;
            serverStart.decode = MessageEncoding.decode;
            serverStart.center=new HandlerCenter();
            serverStart.Start(6650);
            Console.WriteLine("服务器已启动");
            Console.WriteLine("hello");
            SkillInitialProperty skillInitialProperty=new SkillInitialProperty();//初始化技能列表
            InventoryInItialProperty inventoryInItialProperty=new InventoryInItialProperty();//初始化物品列表
            while (true)
            {
                
            }
        }
    }
}
