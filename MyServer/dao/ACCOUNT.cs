using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.dao
{
   public  class ACCOUNT
   {
       private int id;

       private string account;
       private string password;

       public int Id
       {
           set { id = value; }
           get { return id; }
       }
       public string Account
       {
           set { account = value; }
           get { return account; }
       }

       public string Password
       {
           set { password = value; }
           get { return password; }
       }
   }
   /* public class MODIFY
    {

        private int id;
        private string account;
        private string oldPassword;
        private string newPassword;

        public int Id
        {
            set { id = value; }
            get { return id; }
        }
        public string Account
        {
            set { account = value; }
            get { return account; }
        }

        public string OldPassword
        {
            set { oldPassword = value; }
            get { return oldPassword; }
        }
        public string NewPassword
        {
            set { newPassword = value; }
            get { return newPassword; }
        }
    }*/
}
