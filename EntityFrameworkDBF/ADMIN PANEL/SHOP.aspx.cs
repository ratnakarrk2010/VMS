using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EntityFrameworkDBF.ADMIN_PANEL
{
    public partial class SHOP : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        SHOP_MASTER shop = new SHOP_MASTER();
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
                if (DVSC.SHOP_MASTER.Count() > 0)
                {
                    Gv_ShopMaster.DataSource = DVSC.SHOP_MASTER.Where(x => x.FLAG == Flag);
                    Gv_ShopMaster.DataBind();
                }
                else
                {
                    Gv_ShopMaster.DataSource = null;
                    Gv_ShopMaster.DataBind();
                }
            }
            catch (Exception)
            {

                Response.Write("<script> alert('Error while loading data.')</script>");
            }
        }

        protected void Gv_ShopMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gv_ShopMaster.PageIndex = e.NewPageIndex;
            BindGrid(Flag);
        }

        protected void Gv_ShopMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Gv_ShopMaster.EditIndex = -1;
            BindGrid(Flag);
        }

        protected void Gv_ShopMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = (GridViewRow)Gv_ShopMaster.Rows[Convert.ToInt32(e.NewEditIndex)];
            TextBox txtFlag_obj = ((TextBox)row.Cells[0].FindControl("txtFlag"));
            HiddenField hdnFlag = Gv_ShopMaster.Rows[e.NewEditIndex].FindControl("hdn_Flag") as HiddenField;
            hdn_Flag_New.Value = hdnFlag.Value.Trim();
            if (hdn_Flag_New.Value == "Y")
            {
                Gv_ShopMaster.EditIndex = e.NewEditIndex;
                BindGrid("Y");
            }
            else
            {
                Gv_ShopMaster.EditIndex = e.NewEditIndex;
                BindGrid("N");
            }
        }

        protected void Gv_ShopMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int shopid = Convert.ToInt32(Gv_ShopMaster.DataKeys[e.RowIndex].Value);
                GridViewRow row = Gv_ShopMaster.Rows[e.RowIndex];
                shop = DVSC.SHOP_MASTER.First(x => x.SHOP_ID == shopid);
                shop.FLAG = "Y";
                if (!string.IsNullOrEmpty(shop.SHOP_NAME) && !string.IsNullOrEmpty(shop.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_ShopMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in SHOP.')</script>");
                    BindGrid(Flag);
                }
            }
            catch (Exception)
            {
                Response.Write("<script> alert('Error while deleting records please try later.')</script>");
            }
        }

        protected void Gv_ShopMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = Gv_ShopMaster.Rows[e.RowIndex];
                TextBox txtShopname = row.FindControl("txtShopName") as TextBox;
                TextBox txtflag = row.FindControl("txtFlag") as TextBox;
                TextBox txtFileNo = row.FindControl("txtFileNo") as TextBox;
                int shopid = Convert.ToInt32(Gv_ShopMaster.DataKeys[e.RowIndex].Value);
                shop = DVSC.SHOP_MASTER.First(x => x.SHOP_ID == shopid);
                shop.SHOP_NAME = txtShopname.Text.Trim();
                shop.FLAG = txtflag.Text.Trim();
                shop.FILENO = txtFileNo.Text.Trim();
                if (!string.IsNullOrEmpty(shop.SHOP_NAME) && !string.IsNullOrEmpty(shop.FLAG))
                {
                    DVSC.SaveChanges();
                    Gv_ShopMaster.EditIndex = -1;
                    BindGrid(Flag);
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in SHOP.')</script>");
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
            int ShopID = 0;//DVSC.SHOP_MASTER.Max(x => x.FIRM_ID) + 1;
            DataSet ds = new DataSet();
            ds = ddl.get_data_from_DB("select top 1 A.[SHOP_ID]+1 as [SHOP_ID] from SHOP_MASTER A where not exists (select * from SHOP_MASTER B where B.[SHOP_ID] = A.[SHOP_ID]+1)");
            ShopID = Convert.ToInt32(ds.Tables[0].Rows[0]["SHOP_ID"]);

            try
            {
                if (!string.IsNullOrEmpty(txtShop.Text.Trim()))
                {
                    shop.SHOP_NAME = txtShop.Text.Trim();
                    shop.FLAG = "N";
                    shop.FILENO = "Z" + ShopID;
                    shop.SHOP_ID = ShopID;
                    DVSC.SHOP_MASTER.AddObject(shop);
                    DVSC.SaveChanges();
                    BindGrid(Flag);
                    ClearControl();
                }
                else
                {
                    Response.Write("<script>alert('You cannot insert blank in SHOP.')</script>");
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Error while saving the record please try later.')</script>");
            }
        }

        public void ClearControl()
        {
            txtShop.Text = "";
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