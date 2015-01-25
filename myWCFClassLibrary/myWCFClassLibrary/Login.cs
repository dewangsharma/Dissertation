using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data.SqlClient;

namespace myWCFClassLibrary
{
   
    
    public class Login:ILogin
    {
        
        public string[] userLogin(string username, string password)
        {
            string[] result = new string[2];
            using (myProjectEntities context = new myProjectEntities())
            {
                //check in the student db table
                 var s1 = context.tblStudents.Where(q => q.email == username && q.pass_value == password).FirstOrDefault(); // match username and password
                    if (s1 != null) // something found then
                    {
                        if (s1.status == 1) //if user is verified so status is 1
                        { result[0] = "Stu";
                            result[1] = s1.PK_Student_id.ToString();
                        }
                        else // user need to verify 
                        {result[0] = "Stu_Verify"; 
                            result[1]= "";
                        }
                    }
                else // go to professor db table
                    {

                        var s = context.tblProfessors.Where(q => q.email == username && q.pass_value == password).FirstOrDefault();
                        if (s != null) //some thing found then
                        {
                            if (s.status == 1) // if professor is verified so status=1
                            {
                                result[0] = "Prof";
                                result[1] = s.PK_Professor_id.ToString();
                            }
                            else//professor need to verify
                            {
                                result[0] = "Prof_Verify";
                                result[1] = "";
                            }
                        }
                    
                    else
                        result[0] = "NA";
                                        }

            }
            return result;
        }

        //Password reset 
        public string passwordReset(string username, string currentPass, string newPass)
        {
            string result;
            try //to handle the error
            {
                using (myProjectEntities context = new myProjectEntities())
                {
                    string sql = "";
                    string[] role = userLogin(username, currentPass); // just to check user detail one more time 
                    if (role[0] == "Stu")
                    {
                        //update Student's existing password
                        sql = @"Update[tblStudents] SET pass_value = @myPass WHERE email = @myUser"; //update sql query
                    }
                    else if (role[0] == "Prof")
                    {
                        //update Professor's existing password
                        sql = @"Update[tblProfessors] SET pass_value = @myPass WHERE email = @myUser"; //update sql query
                    }
                    else result = "Fail";
                    context.Database.ExecuteSqlCommand(sql, new SqlParameter("myPass", newPass), new SqlParameter("myUser", username)); //execute sql query
                    result = "1";
                }
            }
            catch (Exception e)
            {
                result = e.ToString();
            }
            
            return result;
        }

        //Account verification then update the status value
        public string accountVerification(string username, string verification, bool role)
        {
            string result;
            if (role) //user is professor
            {
                using (myProjectEntities context = new myProjectEntities())
                {
                    var s = context.tblProfessors.Where(q => q.email == username && q.verification_code == verification && q.status == 0);
                    if (s != null) // to handle null response
                    {
                        //update the status
                        var sql = @"Update[tblProfessors] SET status = 1 WHERE email = @myUser and verification_code = @verify ";
                        context.Database.ExecuteSqlCommand(sql, new SqlParameter("myUser", username), new SqlParameter("verify", verification));
                        result = "1";
                    }
                    else result = "NA";
                }
 
            }
            else //user is Student
            {
                using (myProjectEntities context = new myProjectEntities())
                {
                    var s = context.tblStudents.Where(q => q.email == username && q.verification_code == verification && q.status == 0);
                    if (s != null)
                    {
                        //update the status
                        var sql = @"Update[tblStudents] SET status = 1 WHERE email = @myUser and verification_code = @verify ";
                        context.Database.ExecuteSqlCommand(sql, new SqlParameter("myUser", username), new SqlParameter("verify", verification));
                        result = "1";
                    }
                    else result = "NA";
                }
            }
            return result;   
        }

        // this method will resend the verification code to the user 
        public bool verificationResend(string username, bool role) //role is to check the user role is it professor or student
        {
            
                using (myProjectEntities context = new myProjectEntities())
                { 
                    string name,toUser,verify; // user detail for mail 
                    if (role) // if professor
                    {
                        var s = context.tblProfessors.Where(q => q.email == username).FirstOrDefault();
                        if (s != null) //to handle to null response if user is not available
                        {
                            name = s.firstname + " " + s.middlename + " " + s.lastname;
                            toUser = s.email;
                            verify = s.verification_code;
                        }
                        else return false;
                    }
                    else // student
                    {
                        var s1 = context.tblStudents.Where(q => q.email == username).FirstOrDefault();
                        if (s1 != null)
                        {
                            name = s1.firstname + " " + s1.middlename + " " + s1.lastname;
                            toUser = s1.email;
                            verify = s1.verification_code;
                        }
                        else return false;
                    }
                    myWCFClassLibrary.Service1 myService1 = new myWCFClassLibrary.Service1(); // use existing sendMail method
                    bool res =  myService1.SendEmail(name,toUser,"Account Verification Code", verify ); // send the verification code

                    return res;
                }
           
 
            
        }

        
    }
}
