using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data.Entity;
using myWCFClassLibrary.Professor;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;

namespace myWCFClassLibrary.Student
{
    public class Quiz : IQuiz
    {
        //Save individual question of the quiz
        public bool startQuiz(QuizResultModel quizRData)
        {
            bool result = false;
            using (myProjectEntities context = new myProjectEntities())
            {
                Mapper.CreateMap<QuizResultModel, tblQuizResult>();

                tblQuizResult newQuizR = Mapper.Map<QuizResultModel, tblQuizResult>(quizRData);

                context.tblQuizResults.Add(newQuizR);
                //context.Entry(newQuizR).State = EntityState.Modified;
                int q = context.SaveChanges();
                if (q > 0) result = true;
            }
            return result;
        }

        //To save complete quiz in one go 
        public bool quizAnswer(List<QuizResultModel> quizRData)
        {
            bool result = false;
            Stopwatch st = new Stopwatch();
            using (myProjectEntities context = new myProjectEntities())
            {
                for (int i = 0; i < quizRData.Count; i++)
                {

                    Mapper.CreateMap<QuizResultModel, tblQuizResult>();

                    tblQuizResult newQuizR = Mapper.Map<QuizResultModel, tblQuizResult>(quizRData[i]);

                    context.tblQuizResults.Add(newQuizR);
                    

                }
                int q = context.SaveChanges();
                

                //Mapper.CreateMap<QuizResultModel, tblQuizResult>();
                //List<tblQuizResult> newQuizR = Mapper.Map<List<QuizResultModel>, List<tblQuizResult>>(quizRData);
                //st.Restart();
                //context.tblQuizResults.AddRange(newQuizR);
                //st.Stop();
                //st.Restart();
                //context.SaveChanges();
                //st.Stop();
                                
            }


            return result;
        }



        //try enter 
        public bool quizAnswerSave(QuizResultModel quizRData)
        {
 bool result = false;
            Stopwatch st = new Stopwatch();
            using (myProjectEntities context = new myProjectEntities())
            {
                
                    Mapper.CreateMap<QuizResultModel, tblQuizResult>();

                    tblQuizResult newQuizR = Mapper.Map<QuizResultModel, tblQuizResult>(quizRData);

                    context.tblQuizResults.Add(newQuizR);
                    int q = context.SaveChanges();

                
            }
            return result;
        }




        public IEnumerable<quizModel> GetAllQuizByModule(int moduleId)
        {
            using (myProjectEntities context = new myProjectEntities())
            {
                IEnumerable<tblQuizDetail> pro = context.tblQuizDetails.Where(q => q.FK_ModuleId == moduleId && q.quiz_date <= System.DateTime.Now && q.quiz_end_date > System.DateTime.Now && q.quiz_status == 1).AsEnumerable();
                Mapper.CreateMap<tblQuizDetail, quizModel>();
                IEnumerable<quizModel> result = Mapper.Map<IEnumerable<tblQuizDetail>, IEnumerable<quizModel>>(pro);
                return result;
            }

        }

        public bool GetStdudentAttempt(int studentId, int quizId)
        {
            bool result = false;
            using (myProjectEntities context = new myProjectEntities())
            {
                var s = context.tblQuizResults.Where(q => q.FK_studentId == studentId && q.FK_QuizId == quizId).FirstOrDefault();
                if (s == null) result = true;

            }
            return result;
        }

        //Enter Feedback
        public bool createFeedbackAnswer(FeedbackAnswer feedModel)
        {
            bool result = false;
            float fValue = 0;
            using (myProjectEntities context = new myProjectEntities())
            {
                Mapper.CreateMap<FeedbackAnswer, tblFeedbackResult>();

                tblFeedbackResult newFeed = Mapper.Map<FeedbackAnswer, tblFeedbackResult>(feedModel);

                context.tblFeedbackResults.Add(newFeed);
                             
                context.SaveChanges();
                
                var valueFeed = context.tblFeedbackResults.Where(q => q.FK_Feedback_id == feedModel.FK_Feedback_id).Select(q => q.feedback_answer).AsEnumerable();
                float d =float.Parse(valueFeed.Aggregate((a,b)=>a+b).ToString());
                int count = valueFeed.Count();
                fValue = d / count;

                tblFeedback f = context.tblFeedbacks.Where(q => q.PK_Feedback_id == feedModel.FK_Feedback_id).FirstOrDefault();
                f.feedback_value = fValue;
                context.Entry(f).State = EntityState.Modified;
                context.SaveChanges();

            }


            return result;
        }

        public bool createFeedAnswer(int studentId, int lectureId, float answer)
        {
             bool result = false;
            float fValue = 0;
            using (myProjectEntities context = new myProjectEntities())
            {
                int q3 = 0;
                var feedId =context.tblFeedbacks.Where(q => q.FK_Lecture_id == lectureId).FirstOrDefault();
                FeedbackAnswer feedModel = new FeedbackAnswer();
                feedModel.feedback_answer = answer;
                feedModel.feedback_date = System.DateTime.Now;
                feedModel.FK_Feedback_id = Convert.ToInt32(feedId.PK_Feedback_id);
                feedModel.FK_Student_id = studentId;


                Mapper.CreateMap<FeedbackAnswer, tblFeedbackResult>();

                tblFeedbackResult newFeed = Mapper.Map<FeedbackAnswer, tblFeedbackResult>(feedModel);

                context.tblFeedbackResults.Add(newFeed);

                q3=context.SaveChanges();
                //Now update feedback value in tblFeedback
                //This is because after every submission there is change in the overall feedback value 
                if (q3 != 0)
                {
                    var valueFeed = context.tblFeedbackResults.Where(q => q.FK_Feedback_id == feedModel.FK_Feedback_id).Select(q =>q.feedback_answer).AsEnumerable();
                    float d = float.Parse(valueFeed.Aggregate((a, b) => a + b).ToString());
                    int count = valueFeed.Count();
                    fValue = d / count;

                    tblFeedback f = context.tblFeedbacks.Where(q => q.PK_Feedback_id == feedModel.FK_Feedback_id).FirstOrDefault();
                    f.feedback_value = fValue;
                    context.Entry(f).State = EntityState.Modified;
                    q3=context.SaveChanges();
                    if (q3 != 0) result = true;
                }
                
            }
            return result;
        }

        //Get all feedback lectures of by moduleid
        public IEnumerable<lectureModel> GetAllFeedbackByModule(int moduleId, int studentId)
        {
            using (myProjectEntities context = new myProjectEntities())
            {


                List<tblLecture> allLectures = context.tblLectures.Where(q => q.FK_Module_id == moduleId && q.lecture_status == 1 && q.lecture_date <= System.DateTime.Now).ToList();
                    List<tblFeedback> feedModel = new List<tblFeedback>();
                    List<tblLecture> lectModel = new List<tblLecture>();
                  
                    foreach (tblLecture item in allLectures)
                    {

                        tblFeedback d = context.tblFeedbacks.Where(w => w.FK_Lecture_id == item.PK_Lecture_id &&  System.DateTime.Now <= w.end_date).FirstOrDefault();
                        ////////Look here 
                       if (d != null)
                        {
                            var checkStudent = context.tblFeedbackResults.Where(r => r.FK_Student_id == studentId && r.FK_Feedback_id == d.PK_Feedback_id).FirstOrDefault();
                            if (checkStudent == null)
                            {

                                //feedModel.Add(d);
                                lectModel.Add(item);
                            }
                        }
                    }

                    Mapper.CreateMap<tblLecture, lectureModel>();
                    IEnumerable<lectureModel> result = Mapper.Map<IEnumerable<tblLecture>, IEnumerable<lectureModel>>(lectModel);
                    return result;                
            }
            
        }


    }
}
