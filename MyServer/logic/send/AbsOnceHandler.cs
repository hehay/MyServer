using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServer.biz;
using MyServer.biz.user;
using MyServer.cache;
using MyServer.cache.user;
using NetFrame;
using NetFrame.Auto;

namespace MyServer.logic
{
    public class AbsOnceHandler
    {
        private byte type;
        private int area;
        public IUserCache userCache = cacheFactory.UserCache;
        public void  SetType(byte _type)
        {
            type = _type;
        }
        public virtual byte GetType()
        {
            return type; 
        }

        public void SetArea(int _area)
        {
            area = _area;
        }

        public virtual int GetArea()
        {
            return area;
        }

        /// <summary>
        /// 通过用户ID获取连接
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserToken getToken(int id)
        {
            return userCache.GeTokenById(id);
        }

#region 通过链接对象发送
        public void Write(UserToken token, int command)
        {
            Write(token,command,null);
        }
        public void Write(UserToken token, int command, object message)
        {
            Write(token,GetArea(),command,message);
        }

        public void Write(UserToken token, int area, int command, object message)
        {
            Write(token,GetType(),GetArea(),command,message);
        }
        public void Write(UserToken token,byte type, int area, int command, object message)
        {
            byte[] send = MessageEncoding.encode(CreatSocketModel(type, area, command, message));
            send = LengthEncoding.encode(send);
            token.write(send);
        }
#endregion

        #region 通过ID发送
        public void Write(int id, int command)
        {
            Write(id, command, null);
        }
        public void Write(int id, int command, object message)
        {
            Write(id, GetArea(), command, message);
        }
        public void Write(int id, int area, int command, object message)
        {
            Write(id, GetType(), area, command, message);
        }
        public void Write(int id, byte type, int area, int command, object message)
        {
            UserToken token = getToken(id);
            if (token == null) return;
            Write(token, type, area, command, message);
        }

        public void WriteToUsers(int[] users, byte type, int area, int command, object message)
        {
            byte[] value = MessageEncoding.encode(CreatSocketModel(type, area, command, message));
            value = LengthEncoding.encode(value);
            foreach (int item in users)
            {
                UserToken token = userCache.GeTokenById(item);
                if (token == null) continue;
                byte[] bs = new byte[value.Length];
                Array.Copy(value, 0, bs, 0, value.Length);
                token.write(bs);

            }
        }


        #endregion
        public SocketModel CreatSocketModel(byte type, int area, int command, object message)
        {
            return new SocketModel(type,area,command,message);
        }
    }
}
