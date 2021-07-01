<%--Project Name: IDMS Phase 2
    Module Name: Data Analysis
    Page Name: Save Filter Report
    Summary: to save filtered report as html
    Created By: Ranjit Singh

--%>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SaveFilterReport.aspx.vb" Inherits="DataAnalysis_SaveFilterReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="en-us" xml:lang="en-us" xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Save Filter Report</title>
     <link rel="Stylesheet" href="../App_Themes/Themes/StyleSheet.css" type="text/css" />
      <script language="javascript" type="text/java">
function ok_onclick() 

{
window.close();
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div title="Save Report" runat="server" id="divsavereport" style="padding:20px;">
        <table summary="Save Report">
        <caption class="caption">Save Report</caption> 
            
        <tr>
                <td></td>
            </tr>
            <tr><td></td></tr>
        
            
            <tr>
                <td title="Select Department" scope="col" id="tddepttname">
                    <label class="label" title="Select Department" for="DropDowndept">Department</label>
                 </td>
                <td  title="Select Department" scope="col"  id="tdddldept" >
                      <asp:DropDownList  CssClass="dropdownlist" ToolTip="Select Department" ID="DropDowndept" runat="server" AutoPostBack="True">
       
                                 </asp:DropDownList>
                 </td>
              
            </tr>
            <tr>
                <td  title="Client" scope="col" id="tdClienttname">
                     <label class="label" title="Select Client" for="DropDownclient">Client</label>
                </td>
                <td title="Client" scope="col"  id="tdddlclient">
                         <asp:DropDownList CssClass="dropdownlist" ToolTip="Select Client" ID="DropDownclient" runat="server" AutoPostBack="True" >
                           </asp:DropDownList>
                </td>
                
           </tr>
           <tr>
                <td title="Select LOB" scope="col" id="tdloobname">
                    <label class="label" title="Select LOB" for="DropDownlob">LOB</label>
                    </td>
                <td title="LOB" scope="col"  id="tdddllob">
                           <asp:DropDownList  CssClass="dropdownlist" ToolTip="Select LOB"  ID="DropDownlob" runat="server" AutoPostBack="True">
                           </asp:DropDownList>
                 </td>
            </tr>
        
        
           <tr>
              <td title="Report Name" scope="col" id="tdrepname"  class="label">Report Name
              </td>
              <td>
                  <asp:TextBox CssClass="textBox" ToolTip="Fill Report Name" ID="textreportname" runat="server" Width="144px" MaxLength="20"></asp:TextBox>
               </td>
           </tr>
           <tr><td><label id="lblloclal" for="chklocal" class="label" title="Local If Checked">Local If Checked</label></td>
           <td><asp:CheckBox ID="chklocal" runat="server" ToolTip="Local If Checked"  /></td></tr>
          <tr>
             <td>
              <input onclick="return  ok_onclick()" type="button" id="showcalculator" title="Click To Close Window" value="Close" class="button" />
             </td>
             <td style="height: 24px" scope="col">
                <asp:Button CssClass="button" ID="Button4" runat="server" ToolTip="Click To Save Report"  Text="Save" />
                </td>
        
          </tr>
     </table> 
        
        </div>
        <asp:HiddenField ID="DEPART" runat="server" />
         <asp:HiddenField ID="lob" runat="server" />
          <asp:HiddenField ID="client" runat="server" />
    
    </form>
</body>
</html>
<%--
---------------- Change History -------------------------
none
--%>