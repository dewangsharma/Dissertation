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
    
    public partial class tblStudent
    {
        public tblStudent()
        {
            this.tblFeedbackResults = new HashSet<tblFeedbackResult>();
            this.tblStudentLogins = new HashSet<tblStudentLogin>();
            this.tblStudentModules = new HashSet<tblStudentModule>();
        }
    
        public int PK_Student_id { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public string profile_pic { get; set; }
        public Nullable<int> FK_University_id { get; set; }
        public string department_name { get; set; }
        public string course_name { get; set; }
        public Nullable<int> semester { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string pass_value { get; set; }
        public string verification_code { get; set; }
        public Nullable<int> status { get; set; }
    
        public virtual ICollection<tblFeedbackResult> tblFeedbackResults { get; set; }
        public virtual ICollection<tblStudentLogin> tblStudentLogins { get; set; }
        public virtual ICollection<tblStudentModule> tblStudentModules { get; set; }
        public virtual tblUniversity tblUniversity { get; set; }
    }
}