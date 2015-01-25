using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace myWCFClassLibrary
{
    [ServiceContract]
    public interface ILogin
    {
        [OperationContract]
        string[] userLogin(string username, string password); 

        [OperationContract]
        string passwordReset(string username, string currentPass, string newPass);

        [OperationContract]
        string accountVerification(string username,string verification, bool role);

        [OperationContract]
        bool verificationResend(string username, bool role);

        
    }
}
