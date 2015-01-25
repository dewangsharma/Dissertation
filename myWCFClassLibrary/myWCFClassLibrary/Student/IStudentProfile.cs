using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


namespace myWCFClassLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IStudentProfile
    {
        [OperationContract]
        string CreateStudent(StudentProfile stdntProfile);
        

      
        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "myWCFClassLibrary.ContractType".
    [DataContract]
    public class StudentProfile
    {
        [DataMember]
        public int PK_Student_id { get; set; }
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
        public string course_name { get; set; }
        [DataMember]
        public Nullable<int> semester { get; set; }
        [DataMember]
        public string mobile { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string pass_value { get; set; }
        [DataMember]
        public string verification_code { get; set; } 
        [DataMember]
        public int status{get;set;}

    }
}
