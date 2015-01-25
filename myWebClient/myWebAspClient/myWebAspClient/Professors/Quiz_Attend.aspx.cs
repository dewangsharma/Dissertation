using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myWebAspClient.Professors
{
    public partial class Attend_Quiz : System.Web.UI.Page
    {
        professor_Quiz_WCFLib.ProfessorQuizClient proxy = new professor_Quiz_WCFLib.ProfessorQuizClient(); //create one instance
        static int profId;
        static float weight;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Authenticate the user 

            if (Session["userId"] != null)
            {
                string s = Session["userId"].ToString();
                profId = Convert.ToInt32(s);
            }

            else Response.Redirect("~/Login.aspx");
            if (!IsPostBack)
            {
                lblQuizId.Text = myDecryption(HttpUtility.UrlDecode(Request.QueryString["ID"]));
                this.GetQuizData(Convert.ToInt32(lblQuizId.Text));
                professor_Quiz_WCFLib.quizModel qM = proxy.GetQuizById(Convert.ToInt32(lblQuizId.Text));
                weight = qM.quiz_weightage;
                lblQuizTitle.Text = qM.quiz_title;
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

        protected void grdQuiz_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdQuiz.PageIndex = e.NewPageIndex;

            this.GetQuizData(Convert.ToInt32(lblQuizId.Text));
        }

        protected void grdQuiz_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                int rCount = grdQuiz.Rows.Count;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label hiddenAnswer = (e.Row.FindControl("lblAnswer") as Label);
                    RadioButtonList myRadioList = (e.Row.FindControl("lblOption") as RadioButtonList);
                    CheckBoxList myCheckList = (e.Row.FindControl("checkOption") as CheckBoxList);
                    TextBox myText = (e.Row.FindControl("txtOption") as TextBox);
                    Label questionId = (e.Row.FindControl("lblQuestionId") as Label);
                    Label qType = (e.Row.FindControl("lblQuestionType") as Label);

                    if (questionId != null)
                    {
                        int i = Convert.ToInt32(questionId.Text);

                        professor_Quiz_WCFLib.optionModel[] optModel = OptionData(i);
                    
                        foreach (professor_Quiz_WCFLib.optionModel item in optModel)
                        {
                            ListItem LI = new ListItem();
                            LI.Text = item.option_value;
                            LI.Value = item.PK_Option_id.ToString();
                            if (item.isAnswer == 1)
                                hiddenAnswer.Text = LI.Value;
                            if (qType.Text == "Optional")
                            {
                                myRadioList.Items.Add(LI);
                                myRadioList.Visible = true;
                            }
                            else if (qType.Text == "Multiple Choice")
                            {
                                myCheckList.Visible = true;
                                myCheckList.Items.Add(LI);
                            }

                            //else if (qType.Text == "Fill in the blanks")
                            //{
                            //    TextBox txt = new TextBox();
                            //    txt.ID = item.PK_Option_id.ToString();
                            //    txt.Attributes.Add("placeholder", item.option_value);

                            //    e.Row.Cells[1].Controls.Add(txt);
                            //}

                        }
                        if (qType.Text == "Paragraph" || qType.Text == "Fill in the blanks")
                        {
                            myText.Visible = true;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.ToString();
                Response.Redirect("Error.aspx");
            }
        }

        private void GetQuizData(int quizId)
        {

            professor_Quiz_WCFLib.questionModel[] quesModel = proxy.GetAllQuestionByQuiz(quizId);

            grdQuiz.DataSource = quesModel;
            grdQuiz.DataBind();


        }

        private professor_Quiz_WCFLib.optionModel[] OptionData(int quiestionId)
        {

            professor_Quiz_WCFLib.optionModel[] optModel = proxy.GetAllOptionByQues(quiestionId);

            return optModel;


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            float qValue = 0;

            foreach (GridViewRow item in grdQuiz.Rows)
            {
                float score = 0;
                Label aId = (Label)item.FindControl("lblAnswer"); //Get answer value
                Label questionMark = (Label)item.FindControl("lblQuestionMark");
                Label qType = (Label)item.FindControl("lblQuestionType");
                Label correct = (Label)item.FindControl("lblCorrect");// Result display
                Label questionId = (Label)item.FindControl("lblQuestionId");
               
                int i = Convert.ToInt32(questionId.Text);

                float qMark = float.Parse(questionMark.Text);
                correct.Visible = true;
                string selectedAnswer = "0";
                RadioButtonList rList = (RadioButtonList)item.FindControl("lblOption");
                CheckBoxList checkList = (CheckBoxList)item.FindControl("checkOption");

                professor_Quiz_WCFLib.optionModel[] optModel = OptionData(i);
                
                switch (qType.Text)
                {
                    case "Optional":
                        if (rList.SelectedIndex > -1)
                        {
                            selectedAnswer = rList.SelectedItem.Value;
                            if (aId.Text == selectedAnswer) //Answer is true
                            {
                                score = qMark;
                                item.CssClass = "alert alert-success";
                                correct.Text = "[Correct]";
                            }
                            else
                            {
                                item.CssClass = "alert alert-danger";
                                correct.Text = "[Not Correct]";
                            }
                        }
                        break;

                    case "Multiple Choice":

                        int ansCount = 0;
                        int ds = 0;
                        int ss = 0;
                        if (checkList.SelectedIndex > -1)
                        {
                            foreach (professor_Quiz_WCFLib.optionModel op in optModel)
                            {
                                if (op.isAnswer == 1) ds = ds+1; //flag to collect answer 
                                for (int z = 0; z < checkList.Items.Count; z++)
                                {
                                    if (op.PK_Option_id == Convert.ToInt32(checkList.Items[z].Value))
                                    {
                                        if (checkList.Items[z].Selected && op.isAnswer == 1)
                                            ansCount = ansCount+1; //partial marking logic
                                        if (checkList.Items[z].Selected) ss = ss+1;
                                    }
                                }
                            }
                            if (ss > ds) //if select more options than correct one
                            {
                                score = 0;
                                item.CssClass = "alert alert-danger";
                                correct.Text = "[Not Correct! You selected more than correct answer]";
                            }
                            else
                            {
                                float r = qMark / ds;
                                score = r * ansCount;
                                if (score == qMark)
                                {
                                    item.CssClass = "alert alert-success";

                                    correct.Text = "[Correct]";
                                }
                                else
                                {
                                    item.CssClass = "alert alert-warning";
                                    correct.Text = "[You answered " + ansCount.ToString() + " correct options]";
                                }

                            }

                        }
                        break;

                    case "Fill in the blanks":
                        score = 1;
                        item.CssClass = "alert alert-warning";
                        item.Enabled = false;
                        break;
                    case "Paragraph":
                        score = 1;
                        item.CssClass = "alert alert-warning";
                        item.Enabled = false;
                        break;
                }
                if (score == 0)
                {
                    item.CssClass = "alert alert-danger";
                    correct.Text = "[Not Correct]";
                     
                }

                qValue = qValue+score;
            }

            alert.Visible = true;
            lblFinalResult.Visible = true;
            lblFinalResult.Text = qValue.ToString();
            lblTotal.Visible = true;
            lblTotal.Text = weight.ToString();

        }
    }
}