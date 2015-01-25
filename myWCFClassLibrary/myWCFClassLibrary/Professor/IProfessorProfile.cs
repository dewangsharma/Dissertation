using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace myWCFClassLibrary.Professor
{
    [ServiceContract]
    public interface IProfessorProfile
    {
        [OperationContract]
        string CreateProfessor(Professor_Profile professorProfile);

        [OperationContract]
        int[] Dashboard(int ProfId);

    }

    [DataContract]
    public class Professor_Profile
    {
        [DataMember]
        public int PK_Professor_id { get; set; }
        [DataMember]
        public string firstname { get; set; }
        [DataMember]
        public string middlename { get; set; }
        [DataMember]
        public string lastname { get; set; }
        [DataMember]
        public string profile_pic { get; set; }
        [DataMember]
        public Nullable<int> FK_University_id { get; set; }
        [DataMember]
        public string department_name { get; set; }
        [DataMember]
        public Nullable<int> mobile { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string pass_value { get; set; }
        [DataMember]
        public string verification_code { get; set; }
        [DataMember]
        public Nullable<int> status { get; set; }
    
 
    }
}
