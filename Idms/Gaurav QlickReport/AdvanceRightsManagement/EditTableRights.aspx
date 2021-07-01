<%--<%-
ProjectName :-  IDMS
ModuleName:-    Advance Right Management
Page Tittle:-   Edit Report Rights Form
Created on :-
Created By:-   Vikas & Jitendra

- %>--%>

<%@ Page AutoEventWireup="false" CodeFile="EditTableRights.aspx.vb" Inherits="EditTableRights"
    Language="VB" MasterPageFile="~/MasterPage/MasterPage.master"%>

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
<asp:Content ID="EditTableRight" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div>
        <table class="table" style="width: 168px" summary="used for contain controls for Edit Table Rights">
        <caption class ="caption" style ="background-color:#0591D3">Edit Table Rights</caption>
            <tr>               
            </tr>
            <tr></tr>
            <tr></tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td  >
                    <uc1:DCLHUserControl ID="DCLHUserControl1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="height: 22px; width: 700px; color : Black ">
                    <asp:Label ID="lblSelectMode" runat="server" CssClass="label">Select Mode</asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rdoUser" runat="server" AutoPostBack="True" GroupName="grpMode"
                        Text="ByUser" ToolTip=" Select Userwise" />
                    &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;<asp:RadioButton ID="rdoTable" runat="server" AutoPostBack="True" GroupName="grpMode"
                        Text="ByTable" ToolTip="Select Tablewise" /></td>
            </tr>
            <tr>
                <td style="width: 700px; color: black" title="Select Table or User">
                </td>
            </tr>
            <tr>
                <td style="width: 700px; color :Black" title ="Select Table or User">
                    <asp:Label ID="lblUser" runat="server" CssClass="label" AssociatedControlID="ddlUserTable" ToolTip="Selct Table or User">Select User/Table</asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:DropDownList ID="ddlUserTable" runat="server" AutoPostBack="True" CssClass="dropdownlist"
                        ToolTip="select User/Table">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 700px">
                </td>
            </tr>
            <tr>
                <td style="width: 700px">
                    &nbsp;
                    <asp:GridView ID="gvTableRight" runat="server" AllowPaging="True" style ="color:black" AutoGenerateColumns="False"
                        DataKeyNames="TableId,UserId" ToolTip="table or userwise rights for edit">
                        <Columns>
                            <asp:BoundField DataField="TableId" HeaderText="TableId" ReadOnly="True" Visible="False" />
                            <asp:BoundField DataField="TableName" HeaderText="TableName" ReadOnly="True" />
                            <asp:BoundField DataField="UserId" HeaderText="UserId" ReadOnly="True" />
                            <asp:BoundField DataField="UserName" HeaderText="UserName" ReadOnly="True" />
                            <%--<asp:CheckBoxField DataField="View" HeaderText="View"   >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CheckBoxField>--%>
                             <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblView"  runat="server" Text="View"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
 <asp:Label ID="lbchkView" AssociatedControlID ="chkView" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkView" Checked ='<%# Eval("View") %>' Enabled ="false"   runat="server" /></ItemTemplate>
                        <ItemStyle HorizontalAlign ="Center" />
                        </asp:TemplateField>
                        
                           <%-- <asp:CheckBoxField DataField="Edit" HeaderText="Edit">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CheckBoxField>--%>
                            
                            <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblEdit"  runat="server" Text="Edit"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
 <asp:Label ID="lbchkEdit" AssociatedControlID ="chkEdit" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkEdit" Checked ='<%# Eval("Edit") %>' Enabled ="false"   runat="server" /></ItemTemplate>
                       <ItemStyle HorizontalAlign ="Center" />
                        </asp:TemplateField>
                            
                           <%-- <asp:CheckBoxField DataField="Delete" HeaderText="Delete">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CheckBoxField>--%>
                            <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblDelete"  runat="server" Text="Delete"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
 <asp:Label ID="lbchkDelete" AssociatedControlID ="chkDelete" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkDelete" Checked ='<%# Eval("Delete") %>' Enabled ="false"   runat="server" /></ItemTemplate>
                      <ItemStyle HorizontalAlign ="Center" />
                        </asp:TemplateField>
                            
                            <%--<asp:CheckBoxField  DataField="DeleteData" HeaderText="DeleteData" >
                                <ItemStyle HorizontalAlign="Center"  />
                            </asp:CheckBoxField>--%>
                            
                            <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblDeleteData"  runat="server" Text="DeleteData"></asp:Label></HeaderTemplate>
                            <ItemTemplate  >
 <asp:Label ID="lbchkDeleteData" AssociatedControlID ="chkDeleteData" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkDeleteData" Checked ='<%# Eval("DeleteData") %>'  Enabled ="false"   runat="server" /></ItemTemplate>
                       <ItemStyle HorizontalAlign ="Center" />
                        </asp:TemplateField>
                            
                            <%--<asp:CheckBoxField DataField="AddColumn" HeaderText="AddColumn">
                                <ItemStyle HorizontalAlign="Center" />
                                
                            </asp:CheckBoxField>--%>
                             <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblAddColumn"  runat="server" Text="AddColumn"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
 <asp:Label ID="lbchkAddColumn" AssociatedControlID ="chkAddColumn" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkAddColumn" Checked ='<%# Eval("AddColumn") %>' Enabled ="false"   runat="server" /></ItemTemplate>
                       <ItemStyle HorizontalAlign ="Center" />
                        </asp:TemplateField>
                            
                           <%-- <asp:CheckBoxField DataField="ImportData" HeaderText="ImportData" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CheckBoxField>--%>
                            
                            <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblImportData"  runat="server" Text="ImportData"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
 <asp:Label ID="lbchkImportData" AssociatedControlID ="chkImportData" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkImportData" Checked ='<%# Eval("ImportData") %>' Enabled ="false"   runat="server" /></ItemTemplate>
                        <ItemStyle HorizontalAlign ="Center" />
                        </asp:TemplateField>
                           
                            
                            <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                <ItemStyle ForeColor="Blue" />
                            </asp:CommandField>
                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" >
                                <ItemStyle ForeColor="Blue"  />
                            </asp:CommandField>
                        </Columns>
                        <PagerStyle ForeColor="#404040" />
                    </asp:GridView>
                    <asp:Panel ID="pandelete" runat="server" BackColor="whitesmoke" BorderColor="lightgrey"
                    BorderStyle="Outset" BorderWidth="1px" Height="32px" Style="z-index: 300; left: 330px;
                    position: absolute; top: 400px" Visible="False" Width="276px">
                    <table style="width: 273px; height: 40px" width="273" summary ="used to contain control for confirmation box"  border ="2">
                        <tr>
                            <td align="center" style="height: 34px">
                                <b>Are you sure, you want to cancel this assigned right?</b></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="cmdyes" runat="server" CssClass="button" Text="Yes" Width="38px" ToolTip="Click YES  if you want to cancel rights or press (alt+y)" AccessKey="y" />
                                <asp:Button ID="cmdno" runat="server" CssClass="button" Text="No" Width="38px" ToolTip="click NO  if you  do not cancel the rights or press(alt+n)" AccessKey="n" /></td>
                        </tr>
                    </table>
                </asp:Panel>
                    <asp:HiddenField ID="hdtableid" runat="server" Visible="False" />
                    <asp:HiddenField ID="hduserid" runat="server" Visible="False" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<%--<%-

Changed on :-
Created By:-   Vikas & Jitendra

- %>--%>
