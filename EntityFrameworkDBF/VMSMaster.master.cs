using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;



public partial class VMSMaster : System.Web.UI.MasterPage
{

    //protected ParameterDetails stParameterDetails;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            //if (Session["UserID"] == null)
            //{
            //    Response.Redirect("Default.aspx");
            //}

            DataSet dsRecent = new DataSet();
            DataSet dsFavourite = new DataSet();
            DataSet dsAnnouncement = new DataSet();


            //Int32 intUserID;swap
            //intUserID = Convert.ToInt32(Session["UserID"].ToString());



            //          lblUserName.InnerText = Session["username"].ToString();


            //lblUserName.InnerText = Session["LoginName"].ToString();

            // lblUserName.InnerText = Session["EmployeeLogiName"].ToString();
            //GetFormAccess();

            //string Formname = Request.Url.AbsolutePath.Replace("/", "");
            //Session["FormName"] = Formname.Replace(".aspx", "");
            //Session["FormID"] = Common.GetFormID(Session["FormName"].ToString());

            ////--------------Start For RECENT---------------------------------

            //Int32 intFormID;
            //intFormID = Common.GetFormID(Session["FormName"].ToString());
            //intUserID = Convert.ToInt32(Session["UserID"].ToString());

            //// Common.InsertUpdateRecent(intUserID, intFormID, "~/" + Session["FormName"].ToString() + ".aspx");

            ////--------------End For RECENT---------------------------------
            //GetAgentTaskCounts();

            //Session["FavUrl"] = Request.Url.ToString();
            // btnFav1.Attributes.Add("onclick", "javascript:return Openfavourite('FavSet.aspx','0');");
        }
        catch(Exception ee)
        {
            //Response.Redirect("~/errorpage.aspx");

        }
        if(!IsPostBack)
        {
            GetFormAccess();
        }
    }

    public void GetFormAccess()
    {
        //AccessRights- Start************************************************************************************************
        DataSet dsForms = new DataSet();
        String FormCaption;
        String LoginUser;
        Int32 FormShowCount;
        Int32 formCount;

        // FormCaption = InputFormName;
        FormShowCount = 0;


        //Get the Form Name Whcich the User Have Rights 
        //DataSet dsFormAccess = new DataSet();
        //dsFormAccess = GetFormAccessRights();

        //for (int i = 0; i < dsFormAccess.Tables[0].Rows.Count; i++)
        //{
        try
        {
            System.Web.UI.HtmlControls.HtmlGenericControl div1 = (System.Web.UI.HtmlControls.HtmlGenericControl)this.FindControl("DivDISPATCHAllocation2");

            if(Session["roletypeid"].ToString() == "1")
            {
                //aHome.HRef = "~/Admin/Home.aspx";
                //aHome.Visible = true;
                //aMaster.Visible = true;
                //aUserMstr.Visible = true;
                //aFirmMstr.Visible = false;
                //aHqAdmin.Visible = false;

            }
            else if(Session["roletypeid"].ToString() == "3")
            {
                //liReports.Visible = true;
                //liMaster.Visible = false;
                //li1.Visible = false;
                //li2.Visible = true;
                //li5.Visible = false;
                //liDispatch.Visible = false;
                //DivDISPATCHAllocation2.Visible = false;
                //liSettings.Visible = true;
                //li4.Visible = false;
                //li3.Visible = false;
                //li6.Visible = false;
                //li7.Visible = false;
                //li9.Visible = false;
                //aOther.Visible = true;
                //aContrctPass.Visible = true;
                //aCasualPass.Visible = true;
                //aTempPass.Visible = true;
                //aContrctID.Visible = true;
            }

            else if(Session["roletypeid"].ToString() == "2")
            {
                //aHome.HRef = "Operator_Home.aspx";
                //aHome.Visible = true;

                //aPasses.Visible = true;
                //aVistorPass.Visible = false;
                //aLabourPass.Visible = true;
                //aContrctPass.Visible = true;
                //aCasualPass.Visible = true;
                //aTempPass.Visible = true;
                //aContrctID.Visible = true;
                //aDlyVisPass.Visible = true;
                //aReprint.Visible = true;

                //aUpdate.Visible = true;
                //aSearch.Visible = true;
                //aSrchVPass.Visible = false;
                //aSrchContrct.Visible = false;
                //aSrchContID.Visible = false;

                //aReports.Visible = true;
                //aVisSumRpt.Visible = true;
                //aContSumRpt.Visible = false;
                //aContIDSumRpt.Visible = false;
                //aCasSumRpt.Visible = false;
                //aTempSumRpt.Visible = false;
                //aIORpt.Visible = false;

                //aIO.Visible = true;
                //aVistIO.Visible = true;
                //aLabourIO.Visible = false;
            }
            else if(Session["roletypeid"].ToString() == "4")
            {
                //aHome.HRef = "~/Commander/CommanderHome.aspx";
                //aHome.Visible = true;
                //aReports.Visible = false;
                //aVisSumRpt.Visible = true;
                //aContSumRpt.Visible = true;
                //aContIDSumRpt.Visible = true;
                //aCasSumRpt.Visible = true;
                //aTempSumRpt.Visible = true;
                //aIORpt.Visible = true;
                //aPasses.Visible = true;
                ////aVistorPass.Visible = true;
                //aDlyVisPass.Visible = true;
                // aConfHallBook.Visible = false;
            }
            else if(Session["roletypeid"].ToString() == "5")
            {
                //aHome.Visible = true;
                //aHome.HRef = "~/LPM/LPMHome.aspx";

                //aReports.Visible = false;

                //aContSumRpt.Visible = true;
                //aContIDSumRpt.Visible = true;
                //aContIDSumRpt.Visible = true;
                //aCasSumRpt.Visible = true;
                //aTempSumRpt.Visible = true;
                //aIORpt.Visible = true;
            }
            else if(Session["roletypeid"].ToString() == "6")
            {
                //aHome.Visible = true;
                //aHome.HRef = "~/DM/DMHome.aspx";

                //aPasses.Visible = true;
                //aDlyVisPass.Visible = true;
                //aDlyLabourPass.Visible = false;
                ////aConfHallBook.Visible = true;
                //aReports.Visible = false;
                //aContSumRpt.Visible = true;
                //aContIDSumRpt.Visible = true;
                //aIORpt.Visible = true;

            }
            else
            {
                //li3.Visible = true;
            }

        }
        catch(Exception Ex)
        {

        }      // }
    }

    private DataSet GetFormAccessRights()
    {
        DataSet dsForms = new DataSet();
        try
        {
            //using (Project.Db.DbConnection db = new Project.Db.DbConnection())
            //{
            //    ParameterList.AddParameter.Clear();

            //    stParameterDetails.Value = Session["RoleID"];
            //    stParameterDetails.DataType = SqlDbType.VarChar;
            //    //stParameterDetails.Value = Session["RoleID"];
            //    //stParameterDetails.DataType = DbType.Int32;
            //    ParameterList.AddParameter.Add("RoleID", stParameterDetails);
            //    dsForms = db.CommonCollection.GetAsDataSet("[dbo].[GetForm_AccessDetails]", ParameterList.AddParameter);

            //    if (dsForms.Tables[0].Rows.Count < 0)
            //    {
            //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "click", "alert('User have no Rights');", true);
            //    }
            //}
        }
        catch(Exception Ex)
        {

        }

        return dsForms;


    }

    protected void GvAnnouncement_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink LnkView = (HyperLink)e.Row.FindControl("HyperLink2");
                LnkView.Attributes.Add("onclick", "javascript:return OpenAnnoucementlisting('AnnouncementListing.aspx?ID=" + (e.Row.DataItem as DataRowView).Row["AnnouncementID"].ToString() + "');");
            }
            e.Row.Cells[0].Visible = false;
        }
        catch(Exception ex)
        {
            // lblErrorMessage.InnerHtml = Logger.ErrorLogFile(ex, "Error occured");
        }
    }

    protected void gvRecent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
            }
            e.Row.Cells[0].Visible = false;
        }
        catch(Exception ex)
        {
            // lblErrorMessage.InnerHtml = Logger.ErrorLogFile(ex, "Error occured");
        }
    }


    protected void gvRecent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //ParameterList.AddParameter.Clear();

            if(e.CommandName == "EditRecent")
            {
            }
        }
        catch(Exception ex)
        {
            //lblErrorMessage.InnerHtml = Logger.ErrorLogFile(ex, "Error occured");
        }
    }

    protected void gvFavourite_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
            }
            e.Row.Cells[0].Visible = false;

        }
        catch(Exception ex)
        {
            // lblErrorMessage.InnerHtml = Logger.ErrorLogFile(ex, "Error occured");
        }
    }


    protected void gvFavourite_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //ParameterList.AddParameter.Clear();

            if(e.CommandName == "EditFav")
            {
            }

        }


        catch(Exception ex)
        {
            //lblErrorMessage.InnerHtml = Logger.ErrorLogFile(ex, "Error occured");
        }


    }

    //protected void btnFav_Click(object sender, EventArgs e)
    //{
    //    Session["FavUrl"] = Request.Url.ToString();
    //    string Formname = Request.Url.ToString();
    //    Session["FormName"] = Formname.Replace(".aspx", "");
    //    //Session["FavUrl"]
    //    //Load Favourites
    //    DataSet dsFavourite = Common.FavouriteBindActivity(Convert.ToInt32(Session["UserID"]));

    //    //if (dsFavourite.Tables[0].Rows.Count > 0)
    //    //{
    //    //    gvFavourite.DataSource = dsFavourite;
    //    //    gvFavourite.DataBind();
    //    //}
    //}

    #region "Get Agent Pending Counts"
    //public void GetAgentTaskCounts()
    //{
    //    DataTable dtCompanyDetails = new DataTable();

    //    using (Project.Db.DbConnection db = new Project.Db.DbConnection())
    //    {
    //        ParameterList.AddParameter.Clear();
    //        stParameterDetails.Value = Session["UserID"];
    //        stParameterDetails.DataType = SqlDbType.Int;
    //        ParameterList.AddParameter.Add("UserID", stParameterDetails);

    //        stParameterDetails.Value = Session["FormID"];
    //        stParameterDetails.DataType = SqlDbType.Int;
    //        ParameterList.AddParameter.Add("FormID", stParameterDetails);

    //        dtCompanyDetails = db.CommonCollection.GetAsDataTable("Get_AssignPendingCountByAgentTask", ParameterList.AddParameter);
    //        if (dtCompanyDetails.Rows.Count > 0)
    //        {
    //            string strassign = Convert.ToString(dtCompanyDetails.Rows[0]["Complete"]).Length == 0 ? "0" : Convert.ToString(dtCompanyDetails.Rows[0]["Complete"]);
    //            string strpending = Convert.ToString(dtCompanyDetails.Rows[0]["Pending"]).Length == 0 ? "0" : Convert.ToString(dtCompanyDetails.Rows[0]["Pending"]);
    //            //if (Session["FormID"].ToString() == "21" | Session["FormID"].ToString() == "20")
    //            //{
    //            //    lblAssign.Text = "Complete (" + strassign + ")";
    //            //    lblComplete.Text = "Assign (" + strpending + ")";
    //            //}
    //            //else
    //            //{
    //            //    lblAssign.Text = "Complete (" + strassign + ")";
    //            //    lblComplete.Text = "Pending (" + strpending + ")";
    //            //}
    //        }
    //        //else
    //        //{
    //        //    lblAssign.Text = "Complete (" + "0" + ")";// Convert.ToString(dtCompanyDetails.Rows[0]["Complete"]).Length == 0 ? "0" : Convert.ToString(dtCompanyDetails.Rows[0]["Complete"]);
    //        //    lblComplete.Text = "Pending (" + "0" + ")";// Convert.ToString(dtCompanyDetails.Rows[0]["Pending"]).Length == 0 ? "0" : Convert.ToString(dtCompanyDetails.Rows[0]["Pending"]);
    //        //}
    //    }

    //}
    #endregion

    protected void btnFav1_Click(object sender, ImageClickEventArgs e)
    {
        Session["FavUrl"] = Request.Url.ToString();
        string Formname = Request.Url.ToString();
        Session["FormName"] = Formname.Replace(".aspx", "");
        //Session["FavUrl"]
        //Load Favourites
        //DataSet dsFavourite = Common.FavouriteBindActivity(Convert.ToInt32(Session["UserID"]));

        //if (dsFavourite.Tables[0].Rows.Count > 0)
        //{
        //    gvFavourite.DataSource = dsFavourite;
        //    gvFavourite.DataBind();
        //}

    }
    protected void lbtLogOut_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        //Response.Redirect("login.aspx", false);
        Response.Redirect("~/Login.aspx");
    }
}


