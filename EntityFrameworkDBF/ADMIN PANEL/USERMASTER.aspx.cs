using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityFrameworkDBF.ADMIN_PANEL
{
    public partial class USERMASTER : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        USER_MASTER _USERMASTER = new USER_MASTER();
        DropDownFunction ddl = new DropDownFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if(!IsPostBack)
            {
                spnDate.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy").Replace("-", " ").ToUpper();
                //BindGriDForUserMaster();
                BindGrid();
                ddl.BindRoleddl(ref ddlroletype);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            String Rights = "";
            for (int i = 0; i <= chkRights.Items.Count - 1; i++)
            {
                if (chkRights.Items[i].Selected)
                {
                    if (Rights == "")
                    {
                        Rights = chkRights.Items[i].Value;
                    }
                    else
                    {
                        Rights += "," + chkRights.Items[i].Value;
                    }
                }
            }
            _USERMASTER.USERNAME = txtUsertName.Text;
            _USERMASTER.PASSWORD = txtPassword.Text;
            _USERMASTER.EMPTOKEN = txtEmployeeCode.Text;
            _USERMASTER.ROLETYPEID = Convert.ToInt32(ddlroletype.SelectedItem.Value);
            _USERMASTER.STATUS = txtStatus.Text;
            _USERMASTER.ISACTIVE = rdbActivation.SelectedValue;
            //_USERMASTER.MENUID = txtMenuid.Text;
            _USERMASTER.APPLICATIONRIGHTS = Rights;
            //_USERMASTER.DEPARTMENTID = Convert.ToInt32(txtDeptid.Text);
            //_USERMASTER.DEPARTMENTNAME = txtDeptName.Text;
            _USERMASTER.CREATEDBY = "110.10";
            _USERMASTER.CREATIONDATE = DateTime.Now;
            _USERMASTER.UPDATEDATE = DateTime.Now;
            DVSC.USER_MASTER.AddObject(_USERMASTER);
            DVSC.SaveChanges();
            //BindGriDForUserMaster(_USERMASTER.USERID);
            BindGrid();
        }

        public void BindGriDForUserMaster(int id)
        {
            //Flag = "N";
            try
            {
                if(DVSC.USER_MASTER.Count() > 0)
                {
                    Gv_Users.DataSource = DVSC.USER_MASTER.Where(x => x.USERID == id);
                    Gv_Users.DataBind();
                }
                else
                {
                    Gv_Users.DataSource = null;
                    Gv_Users.DataBind();
                }
            }
            catch(Exception)
            {
                Response.Write("<script> alert('Error while loading data.')</script>");
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

        public void BindGrid()
        {
            try
            {
                if(DVSC.USER_MASTER.Count() > 0)
                {
                    Gv_Users.DataSource = DVSC.USER_MASTER;
                    Gv_Users.DataBind();
                }
                else
                {
                    Gv_Users.DataSource = null;
                    Gv_Users.DataBind();
                }
            }
            catch(Exception)
            {
                Response.Write("<script> alert('Error while loading data.')</script>");
            }
        }
    }
}