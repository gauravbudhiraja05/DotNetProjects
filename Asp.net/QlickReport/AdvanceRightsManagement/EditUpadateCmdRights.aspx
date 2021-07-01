<%--<%-
ProjectName :-  IDMS
ModuleName:-    Advance Right Management
Page Tittle:-   Edit Update Command Rights Form
Created on :-
Created By:-   Vikas & Jitendra

- %>--%>

<%@ Page AutoEventWireup="false" CodeFile="EditUpadateCmdRights.aspx.vb" Inherits="AdvanceRightsManagement_EditUpadateCmdRights"
    Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" %>

<%@ Register Src="~/AdvanceRightsManagement/UserConrol/DCLHUserControl.ascx" TagName="DCLHUserControl"
    TagPrefix="uc1" %>
<asp:Content ID="lftplaceholder" runat="server" ContentPlaceHolderID="LeftPlaceHolder">
    <table>
        <tr>
            <td style="width: 206px;">
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="EditUpdateCmdRights" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div>
        <table class="table" style="width: 168px" summary="used for contain controls for Edit Update command  Rights">
        <caption class ="caption" style ="background-color:#0591D3">Edit Update Command Rights</caption>
            <tr>
               <!-- <td align="center" colspan="4">
                    <asp:Label ID="lblTableHeader" runat="server" CssClass="tableHeader" Text="Edit Update Command  Rights"
                        Width="696px"></asp:Label></td>-->
            </tr>
            <tr></tr>
            <tr></tr>
            <tr>
                <td style="width: 700px">
                    <uc1:DCLHUserControl ID="DCLHUserControl1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="height: 22px; width: 700px; color:Black ">
                    <asp:Label ID="lblSelectMode" runat="server" CssClass="label">Select Mode</asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<asp:RadioButton ID="rdoUser" runat="server" AutoPostBack="True" GroupName="grpMode"
                        Text="ByUser" ToolTip=" Select Userwise" />
                    &nbsp; &nbsp; &nbsp;
                    <asp:RadioButton ID="rdoCmd" runat="server" AutoPostBack="True" GroupName="grpMode"
                        Text="ByCmd" ToolTip="Select Cmdwise" /></td>
            </tr>
            <tr>
                <td style="width: 700px;color:black" title ="Select User or Cmd">
                    <asp:Label ID="lblUser" runat="server" AssociatedControlID="ddlUserCmd" CssClass="label" ToolTip ="Select User or Cmd">Select User/Cmd</asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<asp:DropDownList ID="ddlUserCmd" runat="server" CssClass="dropdownlist" ToolTip="select User/View" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfValSelectUser" runat="server" ControlToValidate="ddlUserCmd"
                        ErrorMessage="Select User/Cmd">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 700px">
                    <asp:GridView ID="gvCmdRight" runat="server" style ="color:black"  AllowPaging="True" AutoGenerateColumns="False"
                        DataKeyNames="cmdId,UserId" ToolTip="Cmd or userwise rights for edit">
                        <Columns>
                            <asp:BoundField DataField="CmdId" HeaderText="CmdId" ReadOnly="True" Visible="False" />
                            <asp:BoundField DataField="CmdName" HeaderText="CmdName" ReadOnly="True" />
                            <asp:BoundField DataField="UserId" HeaderText="UserId" ReadOnly="True" />
                            <asp:BoundField DataField="UserName" HeaderText="UserName" ReadOnly="True" />
                            <%--<asp:CheckBoxField DataField="View" HeaderText="View">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CheckBoxField>--%>
                            
                            <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblView"  runat="server" Text="View"></asp:Label></HeaderTemplate>
                            <ItemTemplate  >
 <asp:Label ID="lbchkView" AssociatedControlID ="chkView" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkView" Checked ='<%# Eval("View") %>'  Enabled ="false"   runat="server" /></ItemTemplate>
                       <ItemStyle HorizontalAlign ="Center" />
                        </asp:TemplateField>
                        
                            <%--<asp:CheckBoxField DataField="Run" HeaderText="Run">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CheckBoxField>--%>
                            <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblRun"  runat="server" Text="Run"></asp:Label></HeaderTemplate>
                            <ItemTemplate  >
 <asp:Label ID="lbchkRun" AssociatedControlID ="chkRun" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkRun" Checked ='<%# Eval("Run") %>'  Enabled ="false"   runat="server" /></ItemTemplate>
                       <ItemStyle HorizontalAlign ="Center" />
                        </asp:TemplateField>
                        
                          
                            <%--<asp:CheckBoxField DataField="Delete" HeaderText="Delete" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CheckBoxField>--%>
                            <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblDelete"  runat="server" Text="Delete"></asp:Label></HeaderTemplate>
                            <ItemTemplate  >
 <asp:Label ID="lbchkDelete" AssociatedControlID ="chkDelete" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkDelete" Checked ='<%# Eval("Delete") %>'  Enabled ="false"   runat="server" /></ItemTemplate>
                       <ItemStyle HorizontalAlign ="Center" />
                        </asp:TemplateField>
                            
                           <%-- <asp:CheckBoxField DataField="SaveAs" HeaderText="SaveAs" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CheckBoxField>--%>
                             <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblSaveAs"  runat="server" Text="SaveAs"></asp:Label></HeaderTemplate>
                            <ItemTemplate  >
 <asp:Label ID="lbchkSaveAs" AssociatedControlID ="chkSaveAs" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkSaveAs" Checked ='<%# Eval("SaveAs") %>'  Enabled ="false"   runat="server" /></ItemTemplate>
                       <ItemStyle HorizontalAlign ="Center" />
                        </asp:TemplateField>
                            
                          <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                <ItemStyle ForeColor="Blue" />
                            </asp:CommandField>
                             
                            
                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True">
                                <ItemStyle ForeColor="Blue" />
                            </asp:CommandField>
                        </Columns>
                        <PagerStyle ForeColor="#404040" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<%--<%-

Changed on :-
Created By:-   Vikas & Jitendra

- %>--%>
