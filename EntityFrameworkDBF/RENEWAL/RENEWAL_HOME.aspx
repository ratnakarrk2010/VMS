<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.master" AutoEventWireup="true"
    CodeBehind="RENEWAL_HOME.aspx.cs" Inherits="EntityFrameworkDBF.RENEWAL.RENEWAL_HOME"
    Theme="Admin_Basic" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/templatemo_style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ContPopup() {
            popupWindow = window.open("..\EntityFrameworkDBF\EntityFrameworkDBF\ISSUES MODULE\Print_ContractorPass.aspx", 'popUpWindow', 'height=600,width=1000,left=100,top=30,resizable=No,scrollbars=No,toolbar=no,menubar=no,location=no,directories=no, status=No');
        }
    </script>
    <script type="text/javascript">
        function ContPopupEs() {
            popupWindow = window.open("..\EntityFrameworkDBF\EntityFrameworkDBF\ISSUES MODULE\Print_EscortedPass.aspx", 'popUpWindow', 'height=600,width=1000,left=100,top=30,resizable=No,scrollbars=No,toolbar=no,menubar=no,location=no,directories=no, status=No');
        }
    </script>
    <script type="text/javascript">
        function BankPopup() {
            popupWindow = window.open("..\EntityFrameworkDBF\EntityFrameworkDBF\ISSUES MODULE\Print_BankPass.aspx", 'popUpWindow', 'height=600,width=1000,left=100,top=30,resizable=No,scrollbars=No,toolbar=no,menubar=no,location=no,directories=no, status=No');
        }
    </script>
    <script type="text/javascript">
        function CBPopup() {
            popupWindow = window.open("..\EntityFrameworkDBF\EntityFrameworkDBF\ISSUES MODULE\Print_DBPass.aspx", 'popUpWindow', 'height=600,width=1000,left=100,top=30,resizable=No,scrollbars=No,toolbar=no,menubar=no,location=no,directories=no, status=No');
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.txtView').keyup(function () {
                $(this).val($(this).val().toUpperCase());
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=img_AppPhoto]").click(function () {
                ShowPopup();
                return false;
            });
        });
        function ShowPopup() {
            $("#dialog").dialog({
                title: "GridView",
                width: 450,
                buttons: {
                    Ok: function () {
                        $(this).dialog('close');
                    }
                },
                modal: true
            });
        }
    </script>
    <script type="text/javascript" language="javascript">
        function validatenumerics(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            //comparing pressed keycodes

            if (keycode > 31 && (keycode < 48 || keycode > 57)) {
                alert(" You can enter only characters 0 to 9 ");
                return false;
            }
            else return true;
        }

    </script>
    <script type="text/javascript">
        function basicPopup() {
            popupWindow = window.open("ShowApplication.aspx", 'popUpWindow', 'height=600,width=1000,left=100,top=30,resizable=No,scrollbars=No,toolbar=no,menubar=no,location=no,directories=no, status=No');
        }
    </script>
    <script type="text/javascript">
        function basicPopup1() {
            popupWindow1 = window.open("GetImage.aspx", 'popUpWindow1', 'height=600,width=1000,left=100,top=30,resizable=No,scrollbars=No,toolbar=no,menubar=no,location=no,directories=no, status=No');
        }
    </script>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Scripts/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <center>
        <div class="header_02">
            RENEWAL / MODIFICATION
        </div>
        <div style="padding-left: 900px">
            <asp:Button ID="btnissuehome" runat="server" Text="BACK TO HOME" CssClass="btnView"
                OnClick="btnissuehome_Click" />
        </div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblSearchby" runat="server" Text="SEARCH BY :" ForeColor="#a39a19"
                        Font-Size="Large" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlsearch" runat="server" Style="font-weight: 700">
                        <asp:ListItem Text="--SELECT--" Value="-1"></asp:ListItem>
                        <asp:ListItem Text="PASS NO" Value="1"></asp:ListItem>
                        <asp:ListItem Text="AADHAAR NO" Value="2"></asp:ListItem>
                        <asp:ListItem Text="RFID ID NO" Value="3"></asp:ListItem>
                      <%--  <asp:ListItem Text="DOCKYARD ID NO" Value="4"></asp:ListItem>--%>
                        <%--<asp:ListItem Text="NAME" Value="5"></asp:ListItem>--%>
                        <asp:ListItem Text="MOBILE NO" Value="6"></asp:ListItem>
                        <%--<asp:ListItem Text="PSU" Value="7"></asp:ListItem>
                        <asp:ListItem Text="UNIT" Value="8"></asp:ListItem>
                        <asp:ListItem Text="SHOP" Value="9"></asp:ListItem>--%>
                        <asp:ListItem Text="NAME" Value="11"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtSearch" runat="server" Width="150px" CssClass="txtView"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <div class="header_03" style="visibility: hidden">
                        <asp:RadioButton ID="rdoRenew" ForeColor="#a39a19" runat="server" GroupName="Renew"
                            Text="RENEW PASS" AutoPostBack="true" Visible="false" Font-Bold="true" Font-Size="Large"
                            OnCheckedChanged="rdoRenew_CheckedChanged" />
                    </div>
                </td>
                <td>
                    <div class="header_03">
                        <asp:RadioButton ID="rdoModify" ForeColor="#a39a19" runat="server" GroupName="Renew"
                            Text="MODIFY DATA" AutoPostBack="true" Visible="false" Font-Bold="true" Font-Size="Large"
                            OnCheckedChanged="rdoModify_CheckedChanged" />
                    </div>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                        background-repeat: no-repeat">
                        <asp:Button ID="btnSearch" runat="server" Text="SEARCH" CssClass="btnView" OnClick="btnSearch_Click" />
                    </div>
                </td>
            </tr>
        </table>
        <div class="lblspan1">
            <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Medium"
                Text=""></asp:Label>
        </div>
    </center>
    <div runat="server" id="BasicDetailDiv">
        <table class="tbl-form">
            <tr>
                <td colspan="4">
                    &nbsp;
                    <asp:Label ID="lblContID" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Visible="true"></asp:Label>
                </td>
                <td style="text-align: center;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDocid" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="Dockyard Id :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDockyardID" runat="server" placeholder="Enter Docyard ID" CssClass="txtView"
                        Width="150px" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="return alphabeticCheck(event)"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblCardNo" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="Card No :"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblcno" runat="server" Style="font-size: medium; font-family: Calibri;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <span style="color: Red;">*</span>
                    <asp:Label ID="lblname" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="Name :"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtContractorName" runat="server" placeholder="Name" CssClass="txtView"
                        Width="150px" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="return alphabeticCheck(event)"></asp:TextBox>
                </td>
                <td>
                    <span style="color: Red;">* </span>&nbsp;<asp:Label ID="lbldob" runat="server" Style="font-size: medium;
                        font-family: Calibri;" Text="DOB :" ToolTip="Date Of Birth"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDateOfBirth" runat="server" placeholder="Date Of Birth" Width="150px"
                        AutoPostBack="true"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtDateOfBirth_CalendarExtender" runat="server" Enabled="True"
                        Format="dd/MM/yyyy" TargetControlID="txtDateOfBirth">
                    </cc1:CalendarExtender>
                </td>
                <td rowspan="5">
                    <asp:Panel ID="pnl" runat="server">
                        <object style="height: 160px; width: 250px">
                            <param name="movie" value="WebcamResources/save_picture.swf" />
                            <embed height="180" src="../WebcamResources/save_picture.swf" width="280"></embed>
                        </object>
                    </asp:Panel>
                </td>
                <td rowspan="4">
                    <asp:Panel ID="pnlAlreadyImage" runat="server" Height="150px">
                        <asp:Image ID="imgPicture" runat="server" Height="125px" Width="140px" />
                    </asp:Panel>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img alt="" src="" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td class="">
                    <span style="color: Red;">*</span>
                    <asp:Label ID="lblaadhaar" runat="server" Text="Aadhaar No :" Style="font-size: medium;
                        font-family: Calibri;"></asp:Label>
                </td>
                <td class="">
                    <asp:TextBox ID="txtAADHAAR" runat="server" placeholder="Aadhaar Number" Width="150px"
                        MaxLength="12" onkeypress="validatenumerics(event)" CssClass="txtView" AutoPostBack="true"
                        OnTextChanged="txtAADHAAR_TextChanged"></asp:TextBox>
                </td>
                <td class="">
                    <span style="color: Red;">*</span>
                    <asp:Label ID="lbldesignation" Style="font-size: medium; font-family: Calibri;" runat="server"
                        Text="Designation:"></asp:Label>
                </td>
                <td class="">
                    &nbsp;
                    <asp:DropDownList ID="ddlDesignation" runat="server" Width="150px" CssClass="txtView">
                    </asp:DropDownList>
                </td>
                <%--    <td class="style7">
            </td>--%>
            </tr>
            <tr>
                <td class="">
                    <span style="color: Red;">*</span>
                    <asp:Label ID="lblcontact" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="Mobile No. :"></asp:Label>
                </td>
                <td class="">
                    &nbsp;
                    <asp:TextBox ID="txtContactNo" CssClass="" runat="server" Width="150px" MaxLength="10"
                        placeholder="Mobile Number" onkeypress="validatenumerics(event)"></asp:TextBox>
                </td>
                <td class="">
                    <span style="color: Red;">*</span>
                    <asp:Label ID="lblReligion" runat="server" Text="Religion :" Style="font-size: medium;
                        font-family: Calibri;"></asp:Label>
                </td>
                <td class="">
                    &nbsp;
                    <asp:DropDownList ID="ddlReligion" Width="150px" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <span style="color: Red;">*</span>
                    <asp:Label ID="lblOther" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="Other Govt ID :"></asp:Label>
                </td>
                <td class="auto-style2">
                    &nbsp;
                    <asp:DropDownList ID="ddlother" CssClass="" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td class="auto-style2">
                    <span style="color: Red;">*</span>
                    <asp:Label ID="lblOtherDoc" runat="server" placeholder="document number" Style="font-size: medium;
                        font-family: Calibri;" Text="Govt ID No :"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtOtherDoc" CssClass="txtView" runat="server" Width="150px" AutoPostBack="true"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <span style="color: Red;">*</span>
                    <asp:Label ID="lblGender" runat="server" Text="Gender :" Style="font-size: medium;
                        font-family: Calibri;"></asp:Label>
                </td>
                <td class="auto-style2">
                    &nbsp;
                    <asp:DropDownList ID="ddlGender" Width="150px" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="auto-style2">
                </td>
                <td class="auto-style2">
                </td>
                <td>
                    <asp:LinkButton ID="LnkDownload" runat="server" Text="Download Image" OnClick="LnkDownload_Click"
                        Font-Size="20px"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="header_03" runat="server" id="DivFirmHeader">
        Firm Details :
    </div>
    <div class="margin_bottom_10 border_bottom">
    </div>
    <table class="tbl-form">
        <tr>
            <td class="style30">
                <asp:Label ID="lblfirmfileno" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="Firm File No :"></asp:Label>
                &nbsp;
            </td>
            <td class="style41">
                <asp:TextBox ID="txtfirmfileno" CssClass="txtView" runat="server" Width="150px" AutoPostBack="true"
                    OnTextChanged="txtfirmfileno_TextChanged"></asp:TextBox>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblfirm" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="Firm  Name :"></asp:Label>
                <asp:Label ID="lblPSUunit" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="PSU Unit :"></asp:Label>
                <asp:Label ID="lblShop" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="Shop Name :"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlFirm" runat="server" Width="150px" CssClass="txtView" AutoPostBack="true">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlPSUunit" runat="server" Width="150px" CssClass="txtView"
                    AutoPostBack="true">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlShop" runat="server" Width="150px" CssClass="txtView" AutoPostBack="true">
                </asp:DropDownList>
            </td>
            <td class="style36" style="margin-left: 40px">
                <asp:Label ID="lblgst" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="GST No:"></asp:Label>
                <asp:Label ID="lblUnitIcard" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="Unit I Card No:"></asp:Label>
                <asp:Label ID="lblUnitEmp" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="Unit Employee :"></asp:Label>
            </td>
            <td class="style41">
                <asp:TextBox ID="txtgst" CssClass="txtView" placeholder="GST No" runat="server" Width="150px"></asp:TextBox>
                <asp:TextBox ID="txtUnitIcard" CssClass="txtView" placeholder="Unit I Card No" runat="server"
                    Width="150px"></asp:TextBox>
                <asp:TextBox ID="txtUnitEmp" CssClass="" runat="server" Width="150px"></asp:TextBox>
            </td>
            <td class="style38">
                <asp:Label ID="lblUnit" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="Unit / Dept :"></asp:Label>
                &nbsp;
            </td>
            <td class="style33">
                <asp:TextBox ID="txtunit" CssClass="txtView" placeholder="Unit Name" runat="server"
                    Width="150px" MaxLength="40"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style40">
                <asp:Label ID="lblFirmWONo" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="Work Order No :"></asp:Label>
            </td>
            <td class="style42">
                <asp:TextBox ID="txtFirmWorkOrderNo" CssClass="" runat="server" Width="150px"></asp:TextBox>
            </td>
            <td class="style35">
                <asp:Label ID="lblwovalidity" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="Work Order Validity:"></asp:Label>
            </td>
            <td class="style42">
                &nbsp;
                <asp:TextBox ID="txtWoValidity" runat="server" placeholder="Work Order Validity"
                    Width="150px" AutoPostBack="true"></asp:TextBox>
                <cc1:CalendarExtender ID="txtWoValidity_CalendarExtender" runat="server" Enabled="True"
                    Format="dd/MM/yyyy" TargetControlID="txtWoValidity">
                </cc1:CalendarExtender>
            </td>
            <td class="style40">
            </td>
            <td class="style42">
            </td>
        </tr>
    </table>
    <br />
    <div runat="server" id="PmtAddressDiv">
        <div class="header_03">
            Parmanent Address:
        </div>
        <div class="margin_bottom_10 border_bottom">
        </div>
        <table class="tbl-form">
            <tr>
                <td class="style40">
                    <asp:Label ID="lblpmtadd" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="Permanent Address :"></asp:Label>
                </td>
                <td class="style42">
                    <asp:TextBox ID="txtPmtAdd" CssClass="txtView" runat="server" Width="242px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td class="style40">
                    <asp:Label ID="lblTaluka" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="Taluka :"></asp:Label>
                </td>
                <td class="style42">
                    <asp:TextBox ID="txtTaluka" CssClass="txtView" runat="server" Width="150px"></asp:TextBox>
                </td>
                <td class="style40">
                    <asp:Label ID="lblDist" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="District :"></asp:Label>
                </td>
                <td class="style42">
                    <asp:TextBox ID="txtDistrict" CssClass="txtView" runat="server" Width="150px"></asp:TextBox>
                </td>
                <td class="style35">
                    <asp:Label ID="lblstate" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="State :"></asp:Label>
                </td>
                <td class="style42">
                    <asp:DropDownList ID="ddlstate" runat="server" Width="150px" CssClass="txtView">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style40">
                    <asp:Label ID="lblPin" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="Pin :"></asp:Label>
                </td>
                <td class="style42">
                    <asp:TextBox ID="txtPin" onkeypress="validatenumerics(event)" CssClass="txtView"
                        runat="server" Width="150px"></asp:TextBox>
                </td>
                <td class="style37">
                    <asp:Label ID="lblnationality" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="Nationality :"></asp:Label>
                </td>
                <td class="style42">
                    <asp:DropDownList ID="ddlNationality" runat="server" Width="150px" CssClass="txtView">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div runat="server" id="ApplicationDiv">
        <div class="header_03">
            Application :
        </div>
        <div class="margin_bottom_10 border_bottom">
        </div>
        <table>
            <tr>
                <td class="style40">
                    <asp:Label ID="lblapplicationno" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="Application No :"></asp:Label>&nbsp;
                </td>
                <td class="style42">
                    &nbsp;
                    <asp:TextBox ID="txtapplicationno" CssClass="txtView" runat="server" Width="150px"
                        placeholder="Application No"></asp:TextBox>
                </td>
                <td class="style35">
                    &nbsp;
                    <asp:Label ID="lblattchapp" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="Attach Application :"></asp:Label>
                </td>
                <td class="style42">
                    &nbsp;
                    <asp:FileUpload runat="server" ID="attachappno" />
                </td>
                <td class="style42">
                    &nbsp;
                    <asp:Button runat="server" Text="ADD APPLICATION" ID="btnapplicationlist" OnClick="btnapplicationlist_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <asp:GridView ID="Gv_AppPhoto" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                            SkinID="grid1" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                            AlternatingRowStyle-BackColor="#ffffdd" CellPadding="3" DataKeyNames="APP_ID"
                            Visible="true"  >
                            <Columns>
                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="APPLICATION NUMBER">
                                    <ItemTemplate>
                                        <asp:Label ID="lblappnum" runat="server" Text='<%#Eval("APP_NUMBER") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtappnum" runat="server" Text='<%#Eval("APP_NUMBER") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="APPLICATION PHOTO">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="img_AppPhoto" Text="VIEW" OnClick="img_AppPhoto_Click" />
                                        <asp:HiddenField runat="server" ID="hdn_datakey" Value='<% #Eval("APP_ID")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DOWNLOAD APPLICATION PHOTO">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="Lnk_DownloadApp" Text="DOWNLOAD" OnClick="Lnk_DownloadApp_Click" />
                                        <asp:HiddenField runat="server" ID="hdn_datakey1" Value='<% #Eval("APP_ID")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="Lnk_DeleteApp" Text="Delete" OnClick="Lnk_DeleteApp_Click" />
                                        <asp:HiddenField runat="server" ID="hdn_datakey2" Value='<% #Eval("APP_ID")%>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div id="ValidityDiv" runat="server">
        <div class="header_03">
            Validity :
        </div>
        <div class="margin_bottom_10 border_bottom">
        </div>
        <table>
            <tr>
                <td class="style37">
                    <asp:Label ID="lblpvcno" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="PVC No :"></asp:Label>&nbsp;
                </td>
                <td class="style42">
                    <asp:DropDownList ID="ddlPvcYN" Width="150px" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="--SELECT--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                        <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtpvcno" CssClass="txtView" runat="server" Width="150px" MaxLength="150"
                        placeholder="PVC No"></asp:TextBox>
                </td>
                <td class="style39">
                    <asp:Label ID="lblpvcvalidty" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="PVC Validity :"></asp:Label>
                    &nbsp;
                </td>
                <td class="style42">
                    <asp:TextBox ID="txtpvcValidity" runat="server" placeholder="PVC Validity" Width="150px"
                        AutoPostBack="true"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtpvcValidity_CalendarExtender" runat="server" Enabled="True"
                        Format="dd/MM/yyyy" TargetControlID="txtpvcValidity">
                    </cc1:CalendarExtender>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style39">
                    <asp:Label ID="lblrfid" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="RFID No :"></asp:Label>
                    &nbsp;
                </td>
                <td class="style42">
                    &nbsp;
                    <asp:TextBox ID="txtrfidno" CssClass="txtView" runat="server" Width="150px" MaxLength="150"
                        placeholder="RFID No"></asp:TextBox>
                </td>
                <td class="style39">
                    <asp:Label ID="lblrfidvalidity" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="RFID Validity :"></asp:Label>
                    &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="txtrfidValidity" runat="server" placeholder="RFID Validty" Width="150px"
                        AutoPostBack="true"></asp:TextBox>
                    <cc1:CalendarExtender ID="rfidValidity_CalendarExtender" runat="server" Enabled="True"
                        Format="dd/MM/yyyy" TargetControlID="txtrfidValidity">
                    </cc1:CalendarExtender>
                </td>
            </tr>
        </table>
        <center>
            <table>
                <tr>
                    <td>
                        <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                            background-repeat: no-repeat">
                            <asp:Button ID="btnUpdate" Text="RENEW PASS" Visible="false" runat="server" OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnModify" Text="SAVE DATA" runat="server" OnClick="btnModify_Click" />
                        </div>
                    </td>
                    <td>
                        <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                            background-repeat: no-repeat">
                            <asp:Button ID="btnReset" Text="RESET" runat="server" OnClick="btnReset_Click" />
                        </div>
                    </td>
                    <td>
                        <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                            background-repeat: no-repeat">
                            <asp:Button ID="Button1" Text="PRINT" runat="server" OnClick="Button1_Click" Visible="False" />
                        </div>
                    </td>
                </tr>
            </table>
        </center>
        <div class="header_03">
            Finger Download/Upload :
        </div>
        <div class="margin_bottom_10 border_bottom">
        </div>
        <table width="100%" border="0">
            <tr>
                <td class="style39" colspan="5" align="center">
                    <asp:Label ID="Label1" runat="server" Style="font-size: medium; font-family: Calibri;"
                        Text="Download Device :"></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td rowspan="1" align="center" colspan="5">
                    <asp:DropDownList ID="ddlDownload" runat="server" Style="font-weight: 700" OnSelectedIndexChanged="ddlDownload_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="20%">
                </td>
                <td colspan="1" width="25%" align="center">
                    <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                        background-repeat: no-repeat; width: 150px">
                        <asp:Button ID="AddFig" runat="server" Text="ADD FINGER" OnClick="AddFig_Click" />
                    </div>
                </td>
                <td>
                    <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png');
                        background-repeat: no-repeat; width: 250px">
                        <asp:Button ID="Btndownlaod" runat="server" Text="DOWNLOAD BIOMETRIC FINGER" OnClick="Btndownlaod_Click" />
                    </div>
                </td>
                <td width="25%">
                </td>
            </tr>
            <tr>
                <td colspan="4" width="50%" align="center">
                    <div id="DDeaciiveSuccess" runat="server" visible="false" style="color: Green; font-size: small">
                        <b>DOWNLOAD TEMPLATE SUCESSFULLY</b>
                    </div>
                    <div id="DDeaciiveFail" runat="server" visible="false" style="color: Red; font-size: small">
                        <b>DOWNLOAD TEMPLATE FAIL</b>
                    </div>
                    <div id="LblDownload" runat="server" visible="false" style="color: Red; font-size: small">
                        <b>PLEASE DOWNLOAD TEMPLATE </b>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
