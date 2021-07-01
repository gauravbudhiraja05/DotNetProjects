<%--<%-
ProjectName :-  IDMS
ModuleName:-    Advance Right Management
Page Tittle:-   Assign Table Rights
Created on :-   05/04/08 
Created By:-   Vikas & Jitendra

- %>--%>
<%@ Control AutoEventWireup="false" CodeFile="DCLUserControl.ascx.vb" Inherits="DCLUserControl"
    Language="VB" %>
<table summary ="used to create user control inside it">
    <tr>
        <td align ="left" style ="color:black" >
            <asp:Label ID="lblDepartment" runat="server" AccessKey="d" AssociatedControlID="ddlDepartment"
                CssClass="label" Text="Department" ToolTip="Select Department"></asp:Label>
        </td>
        <td title ="Select department">
            <asp:DropDownList ID="ddlDepartment" runat="server" AccessKey="d" AutoPostBack="True"
                CssClass="dropdownlist" ToolTip="Select Department">
            </asp:DropDownList>
         </td>
    </tr>
    <tr>
        <td align ="left" style ="color:black">
            <asp:Label ID="lblClient" runat="server" AccessKey="c" AssociatedControlID="ddlClient"
                CssClass="label" Text="Client" ToolTip ="select Client"></asp:Label>
        </td>
        <td title ="Select client">
            <asp:DropDownList ID="ddlClient" runat="server" AccessKey="c" AutoPostBack="True"
                CssClass="dropdownlist" ToolTip="Select Client">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td align ="left" style ="color:black" >
            <asp:Label ID="lblLob" runat="server" AccessKey="l" AssociatedControlID="ddlLob"
                CssClass="label" Text="LOB" ToolTip ="Select LOB"></asp:Label>
        </td>
        <td title ="Select LOB">
            <asp:DropDownList ID="ddlLob" runat="server" AccessKey="l" AutoPostBack="True" CssClass="dropdownlist" ToolTip="Select LOB">
            </asp:DropDownList></td>
    </tr>
</table>
<%--<%-

Changed on :-
Created By:-   Vikas & Jitendra

- %>--%>
