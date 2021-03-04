<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.master" AutoEventWireup="true"
    CodeBehind="CANCEL_HOME.aspx.cs" Inherits="EntityFrameworkDBF.CANCELLATION_MODULE.CANCEL_HOME"
    Theme="Admin_Basic" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.txtView').keyup(function () {
                $(this).val($(this).val().toUpperCase());
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <center>
        <div class="header_02">
            DVSC : PASS MANAGEMENT SYSTEM - <span id="spnDate" runat="server"></span>
            <br />
            <br />
            CANCELLATION
        </div>
        <div style="padding-left: 900px">
            <asp:Button ID="btncancelhome" runat="server" Text="BACK TO HOME" CssClass="btnView"
                OnClick="btncancelhome_Click" />
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
                        <asp:ListItem Text="NAME" Value="5"></asp:ListItem>
                        <asp:ListItem Text="MOBILE NO" Value="6"></asp:ListItem>
                     <%--   <asp:ListItem Text="DOCKYARD ID NO" Value="7"></asp:ListItem>--%>
                        <%-- <asp:ListItem Text="PSU" Value="7"></asp:ListItem>
                        <asp:ListItem Text="UNIT" Value="8"></asp:ListItem>
                        <asp:ListItem Text="SHOP" Value="9"></asp:ListItem>
                             <asp:ListItem Text="UNIQUE ID NO" Enabled="false" Value="4"></asp:ListItem>--%>
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
    </center>
    <center>
        <table>
            <tr>
                <td>
                    <asp:GridView runat="server" ShowFooter="true" ShowHeaderWhenEmpty="true" ID="Gv_CancelViewData"
                        AutoGenerateColumns="false" AllowPaging="true" SkinID="grid1" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" AlternatingRowStyle-BackColor="#ffffdd"
                        CellPadding="3" DataKeyNames="Cont_Id" OnRowDataBound="Gv_CancelViewData_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DockyardID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDockyardid" runat="server" Text='<%#Eval("Cont_DocID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NAME">
                                <ItemTemplate>
                                    <asp:Label ID="lblCName" runat="server" Text='<%#Eval("Cont_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCName" runat="server" Text='<%#Eval("Cont_Name") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FIRM NAME">
                                <ItemTemplate>
                                    <asp:Label ID="lblCFName" runat="server" Text='<%#Eval("FIRM_NAME") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCFName" runat="server" Text='<%#Eval("FIRM_NAME") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VALIDITY">
                                <ItemTemplate>
                                    <asp:Label ID="lblCValidity" runat="server" Text='<%#Eval("Cont_RFIDValidity") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCValidity" runat="server" Text='<%#Eval("Cont_RFIDValidity") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PASS NO">
                                <ItemTemplate>
                                    <asp:Label ID="lblCPassno" runat="server" Text='<%#Eval("Cont_CardNo") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCPassno" runat="server" Text='<%#Eval("Cont_CardNo") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AADHAAR NO">
                                <ItemTemplate>
                                    <asp:Label ID="lblCAadhaarNo" runat="server" Text='<%#Eval("Cont_Aadhaar") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCAadhaarNo" runat="server" Text='<%#Eval("Cont_Aadhaar") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RFID NO">
                                <ItemTemplate>
                                    <asp:Label ID="lblCRfidNo" runat="server" Text='<%#Eval("Cont_RFIDNo") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCRfidNo" runat="server" Text='<%#Eval("Cont_RFIDNo") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DELETE FLAG" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDeleteFlag" runat="server" Text='<%#Eval("Cont_Delete_Flag") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDeleteFlag" runat="server" Text='<%#Eval("Cont_Delete_Flag") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CANCEL FLAG" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCancelFlag" runat="server" Text='<%#Eval("Cont_CancelFLag") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCancelFlag" runat="server" Text='<%#Eval("Cont_CancelFLag") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="animation" BackColor="Black" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblLossReason" runat="server" Text="REASON :"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlLossReason" runat="server" AutoPostBack="true" Style="font-weight: 700"
                        OnSelectedIndexChanged="ddlLossReason_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDateOFLoss" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="Date Of Loss :" ToolTip="Date Of Loss"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDateOfLoss" runat="server" placeholder="Date Of Loss" Width="150px"
                        AutoPostBack="true"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtDateOfLoss_CalendarExtender" runat="server" Enabled="True"
                        Format="dd/MM/yyyy" TargetControlID="txtDateOfLoss">
                    </cc1:CalendarExtender>
                </td>
                <td>
                </td>
                <td>
                    <asp:Label ID="lblPlaceOfLoss" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="Place Of Loss :" ToolTip="Place Of Loss"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPlaceOfLoss" CssClass="txtView" runat="server" placeholder="Place Of Loss"
                        Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFir" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="FIR :" ToolTip="FIR"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFir" runat="server" CssClass="txtView" placeholder="FIR" Width="150px"></asp:TextBox>
                </td>
                <td>
                </td>
                <td>
                    <asp:Label ID="lblFine" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="FINE :" ToolTip="FINE"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFine" runat="server" CssClass="txtView" placeholder="FINE" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <div runat="server" id="DivCancel" class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                        background-repeat: no-repeat">
                        <asp:Button ID="btnCancelPass" runat="server" Text="CANCEL" CssClass="btnView" OnClick="btnCancelPass_Click" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                <div id="DDeaciiveSuccess" runat="server" visible="false" style="color: Green; font-size: small">
                    <b>DEACTIVATED TEMPLATE SUCESSFULLY</b>
                </div>
                <div id="DDeaciiveFail" runat="server" visible="false" style="color: Red; font-size: small">
                    <b>DEACTIVATED TEMPLATE FAIL</b>
                </div>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:GridView runat="server" ShowFooter="true" ShowHeaderWhenEmpty="true" ID="Gv_LossDetail"
                        AutoGenerateColumns="false" AllowPaging="true" SkinID="grid1" BackColor="White"
                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" AlternatingRowStyle-BackColor="#ffffdd"
                        CellPadding="3" DataKeyNames="Cancel_Id">
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NAME">
                                <ItemTemplate>
                                    <asp:Label ID="lblCName" runat="server" Text='<%#Eval("Cancel_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCName" runat="server" Text='<%#Eval("Cancel_Name") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CARD NO">
                                <ItemTemplate>
                                    <asp:Label ID="lblCardNo" runat="server" Text='<%#Eval("Cancel_PassNo") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCardNo" runat="server" Text='<%#Eval("Cancel_PassNo") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CANCEL REASON">
                                <ItemTemplate>
                                    <asp:Label ID="lblCancelReason" runat="server" Text='<%#Eval("Cancel_Cr") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCancelReason" runat="server" Text='<%#Eval("Cancel_Cr") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PLACE OF LOSS">
                                <ItemTemplate>
                                    <asp:Label ID="lblPlaceLoss" runat="server" Text='<%#Eval("Cancel_PlaceLoss") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPlaceLoss" runat="server" Text='<%#Eval("Cancel_PlaceLoss") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FINE">
                                <ItemTemplate>
                                    <asp:Label ID="lblFine" runat="server" Text='<%#Eval("Cancel_Fine") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFine" runat="server" Text='<%#Eval("Cancel_Fine") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="LOSS DATE">
                                <ItemTemplate>
                                    <asp:Label ID="lblLossDate" runat="server" Text='<%#Eval("Loss_Date") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtLossDate" runat="server" Text='<%#Eval("Loss_Date") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FIR NO">
                                <ItemTemplate>
                                    <asp:Label ID="lblfirNo" runat="server" Text='<%#Eval("Loss_Fir") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtfirNo" runat="server" Text='<%#Eval("Loss_Fir") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="animation" BackColor="Black" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
