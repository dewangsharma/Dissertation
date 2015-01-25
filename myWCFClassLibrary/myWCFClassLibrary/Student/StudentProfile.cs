using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AutoMapper;
using System.Net.Mail; //Class library to map obect with Entity Framework


namespace myWCFClassLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IStudentProfile
    {
        public string CreateStudent(StudentProfile stdntProfile)
        {
            string result;
            bool exEmail = ExistingEmail(stdntProfile.email);
            if (exEmail)
            {
                using (myProjectEntities context = new myProjectEntities())
                {
                    string vCode = Guid.NewGuid().ToString(); // create verification code
                    stdntProfile.verification_code = vCode;
                    stdntProfile.profile_pic = "~/Images/profile.png";
                    stdntProfile.status = 0;


                    Mapper.CreateMap<StudentProfile, tblStudent>();
                    tblStudent newStudent = Mapper.Map<StudentProfile, tblStudent>(stdntProfile);
                    context.tblStudents.Add(newStudent);
                    int q = context.SaveChanges();
                    result = q.ToString();

                    bool mail = SendEmail(stdntProfile.firstname + " " +stdntProfile.middlename + " " + stdntProfile.lastname,stdntProfile.email, "Account Verification", stdntProfile.verification_code);

                }
            }
            else
                result = "Existing Email";
            return result;
        }
        //Check Existing Email
        public bool ExistingEmail(string Email)
        {
            bool result = true;
            using (myProjectEntities context = new myProjectEntities())
            {
                var s = context.tblStudents.Where(d => d.email == Email).FirstOrDefault();
                var p = context.tblProfessors.Where(d => d.email == Email).FirstOrDefault();
                if (s != null || p != null)
                    result = false;
            }
            return result;
        }

        //Send Mail
        public bool SendEmail(string user, string toUsername, string subject, string message)
        {
            string heading = "<!DOCTYPE html><html lang='en' xmlns='http://www.w3.org/1999/xhtml'><head><meta charset='utf-8' /> <title></title></head><body><br />Hello <b>" + user + " ,</b><br /><br />Welcome to the new Result Analysis System.<br /><br />Verification Code :  <b>" + message + "</b>,<br /><br />Please enter this verification on your login screen.<br /><br />Thanks<br />Result Analysis System</body></html>";


            MailMessage mm = new MailMessage();

            SmtpClient smtp = new SmtpClient();
            mm.From = new MailAddress("sp150988@gmail.com");
            mm.Subject = subject;
            mm.To.Add(new MailAddress(toUsername));

            mm.Body = heading;
            mm.IsBodyHtml = true;
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            System.Net.NetworkCredential nwCredential = new System.Net.NetworkCredential();
            nwCredential.UserName = "sp150988@gmail.com";
            nwCredential.Password = "ud_99998";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nwCredential;
            smtp.Send(mm);

            return true;

        }


    }
}
