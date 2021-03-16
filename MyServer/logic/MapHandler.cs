using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.cache;
using MyServer.cache.accaount;
using MyServer.cache.user;
using MyServer.dao;
using NetFrame;
using Protocols;
using Protocols.dto;

namespace MyServer.logic
{
    public class MapHandler :AbsMulitHandler, HandlerInterface

    {
        private IAccountCache accountCache = cacheFactory.AccaountCache;
        private IUserCache userCache = cacheFactory.UserCache;
        public ConcurrentDictionary<int, MapRoom> mapToRoom = new ConcurrentDictionary<int, MapRoom>();
        public static MapRoom AllRoom;
        public MapHandler()
        {
            AllRoom=new MapRoom();
            AllRoom.SetArea(-1);
            mapToRoom.TryAdd(-1, AllRoom);

            MapRoom mapRoom3 = new MapRoom();
            mapRoom3.SetArea(3);
            mapToRoom.TryAdd(3, mapRoom3); //地图4


             MapRoom mapRoom4 = new MapRoom();
            mapRoom4.SetArea(4);
            mapToRoom.TryAdd(4, mapRoom4); //地图3


             MapRoom mapRoom5 = new MapRoom();
             mapRoom5.SetArea(5);
             mapToRoom.TryAdd(5, mapRoom5); //地图4


             MapRoom mapRoom6 = new MapRoom();
            mapRoom6.SetArea(6);
            mapToRoom.TryAdd(6, mapRoom6); //地图4


             MapRoom mapRoom7 = new MapRoom();
             mapRoom7.SetArea(7);
             mapToRoom.TryAdd(7, mapRoom7); //地图5

        }

        public void ClientClose(UserToken token, string error)
        {
            USER user = userCache.GetUserByAccId(accountCache.GetAccountId(token));
            if (user != null)
            {
                mapToRoom[user.Map].ClientClose(token, error);
                AllRoom.Leave(token);
            }
        }

        public void MessageReceive(UserToken token, NetFrame.Auto.SocketModel message)
        {
            mapToRoom[message.area].MessageReceive(token, message);
        }


  
    }
}
