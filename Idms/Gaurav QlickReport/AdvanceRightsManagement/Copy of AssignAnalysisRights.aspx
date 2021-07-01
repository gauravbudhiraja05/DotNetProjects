<%--<%-
ProjectName :-  IDMS Phase 2
ModuleName:-    Advance Right Management
Page Tittle:-   Assign Analysis Rights
Created on :-
Created By:-   Vikas 

- %>--%>

<%@ Page Language="VB" AutoEventWireup="false"  MasterPageFile ="~/MasterPage/MasterPage.master"CodeFile="AssignAnalysisRights.aspx.vb" Inherits="AdvanceRightsManagement_AssignAnalysisRights" %>

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
<asp:Content ID="AssignAnalysisRights" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" type="text/javascript">
			
			function SelectAll(id)
        {
           
            var grid = document.getElementById("<%= gvAnalysis.ClientID %>");           
            var cell;
            
            if (grid.rows.length > 0)
            {
               
                for (i=1; i<grid.rows.length; i++)
                {
                   
                    cell = grid.rows[i].cells[0];
                    
                   for (j=0; j<cell.childNodes.length; j++)
                    {           
                       
                        if (cell.childNodes[j].type =="checkbox")
                        {
                        
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }
			
			
			function Select(id)
        {
           
            var grid = document.getElementById("<%= gvUsers.ClientID %>");           
            var cell;
            
            if (grid.rows.length > 0)
            {
               
                for (i=1; i<grid.rows.length; i++)
                {
                   
                    cell = grid.rows[i].cells[0];
                    
                   for (j=0; j<cell.childNodes.length; j++)
                    {           
                       
                        if (cell.childNodes[j].type =="checkbox")
                        {
                        
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }
			
    </script>

    <table cellpadding="0" cellspacing="5" class="table" summary="used to contain controls for Assign analysis rights"
        width="80%">
        <caption class="caption" style ="background-color:#0591D3" >
            Assign Analysis Rights</caption>
        <tr>
        </tr>
        <tr>
            <td>
                <uc1:dclusercontrol id="DCLUserControl3" runat="server">
</uc1:dclusercontrol>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DCLUserControl3$ddlDepartment"
                    ErrorMessage="Select Department" ForeColor="White" InitialValue="--Select--"
                    SetFocusOnError="True" ValidationGroup="user">*</asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="user" />
            </td>
            <td>
                <uc1:dclusercontrol id="DCLUserControl4" runat="server">
</uc1:dclusercontrol>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DCLUserControl4$ddlDepartment"
                    ErrorMessage="Select Department" ForeColor="White" InitialValue="--Select--"
                    SetFocusOnError="True" ValidationGroup="report">*</asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="report" />
            </td>
        </tr>
        <tr>
            <td align="center" style="height: 22px">
                <asp:Button ID="btnUser" runat="server" CssClass="button" Text="Show User" ToolTip="Click to see user list in gridview below"
                    ValidationGroup="user" />
            </td>
            <td align="center" style="height: 22px">
                <asp:Button ID="btnShowAnalysis" runat="server" CssClass="button" Text="Show Analysis"
                    ToolTip="click to see analysis list" ValidationGroup="report" />
            </td>
        </tr>
        <tr>
            <td align="left" rowspan="2" valign="top">
                <asp:GridView ID="gvUsers" runat="server" AllowPaging="True" style ="color:black" AllowSorting="True"
                    AutoGenerateColumns="False" BorderColor="White" BorderWidth="2px" CssClass="datagrid"
                    PageSize="15" ToolTip="select users from here" Width="256px">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkSelectAll" runat="server" CausesValidation="false" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkselectuser" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="UserName" HeaderText="UserName" />
                        <asp:BoundField DataField="UserId" HeaderText="UserId" />
                    </Columns>
                    <PagerStyle BackColor="PaleTurquoise" ForeColor="Black" />
                    <HeaderStyle BackColor="#377C84" />
                </asp:GridView>
            </td>
            <td align ="left"  valign ="top"  >
            <asp:Label ID="lblspacer" runat ="server"  Text ="vikas" ForeColor ="white"></asp:Label>
                <asp:GridView ID="gvAnalysis" runat="server" AllowPaging="True" style ="color:black" AllowSorting="True"
                    AutoGenerateColumns="False" BorderColor="White" BorderStyle="None" BorderWidth="2px"
                    CssClass="datagrid" PageSize="15" ToolTip="Choose rights  on selected analysis"
                    Width="200px">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkSelectAll" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkAnalysis" runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="AnalysisName" HeaderText="Name" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblView" runat="server" Text="View"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkView" runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblDelete" runat="server" Text="Delete"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkDelete" runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                       
                    </Columns>
                    <PagerStyle BackColor="PaleTurquoise" ForeColor="Black" />
                    <FooterStyle BackColor="#337C84" />
                    <HeaderStyle BackColor="#377C84" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
        </tr>
        <tr></tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnAssignAnalysisRight" runat="server" CssClass="button" Text="Assign Right"
                    ToolTip="Click to assign rights on selected analysis" Visible="false" />
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
<%--<%-

Changed on :-
Created By:-   Vikas & Jitendra

- %>--%>
