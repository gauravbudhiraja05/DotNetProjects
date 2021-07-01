<%--<%-
ProjectName :-  IDMS
ModuleName:-    Advance Right Management
Page Tittle:-   Assign View Rights
Created on :-
Created By:-   Vikas & Jitendra

- %>--%>

<%@ Page AutoEventWireup="false" CodeFile="AssignViewRight.aspx.vb" Inherits="AdvanceRightsManagement_ViewRightsManagement_AssignViewRight"
    Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" %>

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
<asp:Content ID="AssignViewRight" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">


    <script language="javascript" type="text/javascript">
			
			function SelectAll(id)
        {
           
            var grid = document.getElementById("<%= gvViews.ClientID %>");           
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
    <table cellpadding="0" cellspacing="5" class="table" summary="used to contain controls for Assign View Rights"
        width="80%">
        <caption class="caption" style ="background-color:#0591D3" >Assign View Rights</caption>
       
        <tr>
            <td style="height: 96px" scope="col">
                <uc1:DCLUserControl ID="DCLUserControl3" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DCLUserControl3$ddlDepartment"
                    ErrorMessage="Select Department" ForeColor="White" InitialValue="--Select--"
                    SetFocusOnError="True" ValidationGroup="user">*</asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="user" />
            </td>
            <td style="height: 96px" scope="col">
                <uc1:DCLUserControl ID="DCLUserControl4" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DCLUserControl4$ddlDepartment"
                    ErrorMessage="Select Department" ForeColor="White" InitialValue="--Select--"
                    SetFocusOnError="True" ValidationGroup="view">*</asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="view" />
            </td>
        </tr>
        <tr>
            <td align ="center" scope="col" >
                <asp:Button ID="btnUser" runat="server" CssClass="button" Text="Show User" ToolTip="Click to see user"  />
            </td>
            <td align ="center" scope="col" >
                <asp:Button ID="btnView" runat="server" CssClass="button" Text="Show View" ToolTip="Click to see view according to span selection" ValidationGroup="view" />
            </td>
        </tr>
        <tr>
            <td valign ="top"  scope="rowgroup">
                <asp:GridView ID="gvUsers" runat="server" AllowPaging="True" style ="color:black"  AllowSorting="True"
                    AutoGenerateColumns="False" BorderColor="White" BorderWidth="2px" CssClass="datagrid" PageSize="15" Width="256px">
                    <Columns>
                        <asp:TemplateField >
                            <HeaderTemplate >
                            <asp:Label ID="lbchkvisible" AssociatedControlID ="chkSelectAll" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkSelectAll" runat="server"  CausesValidation="false"  />
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
                    <HeaderStyle BackColor="#377C84" ForeColor="White" />
                </asp:GridView>
            </td>
            <td valign ="top"  scope="rowgroup">
                <asp:GridView ID="gvViews" runat="server" AllowPaging="True" style ="color:black"  AllowSorting="True"
                    AutoGenerateColumns="False" BorderColor="White" BorderStyle="None" BorderWidth="2px"
                    CssClass="datagrid"
                    PageSize="15" ToolTip="Choose rights  on selected tables" Width="200px">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                            <asp:Label ID="lbchkvisible" AssociatedControlID ="chkSelectAll" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkSelectAll" runat="server" CausesValidation="false" />
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:Label ID="lbchkvisible" AssociatedControlID ="chkView1" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkView1" runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ViewName" HeaderText="ViewName" />
                        <asp:BoundField DataField="ViewId" HeaderText="ViewId" Visible ="False"  />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblView" runat="server"  Text="View"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
<asp:Label ID="lblchkView" AssociatedControlID ="chkView" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkView" runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblEdit" runat="server"  Text="Edit"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
<asp:Label ID="lblchkEdit" AssociatedControlID ="chkEdit" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkEdit" runat="server"  /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblDelete" runat="server"  Text="Delete"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
<asp:Label ID="lblchkDelete" AssociatedControlID ="chkDelete" runat ="server"  ></asp:Label>
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
            
            <td align ="right" scope="col" >
                <asp:Button ID="btnAssignRight" runat="server" CssClass="button" Text="Assign Right"
                    ToolTip="Click To assign rights on selectd views"  Visible ="false"  />
            </td>
        </tr>
    </table>
</asp:Content>
<%--<%-

Changed on :-
Created By:-   Vikas & Jitendra

- %>--%>
