<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.master" AutoEventWireup="true"
    CodeBehind="VisitorVPass.aspx.cs" Inherits="EntityFrameworkDBF.VisitorVPass"
    Theme="Admin_Basic" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Src="~/UserControls/GridViewPaging.ascx" TagPrefix="uc1" TagName="GridViewPaging" %>--%>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <a href="App_Themes/Admin_Basic/SkinFile.skin"></a><a href="App_Themes/Admin_Basic/">
    </a>
    <style type="text/css">
        .textboxLarge
        {
            margin-left: 0px;
        }
        .style7
        {
            width: 89px;
        }
        .textboxLarge
        {
            margin-left: 0px;
        }
        .style10
        {
            width: 59px;
        }
        .style18
        {
            height: 28px;
            width: 87px;
        }
        .style20
        {
            height: 28px;
            width: 59px;
        }
        .style22
        {
            height: 30px;
            width: 87px;
        }
        .style24
        {
            height: 30px;
            width: 59px;
        }
        .style26
        {
            width: 215px;
        }
        .style27
        {
            height: 28px;
            width: 215px;
        }
        .style28
        {
            height: 30px;
            width: 215px;
        }
        .style29
        {
            width: 117px;
        }
        .style30
        {
            width: 117px;
            height: 28px;
            text-align: center;
        }
        .style31
        {
            width: 117px;
            height: 30px;
        }
        .style32
        {
            width: 116px;
        }
        .style33
        {
            width: 116px;
            height: 28px;
            text-align: center;
        }
        .style34
        {
            width: 116px;
            height: 30px;
        }
        .style37
        {
            width: 87px;
        }
        .style39
        {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
        }
        .style40
        {
            border-style: solid;
            border-width: 1px;
            font-family: Arial;
        }
        .style41
        {
            width: 158px;
        }
        .style42
        {
            font-family: Arial;
            font-size: small;
            font-weight: bold;
        }
        .style43
        {
            margin-left: 0px;
            font-family: Arial;
            font-size: small;
            font-weight: bold;
            text-align: left;
        }
        .style44
        {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            font-family: Arial;
        }
        .style45
        {
            -moz-border-radius: 7px 7px 7px 7px;
            border-radius: 7px 7px 7px 7px;
            -webkit-border-radius: 7px 7px 7px 7px;
            -khtml-border-radius: 7px 7px 7px 7px;
            border: 1px solid #7bb3d9;
            color: #FFFFFF;
            font-size: 12px;
            font-family: Arial;
            padding-top: 3px;
            padding-right: 10px;
            padding-bottom: 5px;
            padding-left: 10px;
            text-decoration: none;
            cursor: pointer; /* FF3.6+ */ /* Chrome,Safari4+ */ /* Chrome10+,Safari5.1+ */ /* Opera 11.10+ */ /* IE10+ */ /* W3C */;
            background-color: #3cb8d3;
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#3cb8d3', endColorstr='#0e8099',GradientType=0 );
            margin-left: 0px;
            font-weight: bold;
        }
        .style46
        {
            font-family: Arial;
            font-size: small;
            font-weight: bold;
            color: #666666;
        }
        .style47
        {
            width: 125px;
        }
        .style48
        {
            width: 31px;
        }
        .style49
        {
            width: 33px;
        }
        .style50
        {
            width: 153px;
        }
        </style>
    <script type="text/javascript">
        function Confirmationbox() {
            var result = confirm('Are you sure you want to Out selected Visitors(s)?');
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        function Confirmationbox1() {
            var result = confirm('Are you sure you want to Delete selected Visitors(s)?');
            if (result) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">





        function NumericCheck(e) {
            var key;
            var keychar;
            if (window.event)
                key = window.event.keyCode;
            else if (e)
                key = e.which;
            else
                return true;
            keychar = String.fromCharCode(key);
            keychar = keychar.toLowerCase();
            // control keys
            if ((key == null) || (key == 0) || (key == 8) ||
           (key == 9) || (key == 13) || (key == 46) || (key == 27) || (key == 32))
                return true;
            // alphas and numbers
            else if ((("0123456789-()").indexOf(keychar) > -1))
                return true;
            else

                return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <!-- <div style="float: left">
        <div style="float: left; width: 100%"> -->
    <div>
        <div class="header_02">
            Visitor Out
            <center>
                <div class="header_02">
                    <span id="spnHeading" runat="server"></span>
                </div>
                <div style="padding-left: 600px">
                    <asp:Button ID="btnhome" runat="server" Text="BACK TO HOME" CssClass="button" OnClick="btnhome_Click" />
                </div>
            </center>
        </div>
        <div class="margin_bottom_10 border_bottom">
        </div>
        <center>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:RadioButton ID="rdoVisitor" runat="server" GroupName="Report" Text="VISITOR REPORT"
                                Font-Bold="true" Visible="false" />
                        </td>
                        <td>
                            <asp:RadioButton ID="rdoForeign" runat="server" GroupName="Report" Text="FOREIGN VISITOR REPORT"
                                Font-Bold="true" Checked="true" />
                        </td>
                    </tr>
                </table>
            </div>
        </center>
        <table style="width: 868px; margin-right: 2px;">
            <tr valign="top">
                <td id="td_message" runat="server" colspan="3" align="left">
                    <span id="lblErrorMessage" runat="server"></span>
                </td>
            </tr>
            <tr>
                <td class="style37">
                    <asp:Label ID="Label1" runat="server" Text="Visitor ID" CssClass="style42" Visible="False"></asp:Label>
                </td>
                <td class="style32">
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="style43" placeholder="Visitor ID"
                        Width="128px" onkeypress="return NumericCheck(event);" MaxLength="10" Visible="False"></asp:TextBox>
                </td>
                <td class="style10">
                    &nbsp;
                </td>
                <td class="style29">
                    <asp:DropDownList ID="ddlSearchBy" runat="server" AppendDataBoundItems="True" CssClass="dropdown"
                        Visible="False">
                        <asp:ListItem>Visitor ID</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style41">
                </td>
                <td class="style26">
                    <asp:Label ID="CVIssued" runat="server" Text="Total CV Pass Issued" Style="font-size: medium;
                        text-align: left; font-family: Arial; font-weight: 700;" CssClass="style39" Font-Size="X-Small"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label18" runat="server" Text=":" Style="font-size: medium" CssClass="strong"></asp:Label>
                </td>
                <td width="50px">
                    <asp:Label ID="txti" runat="server" Font-Size="Medium" Font-Bold="True" Style="border-style: solid"
                        CssClass="style44"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style18">
                    <asp:Label ID="lblfromdate" runat="server" Text="From Date" CssClass="style42"></asp:Label>
                </td>
                <td class="style33">
                    <asp:TextBox ID="txtfromdate" runat="server" Width="128px" CssClass="style46"></asp:TextBox>
                    <cc1:CalendarExtender ID="FromDateCalendar" runat="server" Enabled="True" Format="dd/MM/yyyy"
                        TargetControlID="txtfromdate">
                    </cc1:CalendarExtender>
                </td>
                <td class="style20">
                    <asp:Label ID="lbltodate" runat="server" Text="To Date" CssClass="style42"></asp:Label>
                </td>
                <td class="style30">
                    <asp:TextBox ID="txttodate" runat="server" Width="128px" CssClass="style46"></asp:TextBox>
                    <cc1:CalendarExtender ID="ToCalendar" runat="server" Enabled="True" Format="dd/MM/yyyy"
                        TargetControlID="txttodate">
                    </cc1:CalendarExtender>
                </td>
                <td class="style41">
                </td>
                <td class="style27">
                    <asp:Label ID="CVReturned" runat="server" Text="Total CV Pass Returned" Style="font-size: medium;
                        font-family: Arial; font-weight: 700;" Font-Size="X-Small" CssClass="style39"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label19" runat="server" Text=":" Style="font-size: medium" CssClass="strong"></asp:Label>
                </td>
                <td width="50px">
                    <asp:Label ID="txtr" runat="server" Font-Size="Medium" Font-Bold="True" Style="border-style: solid;
                        border-width: 1px; padding: 1px 4px" CssClass="style40"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style22">
                </td>
                <td class="style34">
                    <asp:CheckBox ID="CheckNull" runat="server" Font-Bold="True" Text="All VIsitors"
                        CssClass="style42" Visible="false" />
                </td>
                <td class="style24">
                </td>
                <td class="style31">
                    <asp:CheckBox ID="CheckForeignNull" runat="server" Font-Bold="True" Text="All Foreign"
                        CssClass="style42" />
                </td>
                <td class="style41">
                </td>
                <td class="style28">
                    <asp:Label ID="CVBalanced" runat="server" Text="Total CV Pass Balanced" Style="font-size: medium;
                        font-family: Arial; font-weight: 700;" CssClass="style39" Font-Size="X-Small"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label20" runat="server" Text=":" Style="font-size: medium" CssClass="strong"></asp:Label>
                </td>
                <td width="50px">
                    <asp:Label ID="txtb" runat="server" Font-Size="Medium" Font-Bold="True" CssClass="style44"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style37">
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td class="style7">
                </td>
                <td style="color: #ef6931">
                    <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" CssClass="btnView"
                        OnClientClick="return Validate();" Width="93px" BackColor="#ef6931" />
                </td>
                <td>
                    <asp:Button ID="btncancel" runat="server" Width="93px" Text="Cancel" CssClass="btnView"
                        OnClick="btncancel_Click" Visible="false" BackColor="#ef6931" />
                </td>
                <td class="style48">
                </td>
                <td>
                    <asp:Button ID="btntotal" runat="server" Width="93px" Text="Total" CssClass="btnView"
                        OnClick="btntotal_Click" Visible="false" BackColor="#ef6931" />
                </td>
                <td class="style49">
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnCheckOut" runat="server" Text="CheckOut" CssClass="btnView" OnClick="btnCheckOut_Click"
                        OnClientClick="javascript:return Confirmationbox();" Width="93px" BackColor="#ef6931" />
                </td>
                <td class="style49">
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnView" OnClick="btnDelete_Click"
                        OnClientClick="javascript:return Confirmationbox1();" Width="93px" Visible="false"
                        BackColor="#ef6931" />
                </td>
                <td class="style49">
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnExport" runat="server" Text="Export To Excel" CssClass="btnView"
                        OnClick="btnExport_Click" OnClientClick="return Validate()" BackColor="#ef6931"
                        Width="133px" />
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style7">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table class="tbl-full">
            <tr>
                <td>
                    <asp:GridView ID="gvPass" runat="server" AllowPaging="false" AllowSorting="false"
                        SkinID="grid" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="SerialNo"
                        Width="1190px" ForeColor="#333333" GridLines="Vertical" Font-Names="Arial" OnSelectedIndexChanged="gvPass_SelectedIndexChanged">
                        <AlternatingRowStyle CssClass="row2" BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="SerialNo" HeaderText="Serial No" ReadOnly="True" Visible="false"
                                SortExpression="SerialNo">
                                <ItemStyle Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="V_ID" HeaderText="Visitor Id" ReadOnly="True" SortExpression="VisitorTranID">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VisitorName" HeaderText="Visitor Name" ReadOnly="True"
                                SortExpression="VisitorName">
                                <ItemStyle Width="140px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Card_No" HeaderText=" Card Number" ReadOnly="True" SortExpression="CardNumber">
                                <ItemStyle Width="101px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MobileNumber" HeaderText="Mobile No" ReadOnly="True" SortExpression="MobileNumber">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Pass_No" HeaderText="Pass No" SortExpression="PassNumber" />
                            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="FromDate" />
                            <asp:BoundField DataField="ToDate" HeaderText="Valid Till" SortExpression="ToDate"
                                Visible="true" />
                            <asp:BoundField DataField="Company" HeaderText="Company Name" ReadOnly="True" SortExpression="Oragantization">
                                <ItemStyle Width="130px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TimeIn" HeaderText="Time In" ReadOnly="True" SortExpression="TimeIn">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Out Time">
                                <HeaderTemplate>
                                    <asp:Label ID="lblOutTime" runat="server" Text="Enter Out Time" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOuttime" runat="server" Width="120px" Text='<%# Bind("[TimeOut]") %>'
                                        Enabled="False">00:00:00</asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AcceptAMPM="false"
                                        ErrorTooltipEnabled="True" Mask="99:99:99" MaskType="Time" MessageValidatorTip="true"
                                        OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="txtOuttime"
                                        UserTimeFormat="TwentyFourHour" DisplayMoney="Left" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbAOutTime" runat="server" OnCheckedChanged="cbAOutTime_CheckedChanged"
                                        OnClientClick="javascript:return Confirmationbox();" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" CssClass="GriedFooter" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#507CD1" CssClass="GriedHeader" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" CssClass="GriedFooter" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle CssClass="row1" BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" CssClass="SelectedGridItems" Font-Bold="True"
                            ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td class="PagingCtrl">
                    <%--   <uc1:GridViewPaging ID="GridViewPagingControl" runat="server" Width="100%" />--%>
                </td>
            </tr>
        </table>
        <!-- <div style="float: left; width: 100%; >-->
        <rsweb:ReportViewer ID="ReportViewer2" runat="server" Width="100%" Visible="False"
            OnLoad="ReportViewer_OnLoad">
        </rsweb:ReportViewer>
    </div>
</asp:Content>
