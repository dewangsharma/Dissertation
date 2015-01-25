using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myWebAspClient.Professors
{
    public partial class Quiz_Create : System.Web.UI.Page
    {
        professor_Quiz_WCFLib.ProfessorQuizClient proxy = new professor_Quiz_WCFLib.ProfessorQuizClient(); //create one instance
        modules_WCFLib.ModulesClient moduleProxy = new modules_WCFLib.ModulesClient();
        static int profId;
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
                
               
                //Add Modules to the drop down
                modules_WCFLib.DepartmentModule[] mod = moduleProxy.GetAllModulesByProfessor(profId);
                for (int i = 0; i < mod.Length; i++)
                {
                    ddlModule.Items.Insert(i, new ListItem(mod[i].module_name, mod[i].PK_Module_id.ToString()));
                }
            }

            placeHolder();

        }

        void placeHolder()
        {
            txtMark.Attributes.Add("placeholder", "Marks");

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
        //****************End Fill in the blanks type question*******************//

        //********************************************************** Create Quiz *************************************************//
        protected void btnCreateQuiz_Click(object sender, EventArgs e)
        {
            try
            {
                professor_Quiz_WCFLib.quizModel quizM = new professor_Quiz_WCFLib.quizModel(); // initialise the question model
                quizM.FK_Professor_id = Convert.ToInt32(profId); // professor Id for 
                quizM.quiz_title = txtTitle.Value.Trim();
                quizM.FK_ModuleId = Convert.ToInt32(ddlModule.SelectedItem.Value);
                DateTime d = Convert.ToDateTime(txtDate.Value);
                quizM.quiz_date = d.Add(TimeSpan.Parse(txtTime.Value));

                if (rdbTimeUp.Checked)
                    quizM.isTimeup = 1;
                else if (rdbTimeUp1.Checked)
                    quizM.isTimeup = 0;
                quizM.quiz_length = float.Parse(txtLength.Value.Trim());
                //quizM.quiz_weightage = float.Parse(txtWeightage.Value.Trim());
                DateTime d1 = Convert.ToDateTime(txtEndDate.Value);
                quizM.quiz_end_date = d1.Add(TimeSpan.Parse(txtEndTime.Value)); 

                int res = proxy.CreateQuiz(quizM); // Return is quiz Id 
                if (res > 1) // If it is successfull then check and proceed further
                {
                    lblQuizId.Text = res.ToString();
                    lblQuizName.Text = quizM.quiz_title;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showQuestions()", true);
                    Quiz.Visible = false;
                    Question.Visible = true;
                }
                else // Not done then 
                {

                }
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.ToString();
                Response.Redirect("~/Error.aspx");
            }
        }
        // ****************************************************************Question Save **************************************************//
        protected void btnSave_Click(object sender, EventArgs e)
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
                 //   lstBlank.Items.Clear();
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

        //********************************************************************************* Display**************************************************//

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
                            else if (q.Text == "Multiple Choice")
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