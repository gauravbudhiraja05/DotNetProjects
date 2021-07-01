<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ControlID.aspx.vb" Inherits="ReportDesigner_ControlID" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
    <script language="javascript" type="text/javascript">
<!--
        function onLoad() {

            document.getElementById("txtControlid").focus();
            document.getElementById("hidAlllabels").value = window.opener.document.getElementById("hidAlllabels").value;

        }
        // to validate id
        function validate() {

            var id = document.getElementById("txtControlid").value;
            var yu = parseInt((id.charAt(0)));
            var mre = 0;
            if (yu.toString() != "NaN") {
                mre = 1;   // control id starts with numeral
            }
            var o = 0;
            if (mre == 0) {
                for (o = 0; o <= id.length - 1; o++) {
                    if (id.charAt(o) == "." || id.charAt(o) == "/" || id.charAt(o) == "?" || id.charAt(o) == "," || id.charAt(o) == "<" || id.charAt(o) == ">" || id.charAt(o) == ":" || id.charAt(o) == ";" || id.charAt(o) == '"' || id.charAt(o) == "'" || id.charAt(o) == "{" || id.charAt(o) == "}" || id.charAt(o) == "[" || id.charAt(o) == "]" || id.charAt(o) == "+" || id.charAt(o) == "-" || id.charAt(o) == "=" || id.charAt(o) == "*" || id.charAt(o) == "(" || id.charAt(o) == ")" || id.charAt(o) == "&" || id.charAt(o) == "^" || id.charAt(o) == "%" || id.charAt(o) == "$" || id.charAt(o) == "@" || id.charAt(o) == "!") {
                        mre = 2; // special  character encountered
                    }
                }
            }
            return mre;
        }
        //////////
        function btnOk_onclick() {

            if (document.getElementById("txtControlid").value != "") {
                ///// validate control id
                var res = validate();
                if (res == 0) {
                    /////////////////////////////
                    var id = document.getElementById("txtControlid").value;
                    var b = 0;
                    var t = 0;
                    var t1 = 0;
                    for (t = 0; t <= id.length - 1; t++)  // check for blank spaces
                    {
                        if (id.charAt(t) == " ")
                            t1 = 1;
                    }
                    if (t1 == 1) {
                        alert("No blank spaces are allowed");
                        document.getElementById("txtControlid").value = "";
                        document.getElementById("txtControlid").focus();
                    }
                    else {
                        if (document.getElementById("hidAlllabels").value == "") {

                            document.getElementById("hidAlllabels").value = id;
                            b = 1;
                        }
                        else {
                            var h = 0;
                            var lb = document.getElementById("hidAlllabels").value.split("~");
                            var t = 0;
                            for (t = 0; t <= lb.length - 1; t++) {
                                if (lb[t] == id.toLowerCase() || lb[t] == id.toUpperCase()) {
                                    h = 1;
                                }
                            }
                            if (h == 0) {
                                b = 1;
                                document.getElementById("hidAlllabels").value = document.getElementById("hidAlllabels").value + "~" + id


                            }
                        }
                        // ends
                        if (b == 1) {
                            if (document.getElementById("hidSrc").value == "") {
                                //created by lalit  //// set created element id in the hidden field for match existing control id

                                window.opener.document.getElementById("hidAlllabels").value = document.getElementById("hidAlllabels").value
                                ///////////
                                window.opener.Label(document.getElementById("txtControlid").value);
                                window.parent.focus();
                                window.close();
                            }
                            else {
                                //created by lalit  //// set created element id in the hidden field for match existing control id
                                window.opener.document.getElementById("hidAlllabels").value = document.getElementById("hidAlllabels").value
                                ///
                                window.opener.createInstance(document.getElementById("txtControlid").value + "`" + document.getElementById("hidSrc").value);
                                window.parent.focus();
                                window.close();
                            }
                        }
                        else {
                            alert("This control ID already exists.Supply another.");

                        }
                    }
                }
                else {
                    if (res == 1) {
                        alert("ControlID Can Only Start With An Alphabet.");
                    }
                    else {
                        alert("ControlID Cannot Contain Any Special Character Other Than Underscore(_)");
                    }
                }
            }
            else {
                alert("Please supply an unique ID");
            }
            document.getElementById("txtControlid").value = "";
            document.getElementById("txtControlid").focus();

        }

        function btnCancel_onclick() {
            window.parent.focus();
            window.close();
        }

// -->
</script>
</head>
<body onload="return onLoad();">
    <form id="frmControlid"  runat="server">
     <div id="divControlid">
    <table style="width: 396px">
        <caption style ="background-color:#0591D3">Provide Control ID</caption>
        <tr>
            <td colspan="2" scope="colgroup" >
                <span style="font-size: 8pt; color: #ff3333">
                Use alphanumeric characters and underscore(_).
                    Do not use blank spaces or special character.</span>
            </td>
        </tr>
        <tr>
            <td style="width: 131px; height: 27px; color :Black " scope ="col" >
                <label title="Control ID"  class="label" for="txtControlid">
                    Control ID:</label>
            </td>
            <td style="height: 27px; width: 262px;" scope ="col">
                <input type="text" id="txtControlid" title="Enter Control ID" class="textbox" style="width: 234px" />
            </td>
        </tr>
        <tr>
            <td scope ="col" >
            </td>
            <td style="width: 262px; height: 27px;" scope ="col">
                <input type="button" id="btnOk" class="button" value="Ok" language="javascript" onclick="return btnOk_onclick()" title="Ok"  /><input type="button" id="btnCancel" class="button" value="Cancel" language="javascript" onclick="return btnCancel_onclick()" title="Cancel" />
            </td>            
        </tr>
    </table>
 </div>
        <input id="hidSrc" name="hidSrc" runat="server" type="hidden" /><%----source of the ID---%>
        <input id="hidAlllabels" name="hidAlllabels" type="hidden" /><%----source of the ID---%>
        <input id="lalitlable" name="hiddenlable" type="hidden" />
    </form>
</body>
</html>
