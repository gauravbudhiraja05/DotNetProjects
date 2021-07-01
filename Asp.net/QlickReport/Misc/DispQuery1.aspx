<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DispQuery1.aspx.vb" Inherits="Misc_DispQuery1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
    <title>Report</title>
    <script language="javascript">
    function CallWin()
			{ 
				var strformula=document.Form1.column.value
				//document.frmReportGenerator.hidtemp.value = document.frmReportGenerator.column.value
				var len1 = strformula.split("$")
				for (i=0;i<len1.length-1;i++)
				{
				strformula = document.Form1.column.value
				document.Form1.column.value = strformula.replace("$"," ");
				}
				var len = strformula.split(" + String.fromCharCode(34) + ")
				for (i=0;i<len.length-1;i++)
				{
				strformula = document.Form1.column.value
				document.Form1.column.value = strformula.replace(" + String.fromCharCode(34) + ","'");
				}
					if (document.Form1.hidReporttype.value=="Simple")
					{
						document.Form1.action = "/QlickReport/ReportDesigner/ShowData.aspx";
					}
					if (document.Form1.hidReporttype.value=="Summarized")
					{
						document.Form1.action = "/QlickReport/ReportDesigner/SummarizedReport.aspx";
					}
					document.Form1.submit();
				
			}
		</script>
</head>
<body onload="javascript:CallWin();">
    <form id="Form1" method="post" runat="server">
			<INPUT id="txtFormula" type="hidden" style="WIDTH: 80%" runat="server" name="txtformula"><!--formula-->
			<INPUT id="txtheadingName" type="hidden" style="WIDTH: 80%" runat="server" name="txtheadingName">
			<INPUT id="column" type="hidden" style="WIDTH: 80%" runat="server" name="column"><!--column-->
			<input id="wheredata" type="hidden" style="WIDTH: 80%" runat="server" name="wheredata"><!--wheredata-->
			<input id="gruopdata" type="hidden" style="WIDTH: 80%" runat="server" name="gruopdata"><!--gruopdata-->
			<input id="OrderbyData" type="hidden" style="WIDTH: 80%" runat="server" name="OrderbyData"><!--Order By-->
			<input id="hidtablename" type="hidden" name="hidtablename" runat="server"> <input id="txtHeader" type="hidden" style="WIDTH: 80%" runat="server" name="txtHeader"><!--In Header-->
			<input id="txtFooter" type="hidden" style="WIDTH: 80%" runat="server" name="txtFooter"><!--In Footer-->
			<input id="txtchk" type="hidden" name="txtchk" runat="server"> <input id="txthidformula" type="hidden" name="txthidformula" runat="server">
			<input id="infor" style="WIDTH: 80%" type="hidden" name="infor" runat="server"><!--In Formula-->
			<input type="hidden" id="txtRepType" name="txtRepType" runat="server"> <input type="hidden" id="txtreportname" name="txtreportname" runat="server">
			<input type="hidden" id="txtdate1" name="txtdate1" runat="server"> <input type="hidden" id="txtDate2" name="txtDate2" runat="server">
			
			<table align="center" height="100%">
				<tr>
					<td>
						<img src="../Images/progress2.gif" border="0" alt="Processing"><br>
						<img src="../Images/loading.gif" border="0" alt="Loading">
					</td>
				</tr>
			</table>
			  <input runat="server" type="hidden" name="hidHpos"  id="hidHpos" /><%-- hidden header elements position--%>     
     <input type="hidden" runat="server" name="hidHeaderformat" id="hidHeaderformat" /><%-- hidden basic formatting of header --%>                
     <%-- ends --%>
      <input type="hidden" name="hidDpos"  runat="server" id="hidDpos" /><%-- hidden details elements position --%>   
     <input type="hidden" runat="server" name="hidDetailsformat" id="hidDetailsformat"  /><%-- hidden basic formatting of details pane --%>                     
     <%-- ends --%>
      <input type="hidden" runat="server"  name="hidFpos"  id="hidFpos" /><%-- hidden footer elements position --%>          
      <input type="hidden" runat="server"  name="hidFooterformat" id="hidFooterformat" /><%-- hidden basic formatting of footer --%>
     <%-- ends --%>
     <input type="hidden" runat="server"  name="hidHeight"  id="hidHeight" /> <%--- To store heights of header and footer --%>
     <input type="hidden" runat="server"  name="hidFformat"  id="hidFformat" /> <%--- To store format of all footer objects --%>
     <input type="hidden" name="hidDformat" runat="server"  id="hidDformat" /> <%--- To store format of all details objects --%>
     <input type="hidden" name="hidHformat" runat="server"  id="hidHformat" /> <%--- To store format of all header objects --%>
     <input type="hidden" name="hidHformula" runat="server" id="hidHformula"  /> <%--- To store formula on dynamic objects --%>
     <input type="hidden" name="hidDformula" runat="server"  id="hidDformula"  /> <%--- To store formula on dynamic objects --%>
     <input type="hidden" name="hidFformula" runat="server" id="hidFformula"  /> <%--- To store formula on dynamic objects --%>
     <input type="hidden" name="hidHcolorcon"  runat="server" id="hidHcolorcon" /> <%--- To store color condition on dynamic objects of header --%>
     <input type="hidden" name="hidFcolorcon" runat="server"  id="hidFcolorcon" /> <%--- To store color condition on dynamic objects of footer--%>
     
    <%-- ends --%>
    
			 <input type="hidden" id="hidTables" runat="server"  name="hidTables" /><%-- hidden all tablename --%>
     <input type="hidden" id="hidTblall" runat="server"  name="hidTblall" /><%-- hidden final tablename --%>
     <input type="hidden" id="hidDatetable" runat="server"  name="hidDatetable" /><%-- dat condition table of the opened report --%>     
     <input type="hidden" runat="server"  name="hidReportname" id="hidReportname" /><%--  scope of the opened report --%>
     <input type="hidden" runat="server" name="hidReportscope" value="Simple" id="hidReportscope" /><%--  scope of the opened report --%>
     <input type="hidden" runat="server" name="hidReporttype" value="Simple" id="hidReporttype" /><%--  type of the opened report --%>
     <input type="hidden" runat="server" name="hidReportmode" value="new" id="hidReportmode" /><%--  mode of the opened report --%>
	 <input type="hidden" runat="server" name="divType" value="header" id="divType" /><%-- report constitute currently under construction --%>
	 <input type="hidden" runat="server" name="hidAlllabels" id="hidAlllabels" /><%-- dynamic label collection --%>
	 <input type="hidden" runat="server" name="hidDepartment" id="hidDepartment" /><%-- hidden saved report department --%>
	 <input type="hidden" runat="server" name="hidClient" id="hidClient" /><%-- hidden saved report client --%>
	 <input type="hidden" runat="server" name="hidLob" id="hidLob" /><%-- hidden saved report lob --%>
			 <%-- Hidden Fields for SQL Query --%>
			 
			   <input type="hidden" runat="server" name="hidSubtotal"  id="hidSubtotal" /><%-- hidden sub total--%>
    <input type="hidden" runat="server" name="txtStartdate"  id="txtStartdate" /><%-- hidden start date--%>
    <input type="hidden" runat="server" name="txtEnddate"  id="txtEnddate" /><%-- hidden end date--%>
    <input type="hidden" runat="server" name="hidWhere"  id="hidWhere" /><%-- hidden where condition--%>
    <input type="hidden" runat="server" name="hidGroupby"  id="hidGroupby" /><%-- hidden group by condition--%>
    <input type="hidden" runat="server" name="hidJoin"  id="hidJoin" /><%-- hidden join condition--%>
    <input type="hidden" runat="server" name="hidFormula"  id="hidFormula" /><%-- hidden formula condition--%>
    <input type="hidden" runat="server" name="hidFormulaname"  id="hidFormulaname" /><%-- hidden formula name--%>
    <input type="hidden" runat="server" name="hidOrderby"  id="hidOrderby" /><%-- hidden orderby condition--%>
    <input type="hidden" runat="server" name="hidHaving"  id="hidHaving" /><%-- hidden having condition--%>
    <input type="hidden" runat="server" name="hidColorcondition"  id="hidColorcondition" /><%-- hidden color condition--%>
    <input type="hidden" id="btnEx" value="aa&#013" />
    <input type="hidden" id="btnex2" name="btnEx2" value="aa&#013" />
    <%-- ends --%>
			
		</form>
</body>
</html>
