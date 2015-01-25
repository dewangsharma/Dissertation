using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace myWCFClassLibrary
{
    [ServiceContract]
    public interface IModules
    {
        [OperationContract]
        IEnumerable<DepartmentModule> GetAllModulesByProfessor(int ProfessorId);

        [OperationContract]
        IEnumerable<DepartmentModule> GetAllModulesByStudent(int StudentId);

    }
    [DataContract]
    public class DepartmentModule
    {
        [DataMember]
        public int PK_Module_id { get; set; }
        [DataMember]
        public string module_name { get; set; }
        [DataMember]
        public Nullable<int> FK_Department_id { get; set; }
        [DataMember]
        public string module_code { get; set; }
        [DataMember]
        public string term { get; set; }
    
    }
}
