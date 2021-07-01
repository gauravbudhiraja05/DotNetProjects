<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="SetDefPass.aspx.vb" Inherits="Accounts_SetDefPass" title="Set Default Password" %>

<%--Project Name: IDMS Phase 2
    Module Name: Accounts Management
    Page Name: Set Default Password
    Summary: Sets A Default Password
    Created on: 10/05/08
    Created By: Yogesh Kumar Verma

--%>
<asp:Content ID="Content1" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" Runat="Server">
    <table class="table" border="0" id="Table2" summary="Purpose of this form:Reset User Password, Unlock/Unlock Account, Set Default Password, Set Default Password" cellpadding="0" cellspacing="0"  style="background-color:#cccccc">
        <tr>
            <td style="background-color:#CC6666; height:22px; width:10px" scope="col"></td>
            <td style="background-color:#CC6666; height:22px; width:15px" align="left" scope="col">
                <img src="../images/LockLogo.jpg" alt="LockLogo Image"/>
            </td>
            <td style="background-color:#CC6666; height:22px" valign="bottom" align="right" scope="col"><span style="font-size: 12pt; color:White;"><strong>Set Default Password &nbsp;&nbsp;&nbsp;</strong></span></td>
            <td style="background-color:#CC6666; height:22px; width:1px" scope="col"></td>
        </tr>
        <tr><td style="background-color:#df9f9f; height:5px" colspan="4" scope="colgroup"></td></tr>
    <tr><td colspan="2" align="center" style="height:20px" scope="col"></td></tr>
    <tr>
    <td colspan="2" align="right" style="height: 26px" scope="col"><label style="background-color:#cccccc" class="label" for="ctl00_MainPlaceHolder_txtSetPassword" title="Department">Set Default Password &nbsp;&nbsp;&nbsp;&nbsp;</label></td>
    <td colspan="2" style="height: 26px" scope="col">
   <%-- <asp:TextBox runat="server" ID="txtDefPass" Text=""></asp:TextBox>--%>
    <asp:TextBox  CssClass="dropdownlist" ID="txtSetPassword" runat="server" ToolTip="Enter Password"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSetPassword"
            ErrorMessage="Password Required" ></asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr><td colspan="2" scope="colgroup" align="center" style="height:20px"></td><td><asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtSetPassword" ValidationExpression="^.*(?=.{8,})(?=.*\d)(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[@#$%^&+=]).*$" runat="server" ErrorMessage="Enter 8-15 Characters in Length (Mix Of Alphabetic, Non Alphabetic & Special Characters)" SetFocusOnError="True" Display="dynamic"></asp:RegularExpressionValidator></td></tr>
    <tr><td colspan="2" scope="colgroup"></td><td colspan="2"><asp:Button runat="server" ID="btnSave" cssclass="button"  Text="Set Password" ToolTip="Set Password" /></td></tr>
    <tr><td colspan="2" align="center" style="height:20px" scope="colgroup"></td></tr>
    </table>
    
</asp:Content>

