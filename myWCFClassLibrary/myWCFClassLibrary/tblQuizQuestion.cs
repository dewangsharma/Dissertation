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
    
    public partial class tblQuizQuestion
    {
        public tblQuizQuestion()
        {
            this.tblQuizOptions = new HashSet<tblQuizOption>();
            this.tblQuizResults = new HashSet<tblQuizResult>();
        }
    
        public int PK_Question_id { get; set; }
        public int FK_Quiz_id { get; set; }
        public string question { get; set; }
        public string question_type { get; set; }
        public string hint { get; set; }
        public Nullable<double> marks { get; set; }
    
        public virtual tblQuizDetail tblQuizDetail { get; set; }
        public virtual ICollection<tblQuizOption> tblQuizOptions { get; set; }
        public virtual ICollection<tblQuizResult> tblQuizResults { get; set; }
    }
}
