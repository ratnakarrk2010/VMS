using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityFrameworkDBF.ADMIN_PANEL
{
    public partial class CANCELREASON : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        CANCEL_REASON_MASTER CancelReason = new CANCEL_REASON_MASTER();
        string Flag = "N";
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
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
                if (DVSC.CANCEL_REASON_MASTER.Count() > 0)
                {
                    Gv_CRMaster.DataSource = DVSC.CANCEL_REASON_MASTER.Where(x => x.FLAG == Flag);
                    Gv_CRMaster.DataBind();
                }
                else
                {
                    Gv_CRMaster.DataSource = null;
                    Gv_CRMaster.DataBind();
                }
            }
            catch (Exception)
            {

                Response.Write("<script> alert('Error while loading data.')</script>");
            }
        }

        protected void Gv_CRMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gv_CRMaster.PageIndex = e.NewPageIndex;
            BindGrid(Flag);
        }

        protected void Gv_CRMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Gv_CRMaster.EditIndex = -1;
            BindGrid(Flag);
        }

        protected void Gv_CRMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = (GridViewRow)Gv_CRMaster.Rows[Convert.ToInt32(e.NewEditIndex)];
            TextBox txtFlag_obj = ((TextBox)row.Cells[0].FindControl("txtFlag"));
            HiddenField hdnFlag = Gv_CRMaster.Rows[e.NewEditIndex].FindControl("hdn_Flag") as HiddenField;
            hdn_Flag_New.Value = hdnFlag.Value.Trim();
            if (hdn_Flag_New.Value == "Y")
            {
                Gv_CRMaster.EditIndex = e.NewEditIndex;
                BindGrid("Y");
            }
            else
            {
                Gv_CRMaster.EditIndex = e.NewEditIndex;
                BindGrid("N");
            }
        }

        protected void Gv_CRMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int crid = Convert.ToInt32(Gv_CRMaster.DataKeys[e.RowIndex].Value);
                GridViewRow row = Gv_CRMaster.Rows[e.RowIndex];
                CancelReason = DVSC.CANCEL_REASON_MASTER.First(x => x.CR_ID == crid);
                CancelReason.FLAG = "Y";
                if (!string.IsNullOrEmpty(CancelReason.CR_NAME) && !string.IsNullOrEmpty(CancelReason.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_CRMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in CANCEL REASON.')</script>");
                    BindGrid(Flag);
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while deleting records please try later.')</script>");
            }
        }

        protected void Gv_CRMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = Gv_CRMaster.Rows[e.RowIndex];
                TextBox txtcancelreason = row.FindControl("txtCancelName") as TextBox;
                TextBox txtflag = row.FindControl("txtFlag") as TextBox;
                int crid = Convert.ToInt32(Gv_CRMaster.DataKeys[e.RowIndex].Value);
                CancelReason = DVSC.CANCEL_REASON_MASTER.First(x => x.CR_ID == crid);
                CancelReason.CR_NAME = txtcancelreason.Text.Trim();
                CancelReason.FLAG = txtflag.Text.Trim();
                if (!string.IsNullOrEmpty(CancelReason.CR_NAME) && !string.IsNullOrEmpty(CancelReason.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_CRMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in CANCEL REASON.')</script>");
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
                if (!string.IsNullOrEmpty(txtCancel.Text.Trim()))
                {
                    CancelReason.CR_NAME = txtCancel.Text.Trim();
                    CancelReason.FLAG = "N";
                    DVSC.CANCEL_REASON_MASTER.AddObject(CancelReason);
                    DVSC.SaveChanges();
                    BindGrid(Flag);
                    ClearControl();
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in CANCEL REASON.')</script>");
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Error while saving the record please try later.')</script>");
            }
        }

        public void ClearControl()
        {
            txtCancel.Text = "";
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