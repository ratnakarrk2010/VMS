<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.master" AutoEventWireup="true"
    CodeBehind="ACTIAVTE_HOME.aspx.cs" Inherits="EntityFrameworkDBF.ACTIVATION_MODULE.ACTIAVTE_HOME"
    Theme="Admin_Basic" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/templatemo_style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .overlay
        {
            position: fixed;
            z-index: 98;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            background-color: #aaa;
            filter: alpha(opacity=80);
            opacity: 0.8;
        }
        .overlayContent
        {
            z-index: 99;
            margin: 250px auto;
            width: 80px;
            height: 80px;
        }
        .overlayContent h2
        {
            font-size: 18px;
            font-weight: bold;
            color: #000;
        }
        .overlayContent img
        {
            width: 80px;
            height: 80px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <center>
        <div class="header_02">
            DVSC : PASS MANAGEMENT SYSTEM - <span id="spnDate" runat="server"></span>
            <br />
            <br />
            ACTIVATE / DACTIVATE
        </div>
        <div style="padding-left: 900px">
            <asp:Button ID="btnactivatehome" runat="server" Text="BACK TO HOME" CssClass="btnView"
                OnClick="btnactivatehome_Click" />
        </div>
    </center>
    <center>
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
                       <%-- <asp:ListItem Text="DOCKYARD ID NO"></asp:ListItem>--%>
                        <%--                        <asp:ListItem Text="FIRM NAME"></asp:ListItem>
                        <asp:ListItem Text="NAME"></asp:ListItem>
                        <asp:ListItem Text="MOBILE NO"></asp:ListItem>--%>
                        <%--       <asp:ListItem Text="PSU"></asp:ListItem>
                        <asp:ListItem Text="UNIT"></asp:ListItem>
                        <asp:ListItem Text="SHOP"></asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtSearch" runat="server" Width="150px"></asp:TextBox>
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
                        AutoGenerateColumns="false" Font-Size="Smaller" SkinID="grid" Visible="true"
                        OnRowDataBound="Grd_ActivateViewData_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No">
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
                                    <asp:Label ID="lblVALIDITY" runat="server" Text='<%#Eval("Cont_MinDate") %>'></asp:Label>
                                </ItemTemplate>
                                <%-- <FooterTemplate>
                                    <asp:Label ID="lblExpireCheck" runat="server" Text="NA"></asp:Label>
                                </FooterTemplate>--%>
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
        <div style="text-align: center">
            <asp:Label ID="lblactivate" Text="Device List" runat="server" Style="font-size: xx-large;
                font-weight: bold; color: Green" Visible="false"></asp:Label>
        </div>
        <div class="margin_bottom_10 border_bottom">
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <div>
                                <table>
                                    <tr>
                                        <td colspan="3">
                                            <div id="dSucess" runat="server" visible="false" style="color: Green; font-size: small">
                                                <b>ACTIVATED TEMPLATE SUCESSFULLY</b>
                                            </div>
                                            <div id="dfail" runat="server" visible="false" style="color: Red; font-size: small">
                                                <b>ACTIVATED TEMPLATE FAIL</b>
                                            </div>
                                            <div id="DDeaciiveSuccess" runat="server" visible="false" style="color: Green; font-size: small">
                                                <b>DE-ACTIVATED TEMPLATE SUCESSFULLY</b>
                                            </div>
                                            <div id="DDeaciiveFail" runat="server" visible="false" style="color: Red; font-size: small">
                                                <b>DE-ACTIVATED TEMPLATE FAIL</b>
                                            </div>
                                            <div id="LblDownload" runat="server" visible="false" style="color: Red; font-size: small">
                                                <b>PLEASE DOWNLOAD TEMPLATE </b>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:GridView ID="GvContoller" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                                SkinID="grid" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                AlternatingRowStyle-BackColor="#ffffdd" CellPadding="3" Visible="false">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sr.No">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Controller Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblControllerName" runat="server" Text='<%#Eval("ControllerName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Controller IPAddress">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIpaddress" runat="server" Text='<%#Eval("IPAddress") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdConreollerNo" runat="server" Value='<%#Eval("ControllerNo") %>'>
                                                            </asp:HiddenField>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td rowspan="1" width="25%" align="center" colspan="4">
                                            <asp:DropDownList ID="ddlDownload" runat="server" OnSelectedIndexChanged="ddlDownload_SelectedIndexChanged"
                                                AutoPostBack="true" Style="font-weight: 700" Visible="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                        </td>
                                        <td colspan="1" width="25%" align="center" style="visibility: hidden">
                                            <div class="btnView" visible="false" style="background-image: url('../images2/templatemo_button_02.png');
                                                background-repeat: no-repeat; width: 150px">
                                                <asp:Button ID="AddFig" runat="server" Text="ADD FINGER" OnClick="AddFig_Click" Visible="False" />
                                            </div>
                                        </td>
                                        <td colspan="1" align="center" width="20%">
                                            <div class="btnView" runat="server" visible="false" id="divBtnDownload" style="background-image: url('../images2/templatemo_button_02.png');
                                                background-repeat: no-repeat; width: 250px">
                                                <asp:Button ID="Btndownlaod" runat="server" Text="DOWNLOAD BIOMETRIC FINGER" OnClick="Btndownlaod_Click"
                                                    Visible="False" />
                                            </div>
                                        </td>
                                        <td width="25%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                                                background-repeat: no-repeat; width: 130px; text-align: center" runat="server"
                                                visible="false" id="viewActive">
                                                <asp:Button ID="BtnActive" runat="server" Text="ACTIVATE" OnClick="BtnActive_Click"
                                                    OnClientClick="showProgress()" />
                                            </div>
                                        </td>
                                        <td align="center">
                                            <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                                                background-repeat: no-repeat; width: 130px; text-align: center" runat="server"
                                                visible="false" id="viewDeActive">
                                                <asp:Button ID="BtnDeactive" runat="server" Text="DEACTIVATE" OnClick="BtnDeavtive_Click" />
                                            </div>
                                        </td>
                                        <td align="center">
                                            <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                                                background-repeat: no-repeat; width: 130px; text-align: center" runat="server"
                                                visible="false" id="viewRefresh">
                                                <asp:Button ID="BtnRefresh" runat="server" Text="REFRESH" OnClick="BtnRefresh_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div class="overlay" />
                <div class="overlayContent">
                    <h2>
                        Loading...
                    </h2>
                    <img src="../Images/loader.gif" alt="Loading" border="1" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <%--<div class="loading" align="center">
    Loading. Please wait.<br />
    <br />
            <img src="../Images/loader.gif"  alt="" />
</div>--%>
    </center>
</asp:Content>
