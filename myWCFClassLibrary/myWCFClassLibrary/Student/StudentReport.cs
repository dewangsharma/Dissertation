using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWCFClassLibrary.Student
{
   public class StudentReport:IStudentReport
    {
        //Performance of the student on all the quiz

       public DataSet getAllQuizByStudentId(int studentId)
       {
           DataSet ds = new DataSet();
           DataTable dt = new DataTable();
           
           dt.Columns.Add("qId");
           dt.Columns.Add("qTitle");
           dt.Columns.Add("qMarks");
           using (myProjectEntities context = new myProjectEntities()) 
           {
               //List of Quiz
               var quizList = context.tblQuizResults.Where(q=>q.FK_studentId==studentId).Select(q=>q.FK_QuizId).Distinct().ToList();

               foreach (var quiz in quizList)
               {
                   int quizId = (int)quiz; //This is quizId

                   //To get the quiz detail
                   tblQuizDetail quizDetail = context.tblQuizDetails.Where(q => q.PK_Quiz_id == quizId).FirstOrDefault();

                   //Sum all the marks for that particular quiz
                   var marks = from p in context.tblQuizResults
                            where p.FK_QuizId == quizId && p.FK_studentId==studentId
                            group p.marks by p.FK_QuizId into g
                            select new { qID = g.Key, total = g.Sum(i => i.Value) };

                   //To get the total marks 
                   float mark = 0;
                   foreach (var i in marks)
                   {
                       mark = (float)i.total;   
                   }

                   float weightage = (float)quizDetail.quiz_weightage; //weightage of the quiz

                   float r3 = mark / weightage * 100; // percentage
                   string r2 = quizDetail.quiz_title; // title

                   dt.Rows.Add(quizId,r2,r3);

               }
               ds.Tables.Add(dt);
               return ds;
           }
       }

       //Performance on all the question of the Quiz by Student.
       //This is in marks.
       public DataSet getAllQuesByQuizId(int studentId, int quizId)
       {
           DataSet ds = new DataSet();
           DataTable dt = new DataTable();
           dt.Columns.Add("qId");
           dt.Columns.Add("qDetail");
           dt.Columns.Add("marks");
          // dt.Columns.Add("total");
           using(myProjectEntities context = new myProjectEntities())
           {
               //List of Questions for that particular quiz
                var quesList = context.tblQuizQuestions.Where(q=>q.FK_Quiz_id==quizId).Select(q=>q.PK_Question_id).ToList();

               //Loop through all the questions
               foreach (var ques in quesList)
               {
                   int quesId = (int)ques;//QUestion_id

                   //Question
                   string question = context.tblQuizQuestions.Where(q => q.PK_Question_id == quesId).Select(q => q.question).FirstOrDefault();

                   //Total marks for that question
                   float w = (float)context.tblQuizQuestions.Where(q => q.PK_Question_id == quesId).Select(q => q.marks).FirstOrDefault();

                   //Get scored marks for that question by Student_id
                   var mark = from p in context.tblQuizResults
                            where p.FK_QuestionId == quesId && p.FK_studentId==studentId
                            group p.marks by p.FK_QuestionId into g
                            select new { qID = g.Key, total = g.Sum(i => i.Value) };

                   float score = 0;//To convert
                   foreach (var m in mark)
                   {
                       score = (float)m.total;
                   }

                   float s = score / w * 100;
                   dt.Rows.Add(quesId,question,s);

               }
               ds.Tables.Add(dt);
           }
           return ds;
 
       }


    }
}
