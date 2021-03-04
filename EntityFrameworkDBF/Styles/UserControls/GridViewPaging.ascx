<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridViewPaging.ascx.cs" Inherits="UserControls_GridViewPaging" %>

<style type="text/css">
    .navigationButton
    {
       
        border-bottom-color: #333;
        border: 1px solid #ffffff;
        background-color: #507cd1;
       /* border-radius: 5px;
        -moz-border-radius: 5px;
        -webkit-border-radius: 5px;
        text-shadow: #b2e2f5 0 1px 0;
         -webkit-box-shadow: rgba(0,0,0,0.2) 0 1px 0 0;
        -moz-box-shadow: rgba(0,0,0,0.2) 0 1px 0 0;
        box-shadow: rgba(0,0,0,0.2) 0 1px 0 0;
        */
        color: #ffffff;
        font-weight:bolder;
        font-family: 'Verdana' ,Arial,sans-serif;
        font-size: 13px;
        
        padding: 0px;
        cursor: pointer;
    }
    .tablePaging
    {
        font-family: "Trebuchet MS" , Arial, Helvetica, sans-serif;
        border-collapse: collapse;
    }
    .tablePaging td
    {
      
        border: 1px solid #ffffff;
        padding: 0px 7px 0px 7px;
        background-color: #507cd1;
        font-size: 10pt;
         color:White
    }
</style>
<table class="tablePaging" align="center" width="100%" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td style=" text-align: center; vertical-align:top;" > 
          <asp:Label ID="lblPageSize" runat="server" Text="Page Size: "></asp:Label> </td>
        <td style=" text-align: center; vertical-align:top;" >
          
            <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                <asp:ListItem Selected="True">10</asp:ListItem>
                <asp:ListItem>20</asp:ListItem>
                <asp:ListItem>50</asp:ListItem>
                <asp:ListItem>100</asp:ListItem>
               <asp:ListItem>500</asp:ListItem>
                <asp:ListItem>1000</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td style=" text-align: center; vertical-align:top;">
            <asp:Label ID="RecordDisplaySummary" runat="server"></asp:Label>
        </td>
        <td style=" text-align: center;vertical-align:top;">
            <asp:Label ID="PageDisplaySummary" runat="server"></asp:Label>
        </td>
        <td style=" text-align: center;">
            <asp:Button ID="First" runat="server" Text="<<" Width="45px" OnClick="First_Click"
                CssClass="navigationButton" />&nbsp;
            <asp:Button ID="Previous" runat="server" Text="<" Width="45px" OnClick="Previous_Click"
                CssClass="navigationButton" />&nbsp;
            <asp:TextBox ID="SelectedPageNo" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                BorderWidth="1px" Font- Names="Verdana" Font-Size="Small" OnTextChanged="SelectedPageNo_TextChanged"
                Width="100px" AutoPostBack="True" MaxLength="8"></asp:TextBox>&nbsp;
            <asp:Button ID="Next" runat="server" Text=">" Width="45px" OnClick="Next_Click" CssClass="navigationButton" />&nbsp;
            <asp:Button ID="Last" runat="server" Text=">>" Width="45px" OnClick="Last_Click"
                CssClass="navigationButton" />&nbsp;
        </td>
    </tr>
    <tr id="trErrorMessage" runat="server" visible="false">
        <td colspan="4" style="background-color: #e9e1e1;">
            <asp:Label ID="GridViewPagingError" runat="server" Font-Names="Verdana" Font-Size="9pt"
                ForeColor="Red"></asp:Label>
            <asp:HiddenField ID="TotalRows" runat="server" Value="0" />
        </td>
    </tr>
</table>
