using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace myWCFClassLibrary.Professor
{
    [ServiceContract]
    public interface IProfessorReport
    {
        //All quuiz by moduleid
        [OperationContract]
        DataSet getAllQuizByModule(int moduleId);

        //Al module by term 
        [OperationContract]
        DataSet getAllModuleByTerm(string term);

        // Get class performance on question by quizId
        [OperationContract]
        DataSet getAllQuestionsByQuiz(int quizId);

        //Get class status i.e. Distinction Merit, Pass Fail 
        [OperationContract]
        string[] getStatusByQuestionId(int questionId);

        //Get student performance on each quiz of the module
        [OperationContract]
        DataSet getAllQuizbyStudent(int studentId, int moduleId);

        //Get Feedback result 
        [OperationContract]
        DataSet getAllFeedbacks(int moduleId);


    }
}
