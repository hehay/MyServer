using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.dao
{
    public class POS
    {
        private int id;
        private float posx;
        private float posy;
        private float posz;
        private float rotax;
        private float rotay;
        private float rotaz;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public float Posx
        {
            get { return posx; }
            set { posx = value; }
        }

        public float Posy
        {
            get { return posy; }
            set { posy = value; }
        }

        public float Posz
        {
            get { return posz; }
            set { posz = value; }
        }

        public float Rotax
        {
            get { return rotax; }
            set { rotax = value; }
        }

        public float Rotay
        {
            get { return rotay; }
            set { rotay = value; }
        }

        public float Rotaz
        {
            get { return rotaz; }
            set { rotaz = value; }
        }
    }
}
