﻿<%@ Page Title="" Language="C#" MasterPageFile="~/VMSMaster.master" AutoEventWireup="true"
    CodeBehind="LABOUR.aspx.cs" Inherits="EntityFrameworkDBF.ISSUES_MODULE.LABOUR"
    Theme="Admin_Basic" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <link href="../Scripts/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.min.js" type="text/javascript"></script>
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
    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        .auto-style2
        {
            height: 40px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main" runat="server">
    <center>
        <div class="header_02">
            DVSC : PASS MANAGEMENT SYSTEM - <span id="spnDate" runat="server"></span>
            <br />
            <br />
            LABOUR PASS
        </div>
        <div style="padding-left: 600px">
            <asp:Button ID="btnhome" runat="server" Text="BACK TO HOME" CssClass="button" OnClick="btnhome_Click" />
            <asp:Button ID="btnpnlhome" runat="server" Text="BACK TO ISSUE MODULE" CssClass="button"
                OnClick="btnpnlhome_Click" Visible="false" />
        </div>
    </center>
    <div class="header_03">
       LABOUR DEATILS :        
    </div>
    <div class="margin_bottom_10 border_bottom">
    </div>
    <table class="tbl-form">
        <tr>
            <td colspan="4">&nbsp;
                <asp:Label ID="lblContID" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Visible="false"></asp:Label>
            </td>
            <td style="text-align: center;">&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDocid" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="Dockyard Id :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDockyardID" runat="server" placeholder="Enter Docyard ID" CssClass="txtView"
                    Width="150px" onkeydown="return CheckFirstChar(event.keyCode, this);" onkeypress="return alphabeticCheck(event)"
                    OnTextChanged="txtDockyardID_TextChanged"></asp:TextBox>
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
                <span style="color: Red;">* </span>&nbsp;<asp:Label ID="lbldob" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="DOB :" ToolTip="Date Of Birth"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDateOfBirth" runat="server" placeholder="Date Of Birth" Width="150px"
                    AutoPostBack="true"></asp:TextBox>
                <cc1:CalendarExtender ID="txtDateOfBirth_CalendarExtender" runat="server" Enabled="True"
                    Format="dd/MM/yyyy" TargetControlID="txtDateOfBirth">
                </cc1:CalendarExtender>
            </td>
            <td rowspan="3">
                <asp:Panel ID="pnl" runat="server">
                    <object style="height: 160px; width: 250px">
                        <param name="movie" value="WebcamResources/save_picture.swf" />
                        <embed height="180" src="../WebcamResources/save_picture.swf" width="380"></embed>
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
                <asp:Label ID="lblaadhaar" runat="server" Text="Aadhaar No :" Style="font-size: medium; font-family: Calibri;"></asp:Label>
            </td>
            <td class="">
                <asp:TextBox ID="txtAADHAAR" runat="server" placeholder="Aadhaar Number" Width="150px"
                    MaxLength="12" onkeypress="validatenumerics(event)" CssClass="txtView" OnTextChanged="txtAADHAAR_TextChanged"
                    AutoPostBack="true"></asp:TextBox>
            </td>
            <td class="">
                <span style="color: Red;">*</span>
                <asp:Label ID="lbldesignation" Style="font-size: medium; font-family: Calibri;" runat="server"
                    Text="Designation:"></asp:Label>
            </td>
            <td class="">&nbsp;
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
            <td class="">&nbsp;
                <asp:TextBox ID="txtContactNo" CssClass="" runat="server" Width="150px" MaxLength="10"
                    placeholder="Mobile Number" onkeypress="validatenumerics(event)"></asp:TextBox>
            </td>
            <td class="">
                <span style="color: Red;">*</span>
                <asp:Label ID="lblReligion" runat="server" Text="Religion :" Style="font-size: medium; font-family: Calibri;"></asp:Label>
            </td>
            <td class="">&nbsp;
                <asp:DropDownList ID="ddlReligion" Width="150px" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
                <span style="color: Red;">*</span>
                <asp:Label ID="lblOther" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="Other Document :"></asp:Label>
            </td>
            <td class="auto-style2">&nbsp;
                <asp:DropDownList ID="ddlother" CssClass="" runat="server" Width="150px">
                </asp:DropDownList>
            </td>
            <td class="auto-style2">
                <span style="color: Red;">*</span>
                <asp:Label ID="lblOtherDoc" runat="server" placeholder="document number" Style="font-size: medium; font-family: Calibri;"
                    Text="Document No :"></asp:Label>
            </td>
            <td class="auto-style2">
                <asp:TextBox ID="txtOtherDoc" CssClass="txtView" runat="server" Width="150px" AutoPostBack="true"
                    OnTextChanged="txtOtherDoc_TextChanged"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
                <span style="color: Red;">*</span>
                <asp:Label ID="lblGender" runat="server" Text="Gender :" Style="font-size: medium; font-family: Calibri;"></asp:Label>
            </td>
            <td class="auto-style2">&nbsp;
                <asp:DropDownList ID="ddlGender" Width="150px" runat="server">
                </asp:DropDownList>
            </td>
            <td class="auto-style2"></td>
            <td class="auto-style2"></td>
        </tr>
    </table>
    <br />
    <div class="header_03">
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
                <asp:TextBox ID="txtfirmfileno" CssClass="txtView" runat="server" Width="150px" OnTextChanged="txtfirmfileno_TextChanged"
                    AutoPostBack="true"></asp:TextBox>
                &nbsp;
            </td>
            <td class="style34">
                <asp:Label ID="lblfirm" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="Firm Name :"></asp:Label>
            </td>
            <td class="style41">
                <asp:DropDownList ID="ddlFirm" runat="server" Width="150px" CssClass="txtView" OnSelectedIndexChanged="ddlFirm_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
            </td>
            <td class="style36">
                <asp:Label ID="lblgst" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="GST No :"></asp:Label>
            </td>
            <td class="style41">
                <asp:TextBox ID="txtgst" CssClass="txtView" placeholder="GST No" runat="server" Width="150px"
                    MaxLength="20"></asp:TextBox>
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
                <asp:Label ID="Label11" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="Work Order No :"></asp:Label>
            </td>
            <td class="style42">
                <asp:TextBox ID="txtFirmWorkOrderNo" CssClass="" runat="server" Width="150px"></asp:TextBox>
            </td>
            <td class="style35">
                <asp:Label ID="lblwovalidity" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="Work Order Validity:"></asp:Label>
            </td>
            <td class="style42">&nbsp;
                <asp:TextBox ID="txtWoValidity" runat="server" placeholder="Work Order Validity"
                    Width="150px" AutoPostBack="true"></asp:TextBox>
                <cc1:CalendarExtender ID="txtWoValidity_CalendarExtender" runat="server" Enabled="True"
                    Format="dd/MM/yyyy" TargetControlID="txtWoValidity">
                </cc1:CalendarExtender>
            </td>
            <td class="style40">&nbsp;</td>
            <td class="style42">&nbsp;</td>
        </tr>
    </table>
    <br />
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
            <td class="style37">
                <asp:Label ID="lblnationality" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="Nationality :"></asp:Label>
            </td>
            <td class="style42">
                <asp:DropDownList ID="ddlNationality" runat="server" Width="150px" CssClass="txtView">
                </asp:DropDownList>
            </td>
            <td class="style35">
                <asp:Label ID="lblstate" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="State :"></asp:Label>
            </td>
            <td class="style42">
                <asp:DropDownList ID="ddlstate" runat="server" Width="150px" CssClass="txtView">
                </asp:DropDownList>
            </td>
            <td class="style40">
                <asp:Label ID="lblDist" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="District :"></asp:Label>
            </td>
            <td class="style42">
                <asp:TextBox ID="txtDistrict" CssClass="txtView" runat="server" Width="150px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style40">
                <asp:Label ID="lblTaluka" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="Taluka :"></asp:Label>
            </td>
            <td class="style42">
                <asp:TextBox ID="txtTaluka" CssClass="txtView" runat="server" Width="150px"></asp:TextBox>
            </td>
            <td class="style40">
                <asp:Label ID="lblPin" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="Pin :"></asp:Label>
            </td>
            <td class="style42">
                <asp:TextBox ID="txtPin" onkeypress="validatenumerics(event)" CssClass="txtView"
                    runat="server" Width="150px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
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
            <td class="style42">&nbsp;
                <asp:TextBox ID="txtapplicationno" CssClass="txtView" runat="server" Width="150px"
                    placeholder="Application No"></asp:TextBox>
            </td>
            <td class="style35">&nbsp;
                <asp:Label ID="lblattchapp" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="Attach Application :"></asp:Label>
            </td>
            <td class="style42">&nbsp;
                <asp:FileUpload runat="server" ID="attachappno" />
            </td>
            <td class="style42">&nbsp;
                <asp:Button runat="server" Text="ADD APPLICATION" ID="btnapplicationlist" OnClick="btnapplicationlist_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <asp:GridView ID="Gv_AppPhoto" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                        SkinID="grid1" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                        AlternatingRowStyle-BackColor="#ffffdd" CellPadding="3" DataKeyNames="APP_ID"
                        Visible="true">
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
                        </Columns>
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
    <br />
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
            <td>
                <asp:DropDownList ID="ddlPvcYN" Width="150px" runat="server">
                    <asp:ListItem Text="--SELECT--" Value="0"></asp:ListItem>
                    <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                    <asp:ListItem Text="NO" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style42">&nbsp;

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
            <td class="style42">&nbsp;
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
            <td class="style39">
                <asp:Label ID="lblEntryValidity" runat="server" Style="font-size: medium; font-family: Calibri;"
                    Text="Entry Valid Till :"></asp:Label>
                &nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtEntryValidity" runat="server" placeholder="RFID Validty" Width="150px"
                    AutoPostBack="true"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                    Format="dd/MM/yyyy" TargetControlID="txtEntryValidity">
                </cc1:CalendarExtender>
            </td>
        </tr>
    </table>
    <center>
        <table class="">
            <tr>
                <td>
                    <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png'); background-repeat: no-repeat">
                        <asp:Button ID="btnsave" runat="server" Text="SUBMIT" OnClick="btnsave_Click" />
                    </div>
                </td>
                <td>
                    <div class="btnView" style="background-image: url('../images2/templatemo_button_02.png'); background-repeat: no-repeat">
                        <asp:Button ID="btncancel" runat="server" Text="RESET" OnClick="btncancel_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
