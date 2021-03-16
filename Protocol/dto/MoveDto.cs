using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols.dto
{
    [Serializable]
    public class MoveDto
    {
        public int userId;
        public float posx;
        public float posy;
        public float posz;
        public float rotax;
        public float rotay;
        public float rotaz;
        public float dirx;
        public float diry;
        public float dirz;
    }
}
