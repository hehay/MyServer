using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFrame;
using NetFrame.Auto;

namespace MyServer.logic
{
   public class AbsMulitHandler:AbsOnceHandler
    {

        /* List<UserToken> tokens=new List<UserToken>();

         public bool Enter(UserToken token)
         {
             if (tokens.Contains(token))
             {
                 return false;
             }
             tokens.Add(token);
             return true;
         }

         public bool IsEnter(UserToken token)
         {
             if (tokens.Contains(token))
             {
                 return true;
             }
             return false;
         }

         public bool Leave(UserToken token)
         {
             if (tokens.Contains(token))
             {
                 tokens.Remove(token);
                 return true;
             }
             return false;
         }*/

        #region 消息群发API

        public void Brocast(List<UserToken> tokens, int command, object message, UserToken exToken = null)
        {
            Brocast(tokens, GetArea(), command, message, exToken);
        }
        public void Brocast(List<UserToken> tokens, int area, int command, object message, UserToken exToken = null)
        {
            Brocast(tokens, GetType(), area, command, message, exToken);
        }
        public void Brocast(List<UserToken> tokens, byte type, int area, int command, object message, UserToken exToken = null)
        {
            byte[] value = MessageEncoding.encode(CreatSocketModel(type, area, command, message));
            value = LengthEncoding.encode(value);
            foreach (UserToken item in tokens)
            {
                if (item != exToken)
                {
                    byte[] bs = new byte[value.Length];
                    Array.Copy(value, 0, bs, 0, value.Length);
                    item.write(bs);
                }
            }
        }
        #endregion
    }
}
