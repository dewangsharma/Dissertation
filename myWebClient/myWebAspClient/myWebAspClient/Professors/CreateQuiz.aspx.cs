using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myWebAspClient.Professors
{
    public partial class Create_Quiz : System.Web.UI.Page
    {
        professor_Quiz_WCFLib.ProfessorQuizClient proxy = new professor_Quiz_WCFLib.ProfessorQuizClient(); //create one instance
        modules_WCFLib.ModulesClient moduleProxy = new modules_WCFLib.ModulesClient();
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
           ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showQuestions()", true);
           ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showGridQuiz()", true);
           if (!IsPostBack)
           {
               GetQuizData(Convert.ToInt32(lblQuizId.Text));
               //Add Modules to the drop down
               modules_WCFLib.DepartmentModule[] mod = moduleProxy.GetAllModulesByProfessor(profId);
               for (int i = 0; i < mod.Length; i++)
               {
                   //ddlModule.Items.Add(mod[i].module_name);
                   ddlModule.Items.Insert(i, new ListItem(mod[i].module_name, mod[i].PK_Module_id.ToString()));
               }
           }

        }

        protected void btnCreateQuiz_Click(object sender, EventArgs e) //Create new Quiz
        {
            try
            {
                professor_Quiz_WCFLib.quizModel quizM = new professor_Quiz_WCFLib.quizModel(); // initialise the question model
                quizM.FK_Professor_id = Convert.ToInt32(profId); // professor Id for 
                quizM.quiz_title = txtTitle.Value.Trim();
                quizM.FK_ModuleId =Convert.ToInt32(ddlModule.SelectedItem.Value);
                //  var d = DateTime.Parse(txtDate.Value);
                // var t = DateTime.Parse(txtTime.Value);
                //var start = d + t - t.Date ; //Concate start time
                quizM.quiz_date = DateTime.Parse(txtDate.Value);

                if (rdbTimeUp.Checked)
                    quizM.isTimeup = 1;
                else if (rdbTimeUp1.Checked)
                    quizM.isTimeup = 0;
                quizM.quiz_length = float.Parse(txtLength.Value.Trim());
                quizM.quiz_weightage = float.Parse(txtWeightage.Value.Trim());
                quizM.quiz_end_date = Convert.ToDateTime(txtEndDate.Value); //Concate End time ;

                int res = proxy.CreateQuiz(quizM); // Return is quiz Id 
                //int res = 5;
                if (res > 1) // If it is successfull then check and proceed further
                {
                    lblError.Visible = false;
                    lblQuizId.Text = res.ToString();
                    lblQuizName.Text = quizM.quiz_title;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showQuestions()", true);
                }
                else
                {
                    lblError.Text = "There is an error in the Quiz Creation. Please try again.";
                    lblError.Visible = true;
                }
            }
            catch(Exception ex)
             {
                 Session["Error"] = ex.ToString();
                 Response.Redirect("~/Error.aspx");
            }
        }

        protected void btnAddOption_Click(object sender, EventArgs e) //ADd Option to optionList on clientside
        {
            try
            {
                rdbList.Items.Add(new ListItem(txtOption.Value));
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showQuestions()", true);
                btnSave.Enabled = true;
                txtOption.Value = "";
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.ToString();
                Response.Redirect("~/Error.aspx");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e) //Create new question for quiz
        {
            try
            {
                professor_Quiz_WCFLib.questionModel quesM = new professor_Quiz_WCFLib.questionModel();
                List<professor_Quiz_WCFLib.optionModel> re = new List<professor_Quiz_WCFLib.optionModel>();

                //int count = opt.Option.Count();
                foreach (ListItem item in rdbList.Items)
                {
                    int i = 0;
                    if (item.Selected)
                        i = 1;
                    re.Add(new professor_Quiz_WCFLib.optionModel { option_value = item.Text, isAnswer = i });
                }
                quesM.question = txtQuestion.Value;
               // quesM.question_type = txtQuestionType.Value;

                quesM.FK_Quiz_id = Convert.ToInt32(lblQuizId.Text);
                quesM.optionModel = re.ToArray();
                                
                int rr = proxy.CreateQuestion(quesM);
                if (rr > 1)
                {
                    int Q = Convert.ToInt32(lblShowQuestion.Text);
                    int k = Q + 1;
                    lblShowQuestion.Text = k.ToString();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showGridQuiz()", true);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showQuestions()", true);
                    txtQuestion.Value = ""; // clear question field
                   // txtQuestionType.Value = ""; //clear textbox value
                    rdbList.Items.Clear(); //Clear radiobuttonlist items
                    btnSave.Enabled = false;    // Disable new question create button 
                    lblError.Visible = false; //error message
                    GetQuizData(Convert.ToInt32(lblQuizId.Text));
                }
                else {
                    lblError.Text = "Error in the Question Creation. Please try again";
                    lblError.Visible = true;
                }
                
                
            }
            catch (Exception ex)
            {
                Session["Error"] = ex.ToString();
                Response.Redirect("~/Error.aspx");
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

            string option = ((RadioButtonList)grdQuiz.Rows[e.RowIndex].FindControl("lblOption")).SelectedItem.Value;

            var optionList = ((RadioButtonList)grdQuiz.Rows[e.RowIndex].FindControl("lblOption"));

            professor_Quiz_WCFLib.questionModel quesM = new professor_Quiz_WCFLib.questionModel();
            List<professor_Quiz_WCFLib.optionModel> re = new List<professor_Quiz_WCFLib.optionModel>();

            //int count = opt.Option.Count();
            foreach (ListItem item in optionList.Items)
            {
                int i = 0;
                if (item.Selected)
                    i = 1;
                re.Add(new professor_Quiz_WCFLib.optionModel { option_value = item.Text, isAnswer = i, PK_Option_id = Convert.ToInt32(item.Value) });

            }
            quesM.question = question;
            quesM.question_type = "Objective";
            quesM.PK_Question_id = Convert.ToInt32(questionId);
            quesM.FK_Quiz_id = Convert.ToInt32(lblQuizId.Text); ;
            quesM.optionModel = re.ToArray();
          
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
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    RadioButtonList myRadioList = (e.Row.FindControl("lblOption") as RadioButtonList);
                    Label questionId = (e.Row.FindControl("lblQuestionId") as Label);

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
                            myRadioList.Items.Add(LI);
                           
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
        
    }
}