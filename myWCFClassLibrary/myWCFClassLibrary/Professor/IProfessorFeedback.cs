using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace myWCFClassLibrary.Professor
{
    [ServiceContract]
    public interface IProfessorFeedback
    {
        [OperationContract]
        int createFeedback(feedbackModel feedData);

        [OperationContract]
        IEnumerable<feedbackModel> GetFeedbackByProfessor(int ProfId);

        [OperationContract]
        int createLecture(lectureModel lectureData, bool feed, DateTime endDate);

        //Get Lecture Data
        [OperationContract]
        IEnumerable<lectureModel> GetLecturesByProfessor(int ProfId, int isFeed);

        //Update Lecture data
        [OperationContract]
        bool updateLecture(lectureModel lectureData);

      

    }
    [DataContract]
    public class feedbackModel
    {
        [DataMember]
        public int PK_Feedback_id { get; set; }
        [DataMember]
        public int FK_Lecture_id { get; set; }
        [DataMember]
        public float feedback_value { get; set; }
        [DataMember]
        public System.DateTime end_date { get; set; }
       
    
 
    }

    [DataContract]
    public class lectureModel
    {
        [DataMember]
        public int PK_Lecture_id { get; set; }
        [DataMember]
        public int FK_Module_id { get; set; }
        [DataMember]
        public int FK_Professor_id { get; set; }
        [DataMember]
        public string lecture_title { get; set; }
        [DataMember]
        public System.DateTime lecture_date { get; set; }
        [DataMember]
        public System.TimeSpan lecture_time { get; set; }
        [DataMember]
        public int lecture_status { get; set; }

    }
}
