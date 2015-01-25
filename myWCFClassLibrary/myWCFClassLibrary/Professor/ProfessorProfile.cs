using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;


namespace myWCFClassLibrary.Professor
{
   public class ProfessorProfile : IProfessorProfile
    {
       
       
        public string CreateProfessor(Professor_Profile prfsrProfile)
        {
            string result;
            Service1 s = new Service1(); // use some student Registration code
            bool exEmail = s.ExistingEmail(prfsrProfile.email); //check whether the email is existing or not 
            if (exEmail)
            {
                using (myProjectEntities context = new myProjectEntities())
                {
                    string vCode = Guid.NewGuid().ToString(); // create verification code
                    prfsrProfile.verification_code = vCode;
                    prfsrProfile.profile_pic = "~/Images/profile.png";
                    prfsrProfile.status = 0;
                    
                    Mapper.CreateMap<Professor_Profile, tblProfessor>();
                    tblProfessor newProf = Mapper.Map<Professor_Profile, tblProfessor>(prfsrProfile);

                    context.tblProfessors.Add(newProf);
                    int qq = context.SaveChanges();
                    result = qq.ToString();

                    bool sendMail = s.SendEmail(prfsrProfile.firstname + " " + prfsrProfile.middlename + " " + prfsrProfile.lastname, prfsrProfile.email, "Account Verification", prfsrProfile.verification_code);


                }
            }
            else result = "Existing Email";
           
            return result;
        }

       public int[] Dashboard(int ProfId)
       {
           using (myProjectEntities context = new myProjectEntities())
           {
               int[] reply = new int[4];
               
               int studentCount = 0; int activeQuizCount = 0; int activeLectureCount = 0; int quiz = 0;
               List<tblProfessorModule> moduleList = context.tblProfessorModules.Where(q => q.FK_Professor_id == ProfId).ToList();
               foreach (tblProfessorModule mList in moduleList)
               {
                   int student = context.tblStudentModules.Where(q => q.FK_Module_id == mList.FK_Module_id).Count();
                   studentCount += student;
                   int mId = context.tblQuizDetails.Where(q => q.FK_ModuleId == mList.FK_Module_id && System.DateTime.Now <= q.quiz_end_date).Count();
                   activeQuizCount += mId;
                   int lId = context.tblFeedbacks.Where(q => q.tblLecture.FK_Module_id == mList.FK_Module_id).Count();
                   activeLectureCount += lId;
                   int qId = context.tblQuizDetails.Where(q => q.FK_ModuleId == mList.FK_Module_id && System.DateTime.Now == q.quiz_end_date).Count();
                   quiz += qId;
               
               }
               reply[0] = studentCount;
               reply[1] = activeQuizCount;
               reply[2] = activeLectureCount;
               reply[3] = quiz;

               return reply;
           }
       }
    }
}
