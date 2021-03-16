using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.dao;
using NetFrame;
using Protocols.dto;

namespace MyServer.cache
{
    public interface IPosCache
    {
        POS CreatPos(int userId);
        POS GetPos(int id);
        void DeletePos(int id);

        bool IsInPosDic(int id);
    }
}
