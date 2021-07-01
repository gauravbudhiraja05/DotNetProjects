<%@ Page Title="Report Designer" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="Welcome.aspx.vb" Inherits="ReportDesigner_Welcome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">

    <script type="text/javascript" src="../js/VwdCmsSplitterBar.js"></script>

    <%--To set height of the divisions at runtime--%>

    <script language="javascript" src="../js/202pop.js" type="text/javascript"></script>

    <%--To allow the user to chose among a wide range of colors--%>

    <script language="JavaScript" type="text/javascript" src="../js/picker.js"></script>

    <%--To allow the user to chose among a wide range of colors--%>

    <script language="Javascript" type="text/javascript" src="../js/collapseableDIV.js"></script>

    <%--To collapse and expand divisions--%>

    <script language="Javascript" type="text/javascript" src="../js/drag.js"></script>

    <%--To enable object dragging--%>

    <script language="javascript" type="text/javascript">


        //object name to delete
        var toDel = "";
        // To poopup calendar
        function ShowCalendar(obj) {

            document.getElementById(obj).value = window.showModalDialog('../Calendar.htm', document.getElementById(obj).value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
            if (document.getElementById(obj).value == "undefined")
                document.getElementById(obj).value = "";
        }

        // calendar ends

        // This function is used to an existing report
        function openReportdesign() {


            document.getElementById("lblCaption").innerHTML = " Report Name : " + document.getElementById("hidReportname").value;
            document.getElementById("hidSubtotal").value = "";
            document.getElementById("hidDformatid").value = "";
            document.getElementById("hidDcolcon").value = "";
            if (document.getElementById("hidReportmode").value == "Edit") // remove existing tables
            {
                document.getElementById("divTables").innerHTML = "";

            }
            if (document.getElementById("hidReporttype").value == "Summarized") // check summarized true
            {
                document.getElementById("chkSummarized").checked = true;

            }
            if (document.getElementById("hidTables").value != "") {
                setTable(document.getElementById("hidTables").value); // open tables    
                var t = 0;
                var tb = document.getElementById("hidTables").value.split("~");
                for (t = 0; t <= tb.length - 1; t++)  // open table fields
                {
                    get_field(tb[t]);
                }
            }
            // assign height to the header & footer
            if (document.getElementById("hidHeight").value != "") {
                var hgt = document.getElementById("hidHeight").value.split(",");
                document.getElementById("divHeader").style.height = hgt[0];
                document.getElementById("divFooter").style.height = hgt[1];
            }
            // apply format to header
            if (document.getElementById("hidHeaderformat").value != "") {
                var hFormat = document.getElementById("hidHeaderformat").value;
                applyFormat(hFormat, "divHeader");
            }
            else {
                remove_final("Header");
            }
            //apply format to details
            if (document.getElementById("hidDetailsformat").value) {
                var dFormat = document.getElementById("hidDetailsformat").value;
                applyFormat(dFormat, "divDetails");
                ////////// reload format of report
                rldFormat(dFormat);
            }
            else {
                remove_final("Details Pane");
            }
            // apply format to footer
            if (document.getElementById("hidFooterformat").value != "") {
                var fFormat = document.getElementById("hidFooterformat").value;
                applyFormat(fFormat, "divFooter");
            }
            else {
                remove_final("Footer");
            }
            //generate header coumns
            genHeader();

            // generate details columns
            genDetails();

            //generate footer columns
            genFooter();

            // hilight details pane
            //btnDetails_onclick();
            if (document.getElementById("hidColorcondition").value != "") {
                collectColcon(); // to collect color condition on details element
            }

        }

        ////////
        function rldFormat(fmt) {

            var val = fmt.split(";");
            var i = 0;
            for (i = 0; i <= val.length - 1; i++) {
                var nxt = val[i].split(":");
                if (nxt[0] == "font-size") {
                    var hj = nxt[1].replace("pt", "");
                    document.getElementById("<%=ddlFontsize.ClientID%>").value = hj;
                }
                else if (nxt[0] == "font-family")
                    document.getElementById("<%=ddlFontfamily.ClientID%>").value = nxt[1];
                else if (nxt[0] == "color") {
                    document.getElementById("Sample_1").style.backgroundColor = nxt[1];
                    document.getElementById("hidForecolor").value = nxt[1];
                }
                else if (nxt[0] == "background-color") {
                    document.getElementById("Sample_2").style.backgroundColor = nxt[1];
                    document.getElementById("hidBackcolor").value = nxt[1];
                }
                else if (nxt[0] == "font-weight") {
                    if (nxt[1] == "bold") {
                        document.getElementById("chkBold").checked = true;
                    }
                    else {
                        document.getElementById("chkBold").checked = false;
                    }
                }
                else if (nxt[0] == "text-decoration") {
                    if (nxt[1] == "underline")
                        document.getElementById("chkUnderline").checked = true;
                    else
                        document.getElementById("chkUnderline").checked = false;
                }
                else if (nxt[0] == "font-style") {
                    if (nxt[1] == "italic")
                        document.getElementById("chkItalic").checked = true;
                    else
                        document.getElementById("chkItalic").checked = false;
                }
                document.getElementById("ddlConstituent").value = "Details Pane";

            }
        }
        //////////////
        // 
        // To generate header elements of an opned report
        function genHeader() {


            // generate header elements
            var div = document.getElementById("divHeader");
            div.innerHTML = ""; //Remove all elements
            if (document.getElementById("hidHpos").value != "") {


                var ele = document.getElementById("hidHpos").value.split("~");
                var i = 0;
                for (i = 0; i <= ele.length - 1; i++) {
                    var s1 = ele[i].split("@#@");
                    var s2 = s1[1].split("#@#"); // priorily the split was made out of comma(,)
                    var s22 = s2[1].split(",");
                    var vis = s22[2];
                    if (vis == "") {
                        vis = '';
                    } else {
                        vis = 'hidden';
                    }


                    // s1[0] contains id, s[0] contailns value, s[1] contains top, s[2] contains left
                    if (vis != "hidden") {
                        var tb = '<input class="drag" style="height:16px;width:180px;font-size:8pt;font-family:Verdana;visibility:' + vis + ';position:relative;top:' + s22[0] + 'px;left:' + s22[1] + 'px;border:solid 1px gray"   type="text" title="Right click to delete"  oncontextmenu="return menu(' + s1[0] + ');" ondblclick="return setData(' + s1[0] + ');"  value="' + s2[0] + '" id="' + s1[0] + '" />';
                        div.innerHTML = div.innerHTML + tb;

                    }
                }
                // apply format on the header elements
                if (document.getElementById("hidHformat").value != "") {
                    var obs = document.getElementById("hidHformat").value.split("~");
                    var t = 0;
                    for (t = 0; t <= obs.length - 1; t++) {
                        var obsR = obs[t].split(">");
                        applyFormat(obsR[1], obsR[0]);  // applyFormat(formatValue,object)
                    }
                }
            }
        }
        //
        // To generate details elements of an opned report
        function genDetails() {
            // generate details pane elements

            var div = document.getElementById("divDetails");
            div.innerHTML = ""; //Remove all elements
            if (document.getElementById("hidDpos").value != "") {
                var ele = document.getElementById("hidDpos").value.split("~");
                var i = 0;
                var tb = "";
                for (i = 0; i <= ele.length - 1; i++) {
                    var btnCnt = parseInt(document.getElementById("btnCount").value);
                    btnCnt = btnCnt + 1;
                    document.getElementById("btnCount").value = btnCnt.toString();

                    id = "btn" + btnCnt;
                    var col = ele[i].replace("$", "."); // to get tablename.columnname replace tablename$columnname



                    if (div.innerHTML == "") {
                        tb = '<input class="drag" type="button" title="Right click to delete" style=" border:solid 1px grey; color:#000000;background-color:#ffffff;font-size:8pt;font-family:Verdana;" oncontextmenu="return menu(' + id + ');" ondblclick="return setData(' + id + ');"  value="' + col + '" id="' + id + '" />';
                    }
                    else {
                        var tb = '<br id="br_' + id + '"/><input class="drag" type="button" title="Right click to delete" style=" border:solid 1px grey; color:#000000;background-color:#ffffff;font-size:8pt;font-family:Verdana;" oncontextmenu="return menu(' + id + ');" ondblclick="return setData(' + id + ');"  value="' + col + '" id="' + id + '" />';
                    }
                    div.innerHTML = div.innerHTML + tb;

                }
                // apply format on the details elements
                if (document.getElementById("hidDformat").value != "") {
                    var str = document.getElementById("divDetails");
                    var dInp = str.getElementsByTagName("input");
                    var obs = document.getElementById("hidDformat").value.split("~");
                    var t = 0;
                    var j = 0;
                    var vbtn = document.getElementById("btnEx2").value;
                    vbtn = vbtn.replace("a", "");
                    for (t = 0; t <= obs.length - 1; t++) {
                        var obsR = obs[t].split(">");   // we got value of the btn
                        for (j = 0; j <= dInp.length - 1; j++) {
                            ///////// fetch formula name
                            var toVal = dInp[j].value;
                            toVal = toVal.replace("[", "");
                            toVal = toVal.replace("]", "");

                            /////////////////////


                            if (toVal == obsR[0]) {
                                applyFormat(obsR[1], dInp[j].id);   // applyFormat(formatValue,object)
                                // fill hidDformatid with ids of the formatted ids
                                if (document.getElementById("hidDformatid").value == "") {
                                    document.getElementById("hidDformatid").value = dInp[j].id;
                                }
                                else {
                                    document.getElementById("hidDformatid").value = document.getElementById("hidDformatid").value + "~" + dInp[j].id;
                                }
                            }
                        }

                    }
                    document.getElementById("hidDformat").value = "";
                }

            }
        }

        // To collect color condition of details elements
        function collectColcon() {
            var colcon = document.getElementById("hidColorcondition").value;
            var colcon1 = colcon.split("~");
            var y = 0;
            var str = document.getElementById("divDetails");
            var oInputs = str.getElementsByTagName("input");
            var fnlcon = "";
            for (y = 0; y <= colcon1.length - 1; y++) {
                var rt = colcon1[y].split("@#@");

                if (oInputs.length != 0) {
                    var yu = 0;
                    for (yu = 0; yu <= oInputs.length - 1; yu++) {
                        var el = oInputs[yu].value;
                        el = el.replace("[", "");
                        el = el.replace("]", "");
                        if (el == rt[0]) {
                            if (fnlcon == "") {
                                fnlcon = oInputs[yu].id + "@#@" + rt[1];
                            }
                            else {
                                fnlcon = fnlcon + "~" + oInputs[yu].id + "@#@" + rt[1];
                            }
                        }
                    }
                }
            }
            document.getElementById("hidDcolcon").value = fnlcon;
        }

        ////////////////////////

        //
        // To generate footer elements of an opned report
        function genFooter() {
            // generate footer elements
            var div = document.getElementById("divFooter");
            div.innerHTML = ""; //Remove all elements
            if (document.getElementById("hidFpos").value != "") {
                var ele = document.getElementById("hidFpos").value.split("~");
                var i = 0;
                for (i = 0; i <= ele.length - 1; i++) {
                    var s1 = ele[i].split("@#@");
                    var s2 = s1[1].split("#@#");
                    var s22 = s2[1].split(",");
                    var vis = s22[2];
                    if (vis == "") {
                        vis = '';
                    } else {
                        vis = 'hidden';
                    }
                    if (vis != "hidden") {
                        var tb = '<input class="drag" style="height:16px;width:180px;font-size:8pt;visibility:' + vis + ';font-family:Verdana;position:relative;top:' + s22[0] + 'px;left:' + s22[1] + 'px;" type="text" title="Right click to delete" oncontextmenu="return menu(' + s1[0] + ');" ondblclick="return setData(' + s1[0] + ');"  value="' + s2[0] + '" id="' + s1[0] + '" />';
                        div.innerHTML = div.innerHTML + tb;

                    }

                }
                // apply format on the footer elements
                if (document.getElementById("hidFformat").value != "") {
                    var obs = document.getElementById("hidFformat").value.split("~");
                    var t = 0;
                    for (t = 0; t <= obs.length - 1; t++) {
                        var obsR = obs[t].split(">");
                        applyFormat(obsR[1], obsR[0]);  // applyFormat(formatValue,object)
                    }
                }
            }
        }
        //


        //////// Ajax Methods to bind table and tablefields//////

        function setTable(tblVal) {


            //var val=document.getElementById("hidTables").value;
            // document.getElementById("hidTables").value=tblVal;
            //prepare final table
            if (document.getElementById("hidReportmode").value == "new") {
                if (document.getElementById("hidTables").value == "") {
                    document.getElementById("hidTables").value = tblVal;
                }
                else {
                    var extbl = document.getElementById("hidTables").value;  // existing tables
                    var ntbl = tblVal.split("~"); //newTables
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
                }
            }
            ///ends
            var st = tblVal.split("~");
            for (i = 0; i <= st.length - 1; i++) {
                table(st[i]);
            }
        }
        function table(name) {

            var newdiv = document.getElementById("divTables");
            var t = name.split("$");
            var tname = name;
            var b = true;
            var tb = "";
            var divchk = newdiv.innerHTML;
            var chk = divchk.search(tname);
            if (chk != -1) {
                b = false;
            }
            if (b == true) {
                tb = '<div id="div' + tname + '" style="background-color:#42969f;" ><input type="button" title="Click to expand this table & right click to delete" class="buttonTable" oncontextmenu="return menu1(' + tname + ');"  value="' + tname + '" id="' + tname + '" onclick="get_field(' + tname + ');" /></div>';
                newdiv.innerHTML = newdiv.innerHTML + tb;
            }
            else {
                alert(tname + " table already selected");
            }
        }
        function get_field(tblname) {

            var val = ""
            if (document.getElementById("hidReportmode").value == "Edit") {
                val = tblname;
            }
            else {
                val = tblname.value;
            }
            //tblname.disabled=true;    
            var newdiv = document.getElementById('div' + val);
            var inp = newdiv.getElementsByTagName("input");
            var t = 0;
            var l = inp.length;
            var c = "";
            if (l > 1) {
                //Remove div and add it again
                var newdiv1 = document.getElementById("divTables");
                var te = document.getElementById("div" + val);
                newdiv1.removeChild(te);
                var tb = '<div id="div' + val + '" style="background-color:#42969f;" ><input type="button" title="Click to expand this table & right click to delete"  oncontextmenu="return menu1(' + val + ');" class="buttonTable"  value="' + val + '" id="' + val + '" onclick="get_field(' + val + ');" /></div>';
                newdiv1.innerHTML = newdiv1.innerHTML + tb;

            }
            else {
                ReportDesignerAjax.GetTableFields(val, pasteField);
            }
        }
        function pasteField(Response) {
            if (Response != "") {
                var res = Response.value;
                if (res == "Null") {
                    alert("No columnname found.");
                }
                else {

                    var resNew = res.split("~"); // Split all the columns 
                    var res2 = resNew[0].split("$"); // Split to get the column name       
                    var dname = 'div' + res2[0]; // Search for the existing division of the table
                    var newdiv = document.getElementById(dname);
                    var i = 0;
                    for (i = 0; i <= resNew.length - 1; i++) {
                        var col = resNew[i].split("$"); // Split to get the column name
                        var tb = "";
                        tb = '<br id="br_' + resNew[i] + '"/><input type="button" title="' + resNew[i] + '" class="buttonField" style="height:20px;" onclick="return instance(' + resNew[i] + ')"   id="' + resNew[i] + '" value="' + col[1] + '"/>';
                        newdiv.innerHTML = newdiv.innerHTML + tb;
                    }
                }
            }
        }
        function instance(field) {
            var id = "";
            var typ = document.getElementById("divType").value;
            var dd = "";
            //    if(typ=="header")
            //    {
            //    dd="divHeader";
            //    }
            //    else if(typ=="footer")
            //    {
            //        dd="divFooter";
            //        genInstanceID(field)
            //    }    
            if (typ == "details") {
                dd = "divDetails"

                var fname = field.id;
                var btnCnt = parseInt(document.getElementById("btnCount").value);
                btnCnt = btnCnt + 1;
                document.getElementById("btnCount").value = btnCnt.toString();
                id = "btn" + btnCnt;
                var col = fname.replace("$", "."); // to get tablename.columnname replace tablename$columnname
                var div = document.getElementById(dd);
                if (div.innerHTML == "") {
                    var tb = '<input class="drag"  type="button" title="Right click to delete" style=" border:solid 1px grey; color:#000000;background-color:#ffffff;font-size:8pt;font-family:Verdana;" oncontextmenu="return menu(' + id + ');" ondblclick="return setData(' + id + ');"  value="' + col + '" id="' + id + '" />';
                }
                else {
                    var tb = '<br id="br_' + id + '"/><input class="drag"  type="button" title="Right click to delete" style="border:solid 1px grey; color:#000000;background-color:#ffffff;font-size:8pt;font-family:Verdana;" oncontextmenu="return menu(' + id + ');" ondblclick="return setData(' + id + ');"  value="' + col + '" id="' + id + '" />';
                }

                div.innerHTML = div.innerHTML + tb;
            }
            else {
                genInstanceID(field.value);
            }
        }
        function genInstanceID(fval)  // get button id's
        {

            window.open("ControlID.aspx?val=" + fval, "ControlID", "width=400px,height=130px")
        }

        function createInstance(retVal)   // create button and assign id
        {
            var id = retVal.split("`");

            if (id[0] != "") {
                //document.getElementById("hidAlllabels").value=id[0];
                var lblCnt = parseInt(document.getElementById("lblCount").value);
                lblCnt = lblCnt + 1
                document.getElementById("lblCount").value = lblCnt.toString();
                var typ = document.getElementById("divType").value;
                var dd = "";
                if (typ == "header")
                    dd = "divHeader";
                else if (typ == "footer")
                    dd = "divFooter";
                var div = document.getElementById(dd);
                var val = id[1];
                var tb = "";
                // tb='<input class="drag"  type="text" title="Right click to delete" style="height:12px;width:176px;font-family:verdana;font-size:8pt;" oncontextmenu="return menu('+ id[0] +');" ondblclick="return setData('+ id[0] +');"  value="'+ id[1] +'" id="'+ id[0] +'" />';

                ///// Added By lalit for initial blank value in created  element

                tb = '<input class="drag"  type="text" title="Right click to delete" style="height:16px;width:180px;font-family:verdana;font-size:8pt;" oncontextmenu="return menu(' + id[0] + ');" ondblclick="return setData(' + id[0] + ');"  value="" id="' + id[0] + '" />';

                div.innerHTML = div.innerHTML + tb;
            }

        }
        function delMe() // to delete a dynamic object
        {

            if (toDel != "") {
                var v = document.getElementById(toDel);
                var parent;
                if (v.parentElement.id == "div" + toDel)  // if the element is table
                {
                    parent = "divTables";
                    var str = document.getElementById(parent);
                    //var oInputs = str.getElementsByTagName('input');
                    childDiv = document.getElementById("div" + toDel);

                    str.removeChild(childDiv);

                    // remove table name from the list
                    var tbName = document.getElementById("hidTables").value;
                    var tb2 = tbName.split("~");
                    var b = 0;
                    var finl = "";
                    for (b = 0; b <= tb2.length - 1; b++) {
                        if (toDel != tb2[b]) {
                            if (finl == "") {
                                finl = tb2[b];
                            }
                            else {
                                finl = finl + "~" + tb2[b];
                            }
                        }
                    }

                    document.getElementById("hidTables").value = finl;
                    //////
                }
                else // otherwise
                {

                    if (document.getElementById("divType").value == "header") {
                        parent = "divHeader";
                    }
                    else if (document.getElementById("divType").value == "details") {

                        parent = "divDetails";
                        var nul = document.getElementById("btnEx").value;
                        nul = nul.replace("aa", "");

                        // delete format value from the hidden field
                        var tmFormat = document.getElementById("hidDformatid").value;
                        var tmF1 = tmFormat.split("~");
                        var tm1 = 0;
                        var tmF2 = "";
                        for (tm1 = 0; tm1 <= tmF1.length - 1; tm1++) {
                            if (v.id != tmF1[tm1]) {
                                if (tmF2 == "") {
                                    tmF2 = tmF1[tm1];
                                }
                                else {
                                    tmF2 = tmF2 + "~" + tmF1[tm1];
                                }
                            }
                        }
                        document.getElementById("hidDformatid").value = tmF2;
                        //////////////////////////////////////////
                        // remove element from grpby
                        ////            var yes=0;
                        ////            if(document.getElementById("hidGroupby").value!="")
                        ////            {
                        ////                var vb=0;
                        ////                var dfgh=document.getElementById("hidGroupby").value;
                        ////                var torep="";
                        ////                var vbn=document.getElementById("hidGroupby").value.split(",");
                        ////               for(vb=0;vb<=vbn.length-1;vb++)
                        ////                {
                        ////                    var df=vbn[vb].replace("$",".");
                        ////                    if(v.value==df)
                        ////                    {
                        ////                        yes=1;
                        ////                        torep=df;
                        ////                    }
                        ////                }
                        ////                document.getElementById("hidGroupby").value=dfgh.replace(","+torep,"");
                        ////                dfgh=document.getElementById("hidGroupby").value;
                        ////                document.getElementById("hidGroupby").value=dfgh.replace(torep+",","");
                        ////                dfgh=document.getElementById("hidGroupby").value;
                        ////                document.getElementById("hidGroupby").value=dfgh.replace(torep,"");
                        ////               
                        ////            }
                        ////// commented as the process will take place at final processing time
                        // remove formula name from list
                        //            if(yes==0)
                        //            {

                        var st = v.value;
                        ReportDesignerAjax.deleteFormula(document.getElementById("btnEx").value, st, document.getElementById("hidDformula").value, finalFor);
                        //            }
                        //
                        ////////// Delete color condition 
                        var idd = v.id;
                        ReportDesignerAjax.btnDelColConDel(document.getElementById("btnEx").value, document.getElementById("hidDcolcon").value, idd, finalCon);

                        //////////////////
                    }
                    else if (document.getElementById("divType").value == "footer") {
                        parent = "divFooter";
                    }
                    var t = 0;
                    var str = document.getElementById(parent);
                    //var oInputs = str.getElementsByTagName('input');
                    if (parent != "divDetails") {
                        v.style.visibility = 'hidden';
                    } else {
                        str.removeChild(v);
                    }
                    // str.removeChild(v);
                    //        try  // unwanted remove breaks
                    //        {
                    //            if(document.getElementById("br_"+toDel) != "undefined")
                    //            {
                    //                var br=document.getElementById("br_"+toDel);
                    //                str.removeChild(br);
                    //            }
                    //        }
                    //        catch(e)
                    //        {
                    //            t=9;
                    //        }
                }
            }
            hideMenu();

        }

        // updated color condition returnded here after an object is deleted
        function finalCon(response) {
            if (response.value != "undefined" || response.value != null) {
                var t1 = response.value;
                document.getElementById("hidDcolcon").value = t1;
            }
        }


        // formula returned here after deleting an object of details
        function finalFor(response) {
            if (response.value != "undefined" || response.value != null) {
                var t1 = response.value;
                var t = t1.split("@#@");
                document.getElementById("hidFormulacnt").value = t[0];
                if (t.length > 1) {
                    document.getElementById("hidDformula").value = t[1];
                }
            }
        }
        ////////////////
        function hideMenu() {
            document.getElementById("divContext").style.display = "none";
            document.getElementById("divContext").style.color = "#ffffff";
            toDel = "";
        }

        function menu(a) {

            var m = a.id;
            var left = getRealLeft(m);
            var top = getRealTop(m);
            var menuDiv = document.getElementById("divContext");
            //menuDiv.style.top = window.event.clientY;
            //menuDiv.style.left = window.event.clientX;
            menuDiv.style.top = top + 12;
            menuDiv.style.left = left + 15;
            menuDiv.style.display = "block";
            toDel = m;
            return false;
        }
        function menu1(a) {
            var m = a.id;
            var left = getRealLeft(m);
            var top = getRealTop(m);

            var menuDiv = document.getElementById("divContext");
            //menuDiv.style.top = window.event.clientY;
            //menuDiv.style.left = window.event.clientX;
            menuDiv.style.top = top + 12;
            menuDiv.style.left = left + 40;
            menuDiv.style.display = "block";
            toDel = m;
            return false;
        }
        function me() {
            alert("single click");
        }
        ///////// ajax method ends////////////
        // Resize the controls and divisions/////////////
        function splitterOnResize(width) {

            // do any other work that needs to happen when the 
            // splitter resizes. this is a good place to handle 
            // any complex resizing rules, etc.

            // make sure the width is in number format
            if (typeof width == "string") {
                width = new Number(width.replace("px", ""));
            }
        }
        function splitterComplete(width) {

            //to check if any control present at the rezing boundry???
            var str = document.getElementById("divHeader");
            var oid = "divHeader";
            if (width.id == document.getElementById("<%=footerSpliter.ClientID%>").id) {
                str = document.getElementById("divFooter");
                oid = "divFooter";
            }
            var objects = "";
            var oInputs = str.getElementsByTagName("input");
            var myarray = new Array(oInputs.length);

            if (oInputs.length != 0) {
                var i = 0;
                for (i = 0; i <= oInputs.length - 1; i++) {
                    // fetch the top+height value of each object
                    myarray[i] = oInputs[i].style.posTop + oInputs[i].style.posHeight;
                    //////
                }
                /////// find out the max of all 
                var mx = parseInt(myarray[0]);
                for (i = 1; i <= myarray.length - 1; i++) {
                    if (parseInt(myarray[i]) > mx) {
                        mx = parseInt(myarray[i]);
                    }
                }

                //// reassign height to the div if some element lies at the bondary
                var abc = width.primaryResizeTarget.style.height;
                var ty = new Number(abc.replace("px", ""))
                if (ty < mx) {
                    str.style.height = mx + 5 + "px";   //reassign height to div
                    var splitter = width.splitterBar;
                    splitter.style.backgroundColor = width.backgroundColor;
                    splitter.style.position = "absolute";
                    splitter.style.zIndex = 0;
                    splitter.style.height = width.splitterHeight + "px";
                    var op = width.offsetParent;
                    var tp = getRealTop(oid);
                    var yu = str.offsetHeight + tp;
                    splitter.style.top = yu + "px"; //reassign top to the splitterbar
                }
            }
        }
        ///////////////Resize the controls and divisions Ends///////////////////////   

        function openReport()// Open Existing  Report
        {

            window.open("OpenReport.aspx", "OpenReport", "width=550,height=340,scrollbars=no,status=yes");
        }

        function saveReport()// Save/Update Report
        {

            getHPosition(); // to store position of the header elements
            getDPosition(); // to store position of the details elements
            getDformat(); // to store the format value of the details element;
            getFPosition(); // to store position of the footer elements
            getHeight(); // to get the height of the header and footer
            //            document.getElementById("hidHeaderformat").value=document.getElementById("headerFontsize").value+";"+document.getElementById("headerFontfamily").value+";"+document.getElementById("headerForecolor").value+";"+document.getElementById("headerBackcolor").value+";"+document.getElementById("headerStyle").value;
            //            document.getElementById("hidDetailsformat").value=document.getElementById("detailsFontsize").value+";"+document.getElementById("detailsFontfamily").value+";"+document.getElementById("detailsForecolor").value+";"+document.getElementById("detailsBackcolor").value+";"+document.getElementById("detailsStyle").value;
            //            document.getElementById("hidFooterformat").value=document.getElementById("footerFontsize").value+";"+document.getElementById("footerFontfamily").value+";"+document.getElementById("footerForecolor").value+";"+document.getElementById("footerBackcolor").value+";"+document.getElementById("footerStyle").value;


            var dpt = document.getElementById("hidDepartment").value;
            var cli = document.getElementById("hidClient").value;
            var lob = document.getElementById("hidLob").value;
            var dattbl = document.getElementById("hidDatetable").value;
            window.open("SaveReport.aspx?mode=new&dpt=" + dpt + "&cli=" + cli + "&lob=" + lob + "&dattbl=" + dattbl, "SaveReport", "width=470,height=300,scrollbars=no,status=yes");
        }
        function doneSave() {
            alert("Report Saved Successfully");
        }
        function shareReport() /// Share Existing Report
        {
            window.open("ShareReport.aspx", "ShareReport", "width=550,height=630,scrollbars=no,status=yes");
        }
        function setData(objName) /// Set data to an object
        {


            var objects = "";
            var src = "";
            var parent;
            if (document.getElementById("divType").value == "header") {
                parent = "divHeader";
                src = "header";
            }
            else if (document.getElementById("divType").value == "details")
                parent = "divDetails";
            else if (document.getElementById("divType").value == "footer") {
                parent = "divFooter";
                src = "footer";
            }

            var str = document.getElementById(parent);
            var oInputs = str.getElementsByTagName("input");
            var i = 0;
            if (parent == "divDetails") {
                src = "details";
                for (i = 0; i <= oInputs.length - 1; i++) {
                    if (objects == "") {
                        objects = oInputs[i].value;
                    }
                    else {
                        objects = objects + "~" + oInputs[i].value;
                    }
                }
            }
            else {
                for (i = 0; i <= oInputs.length - 1; i++) {
                    if (oInputs[i].style.visibility != 'hidden') {
                        if (objects == "") {
                            objects = oInputs[i].id;
                        }
                        else {
                            objects = objects + "~" + oInputs[i].id;
                        }
                    }
                }
            }

            var objVal = objName.value;
            var val = document.getElementById("hidTables").value;  // storing tablenames in a single field
            window.open("setData.aspx?obj=" + objName.id + "&val=" + objVal + "&tbl=" + val + "&objs=" + objects + "&src=" + src, "SetData", "width=550,height=500,scrollbars=yes,status=yes");
        }

        function getTable() ///////// To open Add Table Window //////////////
        {

            window.open("getTables.aspx?src=Welcome", "SelectTable", "width=450,height=470,scrollbars=yes,status=yes");

        }
        function Where(val)  // store where condition
        {
            if (val != "") {
                var sp = val.split("`");
                if (sp.length > 1) {
                    document.getElementById("hidWhere").value = sp[0];  // store where clause
                    //  document.getElementById("hidTables").value=sp[1];  // update tables
                }
            }
        }
        function Formula(val)  // store formula field
        {
            var form = val.split("`");
            if (val != "") {
                // document.getElementById("hidFormula").value=form[0];
                //document.getElementById("hidFormulaname").value=form[1];
                // document.getElementById("hidTables").value=form[1];

                var temp = document.getElementById("hidDformula").value;

                if (document.getElementById("hidDformula").value == "")
                    document.getElementById("hidDformula").value = form[0];
                else {
                    var temp1 = temp.split("~");

                    var t0 = 0;
                    var tb = 0;
                    for (t0 = 0; t0 <= temp1.length - 1; t0++);
                    {

                        if ((form[0]) == (temp1[t0]))

                            tb = 1;
                    }
                    if (tb == 0) {
                        document.getElementById("hidDformula").value = document.getElementById("hidDformula").value + "~" + form[0];
                    }
                }
                formulaInstance(form[0])
            }

        }
        // whenever a new column is added using formula button
        function formulaInstance(fname) {
            var newVal = fname.split(" AS ");
            fname = newVal[1];
            dd = "divDetails";
            var btnCnt = parseInt(document.getElementById("btnCount").value);
            btnCnt = btnCnt + 1;
            document.getElementById("btnCount").value = btnCnt.toString();
            //alert(btnCnt.toString());
            id = "btn" + btnCnt;
            var div = document.getElementById(dd);
            var tb = ""
            if (div.innerHTML == "") {
                tb = '<input class="drag"  type="button" title="Right click to delete" style="font-size:8pt;font-family:Verdana;border:solid 1px grey; color:#000000;background-color:#ffffff;text-align:center;" oncontextmenu="return menu(' + id + ');" ondblclick="return setData(' + id + ');"  value="' + fname + '" id="' + id + '" />';
            }
            else {
                tb = '<br id="br_' + id + '"/><input class="drag"  type="button" title="Right click to delete" style="font-size:8pt;font-family:Verdana;border:solid 1px grey; color:#000000;background-color:#ffffff;text-align:center;" oncontextmenu="return menu(' + id + ');" ondblclick="return setData(' + id + ');"  value="' + fname + '" id="' + id + '" />';
            }
            div.innerHTML = div.innerHTML + tb;
        }
        function Having(val)  // store having field
        {
            if (val != "") {
                var form = val.split("`");
                document.getElementById("hidHaving").value = form[0];
                // document.getElementById("hidTables").value=form[1];
            }
        }
        function Orderby(val)  // store Orderby field
        {
            if (val != "") {
                var form = val.split("`");
                document.getElementById("hidOrderby").value = form[0];
                // document.getElementById("hidTables").value=form[1];
            }
        }
        function Groupby(val)  // store Groupby field
        {
            if (val != "") {
                var form = val.split("`");
                document.getElementById("hidGroupby").value = form[0];
                //document.getElementById("hidTables").value=form[1];
            }
        }
        function updateFormula(objFormula) //////// To set formula to an individula object
        {

            // store formula for the dynamic object

            var str = objFormula.split("@#@");
            var obj = str[0];                    // get the object name
            var val = "";
            if (str.length > 1) {
                val = str[1];
            }


            document.getElementById(obj).value = val; // update the object textvalue

        }
        function updated(response) {
            if (response.value != "undefined" || response.value != null) {
                document.getElementById("hidDformula").value = response.value;
            }
        }
        function updateColorcon(objColorcon) ///////// To set color condition to an individual object
        {

            var target = "hidFcolorcon"
            if (document.getElementById("divType").value == "header") {
                target = "hidHcolorcon";
            }
            else if (document.getElementById("divType").value == "details") {
                target = "hidDcolcon";
            }
            else {
                target = "hidFcolorcon";
            }

            document.getElementById(target).value = objColorcon;

        }

        function updateFormat(objStyle) /////////////// To set format to an individual object
        {

            var target = "";

            if (document.getElementById("divType").value == "header")
                target = "hidHformat";
            else if (document.getElementById("divType").value == "details")
                target = "hidDformat";
            else if (document.getElementById("divType").value == "footer")
                target = "hidFformat";


            var str = objStyle.split(">");
            var obj = str[0];                  //// fetch object name
            var style = str[1].split(";");   ///// fetch object style
            var i = 0;
            var backchk = "";
            for (i = 0; i <= style.length - 1; i++) {
                var stn = style[i].split(":");
                if (stn[0] == "font-size") {
                    document.getElementById(obj).style.fontSize = stn[1]; //assign font-size to the object
                }
                if (stn[0] == "font-family") {
                    document.getElementById(obj).style.fontFamily = stn[1]; // assign font-family
                }
                if (stn[0] == "font-weight") {
                    document.getElementById(obj).style.fontWeight = stn[1];  //  put the text in bold
                }
                if (stn[0] == "font-style") {
                    document.getElementById(obj).style.fontStyle = stn[1];  //  make it italic
                }
                if (stn[0] == "text-decoration") {
                    document.getElementById(obj).style.textDecoration = stn[1];  //  underline the text
                }
                if (stn[0] == "background-color") {
                    backchk = "asd"
                    document.getElementById(obj).style.backgroundColor = stn[1];  // assign background-color
                }
                if (stn[0] == "color") {
                    document.getElementById(obj).style.color = stn[1];  // assign fore-color
                }
                if (stn[0] == "width") {
                    document.getElementById(obj).style.width = stn[1];  // assign width
                }
                if (stn[0] == "height") {
                    document.getElementById(obj).style.height = stn[1];  // assign height
                }
            }
            if (backchk == "") {
                document.getElementById(obj).style.backgroundColor = "#ffffff";
            }
            // store the format value
            //  var finalSt="";
            //	  if(document.getElementById("divType").value=="details") // if details, then store the value else id
            //	  {
            //	   var st=document.getElementById(obj).value;
            //	   finalSt=st+"@#@"+str[1];
            //	   }
            //	   else
            //	   {
            //	    finalSt=objStyle;
            //	   }


            // store format value only for header & footer. For details pane, store it at processing
            if (document.getElementById("divType").value == "header" || document.getElementById("divType").value == "footer") {
                if (document.getElementById(target).value == "") {
                    document.getElementById(target).value = objStyle;
                }
                else {
                    document.getElementById(target).value = document.getElementById(target).value + "~" + objStyle;
                }
            }
            else // otherwise store id of the formatted details element
            {
                if (document.getElementById("hidDformatid").value == "")
                    document.getElementById("hidDformatid").value = obj;
                else {

                    var sp = document.getElementById("hidDformatid").value.split("~");
                    var t = 0;
                    var b = 0;
                    for (t = 0; t <= sp.length - 1; t++)   // check for already present element
                    {
                        if (sp[t] == obj) {
                            b = 1;
                        }

                    }

                    if (b == 0)  // if already not present then add
                    {
                        document.getElementById("hidDformatid").value = document.getElementById("hidDformatid").value + "~" + obj;
                    }
                }
            }

        }
        ///////// Format the report /////////////////	
        //   
        function btnDone_onclick() {


            var style = "";
            var bold = "";
            var italic = "";
            var underline = "";
            var i = 0;
            var constituent = document.getElementById("ddlConstituent").value;
            style = "font-size:" + document.getElementById("<%=ddlFontsize.ClientID%>").value + "pt";
            style = style + ";font-family:" + document.getElementById("<%=ddlFontfamily.ClientID%>").value;
            style = style + ";color:" + document.getElementById("hidForecolor").value;
            style = style + ";background-color:" + document.getElementById("hidBackcolor").value;
            style = style + ";border:solid 1px " + document.getElementById("hidBackcolor").value;
            if (document.getElementById("chkBold").checked == true) {
                style = style + ";font-weight:bold";
            }
            else {
                style = style + ";font-weight:normal";
            }
            if (document.getElementById("chkItalic").checked == true) {
                style = style + ";font-style:italic";
            }
            else {
                style = style + ";font-style:normal";
            }
            if (document.getElementById("chkUnderline").checked == true) {
                style = style + ";text-decoration:underline";
            }
            else {
                style = style + ";text-decoration:none";
            }

            if (constituent == "Header" || constituent == "Whole Report") {
                document.getElementById("hidHeaderformat").value = style;
                applyFormat(style, "divHeader");
            }
            if (constituent == "Details Pane" || constituent == "Whole Report") {
                document.getElementById("hidDetailsformat").value = style;
                applyFormat(style, "divDetails");
            }
            if (constituent == "Footer" || constituent == "Whole Report") {
                document.getElementById("hidFooterformat").value = style;
                applyFormat(style, "divFooter");
            }
            alert("The format has been applied to the " + constituent);
        }
        function applyFormat(value, obj) // val contains the format value while obj refers to the id of the  object to apply the format
        {

            var val = value.split(";");
            var i = 0;
            for (i = 0; i <= val.length - 1; i++) {
                var nxt = val[i].split(":");
                if (nxt[0] == "font-size")
                    document.getElementById(obj).style.fontSize = nxt[1];
                else if (nxt[0] == "font-family")
                    document.getElementById(obj).style.fontFamily = nxt[1];
                else if (nxt[0] == "color")
                    document.getElementById(obj).style.color = nxt[1];
                else if (nxt[0] == "background-color") {
                    if (nxt[1] != "" || nxt[1] != " ") {
                        document.getElementById(obj).style.backgroundColor = nxt[1];
                        document.getElementById(obj).style.backgroundImage = 'none';
                    }
                }
                else if (nxt[0] == "font-style")
                    document.getElementById(obj).style.fontStyle = nxt[1];
                else if (nxt[0] == "font-weight")
                    document.getElementById(obj).style.fontWeight = nxt[1];
                else if (nxt[0] == "text-decoration")
                    document.getElementById(obj).style.textDecoration = nxt[1];
                else if (nxt[0] == "height")
                    document.getElementById(obj).style.height = nxt[1];
                else if (nxt[0] == "width")
                    document.getElementById(obj).style.width = nxt[1];
            }
        }
        //////////////Format the report ends //////////////////////////////////////////////////////////
        /////////////////// Remove the formatting of the report /////////////////////////
        function btnRemove_onclick() {
            var constituent = document.getElementById("ddlConstituent").value;
            remove_final(constituent);
            alert("The format has been removed from the " + constituent);
        }
        function remove_final(remFrom) {

            var constituent = remFrom;
            document.getElementById("hidForecolor").value = "#000000";
            document.getElementById("hidForecolor").value = "#000000";
            if (constituent == "Header" || constituent == "Whole Report") {
                document.getElementById("hidHeaderformat").value = "";
                document.getElementById("divHeader").style.backgroundImage = "url('../images/header.png')";
                document.getElementById("divHeader").style.backgroundColor = "#ffffff";
                document.getElementById("divHeader").style.color = "#000000";
                document.getElementById("divHeader").style.fontFamily = "Verdana";
                document.getElementById("divHeader").style.fontSize = "8pt";
                document.getElementById("divHeader").style.fontWeight = "normal";
                document.getElementById("divHeader").style.fontStyle = "normal";
                document.getElementById("divHeader").style.textDecoration = "none";
            }
            if (constituent == "Details Pane" || constituent == "Whole Report") {
                document.getElementById("hidDetailsformat").value = "";
                document.getElementById("divDetails").style.backgroundImage = "url('../images/details.png')";
                document.getElementById("divDetails").style.backgroundColor = "#ffffff";
                document.getElementById("divDetails").style.color = "#000000";
                document.getElementById("divDetails").style.fontFamily = "Verdana";
                document.getElementById("divDetails").style.fontSize = "8pt";
                document.getElementById("divDetails").style.fontWeight = "normal";
                document.getElementById("divDetails").style.fontStyle = "normal";
                document.getElementById("divDetails").style.textDecoration = "none";
            }
            if (constituent == "Footer" || constituent == "Whole Report") {
                document.getElementById("hidFooterformat").value = "";
                document.getElementById("divFooter").style.backgroundImage = "url('../images/footer.png')";
                document.getElementById("divFooter").style.backgroundColor = "#ffffff";
                document.getElementById("divFooter").style.color = "#000000";
                document.getElementById("divFooter").style.fontFamily = "Verdana";
                document.getElementById("divFooter").style.fontSize = "8pt";
                document.getElementById("divFooter").style.fontWeight = "normal";
                document.getElementById("divFooter").style.fontStyle = "normal";
                document.getElementById("divFooter").style.textDecoration = "none";
            }

        }
        /////////////////Remove the formatting of the report ends /////////////////////////////
        ///////////////////// Design Header on button Click //////////////////////////
        function btnHeader_onclick() {
            document.getElementById("divDetails").style.display = "none";
            document.getElementById("divFooter").style.display = "none";
            document.getElementById("spl").style.display = "none";
            //var hj=document.getElementById("divHeader").style.height;
            //var al=prompt("Enter Header Height",hj) ;
            //var st=al.split(",");
            // document.getElementById("divHeader").style.height=st[0];
            //document.getElementById("divHeader").style.weight=st[1];
            document.getElementById("divHeader").style.display = "block";
            document.getElementById("hspl").style.display = "block";

            //document.getElementById("<%=footerSpliter.ClientID%>").style.display="none";           
            var myImg = document.getElementById("imgRep");
            myImg.src = "../images/hd.jpg";
            document.getElementById("lblCaption").innerHTML = "Add labels & tablecolumns and apply formula to them. ";
            document.getElementById("divType").value = "header";
        }

        ////////////////////////Design Header on button Click ends ///////////////////////////

        ///////////// Design Details Pane on button click //////////////////////
        function btnDetails_onclick() {
            document.getElementById("hspl").style.display = "none";
            document.getElementById("divHeader").style.display = "none";
            document.getElementById("spl").style.display = "none";
            document.getElementById("divFooter").style.display = "none";
            document.getElementById("divDetails").style.display = "block";
            //                   if(document.getElementById("hidReportmode").value=="New") // remove existing tables
            //           {
            var myImg = document.getElementById("imgRep");
            myImg.src = "../images/det.jpg";
            //           }
            document.getElementById("lblCaption").innerHTML = "Add tablecolumns and prepare a SQL query.";
            document.getElementById("divType").value = "details";
        }
        ////////////// Design Details Pane on button click ends ///////////////////////////////

        ///////////// Design Footer on button click //////////////////////
        function btnFooter_onclick() {
            document.getElementById("divFooter").style.display = "block";
            document.getElementById("spl").style.display = "block";
            document.getElementById("hspl").style.display = "none";
            document.getElementById("divHeader").style.display = "none";
            document.getElementById("divDetails").style.display = "none";
            var myImg = document.getElementById("imgRep");
            myImg.src = "../images/ft.jpg";
            document.getElementById("lblCaption").innerHTML = "Add labels & tablecolumns and apply formula to them. ";
            document.getElementById("divType").value = "footer";
        }
        //////////////Design Footer on button click ends ///////////////////////
        function genLabel() {
            if (document.getElementById("divType").value == "details") {
                alert("No Labels Are Allowed In Details Pane.")
            }
            else {
                var ss = document.getElementById("divType").value
                window.open("ControlID.aspx?val=" + ss, "ControlID", "width=400px,height=130px")
            }
        }
        function Label(id) //Generate dynamic label
        {

            // document.getElementById("hidAlllabels").value=id;
            var lblCnt = parseInt(document.getElementById("lblCount").value);
            lblCnt = lblCnt + 1;
            document.getElementById("lblCount").value = lblCnt.toString();
            var typ = document.getElementById("divType").value;
            var dd = "";
            if (typ == "header") {
                dd = "divHeader";
            }
            else {
                dd = "divFooter";
            }
            var div = document.getElementById(dd);
            var tb = "";
            tb = '<input type="text"  class="drag" style="height:16px;width:180px;font-family:verdana;font-size:8pt; border:solid 1px gray;" oncontextmenu="return menu(' + id + ');" ondblclick="return setData(' + id + ');" id="' + id + '" />';
            div.innerHTML = div.innerHTML + tb;

        }

        function btnProcessrep_onclick() //Process the report
        {

            getHPosition(); // to store position of the header elements
            getDPosition(); // to store position of the details elements
            getDformat(); // to store the format value of the details element
            getDColcon(); // to store color condition of details element
            getFPosition(); // to store position of the footer elements
            getHeight(); // to get the height of the header and footer

            if (document.getElementById("chkNewwindow").checked == true) {
                document.forms[0].target = "_new" // Open in new window
            }
            else {
                document.forms[0].target = "FRMRESULST"; // open in same window			
            }
            if (document.getElementById("chkSummarized").checked == true) {
                document.forms[0].action = "summarizedreport.aspx?Rep_name="
            + document.getElementById("hidReportname").value
            + "&Rep_Scope=" + document.getElementById("hidReportscope").value
            + "&dept=" + document.getElementById("hidDepartment").value
            + "&clt=" + document.getElementById("hidClient").value
            + "&lob=" + document.getElementById("hidLob").value
                document.forms[0].submit();
                // submit the data at another page
            }
            else {
                document.forms[0].action = "ShowData.aspx?Rep_name="
             + document.getElementById("hidReportname").value
            + "&Rep_Scope=" + document.getElementById("hidReportscope").value
            + "&dept=" + document.getElementById("hidDepartment").value
            + "&clt=" + document.getElementById("hidClient").value
            + "&lob=" + document.getElementById("hidLob").value
                document.forms[0].submit();
                // submit the data at another page
            }



            document.forms[0].target = "_self"; // Call in same window after action

        }
        function getHPosition()  // Store header elements position
        {

            var objects = "";
            var str = document.getElementById("divHeader");
            var oInputs = str.getElementsByTagName("input");
            if (oInputs.length != 0) {
                var i = 0;
                for (i = 0; i <= oInputs.length - 1; i++) {
                    var pro = "";

                    pro = oInputs[i].id + "@#@" + oInputs[i].value + "#@#" + oInputs[i].style.posTop + "," + oInputs[i].style.posLeft + "," + oInputs[i].style.visibility;
                    if (objects == "") {
                        objects = pro
                    }
                    else {
                        objects = objects + "~" + pro;
                    }

                }
                document.getElementById("hidHpos").value = objects;
                // alert(objects);
            }
            else {
                document.getElementById("hidHpos").value = "";
            }
        }

        //////// updated getDPosition using array.sort method on 25-09-08 by Usha Sheokand
        function getDPosition()  // Store Details elements position
        {

            var objects = "";
            var str = document.getElementById("divDetails");
            var oInputs = str.getElementsByTagName("input");
            var arr = "";
            var myarray;
            if (oInputs.length != 0) {
                var i = 0;
                var l = 0;

                var k = 0;
                /// declare an 2d array
                var myarray = new Array(2);
                var rows = oInputs.length;
                for (i = 0; i < rows; i++)
                    myarray[i] = new Array(2);
                ///////////intialize items into this 2d array

                for (i = 0; i <= rows - 1; i++) {
                    var topMe = getRealTop(oInputs[i].id);

                    myarray[i] = [topMe, oInputs[i].id];
                }
                ////// sort the array according to the top position
                myarray.sort(value);
                var result = myarray.join('#');
                sp = result.split("#");


                for (i = 0; i <= rows - 1; i++) {
                    var sp2 = sp[i].split(",");

                    var pro = ""
                    var pro1 = document.getElementById(sp2[1]).value;
                    pro = pro1.replace(".", "$");  // convert tablename.columnname to tablename$columnname


                    if (arr == "") {
                        arr = pro;
                    }
                    else {
                        arr = arr + "~" + pro;
                    }

                }

            }
            document.getElementById("hidDpos").value = arr; // Store the elements' position
        }
        ///////////// ends
        ///////// sorting function
        function value(a, b) {
            a = a[0];
            b = b[0];
            return a == b ? 0 : (a < b ? -1 : 1)
        }
        //////////// ends //////////
        function finaleGrp(res) {

            if (res.value != null || res.value != "undefined") {
                document.getElementById("hidGroupby").value = res.value;

                document.getElementById("hidOrderby").value = res.value;

            }
        }

        function getDformat() // to store the format value of the details element
        {

            document.getElementById("hidDformat").value = "";
            var vbtn = document.getElementById("btnEx2").value;
            vbtn = vbtn.replace("a", "");
            if (document.getElementById("hidDformatid").value != "") {
                var allId = document.getElementById("hidDformatid").value.split("~");
                if (allId.length != 0) {

                    var i = 0;

                    for (i = 0; i <= allId.length - 1; i++) {
                        var ob = document.getElementById(allId[i]);
                        var myval = ob.value;
                        var toVal = myval;

                        toVal = toVal.replace("[", "");
                        toVal = toVal.replace("]", "");

                        var style = toVal + ">"; // get value of the object
                        style = style + "font-size:" + ob.style.fontSize;
                        style = style + ";font-family:" + ob.style.fontFamily;
                        style = style + ";font-weight:" + ob.style.fontWeight;
                        style = style + ";text-decoration:" + ob.style.textDecoration;
                        style = style + ";font-style:" + ob.style.fontStyle;
                        style = style + ";background-color:" + ob.style.backgroundColor;
                        style = style + ";color:" + ob.style.color;
                        style = style + ";width:" + ob.style.width + "px";
                        style = style + ";height:" + ob.style.height + "px";
                        if (document.getElementById("hidDformat").value == "") {
                            document.getElementById("hidDformat").value = style;
                        }
                        else {
                            document.getElementById("hidDformat").value = document.getElementById("hidDformat").value + "~" + style;
                        }

                    }
                }
            }
        }

        function getDColcon() // store colorcondition on details element
        {
            var fnlcon = "";
            var colocon = document.getElementById("hidDcolcon").value;
            if (colocon != "") {
                var col1 = colocon.split("~");
                var y = 0;
                for (y = 0; y <= col1.length - 1; y++) {
                    var col2 = col1[y].split("@#@");
                    var vbn = document.getElementById(col2[0]).value;
                    vbn = vbn.replace("[", "");
                    vbn = vbn.replace("]", "");
                    if (fnlcon == "") {
                        fnlcon = vbn + "@#@" + col2[1];
                    }
                    else {
                        fnlcon = fnlcon + "~" + vbn + "@#@" + col2[1];
                    }
                }

            }
            document.getElementById("hidColorcondition").value = fnlcon;
        }

        function getFPosition()  // Store Footer elements position
        {

            var objects = "";
            var str = document.getElementById("divFooter");
            var oInputs = str.getElementsByTagName("input");
            if (oInputs.length != 0) {
                var i = 0;
                for (i = 0; i <= oInputs.length - 1; i++) {
                    var pro = "";
                    pro = oInputs[i].id + "@#@" + oInputs[i].value + "#@#" + oInputs[i].style.posTop + "," + oInputs[i].style.posLeft + "," + oInputs[i].style.visibility;
                    if (objects == "") {
                        objects = pro;
                    }
                    else {
                        objects = objects + "~" + pro;
                    }
                }
                document.getElementById("hidFpos").value = objects;
            }
            else {
                document.getElementById("hidFpos").value = "";
            }
        }
        function getHeight() {
            document.getElementById("hidHeight").value = document.getElementById("divHeader").style.height + "," + document.getElementById("divFooter").style.height;
        }



        function btnWhere_onclick() {
            var val = document.getElementById("hidTables").value;  // storing tablenames in a single field
            window.open("Whereclause.aspx?tbl=" + val, "WhereClause", "width=850,height=600,scrollbars=no,status=yes");
        }

        function btnFormula_onclick() {
            var val = document.getElementById("hidTables").value;  // storing tablenames in a single field
            window.open("Formula.aspx?tbl=" + val, "Formula", "width=660,height=500,scrollbars=no,status=yes");
        }

        function btnHaving_onclick() {
            var val = document.getElementById("hidTables").value;  // storing tablenames in a single field
            window.open("Having.aspx?tbl=" + val, "HavingClause", "width=450,height=450,scrollbars=no,status=yes");
        }

        function btnOrderby_onclick() {
            var val = document.getElementById("hidTables").value;  // storing tablenames in a single field
            window.open("OrderBy.aspx?tbl=" + val, "OrderBy", "width=550,height=450,scrollbars=no,status=yes");
        }

        function btnGroupby_onclick() {
            //

            getDPosition();
            var elem = document.getElementById("hidDpos").value;
            var formul = document.getElementById("hidDformula").value;
            var grpby = document.getElementById("hidGroupby").value;

            //
            var val = document.getElementById("hidTables").value;  // storing tablenames in a single field
            //window.open("GroupBy.aspx?tbl="+val+"&elem="+elem+"&formu="+formul+"&grpby="+grpby,"GroupBy","width=800,height=350,scrollbars=no,status=yes");
            window.open("GroupBy.aspx?elem=" + elem, "GroupBy", "width=800,height=350,scrollbars=no,status=yes");


        }

        function btnColorcondition_onclick() {
            var val = document.getElementById("hidTables").value;  // storing tablenames in a single field
            window.open("ColorCondition.aspx?tbl=" + val, "ColorCondition", "width=450,height=450,scrollbars=yes,status=yes");
        }

        function btnReset_onclick() {

            location.href("welcome.aspx?val=5");
        }

        function btnArchive_onclick() {
            window.open("ArchiveReport.aspx", "ArchiveReport", "width=520,height=250,scrollbars=no,status=yes");
        }
        function btnGraphs_onclick() {
            var val = "";
            var t = 0;
            t = 1;
            if (document.getElementById("hidReportname").value != "") {
                val = document.getElementById("hidReportname").value;
                t = 1;
            }
            else {
                t = 0;
            }
            if (t == 0) {
                alert("No Report Name Found. Process and Save The Report First!");
            }
            else {
                var dept = document.getElementById("hidDepartment").value;
                var clt = document.getElementById("hidClient").value;
                var lob = document.getElementById("hidLob").value;
                var todate = document.getElementById("txtEnddate").value;
                var fromdate = document.getElementById("txtStartdate").value;
                window.open("../Graphicalpresentation/graphdata.aspx?val=5&currentreport=" + val + "&dept=" + dept + "&client=" + clt + "&lob=" + lob + "&fromdate=" + fromdate + "&todate=" + todate, "ViewGraph");
                //        document.forms[0].action="../Graphicalpresentation/graphdata.aspx?val=5&currentreport="+val;
                //        document.forms[0].target="_self";
                //        document.forms[0].submit();


            }
        }

        function btnDefaultgraph_onclick() {
            var val = document.getElementById("hidReportname").value;
            window.open("viewGraphs.aspx?repname=" + val, "Default", "");
        }

    </script>

    <div id="divOutertable" title="Tables" style="cursor: pointer; background-color: #4D8678;
        color: White;" onclick="toggleDiv('divTable', 'imgTables',320)">
        <table summary="This table holds the image to expand the division for tables" style="width: 220px;
            height: 21px">
            <tr>
                <td title="Tables" scope="col" style="width: 200px">
                    <label for="imgTables" title="Tables" class="leftLabelGP">
                        <strong>Tables</strong></label>
                </td>
                <td style="width: 16px" align="right">
                    <img id="imgTables" alt="Exapand" src="../images/ArrowDown.gif" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divTable" title="Tables" style="overflow: hidden; display: none;">
        <table summary="This table holds the division to add Tables" style="width: 220px;
            height: 21px">
            <tr>
                <td scope="col" style="width: 211px;">
                    <input type="button" title="Click to add tables" value="Add Tables" class="button"
                        id="btnAddtables" onclick="javascript:getTable();" style="width: 205px; font-weight: bold;
                        background-color: #ff9999;" />
                    <div id="divTables" title="Tables" style="width: 204px; overflow: auto; height: 298px;">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="div1" title="Design Report" style="cursor: pointer; background-color: #559585;
        color: White;" onclick="toggleDiv('divReport', 'imgReport',80)">
        <table summary="This table holds the image to expand or collapse the division" style="width: 220px;
            height: 21px">
            <tr>
                <td scope="col" style="width: 190px">
                    <label for="" title="Design Report" class="leftLabelGP">
                        <strong>Design Report</strong></label>
                </td>
                <td title="Expand" scope="col" align="right">
                    <img id="imgReport" alt="Exapnad" src="../images/ArrowDown.gif" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divReport" title="Design Report" style="overflow: hidden; display: none;">
        <table summary="This table actually holds the report design options" style="width: 220px;
            height: 21px">
            <tr>
                <td title="Design Header" scope="col" style="width: 205px">
                    <input type="button" style="border: 0; height: 22; width: 205px;" id="btnHeader"
                        value="Header" class="buttonRD" onclick="return btnHeader_onclick()" title="Design Header" />
                </td>
            </tr>
            <tr>
                <td style="width: 205px; height: 16px;" scope="col" title="Design Details Pane">
                    <input type="button" style="border: 0; height: 22; width: 205px;" id="btnDetails"
                        value="Details Pane" class="buttonRD" language="javascript" onclick="return btnDetails_onclick()"
                        title="Design Details Pane" />
                </td>
            </tr>
            <tr>
                <td style="width: 205px" scope="col" title="Design Footer">
                    <input type="button" style="border: 0; height: 22; width: 205px;" id="btnFooter"
                        value="Footer" class="buttonRD" language="javascript" onclick="return btnFooter_onclick()"
                        title="Design Footer" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divOutersetdata" title="Set Data" style="cursor: pointer; background-color: #5a9e8d;
        color: White;" onclick="toggleDiv('divSetdata', 'imgSetdata',132)">
        <table style="width: 220px; height: 21px" summary="This table holds the image used to expand or collapse the division to set data">
            <tr>
                <td style="width: 190px" title="Set Data" scope="col">
                    <label for="" title="Set Data" class="leftLabelGP">
                        <strong>Set Clauses</strong></label>
                </td>
                <td align="right" title="Set Data" scope="col">
                    <img id="imgSetdata" alt="Expand/Collapse" title="Expand/Collapse" src="../images/ArrowDown.gif" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divSetdata" style="overflow: hidden; display: none;">
        <table id="tblDetailsbtn" style="width: 220px; height: 21px" summary="This table holds the division of the options to Set Data">
            <tr>
                <td scope="col" title="Set Where clause">
                    <input type="button" style="border: 0; height: 22; width: 205px;" id="btnWhere" value="Where"
                        class="buttonRD" title="Set Where clause" language="javascript" onclick="return btnWhere_onclick()" />
                </td>
            </tr>
            <tr>
                <td scope="col" title="Set Formula">
                    <input type="button" style="border: 0; height: 22; width: 205px;" id="btnFormula"
                        value="Formula" class="buttonRD" title="Set Formula" language="javascript" onclick="return btnFormula_onclick()" />
                </td>
            </tr>
            <tr>
                <td scope="col" title="Set Having clause">
                    <input type="button" style="border: 0; height: 22; width: 205px;" id="btnHaving"
                        value="Having" class="buttonRD" title="Set Having clause" language="javascript"
                        onclick="return btnHaving_onclick()" />
                </td>
            </tr>
            <tr>
                <td scope="col" title="Set Order by clause">
                    <input type="button" style="border: 0; height: 22; width: 205px;" id="btnOrderby"
                        value="Order By" class="buttonRD" title="Set Order by clause" language="javascript"
                        onclick="return btnOrderby_onclick()" />
                </td>
            </tr>
            <tr>
                <td scope="col" title="Set Group by clause">
                    <%--<img id="imgSigma" style="height:22px;" alt="Group By" src="../images/sigma.bmp" onclick="return btnGroupby_onclick()" />--%>
                    <input type="button" style="border: 0; height: 22; width: 205px;" id="btnGroupby"
                        value="Group By" class="buttonRD" title="Set Group by clause" language="javascript"
                        onclick="return btnGroupby_onclick()" />
                </td>
            </tr>
            <%--<tr>
                                <td scope="col" title="Set Color condition" >
                                    <input type="button" style="border:0; height:22; width: 205px;" id="btnColorcondition" value="Color Condition" class="button" title="Set Color condition" language="javascript" onclick="return btnColorcondition_onclick()" />
                                </td>
                            </tr>--%>
        </table>
    </div>
    <div id="divUtertoolbox" title="ToolBox" style="cursor: pointer; background-color: #76b1a3;
        color: White;" onclick="toggleDiv('divToolbox', 'imgToolbox',26)">
        <table style="width: 220px; height: 21px" summary="table">
            <tr>
                <td style="width: 190px" title="ToolBox" scope="col">
                    <label for="" class="leftLabelGP" title="ToolBox">
                        <strong>ToolBox</strong></label>
                </td>
                <td align="right" title="Expand/Collapse" scope="col">
                    <img id="imgToolbox" alt="Expand/Collapse" title="Expand/Collapse" src="../images/ArrowDown.gif" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divToolbox" style="overflow: hidden; display: none;">
        <table style="width: 220px; height: 21px" summary="table">
            <tr>
                <td style="width: 205px" scope="col">
                    <input type="button" style="border: 0; height: 22; width: 205px;" id="btnLabel" value="Label"
                        onclick="return genLabel();" class="buttonRD" title="Label" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divOuterformat" title="Format" style="cursor: pointer; background-color: #97c4ba;
        color: White;" onclick="toggleDiv('divFormat', 'imgFormat',270)">
        <table style="width: 220px; height: 21px" summary="This table holds the image to expand or collapse the division of format box">
            <tr>
                <td style="width: 190px" scope="col" title="Format">
                    <label for="" class="leftLabelGP">
                        <strong>Format</strong></label>
                </td>
                <td align="right" scope="col">
                    <img id="imgFormat" title="Expand/Collapse" alt="Expand/Collapse" src="../images/ArrowDown.gif" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divFormat" style="overflow: hidden; display: none;">
        <table summary="This table holds the division of format box" style="width: 220px;
            height: 21px">
            <tr>
                <td style="width: 204px;" scope="col">
                    <table summary="This table holds the division of format box">
                        <tr>
                            <td colspan="2" scope="col" title="Select Constituent" style="width: 211px">
                                <label for="ctl00_LeftPlaceHolder_ddlConstituent">
                                </label>
                                <select title="Select Constituent" id="ddlConstituent" class="leftdropdownlist" style="width: 206px;">
                                    <option value="Whole Report">Whole Report</option>
                                    <option value="Header">Header</option>
                                    <option value="Details Pane">Details Pane</option>
                                    <option value="Footer">Footer</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" scope="col" title="Select font-size" style="width: 211px">
                                <label for="ctl00_LeftPlaceHolder_ddlFontsize">
                                </label>
                                <asp:DropDownList ID="ddlFontsize" CssClass="leftdropdownlist" runat="server" Width="206px">
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem>14</asp:ListItem>
                                    <asp:ListItem>16</asp:ListItem>
                                    <asp:ListItem>18</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>24</asp:ListItem>
                                    <asp:ListItem>28</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>36</asp:ListItem>
                                    <asp:ListItem>40</asp:ListItem>
                                    <asp:ListItem>56</asp:ListItem>
                                    <asp:ListItem>72</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" scope="col" title="Select font-family" style="width: 211px">
                                <label for="ctl00_LeftPlaceHolder_ddlFontfamily">
                                </label>
                                <asp:DropDownList ID="ddlFontfamily" CssClass="leftdropdownlist" Width="206px" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 211px; padding: 0px" scope="col" title="Set text appearance" colspan="2">
                                <table style="background-color: #cccccc; color: #ffffff;" summary="table">
                                    <tr>
                                        <td scope="col" title="Bold" valign="top" style="color: #ffffff; height: 7px">
                                            <input id="chkBold" title="Bold" type="checkbox" style="width: 25px; height: 20px;" /><span
                                                style="font-family: Arial Black; font-size: 10pt;"><strong>B</strong></span>
                                        </td>
                                        <td valign="top" scope="col" style="color: #ffffff; height: 7px" title="Italic">
                                            <input id="chkItalic" title="Italic" type="checkbox" style="width: 23px; font-size: 10pt;" /><em><span
                                                style="font-family: Arial Black; font-size: 10pt">I</span></em>
                                        </td>
                                        <td valign="top" scope="col" style="color: #ffffff; height: 7px" title="Underline">
                                            <input id="chkUnderline" title="Underline" type="checkbox" style="width: 23px" /><span
                                                style="font-family: Arial Black; text-decoration: underline; font-size: 10pt">U</span>
                                        </td>
                                        <td valign="bottom" scope="col" style="height: 7px">
                                            <input id="sample_1" style="width: 12px; height: 14px; border: none; background-color: #000000;"
                                                title="Fore Color" type="text" value="" readonly="readOnly" />
                                        </td>
                                        <td style="height: 7px" scope="col">
                                            <img id="imgForecolor" src="../images/frcolor1.jpg" title="Pick Fore Color" alt="Pick Fore Color"
                                                onclick="pickerPopup202('hidForecolor','sample_1');" language="javascript" style="width: 18px;
                                                height: 18px;" />
                                        </td>
                                        <td valign="bottom" style="height: 7px" scope="col">
                                            <input id="sample_2" style="width: 12px; height: 14px; border: none;" title="Back Color"
                                                type="text" value="" readonly="readOnly" />
                                        </td>
                                        <td style="height: 7px" scope="col">
                                            <img id="imgBackcolor" src="../images/bkcolor1.jpg" title="Pick Back Color" alt="Pick Back Color"
                                                onclick="pickerPopup202('hidBackcolor','sample_2');" style="width: 18px; height: 18px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" title="Format" scope="col" style="width: 211px">
                                <input type="button" title="Click to save formatting" id="btnDone" class="button"
                                    value="Done" language="javascript" onclick="return btnDone_onclick()" style="width: 96px" />
                                <input type="button" id="btnRemove" class="button" title="Click to remove formatting"
                                    value="Remove" language="javascript" onclick="return btnRemove_onclick()" style="width: 98px;" /><%--<asp:button ID="btnDone" runat="server" CssClass="button" Text="Done" Width="75px"/>--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
    <table summary="This table holds the Command Buttons">
        <tr>
            <td title="Report" scope="col" style="height: 24px">
                <div>
                    &nbsp;<input id="btnReset" type="button" class="button" value="Reset" title="Reset"
                        language="javascript" onclick="return btnReset_onclick()" onclick="return btnReset_onclick()" onclick="return btnReset_onclick()" />
                    <input type="button" class="button" id="btnSaverep" value="Save Report" title="Save Report"
                        onclick="return saveReport()" />
                    <input type="button" class="button" id="btnOpenrep" title="Open Report" value="Open Report"
                        onclick="return openReport()" />
                    <%--<input type="button" class="button" id="btnSharerep" value="Share Report" title="share Report"
                        onclick="return shareReport()" />--%>
                    <%-- <asp:Button id="btnGraphs" runat="server" cssclass="button" Text="Graphs"/>--%>
                    <%--<input type="button" runat="server" class="button" id="btnGraphs" value="Graphs" title="Graphs" />--%>
                    <input runat="server" type="button" class="button" id="Button1" value="Graphs" title="Graphs" language="javascript"
                        onclick="return btnGraphs_onclick()" />
                    <input id="btnArchive" type="button" class="button" value="Archive Report" title="Archive Report"
                        language="javascript" onclick="return btnArchive_onclick()" onclick="return btnArchive_onclick()" />
                    <input id="uploadtemptab" type="button" runat="server" visible="false" class="button" value="Upload Temp Table" title="Upload Temp Table"
                       onclick="window.open('\Uploadtemptable.aspx')" language="javascript"/>
                </div>
            </td>
        </tr>
    </table>
    <table style="width: 750px; height: 20px;" summary="table">
        <tr>
            <td style="width: 565px; color: Black" scope="col" id="tdRep">
                <label for="" id="lblCaption" title="Design Report" class="label">
                    Add Labels, Tablecolumns and apply formula upon them</label>
            </td>
            <td scope="col">
                <img id="imgRep" src="../images/det.jpg" title="Design Report" alt="Design Report"
                    align="right" />
            </td>
        </tr>
    </table>
    <table id="divOuterReport" summary="This table holds the report consitutents">
        <tr>
            <td scope="col" title="Design Report" style="font-style: normal; text-decoration: none;
                width: 750px;">
                <div id="divHeader" title="Design Header" style="display: none; height: 350px; width: 750px;
                    border: solid 1px black; overflow: auto; background-image: url(../images/header.png);
                    background-repeat: no-repeat;">
                </div>
                <div id="hspl" title="Resize Header" style="display: none; width: 750px; height: 1px;">
                    <vwdcms:splitterbar ToolTip="Resize Header" runat="server" ID="headSplitter" Orientation="horizontal"
                        TopResizeTargets="divHeader" BackgroundColor="#4493a0" BackgroundColorLimit="firebrick"
                        BackgroundColorHilite="#99cccc" BackgroundColorResizing="#ffccff" SaveHeightToElement="txtHeight"
                        OnResize="splitterOnResize" OnResizeComplete="splitterComplete" Style="background-image: url(images/hsplitter.gif);
                        background-position: center center; background-repeat: no-repeat; position: absolute;
                        overflow: auto;" />
                </div>
                <div title="Design Details Pane" id="divDetails" style="height: auto; width: 750px;
                    border: solid 1px black; overflow: visible; background-image: url(../images/details.png);
                    background-repeat: no-repeat;">
                </div>
                <div id="divFooter" title="Design Footer" style="height: 350px; display: none; width: 750px;
                    border: solid 1px black; overflow: auto; background-image: url(../images/footer.png);
                    background-repeat: no-repeat;">
                </div>
                <div id="spl" title="Resize Footer" style="width: 750px; display: none; height: 1px;">
                    <vwdcms:splitterbar ToolTip="Resize Footer" runat="server" ID="footerSpliter" Orientation="horizontal"
                        TopResizeTargets="divFooter" BackgroundColor="#4493a0" BackgroundColorLimit="firebrick"
                        BackgroundColorHilite="#99cccc" BackgroundColorResizing="#ffccff" SaveHeightToElement="txtHeight"
                        OnResize="splitterOnResize" OnResizeComplete="splitterComplete" 
                        Style="background-image: url(images/hsplitter.gif);
                        background-position: center center; background-repeat: no-repeat; position: absolute" />
                </div>
                <div style="background-color: #91B0C2; color: White;">
                    <table style="width: 753px;" summary="table">
                        <tr>
                            <%-- <td style="width: 109px">
				                            
				                        </td>--%>
                            <td style="width: 115px" scope="col">
                                <input class="button" type="button" id="btnProcessrep" value="Process Report"
                                    title="Process Report" onclick="return btnProcessrep_onclick()"/>
                            </td>
                            <td style="width: 502px" scope="col">
                                <input type="button" class="button" id="btnSavereport2" value="Save Report" title="Save Report"
                                    onclick="return saveReport()"/>
                                <%--<input type="button" class="button" id="btnSetalert" value="Set Alert" title="Set Alert"
                                     onclick="return btnSetalert_onclick()" onclick="return btnSetalert_onclick()" />--%>
                                <input type="button" class="button" id="btnDefaultgraph" value="Default Graph" title="Default Graph"
                                     onclick="return btnDefaultgraph_onclick()" />
                                 <input id="uploadtable" runat="server" visible="false" type="button" value="Upload Table"  onclick="window.open('\Uploadtable.aspx')" 
                                      class="button" />
                            </td>
                            <td style="height: 27px; width: 97px;" align="right" scope="col">
                                <label for="" style="color: White; font-size: 8pt" title="Summarized">
                                    Summarized</label>
                            </td>
                            <td style="height: 27px; width: 19px;" scope="col">
                                <input type="checkbox" id="chkSummarized" title="Summarized" name="chkSummarized"
                                    class="checkbox" onclick="return chkSummarized_onclick()" />
                            </td>
                            <td style="height: 27px; width: 100px;" align="right" scope="col">
                                <label for="" style="color: White; font-size: 8pt" title="New window">
                                    New Window</label>
                            </td>
                            <td style="height: 27px; width: 35px;" scope="col">
                                <input type="checkbox" id="chkNewwindow" title="New window" name="chkNewwindow" class="checkbox" />
                            </td>
                        </tr>
                    </table>
                    <table summary="table">
                        <tr>
                            <td style="width: 85px; height: 26px;" valign="top" scope="col">
                                <label title="Start Date" style="color: White; font-size: 8pt" for="txtStartdate">
                                    Start Date:</label>
                            </td>
                            <td style="width: 158px; height: 26px;" valign="top" scope="col">
                                <input type="text" id="txtStartdate" name="txtStartdate" class="text" />
                            </td>
                            <td style="width: 40px; height: 26px;" valign="top" scope="col">
                                <img alt="Start Date" id="imgStartDate" onclick="ShowCalendar('txtStartdate');" src="../images/Calendar.gif"
                                    style="width: 17px; height: 21px" title="Click to select date" /></td>
                            <td style="width: 85px; height: 26px;" valign="top" scope="col">
                                <label title="Start Date" style="color: White; font-size: 8pt" for="txtEnddate">
                                    End Date:</label>
                            </td>
                            <td style="width: 158px; height: 26px;" valign="top" scope="col">
                                <input type="text" id="txtEnddate" name="txtEnddate" class="text" />
                            </td>
                            <td style="width: 39px; height: 26px;" valign="top" scope="col">
                                <img alt="End Date" id="imgEnddate" onclick="ShowCalendar('txtEnddate');" src="../images/Calendar.gif"
                                    style="width: 17px; height: 21px" title="Clieck to select date" /></td>
                        </tr>
                    </table>
                </div>
                <iframe id="FRMRESULST" title="Report Output" style="visibility: visible; width: 100%;
                    height: 400px; background-color: white; margin: 0px" name="FRMRESULST" frameborder="no"
                    scrolling="auto"></iframe>
            </td>
        </tr>
    </table>
    <div id="divContext" title="Delete" onmouseout="return hideMenu();" onclick="delMe();"
        class="buttonMenu" style="color: #ffffff; border: outset 1px black; background-color: red;
        width: 100px; height: 20px; display: none; position: absolute; text-align: center;
        border-top-width: 0px; border-left-width: 0px; border-bottom-width: 0px; cursor: hand;
        border-right-width: 0px; z-index: 101;">
        <img src="../images/Trash90.jpg" alt="trash" />
    </div>
    <%-- hidden fields for report --%>
    <input type="hidden" name="hidForecolor" id="hidForecolor" /><%-- hidden report/constituent forcolor  --%>
    <input type="hidden" name="hidBackcolor" id="hidBackcolor" /><%-- hidden report/constituent backcolor --%>
    <input type="hidden" id="hidTables" name="hidTables" /><%-- hidden all tablename --%>
    <input type="hidden" id="hidTblall" name="hidTblall" /><%-- hidden final tablename --%>
    <input type="hidden" id="hidDatetable" name="hidDatetable" /><%-- dat condition table of the opened report --%>
    <input type="hidden" name="hidReportname" id="hidReportname" /><%--  scope of the opened report --%>
    <input type="hidden" name="hidRecordid" id="hidRecordid" /><%--  recordid of the opened report --%>
    <input type="hidden" name="hidReportscope" value="Non Local" id="hidReportscope" /><%--  scope of the opened report --%>
    <input type="hidden" name="hidReporttype" value="Simple" id="hidReporttype" /><%--  type of the opened report --%>
    <input type="hidden" name="hidReportmode" value="new" id="hidReportmode" /><%--  mode of the opened report --%>
    <input type="hidden" name="divType" value="details" id="divType" /><%-- report constitute currently under construction --%>
    <input type="hidden" name="hidAlllabels" id="hidAlllabels" /><%-- dynamic label collection --%>
    <input type="hidden" name="hidDepartment" id="hidDepartment" /><%-- hidden saved report department --%>
    <input type="hidden" name="hidClient" id="hidClient" /><%-- hidden saved report client --%>
    <input type="hidden" name="hidLob" id="hidLob" /><%-- hidden saved report lob --%>
    <%-- hidden fields for report header formatting --%>
    <input type="hidden" name="headerBackimage" id="headerBackimage" /><%-- hidden header backimage --%>
    <input type="hidden" name="hidHpos" id="hidHpos" /><%-- hidden header elements position--%>
    <input type="hidden" name="hidHeaderformat" id="hidHeaderformat" /><%-- hidden basic formatting of header --%>
    <%-- ends --%>
    <%-- hidden fields for report details formatting --%>
    <input type="hidden" name="detailsBackimage" id="detailsBackimage" /><%-- hidden details backimage --%>
    <input type="hidden" name="hidDpos" id="hidDpos" /><%-- hidden details elements position --%>
    <input type="hidden" name="hidDetailsformat" id="hidDetailsformat" /><%-- hidden basic formatting of details pane --%>
    <%-- ends --%>
    <%-- hidden fields for report footer formatting --%>
    <input type="hidden" name="footerBackimage" id="footerBackimage" /><%-- hidden footer backimage --%>
    <input type="hidden" name="hidFpos" id="hidFpos" /><%-- hidden footer elements position --%>
    <input type="hidden" name="hidFooterformat" id="hidFooterformat" /><%-- hidden basic formatting of footer --%>
    <%-- ends --%>
    <%-- Hidden Fields for dynamic obects --%>
    <input type="hidden" name="lblCount" value="0" id="lblCount" /><%-- hidden Labael count to generate unique id for labels --%>
    <input type="hidden" name="btnCount" value="0" id="btnCount" /><%-- hidden Button count to generate unique id for columnbutton --%>
    <input type="hidden" name="hidHeight" id="hidHeight" />
    <%--- To store heights of header and footer --%>
    <input type="hidden" name="hidFformat" id="hidFformat" />
    <%--- To store format of all footer objects --%>
    <input type="hidden" name="hidDformat" id="hidDformat" />
    <%--- To store format of all details objects --%>
    <input type="hidden" name="hidHformat" id="hidHformat" />
    <%--- To store format of all header objects --%>
    <input type="hidden" name="hidHformula" id="hidHformula" />
    <%--- To store formula on dynamic objects --%>
    <input type="hidden" name="hidDformula" id="hidDformula" />
    <%--- To store formula on dynamic objects --%>
    <input type="hidden" name="hidFformula" id="hidFformula" />
    <%--- To store formula on dynamic objects --%>
    <input type="hidden" name="hidHcolorcon" id="hidHcolorcon" />
    <%--- To store color condition on dynamic objects of header --%>
    <input type="hidden" name="hidFcolorcon" id="hidFcolorcon" />
    <%--- To store color condition on dynamic objects of footer--%>
    <%-- ends --%>
    <%-- Hidden Fields for SQL Query --%>
    <input type="hidden" name="hidStartdate" id="hidStartdate" /><%-- hidden start date--%>
    <input type="hidden" name="hidEnddate" id="hidEnddate" /><%-- hidden end date--%>
    <input type="hidden" name="hidWhere" id="hidWhere" /><%-- hidden where condition--%>
    <input type="hidden" name="hidGroupby" id="hidGroupby" /><%-- hidden group by condition--%>
    <input type="hidden" name="hidJoin" id="hidJoin" /><%-- hidden join condition--%>
    <input type="hidden" name="hidFormula" id="hidFormula" /><%-- hidden formula condition--%>
    <input type="hidden" name="hidFormulaname" id="hidFormulaname" /><%-- hidden formula name--%>
    <input type="hidden" name="hidOrderby" id="hidOrderby" /><%-- hidden orderby condition--%>
    <input type="hidden" name="hidHaving" id="hidHaving" /><%-- hidden having condition--%>
    <input type="hidden" name="hidColorcondition" id="hidColorcondition" /><%-- hidden color condition on details pane elements--%>
    <input type="hidden" name="hidDcolcon" id="hidDcolcon" /><%-- hidden color condition on details pane elements--%>
    <input type="hidden" name="hidDformatid" id="hidDformatid" /><%-- hidden details pane formatted object id--%>
    <input type="hidden" name="hidFormulacnt" id="hidFormulacnt" /><%-- hidden details pane formula name counter --%>
    <input type="hidden" name="btnEx" id="btnEx" value="aa&#013" /><%-- logical requirement --%>
    <input type="hidden" name="btnEx2" id="btnEx2" value="a&#013" /><%-- logical requirement --%>
    <input type="button" name="btnEx1" style="visibility: hidden;" id="btnEx1" /><%-- logical requirement --%>
    <input type="hidden" name="hidSubtotal" id="hidSubtotal" />
    <%-- hidden show total --%>
    <%-- ends --%>

    <script type="text/javascript" language="javascript">

        //     if('<%=Session("typeofuser")%>'=="Super Admin")
        //        {
        //            document.getElementById("btnSaverep").disabled=true;
        //            document.getElementById("btnSavereport2").disabled=true;
        //            document.getElementById("btnSharerep").disabled=true;
        //            document.getElementById("btnArchive").disabled=true;
        //        }

        function chkSummarized_onclick() {
            if (document.getElementById("chkSummarized").checked == true) {
                document.getElementById("hidReporttype").value = "Summarized"
            }
            else {
                document.getElementById("hidReporttype").value = "Simple"
            }
        }


//        function btnSetalert_onclick() {
//            if (document.getElementById("hidReportname").value != "") 
//            {
//                document.forms[0].target = "_self";
//                document.forms[0].action = "../MailsandAlerts/Escalation.aspx?val=70";
//                document.forms[0].submit();
//            }
//            else {
//                alert("Save The Report First.");
//            }

//        }


function Button1_onclick() {

}

    </script>
</asp:Content>

