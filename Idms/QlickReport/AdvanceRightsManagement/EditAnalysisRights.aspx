<%--<%-
ProjectName :-  IDMS
ModuleName:-    Advance Right Management
Page Tittle:-   Edit Report Rights Form
Created on :-
Created By:-   Vikas 

- %>--%>

<%@ Page Language="VB" AutoEventWireup="false"  MasterPageFile ="~/MasterPage/MasterPage.master"CodeFile="EditAnalysisRights.aspx.vb" Inherits="AdvanceRightsManagement_EditAnalysisRights" %>

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
<asp:Content ID="EditAnalysisRight" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div>
        <table class="table" style="width: 168px" summary="used for contain controls for Edit Analysis Rights">
            <caption class="caption" style ="background-color:#0591D3" >
                Edit Analysis Rights</caption>
           
            <tr>
            </tr>
            <tr>
            </tr>
            <tr>
                <td>
                    <uc1:DCLHUserControl ID="DCLHUserControl1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="height: 22px; width: 700px; color :Black ">
                    <asp:Label ID="lblSelectMode" runat="server" CssClass="label">Select Mode</asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rdoUser" runat="server" AutoPostBack="True" GroupName="grpMode"
                        Text="ByUser" ToolTip=" Select Userwise" />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:RadioButton ID="rdoAnalysis" runat="server" AutoPostBack="True" GroupName="grpMode"
                        Text="ByAnalysis" ToolTip="Select Analysiswise" /></td>
            </tr>
            <tr>
                <td style="width: 700px; color :Black " title="Select Analysis or User">
                    <asp:Label ID="lblUser" runat="server" AssociatedControlID="ddlUserAnalysis" CssClass="label"
                        ToolTip="Selct Analysis or User">Select User/Analysis</asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                    <asp:DropDownList ID="ddlUserAnalysis" runat="server" AutoPostBack="True" CssClass="dropdownlist"
                        ToolTip="select User/Analysis">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 700px">
                    &nbsp;
                    <asp:GridView ID="gvAnalysisRight" runat="server" style ="color:black" AllowPaging="True" AutoGenerateColumns="False"
                        DataKeyNames="analysisname,UserId" ToolTip="analysis or userwise rights for edit">
                        <Columns>
                           
                            <asp:BoundField DataField="AnalysisName" HeaderText="Name" ReadOnly="True" />
                            <asp:BoundField DataField="UserId" HeaderText="UserId" ReadOnly="True"  />
                            <asp:BoundField DataField="UserName" HeaderText="UserName" ReadOnly="True" />
                           <%-- <asp:CheckBoxField DataField="View" HeaderText="View" >
                            
                                <ItemStyle HorizontalAlign="Center" />
                
                            </asp:CheckBoxField>--%>
                            <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblView"  runat="server" Text="View"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
 <asp:Label ID="lbchkView" AssociatedControlID ="chkView" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkView" Checked ='<%# Eval("View") %>' Enabled ="false"   runat="server" /></ItemTemplate>
                        </asp:TemplateField>
                        
                     <%--       <asp:CheckBoxField DataField="Delete" HeaderText="Delete" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CheckBoxField>--%>
                            
                            <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblDelete"  runat="server" Text="Delete"></asp:Label></HeaderTemplate>
                            <ItemTemplate>
 <asp:Label ID="lbchkDelete" AssociatedControlID ="chkDelete" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkDelete" Checked ='<%# Eval("Delete") %>' Enabled ="false"   runat="server" /></ItemTemplate>
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
