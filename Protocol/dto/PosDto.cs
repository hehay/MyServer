using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols.dto
{
    [Serializable]
    public class PosDto
    {
        public int id;
        public float posx;
        public float posy;
        public float posz;
        public float rotax;
        public float rotay;
        public float rotaz;
        public PosDto() { }

        public PosDto(int _id, float _posx, float _posy, float _posz, float _rotax, float _rotay, float _rotaz)
        {
            id = _id;
            posx = _posx;
            posy = _posy;
            posz = _posz;
            rotax = _rotax;
            rotay = _rotay;
            rotaz = _rotaz;
        }
    }
}
