using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityFrameworkDBF.ADMIN_PANEL
{
    public partial class PASSTYPE : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        PASSTYPE_MASTER passtype = new PASSTYPE_MASTER();
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
                if (DVSC.PASSTYPE_MASTER.Count() > 0)
                {
                    Gv_PassMaster.DataSource = DVSC.PASSTYPE_MASTER.Where(x => x.FLAG == Flag);
                    Gv_PassMaster.DataBind();
                }
                else
                {
                    Gv_PassMaster.DataSource = null;
                    Gv_PassMaster.DataBind();
                }
            }
            catch (Exception)
            {

                Response.Write("<script> alert('Error while loading data.')</script>");
            }
        }

        protected void Gv_PassMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gv_PassMaster.PageIndex = e.NewPageIndex;
            BindGrid(Flag);
        }

        protected void Gv_PassMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Gv_PassMaster.EditIndex = -1;
            BindGrid(Flag);
        }

        protected void Gv_PassMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = (GridViewRow)Gv_PassMaster.Rows[Convert.ToInt32(e.NewEditIndex)];
            TextBox txtFlag_obj = ((TextBox)row.Cells[0].FindControl("txtFlag"));
            HiddenField hdnFlag = Gv_PassMaster.Rows[e.NewEditIndex].FindControl("hdn_Flag") as HiddenField;
            hdn_Flag_New.Value = hdnFlag.Value.Trim();
            if (hdn_Flag_New.Value == "Y")
            {
                Gv_PassMaster.EditIndex = e.NewEditIndex;
                BindGrid("Y");
            }
            else
            {
                Gv_PassMaster.EditIndex = e.NewEditIndex;
                BindGrid("N");
            }
        }

        protected void Gv_PassMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int passtypeid = Convert.ToInt32(Gv_PassMaster.DataKeys[e.RowIndex].Value);
                GridViewRow row = Gv_PassMaster.Rows[e.RowIndex];
                passtype = DVSC.PASSTYPE_MASTER.First(x => x.PASSTYPE_ID == passtypeid);
                passtype.FLAG = "Y";
                if (!string.IsNullOrEmpty(passtype.PASSTYPE_NAME) && !string.IsNullOrEmpty(passtype.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_PassMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in PASS TYPE.')</script>");
                    BindGrid(Flag);
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while deleting records please try later.')</script>");
            }
        }

        protected void Gv_PassMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = Gv_PassMaster.Rows[e.RowIndex];
                TextBox txtPassType = row.FindControl("txtPass") as TextBox;
                TextBox txtflag = row.FindControl("txtFlag") as TextBox;
                int passtypeid = Convert.ToInt32(Gv_PassMaster.DataKeys[e.RowIndex].Value);
                passtype = DVSC.PASSTYPE_MASTER.First(x => x.PASSTYPE_ID == passtypeid);
                passtype.PASSTYPE_NAME = txtPassType.Text.Trim();
                passtype.FLAG = txtflag.Text.Trim();
                if (!string.IsNullOrEmpty(passtype.PASSTYPE_NAME) && !string.IsNullOrEmpty(passtype.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_PassMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in PASS TYPE.')</script>");
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
                if (!string.IsNullOrEmpty(txtPass.Text.Trim()))
                {
                    passtype.PASSTYPE_NAME = txtPass.Text.Trim();
                    passtype.FLAG = "N";
                    DVSC.PASSTYPE_MASTER.AddObject(passtype);
                    DVSC.SaveChanges();
                    BindGrid(Flag);
                    ClearControl();
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in PASS TYPE.')</script>");
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Error while saving the record please try later.')</script>");
            }
        }

        public void ClearControl()
        {
            txtPass.Text = "";
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