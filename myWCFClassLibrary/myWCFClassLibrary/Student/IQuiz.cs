using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using myWCFClassLibrary.Professor; // To get already created datacontract

namespace myWCFClassLibrary.Student
{
    [ServiceContract]
    public interface IQuiz
    {
        //Save Induvidual question of the quiz
        [OperationContract]
        bool startQuiz(QuizResultModel quizRData);

        //Save complete quiz in one go
        [OperationContract]
        bool quizAnswer(List<QuizResultModel> quizRData);


        //try save quiz
        [OperationContract]
        bool quizAnswerSave(QuizResultModel quizRData);

        //Get all the Quiz By Module 
        [OperationContract]
        IEnumerable<quizModel> GetAllQuizByModule(int moduleId);

        //Check student has attended quiz or not 
        [OperationContract]
        bool GetStdudentAttempt(int studentId, int quizId);

        //Enter Feedback 
        [OperationContract]
        bool createFeedbackAnswer(FeedbackAnswer feedModel);

        [OperationContract]
        bool createFeedAnswer(int studentId, int lectureId, float answer);

        //All feedback of the module 
        [OperationContract]
        IEnumerable<lectureModel> GetAllFeedbackByModule(int moduleId, int studentId);


       
    }
    [DataContract]
    public class QuizResultModel
    {
        [DataMember]
        public int PK_QuestionResult_Id { get; set; }
        [DataMember]
        public int FK_QuizId { get; set; }
        [DataMember]
        public int FK_QuestionId { get; set; }
        [DataMember]
        public int FK_OptionId { get; set; }
        [DataMember]
        public int FK_studentId { get; set; }
        [DataMember]
        public double marks { get; set; }
        [DataMember]
        public int isAnswered { get; set; }
        [DataMember]
        public Nullable<System.DateTime> update { get; set; }
        [DataMember]
        public string answer { get; set; }
   
        [DataMember]
        public virtual tblQuizDetail tblQuizDetail { get; set; }
        [DataMember]
        public virtual tblQuizOption tblQuizOption { get; set; }
        [DataMember]
        public virtual tblQuizQuestion tblQuizQuestion { get; set; }


    
    }
    [DataContract]
    public class FeedbackAnswer
    {
        [DataMember]
        public int PK_Feedback_result_id { get; set; }
        [DataMember]
        public int FK_Feedback_id { get; set; }
        [DataMember]
        public int FK_Student_id { get; set; }
        [DataMember]
        public float feedback_answer { get; set; }
        [DataMember]
        public System.DateTime feedback_date { get; set; }
        [DataMember]
        public virtual tblFeedback tblFeedback { get; set; }
        [DataMember]
        public virtual tblStudent tblStudent { get; set; }

    }

}
