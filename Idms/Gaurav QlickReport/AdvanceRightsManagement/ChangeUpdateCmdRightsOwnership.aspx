<%--<%-
ProjectName :-  IDMS
ModuleName:-    Advance Right Management
Page Tittle:-  Change Update Command Ownership form
Created on :-
Created By:-   Vikas & Jitendra

- %>--%>

<%@ Page AutoEventWireup="false" CodeFile="ChangeUpdateCmdRightsOwnership.aspx.vb"
    Inherits="AdvanceRightsManagement_ChangeUpdateCmdRightsOwnership" Language="VB"
    MasterPageFile="~/MasterPage/MasterPage.master"%>

<%@ Register Src="~/AdvanceRightsManagement/UserConrol/DCLUserControl.ascx" TagName="DCLUserControl"
    TagPrefix="uc1" %>
<asp:Content ID="lftplaceholder" runat="server" ContentPlaceHolderID="LeftPlaceHolder">
    <table>
        <tr>
            <td style="width: 206px;">
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="ChangeUpdateCmdOwnership" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table class="table" summary="used to contain controls for Change Update command Ownership">
    <caption class ="caption" style ="background-color:#0591D3">Change UpdateCommand Ownership</caption>
        <tr>
           <!-- <td align="center" colspan="2">
                <asp:Label ID="lblChangeOwner" runat="server" CssClass="tableHeader" Text="Change UpdateCommand Ownership "
                    Width="556px"></asp:Label>
            </td>-->
        </tr>
        <tr>
            <td colspan="1">
                <uc1:DCLUserControl ID="DCLUserControl1" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DCLUserControl1$ddlDepartment"
                    ErrorMessage="Select Department" ForeColor="White" InitialValue="--Select--"
                    SetFocusOnError="True" ValidationGroup="cmd">*</asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="cmd" />
            </td>
            <td colspan="1">
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
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                <asp:Button ID="btnGetUpdateCommnad" runat="server" CssClass="button"
                    Text="Get Cmd"  ToolTip ="click to get cmd" ValidationGroup="cmd"/>
            </td>
            <td >
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp;<asp:Button ID="btnGetUser" runat="server" CssClass="button"
                    Text="GetUser" ToolTip ="Click to get Users" />
            </td>
        </tr>
        <tr></tr>
        <tr>
            <td colspan="1" style ="color:black">
                <asp:Label ID="lblSelectView" runat="server" AssociatedControlID="ddlSelectCmd"
                    CssClass="label">Select Cmd</asp:Label>&nbsp;
                <asp:DropDownList ID="ddlSelectCmd" runat="server" CssClass="dropdownlist" ToolTip="Select View" AutoPostBack="True">
                </asp:DropDownList>&nbsp;
            </td>
            <td colspan="1" style ="color:black">
                <asp:Label ID="Label1" runat="server" AssociatedControlID="ddlNewOwner" CssClass="label">New Owner</asp:Label>&nbsp;
                <asp:DropDownList ID="ddlNewOwner" runat="server" CssClass="dropdownlist" ToolTip="Select New Owner ">
                </asp:DropDownList>&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="1" style ="color:black">
                <asp:Label ID="lblPreviousOwner" runat="server" AssociatedControlID="tbxPreviousOwner"
                    CssClass="label">Last  Owner</asp:Label>&nbsp;<asp:TextBox ID="tbxPreviousOwner" runat="server"
                        ToolTip="Previous Owner" ReadOnly="True" BackColor="White" CssClass="textbox"></asp:TextBox></td>
            
        </tr>
        <tr></tr>
        <tr >        
        <td   align ="center" colspan ="2">
        <asp:Button ID="btnChange" runat="server" CssClass="button" Text="Change Owner" ToolTip="Click to change ownership of update command" ValidationGroup="final" />
        </td>
        <td>
        </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdUserId" runat="server" />
    <!-- this control is used to contain userid that can be later used for various purposes -->
</asp:Content>
<%--<%-

Changed on :-
Created By:-   Vikas & Jitendra

- %>--%>
