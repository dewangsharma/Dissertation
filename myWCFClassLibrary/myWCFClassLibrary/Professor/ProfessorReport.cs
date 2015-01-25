using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWCFClassLibrary.Professor
{
    public class ProfessorReport : IProfessorReport
    {
        public DataSet getAllQuizByModule(int moduleId)
        {
            DataSet ds = new DataSet();
            using (myProjectEntities context = new myProjectEntities())
            {
                List<tblQuizDetail> qDetail = context.tblQuizDetails.Where(q => q.FK_ModuleId == moduleId).ToList();
                DataTable dt = new DataTable("marks");
                dt.Columns.Add("quiz");
                dt.Columns.Add("marks");
                foreach (tblQuizDetail item in qDetail)
                {
                    tblQuizResult r = context.tblQuizResults.Where(q => q.FK_QuizId == item.PK_Quiz_id).FirstOrDefault();
                    if (r != null)
                    {
                        var totalMarks = context.tblQuizResults.Where(q => q.FK_QuizId == item.PK_Quiz_id).Select(q => q.marks).AsEnumerable();

                        float d = float.Parse(totalMarks.Aggregate((a, b) => a + b).ToString());
                        float s = (float)item.quiz_weightage;
                        float per = d / s * 100;
                        dt.Rows.Add(item.quiz_title, per);

                    }
                }
                ds.Tables.Add(dt);

            }
            return ds;
        }

        public DataSet getAllModuleByTerm(string term)
        {
            DataSet ds = new DataSet();

            using (myProjectEntities context = new myProjectEntities())
            {
                List<tblDepartmentModule> depModules = context.tblDepartmentModules.Where(w => w.term == term).ToList();
                foreach (tblDepartmentModule dep in depModules)
                {
                    List<tblQuizDetail> qDetail = context.tblQuizDetails.Where(q => q.FK_ModuleId == dep.PK_Module_id).ToList();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("quiz");
                    dt.Columns.Add("marks");
                    foreach (tblQuizDetail item in qDetail)
                    {
                        tblQuizResult r = context.tblQuizResults.Where(q => q.FK_QuizId == item.PK_Quiz_id).FirstOrDefault();
                        if (r != null)
                        {
                            var totalMarks = context.tblQuizResults.Where(q => q.FK_QuizId == item.PK_Quiz_id).Select(q => q.marks).AsEnumerable();

                            float d = float.Parse(totalMarks.Aggregate((a, b) => a + b).ToString());
                            float s = (float)item.quiz_weightage;
                            float per = d / s * 100;
                            dt.Rows.Add(item.quiz_title, per);

                        }
                    }
                    if (dt.Rows.Count > 0)
                        ds.Tables.Add(dt);
                }
            }
            return ds;
        }

        // Get class performance on question by quizId
        public DataSet getAllQuestionsByQuiz(int quizId)
        {
            //Declare for return type
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("qId");
            dt.Columns.Add("question");
            dt.Columns.Add("marks");
            
            using (myProjectEntities context = new myProjectEntities())
            {
                List<tblQuizQuestion> allQuestionList = context.tblQuizQuestions.Where(q => q.FK_Quiz_id == quizId).ToList(); //All the qustions of the quiz

                //Loop through each question
                foreach (tblQuizQuestion item in allQuestionList)
                {
                    //marks for that particular question
                    var sd = from p in context.tblQuizResults where p.FK_QuestionId==item.PK_Question_id
                             group p.marks by p.FK_QuestionId into g
                             select new { qID = g.Key, total = g.Sum(i=>i.Value) };

                    float s = 0; //To get the sum of all the student's answer

                    foreach (var gr in sd)
                    {
                        s = (float)gr.total;

                    }
                    //number of students who attend the quiz
                    var c = context.tblQuizResults.Where(q => q.FK_QuizId == item.FK_Quiz_id).Select(g=>g.FK_studentId).Distinct().ToList(); 

                    //Marks of Question
                    float qmark = (float)item.marks;
                        
                    //average marks of the class: dividing sum of marks by total number of students for that quiz
                    float pc = s/c.Count ;

                    //Change average makrs to percentage
                    float per = pc / qmark * 100;

                    dt.Rows.Add(item.PK_Question_id, item.question, per);

                }
                ds.Tables.Add(dt);
            }
            return ds;
        }


        //Get class status i.e. Fail, Pass, Merit , Distinction
        public string[] getStatusByQuestionId(int questionId) {
            using (myProjectEntities context = new myProjectEntities())
            {

                //List of student who attend the question
                var studentList = context.tblQuizResults.Where(q => q.FK_QuestionId == questionId).Select(q=>q.FK_studentId).Distinct().ToList();

                //Question mark
                float questionMark = (float)context.tblQuizQuestions.Where(q=>q.PK_Question_id==questionId).Select(q=>q.marks).FirstOrDefault();

                //Title of the question
                string questionTitle = context.tblQuizQuestions.Where(q => q.PK_Question_id == questionId).Select(q => q.question).FirstOrDefault();

                //Decalare to return the value
                string[] result = new string[6];
                
                int f = 0 ; int p = 0; int m =0 ; int distinction = 0;
                for (int i = 0; i < studentList.Count; i++)
                {
                    int we = studentList[i].Value;
                    var d = context.tblQuizResults.Where(q => q.FK_studentId == we && q.FK_QuestionId==questionId).Select(q=>q.marks).ToList();

                    float mark = float.Parse(d.Sum().ToString());
                    
                    float status = mark/questionMark * 100;


                    if (status < 50) f++;
                    else if (status > 50 && status < 60) p++;
                    else if (status > 60 && status < 70) m++;
                    else if (status > 70) distinction++;

                    
                }

                result[0] = studentList.Count.ToString();
                result[1] = f.ToString(); // Fail 
                result[2] = p.ToString(); //Pass
                result[3] = m.ToString(); // Merit 
                result[4] = distinction.ToString(); //distinction
                result[5] = questionTitle;

                return result;

            }
        }


        //Get student performance on each quiz of the module
        public DataSet getAllQuizbyStudent(int studentId, int moduleId)
        {
            
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            
            dt.Columns.Add("quiz");
            dt.Columns.Add("marks");
            using (myProjectEntities context = new myProjectEntities())
            {
                var allQuiz = context.tblQuizDetails.Where(q => q.FK_ModuleId == moduleId).Select(q=>q.PK_Quiz_id).ToList();

                for (int i = 0; i < allQuiz.Count; i++)
                {
                    int s =Convert.ToInt32(allQuiz[i].ToString());
                    tblQuizDetail q = context.tblQuizDetails.Where(t => t.PK_Quiz_id == s).FirstOrDefault(); //Quiz Detail
                    string title = q.quiz_title;
                    float weightage = (float)q.quiz_weightage;
                            

                    var sd = context.tblQuizResults.Where(w => w.FK_QuizId == s && w.FK_studentId == studentId).Select(w => w.marks).ToList(); //All marks scored by student for that quiz
                    float mark = float.Parse(sd.Sum().ToString()); // Total marks

                    float p = (mark / weightage) * 100 ; //Percantage scored by the student

                    dt.Rows.Add(title,p.ToString());
                   

                }
                ds.Tables.Add(dt);

            }
            return ds ;
        }


        //Get all feedback
        public DataSet getAllFeedbacks(int moduleId)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            dt.Columns.Add("Lecture");
            dt.Columns.Add("Average");
            using (myProjectEntities context = new myProjectEntities())
            {

                var listFeedback = context.tblFeedbacks.Where(q => q.tblLecture.FK_Module_id == moduleId).Select(q => q.PK_Feedback_id).ToList();

                foreach (var feedId in listFeedback)
                
                {
                    var feedAnswer = context.tblFeedbackResults.Where(q => q.FK_Feedback_id == feedId).Select(q=>q.feedback_answer).ToList();
                    
                    var feedAnswer1 = context.tblFeedbackResults.Where(q => q.FK_Feedback_id == feedId).Select(q=>q.tblFeedback.tblLecture.lecture_title).FirstOrDefault();

                    float m = 0;
                    
                    m = float.Parse(feedAnswer.Sum().ToString()); 
                    
                    float d = (float)feedAnswer.Count;

                    float average = m / d;

                    dt.Rows.Add(feedAnswer1,average.ToString());

                 }

                ds.Tables.Add(dt);
                
            }
            
            return ds; 

        }

    }
}
