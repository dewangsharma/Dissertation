//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace myWCFClassLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblProfessorModule
    {
        public int PK_ProfessorModule_id { get; set; }
        public Nullable<int> FK_Professor_id { get; set; }
        public Nullable<int> FK_Module_id { get; set; }
    
        public virtual tblDepartmentModule tblDepartmentModule { get; set; }
        public virtual tblProfessor tblProfessor { get; set; }
    }
}
