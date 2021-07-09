using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MyServer.cache.user;
using MyServer.dao;
using NetFrame;

namespace MyServer.cache.accaount
{
    public class AccountCache:IAccountCache
    {
        public int index=1;
        public Dictionary<UserToken,string> OnLineDictionary=new Dictionary<UserToken, string>(); 
        public Dictionary<string,ACCOUNT> NameAndAccount=new Dictionary<string, ACCOUNT>();
        private XmlDocument accountDoc = new XmlDocument();
        private string _path;
        
        public AccountCache(string path) 
        {
            _path = path;
            ReadAccountFile();
            
        }
        public bool HasAccaount(string accaount)
        {
            return NameAndAccount.ContainsKey(accaount);
        }
        /// <summary>
        /// 账号和密码是否匹配
        /// </summary>
        /// <param name="accaount"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool MatchAccaount(string account, string password)
        {
            if (HasAccaount(account))
            {
                //byte[] x= Encoding.UTF8.GetBytes(password);
                //string p = Convert.ToBase64String(x);
                //加密解密todo
                bool isMatch = NameAndAccount[account].Password.Equals(password);
                return isMatch;
            }
            return false;
        }
        public int ModifyPassword(string account,string oldPassword,string newPassword)
        {

            if (HasAccaount(account)) 
            {
                if (NameAndAccount[account].Password != oldPassword)
                {
                    return -2;//旧密码错误
                }
                else 
                {
                    NameAndAccount[account].Password = newPassword;
                    ModifyAccountToFile(NameAndAccount[account]);
                    return 1;//修改成功
                }
            }
            return -1;//无此账号
        }
        /// <summary>
        /// 账号是否在线
        /// </summary>
        /// <param name="accaount"></param>
        /// <returns></returns>
        public bool IsOnline(string account)
        {
            return OnLineDictionary.ContainsValue(account);
        }
        /// <summary>
        /// 账号上线
        /// </summary>
        /// <param name="token"></param>
        /// <param name="accaount"></param>
        public void OnLine(NetFrame.UserToken token, string account)
        {
            OnLineDictionary.Add(token,account);
        }
        /// <summary>
        /// 下线
        /// </summary>
        /// <param name="token"></param>
        public void OffLine(NetFrame.UserToken token)
        {
            if(OnLineDictionary.ContainsKey(token))
            OnLineDictionary.Remove(token);
        }
        /// <summary>
        /// 获取链接对象id
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public int GetAccountId(NetFrame.UserToken token)
        {
            if (!OnLineDictionary.ContainsKey(token)) return -1;//没有此用户上线获取不到id
            return NameAndAccount[OnLineDictionary[token]].Id;
        }
        /// <summary>
        /// 添加账号
        /// </summary>
        /// <param name="accaount"></param>
        /// <param name="password"></param>
        public void CreateAccount(string account, string password)
        {
            index++;
            //创建账号实体并进行绑定
            ACCOUNT model = new ACCOUNT();
            model.Account = account;
            //model.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
            model.Password = password;
            model.Id = index;
            NameAndAccount.Add(account, model);
            
            //将新用户的信息保存到文件
            AddAccountToFile(account,password);
            
        }
        public void AddAccountToFile(string account,string password)
        {
            XmlNode node = accountDoc.SelectSingleNode("Root");
            XmlElement element = accountDoc.CreateElement("Account");
            element.SetAttribute("id", index.ToString());
            element.SetAttribute("账号", account);
            element.SetAttribute("密码", password);
            node.AppendChild(element);
            accountDoc.Save(_path);
        }
        public void ReadAccountFile()
        {
            if (!File.Exists(_path))
            {
                XmlElement root = accountDoc.CreateElement("Root");
                accountDoc.AppendChild(root);
                //XmlElement account = AccountDoc.CreateElement("Account");
                //account.SetAttribute("id", "001");
                //account.SetAttribute("账号", "0001");
                //account.SetAttribute("密码", "1234");
                //root.AppendChild(account);
                //XmlElement account1 = AccountDoc.CreateElement("Account");
                //account1.SetAttribute("id", "002");
                //account1.SetAttribute("账号", "0002");
                //account1.SetAttribute("密码", "1234");
                //root.AppendChild(account1);
                accountDoc.Save(_path);

            }
            accountDoc.Load(_path);
            XmlNodeList nodeList = accountDoc.SelectSingleNode("Root").ChildNodes;
            foreach (XmlElement node in nodeList)
            {
                ACCOUNT account = new ACCOUNT();
                account.Id = int.Parse(node.GetAttribute("id"));
                account.Account = node.GetAttribute("账号");
                account.Password = node.GetAttribute("密码");
                NameAndAccount.Add(account.Account, account);
            }
            index = nodeList.Count;
        }
        public void WriteAccountFile()
        {
            accountDoc.RemoveAll();
            XmlElement root = accountDoc.CreateElement("Root");
            accountDoc.AppendChild(root);
            foreach (var a in NameAndAccount.Values)
            {
                XmlElement account = accountDoc.CreateElement("Account");
                account.SetAttribute("id", a.Id.ToString());
                account.SetAttribute("账号", a.Account);
                account.SetAttribute("密码", a.Password);
                root.AppendChild(account);
            }
            accountDoc.Save(_path);

        }
        public void ModifyAccountToFile(ACCOUNT account)
        {
            XmlNodeList nodeList = accountDoc.SelectSingleNode("Root").ChildNodes;
            foreach (XmlElement element in nodeList)
            {
                if (element.GetAttribute("账号") == account.Account)
                {
                    element.SetAttribute("密码", account.Password);
                }
            }
            accountDoc.Save(_path);
        }

    }
}
