<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.master" AutoEventWireup="true"
    CodeBehind="USERMASTER.aspx.cs" Inherits="EntityFrameworkDBF.ADMIN_PANEL.USERMASTER"
    Theme="Admin_Basic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
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
            USER MASTER
        </div>
        <div style="padding-left: 600px">
            <asp:Button ID="btnhome" runat="server" Text="BACK TO HOME" CssClass="btnView" OnClick="btnhome_Click" />
            <asp:Button ID="btnpnlhome" runat="server" Text="BACK TO ADMIN PANEL" CssClass="btnView"
                OnClick="btnpnlhome_Click" />
        </div>
    </center>
    <center>
        <table border="1">
            <tr>
                <td>
                    <asp:Label ID="lblCode" runat="server" CssClass="Lable" Text="EMPLOYEE TOKEN "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="txtView" MaxLength="18"
                        onkeypress="return alphaNumericCheck(event)" Width="160px"></asp:TextBox>
                    <asp:Label ID="Label6" runat="server" CssClass="ErrorMassege" Text="*"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblUserName" runat="server" CssClass="Lable" Text="USER NAME"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUsertName" runat="server" CssClass="txtView" MaxLength="25" onkeypress="return alphabeticCheck(event)"
                        Width="160px"></asp:TextBox>
                    <asp:Label ID="Label9" runat="server" CssClass="ErrorMassege" Text="*"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPassword" runat="server" CssClass="Lable" Text="PASSWORD"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="txtView" MaxLength="25" Width="160px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblRePassword" runat="server" CssClass="Lable" Text="RE-ENTER PASSWORD"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRePassword" runat="server" CssClass="txtView" MaxLength="25"
                        Width="160px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblRoleTypeid" runat="server" CssClass="Lable" Text="ROLETYPE"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlroletype" runat="server" CssClass="txtView" MaxLength="25"
                        Width="160px">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblStatus" runat="server" CssClass="Lable" Text="STATUS"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtStatus" runat="server" CssClass="txtView" MaxLength="25" Width="160px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:RadioButtonList ID="rdbActivation" runat="server" CssClass="lblspan" RepeatDirection="Horizontal"
                        TabIndex="18" Enabled="true" Width="175px">
                        <asp:ListItem Value="A">ACTIVE</asp:ListItem>
                        <asp:ListItem Selected="True" Value="D">DE-ACTIVE</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <div>
            <asp:CheckBoxList ID="chkRights" runat="server" EnableTheming="True" Font-Bold="True"
                Height="67px" Width="375px">
                <asp:ListItem Value="1">ADMIN PANEL</asp:ListItem>
                <asp:ListItem Selected="True" Value="2">ISSUE</asp:ListItem>
                <asp:ListItem Value="3">RENEWAL</asp:ListItem>
                <asp:ListItem Value="4">ACTIVATE / DEACTIVATE</asp:ListItem>
                <asp:ListItem Value="5">CANCELLATION</asp:ListItem>
                <asp:ListItem Value="6">LABOUR MODULE</asp:ListItem>
                <asp:ListItem Value="7">PRINT RFID</asp:ListItem>
                <asp:ListItem Value="8">CV PASS</asp:ListItem>
                <asp:ListItem Value="9">REPORTS</asp:ListItem>
            </asp:CheckBoxList>
        </div>
        <table>
            <tr>
                <td>
                    <div>
                        <asp:Button ID="btnSave" runat="server" Text="SAVE" OnClick="btnSave_Click" />
                        <asp:Label ID="Mesg" runat="server" CssClass="Lable"></asp:Label>
                    </div>
                </td>
            </tr>
        </table>
    </center>
    <center>
        <div>
            <asp:GridView ID="Gv_Users" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                SkinID="grid1" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                AlternatingRowStyle-BackColor="#ffffdd" CellPadding="3">
                <Columns>
                    <asp:TemplateField HeaderText="SL">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EMPLOYEE TOKEN">
                        <ItemTemplate>
                            <asp:Label ID="lblEtoken" runat="server" Text='<%#Eval("EMPTOKEN") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEtoken" CssClass="txtView" runat="server" Text='<%#Eval("EMPTOKEN") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Name">
                        <ItemTemplate>
                            <asp:Label ID="lblUname" runat="server" Text='<%#Eval("USERNAME") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtUname" CssClass="txtView" runat="server" Text='<%#Eval("USERNAME") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Department">
                        <ItemTemplate>
                            <asp:Label ID="lblDept" runat="server" Text='<%#Eval("DEPARTMENTNAME") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDept" runat="server" CssClass="txtView" Text='<%#Eval("DEPARTMENTNAME") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Menu">
                        <ItemTemplate>
                            <asp:Label ID="lblMenu" runat="server" Text='<%#Eval("MENUID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMenu" runat="server" CssClass="txtView" Text='<%#Eval("MENUID") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Active">
                        <ItemTemplate>
                            <asp:Label ID="lblActv" runat="server" Text='<%#Eval("ISACTIVE") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtActv" runat="server" CssClass="txtView" Text='<%#Eval("ISACTIVE") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </center>
</asp:Content>
