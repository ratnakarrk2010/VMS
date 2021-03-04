<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.master" AutoEventWireup="true"
    CodeBehind="FIRM.aspx.cs" Inherits="EntityFrameworkDBF.ADMIN_PANEL.FIRM" Theme="Admin_Basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.txtView').keyup(function () {
                $(this).val($(this).val().toUpperCase());
            });
        });
    </script>
    <style type="text/css">
        .uppercase
        {
            text-transform: uppercase;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <center>
        <div class="header_02">
            DVSC : PASS MANAGEMENT SYSTEM - <span id="spnDate" runat="server"></span>
            <br />
            <br />
            FIRM MASTER
        </div>
        <div style="padding-left: 600px">
            <asp:Button ID="btnhome" runat="server" Text="BACK TO HOME" CssClass="btnView" OnClick="btnhome_Click" />
            <asp:Button ID="btnpnlhome" runat="server" Text="BACK TO ADMIN PANEL" CssClass="btnView"
                OnClick="btnpnlhome_Click" />
        </div>
    </center>
    <br />
    <center>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblSearch" runat="server" Text="SEARCH" Style="font-size: x-large;"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="txtView" Style="height: 25px"></asp:TextBox>
                </td>
                <td>
                    <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                        background-repeat: no-repeat;">
                        <asp:Button ID="btnSearch" runat="server" Text="     SEARCH" OnClick="btnSearch_Click" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFirm" runat="server" Text="FIRM" Style="font-size: x-large;"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFirm" runat="server" CssClass="txtView" Placeholder="Enter Firm Name"
                        Style="height: 25px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblgst" runat="server" Text="FIRM GST NO" Style="font-size: x-large;"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtgst" runat="server" CssClass="txtView" Placeholder="Enter Firm GST"
                        Style="height: 25px"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPropName" runat="server" Text="PROPRITER NAME" Style="font-size: x-large;"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPropName" runat="server" CssClass="txtView" Placeholder="Enter Firm Propriter Name"
                        Style="height: 25px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblFirmAddress" runat="server" Text="FIRM ADDRESS" Style="font-size: x-large;"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFirmAddress" runat="server" CssClass="txtView" Placeholder="Enter Firm Address"
                        Style="height: 25px"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                        background-repeat: no-repeat;">
                        <asp:Button ID="btnSave" runat="server" Text="     SAVE" OnClick="btnSave_Click" />
                    </div>
                </td>
                <td>
                    <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                        background-repeat: no-repeat;">
                        <asp:Button ID="btnRefresh" runat="server" Text="     REFRESH" OnClick="btnRefresh_Click" />
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <div class="btnView" style="padding-left: 900px">
            <asp:Button ID="btnGetRecord" runat="server" Text="GET DELETED RECORDS" OnClick="btnGetRecord_Click" />
        </div>
    </center>
    <br />
    <center>
        <table>
            <tr>
                <td>
                    <div>
                        <asp:GridView ID="Gv_FirmMaster" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                            SkinID="grid1" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                            AlternatingRowStyle-BackColor="#ffffdd" CellPadding="3" OnPageIndexChanging="Gv_FirmMaster_PageIndexChanging"
                            OnRowCancelingEdit="Gv_FirmMaster_RowCancelingEdit" OnRowDeleting="Gv_FirmMaster_RowDeleting"
                            OnRowEditing="Gv_FirmMaster_RowEditing" OnRowUpdating="Gv_FirmMaster_RowUpdating"
                            AutoGenerateDeleteButton="true" AutoGenerateEditButton="true" DataKeyNames="FIRM_ID">
                            <Columns>
                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FIRM NAME">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFirmName" runat="server" Text='<%#Eval("FIRM_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtFirmName" runat="server" Text='<%#Eval("FIRM_NAME") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FIRM GST NO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFirmGst" runat="server" Text='<%#Eval("FIRM_GST") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtFirmGst" runat="server" Text='<%#Eval("FIRM_GST") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FIRM FILE NO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFirmFile" runat="server" Text='<%#Eval("FIRM_FILE_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtFirmFile" runat="server" Text='<%#Eval("FIRM_FILE_NO") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FIRM ADDRESS">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFirmAddress" runat="server" Text='<%#Eval("FIRM_ADDRESS") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtFirmAddress" runat="server" Text='<%#Eval("FIRM_ADDRESS") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FIRM PROPRITER NAME">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFirmPropName" runat="server" Text='<%#Eval("FIRM_PROPRITER") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtFirmPropName" runat="server" Text='<%#Eval("FIRM_PROPRITER") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DELETE FLAG">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFlag" runat="server" Text='<%#Eval("FLAG") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="hdn_Flag" Value='<%#Eval("FLAG") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtFlag" runat="server" Text='<%#Eval("FLAG") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <%--<FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" CssClass="Pager" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />--%>
                        </asp:GridView>
                        <asp:HiddenField runat="server" ID="hdn_Flag_New" />
                    </div>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
