using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityFrameworkDBF.ADMIN_PANEL
{
    public partial class DESIGNATION : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        DESIGNATION_MASTER designation = new DESIGNATION_MASTER();
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
                if (DVSC.DESIGNATION_MASTER.Count() > 0)
                {
                    Gv_DesignationMaster.DataSource = DVSC.DESIGNATION_MASTER.Where(x => x.FLAG == Flag);
                    Gv_DesignationMaster.DataBind();
                }
                else
                {
                    Gv_DesignationMaster.DataSource = null;
                    Gv_DesignationMaster.DataBind();
                }
            }
            catch (Exception)
            {

                Response.Write("<script> alert('Error while loading data.')</script>");
            }
        }

        protected void Gv_DesignationMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gv_DesignationMaster.PageIndex = e.NewPageIndex;
            BindGrid(Flag);
        }

        protected void Gv_DesignationMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Gv_DesignationMaster.EditIndex = -1;
            BindGrid(Flag);
        }

        protected void Gv_DesignationMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = (GridViewRow)Gv_DesignationMaster.Rows[Convert.ToInt32(e.NewEditIndex)];
            TextBox txtFlag_obj = ((TextBox)row.Cells[0].FindControl("txtFlag"));
            HiddenField hdnFlag = Gv_DesignationMaster.Rows[e.NewEditIndex].FindControl("hdn_Flag") as HiddenField;
            hdn_Flag_New.Value = hdnFlag.Value.Trim();
            if (hdn_Flag_New.Value == "Y")
            {
                Gv_DesignationMaster.EditIndex = e.NewEditIndex;
                BindGrid("Y");
            }
            else
            {
                Gv_DesignationMaster.EditIndex = e.NewEditIndex;
                BindGrid("N");
            }
        }

        protected void Gv_DesignationMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int desigid = Convert.ToInt32(Gv_DesignationMaster.DataKeys[e.RowIndex].Value);
                GridViewRow row = Gv_DesignationMaster.Rows[e.RowIndex];
                designation = DVSC.DESIGNATION_MASTER.First(x => x.DESIGNATION_ID == desigid);
                designation.FLAG = "Y";
                if (!string.IsNullOrEmpty(designation.DESIGNATION_NAME) && !string.IsNullOrEmpty(designation.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_DesignationMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in DESIGNATION.')</script>");
                    BindGrid(Flag);
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while deleting records please try later.')</script>");
            }
        }

        protected void Gv_DesignationMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = Gv_DesignationMaster.Rows[e.RowIndex];
                TextBox txtDesigname = row.FindControl("txtDesigName") as TextBox;
                TextBox txtflag = row.FindControl("txtFlag") as TextBox;
                int desigid = Convert.ToInt32(Gv_DesignationMaster.DataKeys[e.RowIndex].Value);
                designation = DVSC.DESIGNATION_MASTER.First(x => x.DESIGNATION_ID == desigid);
                designation.DESIGNATION_NAME = txtDesigname.Text.Trim();
                designation.FLAG = txtflag.Text.Trim();
                if (!string.IsNullOrEmpty(designation.DESIGNATION_NAME) && !string.IsNullOrEmpty(designation.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_DesignationMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in DESIGNATION.')</script>");
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
                var count = DVSC.DESIGNATION_MASTER.Count(x => x.DESIGNATION_NAME == txtdesig.Text.Trim());
                if (count > 0)
                {
                    Response.Write("<script> alert('Designation Is already registered.')</script>");
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtdesig.Text.Trim()))
                    {
                        designation.DESIGNATION_NAME = txtdesig.Text.Trim();
                        designation.FLAG = "N";
                        DVSC.DESIGNATION_MASTER.AddObject(designation);
                        DVSC.SaveChanges();
                        BindGrid(Flag);
                        ClearControl();
                    }
                    else
                    {
                        Response.Write("<script>alert('You cannot insert blank in DESIGNATION.')</script>");
                    }
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Error while saving the record please try later.')</script>");
            }

        }

        public void ClearControl()
        {
            txtdesig.Text = "";
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                var GetDesig = (from x in DVSC.DESIGNATION_MASTER
                                where (x.DESIGNATION_NAME.Contains(txtSearch.Text.Trim()))
                                select x);
                if (GetDesig.Count() > 0)
                {
                    Gv_DesignationMaster.DataSource = GetDesig;
                    Gv_DesignationMaster.DataBind();
                    txtSearch.Text = "";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGrid("N");
        }
    }
}