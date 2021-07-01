<%--Project Name: IDMS Phase 2
    Module Name: Data Analysis
    Page Name: Save Analysis
    Summary: to save the performed analysis
    Created By: Ranjit Singh

--%>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Saved_Analysis.aspx.vb" Inherits="DataAnalysis_Saved_Analysis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html  xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
   <title>Save Analysis</title>
     <link rel="Stylesheet" href="../App_Themes/Themes/StyleSheet.css" type="text/css" />
 </head>
      <script language="javascript" type="text/javascript">
function ok_onclick() 

{
window.close();
}
</script>
<body>

    <form id="form1" runat="server">
    <div title="Save Report" runat="server" id="divsavereport" style="padding:20px;">
        <table summary="Save Analysis">
        <caption class="caption">Save Analysis</caption> 
            
        <tr>
                <td></td>
            </tr>
            <tr><td></td></tr>
        
            
            <tr>
                <td title="Select Department" scope="col" id="tddepttname">
                   <%-- <label class="label" for="DropDowndept" title="Select Department">Department</label>--%>
                 </td>
                <td  title="Select Department" scope="col"  id="tdddldept" >
                    <%--  <asp:DropDownList  CssClass="dropdownlist" ToolTip="Select Department" ID="DropDowndept" runat="server" AutoPostBack="True">
       
                                 </asp:DropDownList>--%>
                 </td>
              
            </tr>
            <tr>
                <td  title="Client" scope="col" id="tdClienttname">
                     <%--<label class="label" for="DropDownclient" title="Select Client">Client</label>--%>
                </td>
                <td title="Client" scope="col"  id="tdddlclient">
                         <%--<asp:DropDownList CssClass="dropdownlist" ToolTip="Select Client" ID="DropDownclient" runat="server" AutoPostBack="True" >
                           </asp:DropDownList>--%>
                </td>
                
           </tr>
           <tr>
                <td title="Select LOB" scope="col" id="tdloobname">
                    <%--<label class="label" title="Select LOB" for="DropDownlob">LOB</label>--%>
                    </td>
                <td title="LOB" scope="col"  id="tdddllob">
                           <%--<asp:DropDownList  CssClass="dropdownlist" ToolTip="Select LOB"  ID="DropDownlob" runat="server" AutoPostBack="True">
                           </asp:DropDownList>--%>
                 </td>
            </tr>
        
        
           <tr>
              <td title="Report Name" scope="col" id="tdrepname"  class="label">
              <label class="label"  for="textreportname"> Analysis Name</label>
                 
              </td>
              <td scope="col">
                  <asp:TextBox CssClass="textBox" ToolTip="Fill Report Name" ID="textreportname" runat="server" Width="144px" MaxLength="20"></asp:TextBox>
               </td>
           </tr>
           <tr>
            <td title="Status" scope="col" id="td1"  class="label">
            <%-- <label class="label"  for="Status"> Status</label>--%>
                  
              </td>
              <td scope="col">
                  <%--<asp:CheckBox ID="Status" runat="server" ToolTip="Local If Checked" Text="Local/Non Local" />--%>
               </td>
           </tr>
          <tr>
             <td style="height: 24px">
                 &nbsp;<asp:Button CssClass="button" ID="Button4" runat="server" ToolTip="Click To Save Analysis"  Text="Save"/></td>
             <td scope="col" style="height: 24px">
                 &nbsp;<input onclick="return  ok_onclick()" type="button" id="showcalculator" title="Click To Close Window" value="Close" class="button" /></td>
          </tr>
     </table> 
        <br />
        <asp:Label ID="Errormsg" runat="server" CssClass="label" ForeColor="Red" Text="Label"
            Width="484px"></asp:Label><br />
       <%-- <asp:Label ID="Errormsg" runat="server" Font-Bold="True" ForeColor="Red" Text="Label"
            Width="484px"></asp:Label>--%></div>
        <asp:HiddenField ID="hidclientid" runat="server" />
    <asp:HiddenField ID="hidlobid" runat="server" />
     <asp:HiddenField ID="hidclientname" runat="server" />
    <asp:HiddenField ID="hidlobname" runat="server" />
    </form>
    
</body>
</html>
<%--
---------------- Change History -------------------------
none
--%>
