<%--<%-
ProjectName :-  IDMS
ModuleName:-    Advance Right Management
Page Tittle:-  Change Analysis Ownership 
Created on :-
Created By:-   Vikas & Jitendra

- %>--%>

<%@ Page Language="VB" AutoEventWireup="false"  MasterPageFile ="~/MasterPage/MasterPage.master"CodeFile="ChangeAnalysisOwnership.aspx.vb" Inherits="AdvanceRightsManagement_ChangeAnalysisOwnership" %>


<%@ Register Src="~/AdvanceRightsManagement/UserConrol/DCLUserControl.ascx" TagName="DCLUserControl"
    TagPrefix="uc1" %>
<asp:Content ID="lftplaceholder" runat="server" ContentPlaceHolderID="LeftPlaceHolder">
    <table summary="">
        <tr>
            <td style="width: 206px;" scope="col">
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="ChangeAnalysisOwnership" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <table class="table" summary="used to contain controls for Change Analysis Ownership">
        <caption class="caption" style ="background-color:#0591D3">
            Change Analysis Ownership</caption>
        <tr>
        </tr>
        <tr>
        </tr>
        <tr>
            <td  scope="col">
                <uc1:DCLUserControl ID="DCLUserControl1" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DCLUserControl1$ddlDepartment"
                    ErrorMessage="Select Department" ForeColor="White" InitialValue="--Select--"
                    SetFocusOnError="True" ValidationGroup="report">*</asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="report" />
            </td>
            <td  scope="col">
                <uc1:DCLUserControl ID="DCLUserControl2" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DCLUserControl2$ddlDepartment"
                    ErrorMessage="Select Department" ForeColor="White" InitialValue="--Select--"
                    SetFocusOnError="True" ValidationGroup="user">*</asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="user" />
            </td>
        </tr>
        <tr>
            <td scope="col" >
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;<asp:Button ID="btnGetAnalysis" runat="server" CssClass="button" Text="Get Analysis"
                    ToolTip="Click to get Analysis" ValidationGroup="report" />
            </td>
            <td  scope="col">
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                 <asp:Button ID="btnGetUser" runat="server" CssClass="button" Text="GetUser" ToolTip="Click to get Users"
                    />
            </td>
        </tr>
        <tr>
        </tr>
        <tr>
        </tr>
        <tr>
            <td  scope="col" style ="color:Black">
                <asp:Label ID="lblSelectAnalysis" runat="server" AssociatedControlID="ddlSelectAnalysis"
                    CssClass="label" ToolTip="Select Analysis" Width="125px">Select Analysis</asp:Label>
                <asp:DropDownList ID="ddlSelectAnalysis" runat="server" AutoPostBack="True" CssClass="dropdownlist"
                    ToolTip="Select Analysis">
                </asp:DropDownList>&nbsp;
            </td>
            <td  scope="col" style ="color:Black">
                <asp:Label ID="Label1" runat="server" AssociatedControlID="ddlNewOwner" CssClass="label"
                    ToolTip="Select user to make owner">New Owner</asp:Label>&nbsp; &nbsp;<asp:DropDownList ID="ddlNewOwner"
                        runat="server" AutoPostBack="True" CssClass="dropdownlist" ToolTip="Select New Owner ">
                    </asp:DropDownList>&nbsp;
            </td>
        </tr>
        <tr>
        </tr>
        <tr>
            <td  scope="col" style ="color:Black">
                <asp:Label ID="lblPreviousOwner" runat="server" AssociatedControlID="tbxPreviousOwner"
                    CssClass="label" ToolTip="Previous owner shown here">Last  Owner</asp:Label>
                &nbsp;&nbsp; &nbsp;
                <asp:TextBox
                        ID="tbxPreviousOwner" runat="server" BackColor="White" CssClass="textbox" Enabled="False"
                        ReadOnly="True" ToolTip="Previous Owner of selectd Analysis"></asp:TextBox></td>
            <td  scope="col">
                &nbsp;</td>
        </tr>
        <tr></tr>
        <tr></tr>
        <tr>
            <td align="center" colspan="2" scope="colgroup">
                <asp:Button ID="btnChange" runat="server" CssClass="button" Text="Change Owner" ToolTip="Click to Change ownership of Analysis" />
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
