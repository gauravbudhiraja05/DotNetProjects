<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master"  AutoEventWireup="false" CodeFile="ImportData.aspx.vb" Inherits="DataManager_ImportData" title="Import Data" %>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<label id="ctl00_MainPlaceHolder_lblLob" for="ddlLob" class="label" title="Select LOB">LOB</label>--%>
<link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
<%--<table cellpadding="0"  cellspacing="0" class="table" id="TABLE1" summary="Contains file path to be upload"> 
    <tr>
        <td colspan="3" align="center">
                                        <br />
                                        <br />
        </td>
     </tr>
   </table>--%>
   <table cellpadding="0"   cellspacing="0"  class="table"  runat="server" id="spantable"  summary="Contains Span">
    
    
    <tr runat="server" visible="false" id="dept_row">
        <td scope="row">
        <asp:Label ID="select_level1" runat="server" Text="Select Level 1" CssClass="label"></asp:Label>
        </td>
        <td runat="server" visible="false"  class="style3"><asp:DropDownList  Visible="false" ID="ddlDept" runat="server"  CssClass="dropdownlist" ToolTip="Select Department"></asp:DropDownList>
            <br />
            </td>
            <td class="style5" >
            <asp:DropDownList ID="DepartmentName" CssClass="dropdownlist" runat="server" 
                AutoPostBack="True">
            </asp:DropDownList>
                <br />
                <br />
        </td>
    </tr>
     
    <tr runat="server" visible="false" id="client_row">
        <td scope="row" class="style1" ><%--<label id="lblClient" for="ctl00_MainPlaceHolder_ddlClient" class="label" title="Select Client">Client</label>--%>
        <asp:Label ID="Label2" runat="server" Text="Select Level 2" CssClass="label"></asp:Label>
        </td>
        <td runat="server" visible="false" scope="row" class="style3"><asp:DropDownList Visible="false" ID="ddlClient" runat="server"  CssClass="dropdownlist" ToolTip="Select Client"></asp:DropDownList>
            <br />
            </td>
            <td colspan="2" class="style5"><asp:DropDownList ID="ClientName" CssClass="dropdownlist" runat="server" 
                AutoPostBack="True">
            </asp:DropDownList>
                <br />
                <br />
        </td>
    </tr>
     <tr runat="server" visible="false" id="lob_row">
        <td scope="row" class="style1"><%--<label id="ctl00_MainPlaceHolder_lblLob" for="ddlLob" class="label" title="Select LOB">LOB</label>--%>
        <asp:Label ID="Label3" CssClass="label" runat="server" Text="Select Level 3"></asp:Label></td>
        <td runat="server" visible="false" scope="row" class="style3"><asp:DropDownList ID="ddlLob" Visible="false" runat="server"  CssClass="dropdownlist" ToolTip="Select LOB"></asp:DropDownList></td>
           <td><asp:DropDownList ID="ddlLobName" runat="server" CssClass="dropdownlist" 
                AutoPostBack="True">
            </asp:DropDownList>
               <br />
               <br />
            </td>
                <td>  
                    &nbsp;</td>
    </tr>
    <tr><td></td><td>  
             <asp:Button ID="get_table" runat="server" Text="Get Tables" CssClass="button" Visible="false" />
             <br />
             <br />
        </td></tr>
    <tr>
        <td><label id="lblTable" for="ctl00_MainPlaceHolder_ddlTable" class="label" title="Select Table">TableName</label>
        </td>
        <td scope="row"  > <asp:DropDownList ID="ddlTable" runat="server"   CssClass="dropdownlist" ToolTip="Select Table"></asp:DropDownList>
            <br />
            <br />
        </td>
    </tr>
    <tr><td></td></tr>
    <tr><td></td></tr>
    <tr><td></td></tr>
    <tr><td><label id="lblFilename" for="ctl00_MainPlaceHolder_txtFile"   class="label" title="Select File">
        <br />
        File Name<br />
        <br />
        </label></td>
    <td>
        <input  type="file" id="txtFile" name="txtFile"  runat="server"/>&nbsp;&nbsp; <input  type="button" id="btnCancel" runat="server" value="Cancel" class="button" title="Click to cancel the selection "/>
                                        <input id="Button1" type="button" 
    value="Help to Import Data" onclick="window.open('importdatahelp.aspx')" 
    class="button" /><br />
&nbsp;
        <br />
&nbsp;<asp:Label ID="Label1" runat="server" Text=" * Only Excel File Allowed" CssClass="label"></asp:Label>
                                        </td>
    <td>&nbsp;</td>
    </tr>
    
    <tr>
        <td   align="right" scope="row" style="  height:20px" colspan="2"></td></tr>
    <tr>
        <td  scope="row" class="style4"><%--<input type="button" id="btnImport" value="Import" runat="server"  title="Click to import data"   class="button"/>--%>
            <asp:Button ID="btnImport_multiuser" runat="server" Text="Import" 
                CssClass="button" Visible="False" />
            <asp:Button ID="btnImport_singleuser" runat="server" Text="Import" 
                CssClass="button" Visible="False" />
        </td>
       <td  align="left" scope="row" class="style4"><asp:Button ID="btnFinalize_multiuser" CssClass="button" 
               runat="server" Text="Finalize" Visible="False" />
           <asp:Button ID="btnFinalize_singleuser" CssClass="button" runat="server" 
               Text="Finalize" Visible="False" />
        </td>
    </tr>
    <tr>
        <td scope="row" style="width: 174px"><input type="hidden" id="txttablname" runat="server" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="clt" runat="server" Visible="false"  />
<asp:HiddenField ID="lob" runat="server" Visible="false" />

</asp:Content>




