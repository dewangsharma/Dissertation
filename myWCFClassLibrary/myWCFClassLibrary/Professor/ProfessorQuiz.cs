using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data.Entity;

namespace myWCFClassLibrary.Professor
{
    public class ProfessorQuiz : IProfessorQuiz
    {
        //Create Quiz
        public int CreateQuiz(quizModel quizData)
        {
            int result=0;
            using (myProjectEntities context = new myProjectEntities())
            {
                quizData.quiz_update = System.DateTime.Now; // Last Update or Created Date Time 
                quizData.quiz_status = 1; // Live coming Quiz 

            Mapper.CreateMap<quizModel, tblQuizDetail>();
            tblQuizDetail newQuiz = Mapper.Map<quizModel, tblQuizDetail>(quizData);
            

            context.tblQuizDetails.Add(newQuiz); //Execute the add function
            int q = context.SaveChanges();
            if (q == 1) result = newQuiz.PK_Quiz_id; //set return to true
            
            }
            return result;
        }

        //Update existing Quiz + Change the status to delete the quiz
        public bool UpdateQuiz(quizModel quizData)
        {
            bool result = false;
            using (myProjectEntities context = new myProjectEntities())
            {
                Mapper.CreateMap<quizModel, tblQuizDetail>();
                tblQuizDetail s = Mapper.Map<quizModel, tblQuizDetail>(quizData);
                context.Entry(s).State = EntityState.Modified;
                int q = context.SaveChanges();
                if(q==1)
                    result =true;
                return result;
                

            }
        }

        //Get Individual Quiz detail 
        public quizModel GetQuizById(int quizId)
        {
            using (myProjectEntities context = new myProjectEntities())
            {
                tblQuizDetail pro = context.tblQuizDetails.Where(q => q.PK_Quiz_id == quizId ).FirstOrDefault();
                Mapper.CreateMap<tblQuizDetail, quizModel>();
                quizModel result = Mapper.Map<tblQuizDetail, quizModel>(pro);
                return result;
            }

        }

        //Collect all the quiz of the professor
        public IEnumerable<quizModel> GetAllQuizByProf(int ProfId,int status) 
        {
            using (myProjectEntities context = new myProjectEntities())
            {
                IEnumerable<tblQuizDetail> pro = context.tblQuizDetails.Where(q => q.FK_Professor_id == ProfId && q.quiz_status == status).AsEnumerable();
                Mapper.CreateMap<tblQuizDetail, quizModel>();
                IEnumerable<quizModel> result = Mapper.Map<IEnumerable<tblQuizDetail>, IEnumerable<quizModel>>(pro);
                return result;
            }
 
        }

        //Collect all quiz by module id
        public IEnumerable<quizModel> GetAllQuizByModule(int moduleId, int ProfId)
        {
            using (myProjectEntities context = new myProjectEntities())
            {
                IEnumerable<tblQuizDetail> pro = context.tblQuizDetails.Where(q => q.FK_Professor_id == ProfId && q.FK_ModuleId == moduleId && System.DateTime.Now<=q.quiz_end_date ).AsEnumerable();
                Mapper.CreateMap<tblQuizDetail, quizModel>();
                IEnumerable<quizModel> result = Mapper.Map<IEnumerable<tblQuizDetail>, IEnumerable<quizModel>>(pro);
                return result;
            }
        }

        //Create Questions for quiz. This is awesome
        public int CreateQuestion(questionModel quesData)
        {
            int result = 0;
            using (myProjectEntities context = new myProjectEntities())
            {
                Mapper.CreateMap<questionModel, tblQuizQuestion>();
                tblQuizQuestion newQues = Mapper.Map<questionModel, tblQuizQuestion>(quesData);

                List<tblQuizOption> newOpt = new List<tblQuizOption>();

                Mapper.CreateMap<optionModel, tblQuizOption>();
                foreach (optionModel opt in quesData.optionModel)
                {
                    tblQuizOption o = Mapper.Map<optionModel, tblQuizOption>(opt);

                    newOpt.Add(o);
                }

                newQues.tblQuizOptions = newOpt.ToList();
                context.tblQuizQuestions.Add(newQues); //Execute the add function
                int q = context.SaveChanges();
                if (q > 0) result = newQues.PK_Question_id; //set return to true
            }
            return result;
        }

        //Updated
        public int Question_Create(questionModel quesData)
        {
            int result = 0;
            using (myProjectEntities context = new myProjectEntities())
            {
                Mapper.CreateMap<questionModel, tblQuizQuestion>();
                tblQuizQuestion newQues = Mapper.Map<questionModel, tblQuizQuestion>(quesData);

                if (quesData.question_type == "Optional" || quesData.question_type == "Multiple Choice")//Check is not options
                {
                List<tblQuizOption> newOpt = new List<tblQuizOption>();

                Mapper.CreateMap<optionModel, tblQuizOption>();


                foreach (optionModel opt in quesData.optionModel)
                {
                    tblQuizOption o = Mapper.Map<optionModel, tblQuizOption>(opt);

                    newOpt.Add(o);
                }

                newQues.tblQuizOptions = newOpt.ToList();

               }
                context.tblQuizQuestions.Add(newQues); //Execute the add function
                int q = context.SaveChanges();
                if (q > 0) result = newQues.PK_Question_id; //set return to true

                tblQuizDetail quizDetail = context.tblQuizDetails.Where(a => a.PK_Quiz_id == newQues.FK_Quiz_id).FirstOrDefault();
                quizDetail.quiz_weightage = quizDetail.quiz_weightage + (float)newQues.marks;
                context.Entry(quizDetail).State = EntityState.Modified;
                context.SaveChanges();
            }
            return result;
        }

        //Update existing Questions
        public bool UpdateQuestion(questionModel quesData)
        {

             bool result = false;
             using (myProjectEntities context = new myProjectEntities()) //update weightage
             {
                 tblQuizQuestion quesDetail = context.tblQuizQuestions.Where(w => w.PK_Question_id == quesData.PK_Question_id).FirstOrDefault();
                 float m1 = (float)quesDetail.marks;

                 tblQuizDetail quizDetail = context.tblQuizDetails.Where(a => a.PK_Quiz_id == quesData.FK_Quiz_id).FirstOrDefault();
                 float m2 = (float)quizDetail.quiz_weightage - m1;

                 quizDetail.quiz_weightage = m2 + (float)quesData.marks;
                 context.Entry(quizDetail).State = EntityState.Modified;
                 context.SaveChanges();

             }
            using (myProjectEntities context = new myProjectEntities())
            {
                
                Mapper.CreateMap<questionModel, tblQuizQuestion>();
                tblQuizQuestion newQues = Mapper.Map<questionModel, tblQuizQuestion>(quesData);
                context.Entry(newQues).State = EntityState.Modified;
                context.SaveChanges();

                
            }
            if (quesData.question_type == "Optional" || quesData.question_type == "Multiple Choice")
            {
                using (myProjectEntities context = new myProjectEntities())
                {
                    List<tblQuizOption> newOpt = context.tblQuizOptions.Where(s => s.FK_Question_id == quesData.PK_Question_id).ToList();

                    foreach (optionModel item in quesData.optionModel)
                    {
                        foreach (tblQuizOption item1 in newOpt)
                        {
                            if (item.PK_Option_id == item1.PK_Option_id)
                            {
                                item1.isAnswer = item.isAnswer;
                            }
                        }
                    }

                    context.SaveChanges();

                }
            }
            
            result = true;
            return result;
                
                              
                //This is to update just the Question i.e. without option model
                //Mapper.CreateMap<questionModel, tblQuizQuestion>();
                //tblQuizQuestion s = Mapper.Map<questionModel, tblQuizQuestion>(quesData);
                //context.Entry(s).State = EntityState.Modified;
                //int q = context.SaveChanges();
                //if (q == 1)
                //    result = true;
                //return result;

            
        }


        //Delete Question with all Options 
        public bool DeleteQuestion(int quesData)
        {
            bool result = false;
            using (myProjectEntities context = new myProjectEntities())
            {
                tblQuizQuestion quesDetail = context.tblQuizQuestions.Where(w => w.PK_Question_id == quesData).FirstOrDefault();
                float m1 = (float)quesDetail.marks;

                tblQuizDetail quizDetail = context.tblQuizDetails.Where(a => a.PK_Quiz_id == quesDetail.FK_Quiz_id).FirstOrDefault();
                float m2 = (float)quizDetail.quiz_weightage - m1;

                quizDetail.quiz_weightage = m2;
                context.Entry(quizDetail).State = EntityState.Modified;
                context.SaveChanges();

               int q = context.spDeleteQuestionWithOption(quesData);
                
                if (q != 0) result = true; //set return to true

            }
            return result;
        }

        //Get individual question detail. This is awesome.
        public questionModel GetQuestionById(int quesId)
        {
            using (myProjectEntities context = new myProjectEntities())
            {
                tblQuizQuestion pro = context.tblQuizQuestions.Where(q => q.PK_Question_id == quesId).FirstOrDefault();
                List<optionModel> opt = new List<optionModel>();
                Mapper.CreateMap<tblQuizOption, optionModel>();
                foreach (tblQuizOption item in pro.tblQuizOptions)
                {
                    optionModel rtr = Mapper.Map<tblQuizOption, optionModel>(item);
                    opt.Add(rtr);
                }

                Mapper.CreateMap<tblQuizQuestion, questionModel>();
                questionModel result = Mapper.Map<tblQuizQuestion, questionModel>(pro);
                result.optionModel = opt.ToList();
                return result;
            }
        }

        //Get all the questions of the quiz
        public IEnumerable<questionModel> GetAllQuestionByQuiz(int quizId)
        {
            using (myProjectEntities context = new myProjectEntities())
            {
                IEnumerable<tblQuizQuestion> pro = context.tblQuizQuestions.Where(q => q.FK_Quiz_id == quizId).AsEnumerable();
               
                //Question Mapper
                Mapper.CreateMap<tblQuizQuestion, questionModel>();
                List<questionModel> qModel = new List<questionModel>();
                //Option Mapper
                
                Mapper.CreateMap<tblQuizOption, optionModel>();
                foreach (tblQuizQuestion qu in pro)
                {
                    List<optionModel> opt = new List<optionModel>();
                    foreach (tblQuizOption item in qu.tblQuizOptions)
                    {
                        optionModel rtr = Mapper.Map<tblQuizOption, optionModel>(item);
                        opt.Add(rtr);

                    }
                    Mapper.CreateMap<tblQuizQuestion, questionModel>();
                    questionModel result = Mapper.Map<tblQuizQuestion, questionModel>(qu);
                    result.optionModel = opt.ToList();
                    qModel.Add(result);

                }

                var opop = qModel.AsEnumerable();
                 Mapper.CreateMap<tblQuizQuestion, questionModel>();
                IEnumerable<questionModel> result1 = Mapper.Map<IEnumerable<tblQuizQuestion>, IEnumerable<questionModel>>(pro);
                result1 = qModel.AsEnumerable();
                return result1;
                
                //This is without lazy loadin i.e. without option model
                ////IEnumerable<tblQuizQuestion> pro = context.tblQuizQuestions.Where(q => q.FK_Quiz_id == quizId).AsEnumerable();
                ////Mapper.CreateMap<tblQuizQuestion, questionModel>();
                ////IEnumerable<questionModel> result = Mapper.Map<IEnumerable<tblQuizQuestion>, IEnumerable<questionModel>>(pro);
                ////return result;
            }
        }
        
         
        //Create Options
        public bool CreateOption(optionModel optionData)
        {
            bool result = false;
            using (myProjectEntities context = new myProjectEntities())
            {
                Mapper.CreateMap<optionModel, tblQuizOption>();
                tblQuizOption newOpt = Mapper.Map<optionModel, tblQuizOption>(optionData);
                context.tblQuizOptions.Add(newOpt); //Execute the add function
                int q = context.SaveChanges();
                if (q == 1) result = true; //set return to true
            }
            return result;

        }

        //Update Options
        public bool UpdateOption(optionModel optionData)
        {
            bool result = false;
            using (myProjectEntities context = new myProjectEntities())
            {
                Mapper.CreateMap<optionModel, tblQuizOption>();
                tblQuizOption s = Mapper.Map<optionModel, tblQuizOption>(optionData);
                context.Entry(s).State = EntityState.Modified;
                int q = context.SaveChanges();
                if (q == 1)
                    result = true;
                return result;


            }
        }

        //Get individual options of the question
        public optionModel GetOptionById(int optId)
        {
            using (myProjectEntities context = new myProjectEntities())
            {
                tblQuizOption pro = context.tblQuizOptions.Where(q => q.PK_Option_id == optId).FirstOrDefault();
                Mapper.CreateMap<tblQuizOption, optionModel>();
                optionModel result = Mapper.Map<tblQuizOption, optionModel>(pro);
                return result;
            }
        
        }

        //Get all the options by question
        public IEnumerable<optionModel> GetAllOptionByQues(int quesId)
        {
            using (myProjectEntities context = new myProjectEntities())
            {
                IEnumerable<tblQuizOption> pro = context.tblQuizOptions.Where(q => q.FK_Question_id == quesId).AsEnumerable();
                Mapper.CreateMap<tblQuizOption, optionModel>();
                IEnumerable<optionModel> result = Mapper.Map<IEnumerable<tblQuizOption>, IEnumerable<optionModel>>(pro);
                return result;
            }
        }

        //Delete Option
        public bool DeleteOptionByOptionId(int[] optionId)
        {
            bool result = false;int q =0;
            using (myProjectEntities context = new myProjectEntities())
            {
                for(int i = 0 ; i < optionId.Length ; i++ ){
               q = context.spDeleteOption(optionId[i]);
            }
                if (q != 0) result = true; //set return to true

            }

            return result;
        }
        
    }
}
