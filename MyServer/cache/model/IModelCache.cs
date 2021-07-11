using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.dao;
using NetFrame;
using Protocols.dto;

namespace MyServer.cache.model
{
    public interface IModelCache
    {
        bool ContainsModel(int id);

        void AddModel(int accountId);

        MODEL GetModelById(int id);

        bool IsOnline(int id);

        MODEL Online(UserToken token, int id);

        void Offline(UserToken token, int id);
    }
}
