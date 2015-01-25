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
    public partial class Quiz_Attend : System.Web.UI.Page
    {
        professor_Quiz_WCFLib.ProfessorQuizClient proxy = new professor_Quiz_WCFLib.ProfessorQuizClient(); //create one instance
        student_Quiz_WCFLib.QuizClient proxy1 = new student_Quiz_WCFLib.QuizClient(); 
        static int studentId;
        static float weight;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //Authenticate the user 
            if (Session["userId"] != null)
            {
                string s = Session["userId"].ToString();
                studentId = Convert.ToInt32(s);
            }
            else Response.Redirect("~/Login.aspx");
            if (!IsPostBack)
            {
                lblQuizId.Text = myDecryption(HttpUtility.UrlDecode(Request.QueryString["id"]));
                bool status = proxy1.GetStdudentAttempt(studentId,Convert.ToInt32(lblQuizId.Text));
                professor_Quiz_WCFLib.quizModel qM = proxy.GetQuizById(Convert.ToInt32(lblQuizId.Text));
                weight = qM.quiz_weightage;
                lblQuizTitle.Text = qM.quiz_title;
                if (status)
                {
                    

                    GetQuizData(Convert.ToInt32(lblQuizId.Text));
                }
                else { lblAlready.Visible = true;  }
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
                            //    txt.EnableViewState = true;
                                
                            //    e.Row.Cells[1].Controls.Add(txt);
                            //}

                        }
                        if (qType.Text == "Paragraph")
                        {
                            TextBox myText = (e.Row.FindControl("txtOption") as TextBox);
                            myText.Visible = true;
                        }
                        else if (qType.Text == "Fill in the blanks")
                        {
                            TextBox myBlank = (e.Row.FindControl("txtBlank") as TextBox);
                            myBlank.Visible=true ;
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
            List<student_Quiz_WCFLib.QuizResultModel> quizRAllAnswer = new List<student_Quiz_WCFLib.QuizResultModel>();
            student_Quiz_WCFLib.QuizClient pr = new student_Quiz_WCFLib.QuizClient();
            

            float qValue = 0;
            foreach (GridViewRow item in grdQuiz.Rows)
            {
                
        

                float score = 0;
                TextBox ttt = item.FindControl("grdQuiz_189") as TextBox;
                //string asd = ttt.Text;
                Label aId = (Label)item.FindControl("lblAnswer"); //Get answer value
                Label questionMark = (Label)item.FindControl("lblQuestionMark");
                Label qType = (Label)item.FindControl("lblQuestionType");
                Label correct = (Label)item.FindControl("lblCorrect");// Result display
                Label questionId = (Label)item.FindControl("lblQuestionId");

                int i = Convert.ToInt32(questionId.Text);

                float qMark = float.Parse(questionMark.Text);
                correct.Visible = true;
                
              


                professor_Quiz_WCFLib.optionModel[] optModel = OptionData(i);

                switch (qType.Text)
                {
                    case "Optional":
                        string selectedAnswer = "0";
                        RadioButtonList rList = (RadioButtonList)item.FindControl("lblOption");
               
                        student_Quiz_WCFLib.QuizResultModel quizR = new student_Quiz_WCFLib.QuizResultModel();
                        quizR.FK_QuizId = Convert.ToInt32(lblQuizId.Text);
                        quizR.FK_QuestionId = Convert.ToInt32(questionId.Text);
                        quizR.FK_OptionId = 1;
                        quizR.FK_studentId = studentId;
                        quizR.update = System.DateTime.Now;
                        if (rList.SelectedIndex > -1)
                        {
                            quizR.FK_OptionId =Convert.ToInt32(rList.SelectedItem.Value); 
                            quizR.isAnswered = 1;
                            selectedAnswer = rList.SelectedItem.Value;
                            if (aId.Text == selectedAnswer) //Answer is true
                            {
                                score = qMark;
                                quizR.marks = score;
                                item.CssClass = "alert alert-success";
                                correct.Text = "[Correct]";
                            }
                            else
                            {
                                quizR.marks = 0;
                                item.CssClass = "alert alert-danger";
                                correct.Text = "[Not Correct]";
                            }
                            
                        }
                        else
                        {
                            quizR.marks = 0;
                            quizR.isAnswered = 0;
                           
                        }
                        quizRAllAnswer.Add(quizR);
                        break;

                    case "Multiple Choice":
                        CheckBoxList checkList = (CheckBoxList)item.FindControl("checkOption");
                        List<student_Quiz_WCFLib.QuizResultModel> quizRMultAll = new List<student_Quiz_WCFLib.QuizResultModel>();
                        int attemptedAnswer=0;
                        int answerList = 0;
                        float optMark = 0;
                            for(int sw=0;sw<checkList.Items.Count;sw++)
                            {
                                if (checkList.Items[sw].Selected) attemptedAnswer++;
                            }

                            foreach (professor_Quiz_WCFLib.optionModel oopptt in optModel)
                            {
                                if(oopptt.isAnswer == 1) answerList++;
                            }
                            
                            optMark = qMark / answerList;
                            if (attemptedAnswer > answerList) optMark = 0;
                            for (int qw = 0; qw < checkList.Items.Count; qw++)
                            {
                                if (checkList.Items[qw].Selected)
                                {
                                    student_Quiz_WCFLib.QuizResultModel quizRMult = new student_Quiz_WCFLib.QuizResultModel();
                                    quizRMult.FK_OptionId = Convert.ToInt32(checkList.Items[qw].Value);
                                    quizRMult.FK_QuestionId = i;
                                    quizRMult.FK_QuizId = Convert.ToInt32(lblQuizId.Text);
                                    quizRMult.FK_studentId = studentId;
                                    quizRMult.isAnswered = 1;
                                    quizRMult.update = System.DateTime.Now;
                                    foreach (professor_Quiz_WCFLib.optionModel oopptt in optModel)
                                    {
                                        if (oopptt.PK_Option_id == Convert.ToInt32(checkList.Items[qw].Value))
                                        {
                                            if (oopptt.isAnswer == Convert.ToInt32(checkList.Items[qw].Selected))
                                            {
                                                quizRMult.marks = optMark;// Add logic for partial marking
                                                item.CssClass = "alert alert-success";
                                                correct.Text = "[Partially Correct]";

                                            }
                                            else
                                                quizRMult.marks = 0;
                                        }
                                    }
                                    quizRMultAll.Add(quizRMult);
                                }
                        }
                            foreach (student_Quiz_WCFLib.QuizResultModel sds in quizRMultAll)
                            {
                                quizRAllAnswer.Add(sds);

                            }
                        
                            break;

                    case "Fill in the blanks":
                        score = 1;
                        TextBox txtBlank = (TextBox)item.FindControl("txtBlank");
                        student_Quiz_WCFLib.QuizResultModel quizBlank = new student_Quiz_WCFLib.QuizResultModel();
                        quizBlank.answer = txtBlank.Text.Trim();
                        quizBlank.FK_OptionId = 219;
                        quizBlank.FK_QuestionId = i;
                        quizBlank.FK_QuizId = Convert.ToInt32(lblQuizId.Text);
                        quizBlank.FK_studentId = studentId;
                        quizBlank.isAnswered = 1;
                        quizBlank.marks = 0;
                        quizBlank.update = System.DateTime.Now;
                        if (txtBlank.Text == "")
                            quizBlank.isAnswered = 0;
                        quizRAllAnswer.Add(quizBlank);

                        //score = 1;
                        //List<student_Quiz_WCFLib.QuizResultModel> myQuizBlank = new List<student_Quiz_WCFLib.QuizResultModel>();

                        //foreach (professor_Quiz_WCFLib.optionModel ooptt in optModel)
                        //{
                        //    student_Quiz_WCFLib.QuizResultModel quizBlank = new student_Quiz_WCFLib.QuizResultModel();

                        //    string s = ooptt.PK_Option_id.ToString();
                        //    TextBox t = (TextBox)item.FindControl("189");
                        //    t.Text = "Dewang";
                        //    TextBox dt = (TextBox)grdQuiz.Rows[3].FindControl("189");
                        //   // TextBox dt = (TextBox)item.Cells[1].FindControl("grdQuiz_189_");
                        //    string qwe = t.Text;
                        //    quizBlank.answer = t.Text.Trim();
                        //    quizBlank.FK_OptionId = ooptt.PK_Option_id;
                        //    quizBlank.FK_QuestionId = i;
                        //    quizBlank.FK_QuizId = Convert.ToInt32(lblQuizId.Text);
                        //    quizBlank.FK_studentId = studentId;
                        //    quizBlank.isAnswered = 1;
                        //    quizBlank.marks = 0;
                        //    if (t.Text == "")
                        //        quizBlank.isAnswered = 0;
                        //    myQuizBlank.Add(quizBlank);
                            
                        //}
                        //quizRAllAnswer = quizRAllAnswer.Concat(myQuizBlank).ToList();

                        break;
                    case "Paragraph":
                        score = 1;
                        TextBox txtPara = (TextBox)item.FindControl("txtOption");
                        student_Quiz_WCFLib.QuizResultModel quizpara = new student_Quiz_WCFLib.QuizResultModel();
                        quizpara.answer = txtPara.Text.Trim();
                        quizpara.FK_OptionId = 219;
                        quizpara.FK_QuestionId = i;
                        quizpara.FK_QuizId = Convert.ToInt32(lblQuizId.Text);
                        quizpara.FK_studentId = studentId;
                        quizpara.isAnswered = 1;
                        quizpara.marks = 0;
                        quizpara.update = System.DateTime.Now;
                        if (txtPara.Text == "")
                            quizpara.isAnswered = 0;
                        quizRAllAnswer.Add(quizpara);

                        break;
                }
                if (score == 0)
                {
                    item.CssClass = "alert alert-danger";
                    correct.Text = "[Not Correct]";

                }

                qValue = qValue + score;
            }
















            //*************************************??/////////////////////////////////////
            //List<student_Quiz_WCFLib.QuizResultModel> quizRAllAnswer = new List<student_Quiz_WCFLib.QuizResultModel>();
            //// student_Quiz_WCFLib.tblQuizResult[] tt;
            //student_Quiz_WCFLib.QuizClient pr = new student_Quiz_WCFLib.QuizClient();
           

            //int grd = grdQuiz.Rows.Count;

            //float qValue = weight / grd;
            //int s = 0;
            //foreach (GridViewRow item in grdQuiz.Rows)
            //{
            //    student_Quiz_WCFLib.QuizResultModel quizR = new student_Quiz_WCFLib.QuizResultModel();

            //    string selectedAnswer = "0";

            //    Label correct = (Label)item.FindControl("lblCorrect");// Result display
            //    correct.Visible = true;

            //    Label aId = (Label)item.FindControl("lblAnswer"); //Get answer value
            //    Label questionId = (Label)item.FindControl("lblQuestionId"); // Get QuestionId
            //    RadioButtonList rList = (RadioButtonList)item.FindControl("lblOption");

            //    quizR.FK_QuizId = Convert.ToInt32(lblQuizId.Text);
            //    quizR.FK_QuestionId = Convert.ToInt32(questionId.Text);
            //    quizR.FK_studentId = studentId;
                
            //    if (rList.SelectedIndex > -1)
            //    {
            //        //To display result
            //        selectedAnswer = rList.SelectedItem.Value;
            //        quizR.isAnswered = 1;
            //        quizR.FK_OptionId = Convert.ToInt32(selectedAnswer);
            //        if (aId.Text == selectedAnswer) //Answer is true
            //        {
            //            s = s + 1;
            //            item.CssClass = "alert alert-success";

            //            correct.Text = "[Correct]";
            //        }
            //        else //Answer is not correct 
            //        {
            //            item.CssClass = "alert alert-danger";
            //            correct.Text = "[Not Correct]";
            //        }
            //    }
            //    else //Not attempted
            //    {
            //        quizR.isAnswered = 0;
            //        quizR.FK_OptionId = 0;
            //        item.CssClass = "alert alert-warning";
            //        correct.Text = "[Not Attempted]";
            //    }

            //    quizRAllAnswer.Add(quizR);

            //}
            


            bool res = pr.quizAnswer(quizRAllAnswer.ToArray());
            alert.Visible = true;
            lblFinalResult.Visible = true;
            lblFinalResult.Text = qValue.ToString();
            lblTotal.Visible = true;
            lblTotal.Text = weight.ToString();
            btnSubmit.Visible = false;
           
            grdQuiz.Enabled = false;
            
                
        }

        


    }
}