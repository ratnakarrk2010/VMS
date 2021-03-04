<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.master" AutoEventWireup="true"
    CodeBehind="Operator_Home.aspx.cs" Inherits="EntityFrameworkDBF.Operator_Home"
    Theme="Admin_Basic" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .displayCount:hover
        {
            text-decoration: underline;
        }
        .zoomdiv
        {
            transition: 0.5s ease;
        }
        .zoomdiv:hover
        {
            -webkit-transform: scale(1.1);
            -moz-transform: scale(1.1);
            -ms-transform: scale(1.1);
            -o-transform: scale(1.1);
            transform: scale(1.1);
            transition: 0.5s ease;
        }
        .pointer
        {
            cursor: pointer;
        }
        .Pager table
        {
            margin: auto;
            border-collapse: collapse;
        }
        .Pager table tr td
        {
            /* border: 1px solid #275672;*/
            padding: 5px 0px;
            font-weight: bold;
        }
        .Pager table tr td:hover
        {
            background-color: #2ea9d1;
        }
        .Pager table tr td:hover a
        {
            color: #ffa500 !important;
        }
        .Pager table tr td a, .Pager table tr td span
        {
            padding: 5px 8px !important;
        }
        .Pager table tr td a
        {
            /* color:#275672 !important;*/
            text-decoration: none;
            color: #000000 !important;
            font-weight: bold;
        }
        .Pager table tr td a:hover
        {
            background-color: #2ea9d1;
            color: #275672 !important;
        }
        .Pager table tr td span
        {
            color: #ffa500;
        }
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="Server">
    <div>
        <center>
            <div class="header_02">
                DVSC FOREIGN PASS STATUS - <span id="spnDate" runat="server"></span>
            </div>
            <div class="header_02">
                <span id="spnHeading" runat="server"></span>
            </div>
            <div style="padding-left: 600px">
                <asp:Button ID="btnhome" runat="server" Text="BACK TO HOME" CssClass="button" OnClick="btnhome_Click" />
            </div>
        </center>
        <div class="margin_bottom_10 border_bottom">
        </div>
        <div style="">
            <div style="padding-top: 15px;">
                <center>
                    <table class="table-box" style="width: 900px; margin: auto;">
                        <tr>
                            <td>
                                <div class="service_box box-1" style="height: 130px;">
                                    <div class="sub-box">
                                        <asp:LinkButton ID="lnk_ForVisitorPass" runat="server" ClientIDMode="Static" Text="0"
                                            Style="color: Black; text-decoration: none;" OnClick="lnk_ForVisitorPass_Click"
                                            CssClass="btnhover"> </asp:LinkButton>
                                    </div>
                                    <strong><span>FOREIGN VISITOR PASS</span></strong>
                                </div>
                            </td>
                            <td>
                                <div class="service_box box-1" style="height: 130px;">
                                    <div class="sub-box">
                                        <asp:LinkButton ID="btnNewForeign" runat="server" ClientIDMode="Static" Text="FOREIGN PASS ISSUE"
                                            Style="color: Black; text-decoration: none;" CssClass="btnhover" OnClick="btnNewForeign_Click"> </asp:LinkButton>
                                        <asp:LinkButton ID="btnCasualvisi" runat="server" ClientIDMode="Static" Text="" Style="color: Black;
                                            text-decoration: none;" CssClass="btnhover" OnClick="btnCasualvisi_Click" Visible="false"> </asp:LinkButton>
                                    </div>
                                    <strong><span></span></strong>
                                </div>
                            </td>
                            <td>
                                <div class="service_box box-1" style="height: 130px;">
                                    <div class="sub-box">
                                        <asp:LinkButton ID="lnk_ForReport" runat="server" Style="color: Black; text-decoration: none;"
                                            ClientIDMode="Static" Text="FOREIGN VISITOR" OnClick="lnk_ForReport_Click"> </asp:LinkButton>
                                    </div>
                                    <strong><span><strong><span style="font-size: 27px">REPORT</span></strong></span></strong>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="service_box box-1" style="height: 100px;">
                                <div class="sub-box">
                                    <asp:LinkButton ID="lnk_UpdateVisitor" runat="server" Style="color: Black; text-decoration: none;"
                                        ClientIDMode="Static" Text="UPDATE VISITOR" OnClick="lnk_UpdateVisitor_Click"> </asp:LinkButton>
                                </div>
                                <strong><span style="font-size: 27px">DATA</span></strong>
                            </td>
                        </tr>
                    </table>
                </center>
            </div>
            <table>
            </table>
            <table>
                <tr>
                    <td style="visibility: hidden">
                        <div class="service_box box-2">
                            <div class="sub-box">
                                <asp:LinkButton ID="btnexce_visi" runat="server" Style="color: Black; text-decoration: none;"
                                    ClientIDMode="Static" Text="0" OnClick="btnexce_visi_Click"> </asp:LinkButton>
                            </div>
                            <strong><span>Executive Visitors</span></strong>
                        </div>
                    </td>
                    <td style="visibility: hidden">
                        <div class="service_box box-3">
                            <div class="sub-box">
                                <asp:LinkButton ID="btnretired_visi" runat="server" Style="color: Black; text-decoration: none;"
                                    ClientIDMode="Static" Text="0" OnClick="btnretired_visi_Click"> </asp:LinkButton>
                            </div>
                            <strong><span>Retired Visitors</span></strong>
                        </div>
                    </td>
                    <td style="visibility: hidden">
                        <div class="service_box box-4">
                            <div class="sub-box">
                                <asp:LinkButton ID="btn_store" runat="server" Style="color: Black; text-decoration: none;"
                                    ClientIDMode="Static" Text="0" OnClick="btn_store_Click"> </asp:LinkButton>
                            </div>
                            <strong><span>Store</span></strong>
                        </div>
                    </td>
                    <td style="visibility: hidden">
                        <div class="service_box box-4">
                            <div class="sub-box">
                                <asp:LinkButton ID="btn_labour" runat="server" Style="color: Black; text-decoration: none;"
                                    ClientIDMode="Static" Text="0" OnClick="btn_labour_Click"> </asp:LinkButton>
                            </div>
                            <strong><span>Labour</span></strong>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <div class="header_03">
            Casual Visitor Print List
        </div>
        <div style="">
            <asp:GridView ID="GvCasual" ClientIDMode="Static" runat="server" Width="100%" DataKeyNames="Center_No"
                SkinID="grid" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC"
                BorderStyle="None" BorderWidth="1px" AlternatingRowStyle-BackColor="#ffffdd"
                PageSize="10" CellPadding="3" GridLines="Both">
                <Columns>
                    <asp:BoundField HeaderText="Center No" DataField="Center_No" />
                    <asp:BoundField HeaderText="Department" DataField="ES_Dept" />
                    <asp:BoundField HeaderText="Total Pass" DataField="Total" />
                    <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btn_view" OnClick="btn_view_click" Text="View" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgprint" ImageUrl="~/Images/print.gif" runat="server" Width="55"
                                Height="25" OnClick="imgprint_Click" />
                            <asp:HiddenField runat="server" ID="hdn_group_id" Value='<%# Eval("ES_Dept") %>' />
                            <asp:HiddenField runat="server" ID="hdn_Ofcr_Tran_Id" Value='<%# Eval("Ofcr_Tran_Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" CssClass="Pager" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
        </div>
        <asp:HiddenField runat="server" ID="hdn_seleted_center" />
        <asp:HiddenField runat="server" ID="hdn_seleted_deptName" />
        <br />
        <div class="header_03" style="visibility: hidden">
            Executive Visitor Print List
        </div>
        <asp:GridView ID="GvExeVisitorList" ClientIDMode="Static" runat="server" Width="100%"
            SkinID="grid" DataKeyNames="Center_No" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" AlternatingRowStyle-BackColor="#ffffdd"
            PageSize="10" CellPadding="3" Visible="false">
            <Columns>
                <asp:BoundField HeaderText="Center No" DataField="Center_No" />
                <asp:BoundField HeaderText="Department" DataField="ES_Dept" />
                <asp:BoundField HeaderText="Total Emp" DataField="Total" />
                <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ID="imExegprint" ImageUrl="~/Images/print.gif" runat="server" Width="55"
                            Height="25" OnClick="imExegprint_Click" />
                        <asp:HiddenField runat="server" ID="hdn_group_id" Value='<%# Eval("GroupID") %>' />
                        <asp:HiddenField runat="server" ID="hdn_Ofcr_Tran_Id" Value='<%# Eval("Ofcr_Tran_Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" CssClass="Pager" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
        <br />
        <div class="header_03" style="visibility: hidden">
            Retired Visitor Print List
        </div>
        <asp:GridView ID="GvRetiredPrintList" ClientIDMode="Static" runat="server" Width="100%"
            SkinID="grid" DataKeyNames="Center_No" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" AlternatingRowStyle-BackColor="#ffffdd"
            PageSize="10" CellPadding="3" Visible="false">
            <Columns>
                <asp:BoundField HeaderText="Center No" DataField="Center_No" />
                <asp:BoundField HeaderText="Department" DataField="ES_Dept" />
                <asp:BoundField HeaderText="Total Emp" DataField="Total" />
                <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ID="imRetigprint" ImageUrl="~/Images/print.gif" runat="server" Width="55"
                            Height="25" OnClick="imRetigprint_Click" />
                        <asp:HiddenField runat="server" ID="hdn_group_id" Value='<%# Eval("GroupID") %>' />
                        <asp:HiddenField runat="server" ID="hdn_Ofcr_Tran_Id" Value='<%# Eval("Ofcr_Tran_Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" CssClass="Pager" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
        <asp:HiddenField runat="server" ID="btnShowPopup" Visible="true" />
        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
            PopupControlID="pnlpopup" CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Style="display: none; border: 1px solid #000;
            border-color: red; border-style: solid; overflow: visible">
            <%--  <asp:UpdatePanel ID="UPUpdate" runat="server" UpdateMode="Always">
                    <ContentTemplate>--%>
            <%-- <div style="overflow: visible;">--%>
            <asp:Label runat="server" ID="other_than_labour"></asp:Label><br />
            <div style="overflow-y: auto; width: 100%; height: 550px;">
                <asp:GridView ID="Gv_EMP_LIST_TOPRINT" AllowPaging="True" AutoGenerateColumns="false"
                    PageSize="10" runat="server" ShowHeader="true" HeaderStyle-ForeColor="White"
                    AlternatingRowStyle-BackColor="#b8dbff" CaptionAlign="Top" DataKeyNames="V_PassNo"
                    SkinID="grid" OnRowDataBound="Gv_EMP_LIST_TOPRINT_RowDataBound" Visible="true">
                    <Columns>
                        <asp:TemplateField HeaderText="SL">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="DOCKYARD ID NO" DataField="V_PassNo" />
                        <asp:BoundField HeaderText="GOVT ID TYPE" DataField="V_ID_TYPE" />
                        <asp:BoundField HeaderText="GOVT ID NO" DataField="V_ID_NO" />
                        <asp:BoundField HeaderText="NAME" DataField="V_name" />
                        <asp:BoundField HeaderText="AGE" DataField="V_Age" />
                        <asp:BoundField HeaderText="GENDER" DataField="V_Sex" />
                        <asp:BoundField HeaderText="MOBILE" DataField="V_ContactNo" />
                        <asp:BoundField HeaderText="NATIONALITY" DataField="V_Nationality" />
                        <asp:BoundField HeaderText="FIRM TIN NO" DataField="V_FIRM_TIN" />
                        <asp:BoundField HeaderText="FIRM NAME" DataField="V_Firm" />
                        <asp:BoundField HeaderText="PURPOSE OF VISIT" DataField="V_Purpose" />
                        <asp:BoundField HeaderText="DURATION" DataField="DURATION" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:HiddenField runat="server" ID="hdn_Ofcr_Tran_Id" Value='<%#Eval("Ofcr_Tran_Id") %>' />
                                <asp:HiddenField runat="server" ID="hdnPassType" Value='<%#Eval("Emp_Type") %>' />
                                <asp:Button ForeColor="Blue" runat="server" ID="btn_new_print" BackColor="#ef6939"
                                    Text="Print" OnClick="btn_new_print_clicked" Visible='<%#Eval("V_PassNo").ToString().Contains("N") %>' />
                                <asp:Button runat="server" ID="btn_print" Text="Print" Visible='<%#!Eval("V_PassNo").ToString().Contains("N") %>' />
                                <asp:PopupControlExtender ID="PopupControlExtender1" Position="Left" PopupControlID="tbl_ashish"
                                    runat="server" TargetControlID="btn_print">
                                </asp:PopupControlExtender>
                                <asp:DragPanelExtender ID="DragPanelExtender1" TargetControlID="tbl_ashish" runat="server">
                                </asp:DragPanelExtender>
                                <asp:Table runat="server" BorderWidth="1" BorderColor="Black" ID="tbl_ashish" BackColor="AliceBlue">
                                    <asp:TableRow>
                                        <asp:TableCell Wrap="false">Card No. &nbsp;</asp:TableCell><asp:TableCell>
                                            <asp:TextBox runat="server" ID="txt_card_no" Width="80"></asp:TextBox></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="2">
                                            <asp:Button runat="server" ID="btn_print_old" OnClick="btn_print_old_clicked" CssClass="pointer"
                                                Text="OK" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                                <%--!     popup--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" CssClass="Pager" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
            </div>
            <asp:Button runat="server" ID="btnCancel" OnClick="btnCancel_click" Text="Close" />
            <%--      </div>--%>
            <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
        </asp:Panel>
    </div>
</asp:Content>
