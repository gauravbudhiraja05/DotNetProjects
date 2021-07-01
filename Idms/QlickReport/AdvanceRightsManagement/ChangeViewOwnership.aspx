<%--<%-
ProjectName :-  IDMS
ModuleName:-    Advance Right Management
Page Tittle:-  Change view Ownership form
Created on :-
Created By:-   Vikas & Jitendra

- %>--%>

<%@ Page AutoEventWireup="false" CodeFile="ChangeViewOwnership.aspx.vb" Inherits="AdvanceRightsManagement_ViewRightsManagement_ChangeViewOwner"
    Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" %>

<%@ Register Src="~/AdvanceRightsManagement/UserConrol/DCLUserControl.ascx" TagName="DCLUserControl" TagPrefix="uc1" %>
<asp:Content ID="lftplaceholder" runat="server" ContentPlaceHolderID="LeftPlaceHolder">
    <table>
        <tr>
            <td style="width: 206px;">
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="ChangeViewOwnership" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table class="table" summary="used to contain controls for Change View Ownership">
    <caption class ="caption" style ="background-color:#0591D3" >Change View Ownership</caption>
        
        <tr></tr>
        <tr></tr>
        <tr>
            <td >
                <uc1:DCLUserControl ID="DCLUserControl1" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DCLUserControl1$ddlDepartment"
                    ErrorMessage="Select Department" ForeColor="White" InitialValue="--Select--"
                    SetFocusOnError="True" ValidationGroup="view">*</asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="view" />
            </td>
            <td  >
             <uc1:DCLUserControl ID="DCLUserControl2" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DCLUserControl2$ddlDepartment"
                    ErrorMessage="Select Department" ForeColor="White" InitialValue="--Select--"
                    SetFocusOnError="True" ValidationGroup="user">*</asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="user" />
            </td>
        </tr>
        <tr>
            <td >
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="btnGetView" runat="server" CssClass="button"
                    Text="GetView" ValidationGroup="view" />
            </td>
            <td >
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                <asp:Button ID="btnGetUser" runat="server" CssClass="button"
                    Text="GetUser" ToolTip="Click to get Users"  />
            </td>
        </tr>
        <tr></tr>
        <tr>
            <td style ="color:black">
                <asp:Label ID="lblSelectView" runat="server" CssClass="label" AssociatedControlID="ddlSelectView">Select View</asp:Label>
                &nbsp;
                <asp:DropDownList ID="ddlSelectView" runat="server" CssClass="dropdownlist" ToolTip="Select View" AutoPostBack="True">
                </asp:DropDownList>&nbsp;
                    
            </td>
            <td style ="color:black">
                <asp:Label ID="Label1" runat="server" CssClass="label" AssociatedControlID="ddlNewOwner">New Owner</asp:Label>&nbsp;
                &nbsp;<asp:DropDownList ID="ddlNewOwner" runat="server" CssClass="dropdownlist" ToolTip="Select New Owner ">
                </asp:DropDownList>&nbsp;
                    
            </td>
        </tr>
        <tr>
            <td style ="color:black">
                <asp:Label ID="lblPreviousOwner" runat="server" CssClass="label" AssociatedControlID="tbxPreviousOwner">Last Owner</asp:Label>&nbsp;&nbsp;&nbsp;<asp:TextBox
                    ID="tbxPreviousOwner" runat="server" ToolTip="Last Owner" ReadOnly="True" BackColor="White" CssClass="textbox"></asp:TextBox></td>            
        </tr>
        <tr></tr>
        <tr></tr>
        <tr>
            <td align="center" colspan ="2">
                <asp:Button ID="btnChange" runat="server" CssClass="button" Text="Change Owner" ToolTip="click to change ownership of view"
                    ValidationGroup="final" />&nbsp;
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
