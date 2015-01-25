using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace myWebAspClient.Professors
{
    
    public partial class Quiz_View : System.Web.UI.Page
    {
        static int profId;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Authenticate the user 
            if (Session["userId"] != null)
            {
                //Label userId = new Label();
                //userId = (Label)Page.Master.FindControl("lblUserId");
                string s = Session["userId"].ToString();
                profId = Convert.ToInt32(s);
            }
            else Response.Redirect("~/Login.aspx");
            if(!IsPostBack)
            GetQuiz();
            
             
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;

            int quizId = Convert.ToInt32(link.CommandArgument);
            string s = HttpUtility.UrlEncode(myEncrypttion(quizId.ToString()));
            Response.Redirect("Quiz_View.aspx?ID=" + s);
        }

        //Encrption code is inspired from : http://www.aspsnippets.com/Articles/Encrypt-and-Decrypt-QueryString-Parameter-Values-in-ASPNet-using-C-and-VBNet.aspx
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

         private void GetQuiz()
        {
            professor_Quiz_WCFLib.ProfessorQuizClient proxy = new professor_Quiz_WCFLib.ProfessorQuizClient();
            professor_Quiz_WCFLib.quizModel[] quizModel = proxy.GetAllQuizByProf(profId, 1);
            grdAllQuiz.DataSource = quizModel;
            grdAllQuiz.DataBind();
        }

         protected void btnAttend_Click(object sender, EventArgs e)
         {
             LinkButton link = (LinkButton)sender;
             
             int quizId = Convert.ToInt32(link.CommandArgument);
             string s = HttpUtility.UrlEncode(myEncrypttion(quizId.ToString()));
             Response.Redirect("Quiz_Attend.aspx?ID=" + s );
         
         }

    }
}