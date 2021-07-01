<%--Project Name: AutoWhiz
    Module Name: Accounts Management
    Page Name: display query
    vesrsion: 1.0
    Summary: display report
    Created By: usha

--%>
<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master"  AutoEventWireup="false" CodeFile="DispQuery.aspx.vb" Inherits="Misc_DispQuery" title="View BFI Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" language="javascript">
		var Flag;
		function ShowCalendar()
		{
			document.all["txtDate"].value = window.showModalDialog('/AutoWhiz/Calendar/Calendar.htm',document.all["txtDate"].value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
		}
		function ShowCalendar1()
		{
			document.all["txtDate1"].value = window.showModalDialog('/AutoWhiz/Calendar/Calendar.htm',document.all["txtDate"].value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
		}
		function winopen(strname)
		{
			url = "DispQuery1.aspx?name=" + strname + "&dept=" + document.getElementById("txtdept").value + "&client=" + document.getElementById("txtclient").value + "&lob=" + document.getElementById("txtlob").value + "&date=" + document.getElementById("txtDate").value + "&date1=" + document.getElementById("txtDate1").value;
			window.open(url)
		}
		function winopen1(strname)
		{
			url = "DispQuery2.aspx?name=" + strname + "&dept=" + document.getElementById("txtdept").value + "&client=" + document.getElementById("txtclient").value + "&lob=" + document.getElementById("txtlob").value + "&date=" + document.getElementById("txtDate").value + "&date1=" + document.getElementById("txtDate1").value;
			window.open(url)
		}
		
		</script>
		
		<input id=txtlob type="hidden" name=txtlob value="<%=Request("LOB")%>">
			<input id=txtclient type="hidden" name=txtclient value="<%=Request("Client")%>">
			<input id=txtdept type="hidden" name=txtdept value="<%=Request("Department")%>" />
			<INPUT id="txtFormula" type="hidden" style="WIDTH: 80%"  name="txtformula"><!--formula-->
			<INPUT id="txtheadingName" type="hidden" style="WIDTH: 80%" name="txtheadingName">
			<INPUT id="column" type="hidden"  style="WIDTH: 80%" name="column"><!--column-->
			<input id="wheredata" type="hidden" style="WIDTH: 80%" name="wheredata"><!--wheredata-->
			<input id="gruopdata" type="hidden" style="WIDTH: 80%" name="gruopdata"><!--gruopdata-->
			<input id="OrderbyData" type="hidden" style="WIDTH: 80%" name="OrderbyData"><!--Order By-->
			<input id="hidtablename" type="hidden"  name="hidtablename">
			<input id="txtHeader" type="hidden" style="WIDTH: 80%" name="txtHeader"><!--In Header-->
			<input id="txtFooter" type="hidden" style="WIDTH: 80%" name="txtFooter"><!--In Footer-->
			<input id="txtchk" type="hidden" name="txtchk"> 
			<input id="txthidformula" type="hidden"  name="txthidformula">
			<input id="infor" style="WIDTH: 80%" type="hidden" name="infor"><!--In Formula-->
			<div style="padding:20px;">
			<table style="width: 101%" >
				<caption>Saved Queries</caption>
				<tr>
					<td align="left">
                        <span style="font-size: 10pt">
					<font color=maroon>Note:&nbsp;Date must be in this format:- "mm/dd/yyyy"</font>
					<br>
                        </span>
                        <br>
						<label for="txtDate" class="label" title="Start Date">Start Date</label>
						<INPUT id="txtDate" style="FONT-SIZE: 12px; WIDTH: 140px; FONT-FAMILY: Verdana; HEIGHT: 13px"	tabIndex="1" type="text" maxLength="10" size="28" name="txtDate"  onkeypress="if (event.keyCode >= 47 && event.keyCode <= 57){event.returnValue = true;} else {event.returnValue = false;} if(event.keyCode==8){charRemove();}" class="textBox"><INPUT id="imageFromDate" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(/AutoWhiz/Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 23px; CURSOR: hand; BORDER-BOTTOM: 0px solid;" onclick="ShowCalendar(this.id);" tabIndex="1" type="button" name="imageFromDate" title="Enter Date">
						&nbsp;&nbsp;&nbsp;&nbsp; 
						<label for="txtDate1" class="label" title="End Date">End Date</label><INPUT id="txtDate1" style="FONT-SIZE: 12px; WIDTH: 140px; FONT-FAMILY: Verdana; HEIGHT: 13px"
							tabIndex="1" type="text" maxLength="10" size="28" name="txtDate1"  onkeypress="if (event.keyCode >= 47 && event.keyCode <= 57){event.returnValue = true;} else {event.returnValue = false;} if(event.keyCode==8){charRemove();}" class="textBox"><INPUT id="imageFromDate1" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(/AutoWhiz/Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 24px; CURSOR: hand; BORDER-BOTTOM: 0px solid;" onclick="ShowCalendar1(this.id);" tabIndex="2" type="button" name="imageFromDate" title="Enter Date">
							
					</td>
				</tr>
				<tr>
					<td>
						<div id="divquery" runat="server" style="WIDTH: 713px; height: 400px; overflow-y:scroll  "></div>
					</td>
				</tr>
				<tr>
					<td style="overflow:auto;">
					<table width="100%"><caption>Saved HTML Files</caption></table>
						<iframe id="FRMRESULST" style="VISIBILITY: visible; WIDTH: 744px; overflow-y:scroll;overflow-x:hidden; HEIGHT: 350px; BACKGROUND-COLOR: white" src="ReportHTMLForm.aspx"
							name="FRMRESULST" frameBorder="no" scrolling="auto"></iframe>
					</td>
				</tr>
			</table>
			</div>
</asp:Content>
<%--
---------------- Change History -------------------------
none

--%>

