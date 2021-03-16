using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyServer.tool
{
    public delegate void  ExecutorDelegateEvent();
    public class ExecutorPool
    {
        private static ExecutorPool _instance;

        public static ExecutorPool Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance=new ExecutorPool();
                }
                return _instance;
            } 
        }

        Mutex _mutex=new Mutex();

        public void Executor(ExecutorDelegateEvent e)
        {
            lock (this)
            {
                _mutex.WaitOne();
                e();
                _mutex.ReleaseMutex();
            }
        }
    }
}
