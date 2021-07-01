<%--<%-
ProjectName :-  IDMS
ModuleName:-    Advance Right Management
Page Tittle:-   Assign Report Rights
Created on :-
Created By:-   Vikas & Jitendra

- %>--%>

<%@ Page AutoEventWireup="false" CodeFile="AssignReportRights.aspx.vb" Inherits="AdvanceRightsManagement_AssignReportRights"
    Language="VB" MasterPageFile="~/MasterPage/MasterPage.master"%>

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
<asp:Content ID="AssignReportRights" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">


    <script language="javascript" type="text/javascript">
			
			function SelectAll(id)
        {
           
            var grid = document.getElementById("<%= gvReport.ClientID %>");           
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
    <table cellpadding="0" cellspacing="5" class="table" width="80%" summary ="used to contain controls for Assign Report rights" >
    <caption class ="caption" style ="background-color:#0591D3">Assign Report Rights</caption>
       <tr></tr>
        <tr>
            <td scope="col" style ="color:black">
                <uc1:DCLUserControl ID="DCLUserControl3" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DCLUserControl3$ddlDepartment"
                    ErrorMessage="Select Department" ForeColor="White" InitialValue="--Select--"
                    SetFocusOnError="True" ValidationGroup="user">*</asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="user" />
            </td>
            <td scope="col" style ="color:black">
                <uc1:DCLUserControl ID="DCLUserControl4" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DCLUserControl4$ddlDepartment"
                    ErrorMessage="Select Department" ForeColor="White" InitialValue="--Select--"
                    SetFocusOnError="True" ValidationGroup="report">*</asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="report" />
            </td>
        </tr>
        <tr>
            <td style="height: 22px" align ="center" scope="col" >
                <asp:Button ID="btnUser" runat="server" CssClass="button" Text="Show User" ToolTip="Click to see user list in gridview below"  />
            </td>
            <td style="height: 22px" align ="center" scope="col" >
                <asp:Button ID="btnShowReport" runat="server" CssClass="button" Text="Show Report" ToolTip="click to see reports list" ValidationGroup="report" />
            </td>
        </tr>
        <tr>
            <td align="left" rowspan="2" valign="top" scope="rowgroup">
                <asp:GridView ID="gvUsers" runat="server" AllowPaging="True" style ="color:black" AllowSorting="True"
                    AutoGenerateColumns="False" BorderColor="White" BorderWidth="2px" 
                    CssClass="datagrid" PageSize="15" Width="256px" ToolTip="select users from here">
                    <Columns>
                        <asp:TemplateField >
                            <HeaderTemplate>
                                <asp:Label ID="lbchkvisible" AssociatedControlID ="chkSelectAll" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkSelectAll" runat="server" CausesValidation="false" />
                            </HeaderTemplate>
                            <ItemTemplate>
                             <asp:Label ID="lbchkvisible" AssociatedControlID ="chkselectuser" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkselectuser" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="UserName" HeaderText="UserName" />
                        <asp:BoundField DataField="UserId" HeaderText="UserId"   />
                    </Columns>
                    <PagerStyle BackColor="PaleTurquoise" ForeColor="Black" />
                    <HeaderStyle BackColor="#377C84" />
                </asp:GridView>
            </td>
            <td valign ="top"  scope="rowgroup">
                <asp:GridView ID="gvReport" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" BorderColor="White" style ="color:black" BorderStyle="None" BorderWidth="2px"
                    CssClass="datagrid"
                    PageSize="15" ToolTip="Choose rights  on selected tables" Width="200px">
                    <Columns>
                        <asp:TemplateField>                            
                                <headertemplate>
                                <asp:Label ID="lbchkvisible" AssociatedControlID ="chkSelectAll" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkSelectAll" runat="server" />
                            </headertemplate>
                                
                            <ItemTemplate>
                            <asp:Label ID="lbchkvisible" AssociatedControlID ="chkReport" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkReport" runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="QueryName" HeaderText="ReportName" />
                        <asp:BoundField DataField="RecordId" HeaderText="ReportId"  Visible ="False" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblView"  runat="server" Text="View"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
 <asp:Label ID="lbchkView" AssociatedControlID ="chkView" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkView"  runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblEdit" runat="server"  Text="Edit"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
<asp:Label ID="lbcchkEdit" AssociatedControlID ="chkEdit" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkEdit" runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblDelete" runat="server"  Text="Delete"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
<asp:Label ID="lbchkDelete" AssociatedControlID ="chkDelete" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkDelete" runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblSaveAs" runat="server"  Text="SaveAs"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
<asp:Label ID="lblchkSaveAs" AssociatedControlID ="chkSaveAs" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkSaveAs" runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="PaleTurquoise" ForeColor="Black" />
                    <FooterStyle BackColor="#337C84" />
                    <HeaderStyle BackColor="#377C84" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </td>
        </tr>
        <tr></tr>
        <tr>
            <td  align ="center" colspan ="2"  scope="colgroup">
                <asp:Button ID="btnAssignReportRight" runat="server"  Visible ="false" CssClass="button" Text="Assign Right" ToolTip="Click to assign rights on selected reports" />
            </td>
            <td></td>
        </tr>
    </table>
</asp:Content>
<%--<%-

Changed on :-
Created By:-   Vikas & Jitendra

- %>--%>
