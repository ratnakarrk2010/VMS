using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EntityFrameworkDBF.ADMIN_PANEL
{
    public partial class PSU : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        PSU_MASTER psu = new PSU_MASTER();
        string Flag = "N";
        DropDownFunction ddl = new DropDownFunction();
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
                if (DVSC.PSU_MASTER.Count() > 0)
                {
                    Gv_PSUMaster.DataSource = DVSC.PSU_MASTER.Where(x => x.FLAG == Flag);
                    Gv_PSUMaster.DataBind();
                }
                else
                {
                    Gv_PSUMaster.DataSource = null;
                    Gv_PSUMaster.DataBind();
                }
            }
            catch (Exception)
            {

                Response.Write("<script> alert('Error while loading data.')</script>");
            }
        }

        protected void Gv_PSUMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gv_PSUMaster.PageIndex = e.NewPageIndex;
            BindGrid(Flag);
        }

        protected void Gv_StateMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Gv_PSUMaster.EditIndex = -1;
            BindGrid(Flag);
        }

        protected void Gv_PSUMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = (GridViewRow)Gv_PSUMaster.Rows[Convert.ToInt32(e.NewEditIndex)];
            TextBox txtFlag_obj = ((TextBox)row.Cells[0].FindControl("txtFlag"));
            HiddenField hdnFlag = Gv_PSUMaster.Rows[e.NewEditIndex].FindControl("hdn_Flag") as HiddenField;
            hdn_Flag_New.Value = hdnFlag.Value.Trim();
            if (hdn_Flag_New.Value == "Y")
            {
                Gv_PSUMaster.EditIndex = e.NewEditIndex;
                BindGrid("Y");
            }
            else
            {
                Gv_PSUMaster.EditIndex = e.NewEditIndex;
                BindGrid("N");
            }
        }

        protected void Gv_PSUMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int psuid = Convert.ToInt32(Gv_PSUMaster.DataKeys[e.RowIndex].Value);
                GridViewRow row = Gv_PSUMaster.Rows[e.RowIndex];
                psu = DVSC.PSU_MASTER.First(x => x.PSU_ID == psuid);
                psu.FLAG = "Y";
                if (!string.IsNullOrEmpty(psu.PSU_NAME) && !string.IsNullOrEmpty(psu.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_PSUMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in PSU.')</script>");
                    BindGrid(Flag);
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while deleting records please try later.')</script>");
            }
        }

        protected void Gv_PSUMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = Gv_PSUMaster.Rows[e.RowIndex];
                TextBox txtPSUName = row.FindControl("txtPSUName") as TextBox;
                TextBox txtflag = row.FindControl("txtFlag") as TextBox;
                int psuid = Convert.ToInt32(Gv_PSUMaster.DataKeys[e.RowIndex].Value);
                psu = DVSC.PSU_MASTER.First(x => x.PSU_ID == psuid);
                psu.PSU_NAME = txtPSUName.Text.Trim();
                psu.FLAG = txtflag.Text.Trim();
                if (!string.IsNullOrEmpty(psu.PSU_NAME) && !string.IsNullOrEmpty(psu.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_PSUMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in PSU.')</script>");
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
            int psuID =0; //DVSC.PSU_MASTER.Max(x => x.PSU_ID) + 1;
            DataSet ds = new DataSet();
            ds = ddl.get_data_from_DB("select top 1 A.[PSU_ID]+1 as [PSU_ID] from PSU_MASTER A where not exists (select * from PSU_MASTER B where B.[PSU_ID] = A.[PSU_ID]+1)");
            psuID = Convert.ToInt32(ds.Tables[0].Rows[0]["PSU_ID"]);
            try
            {
                if (!string.IsNullOrEmpty(txtpsu.Text.Trim()))
                {
                    psu.PSU_NAME = txtpsu.Text.Trim();
                    psu.FLAG = "N";
                    psu.PSU_FIRMFILENO = "P" + psuID;
                    psu.PSU_ID = psuID;
                    DVSC.PSU_MASTER.AddObject(psu);
                    DVSC.SaveChanges();
                    BindGrid(Flag);
                    ClearControl();
                    Response.Write("<script> alert('Record saved successfully. File number for PSU " + psu.PSU_NAME + " is " + psu.PSU_FIRMFILENO + "')</script>");
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in PSU.')</script>");
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Error while saving the record please try later.')</script>");
            }
        }

        public void ClearControl()
        {
            txtpsu.Text = "";
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

        protected void Gv_PSUMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Gv_PSUMaster.EditIndex = -1;
            BindGrid(Flag);
        }
    }
}