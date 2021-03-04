using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityFrameworkDBF.ADMIN_PANEL
{
    public partial class STATE : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        STATE_MASTER state = new STATE_MASTER();
        string Flag = "N";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                spnDate.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy").Replace("-", " ").ToUpper();
                BindGrid(Flag);
            }
        }

        protected void btnhome_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\DVSC_HOME.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }

        protected void btnpnlhome_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~\\ADMIN PANEL\\ADMIN_PANEL_HOME.aspx");
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }

        public void BindGrid(string Flag)
        {
            try
            {
                if (DVSC.STATE_MASTER.Count() > 0)
                {
                    Gv_StateMaster.DataSource = DVSC.STATE_MASTER.Where(x => x.FLAG == Flag);
                    Gv_StateMaster.DataBind();
                }
                else
                {
                    Gv_StateMaster.DataSource = null;
                    Gv_StateMaster.DataBind();
                }
            }
            catch (Exception)
            {

                Response.Write("<script> alert('Error while loading data.')</script>");
            }
        }

        protected void Gv_StateMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gv_StateMaster.PageIndex = e.NewPageIndex;
            BindGrid(Flag);
        }

        protected void Gv_StateMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Gv_StateMaster.EditIndex = -1;
            BindGrid(Flag);
        }

        protected void Gv_StateMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = (GridViewRow)Gv_StateMaster.Rows[Convert.ToInt32(e.NewEditIndex)];
            TextBox txtFlag_obj = ((TextBox)row.Cells[0].FindControl("txtFlag"));
            HiddenField hdnFlag = Gv_StateMaster.Rows[e.NewEditIndex].FindControl("hdn_Flag") as HiddenField;
            hdn_Flag_New.Value = hdnFlag.Value.Trim();
            if (hdn_Flag_New.Value == "Y")
            {
                Gv_StateMaster.EditIndex = e.NewEditIndex;
                BindGrid("Y");
            }
            else
            {
                Gv_StateMaster.EditIndex = e.NewEditIndex;
                BindGrid("N");
            }
        }

        protected void Gv_StateMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int stateid = Convert.ToInt32(Gv_StateMaster.DataKeys[e.RowIndex].Value);
                GridViewRow row = Gv_StateMaster.Rows[e.RowIndex];
                state = DVSC.STATE_MASTER.First(x => x.STATE_ID == stateid);
                state.FLAG = "Y";
                if (!string.IsNullOrEmpty(state.STATE_NAME) && !string.IsNullOrEmpty(state.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_StateMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in STATE.')</script>");
                    BindGrid(Flag);
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while deleting records please try later.')</script>");
            }
        }

        protected void Gv_StateMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = Gv_StateMaster.Rows[e.RowIndex];
                TextBox txtStatename = row.FindControl("txtStateName") as TextBox;
                TextBox txtflag = row.FindControl("txtFlag") as TextBox;
                int stateid = Convert.ToInt32(Gv_StateMaster.DataKeys[e.RowIndex].Value);
                state = DVSC.STATE_MASTER.First(x => x.STATE_ID == stateid);
                state.STATE_NAME = txtStatename.Text.Trim();
                state.FLAG = txtflag.Text.Trim();
                if (!string.IsNullOrEmpty(state.STATE_NAME) && !string.IsNullOrEmpty(state.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_StateMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in STATE.')</script>");
                    BindGrid(Flag);
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while updating records please try later.')</script>");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtstate.Text.Trim()))
                {
                    state.STATE_NAME = txtstate.Text.Trim();
                    state.FLAG = "N";
                    DVSC.STATE_MASTER.AddObject(state);
                    DVSC.SaveChanges();
                    BindGrid(Flag);
                    ClearControl();
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in STATE.')</script>");
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Error while saving the record please try later.')</script>");
            }
        }

        public void ClearControl()
        {
            txtstate.Text = "";
        }

        protected void btnGetRecord_Click(object sender, EventArgs e)
        {
            try
            {
                Flag = "Y";
                BindGrid(Flag);
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Error while loading record please try later.')</script>");
            }
        }
    }
}