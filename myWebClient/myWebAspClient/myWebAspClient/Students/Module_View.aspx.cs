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
    public partial class Module_View : System.Web.UI.Page
    {
        modules_WCFLib.ModulesClient proxy = new modules_WCFLib.ModulesClient();
        static int studentId;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Authenticate the user 
            if (Session["userId"] != null)
            {

                string s = Session["userId"].ToString();
                studentId = Convert.ToInt32(s);
            }
            else Response.Redirect("~/Login.aspx");
            

           modules_WCFLib.DepartmentModule[] dModule =  proxy.GetAllModulesByStudent(studentId);
           grdModules.DataSource = dModule;
           grdModules.DataBind();

        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;

            int quizId = Convert.ToInt32(link.CommandArgument);
            string s = HttpUtility.UrlEncode(myEncrypttion(quizId.ToString()));
            Response.Redirect("Quiz_All.aspx?id=" + s);
        }
        //Encryption code is inspired from : http://www.aspsnippets.com/Articles/Encrypt-and-Decrypt-QueryString-Parameter-Values-in-ASPNet-using-C-and-VBNet.aspx
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

        protected void lnkFeed_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;

            int moduleId = Convert.ToInt32(link.CommandArgument);
            string s = HttpUtility.UrlEncode(myEncrypttion(moduleId.ToString()));
            Response.Redirect("Feed_All.aspx?id=" + s);
       
        }

    }
}