using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EntityFrameworkDBF.ADMIN_PANEL
{
    public partial class FIRM : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        FIRMMASTER firm = new FIRMMASTER();
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
                if (DVSC.FIRMMASTERs.Count() > 0)
                {
                    Gv_FirmMaster.DataSource = DVSC.FIRMMASTERs.Where(x => x.FLAG == Flag);
                    Gv_FirmMaster.DataBind();
                }
                else
                {
                    Gv_FirmMaster.DataSource = null;
                    Gv_FirmMaster.DataBind();
                }
            }
            catch (Exception)
            {

                Response.Write("<script> alert('Error while loading data.')</script>");
            }
        }

        protected void Gv_FirmMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gv_FirmMaster.PageIndex = e.NewPageIndex;
            BindGrid(Flag);
        }

        protected void Gv_FirmMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Gv_FirmMaster.EditIndex = -1;
            BindGrid(Flag);
        }

        protected void Gv_FirmMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = (GridViewRow)Gv_FirmMaster.Rows[Convert.ToInt32(e.NewEditIndex)];
            //TextBox txtFile = (TextBox)Gv_FirmMaster.Rows[e.NewEditIndex].FindControl("txtFirmFile"); //row.FindControl("txtFirmFile") as TextBox;
            TextBox txtFlag_obj = ((TextBox)row.Cells[0].FindControl("txtFlag"));
            HiddenField hdnFlag = Gv_FirmMaster.Rows[e.NewEditIndex].FindControl("hdn_Flag") as HiddenField;
            hdn_Flag_New.Value = hdnFlag.Value.Trim();
            //txtFile.Enabled = false;
            if (hdn_Flag_New.Value == "Y")
            {
                Gv_FirmMaster.EditIndex = e.NewEditIndex;
                BindGrid("Y");
                //txtFile.Enabled = false;
            }
            else
            {
                Gv_FirmMaster.EditIndex = e.NewEditIndex;
                BindGrid("N");
                //txtFile.Enabled = false;
            }
        }

        protected void Gv_FirmMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int firmid = Convert.ToInt32(Gv_FirmMaster.DataKeys[e.RowIndex].Value);
                GridViewRow row = Gv_FirmMaster.Rows[e.RowIndex];
                firm = DVSC.FIRMMASTERs.First(x => x.FIRM_ID == firmid);
                firm.FLAG = "Y";
                if (!string.IsNullOrEmpty(firm.FIRM_NAME) && !string.IsNullOrEmpty(firm.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_FirmMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in FIRM.')</script>");
                    BindGrid(Flag);
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while deleting records please try later.')</script>");
            }
        }

        protected void Gv_FirmMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = Gv_FirmMaster.Rows[e.RowIndex];
                TextBox txtFirm = row.FindControl("txtFirmName") as TextBox;
                TextBox txtFirmGst = row.FindControl("txtFirmGst") as TextBox;
                TextBox txtflag = row.FindControl("txtFlag") as TextBox;
                TextBox txtProp = row.FindControl("txtFirmPropName") as TextBox;
                TextBox txtFirmAdd = row.FindControl("txtFirmAddress") as TextBox;
                TextBox txtFile = row.FindControl("txtFirmFile") as TextBox;
                int firmid = Convert.ToInt32(Gv_FirmMaster.DataKeys[e.RowIndex].Value);
                firm = DVSC.FIRMMASTERs.First(x => x.FIRM_ID == firmid);
                firm.FIRM_NAME = txtFirm.Text.Trim();
                firm.FIRM_GST = txtFirmGst.Text.Trim();
                firm.FLAG = txtflag.Text.Trim();
                firm.FIRM_PROPRITER = txtProp.Text.Trim();
                firm.FIRM_ADDRESS = txtFirmAdd.Text.Trim();
                txtFile.Enabled = false;
                if (!string.IsNullOrEmpty(firm.FIRM_NAME) && !string.IsNullOrEmpty(firm.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_FirmMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in FIRM.')</script>");
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
            var count = DVSC.FIRMMASTERs.Count(x => x.FIRM_GST == txtgst.Text.Trim() && x.FIRM_GST != "NA" && x.FIRM_GST.Length>0);
            if (count > 0)
            {
                Response.Write("<script> alert('Firm Is already registered with this gst number.')</script>");
            }
            else
            {
                DataSet ds = new DataSet();
                int FirmID = 0;//DVSC.FIRMMASTERs.Max(x => x.FIRM_ID) + 1;
                ds = ddl.get_data_from_DB("select top 1 A.[FIRM_ID]+1 as [FIRM_ID] from FIRMMASTER A where not exists (select * from FIRMMASTER B where B.[FIRM_ID] = A.[FIRM_ID]+1)");
                FirmID = Convert.ToInt32(ds.Tables[0].Rows[0]["FIRM_ID"]);

                try
                {
                    if (!string.IsNullOrEmpty(txtFirm.Text.Trim()))
                    {
                        firm.FIRM_NAME = txtFirm.Text.Trim();
                        firm.FIRM_GST = txtgst.Text.Trim();
                        firm.FIRM_FILE_NO = "F" + FirmID;
                        firm.FLAG = "N";
                        firm.FIRM_PROPRITER = txtPropName.Text.Trim();
                        firm.FIRM_ADDRESS = txtFirmAddress.Text.Trim();
                        firm.FIRM_ID = FirmID;
                        DVSC.FIRMMASTERs.AddObject(firm);
                        DVSC.SaveChanges();
                        //lblFirmFileNo.Text = firm.FIRM_FILE_NO;
                        BindGrid(Flag);
                        ClearControl();
                        Response.Write("<script> alert('Record saved successfully. File number for firm " + firm.FIRM_NAME + " is " + firm.FIRM_FILE_NO + "')</script>");

                    }
                    else
                    {
                        Response.Write("<script>alert('You cannot insert blank in FIRM.')</script>");
                    }
                }
                catch (Exception)
                {
                    Response.Write("<script>alert('Error while saving the record please try later.')</script>");
                }
            }
        }

        public void ClearControl()
        {
            txtFirm.Text = "";
            txtgst.Text = "";
            txtFirmAddress.Text = "";
            txtPropName.Text = "";
            //lblFirmFileNo.Text = "";
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var GetFirm = (from x in DVSC.FIRMMASTERs
                               where (x.FIRM_NAME.Contains(txtSearch.Text.Trim()) ||
                                   x.FIRM_FILE_NO.Contains(txtSearch.Text.Trim()) ||
                                   x.FIRM_PROPRITER.Contains(txtSearch.Text.Trim()) ||
                                   x.FIRM_GST.Contains(txtSearch.Text.Trim()))
                               select x);
                if (GetFirm.Count() > 0)
                {
                    Gv_FirmMaster.DataSource = GetFirm;
                    Gv_FirmMaster.DataBind();
                    txtSearch.Text = "";
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while searching record.')</script>");
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGrid("N");
        }
    }
}