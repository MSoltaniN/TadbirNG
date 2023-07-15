using System;

namespace SPPC.Tools.Model
{
    public class UserInfo
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public override string ToString()
        {
            return $"{UserName} (Id : {UserID}, FullName : {FullName})";
        }
    }
}
