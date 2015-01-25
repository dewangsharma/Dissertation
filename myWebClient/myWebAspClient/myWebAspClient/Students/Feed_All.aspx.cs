using AjaxControlToolkit;
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
    
    public partial class Feed_All : System.Web.UI.Page
    {
        student_Quiz_WCFLib.QuizClient proxy = new student_Quiz_WCFLib.QuizClient();

        static int studentId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] != null)
            {
                string s = Session["userId"].ToString();
                studentId = Convert.ToInt32(s);
            }
            else Response.Redirect("~/Login.aspx");

            if (!IsPostBack)
            {
                lblModuleId.Text = myDecryption(HttpUtility.UrlDecode(Request.QueryString["ID"]));

                getAllFeed();
            }
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
        void getAllFeed()
        {

            
            grdFeedAll.DataSource = proxy.GetAllFeedbackByModule(Convert.ToInt32(lblModuleId.Text), studentId);
            grdFeedAll.DataBind();
          
        }

        

        protected void grdFeedAll_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lectureId = (e.Row.FindControl("lblLectureId") as Label);
                if (lectureId != null)
                {
                    
                }
                   
            }
        }

        protected void Rating1_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            int rIndex = ((sender as Rating).NamingContainer as GridViewRow).RowIndex;
            int LectureId = Convert.ToInt32(grdFeedAll.DataKeys[rIndex].Value);
            float d = float.Parse(e.Value);
            bool ds =  proxy.createFeedAnswer(studentId, LectureId, float.Parse(e.Value));


        }
    }
}