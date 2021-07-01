<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChangeStatus.aspx.vb"  MasterPageFile ="~/MasterPage/MasterPage.master"   Inherits="AdvanceRightsManagement_ChangeStatus" %>
<%@ Register Src="~/AdvanceRightsManagement/UserConrol/DCLUserControl.ascx" TagName="DCLUserControl"
    TagPrefix="uc1" %>

<asp:Content ID="ChangeStatus" runat="server" ContentPlaceHolderID ="ContentPlaceHolder1" >
<table class="table" width="80%" summary ="used to contain controls for Change status">
            <caption  class ="caption" style ="background-color:#0591D3">Change Status</caption>
            <tr></tr>
<tr>
<td  valign ="top" scope="col"  style ="color:black" >
<asp:Label ID="lblSelectSpan" runat="server" CssClass="label" Text="Choose Span " ToolTip="Choose Span"></asp:Label>    
</td>
    <td  align ="left"  scope="col">  
        <uc1:DCLUserControl ID="DCLUserControl1" runat="server" />     
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DCLUserControl1$ddlDepartment"
            ErrorMessage="Choose department" InitialValue="--Select--" SetFocusOnError="True"
            ValidationGroup="final" ForeColor="White">*</asp:RequiredFieldValidator></td>
</tr>
<tr>
<td  scope="col"  style ="color:black" >
<asp:Label ID="lblSelectItem" runat="server" CssClass="label" Text="Choose Item"></asp:Label>
</td>
<td   scope="col" style ="color:black" >
<asp:RadioButton  ID="rdoTable" runat ="server" Text ="Table" GroupName="item" ToolTip="Choose table" AutoPostBack="True" /> 
</td>
</tr>
<tr>
<td style="height: 22px" scope="col"></td>
<td style="height: 22px; color :Black "  scope="col"   >
<asp:RadioButton  ID="rdoView" runat ="server" Text ="View" GroupName="item" ToolTip="Choose view" AutoPostBack="True" />
 </td>
</tr>
<tr>
<td style="height: 22px" scope="col"></td>
<td style="height: 22px; color :Black "  scope="col"   >
<asp:RadioButton  ID="rdoCmd" runat ="server" Text ="Cmd" GroupName="item" ToolTip="Choose cmd" AutoPostBack="True" /> 
</td>
</tr>
<tr>
<td style="height: 22px" scope="col"></td>
<td style="height: 22px; color :Black " scope="col"   >
<asp:RadioButton  ID="rdoReport" runat ="server"  Text ="Report" GroupName="item" ToolTip="Choose report" AutoPostBack="True"/> 
</td>
</tr>
    <tr>
        <td style="height: 22px" scope="col">
        </td>
        <td style="height: 22px; color :Black " >
            <asp:RadioButton ID="rdoAnalysis" runat="server" AutoPostBack="True" GroupName="item"
                Text="Analysis" ToolTip="Choose Analysis" />
        </td>
    </tr>
<tr>
<td style="height: 24px; color :Black " scope="col" >
<asp:Label ID="Label1" runat="server" CssClass="label" Text="Choose Entity" ToolTip="Select entity"></asp:Label>
</td>
<td style="height: 24px" scope="col" >
<asp:DropDownList ID="ddlSelect" runat="server" CssClass="dropdownlist"
                    ToolTip="Select entity" AutoPostBack="True">
                </asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlSelect"
        ErrorMessage="Select Entity" ForeColor="White" InitialValue="--Select--" SetFocusOnError="True"
        ValidationGroup="final">*</asp:RequiredFieldValidator></td>
</tr>
<tr>
<td style ="color:black"  >
<asp:Label ID="Label2" runat="server" CssClass="label" Text="Last Status" ToolTip="Last status"></asp:Label>
</td>
<td  >
<asp:TextBox  ID="tbxLast"  runat ="server"  CssClass ="textbox" ReadOnly="True" AutoPostBack="True" Width="144px" ToolTip="Last status" ></asp:TextBox>
                </td>
</tr>
<tr>
<td  style ="color:black" >
<asp:Label ID="Label3" runat="server" CssClass="label" Text="Change Status" ToolTip="select status"></asp:Label>
</td>
<td  >
<asp:DropDownList ID="ddlChangeStatus" runat="server" CssClass="dropdownlist"
                    ToolTip="Select status" AutoPostBack="True">
                </asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlChangeStatus"
        ErrorMessage="Select new status" ForeColor="White" InitialValue="--Select--"
        SetFocusOnError="True" ValidationGroup="final">*</asp:RequiredFieldValidator></td>
</tr>
<tr></tr>
<tr></tr>
<tr>
<td></td>
<td  align ="left"  scope="col">
 <asp:Button ID="btnSet" runat="server"  CssClass="button"
                        Text="SET" ToolTip="Click to Change status" ValidationGroup="final"   />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="final" />

</td>
<td></td>
</tr>
</table>

    












</asp:Content>