<%--<%-
ProjectName :-  IDMS
ModuleName:-    Advance Right Management
Page Tittle:-   Assign Table Rights
Created on :-   05/04/08 
Created By:-   Vikas & Jitendra

- %>--%>

<%@ Page AutoEventWireup="false" CodeFile="AssignTableRights.aspx.vb" Inherits="_Default"
    Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" %>

<%@ Register Src="~/AdvanceRightsManagement/UserConrol/DCLUserControl.ascx" TagName="DCLUserControl"
    TagPrefix="uc1" %>
<asp:Content ID="lftplaceholder" runat="server" ContentPlaceHolderID="LeftPlaceHolder">

    
    <table >
        <tr>
            <td style="width: 206px;">
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="AssignTable" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" type="text/javascript">
			
			function SelectAll(id)
        {
           
            var grid = document.getElementById("<%= gvTables.ClientID %>");           
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

    <table cellpadding="0" cellspacing="5" class="table" style="" width="80%" summary ="used to contain control for Assign Table Rights">
    <caption class ="caption" style ="background-color:#0591D3" >Assign Table Rights</caption>
       
        <tr>
            <td  scope="col">
                <uc1:DCLUserControl ID="DCLUserControl3" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DCLUserControl3$ddlDepartment"
                    ErrorMessage="Select Department" ForeColor="White" InitialValue="--Select--"
                    SetFocusOnError="True" ValidationGroup="user">*</asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="user" />
            </td>
            <td  scope="col" >
                <uc1:DCLUserControl ID="DCLUserControl4" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DCLUserControl4$ddlDepartment"
                    ErrorMessage="Select Department" ForeColor="White" InitialValue="--Select--"
                    SetFocusOnError="True" ValidationGroup="table">*</asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="table" />
            </td>
        </tr>
        <tr>
            <td  scope="col">
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnUser" runat="server" CssClass="button" Text="Show User" ToolTip="Click to see users or press (Alt+u)" AccessKey="u"  />
            </td>
            <td >
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp;<asp:Button ID="btnTable" runat="server" CssClass="button" Text="Show Table" ToolTip="Click to see tables or press (Alt+t)" AccessKey="t" ValidationGroup="table" />
            </td>
        </tr>
        <tr>
            <td  align ="center"  valign ="top"  scope="rowgroup">
                <asp:GridView ID="gvUsers" runat="server" AllowPaging="True" style ="color:black" AllowSorting="True"
                    AutoGenerateColumns="False"  CssClass="datagrid" PageSize="15" Width="256px" ToolTip="choose users" BorderColor="White" BorderWidth="2px">
                    <Columns>
                        <asp:TemplateField >
                            <HeaderTemplate>
                            <asp:Label ID="lbchkvisible" AssociatedControlID ="chkSelectAll" runat ="server"  ></asp:Label>
                               <asp:CheckBox ID="chkSelectAll" runat ="server"  CausesValidation ="false" />
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:Label ID="lbchkvisible" AssociatedControlID ="chkselectuser" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkselectuser" runat="server"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="UserName" HeaderText="User Name"   >
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UserId" HeaderText="UserId"  />
                    </Columns>
                    <PagerStyle BackColor="PaleTurquoise" ForeColor="Black"  HorizontalAlign ="Left" />
                    <HeaderStyle BackColor="#377C84" ForeColor="White" />
                </asp:GridView>
            </td>
            <td colspan="2" style="height: 180px" valign="top" align  ="center"  scope="colgroup" >
                <asp:GridView ID="gvTables" runat="server" AllowPaging="True" style ="color:black" AllowSorting="True"
                    AutoGenerateColumns="False" BorderColor="White" BorderStyle="None" BorderWidth="2px"
                     CssClass="datagrid"
                    PageSize="15" ToolTip="Choose rights  on selected tables" Width="200px">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                            <asp:Label ID="lbchkvisible" AssociatedControlID ="chkSelectAll" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkSelectAll" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:Label ID="lbchkvisible" AssociatedControlID ="chkTable" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkTable" runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TableName" HeaderText="Table Name"  >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TableId" HeaderText="TableId"  Visible ="False" />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblView" runat="server" Text="View"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
                              <asp:Label ID="lblchkView" AssociatedControlID ="chkView" runat ="server"  ></asp:Label>
                              <asp:CheckBox ID="chkView" runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblEdit" runat="server"  Text="Edit"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
                             <asp:Label ID="lblchkEdit" AssociatedControlID="chkEdit" runat ="server"  ></asp:Label>
                             <asp:CheckBox ID="chkEdit" runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblDelete" runat="server"  Text="Delete"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblchkDelete" AssociatedControlID="chkDelete" runat ="server"  ></asp:Label>
                               <asp:CheckBox ID="chkDelete" runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblDeleteData" runat="server" Text="Delete Data"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
                             <asp:Label ID="lblchkDeleteData" AssociatedControlID="chkDeleteData" runat ="server"  ></asp:Label>
                             <asp:CheckBox ID="chkDeleteData" runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblAddColumn" runat="server"  Text="Add Column"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
                               <asp:Label ID="lblchkAddColumn" AssociatedControlID="chkAddColumn" runat ="server"  ></asp:Label>
                               <asp:CheckBox ID="chkAddColumn" runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblimportdata" runat="server"  Text="Import Data"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
                             <asp:Label ID="lblchkimportdata" AssociatedControlID="chkimportdata" runat ="server"  ></asp:Label>
                             <asp:CheckBox ID="chkimportdata" runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                    <PagerStyle BackColor="PaleTurquoise" ForeColor="Black"  HorizontalAlign ="Left" />
                    <FooterStyle BackColor="#337C84" />
                    <HeaderStyle BackColor="#377C84" Font-Bold="True" ForeColor="White" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3" style="height: 17px" scope="colgroup">
                <asp:Button ID="btnAssignRight" runat="server" BorderStyle="None" CssClass="button"
                    Text="Assign Right" ToolTip="Click to assign rights on selected tables" Visible ="false"  />
            </td>
        </tr>
    </table>
</asp:Content>
<%--<%-

Changed on :-
Created By:-   Vikas & Jitendra

- %>--%>
