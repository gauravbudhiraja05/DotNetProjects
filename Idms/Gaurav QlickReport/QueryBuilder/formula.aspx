<%@ Page Language="VB" AutoEventWireup="false" CodeFile="formula.aspx.vb" Inherits="QueryBuilder_formula" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
		<title>Formula</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<link href="../App_Themes/Themes/StyleSheet.css"type="text/css" rel="stylesheet"/>
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
				
				if (blank(document.Form1.txtFormula.value))
				{
					document.Form1.txtFormula.value =  document.Form1.cbofdata.value 
				}
				else 
				{
//					document.Form1.txtFormula.value = document.Form1.txtFormula.value  + "$" + document.Form1.cbofdata.value 
document.Form1.txtFormula.value = document.Form1.txtFormula.value   + document.Form1.cbofdata.value 
				}

			}
			function chkNumeric()
			{
				
				if (isNaN(document.Form1.txtaddformula.value) == true)
				{
					document.Form1.txtaddformula.value = "";
					alert("Enter only numeric value is allowed in the textbox.");
					document.Form1.txtaddformula.focus();
				}
			}
			function setFormula(a)
			{
				
				if (!blank(a))
				{
					if (blank(document.Form1.txtFormula.value))
					{
						document.Form1.txtFormula.value = document.Form1.txtFormula.value +  a
					}
					else 
					{
					//changed
//						document.Form1.txtFormula.value = document.Form1.txtFormula.value + "$" + a
						document.Form1.txtFormula.value = document.Form1.txtFormula.value  + a
					}
				}
			}
			function setData()
			{
				
//				if (document.Form1.chkPercentage.checked) 
//				{
//					window.opener.document.getElementById("chkPercentage").value = "Yes"
//				}
//				else
//				{
//					window.opener.document.getElementById("chkPercentage").value = ""
//				}
//				alert(document.Form1.txtFormulaName.value);
//				window.opener.document.getElementById("hidFormulaName").value=document.Form1.txtFormulaName.value;
				var tbl=""
				var id ="";
				id=document.Form1.txtFormula.value; 
				if(id=="")
				{
				alert("No formula is present");
				}
				else
				{
//				+" as "+ document.Form1.txtFormulaName.value;
				tbl="<input type='button' value='"+id+"' id='"+id+"' title='Right click to remove the formula' onclick=FormulaToData(); oncontextmenu=delformula(); />"
//				window.opener.document.getElementById("txtaFormula").value = document.Form1.txtFormula.value;
                    if (window.opener.document.getElementById("formulaIds").value=="")
                    {
                    window.opener.document.getElementById("formulaIds").value=id;
                    }
                    else
                     {
                    window.opener.document.getElementById("formulaIds").value =  window.opener.document.getElementById("formulaIds").value +","+ id;
                    }
                     window.opener.document.getElementById("FormulaClicked").value=id; 

				window.opener.document.getElementById("txtaFormula").innerHTML=window.opener.document.getElementById("txtaFormula").innerHTML+tbl;
				window.close()
				}
			}
			function onload()
			{
		
			
//				if (!blank(window.opener.document.getElementById("txtaFormula").value))
//				{

//					document.Form1.txtFormula.value=window.opener.document.getElementById("txtaFormula").value;
//				}
//				if (!blank(window.opener.document.getElementById("hidFormulaName").value))
//				{
//					document.Form1.txtFormulaName.value=window.opener.document.getElementById("hidFormulaName").value;
//				}
//				
//				
				
				
				
				
				
//				if(document.getElementById("cbofdata").option[0].value!="")
//				document.getElementById("cbofdata").removeAttribute[0];
			}
			//-->
		</script>
	</head>
	<body onload="onload()">
		<form id="Form1" method="post" runat="server">
			<table border="0" align="center" summary="">
				<tr>
					<td valign="top" style="height:20px" scope="col">
						<input id="cmdPlus" class="button" type="button" value="+" style=" visibility:hidden;WIDTH:18px;HEIGHT:18px;" onclick="setFormula('+')" disabled="disabled"/> 
						<input id="cmdMinus" class="button" type="button" value="-" style="VISIBILITY:hidden;WIDTH:18px;HEIGHT:18px;" onclick="setFormula('-')" disabled="disabled"/> 
						<input id="cmdMultiply" class="button" type="button" value="*" style="VISIBILITY:hidden;WIDTH:18px;HEIGHT:18px;" onclick="setFormula('*')" disabled="disabled"/> 
						<input id="cmdDivide" class="button" type="button" value="/" style="VISIBILITY:hidden;WIDTH:18px;HEIGHT:18px;" onclick="setFormula('/')" disabled="disabled"/>
						<input id="cmdOpen" class="button" type="button" value="(" style="VISIBILITY:hidden;WIDTH:18px;HEIGHT:18px;" onclick="setFormula('(')" disabled="disabled"/> 
						<input id="cmdClose" class="button" type="button" value=")" style="VISIBILITY:hidden;WIDTH:18px;HEIGHT:18px;" onclick="setFormula(')')" disabled="disabled"/>
						<br/>
						<br/>
<label for="txtFormula"></label>

						<textarea id="txtFormula" cols="*,*" rows="*,*"  class="textBox" style="VISIBILITY:visible;WIDTH:130px;HEIGHT:188px;"></textarea>
					</td>
					<td valign="top" scope="col"><label for="cbofdata"></label><select size="14" id="cbofdata" name="cbofdata" onclick="putdata();"  style="BORDER-TOP-STYLE: outset; BORDER-RIGHT-STYLE: outset; BORDER-LEFT-STYLE: outset; LIST-STYLE-TYPE: disc; BORDER-BOTTOM-STYLE: outset; width: 125px;">'<%=datatablestring%>'
					                 
					                </select></td>
				</tr>
				<tr>
					<td colspan="2" scope="colgroup"><label for="txtaddformula"></label><input maxlength="20" type="text" id="txtaddformula" class="textBox" style="VISIBILITY:hidden;WIDTH:124px;HEIGHT:16px;" size="15" onblur="chkNumeric();" disabled="disabled"/> <input type="button" class="button" style="VISIBILITY:hidden;WIDTH:32px;HEIGHT:18px;" value="Add" onclick="setFormula(document.Form1.txtaddformula.value)" disabled="disabled"/></td>
				</tr>
				<tr id="tr1" runat="server" visible="false">
					<td width="100%"  colspan="2"  align="center" valign="top" scope="col"><label for="checkbox"></label><input type="checkbox" id="chkPercentage" visible="false" name="chkPercentage" value="Yes" />Show % sign</td>
				</tr>
				<tr id="tr2"  runat="server"  visible="false">
					<td colspan="2" scope="colgroup" > <label for="txtFormulaName">Formula Name</label><input type="text" id="txtFormulaName" class="textBox" style="VISIBILITY:hidden;WIDTH:124px;HEIGHT:16px;" size="15" maxlength="20" /></td>
				</tr>
				<tr>
					<td width="100%" colspan="2" align="center" valign="top" scope="col"><input id="cmdDone" type="button" value="Set Formula" class="button" style="WIDTH:100%" onclick="setData();"/></td>
				</tr>
			</table>
		</form>
	</body>
</html>