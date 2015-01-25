using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace myWCFClassLibrary.Student
{
    [ServiceContract]
    public interface IStudentReport
    {

        //Performance of the student on all the quiz.
        //Marks reply in percentage
        [OperationContract]
        DataSet getAllQuizByStudentId(int studentId);

        //Performance on all the question of the Quiz by Student.
        //This is in percentage.
        [OperationContract]
        DataSet getAllQuesByQuizId(int studentId, int quizId);

    }
}
