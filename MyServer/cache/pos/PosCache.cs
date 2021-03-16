using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.dao;
using NetFrame;
using Protocols.dto;

namespace MyServer.cache.pos
{
    public class PosCache:IPosCache
    {
        public Dictionary<int, POS> UserIdToPosDic = new Dictionary<int, POS>(); 
        public POS CreatPos(int id)
        {
            POS pos = new POS();//默认开始坐标id, 7, 6, -6, 0, 0, 0
            pos.Id = id;
            pos.Posx = 7;
            pos.Posy = 6;
            pos.Posz = -6;
            pos.Rotax = 0;
            pos.Rotay = 0;
            pos.Rotaz = 0;
            UserIdToPosDic.Add(id, pos);
            return pos;
        }

        public POS GetPos(int id)
        {
            return UserIdToPosDic[id];           
        }

        public void DeletePos(int id)
        {
            UserIdToPosDic.Remove(id);
        }


        public bool IsInPosDic(int id)
        {
            if (UserIdToPosDic.ContainsKey(id))  return true;
            return false;
        }


    }
}
