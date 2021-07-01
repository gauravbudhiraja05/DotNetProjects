<%--Project Name: IDMS Phase 2
    Module Name: Data Analysis
    Page Name: Analysis Result
    Summary: display the Analysis Result

    Created By: Ranjit Singh

--%>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ResultDisplay.aspx.vb" Inherits="DataAnalysis_ResultDisplay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script language="javascript" type="text/javascript">
function anagraph()
{
window.open("Analysisgrpah.aspx","AnalysisGraph","");
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
    <title>Analysis Result</title>
<link rel="Stylesheet" href="../App_Themes/Themes/StyleSheet.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
<table width="100%" summary="Plot Graph">
    <tr><td align="center"><asp:Button ID="btnanalysis" Visible="false" Text="Plot Graph" CssClass="button"  runat="server" /></td></tr>
    </table>
    <div id="formularesults" runat="server" style="margin-left:20px">
    
    </div>
    <div id="divhead" runat="server" style="margin-left:20px">
    
    </div>
    <div id="Report" runat="server" style="margin-left:20px">
    
    </div>
    </form>
</body>
</html>

<%--
---------------- Change History -------------------------
none
--%>
