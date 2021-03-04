using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityFrameworkDBF.ADMIN_PANEL
{
    public partial class RELIGION : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        RELIGION_MASTER religion = new RELIGION_MASTER();
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
                if (DVSC.RELIGION_MASTER.Count() > 0)
                {
                    Gv_ReligionMaster.DataSource = DVSC.RELIGION_MASTER.Where(x => x.FLAG == Flag);
                    Gv_ReligionMaster.DataBind();
                }
                else
                {
                    Gv_ReligionMaster.DataSource = null;
                    Gv_ReligionMaster.DataBind();
                }
            }
            catch (Exception)
            {

                Response.Write("<script> alert('Error while loading data.')</script>");
            }
        }

        protected void Gv_ReligionMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gv_ReligionMaster.PageIndex = e.NewPageIndex;
            BindGrid(Flag);
        }

        protected void Gv_ReligionMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Gv_ReligionMaster.EditIndex = -1;
            BindGrid(Flag);
        }

        protected void Gv_ReligionMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = (GridViewRow)Gv_ReligionMaster.Rows[Convert.ToInt32(e.NewEditIndex)];
            TextBox txtFlag_obj = ((TextBox)row.Cells[0].FindControl("txtFlag"));
            HiddenField hdnFlag = Gv_ReligionMaster.Rows[e.NewEditIndex].FindControl("hdn_Flag") as HiddenField;
            hdn_Flag_New.Value = hdnFlag.Value.Trim();
            if (hdn_Flag_New.Value == "Y")
            {
                Gv_ReligionMaster.EditIndex = e.NewEditIndex;
                BindGrid("Y");
            }
            else
            {
                Gv_ReligionMaster.EditIndex = e.NewEditIndex;
                BindGrid("N");
            }
        }

        protected void Gv_ReligionMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rid = Convert.ToInt32(Gv_ReligionMaster.DataKeys[e.RowIndex].Value);
                GridViewRow row = Gv_ReligionMaster.Rows[e.RowIndex];
                religion = DVSC.RELIGION_MASTER.First(x => x.R_ID == rid);
                religion.FLAG = "Y";
                if(!string.IsNullOrEmpty(religion.R_NAME) && !string.IsNullOrEmpty(religion.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_ReligionMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in RELIGION.')</script>");
                    BindGrid(Flag);
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while deleting records please try later.')</script>");
            }
        }

        protected void Gv_ReligionMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = Gv_ReligionMaster.Rows[e.RowIndex];
                TextBox txtStatename = row.FindControl("txtReligionName") as TextBox;
                TextBox txtflag = row.FindControl("txtFlag") as TextBox;
                int rid = Convert.ToInt32(Gv_ReligionMaster.DataKeys[e.RowIndex].Value);
                religion = DVSC.RELIGION_MASTER.First(x => x.R_ID == rid);
                religion.R_NAME = txtStatename.Text.Trim();
                religion.FLAG = txtflag.Text.Trim();
                if(!string.IsNullOrEmpty(religion.R_NAME) && !string.IsNullOrEmpty(religion.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_ReligionMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in RELIGION.')</script>");
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
                if(!string.IsNullOrEmpty(txtReligion.Text.Trim()))
                {
                    religion.R_NAME = txtReligion.Text.Trim();
                    religion.FLAG = "N";
                    DVSC.RELIGION_MASTER.AddObject(religion);
                    DVSC.SaveChanges();
                    BindGrid(Flag);
                    ClearControl();
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in RELIGION.')</script>");
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Error while saving the record please try later.')</script>");
            }
        }

        public void ClearControl()
        {
            txtReligion.Text = "";
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