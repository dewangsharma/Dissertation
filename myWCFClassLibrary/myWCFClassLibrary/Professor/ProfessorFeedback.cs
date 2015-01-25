using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data.Entity;

namespace myWCFClassLibrary.Professor
{
    public class ProfessorFeedback:IProfessorFeedback
    {
        //Create new Feedback 
        public int createFeedback(feedbackModel feedData)
        {
            int result = 0;
            using (myProjectEntities context = new myProjectEntities())
            {
                Mapper.CreateMap<feedbackModel, tblFeedback>();

                tblFeedback newFeed = Mapper.Map<feedbackModel, tblFeedback>(feedData);

                context.tblFeedbacks.Add(newFeed);
                int q = context.SaveChanges();
                if (q > 0) result = newFeed.PK_Feedback_id;
                return result;
                
            }
        }

        //Get FeedBackList by Professor Id
        public IEnumerable<feedbackModel> GetFeedbackByProfessor(int ProfId)
        {
            using (myProjectEntities context = new myProjectEntities())
            {
                List<tblFeedback> pro = new List<tblFeedback>();
                IEnumerable<tblLecture> lectures = context.tblLectures.Where(qq => qq.FK_Professor_id == ProfId).AsEnumerable();
                foreach (tblLecture item in lectures)
                {
                    int lectureId = item.PK_Lecture_id;
                    tblFeedback feed = context.tblFeedbacks.Where(q => q.FK_Lecture_id == lectureId).FirstOrDefault();
                    if (feed != null)
                        pro.Add(feed);
                }
                
                Mapper.CreateMap<tblFeedback, feedbackModel>();
                IEnumerable<feedbackModel> result = Mapper.Map<IEnumerable<tblFeedback>, IEnumerable<feedbackModel>>(pro);
                return result;
            }

        }
        public feedbackModel GetFeedbackByLectureId(int lectureId)
        {
            using (myProjectEntities context = new myProjectEntities())
            {
                tblFeedback feed = context.tblFeedbacks.Where(q => q.FK_Lecture_id == lectureId).FirstOrDefault();
                Mapper.CreateMap<tblFeedback, feedbackModel>();
                feedbackModel result = Mapper.Map<tblFeedback, feedbackModel>(feed);
                return result;
            }
        }




        // Create Lectures
        public int createLecture(lectureModel lectureData, bool feed, DateTime endDate)
        {
            int result = 0;
            using (myProjectEntities context = new myProjectEntities())
            {
                lectureData.lecture_status = 1;

                Mapper.CreateMap<lectureModel, tblLecture>();
                tblLecture newLec = Mapper.Map<lectureModel, tblLecture>(lectureData);

                context.tblLectures.Add(newLec);
                int qq = context.SaveChanges();
                result = qq;

                if (feed)
                {
                    feedbackModel feedModel = new feedbackModel();
                    feedModel.FK_Lecture_id = newLec.PK_Lecture_id;
                    feedModel.end_date = endDate;
                    qq= createFeedback(feedModel);
                }
                return result;
                
            }
 
        }

        //Get Lecture Data 
        public IEnumerable<lectureModel> GetLecturesByProfessor(int ProfId, int isFeed)
        {
            using (myProjectEntities context = new myProjectEntities())
            {
                List<tblLecture> lectures = new List<tblLecture>();
                IEnumerable<tblLecture> pro = context.tblLectures.Where(q => q.FK_Professor_id == ProfId && q.lecture_status == 1).AsEnumerable();

                if (isFeed==1)  // If feedback want 
                {
                    foreach (tblLecture item in pro)
                    {
                        int lectureId = item.PK_Lecture_id;
                        tblFeedback feed = context.tblFeedbacks.Where(q => q.FK_Lecture_id == lectureId).FirstOrDefault(); // If there is feedback of the lecture
                        if (feed != null)
                            lectures.Add(item);

                    }
                }
                else if(isFeed == 0)// if not 
                {
                    foreach (tblLecture item in pro)
                    {
                        int lectureId = item.PK_Lecture_id;
                        tblFeedback feed = context.tblFeedbacks.Where(q => q.FK_Lecture_id == lectureId).FirstOrDefault();
                        if (feed == null)
                            lectures.Add(item);

                    }
                }
                else if (isFeed == 2) //All lectures
                {
                    lectures = pro.ToList();
                }

                //IEnumerable<tblLecture> pro = context.tblLectures.Where(q => q.FK_Professor_id == ProfId && q.lecture_status == 1).AsEnumerable();
                 Mapper.CreateMap<tblLecture, lectureModel>();
                 IEnumerable<lectureModel> result = Mapper.Map<IEnumerable<tblLecture>, IEnumerable<lectureModel>>(lectures);
                return result;
            }
 
        }

        //Update Lecture data
        public bool updateLecture(lectureModel lectureData)
        {
            bool result = false;
            using (myProjectEntities context = new myProjectEntities())
            {
                Mapper.CreateMap<lectureModel, tblLecture>();
                tblLecture s = Mapper.Map<lectureModel, tblLecture>(lectureData);
                context.Entry(s).State = EntityState.Modified;
                int q = context.SaveChanges();
                if (q == 1)
                    result = true;
                return result;
                
            }
        }

    }
}
