<%--<%-
ProjectName :-  IDMS
ModuleName:-    Advance Right Management
Page Tittle:-   Edit Update Command Rights Form
Created on :-
Created By:-   Vikas & Jitendra

- %>--%>
<%@ Page AutoEventWireup="false" CodeFile="EditViewRight.aspx.vb" Inherits="AdvanceRightsManagement_ViewRightsManagement_EditViewRight"
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
<asp:Content ID="EditViewRight" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div>
        <table class="table" style="width: 168px" summary="used for contain controls for Edit View Rights">
        <caption class ="caption" style ="background-color:#0591D3">Edit View Rights</caption>
            
            <tr> </tr>
            <tr></tr>
           
            
            <tr>
                <td style="width: 505px">
                    <uc1:DCLHUserControl ID="DCLHUserControl1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="height: 22px; width: 700px; color :Black ">
                    <asp:Label ID="lblSelectMode" runat="server" CssClass="label" ToolTip ="Select Mode">Select Mode</asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rdoUser" runat="server" AutoPostBack="True" GroupName="grpMode"
                        Text="ByUser" ToolTip=" Select Userwise" />
                    &nbsp; &nbsp;&nbsp; &nbsp;
                    <asp:RadioButton ID="rdoView" runat="server" AutoPostBack="True" GroupName="grpMode"
                        Text="ByView" ToolTip="Select Viewwise" /></td>
            </tr>
            <tr>
                <td style="width: 505px; color :Black " title ="Select User or View">
                    <asp:Label ID="lblUser" runat="server" AssociatedControlID="ddlUserView" CssClass="label" ToolTip ="Select User or View">Select User/View</asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                    <asp:DropDownList ID="ddlUserView" runat="server" CssClass="dropdownlist" ToolTip="select User/View" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfValSelectView" runat="server" ControlToValidate="ddlUserView"
                        ErrorMessage="Select User/View">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 505px">
                    <asp:GridView ID="gvViewRight" runat="server" style ="color:black" AllowPaging="True" AutoGenerateColumns="False"
                        DataKeyNames="ViewId,UserId" ToolTip="view or userwise rights for edit">
                        <Columns>
                            <asp:BoundField DataField="ViewId" HeaderText="ViewId" ReadOnly="True" Visible="False" />
                            <asp:BoundField DataField="ViewName" HeaderText="ViewName" ReadOnly="True" />
                            <asp:BoundField DataField="UserId" HeaderText="UserId" ReadOnly="True" />
                            <asp:BoundField DataField="UserName" HeaderText="UserName" ReadOnly="True" />
                            
                           <%-- <asp:CheckBoxField DataField="View" HeaderText="View">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CheckBoxField>--%>
                            
                            <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblView"  runat="server" Text="View"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
 <asp:Label ID="lbchkView" AssociatedControlID ="chkView" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkView" Checked ='<%# Eval("View") %>' Enabled ="false"   runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                        
                          <%--  <asp:CheckBoxField DataField="Edit" HeaderText="Edit">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CheckBoxField>--%>
                            <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblEdit"  runat="server" Text="Edit"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
 <asp:Label ID="lbchkEdit" AssociatedControlID ="chkEdit" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkEdit" Checked ='<%# Eval("Edit") %>' Enabled ="false"   runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                            
                          <%--  <asp:CheckBoxField DataField="Delete" HeaderText="Delete" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CheckBoxField>--%>
                            
                             <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblDelete"  runat="server" Text="Delete"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
 <asp:Label ID="lbchkDelete" AssociatedControlID ="chkDelete" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkDelete" Checked ='<%# Eval("Delete") %>' Enabled ="false"   runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                            
                            <%--<asp:CheckBoxField DataField="SaveAs" HeaderText="SaveAs">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CheckBoxField>--%>
                             <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblSaveAs"  runat="server" Text="SaveAs"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
 <asp:Label ID="lbchkSaveAs" AssociatedControlID ="chkSaveAs" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkSaveAs" Checked ='<%# Eval("Delete") %>' Enabled ="false"   runat="server" /></ItemTemplate>
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
