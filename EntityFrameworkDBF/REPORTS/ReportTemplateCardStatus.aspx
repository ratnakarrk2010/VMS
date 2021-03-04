<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.master" AutoEventWireup="true"
    CodeBehind="ReportTemplateCardStatus.aspx.cs" Inherits="EntityFrameworkDBF.REPORTS.ReportTemplateCardStatus"
    Theme="Admin_Basic" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/templatemo_style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <center>
        <div class="header_02">
            DVSC : PASS MANAGEMENT SYSTEM - <span id="spnDate" runat="server"></span>
            <br />
            <br />
            TEMPLATES STATUS
        </div>
        <div style="padding-left: 900px">
            <asp:Button ID="btnactivatehome" runat="server" Text="BACK TO HOME" CssClass="btnView"
                OnClick="btnactivatehome_Click" />
        </div>
    </center>
    <center>
        <table>
            <tr>
                <td colspan='2'>
                    <asp:Label ID="lblSelectReport" runat="server" Text="SELECT REPORT :" Style="font-size: x-large;
                        font-weight: bold;"></asp:Label>
                </td>
                <td colspan='2'>
                    <asp:DropDownList ID="ddlselectReport" runat="server" Style="font-weight: 700" OnSelectedIndexChanged="ddlselectReport_SelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="Individual Report" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Loss Report" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Total Issue Report" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Total Cancel Report" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Total Valid Report" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Expired But Not Renewed" Value="6"></asp:ListItem>
                        <asp:ListItem Text="Total In Out Report" Value="7"></asp:ListItem>
                        <asp:ListItem Text="Religion Wise Report" Value="8"></asp:ListItem>
                        <asp:ListItem Selected="True" Text="Biometric Status Report" Value="9"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblfromdate" runat="server" Text="FROM DATE" Style="font-size: large;
                        font-weight: bold;"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtfromdate" placeholder="FROM DATE" runat="server" Style="font-size: medium;
                        font-weight: bold;"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                        TargetControlID="txtfromdate">
                    </cc1:CalendarExtender>
                </td>
                <td>
                    <asp:Label ID="lbltodate" runat="server" Text="TO DATE" Style="font-size: large;
                        font-weight: bold;"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txttoadte" placeholder="TO DATE" runat="server" Style="font-size: large;
                        font-weight: bold;"></asp:TextBox>
                    <cc1:CalendarExtender ID="txttoadte_CalendarExtender" runat="server" Enabled="True"
                        Format="dd/MM/yyyy" TargetControlID="txttoadte">
                    </cc1:CalendarExtender>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblSearchby" runat="server" Text="SEARCH BY :" Style="font-size: x-large;
                        font-weight: bold;"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlsearch" runat="server" Style="font-weight: 700">
                        <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="PASS NO"></asp:ListItem>
                        <asp:ListItem Text="AADHAAR NO"></asp:ListItem>
                        <asp:ListItem Text="RFID ID NO"></asp:ListItem>
                        <asp:ListItem Text="DOCKYARD ID NO"></asp:ListItem>
                        <asp:ListItem Text="FIRM NAME"></asp:ListItem>
                        <asp:ListItem Text="NAME"></asp:ListItem>
                        <asp:ListItem Text="MOBILE NO"></asp:ListItem>
                        <%--       <asp:ListItem Text="PSU"></asp:ListItem>
                        <asp:ListItem Text="UNIT"></asp:ListItem>
                        <asp:ListItem Text="SHOP"></asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtSearch" runat="server" Width="150px"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="dlCardCommand" runat="server" Style="font-weight: 700">
                        <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                        <asp:ListItem Text="UPLOAD" Value="Uplaod"></asp:ListItem>
                        <asp:ListItem Text="DOWNLOAD" Value="Download"></asp:ListItem>
                        <asp:ListItem Text="ACTIVE" Value="Activate"></asp:ListItem>
                        <asp:ListItem Text="DEACTIVE" Value="DeActivate"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                        background-repeat: no-repeat;">
                        <asp:Button ID="btnSearch" runat="server" Text="SEARCH" CssClass="btnView" OnClick="btnSearch_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </center>
    <center>
        <table>
            <tr>
                <td>
                    <asp:GridView runat="server" ShowFooter="true" ShowHeaderWhenEmpty="true" ID="Grd_ActivateViewData"
                        AutoGenerateColumns="false" Font-Size="Smaller" SkinID="grid" Visible="true">
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--    <asp:BoundField HeaderText="NAME" />--%>
                            <asp:TemplateField HeaderText="NAME">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("Cont_Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DockyardID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDockyardid" runat="server" Text='<%#Eval("Cont_DocID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FIRM NAME">
                                <ItemTemplate>
                                    <asp:Label ID="lblFIRM" runat="server" Text='<%#Eval("FIRM_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VALIDITY">
                                <ItemTemplate>
                                    <asp:Label ID="lblVALIDITY" runat="server" Text='<%#Eval("Cont_RFIDValidity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PASS NO">
                                <ItemTemplate>
                                    <asp:Label ID="lblPASSNO" runat="server" Text='<%#Eval("Cont_CardNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AADHAAR NO">
                                <ItemTemplate>
                                    <asp:Label ID="lblAADHAARNO" runat="server" Text='<%#Eval("Cont_Aadhaar") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RFID NO">
                                <ItemTemplate>
                                    <asp:Label ID="lblRFIDNO" runat="server" Text='<%#Eval("Cont_RFIDNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MinDate" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblMinDate" runat="server" Text='<%#Eval("Cont_MinDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="animation" BackColor="Black" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </center>
    <center>
        <table>
            <tr height="10">
                <td>
                    <asp:Label ID="lblTemplateMessage" Text="" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <asp:Label ID="lblactivate" Text="" runat="server" Style="font-size: x-large; font-weight: bold;
                            color: Green; align: center;" Visible="false"></asp:Label>
                        <table>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="GvContoller" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                        SkinID="grid1" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                        AlternatingRowStyle-BackColor="#ffffdd" OnPageIndexChanging="GvContoller_PageIndexChanging"
                                        CellPadding="3" PageSize="10" Visible="false">
                                        <Columns>
                                            <%--<asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="SR.NO">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CONTRACTOR NAME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContName" runat="server" Text='<%#Eval("CONTNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CARD NO">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCardNo" runat="server" Text='<%#Eval("CardNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FIRM NAME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFirmname" runat="server" Text='<%#Eval("FIRMNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="REQUEST STATUS">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRstatus" runat="server" Text='<%#Eval("DEVICECOMMAND") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DEVICE STATUS">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDstatus" runat="server" Text='<%#Eval("DEVICESTATUS") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CONTROLLER NO">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblControllerNo" runat="server" Text='<%#Eval("ControllerNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TRANDATE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTrandate" runat="server" Text='<%#Eval("Trandate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <%--  <tr>
                            <td>
                            <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                    background-repeat: no-repeat" runat="server" Visible="false" id="viewActive" >
                    <asp:Button ID="BtnActive" runat="server" Text="Active Template" OnClick="BtnActive_Click"  />
                </div>
                            </td>
                            <td>
                             <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                    background-repeat: no-repeat" runat="server" Visible="false" id="viewDeActive">
                    <asp:Button ID="BtnDeactive" runat="server" Text="Deactive Template" OnClick="BtnDeavtive_Click"  />
                </div>
                            </td>
                            </tr>--%>
                        </table>
                    </div>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
