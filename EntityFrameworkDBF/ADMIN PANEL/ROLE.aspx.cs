using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityFrameworkDBF.ADMIN_PANEL
{
    public partial class ROLE : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        ROLEMASTER role = new ROLEMASTER();
        string Flag = "N";
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if(!IsPostBack)
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
            catch(Exception)
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
            catch(Exception)
            {
                Response.Write("<script> alert('Not able to reedirect due to some issue')</script>");
            }
        }

        public void BindGrid(string Flag)
        {
            try
            {
                if(DVSC.ROLEMASTERs.Count() > 0)
                {
                    Gv_RoleMaster.DataSource = DVSC.ROLEMASTERs.Where(x => x.FLAG == Flag);
                    Gv_RoleMaster.DataBind();
                }
                else
                {
                    Gv_RoleMaster.DataSource = null;
                    Gv_RoleMaster.DataBind();
                }
            }
            catch(Exception)
            {

                Response.Write("<script> alert('Error while loading data.')</script>");
            }
        }

        protected void Gv_RoleMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gv_RoleMaster.PageIndex = e.NewPageIndex;
            BindGrid(Flag);
        }

        protected void Gv_RoleMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Gv_RoleMaster.EditIndex = -1;
            BindGrid(Flag);
        }

        protected void Gv_RoleMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = (GridViewRow)Gv_RoleMaster.Rows[Convert.ToInt32(e.NewEditIndex)];
            TextBox txtFlag_obj = ((TextBox)row.Cells[0].FindControl("txtFlag"));
            HiddenField hdnFlag = Gv_RoleMaster.Rows[e.NewEditIndex].FindControl("hdn_Flag") as HiddenField;
            hdn_Flag_New.Value = hdnFlag.Value.Trim();
            if(hdn_Flag_New.Value == "Y")
            {
                Gv_RoleMaster.EditIndex = e.NewEditIndex;
                BindGrid("Y");
            }
            else
            {
                Gv_RoleMaster.EditIndex = e.NewEditIndex;
                BindGrid("N");
            }
        }

        protected void Gv_RoleMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int roleid = Convert.ToInt32(Gv_RoleMaster.DataKeys[e.RowIndex].Value);
                GridViewRow row = Gv_RoleMaster.Rows[e.RowIndex];
                role = DVSC.ROLEMASTERs.First(x => x.ROLEID == roleid);
                role.FLAG = "Y";
                if(!string.IsNullOrEmpty(role.ROLENAME) && !string.IsNullOrEmpty(role.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_RoleMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in ROLE.')</script>");
                    BindGrid(Flag);
                }
            }
            catch(Exception)
            {
                Response.Write("<script> alert('Error while deleting records please try later.')</script>");
            }
        }

        protected void Gv_RoleMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = Gv_RoleMaster.Rows[e.RowIndex];
                TextBox txtRole = row.FindControl("txtRoleName") as TextBox;
                TextBox txtflag = row.FindControl("txtFlag") as TextBox;
                int roleid = Convert.ToInt32(Gv_RoleMaster.DataKeys[e.RowIndex].Value);
                role = DVSC.ROLEMASTERs.First(x => x.ROLEID == roleid);
                role.ROLENAME = txtRole.Text.Trim();
                role.FLAG = txtflag.Text.Trim();
                if(!string.IsNullOrEmpty(role.ROLENAME) && !string.IsNullOrEmpty(role.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_RoleMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in ROLE.')</script>");
                    BindGrid(Flag);
                }
            }
            catch(Exception)
            {
                Response.Write("<script> alert('Error while updating records please try later.')</script>");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(txtRole.Text.Trim()))
                {
                    role.ROLENAME = txtRole.Text.Trim();
                    role.FLAG = "N";
                    DVSC.ROLEMASTERs.AddObject(role);
                    DVSC.SaveChanges();
                    BindGrid(Flag);
                    ClearControl();
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in ROLE.')</script>");
                }
            }
            catch(Exception)
            {
                Response.Write("<script>alert('Error while saving the record please try later.')</script>");
            }
        }

        public void ClearControl()
        {
            txtRole.Text = "";
        }

        protected void btnGetRecord_Click(object sender, EventArgs e)
        {
            try
            {
                Flag = "Y";
                BindGrid(Flag);
            }
            catch(Exception)
            {
                Response.Write("<script>alert('Error while loading record please try later.')</script>");
            }
        }
    }
}