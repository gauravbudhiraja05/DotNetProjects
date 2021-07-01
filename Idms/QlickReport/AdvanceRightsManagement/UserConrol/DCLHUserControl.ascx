<%--<%-
ProjectName :-  IDMS
ModuleName:-    Advance Right Management
Page Tittle:-   Assign Table Rights
Created on :-   
Created By:-   Vikas & Jitendra

- %>--%>
<%@ Control AutoEventWireup="false" CodeFile="DCLHUserControl.ascx.vb" Inherits="AdvanceRightsManagement_UserConrol_DCLHUserControl"
    Language="VB" %>
<table summary ="used to create user control inside">
    <tr>
        <td style ="color:black">
            <asp:Label ID="lblDepartment" runat="server" AssociatedControlID="ddlDepartment"
                CssClass="label" Text="Department"  ToolTip ="Select Department"></asp:Label>
                </td>
        <td title ="Select Department">
            <asp:DropDownList ID="ddlDepartment" runat="server" AccessKey="d" AutoPostBack="True"
                CssClass="dropdownlist" ToolTip="Select Department">
            </asp:DropDownList>
        </td>
        <td>
        </td>
        <td style ="color:black">
            <asp:Label ID="lblClient" runat="server" AccessKey="c" AssociatedControlID="ddlClient"
                CssClass="label" Text="Client" ToolTip ="Select Client"></asp:Label></td>
        <td title="Select Client">
            <asp:DropDownList ID="ddlClient" runat="server" AccessKey="c" AutoPostBack="True"
                CssClass="dropdownlist" TabIndex="1" ToolTip="Select Client">
            </asp:DropDownList>
        </td>
        <td>
        </td>
        <td style ="color:black">
            <asp:Label ID="lblLob" runat="server" AccessKey="l" AssociatedControlID="ddlLob"
                CssClass="label" Text="LOB" ToolTip ="Select Lob"></asp:Label></td>
        <td title ="Select Lob" style="width: 90px">
            <asp:DropDownList ID="ddlLob" runat="server" AccessKey="l" AutoPostBack="True" CssClass="dropdownlist"
                TabIndex="2" ToolTip="Select LOB">
            </asp:DropDownList>
            
        </td>
    </tr>
</table>
<%--<%-

Changed on :-
Created By:-   Vikas & Jitendra

- %>--%>
