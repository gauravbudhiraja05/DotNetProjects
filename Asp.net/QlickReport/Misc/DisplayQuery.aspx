<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DisplayQuery.aspx.vb" Inherits="Misc_DisplayQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
    <title></title>
    <link href="../App_Themes/Themes/StyleSheet.css" type="text/css" rel="stylesheet" />
    <script type ="text/javascript" >
		var Flag;
		function ShowCalendar()
		{
			document.all["txtDate"].value = window.showModalDialog('/QlickReport/Calendar.htm',document.all["txtDate"].value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			if(document.all["txtDate"].value=="undefined")
			document.all["txtDate"].value="";
		}
		function ShowCalendar1()
		{
			document.all["txtDate1"].value = window.showModalDialog('/QlickReport/Calendar.htm',document.all["txtDate"].value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			if(document.all["txtDate1"].value=="undefined")
			document.all["txtDate1"].value="";
		}
		function winopen(strname)
		{
		
		 var chk=strname.search('QueryBuilder'); 
                 
               if (chk!=-1)
               {
               url1 = "ResultOutput.aspx?name=" + strname + "&dept=" + document.getElementById("txtdept").value + "&client=" + document.getElementById("txtclient").value + "&lob=" + document.getElementById("txtlob").value + "&date=" + document.getElementById("txtDate").value + "&date1=" + document.getElementById("txtDate1").value;
			window.open(url1)
             }
             else
             {
			url = "DispQuery1.aspx?name=" + strname + "&dept=" + document.getElementById("txtdept").value + "&client=" + document.getElementById("txtclient").value + "&lob=" + document.getElementById("txtlob").value + "&date=" + document.getElementById("txtDate").value + "&date1=" + document.getElementById("txtDate1").value;
			window.open(url)
			}
		}
		function winopen1(strname)
		{
			url = "DispQuery2.aspx?name=" + strname + "&dept=" + document.getElementById("txtdept").value + "&client=" + document.getElementById("txtclient").value + "&lob=" + document.getElementById("txtlob").value + "&date=" + document.getElementById("txtDate").value + "&date1=" + document.getElementById("txtDate1").value;
			window.open(url)
		}
		/*function getrep(strname)
		{
			
			IDMS.AjaxClass1.FillQuery(strname,document.Form1.txtdept.value,document.Form1.txtclient.value,document.Form1.txtlob.value,0,filformula)
			IDMS.AjaxClass1.FillQuery(strname,document.Form1.txtdept.value,document.Form1.txtclient.value,document.Form1.txtlob.value,1,filorderby)
			IDMS.AjaxClass1.FillQuery(strname,document.Form1.txtdept.value,document.Form1.txtclient.value,document.Form1.txtlob.value,2,filwheredata)
			IDMS.AjaxClass1.FillQuery(strname,document.Form1.txtdept.value,document.Form1.txtclient.value,document.Form1.txtlob.value,3,filgrpby)
			IDMS.AjaxClass1.FillQuery(strname,document.Form1.txtdept.value,document.Form1.txtclient.value,document.Form1.txtlob.value,4,filheader)
			IDMS.AjaxClass1.FillQuery(strname,document.Form1.txtdept.value,document.Form1.txtclient.value,document.Form1.txtlob.value,5,filfooter)
			IDMS.AjaxClass1.FillQuery(strname,document.Form1.txtdept.value,document.Form1.txtclient.value,document.Form1.txtlob.value,6,filcolname)
			IDMS.AjaxClass1.FillQuery(strname,document.Form1.txtdept.value,document.Form1.txtclient.value,document.Form1.txtlob.value,7,filtabname)
			IDMS.AjaxClass1.FillQuery(strname,document.Form1.txtdept.value,document.Form1.txtclient.value,document.Form1.txtlob.value,8,filheadname)
			IDMS.AjaxClass1.FillQuery(strname,document.Form1.txtdept.value,document.Form1.txtclient.value,document.Form1.txtlob.value,9,filReportType)
			
			if ((document.all["txtDate"].value != "") && (document.all["txtDate1"].value != ""))
			{
				IDMS.AjaxClass1.ChkDate(document.Form1.hidtablename.value,chkdate)
			}
			//document.Form1.txtFormula.value = document.Form1.txthidformula.value;
			if ((document.Form1.txtchk.value == 'Y') && (document.all["txtDate"].value != "") && (document.all["txtDate1"].value != ""))
			{
				var str = " (Date Between '" + document.all["txtDate"].value + "' And '" + document.all["txtDate1"].value + "') ";
				if (document.Form1.txtFormula.value == "")
				{
						document.Form1.txtFormula.value = str;
				}
				else
				{
					document.Form1.txtFormula.value = document.Form1.txtFormula.value + " And " + str;
				}
				//alert(document.Form1.txtFormula.value)
			}
			document.Form1.infor.value=document.Form1.txtFormula.value;
		}
		function chkdate(res)
		{
			var strdata = res.value;
			document.Form1.txtchk.value = strdata;
		}
		function filformula(res)
		{
			if (res.value!="N")
			{
				var strdata = res.value;
				document.Form1.txtFormula.value = strdata;
			}
			else
			{
				document.Form1.txtFormula.value = "";
			}
		}
		function filorderby(res)
		{
			if (res.value!="N")
			{
				var strdata = res.value;
				document.Form1.OrderbyData.value = strdata;
			}
			else
			{
				document.Form1.OrderbyData.value = "";
			}
		}
		function filwheredata(res)
		{
			if (res.value!="N")
			{
				var strdata = res.value;
				document.Form1.wheredata.value = strdata;
			}
			else
			{
				document.Form1.wheredata.value = "";
			}
		}
		function filgrpby(res)
		{
			if (res.value!="N")
			{
				var strdata = res.value;
				document.Form1.gruopdata.value = strdata;
			}
			else
			{
				document.Form1.gruopdata.value = "";
			}
		}
		function filheader(res)
		{
			if (res.value!="N")
			{
				var strdata = res.value;
				document.Form1.txtHeader.value = strdata;
			}
			else
			{
				document.Form1.txtHeader.value = "";
			}
		}
		function filfooter(res)
		{
			if (res.value!="N")
			{
				var strdata = res.value;
				document.Form1.txtFooter.value = strdata;
			}
			else
			{
				document.Form1.txtFooter.value = "";
			}
		}
		function filcolname(res)
		{
			if (res.value!="N")
			{
				var strdata = res.value;
				document.Form1.column.value = strdata;
			}
			else
			{
				document.Form1.column.value = "";
			}
		}
		function filReportType(res)
		{
			var reptyp=res.value;
			document.Form1.txtRepType.value=reptyp;		
		}
		function filtabname(res)
		{
			if (res.value!="N")
			{
				var strdata = res.value;
				document.Form1.hidtablename.value = strdata;
			}
			else
			{
				document.Form1.hidtablename.value = "";
			}
		}
		function filheadname(res)
		{
			if (res.value!="N")
			{
				var strdata = res.value;
				document.Form1.txtheadingName.value = strdata;
			}
			else
			{
				document.Form1.txtheadingName.value = "";
			}
		}*/
		</script>
</head>
<body>
    <form id="form1" runat="server">
   <input id="txtlob" type="hidden" name="txtlob" value="<%=Request("LOB")%>"/>
			<input id="txtclient" type="hidden" name="txtclient" value="<%=Request("Client")%>"/>
			<input id="txtdept" type="hidden" name="txtdept" value="<%=Request("Department")%>" />
			<input id="txtFormula" type="hidden" style="WIDTH: 80%"  name="txtformula"/><!--formula-->
			<input id="txtheadingName" type="hidden" style="WIDTH: 80%" name="txtheadingName"/>
			<input id="column" type="hidden"  style="WIDTH: 80%" name="column"/><!--column-->
			<input id="wheredata" type="hidden" style="WIDTH: 80%" name="wheredata"/><!--wheredata-->
			<input id="gruopdata" type="hidden" style="WIDTH: 80%" name="gruopdata"/><!--gruopdata-->
			<input id="OrderbyData" type="hidden" style="WIDTH: 80%" name="OrderbyData"/><!--Order By-->
			<input id="hidtablename" type="hidden"  name="hidtablename"/>
			<input id="txtHeader" type="hidden" style="WIDTH: 80%" name="txtHeader"/><!--In Header-->
			<input id="txtFooter" type="hidden" style="WIDTH: 80%" name="txtFooter"/><!--In Footer-->
			<input id="txtchk" type="hidden" name="txtchk" /> 
			<input id="txthidformula" type="hidden"  name="txthidformula"/>
			<input id="infor" style="WIDTH: 80%" type="hidden" name="infor"/><!--In Formula-->
			<div style="padding:20px;"/>
			<table style="width: 101%" >
				<caption>Saved Queries</caption>
				<tr>
					<td align="left" >
                        <span style="font-size: 10pt">
					<font color="maroon">Note:&nbsp;Date must be in this format:- "mm/dd/yyyy"</font>
					<br/>
                        </span>
                        <br/>
						<label class="label" for="txtDate" title="Start Date">Start Date</label><input id="txtDate" style="FONT-SIZE: 12px; WIDTH: 140px; FONT-FAMILY: Verdana; HEIGHT: 13px"	tabIndex="1" type="text" maxLength="10" size="28" name="txtDate"  onkeypress="if (event.keyCode >= 47 && event.keyCode <= 57){event.returnValue = true;} else {event.returnValue = false;} if(event.keyCode==8){charRemove();}" class="textBox"><input id="imageFromDate" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(/QlickReport/Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 23px; CURSOR: hand; BORDER-BOTTOM: 0px solid;" onclick="ShowCalendar(this.id);" tabIndex="1" type="button" name="imageFromDate" title="Select Date" />
						&nbsp;&nbsp;&nbsp;&nbsp; <label class="label" for ="txtDate1" title="End Date">End Date</label><input id="txtDate1" style="FONT-SIZE: 12px; WIDTH: 140px; FONT-FAMILY: Verdana; HEIGHT: 13px"	tabIndex="1" type="text" maxLength="10" size="28" name="txtDate1"  onkeypress="if (event.keyCode >= 47 && event.keyCode <= 57){event.returnValue = true;} else {event.returnValue = false;} if(event.keyCode==8){charRemove();}" class="textBox"><input id="imageFromDate1" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(/AutoWhiz/Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 24px; CURSOR: hand; BORDER-BOTTOM: 0px solid;" onclick="ShowCalendar1(this.id);" tabIndex="2" type="button" name="imageFromDate" title="Select Date" />
							
					</td>
				</tr>
				<tr>
					<td>
						<div id="divquery" runat="server" style="WIDTH: 744px; height: 400px;"></div>
					</td>
				</tr>
				<tr>
					<td style="overflow:auto;">
					<table width="100%"><caption>Saved HTML Files</caption></table>
						<iframe id="FRMRESULST"  style="VISIBILITY: visible; WIDTH: 744px; overflow-y:scroll; overflow-x:hidden; HEIGHT: 350px; BACKGROUND-COLOR: white" src="ReportHTMLForm.aspx" title="FRMresult" name="FRMRESULST" frameBorder="no" scrolling="no"></iframe>
					</td>
				</tr>
			
			</table>
		
			
    </form>
</body>
</html>
