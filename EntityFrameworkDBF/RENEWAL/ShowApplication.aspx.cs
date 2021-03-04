using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityFrameworkDBF.RENEWAL
{
    public partial class ShowApplication : System.Web.UI.Page
    {
        int AppID = 0;
        int PDF = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            MaintainScrollPositionOnPostBack = true;
            if (!IsPostBack)
            {
                spnDate.InnerText = DateTime.Now.Date.ToString("dd-MMM-yyyy").Replace("-", " ").ToUpper();
                GetApplicationImage();
            }

        }

        public void GetApplicationImage()
        {
            try
            {
                AppID = Convert.ToInt32(Session["APPID"]);
                lblGetAppNo.Text = Session["APPNO"].ToString();
                PDF = Convert.ToInt32(Session["PDF"]);
                
                if (PDF == 0)
                {
                    imgApplication.ImageUrl = "~/GetApplicationData.ashx?Appid=" + AppID;
                }
                else
                {
                    //imgApplication.ImageUrl = "~/GetApplicationData.ashx?Appid=" + AppID + "&PDF=1";
                    string embed = "<object data=\"{0}{1}\" type=\"application/pdf\" width=\"500px\" height=\"300px\">";
                    embed += "If you are unable to view file, you can download from <a href = \"{0}{1}&download=1\">here</a>";
                    embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                    embed += "</object>";
                    ltEmbed.Text = string.Format(embed, ResolveUrl("~/GetApplicationData.ashx?Appid=" + AppID + "&PDF=1"), AppID);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}