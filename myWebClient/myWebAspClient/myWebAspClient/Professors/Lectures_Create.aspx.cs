using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myWebAspClient.Professors
{
    public partial class Lecture_Create : System.Web.UI.Page
    {
        professor_Feedback_WCFLib.ProfessorFeedbackClient proxy = new professor_Feedback_WCFLib.ProfessorFeedbackClient();
        modules_WCFLib.ModulesClient moduleProxy = new modules_WCFLib.ModulesClient();
        static int profId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] != null)
            {
                string s = Session["userId"].ToString();
                profId = Convert.ToInt32(s);
            }
            else Response.Redirect("~/Login.aspx");
            if (!IsPostBack)
            {
                GetLectureData();
                //Add Modules to the drop down
                modules_WCFLib.DepartmentModule[] mod = moduleProxy.GetAllModulesByProfessor(profId);
                for (int i = 0; i < mod.Length; i++)
                {
                    ddlModule.Items.Insert(i, new ListItem(mod[i].module_name, mod[i].PK_Module_id.ToString()));
                }

            }

        }
        private void GetLectureData()
        {
            professor_Feedback_WCFLib.lectureModel[] lect = proxy.GetLecturesByProfessor(profId, 2);
            grdLectures.DataSource = lect;
            grdLectures.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            professor_Feedback_WCFLib.lectureModel lectureModule = new professor_Feedback_WCFLib.lectureModel();
            lectureModule.FK_Module_id = Convert.ToInt32(ddlModule.SelectedItem.Value);
            lectureModule.lecture_date = Convert.ToDateTime(txtDate.Value);
            lectureModule.lecture_time = TimeSpan.Parse(txtTime.Value);
            lectureModule.lecture_title = txtTitle.Text.Trim();
            lectureModule.FK_Professor_id = profId;
            bool isFeed=false;
            DateTime endingDate = DateTime.Now;
            if (checkFeed.Checked) {isFeed = true;
                endingDate =Convert.ToDateTime(endDate.Value);}
            int lectureId =  proxy.createLecture(lectureModule,isFeed,endingDate);
            alert.Visible = true;
            txtTitle.Text = "";
            GetLectureData();
        }

        //protected void grdLectures_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        DropDownList ddlModule = (e.Row.FindControl("ddlModule") as DropDownList);
        //        Label moduleId = (e.Row.FindControl("lblModuleId") as Label);
        //        Label moduleName = (e.Row.FindControl("lblModule") as Label);
        //        modules_WCFLib.DepartmentModule[] mod = moduleProxy.GetAllModulesByProfessor(profId);
        //        foreach (modules_WCFLib.DepartmentModule department in mod)
        //        {
        //            ListItem LI = new ListItem();
        //            LI.Text = department.module_name;
        //            LI.Value = department.PK_Module_id.ToString();
        //            if (LI.Text == moduleId.Text) moduleName.Text = LI.Value ;
        //            ddlModule.Items.Add(LI);

        //        }
        //    }
        //    catch (Exception ex)
        //    { }
        //}

        //private modules_WCFLib.DepartmentModule[] getProfessorModule()
        //{
        //    modules_WCFLib.DepartmentModule[] mod = moduleProxy.GetAllModulesByProfessor(profId);
        //    return mod;
        //}



        protected void grdLectures_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdLectures.EditIndex = e.NewEditIndex;

            this.GetLectureData();

            DropDownList d = (DropDownList)grdLectures.Rows[e.NewEditIndex].FindControl("ddlModule");
            modules_WCFLib.DepartmentModule[] mod = moduleProxy.GetAllModulesByProfessor(profId);
            foreach (modules_WCFLib.DepartmentModule department in mod)
            {
                ListItem LI = new ListItem();
                LI.Text = department.module_name;
                LI.Value = department.PK_Module_id.ToString();
                d.Items.Add(LI);

            }


        }

        

        //protected void grdLectures_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
            
        //}

        //protected void grdLectures_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    grdLectures.EditIndex = -1;
        //    this.GetLectureData();
        //}
    }
}