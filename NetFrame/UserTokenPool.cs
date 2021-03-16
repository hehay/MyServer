using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFrame
{
    public class UserTokenPool
    {
        private Stack<UserToken> Pool;

        public UserTokenPool (int maxNum)
        {
            Pool=new Stack<UserToken>(maxNum);
        }
        /// <summary>
        /// 获取长度
        /// </summary>
        /// <returns></returns>
        public int GetPoolCount()
        {
            return Pool.Count;
        }
        /// <summary>
        /// 取出
        /// </summary>
        /// <returns></returns>
        public UserToken Pop()
        {
            return Pool.Pop();
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="userToken"></param>
        public void Push(UserToken userToken)
        {
            if (userToken != null)
            {
                Pool.Push(userToken);
            }
        }
    }
}
