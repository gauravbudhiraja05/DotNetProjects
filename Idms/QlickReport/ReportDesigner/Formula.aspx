<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Formula.aspx.vb" Inherits="ReportDesigner_Formula" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
    <script language="javascript" type="text/javascript">
        function btnMorecolumn_onclick() {
            window.open("getTables.aspx?src=formula", "AddColumns", "width=450,height=600,scrollbars=yes,status=yes");
        }
        function onLoad() {
            //Load existing formulas

            var exFor = window.opener.document.getElementById("hidDformula").value;
            if (exFor != "") {
                var exFor1 = exFor.split("~");
                var i = 0;

                var theSel = document.getElementById("cbofdata");
                var cnt = theSel.length;
                for (i = 0; i <= exFor1.length - 1; i++) {
                    var nStr = exFor1[i].replace("$", ".");
                    var newOpt1 = new Option(nStr, nStr);
                    theSel.options[cnt] = newOpt1;
                    cnt++;
                }
            }
            //

            document.getElementById("hidFormulacnt").value = window.opener.document.getElementById("hidDformula").value;
            var strTblp = document.getElementById("hidTables").value;
            if (strTblp != "") {
                addColumns(strTblp);
            }
            else {
                alert("No columns found. Please add columns to set formula.")
            }

        }
        function addColumns(cols) {

            var tb = cols.split("~");
            var i = 0;
            for (i = 0; i <= tb.length - 1; i++) {
                ReportDesignerAjax.GetTableFields(tb[i], pasteField);
            }
        }
        function pasteField(Response)       // Get columns
        {
            if (Response != "") {
                var res = Response.value;
                var resNew = res.split("~"); // Split all the columns
                var res2 = resNew[0].split("$"); // Split to get the table name
                var i = 0;
                var theSel = document.getElementById("cbofdata");
                var cnt = theSel.length;
                for (i = 0; i <= resNew.length - 1; i++) {
                    var nStr = resNew[i].replace("$", ".");
                    var newOpt1 = new Option(nStr, nStr);
                    theSel.options[cnt] = newOpt1;
                    cnt++;
                }

            }
        }
        function cbofdata_onclick() {
            var i = document.getElementById("cbofdata").selectedIndex;
            if (document.selection) {
                document.getElementById("txtFormula").focus();
                sel = document.selection.createRange();
                var d = document.getElementById("cbofdata").options[i].text;
                var d1 = d.split(" AS ");
                if (d1.length > 1) {
                    sel.text = d1[1];
                }
                else {
                    sel.text = d1[0];
                    // sel.text = " "+document.getElementById("selCol").options[i].text+" ";
                }
            }
        }
        function setFormula(op) {
            //document.getElementById("txtWhere").value=document.getElementById("txtWhere").value+ op ;
            if (document.selection) {
                document.getElementById("txtFormula").focus();
                sel = document.selection.createRange();
                sel.text = op;
            }
        }
        function setData() // update parent with formula and close the window
        {
            document.getElementById("txtFormula").value = "(" + document.getElementById("txtFormula").value + ")";
            var formu = document.getElementById("txtFormula").value + " AS [" + document.getElementById("txtFormulaname").value + "]";

            if (document.getElementById("txtFormulaname").value == "" || document.getElementById("txtFormula").value == "") {
                alert("Either Formula Value Or Formula Name is Empty");
            }
            else {
                var b = 0;
                if (document.getElementById("hidFormulacnt").value != "") {
                    var tar = document.getElementById("txtFormulaname").value;
                    var sp = document.getElementById("hidFormulacnt").value;
                    var sp1 = sp.split("~");
                    var t = 0;
                    for (t = 0; t <= sp1.length - 1; t++) {
                        var ty = sp1[t].split(" AS ");
                        if (ty[1] == tar)
                            b = 1;
                    }
                }
                if (b == 0) {
                    if (document.getElementById("hidFormulacnt").value == "") {
                        document.getElementById("hidFormulacnt").value = formu;
                    }
                    else {
                        document.getElementById("hidFormulacnt").value = document.getElementById("hidFormulacnt").value + "~" + formu;
                    }
                    //window.opener.document.getElementById("hidDformula").value=document.getElementById("hidFormulacnt").value;
                    //window.opener.Formula(formu+"`"+document.getElementById("hidTables").value);
                    ReportDesignerAjax.swapFormula(document.getElementById("txtFormula").value, document.getElementById("hidFormulacnt").value, finalFormula);


                }
                else {
                    alert("The Formula Name Already Exists");
                    document.getElementById("txtFormulaname").value = "";
                }
            }
        }
        function finalFormula(response) {
            // debugger;
            window.opener.Formula(response.value + " AS [" + document.getElementById("txtFormulaname").value + "]`" + document.getElementById("hidTables").value);
            window.parent.focus();
            window.close();
        }
        function closeWin() {
            window.parent.focus();
            window.close();
        }
    </script>
</head>
<body onload="onLoad();" style="background-color: aliceblue">
    <form id="form1" runat="server">
    <div>
        <table style="width: 637px" >
            <caption style ="background-color:#0591D3">
            Formula </caption>
            <tr>
                <td colspan="2" style="height: 208px" scope ="colgroup" >
                <label  for="txtFormula" ></label>
                    <textarea rows="*,*" cols="*,*"  id="txtFormula" class="textBox" style="visibility: visible; width: 622px;height: 198px; background-color: white"></textarea>
                </td>
            </tr>
            <tr>
                <td style="height: 20px" scope ="col" >
                   <label class="label" for="btnMax" style ="color:black">Select Column</label> 
                </td>
                <td style="height: 22px" scope ="col">
                    <input id="btnMax" class="button" onclick="setFormula('MAX(')" style="width: 50px" title="Click to add an operator" type="button" value="MAX" />
                    <input id="btnMin" class="button" onclick="setFormula('MIN(')" style="width: 50px" title="Click to add an operator" type="button" value="MIN" />
                    <input id="btnCount" class="button" onclick="setFormula('COUNT(')" style="width: 50px" title="Click to add an operator" type="button" value="COUNT" />
                    <input id="btnSum" class="button"  onclick="setFormula('SUM(')"  style="width: 50px" title="Click to add an operator" type="button" value="SUM" />
                    <input id="btnAvg" class="button"  onclick="setFormula('AVG(')" style="width: 50px" title="Click to add an operator" type="button" value="AVG" /></td>
            </tr>
            <tr>
                <td style="width: 208px" scope ="col">
                <label class="label" for="cbofdata" ></label> 
                    <select id="cbofdata" name="cbofdata" style="BORDER-TOP-STYLE: outset; BORDER-RIGHT-STYLE: outset; BORDER-LEFT-STYLE: outset; LIST-STYLE-TYPE:disc ; BORDER-BOTTOM-STYLE: outset; width: 300px;" onclick="return cbofdata_onclick()" size="12"></select>                    
                </td>
                <td style="width: 461px" valign="top" scope ="col">
                    <input id="btnPlus" class="button" onclick="setFormula('+')" style="width: 50px" title="Click to add an operator" type="button" value="+" />
                    <input id="btnMinus" class="button" onclick="setFormula('-')" style="width: 50px" title="Click to add an operator" type="button" value="-" />
                    <input id="btnMultiply" class="button" onclick="setFormula('*')" style="width: 50px" title="Click to add an operator" type="button" value="*" />
                    <input id="btnDivide" class="button"  onclick="setFormula('/')"  style="width: 50px" title="Click to add an operator" type="button" value="/" />
                    <input id="btnOpen" class="button"  onclick="setFormula('(')" style="width: 50px" title="Click to add an operator" type="button" value="(" />
                    <input id="btnClose" class="button" onclick="setFormula(')')" style="width: 50px" title="Click to add an operator"  type="button" value=")" />
                    <br />
                    <br />
                    <label title="Formula Name" class="label" for="txtFormulaname">Formula Name:</label>
                    <input id="txtFormulaname" class="textBox" maxlength="30" style="width: 198px; height: 16px;" title="Enter formula name" type="text" /><span style="font-size: 11pt; color: #ff3333">*</span>
                    <br />
                    <br />
                    <input id="btnMorecolumn" class="button"  onclick="return btnMorecolumn_onclick()" style="width: 320px; font-weight: bold; height: 24px;" title="Click to add more columns" type="button" value="ADD MORE COLUMNS" /><br />
                    <input id="btnFormuladone" class="button"  onclick="return setData();" style="width: 320px; font-weight: bold; text-transform: uppercase; height: 25px;" title="Click to save the formula" type="button" value="Set Formula" /><br />
                    <input id="btnCloseFor" class="button" onclick="return closeWin();" style="width: 320px; font-weight: bold; text-transform: uppercase; height: 23px;" title="Click to close window" type="button" value="Close" /></td>
            </tr>
            <tr>
                <td colspan="2" scope ="colgroup">
                    <span style="font-size: 10pt; color: #ff3366">NOTE: Double Click to add desired columns to the textarea. </span>                
                </td>
            </tr>
        </table>
    </div>
     <%-- hidden field declaration --%>
    <input type="hidden" runat="server" name="hidTables" id="hidTables" /> <%-- hidden tables --%>
    <input type="hidden" id="hidFormulacnt" name="hidFormulacnt"/><%-- hidden formula name --%>
    <%-- hidden field declaration ends --%>
    </form>
</body>
</html>
