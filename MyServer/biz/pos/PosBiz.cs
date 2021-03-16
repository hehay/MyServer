using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.cache;
using MyServer.cache.user;
using MyServer.dao;
using NetFrame;
using Protocols.dto;

namespace MyServer.biz.pos
{
    public class PosBiz:IPosBiz
    {
        private IPosCache posCache = cacheFactory.PosCache;
        private IUserCache userCache = cacheFactory.UserCache;
        /// <summary>
        /// 创建角色初始坐标
        /// </summary>
        /// <param name="token"></param>
        public PosDto CreatePos(NetFrame.UserToken token)
        {
            int id = userCache.GetUserId(token);
            if (!posCache.IsInPosDic(id))
            {
                POS pos = posCache.CreatPos(id);
                PosDto posDto = new PosDto(pos.Id, pos.Posx, pos.Posy, pos.Posz, pos.Rotax, pos.Rotay, pos.Rotaz);
                return posDto;
            }
            return null;
        }
        /// <summary>
        /// 获取角色坐标
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public PosDto GetPos(NetFrame.UserToken token)
        {
            int id = userCache.GetUserId(token);
            if (posCache.IsInPosDic(id))
            {
                POS pos = posCache.GetPos(id);
                PosDto posDto = new PosDto(pos.Id, pos.Posx, pos.Posy, pos.Posz, pos.Rotax, pos.Rotay, pos.Rotaz);
                return posDto;
            }
            else
            {
                return CreatePos(token);//如果没有坐标，则创建初始坐标
            }

        }
        /// <summary>
        /// 删除角色坐标
        /// </summary>
        /// <param name="token"></param>
        public void DeletePos(NetFrame.UserToken token)
        {
            int id = userCache.GetUserId(token);
            if (posCache.IsInPosDic(id))
            {
                posCache.DeletePos(id);
            }
        }
        /// <summary>
        /// 更新角色坐标
        /// </summary>
        /// <param name="token"></param>
        /// <param name="posDto"></param>
        public void UpdatePos(UserToken token, PosDto posDto)
        {
            int id = userCache.GetUserId(token);
            if (posCache.IsInPosDic(id))
            {
                POS pos = posCache.GetPos(id);
                pos.Id = id;
                pos.Posx = posDto.posx;
                pos.Posy = posDto.posy;
                pos.Posz = posDto.posz;
                pos.Rotax = posDto.rotax;
                pos.Rotay = posDto.rotay;
                pos.Rotaz = posDto.rotaz;
            }
        }

    }
}
