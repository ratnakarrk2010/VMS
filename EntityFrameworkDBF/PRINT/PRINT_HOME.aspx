<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.master" AutoEventWireup="true"
    CodeBehind="PRINT_HOME.aspx.cs" Inherits="EntityFrameworkDBF.PRINT.PRINT_HOME"
    Theme="Admin_Basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <center>
        <div class="header_02">
            DVSC : PASS MANAGEMENT SYSTEM - <span id="spnDate" runat="server"></span>
        </div>
        <div style="padding-left: 600px">
            <asp:Button ID="btnhome" runat="server" Text="BACK TO HOME" CssClass="button" OnClick="btnhome_Click" />
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
                        <asp:ListItem Text="PASS NO" Value="1"></asp:ListItem>
                        <asp:ListItem Text="AADHAAR NO" Value="2"></asp:ListItem>
                        <asp:ListItem Text="RFID ID NO" Value="3"></asp:ListItem>
                        <asp:ListItem Text="DOCKYARD ID NO" Value="4"></asp:ListItem>
                        <%--  <asp:ListItem Text="NAME" Value="5"></asp:ListItem>--%>
                        <%--  <asp:ListItem Text="MOBILE NO" Value="6"></asp:ListItem>--%>
                        <%--<asp:ListItem Text="PSU" Value="7"></asp:ListItem>
                        <asp:ListItem Text="UNIT" Value="8"></asp:ListItem>
                        <asp:ListItem Text="SHOP" Value="9"></asp:ListItem>--%>
                        <%--  <asp:ListItem Text="FIRM" Value="10" ></asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtSearch" runat="server" Width="150px"></asp:TextBox>
                </td>
                <td>
                    <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                        background-repeat: no-repeat">
                        <asp:Button ID="btnSearch" runat="server" Text="SEARCH" CssClass="btnView" OnClick="btnSearch_Click" />
                    </div>
                </td>
            </tr>
        </table>
        <div style="overflow-x: scroll">
            <asp:GridView runat="server" ShowFooter="true" ShowHeaderWhenEmpty="true" ID="Gv_Reports"
                AutoGenerateColumns="false" AllowPaging="true" SkinID="grid1" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" AlternatingRowStyle-BackColor="#ffffdd"
                CellPadding="3" OnPageIndexChanging="Gv_Reports_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="SL">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="NAME">
                        <ItemTemplate>
                            <asp:Label ID="lblCName" runat="server" Text='<%#Eval("Cont_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AADHAAR NO">
                        <ItemTemplate>
                            <asp:Label ID="lblAadhaar" runat="server" Text='<%#Eval("Cont_Aadhaar") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOBILE NO">
                        <ItemTemplate>
                            <asp:Label ID="lblMobile" runat="server" Text='<%#Eval("Cont_Mobile") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FIRM NAME">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnFirmid" runat="server" Value='<%#Eval("Cont_FirmID") %>' />
                            <asp:Label ID="lblFirmName" runat="server" Text='<%#Eval("FIRM_NAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FIRM WORK ORDER NO">
                        <ItemTemplate>
                            <asp:Label ID="lblFrimWO" runat="server" Text='<%#Eval("Cont_FirmWO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="WORK ORDER VALIDITY">
                        <ItemTemplate>
                            <asp:Label ID="lblWOvalidity" runat="server" Text='<%#Eval("Cont_WOValidity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UNIT NAME">
                        <ItemTemplate>
                            <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("Cont_Unit") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PVC NO">
                        <ItemTemplate>
                            <asp:Label ID="lblPVCNo" runat="server" Text='<%#Eval("Cont_PVCNO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PVC VALIDITY">
                        <ItemTemplate>
                            <asp:Label ID="lblPVCvalidity" runat="server" Text='<%#Eval("Cont_PVCValidity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RFID NO">
                        <ItemTemplate>
                            <asp:Label ID="lblRfidNo" runat="server" Text='<%#Eval("Cont_RFIDNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RFID VALIDITY">
                        <ItemTemplate>
                            <asp:Label ID="lblRfidValidity" runat="server" Text='<%#Eval("Cont_RFIDValidity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CARD NO">
                        <ItemTemplate>
                            <asp:Label ID="lblCardno" runat="server" Text='<%#Eval("Cont_CardNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DESIGANTION">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnDesignation" runat="server" Value='<%#Eval("Cont_DesignationID") %>' />
                            <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("DESIGNATION_NAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ISSUE DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblissuedate" runat="server" Text='<%#Eval("Cont_IssueDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <table>
            <tr>
                <td>
                    <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                        background-repeat: no-repeat">
                        <asp:Button ID="btnPrint" runat="server" Text="PRINT" OnClick="btnPrint_Click" 
                            Visible="False" />
                    </div>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
