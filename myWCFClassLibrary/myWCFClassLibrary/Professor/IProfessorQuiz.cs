using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace myWCFClassLibrary.Professor
{
    [ServiceContract]
    public interface IProfessorQuiz
    {
        //Create New Quiz
        [OperationContract]
        int CreateQuiz(quizModel quizData);

        //Update existing quiz
        [OperationContract]
        bool UpdateQuiz(quizModel quizData);

        //Get Individual Quiz detail 
        [OperationContract]
        quizModel GetQuizById(int quizId);

        //Collect all the quiz of the professor
        [OperationContract]
        IEnumerable<quizModel> GetAllQuizByProf(int profId, int status); //status for Live coming quiz

        //Collect all quiz by moduleId
        [OperationContract]
        IEnumerable<quizModel> GetAllQuizByModule(int moduleId, int profId);

        //Create new question
        [OperationContract]
        int CreateQuestion(questionModel quesData);

        //Question Create
        [OperationContract]
        int Question_Create(questionModel quesData);

        //Update existing Questions
        [OperationContract]
        bool UpdateQuestion(questionModel quesData);

        //Delete Question with all option
        [OperationContract]
        bool DeleteQuestion(int quesData);

        //Get individual question detail
        [OperationContract]
        questionModel GetQuestionById(int quesId);

        //Get all the questions of the quiz
        [OperationContract]
        IEnumerable<questionModel> GetAllQuestionByQuiz(int quizId);


        //Create new Option
        [OperationContract]
        bool CreateOption(optionModel optionData);

        //Update Option
        [OperationContract]
        bool UpdateOption(optionModel optionData);

        //Get individual options of the question
        [OperationContract]
        optionModel GetOptionById(int optId);
        
        //Get all the options of the question
        [OperationContract]
        IEnumerable<optionModel> GetAllOptionByQues(int quesId);

        [OperationContract]
        bool DeleteOptionByOptionId(int[] optionId);

    }
    [DataContract]
    public class quizModel
    {
        [DataMember]
        public int PK_Quiz_id { get; set; }
        
        [DataMember]
        public int FK_Professor_id { get; set; }
        
        [DataMember]
        public string quiz_title { get; set; }
        
        [DataMember]
        public System.DateTime quiz_date { get; set; }
        
        [DataMember]
        public int isTimeup { get; set; }
        
        [DataMember]
        public float quiz_length { get; set; }
        
        [DataMember]
        public float quiz_weightage { get; set; }
        
        [DataMember]
        public int quiz_status { get; set; }
        
        [DataMember]
        public System.DateTime quiz_end_date { get; set; }
        
        [DataMember]
        public System.DateTime quiz_update { get; set; }
       
        [DataMember]
        public int FK_ModuleId { get; set; }
        [DataMember]
        public virtual ICollection<questionModel> questionModel { get; set; }
   

    }

    [DataContract]
    public class questionModel
    {
        [DataMember]
        public int PK_Question_id { get; set; }
        [DataMember]
        public int FK_Quiz_id { get; set; }
        [DataMember]
        public string question { get; set; }
        [DataMember]
        public string question_type { get; set; }
        [DataMember]
        public virtual quizModel quizModel { get; set; }
        [DataMember]
        public string hint { get; set; }
        [DataMember]
        public double marks { get; set; }
    
        [DataMember]
        public virtual ICollection<optionModel> optionModel { get; set; }
    }

    [DataContract]
    public class optionModel
    {
        [DataMember]
        public int PK_Option_id { get; set; }
        [DataMember]
        public int FK_Question_id { get; set; }
        [DataMember]
        public string option_value { get; set; }
        [DataMember]
        public int isAnswer { get; set; }

    }
}
