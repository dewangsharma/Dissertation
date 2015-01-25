using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myWebAspClient.Students
{
    public partial class Quiz_All : System.Web.UI.Page
    {
        student_Quiz_WCFLib.QuizClient proxy = new student_Quiz_WCFLib.QuizClient();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            lblModuleId.Text = myDecryption(HttpUtility.UrlDecode(Request.QueryString["ID"]));
            getAllQuiz(Convert.ToInt32(lblModuleId.Text));
        }
        //Decryption code is inspired from : http://www.aspsnippets.com/Articles/Encrypt-and-Decrypt-QueryString-Parameter-Values-in-ASPNet-using-C-and-VBNet.aspx
        private string myDecryption(string data)
        {
            string confidential = "DEWANGUOL99998";
            data = data.Replace(" ", "+");
            byte[] collectData = Convert.FromBase64String(data);
            using (Aes dec = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(confidential, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                dec.Key = pdb.GetBytes(32);
                dec.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, dec.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(collectData, 0, collectData.Length);
                        cs.Close();
                    }
                    data = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return data;
        }
        private void getAllQuiz(int moduleId)
        {
           student_Quiz_WCFLib.quizModel[] qModel =  proxy.GetAllQuizByModule(moduleId);
           grdQuizAll.DataSource = qModel;
           grdQuizAll.DataBind();
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;

            int quizId = Convert.ToInt32(link.CommandArgument);
            string s = HttpUtility.UrlEncode(myEncrypttion(quizId.ToString()));
            Response.Redirect("Quiz_Attend.aspx?id=" + s);
        }
        private string myEncrypttion(string data)
        {
            string confidential = "DEWANGUOL99998";
            byte[] collectData = Encoding.Unicode.GetBytes(data);
            using (Aes dec = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(confidential, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                dec.Key = pdb.GetBytes(32);
                dec.IV = pdb.GetBytes(16);
                using (System.IO.MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, dec.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(collectData, 0, collectData.Length);
                        cs.Close();
                    }
                    data = Convert.ToBase64String(ms.ToArray());
                }
            }
            return data;
        }
    }
}