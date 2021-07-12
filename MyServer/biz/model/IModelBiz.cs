using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.dao;
using NetFrame;
using Protocols.dto;

namespace MyServer.biz.accaount
{
   public interface IModelBiz
    {
        void CreatModel(int id);
        ModelDTO GetDtoFromModel(MODEL model);
        ModelDTO OnLine(UserToken token, int userId);
        void OffLine(UserToken token);
        MODEL GetModelFromDto(ModelDTO dto);
        
    }
}
