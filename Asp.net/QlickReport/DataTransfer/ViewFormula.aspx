<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewFormula.aspx.vb" Inherits="DataTransfer_ViewFormula"  Title="Formula"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
    <title>Set Formula</title>

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
			
			function chkNumeric()
			{
				if (isNaN(document.forms[0].txtaddformula.value) == true)
				{
					document.forms[0].txtaddformula.value = "";
					alert("Enter only numeric value which is allowed in the textbox.");
					document.forms[0].txtaddformula.focus();
				}
			}
			function setADD(a) 
			{
				if (!blank(a))
				{
					if (blank(document.forms[0].txtFormula.value))
					{
						document.forms[0].txtFormula.value = document.forms[0].txtFormula.value +  a
					}
					else 
					{
						document.forms[0].txtFormula.value = document.forms[0].txtFormula.value + " " + "+" + " " + a
					}
				}
			}
			function setFormula(a)
			{
				
				if (!blank(a))
				{
					if (blank(document.forms[0].txtFormula.value))
					{
						//document.frmsetformula.txtFormula.value = document.frmsetformula.txtFormula.value +  a
						document.forms[0].txtFormula.value = document.forms[0].txtFormula.value +  a
					}
					else 
					{
						//document.frmsetformula.txtFormula.value = document.frmsetformula.txtFormula.value + " " + a
						document.forms[0].txtFormula.value = document.forms[0].txtFormula.value + " " + a
					}
				}
			}
			function setData()
			{
				
			    var cnt = window.opener.getListFormulaLenth();
				var cnt1234 = window.opener.getListFormulaLenth();
			
				var arrnameform=document.forms[0].duplicatename.value.split(",")
				for(i=0;i<=arrnameform.length-1;i++)
				{
				var formval=document.forms[0].txtFormulaName.value.toLowerCase();
				if (arrnameform[i]==formval)
				{
				alert("Formula name exists.Choose another name.");
				return;
				}
				}
				
				if (document.forms[0].txtfname.value==document.forms[0].txtFormulaName.value)
				{
				alert("Formula name exists.Choose another name.");
			return;
				}
				else if (blank(document.form1.txtFormulaName.value))
				{
				alert("Enter Formula Name.");
				}
				else if (!blank(document.form1.txtFormula.value))
				{
					var createdFormula=document.forms[0].txtFormula.value
					/*var len = createdFormula.split(",")
					for (i=0;i<len.length-1;i++)
					{
						document.frmsetformula.txtFormula.value=createdFormula.replace(",","~~")
						createdFormula=document.frmsetformula.txtFormula.value
					}*/
					/////////////////////////////aaaa
					var arrdata = document.forms[0].txtFormula.value;
					var arrFormulaname = document.forms[0].txtFormulaName.value;
					var oOption=window.opener.document.createElement("OPTION");
					window.opener.setListFormula(oOption)
					oOption.text=arrFormulaname;
					oOption.value=arrdata;
////					var oOption1=window.opener.document.createElement("OPTION");
////					//window.opener.document.forms[0].elements["lstgroup"].options.add(oOption1)
////					window.opener.setListGroup(oOption1)
////					oOption1.text=arrdata;
////					oOption1.value=arrdata;
					
					window.close()
				}
				
				else if(document.forms[0].cbofdata.selectedIndex != -1 && document.forms[0].cbofunc.selectedIndex != -1)
				{
					var arrdata = document.forms[0].cbofunc.value + "(" + document.forms[0].cbofdata.value + ")";
					var oOption=window.opener.document.createElement("OPTION");
					window.opener.setListFormula(oOption)
					window.opener.setListGroup(oOption)
					oOption.text=arrFormulaname;
					oOption.value=arrdata;
					
					window.close()
				}
			}

			function putdata()
			{
			    var oDDL = document.all("cbofdata");
	            //document.forms[0].txtfname.value=oDDL.options[oDDL.selectedIndex].text;
	            //change suvi
	            //document.forms[0].formulaindex.value=document.forms[0].cbofdata.selectedIndex
	            
	            
				if (document.forms[0].txtFormula.value=="undefined")
				{
				document.forms[0].txtFormula.value=""
				}
				var strformula=document.forms[0].cbofdata.value
				var oDDL = document.all("cbofdata");
                var curText = oDDL.options[oDDL.selectedIndex].text;
//				document.forms[0].txtFormulaName.value=document.forms[0].cbofdata.selectedIndex.text
                document.forms[0].txtFormulaName.value=curText

				/*var len = strformula.split("~~")
					for (i=0;i<len.length-1;i++)
					{
						strformula = strformula.replace("~~",",")
					}*/
					
				if (blank(document.forms[0].txtFormula.value))
				{
					document.forms[0].txtFormula.value =  strformula
				}
				else 
				{
					document.forms[0].txtFormula.value = document.forms[0].txtFormula.value  + " " +  strformula
				}
			}
			//////////////change by suvi
function UpdateData()
{
	 var seltext=document.forms[0].txtFormulaName.value;
	 var selval=document.forms[0].txtFormula.value;
	window.opener.UpdateFormula(seltext,selval)
    window.close()
}
//			//-->
//////////////change by suvi
		</script>

   <%-- <link href="../App_Themes/Themes/StyleSheet.css" rel="stylesheet" type="text/css" />--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input type="hidden" id="txttype" name="txttype" runat="server"/>
    <input type="hidden" id="txtfname" name="txtfname" runat="server"/>
    <input type="hidden" id="formulaindex" name="formulaindex" runat="server"/>
    <input type="hidden" id="duplicatename" name="duplicatename" runat="server"/>
			<table border="0" align="center" summary ="Table">
				<tr>
					<td valign="top" height="20" scope="col" style="width: 182px" >
						<input id="cmdPlus" class="button" type="button" value="+" style="VISIBILITY:visible;WIDTH:18px;HEIGHT:18px;BACKGROUND-COLOR:white"
							 onclick="setFormula('+')"/> <input id="cmdMinus" class="button" type="button" value="-" style="VISIBILITY:visible;WIDTH:18px;HEIGHT:18px;BACKGROUND-COLOR:white"
							 onclick="setFormula('-')"/> <input id="cmdMultiply" class="button" type="button" value="*" style="VISIBILITY:visible;WIDTH:18px;HEIGHT:18px;BACKGROUND-COLOR:white"
							 onclick="setFormula('*')"/> <input id="cmdDivide" class="button" type="button" value="/" style="VISIBILITY:visible;WIDTH:18px;HEIGHT:18px;BACKGROUND-COLOR:white"
							 onclick="setFormula('/')"/> <input id="cmdOpen" class="button" type="button" value="(" style="VISIBILITY:visible;WIDTH:18px;HEIGHT:18px;BACKGROUND-COLOR:white"
							 onclick="setFormula('(')"/> <input id="cmdClose" class="button" type="button" value=")" style="VISIBILITY:visible;WIDTH:18px;HEIGHT:18px;BACKGROUND-COLOR:white"
							 onclick="setFormula(')')"/><br/>
						<br/>
						<label for="txtFormula"></label>
						<textarea id="txtFormula" class="button"   style="VISIBILITY:visible;WIDTH:183px;HEIGHT:183px;BACKGROUND-COLOR:white"></textarea>
					</td>
					<td valign="top" scope ="col" >
					<label for="cbofdata"></label>
					<select size="14" id="cbofdata" name="cbofdata"  runat="server" onclick="putdata();" class="button"
							style="BORDER-TOP-STYLE: outset; BORDER-RIGHT-STYLE: outset; BORDER-LEFT-STYLE: outset; LIST-STYLE-TYPE: disc; BORDER-BOTTOM-STYLE: outset"></select></td>
				</tr>
				<tr>
					<td colspan="2" scope ="colgroup" >
					<label for="txtaddformula"></label>
					<input maxlength="20" type="text" id="txtaddformula"  class="button" style="VISIBILITY:visible;WIDTH:124px;HEIGHT:16px;BACKGROUND-COLOR:white"
							size="15" onblur="chkNumeric()"/> <input type="button" class="button" style="VISIBILITY:visible;WIDTH:40px;"
							value="Add" onclick="setADD(document.forms[0].txtaddformula.value)"/></td>
				</tr>
				<%--<tr>
					<td colspan="2" scope ="colgroup" style ="color:black">
						Choose Function
						<select id="cbofunc" name="cbofunc">
							<option value="selected" selected="selected">--Select--</option>
							<option value="Count">Count</option>
							<option value="Max">Max</option>
							<option value="Min">Min</option>
							<option value="Avg">Avg</option>
							<option value="Sum">Sum</option>
						</select>
					</td>
				</tr>--%>
				
				<tr>
					<td colspan="2" scope ="colgroup" style ="color:black">Formula Name
					<label for="txtFormulaName"></label>
					 <Input type="text" id="txtFormulaName"  style="VISIBILITY:visible;WIDTH:124px;HEIGHT:16px;BACKGROUND-COLOR:white"
							size="15" maxlength="20"/>
                                            </td>
				</tr>
				<tr>
				<td  align="center" valign="top" scope ="colgroup" style="width: 182px">
					<input id="cmdDone" type="button" value="Set Formula" class="button" style="WIDTH:65%"
					 onclick="setData()"/>
				</td>
				
				<td align="center" valign="top" scope ="colgroup">
					<input id="Button1" type="button" value="Update Formula" class="button" style="WIDTH:100%"
					 onclick="UpdateData()"/>
					 </td>
				</tr>
			</table>
    </div>
    </form>
</body>
</html>
