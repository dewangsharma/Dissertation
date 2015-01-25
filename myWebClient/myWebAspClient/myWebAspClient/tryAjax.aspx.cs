using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myWebAspClient
{
    public partial class tryAjax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetQuizData(50);

            }
        }

        protected void grdQuiz_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdQuiz.PageIndex = e.NewPageIndex;

            this.GetQuizData(50);
        }

        protected void grdQuiz_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdQuiz.EditIndex = e.NewEditIndex;
            this.GetQuizData(50);
        }

        protected void grdQuiz_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string questionId = ((Label)grdQuiz.Rows[e.RowIndex].FindControl("lblQuestionId")).Text;
            string question = ((TextBox)grdQuiz.Rows[e.RowIndex].FindControl("txtQuestion")).Text;

            string option = ((RadioButtonList)grdQuiz.Rows[e.RowIndex].FindControl("lblOption")).SelectedItem.Value;

            var optionList = ((RadioButtonList)grdQuiz.Rows[e.RowIndex].FindControl("lblOption")) ;

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
            quesM.FK_Quiz_id = 50;
            quesM.optionModel = re.ToArray();
            professor_Quiz_WCFLib.ProfessorQuizClient proxy = new professor_Quiz_WCFLib.ProfessorQuizClient();
          
            bool rr = proxy.UpdateQuestion(quesM);
                

        }

        protected void grdQuiz_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdQuiz.EditIndex = -1;
            this.GetQuizData(50);
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
                               // myRadioListEdit.Items.Add(LI);
                            

                                
                               // dewang.Add(item.option_value);
                        }
                        //ddlCountries.DataSource = dewang;
                        //ddlCountries.DataTextField = "option_value";
                        //ddlCountries.DataValueField = "option_value";
                        //ddlCountries.DataBind();

                        //Add Default Item in the DropDownList
                        //ddlCountries.Items.Insert(0, new ListItem("Please select"));
                    }
                    //Select the Country of Customer in DropDownList
                    //  string country = (e.Row.FindControl("lblCountry") as Label).Text;
                    //  ddlCountries.Items.FindByValue(country).Selected = true;
                }
            }
            catch(Exception ex)
            {

            }
        }
        private void GetQuizData(int quizId)
        {
            professor_Quiz_WCFLib.ProfessorQuizClient proxy = new professor_Quiz_WCFLib.ProfessorQuizClient();
            professor_Quiz_WCFLib.questionModel[] quesModel = proxy.GetAllQuestionByQuiz(quizId);
            //try End
            grdQuiz.DataSource = quesModel;
            grdQuiz.DataBind();
           
            
        }
        private professor_Quiz_WCFLib.optionModel[] OptionData(int quiestionId )
        {

            professor_Quiz_WCFLib.ProfessorQuizClient p = new professor_Quiz_WCFLib.ProfessorQuizClient();

            professor_Quiz_WCFLib.optionModel[] optModel = p.GetAllOptionByQues(quiestionId);

            return optModel;


        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;

            int questionId =Convert.ToInt32(link.CommandArgument);
            
            professor_Quiz_WCFLib.ProfessorQuizClient proxy = new professor_Quiz_WCFLib.ProfessorQuizClient();
            
            bool w = proxy.DeleteQuestion(questionId);

            


        }
        

          
        }

        

}
 