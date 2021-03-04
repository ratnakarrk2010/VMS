using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityFrameworkDBF.ADMIN_PANEL
{
    public partial class COUNTY : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        COUNTRY_MASTER country = new COUNTRY_MASTER();
        string Flag = "N";
        protected void Page_Load(object sender, EventArgs e)
        {
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
                if(DVSC.COUNTRY_MASTER.Count() > 0)
                {
                    Gv_CountryMaster.DataSource = DVSC.COUNTRY_MASTER.Where(x => x.FLAG == Flag);
                    Gv_CountryMaster.DataBind();
                }
                else
                {
                    Gv_CountryMaster.DataSource = null;
                    Gv_CountryMaster.DataBind();
                }
            }
            catch(Exception)
            {

                Response.Write("<script> alert('Error while loading data.')</script>");
            }
        }

        protected void Gv_CountryMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gv_CountryMaster.PageIndex = e.NewPageIndex;
            BindGrid(Flag);
        }

        protected void Gv_CountryMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Gv_CountryMaster.EditIndex = -1;
            BindGrid(Flag);
        }

        protected void Gv_CountryMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = (GridViewRow)Gv_CountryMaster.Rows[Convert.ToInt32(e.NewEditIndex)];
            TextBox txtFlag_obj = ((TextBox)row.Cells[0].FindControl("txtFlag"));
            HiddenField hdnFlag = Gv_CountryMaster.Rows[e.NewEditIndex].FindControl("hdn_Flag") as HiddenField;
            hdn_Flag_New.Value = hdnFlag.Value.Trim();
            if(hdn_Flag_New.Value == "Y")
            {
                Gv_CountryMaster.EditIndex = e.NewEditIndex;
                BindGrid("Y");
            }
            else
            {
                Gv_CountryMaster.EditIndex = e.NewEditIndex;
                BindGrid("N");
            }
        }

        protected void Gv_CountryMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int countryid = Convert.ToInt32(Gv_CountryMaster.DataKeys[e.RowIndex].Value);
                GridViewRow row = Gv_CountryMaster.Rows[e.RowIndex];
                country = DVSC.COUNTRY_MASTER.First(x => x.COUNTRY_ID == countryid);
                country.FLAG = "Y";
                if(!string.IsNullOrEmpty(country.COUNTRY_NAME) && !string.IsNullOrEmpty(country.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_CountryMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in country.')</script>");
                    BindGrid(Flag);
                }
            }
            catch(Exception)
            {
                Response.Write("<script> alert('Error while deleting records please try later.')</script>");
            }
        }

        protected void Gv_CountryMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = Gv_CountryMaster.Rows[e.RowIndex];
                TextBox txtcountryname = row.FindControl("txtcountryname") as TextBox;
                TextBox txtNation = row.FindControl("txtNation") as TextBox;
                TextBox txtflag = row.FindControl("txtFlag") as TextBox;
                int countryid = Convert.ToInt32(Gv_CountryMaster.DataKeys[e.RowIndex].Value);
                country = DVSC.COUNTRY_MASTER.First(x => x.COUNTRY_ID == countryid);
                country.COUNTRY_NAME = txtcountryname.Text.Trim();
                country.NATIONALITY = txtNation.Text.Trim();
                country.FLAG = txtflag.Text.Trim();
                if(!string.IsNullOrEmpty(country.COUNTRY_NAME) && !string.IsNullOrEmpty(country.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_CountryMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in country.')</script>");
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
                if(!string.IsNullOrEmpty(txtCountry.Text.Trim()))
                {
                    country.COUNTRY_NAME = txtCountry.Text.Trim();
                    country.NATIONALITY = txtNationality.Text.Trim();
                    country.FLAG = "N";
                    DVSC.COUNTRY_MASTER.AddObject(country);
                    DVSC.SaveChanges();
                    BindGrid(Flag);
                    ClearControl();
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in country.')</script>");
                }
            }
            catch(Exception)
            {
                Response.Write("<script>alert('Error while saving the record please try later.')</script>");
            }
        }

        public void ClearControl()
        {
            txtCountry.Text = "";
            txtNationality.Text = "";
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