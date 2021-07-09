using System;
using System.Collections.Generic;
using System.Text;

namespace Protocols.dto
{
    [Serializable]
    public class ModifyDTO
    {
        public string account;
        public string oldPassword;
        public string newPassword;
    }

}
