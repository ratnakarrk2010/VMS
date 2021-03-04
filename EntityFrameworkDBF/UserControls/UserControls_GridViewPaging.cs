#region Assembly App_Web_ow4rlm4v.dll, v4.0.30319
// C:\Users\wnc-ndm-payroll2\AppData\Local\Temp\Temporary ASP.NET Files\webui\e0e7a5ab\_shadow\87a0a77c\3199498067\30640630\App_Web_ow4rlm4v.dll
#endregion

using ASP;
using System;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public class UserControls_GridViewPaging : UserControl
{
    protected DropDownList ddlPageSize;
    protected Button First;
    protected Label GridViewPagingError;
    protected Button Last;
    protected Label lblPageSize;
    protected Button Next;
    protected Label PageDisplaySummary;
    public EventHandler pagingClickArgs;
    protected Button Previous;
    protected Label RecordDisplaySummary;
    protected TextBox SelectedPageNo;
    protected HiddenField TotalRows;
    protected HtmlTableRow trErrorMessage;

    public UserControls_GridViewPaging();

    protected global_asax ApplicationInstance { get; }
    protected DefaultProfile Profile { get; }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e);
    protected void First_Click(object sender, EventArgs e);
    protected void Last_Click(object sender, EventArgs e);
    protected void Next_Click(object sender, EventArgs e);
    protected void Page_Load(object sender, EventArgs e);
    protected void Previous_Click(object sender, EventArgs e);
    protected void SelectedPageNo_TextChanged(object sender, EventArgs e);
}
