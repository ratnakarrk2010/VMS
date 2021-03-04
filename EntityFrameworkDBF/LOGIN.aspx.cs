using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityFrameworkDBF
{
    public partial class LOGIN : System.Web.UI.Page
    {
        VMSEntities DVSC = new VMSEntities();
        USER_MASTER OBJUSERS = new USER_MASTER();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OpenFormMenu(string loginUrl)
        {
            try
            {
                if (Session["LogoutFlag"] == null)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "OpenMainMenu-function", "<script language=" + "JavaScript" + ">" +
                           "function OpenMainMenu() {" +
                           "window.opener='x';window.close();" +
                           "mywindow=window.open('" + loginUrl + "','mywindow','width=screen.width,height=screen.height,screenX=0,screenY=0,left=0,top=0,,,,status=yes,scrollbars=NO,,copyhistory=yes,resizable=yes');mywindow.moveTo(0,0);mywindow.resizeTo(screen.availWidth,screen.availHeight);" +
                           "}</script>");
                    ClientScript.RegisterStartupScript(this.GetType(), "OpenMainMenu", "<script language=" + "JavaScript" + ">OpenMainMenu();</script>");
                }
                else
                {
                    //Response.Redirect("MasterPage.aspx", false);
                    if (loginUrl == "ChangePassword.aspx")
                        Response.Redirect(loginUrl + "?Msg=UserName and Password cannot be same.Please Change your Password.", false);
                    else
                        Response.Redirect(loginUrl, false);
                }
            }
            catch (Exception ex)
            {
                FailureText.Text = "Session Problem ." + " " + ex.Message;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // WETOS.DataEntity.LoginUserAccessRights objUser = new WETOS.DataEntity.LoginUserAccessRights();
            // CryptorEngine CryptorEngine = new CryptorEngine();

            bool isLicensed = true;//CryptorEngine.ReadIni(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["VirtualDir"] + "/App_Data/WETOS.ini"));
            try
            {
                if (isLicensed)//&& CryptorEngine.ValidateWetos(ref FailureText))
                {
                    if (UserName.Text == "admin" && Password.Text == "admin")
                    {
                        Session["Flag"] = "TRUE";
                        Session["LogoutFlag"] = "TRUE";
                        OBJUSERS.USERNAME = UserName.Text.Trim();
                        OBJUSERS.PASSWORD = "admin";
                        Session["USERNAME"] = UserName.Text.Trim();
                        string loginUrl = "DVSC_HOME.aspx";
                        OpenFormMenu(loginUrl);
                    }
                    else if (UserName.Text == "deo" && Password.Text == "deo")
                    {
                        Session["Flag"] = "TRUE";
                        Session["LogoutFlag"] = "TRUE";
                        OBJUSERS.USERNAME = UserName.Text.Trim();
                        OBJUSERS.PASSWORD = "deo";
                        Session["USERNAME"] = UserName.Text.Trim();
                        string loginUrl = "DVSC_HOME.aspx";
                        OpenFormMenu(loginUrl);
                    }
                    else if (UserName.Text == "deo1" && Password.Text == "deo1")
                    {
                        Session["Flag"] = "TRUE";
                        Session["LogoutFlag"] = "TRUE";
                        OBJUSERS.USERNAME = UserName.Text.Trim();
                        OBJUSERS.PASSWORD = "deo1";
                        Session["USERNAME"] = UserName.Text.Trim();
                        string loginUrl = "DVSC_HOME.aspx";
                        OpenFormMenu(loginUrl);
                    }
                    else if (UserName.Text == "deo2" && Password.Text == "deo2")
                    {
                        Session["Flag"] = "TRUE";
                        Session["LogoutFlag"] = "TRUE";
                        OBJUSERS.USERNAME = UserName.Text.Trim();
                        OBJUSERS.PASSWORD = "deo2";
                        Session["USERNAME"] = UserName.Text.Trim();
                        string loginUrl = "DVSC_HOME.aspx";
                        OpenFormMenu(loginUrl);
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(FailureText.Text))
                    {
                        // FailureText.Text = Messages.ErrorDisplay(705);
                    }
                }
            }
            catch (Exception ex)
            {
                FailureText.Text = "Session Problem ." + " " + ex.Message;
            }
        }

    }
}