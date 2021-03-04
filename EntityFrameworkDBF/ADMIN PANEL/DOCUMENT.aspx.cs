using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityFrameworkDBF.ADMIN_PANEL
{
    public partial class DOCUMENT : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        DOCUMENT_MASTER document = new DOCUMENT_MASTER();
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
                if (DVSC.DOCUMENT_MASTER.Count() > 0)
                {
                    Gv_DocumentMaster.DataSource = DVSC.DOCUMENT_MASTER.Where(x => x.FLAG == Flag);
                    Gv_DocumentMaster.DataBind();
                }
                else
                {
                    Gv_DocumentMaster.DataSource = null;
                    Gv_DocumentMaster.DataBind();
                }
            }
            catch (Exception)
            {

                Response.Write("<script> alert('Error while loading data.')</script>");
            }
        }

        protected void Gv_DocumentMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gv_DocumentMaster.PageIndex = e.NewPageIndex;
            BindGrid(Flag);
        }

        protected void Gv_DocumentMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Gv_DocumentMaster.EditIndex = -1;
            BindGrid(Flag);
        }

        protected void Gv_DocumentMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = (GridViewRow)Gv_DocumentMaster.Rows[Convert.ToInt32(e.NewEditIndex)];
            TextBox txtFlag_obj = ((TextBox)row.Cells[0].FindControl("txtFlag"));
            HiddenField hdnFlag = Gv_DocumentMaster.Rows[e.NewEditIndex].FindControl("hdn_Flag") as HiddenField;
            hdn_Flag_New.Value = hdnFlag.Value.Trim();
            if (hdn_Flag_New.Value == "Y")
            {
                Gv_DocumentMaster.EditIndex = e.NewEditIndex;
                BindGrid("Y");
            }
            else
            {
                Gv_DocumentMaster.EditIndex = e.NewEditIndex;
                BindGrid("N");
            }
        }

        protected void Gv_DocumentMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int docid = Convert.ToInt32(Gv_DocumentMaster.DataKeys[e.RowIndex].Value);
                GridViewRow row = Gv_DocumentMaster.Rows[e.RowIndex];
                document = DVSC.DOCUMENT_MASTER.First(x => x.DOCUMENT_ID == docid);
                document.FLAG = "Y";
                if (!string.IsNullOrEmpty(document.DOCUMENT_NAME) && !string.IsNullOrEmpty(document.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_DocumentMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in DOCUMENT.')</script>");
                    BindGrid(Flag);
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while deleting records please try later.')</script>");
            }
        }

        protected void Gv_DocumentMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = Gv_DocumentMaster.Rows[e.RowIndex];
                TextBox txtDocname = row.FindControl("txtDocumentName") as TextBox;
                TextBox txtflag = row.FindControl("txtFlag") as TextBox;
                int docid = Convert.ToInt32(Gv_DocumentMaster.DataKeys[e.RowIndex].Value);
                document = DVSC.DOCUMENT_MASTER.First(x => x.DOCUMENT_ID == docid);
                document.DOCUMENT_NAME = txtDocname.Text.Trim();
                document.FLAG = txtflag.Text.Trim();
                if (!string.IsNullOrEmpty(document.DOCUMENT_NAME) && !string.IsNullOrEmpty(document.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_DocumentMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in DOCUMENT.')</script>");
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
                if (!string.IsNullOrEmpty(txtdocumnet.Text.Trim()))
                {
                    document.DOCUMENT_NAME = txtdocumnet.Text.Trim();
                    document.FLAG = "N";
                    DVSC.DOCUMENT_MASTER.AddObject(document);
                    DVSC.SaveChanges();
                    BindGrid(Flag);
                    ClearControl();
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in DOCUMENT.')</script>");
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Error while saving the record please try later.')</script>");
            }
        }

        public void ClearControl()
        {
            txtdocumnet.Text = "";
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