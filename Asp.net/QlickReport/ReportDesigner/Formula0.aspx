<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Formula0.aspx.vb" Inherits="ReportDesigner_Formula0" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
    <script language="javascript">
        function btnMorecolumn_onclick() {
            window.open("getTables.aspx?src=formula", "AddColumns")
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

            document.getElementById("hidFormulacnt").value = window.opener.document.getElementById("hidFormulacnt").value;
            var strTblp = document.getElementById("hidTables").value;
            if (strTblp != "") {
                addColumns(strTblp);
            }
            else {
                alert("No columns found. Please add columns to set formula.")
            }

        }
        function addColumns(cols) {
            // prepare final tables

            var extbl = document.getElementById("hidTables").value;  // existing tables
            var ntbl = cols.split("~"); //newTables
            var j = 0;
            var b = 0;
            for (j = 0; j <= ntbl.length - 1; j++) {
                if (extbl != "") {
                    var sp = extbl.split("~");
                    var k = 0;
                    for (k = 0; k <= sp.length - 1; k++) {
                        if (ntbl[j] == sp[k]) {
                            b = 1;
                        }
                    }
                    if (b == 0) {
                        document.getElementById("hidTables").value = document.getElementById("hidTables").value + "~" + ntbl[j];
                    }
                }
            }
            // ends
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
            var nStr = document.getElementById("cbofdata").value.split(" AS ");
            document.getElementById("txtFormula").value = document.getElementById("txtFormula").value + nStr[0];
        }
        function setFormula(op) {
            document.getElementById("txtFormula").value = document.getElementById("txtFormula").value + op;
        }
        function chkNumeric(txtObj) // to chk the entered value is numeric
        {
            if (isNaN(txtObj.value) == true) {
                txtObj.value = "";
                alert("Enter only numeric value!!");
                txtObj.value = "0";
                txtObj.focus();
            }
        }
        function addNumeric() //to add numeric value to the txtFormula
        {
            document.getElementById("txtFormula").value = document.getElementById("txtFormula").value + "+" + document.getElementById("txtaddNumeric").value;
        }
        function setData() // update parent with formula and close the window
        {
            if (document.getElementById("txtFormulaname").value == "" || document.getElementById("txtFormula").value == "")
                alert("Either Formula Value Or Formula Name is Empty");
            else {
                var b = 0;
                if (document.getElementById("hidFormulacnt").value != "") {
                    var tar = document.getElementById("txtFormulaname").value;
                    var sp = document.getElementById("hidFormulacnt").value;
                    var sp1 = sp.split("~");
                    var t = 0;
                    for (t = 0; t <= sp1.length - 1; t++) {
                        if (sp1[t] == tar)
                            b = 1;
                    }
                }
                if (b == 0) {
                    if (document.getElementById("hidFormulacnt").value == "") {
                        document.getElementById("hidFormulacnt").value = document.getElementById("txtFormulaname").value;
                    }
                    else {
                        document.getElementById("hidFormulacnt").value = document.getElementById("hidFormulacnt").value + "~" + document.getElementById("txtFormulaname").value;
                    }
                    window.opener.document.getElementById("hidFormulacnt").value = document.getElementById("hidFormulacnt").value;
                    window.opener.Formula(document.getElementById("txtFormula").value + " AS " + document.getElementById("txtFormulaname").value + "`" + document.getElementById("hidTables").value);
                    window.parent.focus();
                    window.close();
                }
                else {
                    alert("The Formula Name Already Exists");
                    document.getElementById("txtFormulaname").value = "";
                }
            }
        }
        function closeWin() {
            window.close();
        }
    </script>
</head>
<body onload="return onLoad();">
    <form id="frmFormula" runat="server">
    <div>
    <table id="tblFormula" style="border:0px; width: 414px;" title="Formula" summary="Holds the formula division">
        <caption style ="background-color:#0591D3" >
            Formula</caption>
        <tr>
            <td style="width: 191px; text-align: left; height: 34px; color : Black " valign="bottom" scope ="col">
                <label for="txtFormula" class="label" title="Where">
                    Set Where Condition:</label>
            </td>
            <td style="width: 216px; height: 34px; color : Black " scope ="col">
                <label for="" class="label" title="">Double click to add columns to the formula fields:</label>
            </td>
        </tr>
        <tr>
            <td style="width: 191px; " valign="top" scope="col" >
                <table summary ="table" >					                   
					                    <tr>
					                        <td style="width: 32px; " valign="top" scope="col">
					                            <table title="MAke Formula" summary="Holds the formula">
					                    <tr>
					                        <td title="+" scope="col">
					                            <input id="btnPlus" class="button" type="button" value="+" style="WIDTH:25px; height: 25px;"
							                language="javascript" onclick="setFormula('+')" title="Click to add an operator"/>    
					                        </td>
					                    </tr>
					                    <tr>
					                        <td title="-" scope="col">
					                            <input id="btnMinus" class="button" type="button" value="-" style="WIDTH:25px; height: 25px;"
							                language="javascript" onclick="setFormula('-')" title="Click to add an operator"/>
					                        </td>
					                    </tr>
					                    <tr>
					                        <td title="*" scope="col">
					                             <input id="btnMultiply" class="button" type="button" value="*" style="WIDTH:25px; height: 25px;"
							                language="javascript" onclick="setFormula('*')" title="Click to add an operator"/>
					                        </td>
					                    </tr>
					                    <tr>
					                        <td title="/" scope="col">
					                            <input id="btnDivide" class="button" type="button" value="/" style="WIDTH:25px; height: 25px;"
							                language="javascript" onclick="setFormula('/')" title="Click to add an operator"/>
					                        </td>
					                    </tr>
					                    <tr>
					                        <td title="(" scope="col">
					                             <input id="btnOpen" class="button" type="button" value="(" style="WIDTH:25px; height: 25px;"
							                language="javascript" onclick="setFormula('(')" title="Click to add an operator"/>
					                        </td>
					                    </tr>
					                    <tr>
					                        <td title=")" scope="col">
					                             <input id="btnClose" class="button" type="button" value=")" style="WIDTH:25px; height: 25px;"
							                language="javascript" onclick="setFormula(')')" title="Click to add an operator"/>
					                        </td>
					                    </tr>
					                     <tr>
					                        <td title="=" scope="col">
					                             <input id="btnEqual" class="button" type="button" value="=" style="WIDTH:25px; height: 25px;"
							                language="javascript" onclick="setFormula('=')" title="Click to add an operator"/>
					                        </td>
					                    </tr>
					           
					                </table>
					                        </td>
					                        <td style="width: 142px;" title="Formula" scope="col" valign="top">
					                            <textarea id="txtFormula" style="VISIBILITY:visible;WIDTH:141px;HEIGHT:198px;BACKGROUND-COLOR:white" class="textbox"></textarea>        
					                        </td>
					                    </tr>
					                  
					                
					                 </table>	
            </td>
            <td valign="top" style="height: 231px">
                <table  summary ="table">
                    <tr>
                        <td scope="col">
                            <select id="cbofdata" name="cbofdata" style="BORDER-TOP-STYLE: outset; BORDER-RIGHT-STYLE: outset; BORDER-LEFT-STYLE: outset; LIST-STYLE-TYPE:disc ; BORDER-BOTTOM-STYLE: outset; width: 210px;" language="javascript" ondblclick="return cbofdata_onclick()" size="12"></SELECT>                    
                        </td>
                    </tr>
                </table>
                
                
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <hr style="z-index: 100; left: 13px; position: absolute; top: 304px" />
                <span style="font-size: 8pt; color: #ff3366">Note: To set date use date variables '@Date1@'
                    for start date and '@Date2@' for end date</span></td>
        </tr>
        <tr>
            <td scope="col">
                &nbsp;</td>
            <td>
                <input id="btnMorecolumn" type="button" value="Add More Columns" class="button" language="javascript" style="width: 210px" onclick="return btnMorecolumn_onclick()" title="Click to add more columns"/>        
            </td>
        </tr>
        <tr>
            <td style="width: 124px; color : Black " title="Formula Name" scope="col">
				                            <label title="Formula Name" class="label" for="txtFormulaname">Formula Name:</label>
				                        </td>
				                        <td title="Formula Name" scope="col" style="width: 275px">
                                            &nbsp;<input type="text" id="txtFormulaname" style="VISIBILITY:visible;WIDTH:198px;HEIGHT:16px;BACKGROUND-COLOR:white" maxlength=30 title="Enter formula name" class="textbox"/></td>				                       
        </tr>
        <tr>
            <td colspan="2" title="Set Formula" scope="col">
                <input id="btnFormuladone" type="button" value="Set Formula" class="button" language="javascript" onclick="return setData();" title="Click to save the formula"/>
				                             <input type="button" id="btnCloseFor" onclick="return closeWin();" class="button" value="Close" title="Click to close window" />
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
