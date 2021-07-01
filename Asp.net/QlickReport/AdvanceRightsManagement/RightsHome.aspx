<%--<%-
ProjectName :-  IDMS Phase 2
ModuleName:-    Advance Right Management
Page Tittle:-   Assign Analysis Rights
Created on :-
Created By:-   Vikas 

- %>--%>
<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile ="~/MasterPage/MasterPage.master"   CodeFile="RightsHome.aspx.vb" Inherits="AdvanceRightsManagement_RightsHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">
<iframe id="leftFrame" src="../Misc/TreeviewPage.aspx?val=manager"  width="218" height="450" ></iframe>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table>
<tr>
<td style ="color:black" ><label  id="lblWelcom" class="label" runat="server" >Welcome</label></td>
<td style ="color:black" ><asp:Label  id="lblUserid"  CssClass="label" runat="server" ></asp:Label></td>
</tr>
<tr>
<td style ="color:black" ><label  id="lblLogintime" class="label" runat="server" >LoginDate</label></td>
<td style ="color:black" >
<asp:Label  id="lblDate" CssClass="label" runat="server" ></asp:Label>
</td>
</tr>



</table>
</asp:Content>
<%--<%-

Changed on :-
Created By:-   Vikas & Jitendra

- %>--%>
