using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myWebAspClient.Professors
{
   
    public partial class Quiz_View1 : System.Web.UI.Page
    {
        professor_Quiz_WCFLib.ProfessorQuizClient proxy = new professor_Quiz_WCFLib.ProfessorQuizClient(); //create one instance
        static int profId,moduleId;
        float t=0;
        

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
                professor_Quiz_WCFLib.quizModel qModel = proxy.GetQuizById(Convert.ToInt32(lblQuizId.Text));
                if (System.DateTime.Now < qModel.quiz_end_date)
                {
                    GetQuizData(Convert.ToInt32(lblQuizId.Text));


                    lblCurrentQuiz.Text = qModel.quiz_title;
                    txtTitle.Value = qModel.quiz_title;
                    lblStartDate.Text = qModel.quiz_date.ToString();
                    lblEndDate.Text = qModel.quiz_end_date.ToString();
                    if (qModel.isTimeup == 1) rdbTimeUp.Checked = true;
                    else rdbTimeUp1.Checked = false;
                    moduleId = qModel.FK_ModuleId;
                    txtLength.Value = qModel.quiz_length.ToString();
                    txtWeightage.Value = qModel.quiz_weightage.ToString();
                    txtEndDate.Value = qModel.quiz_end_date.ToString();
                }
                else
                {
                   
                    //btnAddOption.Enabled = false;
                    //btnMultAdd.Enabled = false;
                    //btnMultDelete.Enabled = false;
                    //btnOptionDelete.Enabled = false;
                    //btnSave.Enabled = false;
                    //btnUpdateQuiz.Enabled = false;
                    
                }
            }
            placeHolder();

        }
        void placeHolder()
        {
            txtMark.Attributes.Add("placeholder", "Marks");

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

        //********Start Optional QUestion***********//
        // ---> Add Option
        protected void btnAddOption_Click(object sender, EventArgs e)
        {
            try
            {
                rdbList.Items.Add(new ListItem(txtOptOption.Value));
                txtOptOption.Value = "";

            }
            catch (Exception ex)
            {
                Session["Error"] = ex.ToString();
                Response.Redirect("~/Error.aspx");
            }

        }
        //------->Remove Option from RadioButtonList
        protected void btnOptionDelete_Click(object sender, ImageClickEventArgs e)
        {
            if (rdbList.SelectedIndex > -1)
            {
                rdbList.Items.Remove(rdbList.SelectedItem.Value);
            }

        }


        //****************End Option type Question**************************//

        //****************Start Multiple type question*******************//

        //--->Add options to the list
        protected void btnMultAdd_Click(object sender, EventArgs e)
        {
            try
            {
                checkList.Items.Add(new ListItem(txtMultOption.Value));
                txtMultOption.Value = "";

            }
            catch (Exception ex)
            {
                Session["Error"] = ex.ToString();
                Response.Redirect("~/Error.aspx");
            }

        }

        //---->Remove options from the list 
        protected void btnMultDelete_Click(object sender, ImageClickEventArgs e)
        {
            if (checkList.SelectedIndex > -1)
            {

                for (int q = 0; q < checkList.Items.Count; q++)
                {
                    if (checkList.Items[q].Selected) checkList.Items.Remove(checkList.Items[q]);

                }
            }


        }


        //****************End Multiple type question*******************//

        //****************Start Paraphraph type question*******************//
        //****************End Paraphraph type question*******************//

        //****************Start Fill in the blanks type question*******************//

        //-----> Add Blanks to the list
        //protected void btnAddBlank_Click(object sender, EventArgs e)
        //{
        //    int s = lstBlank.Items.Count + 1;

        //    lstBlank.Items.Add(new ListItem("Blank" + s));
        //}

        //Remove Blsnks from the list
        //protected void btnRemove_Click(object sender, ImageClickEventArgs e)
        //{

        //    if (lstBlank.SelectedIndex > -1)
        //    {

        //        for (int q = 0; q < lstBlank.Items.Count; q++)
        //        {
        //            if (lstBlank.Items[q].Selected) lstBlank.Items.Remove(lstBlank.Items[q]);
        //            int s = q + 1;
        //            lstBlank.Items[q].Text = "Blank" + s;
        //        }
        //    }
        //}


        protected void btnSave_Click(object sender, EventArgs e) //Create new question for quiz
        {
            try
            {
                professor_Quiz_WCFLib.questionModel quesM = new professor_Quiz_WCFLib.questionModel();
                quesM.question = txtQuestion.Value.Trim();
                quesM.FK_Quiz_id = Convert.ToInt32(lblQuizId.Text);
                quesM.question_type = ddlQuestionType.SelectedItem.Value;
                quesM.hint = txtHint.Value.Trim();
                quesM.marks = float.Parse(txtMark.Text);
                List<professor_Quiz_WCFLib.optionModel> re = new List<professor_Quiz_WCFLib.optionModel>();
                string myCase = ddlQuestionType.SelectedItem.Value;
                switch (myCase)
                {
                    case "Optional":

                        foreach (ListItem item in rdbList.Items)
                        {
                            int i = 0;
                            if (item.Selected)
                                i = 1;
                            re.Add(new professor_Quiz_WCFLib.optionModel { option_value = item.Text, isAnswer = i });
                        }
                        quesM.optionModel = re.ToArray();

                        break;
                    case "Multiple Choice":
                        foreach (ListItem item in checkList.Items)
                        {
                            int i = 0;
                            if (item.Selected)
                                i = 1;
                            re.Add(new professor_Quiz_WCFLib.optionModel { option_value = item.Text, isAnswer = i });
                        }
                        quesM.optionModel = re.ToArray();
                        break;
                    //case "Fill in the blanks":
                    //    foreach (ListItem item in lstBlank.Items)
                    //    {
                    //        int i = 0;
                    //        if (item.Selected)
                    //            i = 1;
                    //        re.Add(new professor_Quiz_WCFLib.optionModel { option_value = item.Text, isAnswer = i });
                    //    }
                    //    quesM.optionModel = re.ToArray();
                    //    break;
                }
                
                int rr = proxy.Question_Create(quesM);
                if (rr > 1)
                {

                    txtQuestion.Value = ""; // clear question field
                    txtHint.Value = "";
                    txtMark.Text = "";
                    rdbList.Items.Clear(); //Clear radiobuttonlist items
                    checkList.Items.Clear();
                   // lstBlank.Items.Clear();
                    GetQuizData(Convert.ToInt32(lblQuizId.Text)); //Display Quiz

                }
                else
                {

                }


            }
            catch (Exception ex)
            {
                Session["Error"] = ex.ToString();
                Response.Redirect("~/Error.aspx");
            }




        }

        protected void btnUpdateQuiz_Click(object sender, EventArgs e)
        {
            try{
            professor_Quiz_WCFLib.quizModel qModel = new professor_Quiz_WCFLib.quizModel();
            
            if (txtDate.Value == "" || txtTime.Value == "")
            {
                qModel.quiz_date = Convert.ToDateTime(lblStartDate.Text.Trim());
            }
            else
            {
                DateTime ds = Convert.ToDateTime(txtDate.Value);
                qModel.quiz_date = ds.Add(TimeSpan.Parse(txtTime.Value));
            }
            if (txtEndDate.Value == "" || txtEndTime.Value == "")
            {
                qModel.quiz_end_date = Convert.ToDateTime(lblEndDate.Text.Trim());
            }
            else {
                DateTime d1 = Convert.ToDateTime(txtEndDate.Value);
                qModel.quiz_end_date = d1.Add(TimeSpan.Parse(txtEndTime.Value));
            }

            int i = 0;
            if (rdbTimeUp.Checked) i = 1;
            qModel.isTimeup = i;
            qModel.quiz_title = txtTitle.Value.Trim();
            qModel.PK_Quiz_id = Convert.ToInt32(lblQuizId.Text);
            qModel.FK_Professor_id = profId;
            qModel.FK_ModuleId = moduleId;
            qModel.quiz_status = 1;
            qModel.quiz_length = float.Parse(txtLength.Value);
            qModel.quiz_weightage = float.Parse(txtWeightage.Value);
            bool res = proxy.UpdateQuiz(qModel);
            //Response.Redirect(Request.RawUrl);

            }
            catch (Exception ex)
            {
                Session["Error"] = ex.ToString();
                Response.Redirect("~/Error.aspx");
            }

        }
        //********************************************************SELECT Quiz Question **********************************************************//
        void GetQuizData(int quizId)
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;

            int questionId = Convert.ToInt32(link.CommandArgument);

            bool res = proxy.DeleteQuestion(questionId);
            if (res)
            {
                grdQuiz.EditIndex = -1;
                this.GetQuizData(Convert.ToInt32(lblQuizId.Text));
            }
            Response.Redirect(Request.RawUrl);

        }

        protected void grdQuiz_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdQuiz.PageIndex = e.NewPageIndex;

            this.GetQuizData(Convert.ToInt32(lblQuizId.Text));
        }

        protected void grdQuiz_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdQuiz.EditIndex = e.NewEditIndex;

            this.GetQuizData(Convert.ToInt32(lblQuizId.Text));
        }

        protected void grdQuiz_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string questionId = ((Label)grdQuiz.Rows[e.RowIndex].FindControl("lblQuestionId")).Text;
            string question = ((TextBox)grdQuiz.Rows[e.RowIndex].FindControl("txtQuestion")).Text;
            string marks = ((TextBox)grdQuiz.Rows[e.RowIndex].FindControl("txtQuestionMark")).Text;

            //string RadioOption = ((RadioButtonList)grdQuiz.Rows[e.RowIndex].FindControl("gridRdbList")).SelectedItem.Value;
            var RadioOptionList = ((RadioButtonList)grdQuiz.Rows[e.RowIndex].FindControl("gridRdbList"));
            var CheckOptionList = ((CheckBoxList)grdQuiz.Rows[e.RowIndex].FindControl("grdCheckList"));

            string qType = ((Label)grdQuiz.Rows[e.RowIndex].FindControl("lblQuestionType")).Text;

            professor_Quiz_WCFLib.questionModel quesM = new professor_Quiz_WCFLib.questionModel();
            List<professor_Quiz_WCFLib.optionModel> re = new List<professor_Quiz_WCFLib.optionModel>();

            switch (qType)
            {
                case "Optional":

                    foreach (ListItem item in RadioOptionList.Items)
                    {
                        int i = 0;
                        if (item.Selected)
                            i = 1;
                        re.Add(new professor_Quiz_WCFLib.optionModel { option_value = item.Text, isAnswer = i, PK_Option_id = Convert.ToInt32(item.Value) });

                    }
                    quesM.optionModel = re.ToArray();
                    break;
                case "Multiple Choice":

                    foreach (ListItem item in CheckOptionList.Items)
                    {
                        int i = 0;
                        if (item.Selected)
                            i = 1;
                        re.Add(new professor_Quiz_WCFLib.optionModel { option_value = item.Text, isAnswer = i, PK_Option_id = Convert.ToInt32(item.Value) });

                    }
                    quesM.optionModel = re.ToArray();
                    break;
                //case "Fill in the blanks":
                //    foreach (ListItem item in CheckOptionList.Items)
                //    {
                //        re.Add(new professor_Quiz_WCFLib.optionModel { option_value = item.Text, isAnswer = 0, PK_Option_id = Convert.ToInt32(item.Value) });

                //    }
                //    quesM.optionModel = re.ToArray();
                //    break;

            }

            quesM.question = question;
            quesM.marks = Convert.ToDouble(marks);
            quesM.question_type = qType;
            quesM.PK_Question_id = Convert.ToInt32(questionId);
            quesM.FK_Quiz_id = Convert.ToInt32(lblQuizId.Text); ;


            bool res = proxy.UpdateQuestion(quesM);
            if (res)
            {
                grdQuiz.EditIndex = -1;
                this.GetQuizData(Convert.ToInt32(lblQuizId.Text));
            }
            Response.Redirect(Request.RawUrl);

        }

        protected void grdQuiz_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdQuiz.EditIndex = -1;
            this.GetQuizData(Convert.ToInt32(lblQuizId.Text));
        }



        protected void grdQuiz_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {

                    TextBox txtOption = (e.Row.FindControl("txtGridOptionAdd")as TextBox);
                    LinkButton btnAdd = (e.Row.FindControl("btnGridAddOption") as LinkButton);
                    LinkButton btnRemove = (e.Row.FindControl("btnGridRemoveOpt") as LinkButton);
                    RadioButtonList gridRdbList = (e.Row.FindControl("gridRdbList") as RadioButtonList);
                    CheckBoxList gridCheckList = (e.Row.FindControl("grdCheckList") as CheckBoxList);
                    Label questionId = (e.Row.FindControl("lblQuestionId") as Label);
                    CheckBox gridIsAns = (e.Row.FindControl("chkgridIsAnswer") as CheckBox);
                    Label q = (e.Row.FindControl("lblQuestionType") as Label);
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
                                LI.Selected = true;
                            if (q.Text == "Optional")
                            {
                                txtOption.Visible = true;
                                btnAdd.Visible = true;
                                btnRemove.Visible = true;
                                gridRdbList.Visible = true;
                                gridRdbList.Items.Add(LI);

                            }
                            else if (q.Text == "Multiple Choice" || q.Text == "Fill in the blanks")
                            {
                                txtOption.Visible = true;
                                btnAdd.Visible = true;
                                btnRemove.Visible = true;
                                gridCheckList.Visible = true;
                                gridCheckList.Items.Add(LI);
                                if (q.Text == "Multiple Choice")
                                    gridIsAns.Visible = true;
                            }
                        }
                    }
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    RadioButtonList myRadioList = (e.Row.FindControl("lblOption") as RadioButtonList);
                    CheckBoxList myCheckList = (e.Row.FindControl("lblCheckList") as CheckBoxList);
                    Label questionId = (e.Row.FindControl("lblQuestionId") as Label);
                    Label qM = (e.Row.FindControl("lblQuestionMark")as Label);
                    t =t+ float.Parse(qM.Text);

                    
                    Label quesType = (e.Row.FindControl("lblQuestionType") as Label);
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
                                LI.Selected = true;
                            if (quesType.Text == "Optional")
                            {
                                myRadioList.Items.Add(LI);
                                myRadioList.Visible = true;
                            }
                            else if (quesType.Text == "Multiple Choice")
                            {
                                myCheckList.Visible = true;
                                myCheckList.Items.Add(LI);
                            }

                        }

                    }

                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    Label lblTotalMarks = (Label)e.Row.FindControl("lblGridWeightage");
                    lblTotalMarks.Text = t.ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void grdQuiz_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Label qId = (Label)grdQuiz.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblQuestionId");
            Label qType = (Label)grdQuiz.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblQuestionType");
            CheckBox isAns = (CheckBox)grdQuiz.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("chkgridIsAnswer");
            if (e.CommandName == "AddOpt")
            {
                TextBox optionValue = (TextBox)grdQuiz.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("txtGridOptionAdd");
                professor_Quiz_WCFLib.optionModel optModel = new professor_Quiz_WCFLib.optionModel();
                optModel.FK_Question_id = Convert.ToInt32(qId.Text);
                optModel.option_value = optionValue.Text.Trim();

                if (isAns.Checked) optModel.isAnswer = 1;
                else optModel.isAnswer = 0;

                proxy.CreateOption(optModel);
                grdQuiz.EditIndex = -1;
                this.GetQuizData(Convert.ToInt32(lblQuizId.Text));

            }
            else if (e.CommandName == "RemoveOpt")
            {
                if (qType.Text == "Optional")
                {
                    RadioButtonList gridRdbList = (RadioButtonList)grdQuiz.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("gridRdbList");
                    int i = Convert.ToInt32(gridRdbList.SelectedItem.Value);
                    int[] s = new int[1];
                    s[0] = i;
                    proxy.DeleteOptionByOptionId(s);

                }
                else if (qType.Text == "Multiple Choice")
                {
                    CheckBoxList griCheckList = (CheckBoxList)grdQuiz.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("grdCheckList");
                    int[] s = new int[griCheckList.Items.Count];
                    for (int i = 0; i < griCheckList.Items.Count; i++)
                    {
                        if (griCheckList.Items[i].Selected)
                            s[i] = Convert.ToInt32(griCheckList.Items[i].Value);
                    }
                    proxy.DeleteOptionByOptionId(s);
                }
            }
        }




    }
}

        
    