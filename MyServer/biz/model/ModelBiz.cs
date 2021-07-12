using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.cache;
using MyServer.cache.accaount;
using MyServer.cache.model;
using MyServer.dao;
using NetFrame;
using Protocols.dto;

namespace MyServer.biz.accaount
{
    public class ModelBiz:IModelBiz
    {
        public IModelCache modelCache = cacheFactory.ModelCache;
        public IAccountBiz accountBiz = BizFactory.accountBiz;
       
        public void CreatModel(int id)
        {
            modelCache.AddModel(id);
        }
        public ModelDTO GetDtoFromModel(MODEL model) 
        {
            ModelDTO dto = new ModelDTO();
            dto.Id = model.Id;
            dto.Gold = model.Gold;
            return dto;


        }
        
        public ModelDTO OnLine(UserToken token, int userId)
        {
            int accountId = accountBiz.GetAccountId(token);//获取不到此用户
            if (accountId == -1) return null;
            if (modelCache.IsOnline(userId)) return null;//角色已登录
            MODEL model = modelCache.Online(token, userId);
            return GetDtoFromModel(model);
        }

        public void OffLine(UserToken token)
        {
            int accountId = accountBiz.GetAccountId(token);//获取不到此用户
            if (accountId == -1) return;
            modelCache.Offline(token, accountId);
        }

        public MODEL GetModelFromDto(ModelDTO dto)
        {
            MODEL model = new MODEL();
            model.Id = dto.Id;
            model.Gold = dto.Gold;
            return model;
        }

        
    }
}
