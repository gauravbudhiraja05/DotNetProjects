<%--<%-
ProjectName :-  IDMS
ModuleName:-    Advance Right Management
Page Tittle:-   Assign UpdateCommand Rights
Created on :-
Created By:-   Vikas & Jitendra

- %>--%>

<%@ Page AutoEventWireup="false" CodeFile="AssignUpdateCmdRights.aspx.vb" Inherits="AdvanceRightsManagement_AssignUpdateCommandRights"
    Language="VB" MasterPageFile="~/MasterPage/MasterPage.master"%>

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
<asp:Content ID="AssignUpdateCmdRights" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" type="text/javascript">
			
			function SelectAll(id)
        {
           
            var grid = document.getElementById("<%= gvUpdateCommand.ClientID %>");           
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
    <table cellpadding="0" cellspacing="5" class="table" summary ="used to contain controls for Assign Update command">
    <caption class ="caption" style ="background-color:#0591D3" >Assign UpdateCommand Rights</caption>
        <tr>
          <!--  <td align="center" colspan="4">
                <asp:Label ID="lblTableHeader" runat="server" CssClass="tableHeader" Text="Assign UpdateCommand Rights"
                    Width="792px"></asp:Label>
            </td>-->
        </tr>
        <tr>
            <td scope="col">
                <uc1:DCLUserControl ID="DCLUserControl3" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DCLUserControl3$ddlDepartment"
                    ErrorMessage="Select Department" ForeColor="White" InitialValue="--Select--"
                    SetFocusOnError="True" ValidationGroup="user">*</asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="user" />
            </td>
            <td scope="col">
                <uc1:DCLUserControl ID="DCLUserControl4" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DCLUserControl4$ddlDepartment"
                    ErrorMessage="Select Department" ForeColor="White" InitialValue="--Select--"
                    SetFocusOnError="True" ValidationGroup="cmd" Width="13px">*</asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="cmd" />
            </td>
        </tr>
        <tr>
            <td align ="center"  scope="col">
                <asp:Button ID="btnUser" runat="server" CssClass="button" Text="Show User" ToolTip="click to see users"  />
            </td>
            <td align ="center" scope="col" >
                <asp:Button ID="btnCmd" runat="server" CssClass="button" Text="Show Cmd" ToolTip="click to see UpdateCommands" ValidationGroup="cmd" />
            </td>
        </tr>
        <tr>
            <td valign ="top" scope="rowgroup" >
                <asp:GridView ID="gvUsers" runat="server" AllowPaging="True" style ="color:black" AllowSorting="True"
                    AutoGenerateColumns="False" BorderColor="White" BorderWidth="2px" 
                    CssClass="datagrid" PageSize="15" Width="256px" ToolTip="select users">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                            <asp:Label ID="lbchkvisible" AssociatedControlID ="chkSelectAll" runat ="server"  ></asp:Label>
                        <asp:CheckBox ID="chkSelectAll" runat="server" CausesValidation="false">
                        </asp:CheckBox>
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
            <td valign ="top" scope="rowgroup" >
                <asp:GridView ID="gvUpdateCommand" runat="server" style ="color:black" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" BorderColor="White" BorderStyle="None" BorderWidth="2px"
                    CaptionAlign="Top" CssClass="datagrid"
                    PageSize="15" ToolTip="Choose rights  on selected tables" Width="200px">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                           <asp:Label ID="lbchkvisible" AssociatedControlID ="chkSelectAll" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkSelectAll" runat="server" CausesValidation="false" />
                                 </HeaderTemplate>
                            <ItemTemplate>
                            <asp:Label ID="lbchkvisible" AssociatedControlID ="chkCmd" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkCmd" runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CmdName" HeaderText="CmdName" />
                        <asp:BoundField DataField="CmdId" HeaderText="CmdId" Visible ="False"  />
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblView" runat="server"  Text="View"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
<asp:Label ID="lbchkView" AssociatedControlID ="chkView" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkView" runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblRun" runat="server"  Text="Run"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
<asp:Label ID="lbchkRun" AssociatedControlID ="chkRun" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkRun" runat="server" /></ItemTemplate>
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
           
            <td align ="right"  scope="col">
                <asp:Button ID="btnAssignRight" runat="server" CssClass="button" Text="Assign Right" Visible ="false"  />
            </td>
        </tr>
    </table>
</asp:Content>
<%--<%-

Changed on :-
Created By:-   Vikas & Jitendra

- %>--%>
