using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFrame;
using Protocols.dto;

namespace MyServer.biz.pos
{
    public interface IPosBiz
    {

        PosDto CreatePos(UserToken token);
        PosDto GetPos(UserToken token);
        void DeletePos(UserToken token);
        void UpdatePos(UserToken token, PosDto posDto);
    }
}
