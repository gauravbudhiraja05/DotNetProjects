<%@ Page Language="VB" AutoEventWireup="false" CodeFile="where.aspx.vb" Inherits="QueryBuilder_where" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
	<head>
		<title>Where</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<link href="../App_Themes/Themes/StyleSheet.css" type="text/css" rel="stylesheet"/>
		<script language="vbscript" type="text/vbscript">
		<!--
		function getData()
			dim strDataAll, i, intSetNo, strData, a
			strData = "<%=Request("Data")%>"
			strDataAll = window.opener.document.getElementById("wheredata").value
			document.Form1.all.value = strDataAll
			arrData = split(strDataAll,"and")
			intSetNo = 0
			for i=0 to ubound(arrData)
				if instr(1, arrData(i), trim(strData) & "=") > 0 then 
					if intSetNo = 0	then
						document.Form1.Data1.value = trim(replace(replace(arrData(i), trim(strData) & "=",""),"'",""))
						document.Form1.cboItem1.value = document.Form1.Data1.value
						intSetNo = 1
					else
						document.Form1.Data2.value = trim(replace(replace(arrData(i), trim(strData) & "=",""),"'",""))
						document.Form1.cboItem2.value = document.Form1.Data2.value
					end if
					document.Form1.all.value = replace(document.Form1.all.value, arrData(i),"")
				elseif instr(1, arrData(i), trim(strData) & " Like") > 0 then 
					if instr(1, arrData(i), "'%") > 0 and instr(1, arrData(i), "%'") > 0 then
						document.Form1.cboFormula3.selectedIndex = 1
					elseif instr(1, arrData(i), "'%") > 0 then
						document.Form1.cboFormula3.selectedIndex = 2
					elseif instr(1, arrData(i), "%'") > 0 then
						document.Form1.cboFormula3.selectedIndex = 0
					end if
					document.Form1.Data3.value = trim(replace(replace(replace(arrData(i), trim(strData) & " Like",""),"'",""),"%",""))
					document.Form1.txtLike.value = document.Form1.Data3.value
					document.Form1.all.value = replace(document.Form1.all.value, arrData(i),"")
				elseif instr(1, arrData(i), trim(strData) & " in") > 0 then 
					document.Form1.Data4.value = trim(replace(replace(replace(replace(arrData(i), trim(strData) & " in",""),"'",""),"(",""),")",""))
					for a=0 to document.Form1.cboItem4.length -1
						if instr(1,"," & document.Form1.Data4.value & ",", "," & document.Form1.cboItem4.item(a).value & ",") > 0 then
							document.Form1.cboItem4.item(a).selected = true
						end if
					next
					document.Form1.all.value = replace(document.Form1.all.value, arrData(i),"")
				end if
			next
			document.Form1.all.value = trim(replace(replace(replace(document.Form1.all.value,"andandandand","and"),"andandand","and"),"andand","and"))
			if trim(document.Form1.all.value) = "and"  then document.Form1.all.value = ""
			if left(trim(document.Form1.all.value),3) = "and"  then
				document.Form1.all.value = trim(mid(trim(document.Form1.all.value),4))
			end if
			if right(trim(document.Form1.all.value),3) = "and"  then
				document.Form1.all.value = trim(left(trim(document.Form1.all.value),len(document.Form1.all.value)-3))
			end if
			document.Form1.all.value = trim(document.Form1.all.value)
		end function
		//-->
		</script>
		<script language="javascript" type="text/javascript">
		<!--
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
		function putdata()
		{
			if (document.Form1.cboItem1.selectedIndex > 0)
			{
				if (blank(document.Form1.all.value))
				{document.Form1.all.value  = document.Form1.data.value + document.Form1.cboFormula1.value + "'" + document.Form1.cboItem1.value  + "'" }
				else 
				{document.Form1.all.value  = document.Form1.all.value + " and " + document.Form1.data.value + document.Form1.cboFormula1.value + "'" + document.Form1.cboItem1.value  + "'" }
			}
			if (document.Form1.cboItem2.selectedIndex > 0)
			{
				if (blank(document.Form1.all.value))
				{document.Form1.all.value  =  document.Form1.data.value + document.Form1.cboFormula2.value + "'" + document.Form1.cboItem2.value  + "'" }
				else 
				{document.Form1.all.value  = document.Form1.all.value + " and " + document.Form1.data.value + document.Form1.cboFormula2.value + "'" + document.Form1.cboItem2.value  + "'" }
			}
			if (blank(document.Form1.txtLike.value)==false)
			{
				if (document.Form1.cboFormula3.selectedIndex == 0)
				{
					if (blank(document.Form1.all.value))
					{document.Form1.all.value  = document.Form1.data.value + " Like '" + document.Form1.txtLike.value  + "%'" }
					else 
					{document.Form1.all.value  = document.Form1.all.value + " and " + document.Form1.data.value + " Like '" + document.Form1.txtLike.value  + "%'" }
				}
				else if (document.Form1.cboFormula3.selectedIndex == 1)
				{
					if (blank(document.Form1.all.value))
					{document.Form1.all.value  = document.Form1.data.value + " Like '%" + document.Form1.txtLike.value  + "%'" }
					else 
					{document.Form1.all.value  = document.Form1.all.value + " and " + document.Form1.data.value + " Like '%" + document.Form1.txtLike.value  + "%'" }
				}
				else if (document.Form1.cboFormula3.selectedIndex == 2)
				{
					if (blank(document.Form1.all.value))
					{document.Form1.all.value  = document.Form1.data.value + " Like '%" + document.Form1.txtLike.value  + "'" }
					else 
					{document.Form1.all.value  = document.Form1.all.value + " and " + document.Form1.data.value + " Like '%" + document.Form1.txtLike.value  + "'" }
				}
			}
			if (document.Form1.cboItem4.selectedIndex > -1)
			{
				var strData =""
				for (var i=0;i<document.Form1.cboItem4.length;i++)
				{
					if (document.Form1.cboItem4.options[i].selected)
					{
						if (blank(strData))
						{
							strData = "'" + document.Form1.cboItem4.options[i].value + "'"
						}
						else
						{
							strData = strData + ",'" + document.Form1.cboItem4.options[i].value + "'"
						}
					}
				}
				if (blank(document.Form1.all.value))
				{document.Form1.all.value = document.Form1.data.value + " in (" + strData  + ")" }
				else 
				{document.Form1.all.value = document.Form1.all.value + " and " + document.Form1.data.value + " in (" + strData  + ")" }
			}
			window.opener.document.getElementById("wheredata").value = document.Form1.all.value;
			/*if (blank(document.Form1.txteql.value)==false)
			{
				if (blank(window.opener.frmquerybl.wheredata.value))
				{window.opener.frmquerybl.wheredata.value  =  document.Form1.data.value + "='" + document.Form1.txteql.value  + "'" }
				else 
				{window.opener.frmquerybl.wheredata.value  = window.opener.frmquerybl.wheredata.value + " " + document.Form1.conoprt.value + " " +  document.Form1.data.value + " ='" + document.Form1.txteql.value  + "'" 	}
			}
			if (blank(document.Form1.txtneql.value)==false)
			{
				if (blank(window.opener.frmquerybl.wheredata.value))
				{window.opener.frmquerybl.wheredata.value  =  document.Form1.data.value + "<>'" + document.Form1.txtneql.value  + "'" }
				else 
				{window.opener.frmquerybl.wheredata.value  = window.opener.frmquerybl.wheredata.value + " " + document.Form1.conoprt.value + " " +  document.Form1.data.value + " <>'" + document.Form1.txtneql.value  + "'" 	}
			}
			 
			if (blank(document.Form1.txtg.value)==false)
			{
				if (blank(window.opener.frmquerybl.wheredata.value))
				{window.opener.frmquerybl.wheredata.value  =  document.Form1.data.value + ">'" + document.Form1.txtg.value  + "'" }
				else 
				{window.opener.frmquerybl.wheredata.value  = window.opener.frmquerybl.wheredata.value + " " + document.Form1.conoprt.value + " " +  document.Form1.data.value + " >'" + document.Form1.txtg.value  + "'" 	}
			} 	
			
			if (blank(document.Form1.txtgeql.value)==false)
			{
				if (blank(window.opener.frmquerybl.wheredata.value))
				{window.opener.frmquerybl.wheredata.value  =  document.Form1.data.value + ">='" + document.Form1.txtgeql.value  + "'" }
				else 
				{window.opener.frmquerybl.wheredata.value  = window.opener.frmquerybl.wheredata.value + " " + document.Form1.conoprt.value + " " +  document.Form1.data.value + " >='" + document.Form1.txtgeql.value  + "'" 	}
			} 	
			
			if (blank(document.Form1.txtl.value)==false)
			{
				if (blank(window.opener.frmquerybl.wheredata.value))
				{window.opener.frmquerybl.wheredata.value  =  document.Form1.data.value + "<'" + document.Form1.txtl.value  + "'" }
				else 
				{window.opener.frmquerybl.wheredata.value  = window.opener.frmquerybl.wheredata.value + " " + document.Form1.conoprt.value + " " +  document.Form1.data.value + " <'" + document.Form1.txtl.value  + "'" 	}
			} 	
			
			if (blank(document.Form1.txtleql.value)==false)
			{
				if (blank(window.opener.frmquerybl.wheredata.value))
				{window.opener.frmquerybl.wheredata.value  =  document.Form1.data.value + "<='" + document.Form1.txtleql.value  + "'" }
				else 
				{window.opener.frmquerybl.wheredata.value  = window.opener.frmquerybl.wheredata.value + " " + document.Form1.conoprt.value + " " +  document.Form1.data.value + " <='" + document.Form1.txtleql.value  + "'" 	}
			} 		
			
			if (blank(document.Form1.txtlikeeql.value)==false)
			{
				if (blank(window.opener.frmquerybl.wheredata.value))
				{window.opener.frmquerybl.wheredata.value  =  document.Form1.data.value + " Like '" + document.Form1.txtlikeeql.value + "%" + "'" }
				else 
				{window.opener.frmquerybl.wheredata.value  = window.opener.frmquerybl.wheredata.value + " " + document.Form1.conoprt.value + " " +  document.Form1.data.value + " like'" + document.Form1.txtlikeeql.value + "%" + "'" 	}
			} 	
			
			if (blank(document.Form1.txtlikeeqlmid.value)==false)
			{
				if (blank(window.opener.frmquerybl.wheredata.value))
				{window.opener.frmquerybl.wheredata.value  =  document.Form1.data.value + " Like '%" + document.Form1.txtlikeeqlmid.value + "%" + "'" }
				else 
				{window.opener.frmquerybl.wheredata.value  = window.opener.frmquerybl.wheredata.value + " " + document.Form1.conoprt.value + " " +  document.Form1.data.value + " like'%" + document.Form1.txtlikeeqlmid.value + "%" + "'" 	}
			} 
			if (blank(document.Form1.txtlikeeqlend.value)==false)
			{
				if (blank(window.opener.frmquerybl.wheredata.value))
				{window.opener.frmquerybl.wheredata.value  =  document.Form1.data.value + " Like '" + "%" + document.Form1.txtlikeeqlend.value + "'" }
				else 
				{window.opener.frmquerybl.wheredata.value  = window.opener.frmquerybl.wheredata.value + " " + document.Form1.conoprt.value + " " +  document.Form1.data.value + " like'"  + "%" + document.Form1.txtlikeeqlend.value + "'" 	}
			} */
			//document.Form1.submit();
			window.close() 
		}
function cmdclosed_onclick() {

}

		//-->
		</script>
	</head>
	<body onload="vbscript:getData()">
		<form id="Form1"  runat="server">
		<input type="hidden" id="data" value="<%=request("data")%>" />
		<input type="hidden" id="all" value=""/>
		<input type="hidden" id="Data1" value=""/>
		<input type="hidden" id="Data2" value=""/>
		<input type="hidden" id="Data3" value=""/>
		<input type="hidden" id="Data4" value=""/>
		<table border="0"  cellspacing="1" summary="Fill Where Data">
			<tr><td><b>Where '<%=request("data")%>'</b></td></tr>
			<tr><td>
				<table summary="Fill Where Data">
					<tr>
						<td scope="colgroup"><label for="cboFormula1"></label><select id="cboFormula1" name="cboFormula1">
						<option value="=">Equals To</option>
						<option value="<>">Not Equals To</option>
						<option value=">">Greater Than</option>
						<option value="gt=">Greater Than Equal To</option>
						<option value="<">Less Than</option>
						<option value="<=">Less Than Equal To</option>
						</select></td>
						<td><label for="cboItem1"></label><select id="cboItem1" name="cboItem1">'<%=strData1%>'</select></td>
					</tr>
					<tr><td scope="col">and</td></tr>
					<tr>
						<td scope="colgroup"><label for="cboFormula2"></label><select id="cboFormula2" name="cboFormula2">
						<option value="=">Equals To</option>
						<option value="<>">Not Equals To</option>
						<option value=">">Greater Than</option>
						<option value="gt=">Greater Than Equal To</option>
						<option value="<">Less Than</option>
						<option value="<=">Less Than Equal To</option>
						</select></td>
						<td><label for="cboItem2"></label><select id="cboItem2" name="cboItem2">'<%=strData1%>'</select></td>
					</tr>
					<tr><td>and</td></tr>
					<tr>
						<td scope="colgroup"><label for="cboFormula3"></label><select id="cboFormula3" name="cboFormula3">
						<option value="Starts With">Starts With</option>
						<option value="In Mid of">In Mid of</option>
						<option value="Ends With">Ends With</option>
						</select></td>
						<td><label for="txtLike"></label><input type="text" id="txtLike" name="txtLike"/></td>
					</tr>
					<tr><td>and</td></tr>
					<tr>
						<td valign="top" scope="col">In</td>
						<td scope="colgroup"><label for="cboItem4"></label><select id="cboItem4" size="5" multiple="multiple"  name="cboItem4">'<%=strData%>'</select></td>
					</tr>
				</table>
			</td></tr>
			<tr><td colspan="2" scope="colgroup" align="left" ><input type="button" id="cmdclosed"  class ="button"  value="Done" onclick="putdata()"/></td> </tr>
			<tr><td colspan="2" scope="colgroup" style="color:red"><br/><b>Note :</b> Use Ctrl(^) + mouse click for multiple selection/ deselection.<br/><br/></td></tr>
		</table>
		</form>
	</body>
</html>
