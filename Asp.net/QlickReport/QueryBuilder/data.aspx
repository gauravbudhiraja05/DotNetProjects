<%@ Page Language="VB" AutoEventWireup="false" CodeFile="data.aspx.vb" Inherits="QueryBuilder_data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
		<title>Data</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<link href="../App_Themes/Themes/StyleSheet.css" type="text/css" rel="stylesheet"/>
	
	
	
	</head>
	<body>
		<form id="Form1" runat="server" >
		<script type="text/javascript" language="javascript">
<!--
<%dim strFunction=""%>

function blank(s)
{
var slen = s.length
var i
	for(i=0;i<slen;i++)
	{
	if(s.charAt(i)!=" ")return false
	}
	return true
}

function putdata(strFunction)
{


	var strOldString = strFunction + "(<%=request("data")%>)";
	var strNewString = document.Form1.cbodatavalue.value ;
	window.opener.document.getElementById("datashowforblnank3").value =strNewString;
	
	string = window.opener.document.getElementById("Showdata").value;
//	window.opener.document.getElementById("Showdata").value = string.replace(strOldString,strNewString);
	if(window.opener.document.getElementById("Showdata").value=="")
	window.opener.document.getElementById("Showdata").value =strNewString;
	
	else
	{
	
	window.opener.document.getElementById("Showdata").value =window.opener.document.getElementById("Showdata").value+","+strNewString;
	queryBuilderAjax.checkDuplicate(window.opener.document.getElementById("Showdata").value,strNewString,strOldString,resPonse);
	}
var val1="<%=request("div")%>";
//var val2="<%=request("btn")%>";
var val2="<%=request("old")%>";
	window.opener.removeItem(val1,val2);
  	window.opener.binddivs();
/*
	//Start - Code to replace old function
	string = window.opener.frmquerybl.Showdata.value 
	window.opener.frmquerybl.Showdata.value = string.replace('<%=request("data")%>',"")
	string = window.opener.frmquerybl.Showdata.value 
	window.opener.frmquerybl.Showdata.value = string.replace('Count()',"")
	string = window.opener.frmquerybl.Showdata.value 
	window.opener.frmquerybl.Showdata.value = string.replace('Max()',"")
	string = window.opener.frmquerybl.Showdata.value 
	window.opener.frmquerybl.Showdata.value = string.replace('Min()',"")
	string = window.opener.frmquerybl.Showdata.value 
	window.opener.frmquerybl.Showdata.value = string.replace('Avg()',"")
	string = window.opener.frmquerybl.Showdata.value 
	window.opener.frmquerybl.Showdata.value = string.replace('Sum()',"")
	string = window.opener.frmquerybl.Showdata.value 
	window.opener.frmquerybl.Showdata.value = string.replace('$$$$$',"$")
	string = window.opener.frmquerybl.Showdata.value 
	window.opener.frmquerybl.Showdata.value = string.replace('$$$$',"$")
	string = window.opener.frmquerybl.Showdata.value 
	window.opener.frmquerybl.Showdata.value = string.replace('$$$',"$")
	string = window.opener.frmquerybl.Showdata.value 
	window.opener.frmquerybl.Showdata.value = string.replace('$$',"$")
	string = window.opener.frmquerybl.Showdata.value 
	if(string.charAt(0)=="$")
	{
		window.opener.frmquerybl.Showdata.value = string.substr(1,string.length - 1)
	}
	string = window.opener.frmquerybl.Showdata.value 
	if(string.charAt(string.length-1)=="$")
	{
		window.opener.frmquerybl.Showdata.value = string.substr(0,string.length-1)
	}
	//End - Code to replace old function
	if (!blank(window.opener.frmquerybl.Showdata.value))
	{
		window.opener.frmquerybl.Showdata.value  =  window.opener.frmquerybl.Showdata.value + "$" + document.Form1.cbodatavalue.value 
		window.opener.frmquerybl.gruopdata.value  = window.opener.frmquerybl.gruopdata.value + "$" +  document.Form1.data.value
		
		//window.opener.frmquerybl.Showdata.value  =  document.Form1.cbodatavalue.value   
		//window.opener.frmquerybl.gruopdata.value  =  document.Form1.data.value
	}
	else
	{
		window.opener.frmquerybl.Showdata.value  =  document.Form1.cbodatavalue.value   
		window.opener.frmquerybl.gruopdata.value  =  document.Form1.data.value   
		
	}
*/
//document.Form1.submit()
window.close() 
}
function resPonse(response)
{
    if(response!="")
    {
        window.opener.document.getElementById("Showdata").value=response.value;
    }
}
//-->
</script>
		<input type="hidden" id="data" value="<%=request("data")%>" />

		<table style="text-align:center;" summary="Select Formula">
			<tr><td scope="col">
			<label for="cbodatavalue" ></label>
			<select id="cbodatavalue">
				<%if instr(request("all"), "Count(" & request("data") & ")") > 0 then
					strFunction = "Count" %>
					<option value="Count(<%=request("data")%>)" selected="selected"> Count of <%=request("data")%></option>
				<%else%>
					<option value="Count(<%=request("data")%>)"> Count of <%=request("data")%></option>
				<%end if
				if instr(request("all"), "Max(" & request("data") & ")") > 0 then
					strFunction = "Max"%>
					<option value="Max(<%=request("data")%>)" selected="selected"> Max of <%=request("data")%></option>
				<%else%>
					<option value="Max(<%=request("data")%>)"> Max of <%=request("data")%></option>
				<%end if%>
				<%if instr(request("all"), "Min(" & request("data") & ")") > 0 then
					strFunction = "Min"%>
					<option value="Min(<%=request("data")%>)" selected="selected"> Min of <%=request("data")%></option>
				<%else%>
					<option value="Min(<%=request("data")%>)"> Min of <%=request("data")%></option>
				<%end if%>
				<%if instr(request("all"), "Avg(" & request("data") & ")") > 0 then
					strFunction = "Avg"%>
					<option value="Avg(<%=request("data")%>)" selected="selected"> Avg of <%=request("data")%></option>
				<%else%>
					<option value="Avg(<%=request("data")%>)"> Avg of <%=request("data")%></option>
				<%end if%>
				<%if instr(request("all"), "Sum(" & request("data") & ")") > 0 then
					strFunction = "Sum"%>
					<option value="Sum(<%=request("data")%>)" selected="selected"> Sum of <%=request("data")%></option>
				<%else%>
					<option value="Sum(<%=request("data")%>)"> Sum of <%=request("data")%></option>
				<%end if%>
				</select>
			</td>
			<td  scope="col" align="center"><input type="button" id="cmdclosed"  class ="button"  onclick="putdata('<%=strFunction%>');" value="Done"/></td> </tr>
		</table>
		
		</form>
	</body>
</html>