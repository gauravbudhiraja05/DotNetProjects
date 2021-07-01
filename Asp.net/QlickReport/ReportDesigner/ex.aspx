<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ex.aspx.vb" Inherits="ReportDesigner_ex" title="Untitled Page" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Where</title>
    <link href="../App_Themes/Themes/StyleSheet.css" rel="stylesheet" />
<script type="text/javascript">
<!--

function insertText() {
if (document.selection) {
document.getElementById("TextBox1").focus();
sel = document.selection.createRange();
sel.text = document.getElementById("DropDownList1").options[document.getElementById("DropDownList1").selectedIndex].text;
}
//MOZILLA/NETSCAPE support
else if (document.getElementById("TextBox1").selectionStart || document.getElementById("TextBox1").selectionStart == '0') {
var startPos = TextBox1.selectionStart;
var endPos = document.getElementById("TextBox1").selectionEnd;
document.getElementById("TextBox1").value = document.getElementById("TextBox1").value.substring(0, startPos) + document.getElementById("DropDownList1")[document.getElementById("DropDownList1").selectedIndex] + document.getElementById("TextBox1").value.substring(endPos, document.getElementById("TextBox1document.getElementById").value.length);
} 
else 
{
document.getElementById("TextBox1").value += document.getElementById("DropDownList1")[document.getElementById("DropDownList1").selectedIndex];
}
}
// -->
</script>
</head>

<body>
<form runat="server" id="frm">
<textarea name="TextBox1" rows="2" cols="20" id="TextBox1" style="height:192px;width:352px;"></textarea><br />

        <br />
        <select name="DropDownList1" onchange="javascript:insertText()" id="DropDownList1" style="width:192px;">
	<option value="January">January</option>
	<option value="February">February</option>
	<option selected="selected" value="March">March</option>
	<option value="April">April</option>
	<option value="May">May</option>
	<option value="June">June</option>
	<option value="July">July</option>

</select>
</form>

</body>
</html>