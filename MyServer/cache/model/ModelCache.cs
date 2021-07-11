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

namespace MyServer.cache.model
{
    public class ModelCache:IModelCache
    {
        //public int index = 1;

        public Dictionary<int,MODEL> idAndModel=new Dictionary<int, MODEL>(); 
        public Dictionary<int, UserToken> idAndToken = new Dictionary<int, UserToken>();
        //public Dictionary<UserToken, int> tokenAndId = new Dictionary<UserToken, int>();
        //public Dictionary<int, MODEL> accIdAndModel = new Dictionary<int, MODEL>();
        private XmlDocument modelDoc = new XmlDocument();
        private string _path;
        
        public ModelCache(string path) 
        {
            _path = path;
            ReadModelFile();
            
        }
        public void ReadModelFile() 
        {
            if (!File.Exists(_path))
            {
                XmlElement root = modelDoc.CreateElement("Root");
                modelDoc.AppendChild(root);
                modelDoc.Save(_path);

            }
            modelDoc.Load(_path);
            XmlNodeList nodeList = modelDoc.SelectSingleNode("Root").ChildNodes;
            foreach (XmlElement node in nodeList)
            {
                MODEL model = new MODEL
                {
                    Id = int.Parse(node.GetAttribute("id")),
                    Gold =int.Parse(node.GetAttribute("金币")),
                
                };
                idAndModel.Add(model.Id, model);
            }
        }
        public void AddModelToFile(MODEL model)
        {
            XmlNode node = modelDoc.SelectSingleNode("Root");
            XmlElement element = modelDoc.CreateElement("Model");
            element.SetAttribute("id", model.Id.ToString());
            element.SetAttribute("金币", model.Gold.ToString());
            node.AppendChild(element);
            modelDoc.Save(_path);
        }
        public void ModifyAccountToFile(MODEL model)
        {
            XmlNodeList nodeList = modelDoc.SelectSingleNode("Root").ChildNodes;
            foreach (XmlElement element in nodeList)
            {
                if (element.GetAttribute("id") ==model.Id.ToString())
                {
                    element.SetAttribute("金币", model.Gold.ToString());
                }
            }
            modelDoc.Save(_path);
        }
        public bool ContainsModel(int id) 
        {
            return idAndModel.ContainsKey(id);
        }
        public void AddModel(int accountId) 
        {
            MODEL model = new MODEL
            {
                Id = accountId,
                Gold = 0
            
            };
            idAndModel.Add(model.Id,model);
        }
        public MODEL GetModelById(int id) 
        {
            if (idAndModel.ContainsKey(id)) return idAndModel[id];
            return null;
        }
        public bool IsOnline(int id) 
        {
            return idAndToken.ContainsKey(id);
        }

        public MODEL Online(UserToken token, int id) 
        {
            if (idAndToken.ContainsKey(id)) idAndToken.Remove(id);
            idAndToken.Add(id, token);
            return idAndModel[id];
        }
        public void Offline(UserToken token, int id) 
        {
            Console.WriteLine("走了ModelCache的Offline");
        }
    }
}
