<%--<%-
ProjectName :-  IDMS
ModuleName:-    Advance Right Management
Page Tittle:-  Change Table Ownership form
Created on :-
Created By:-   Vikas & Jitendra

- %>--%>

<%@ Page AutoEventWireup="false" CodeFile="ChangeTableOwnership.aspx.vb" Inherits="ChangeOwnership"
    Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" %>

<%@ Register Src="~/AdvanceRightsManagement/UserConrol/DCLUserControl.ascx" TagName="DCLUserControl" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="LeftPlaceHolder" ID="lftplaceholder" runat="server">
<table>
<tr>
<td style="width:206px;"></td></tr></table>
</asp:Content>
<asp:Content ID="ChangeOwner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table class="table" summary ="used to contain controls for Change table ownership" cellspacing ="5">
    <caption class ="caption" style ="background-color:#0591D3">Change Table Ownership</caption>
       
        <tr>
            <td >
                <uc1:DCLUserControl ID="DCLUserControl1" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DCLUserControl1$ddlDepartment"
                    ErrorMessage="Select Department" ForeColor="White" InitialValue="--Select--"
                    SetFocusOnError="True" ValidationGroup="table">*</asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="table" />
            </td>
            <td >
                <uc1:DCLUserControl ID="DCLUserControl2" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DCLUsercontrol2$ddlDepartment"
                    ErrorMessage="Select Department" ForeColor="White" InitialValue="--Select--"
                    SetFocusOnError="True" ValidationGroup="user">*</asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="user" />
            </td>
        </tr>
        <tr>
        <td  align ="center" style="width: 312px" >
        <asp:Label ID="lblspacer" runat ="server"  CssClass  ="label" Visible="False" ></asp:Label>
        <asp:Button ID="btnGetTable" Text="Get Table" runat="server" CssClass="button"  ToolTip ="Click to get table(alt+t)" AccessKey="t" ValidationGroup="table"/>
        </td>
        <td  align ="center" style="width: 297px" >
            <asp:Label ID="Label2" runat="server" cssclass="label" Visible="False"></asp:Label>
        <asp:Button ID="btnGetUser" Text="GetUser" runat="server" CssClass="button" ToolTip ="click to get user(alt+g)" AccessKey="g" />
        </td>
        </tr>
        <tr></tr>
        <tr>
            <td style ="color:black">
                  <asp:Label ID="lblSelectTable" runat="server" CssClass="label" AssociatedControlID="ddlSelectTable" Width="134px">Select Table</asp:Label>
                <asp:DropDownList ID="ddlSelectTable" runat="server" CssClass="dropdownlist"
                    ToolTip="Select Table" AutoPostBack="True">
                </asp:DropDownList>&nbsp;
                    
            </td>
            <td style ="color:black" >
                <asp:Label ID="Label1" runat="server" CssClass="label" AssociatedControlID="ddlNewOwner">New Owner</asp:Label>&nbsp;
                <asp:DropDownList ID="ddlNewOwner" runat="server" CssClass="dropdownlist"
                    ToolTip="Select New Owner " AutoPostBack="True">
                </asp:DropDownList>&nbsp;
                    
            </td>
        </tr>
        <tr></tr>
       
        <tr>
            <td style ="color:black">
               
                <asp:Label ID="lblPreviousOwner" runat="server" CssClass="label" AssociatedControlID="tbxPreviousOwner" Width="167px">Last Owner</asp:Label>&nbsp;&nbsp;
                 
                <asp:TextBox ID="tbxPreviousOwner" runat="server" ToolTip="Previous Owner" ReadOnly="True" CssClass="textbox"></asp:TextBox></td>
            </tr>
            <tr></tr>
            <tr>
            <td align="center"  colspan ="2"   >
                <asp:Button ID="btnChange" runat="server" CssClass="button" Text="Change Owner" />&nbsp;
            </td>
            <td></td>
        </tr>
    </table>
    <asp:HiddenField ID="hdUserId" runat="server" />
    <!-- this control is used to contain userid that can be later used for various purposes -->
</asp:Content>
<%--<%-

Changed on :-
Created By:-   Vikas & Jitendra

- %>--%>
