<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Having.aspx.vb" Inherits="ReportDesigner_Having" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en" >
<head runat="server">
    <title>Having</title>
    <link href="../App_Themes/Themes/StyleSheet.css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript">
        <!--
    function onLoad()
    {
        if(window.opener.document.getElementById("hidHaving").value!="")
        {
            document.getElementById("txtHaving").value=window.opener.document.getElementById("hidHaving").value;
        }
////        var strTblp=document.getElementById("hidTblname").value;         
////           if(strTblp!="")
////           {
////            addColumns(strTblp);     
////           }
////           else
////           {
////            alert("No columns found. Please add columns to set formula.")
////           }
           
           //Load existing formulas
                var exFor=window.opener.document.getElementById("hidDformula").value;
                if(exFor!="")
                {
                        var exFor1=exFor.split("~");
                        var i=0;
                      
                       var theSel=document.getElementById("selFormula");
                        var cnt=theSel.length;
                        for(i=0;i<=exFor1.length-1;i++)
                        {
                            var nStr=exFor1[i].replace("$",".");
                            var newOpt1 = new Option(nStr, nStr);
                            theSel.options[cnt] = newOpt1;
                            cnt++;
                        }
                }
                else
                {
                    alert("No Formula Found.")
                }
            //
    }
    function addColumns(tblName)
    {
          // prepare final tables
            
            var extbl=document.getElementById("hidTblname").value;  // existing tables
            var ntbl=tblName.split("~"); //newTables
            var j=0;
            var b=0;
            for(j=0;j<=ntbl.length-1;j++)
            {
                if(extbl!="")
                {
                    var sp=extbl.split("~");
                    var k=0;
                    for(k=0;k<=sp.length-1;k++)
                    {
                        if(ntbl[j]==sp[k])
                        {
                            b=1;        
                        }    
                    }
                    if(b==0)
                    {
                        document.getElementById("hidTblname").value=document.getElementById("hidTblname").value+"~"+ntbl[j];
                    }
                }
            }
         //ends
         var whrSel=document.getElementById("selWhere");
         if(whrSel.length==0)
         {
            var newOpt = new Option('Select Column','Select Column');
            whrSel.options[0] = newOpt;
         }
         var tb=tblName.split("~");
         for(i=0;i<=tb.length-1;i++)
         {
         
           ReportDesignerAjax.GetTableFields(tb[i], fillWhere);
         }
    }
    function selWhere_onclick()          //fetch values corresponding to column
        {
            if(document.getElementById("selWhere").value!="Select Column")
            {
                var column=document.getElementById("selWhere").value;
                ReportDesignerAjax.GetColumnValue(column, getColumn);
            }
        }
        function fillWhere(Response)  // fill column value to Where
        {
            if(Response.value!="")
            {
               var str=Response.value.split("~")
               var c=document.getElementById("selWhere").length;
             			
				    for(i=0;i<str.length;i++)
				    { 
				        var nStr=str[i].replace("$",".");
				        document.getElementById("selWhere").options[c]=new Option(nStr,nStr);
					    c=c+1;
				    }
			   
            }
        }
        function getColumn(Response)  // fill column value to selValue
        {
            if(Response.value!="" && Response.value!="Error")
            {
               var ds = Response.value;
               document.getElementById("txtValue").value=ds;
//               document.getElementById("selValue").length=0;
//				if(ds!= null && typeof(ds) == "object" && ds.Tables!=null)
//				{
//				
//				   	document.getElementById("selValue").options[0]=new Option("--Select--");
//				    for(i=0;i<ds.Tables[0].Rows.length;i++)
//				    {
//					    document.getElementById("selValue").options[i+1]=new Option(ds.Tables[0].Rows[i].ColumnName,ds.Tables[0].Rows[i].ColumnName);
//				    }
//			    }
            }
            else
            {
                alert("Aggregate Functions Such As Sum And Avg Cannot Be Performed On Varchar Datatype.");
            }
        }
        function btnMorecolumn_onclick()
        {
            window.open("getTables.aspx?src=having","AddColumns")
        } 
        function btnSethaving_onclick() // update the parent with the having clause and close the window
        {
            if(document.getElementById("txtHaving").value=="")
            {
                alert("No Having Clause Found")
            }
            else
            {
               
                window.opener.Having(document.getElementById("txtHaving").value+"`"+document.getElementById("hidTblname").value);
                alert("The Having Clause Has Been Set");
                window.parent.focus();
                window.close();
             }
        }
        function closeWindow()  // Close this window
        {
                window.parent.focus();
                window.self.close();
        }
        function btnOk_onclick() 
        {
            var having="";
            if(document.getElementById("selFormula").value!="Select Formula")
            {
                var nStr=document.getElementById("selFormula").value.split(" AS ");
                having=nStr[0];
                if(document.getElementById("selCond").value=="greater than equal to")
                {
                    cond=">=";
                }
                else
                {
                    cond=document.getElementById("selCond").value;
                }
                having=having+cond+"'"+document.getElementById("txtValue").value+"'";
                if(document.getElementById("txtHaving").value=="")
                {
                    document.getElementById("txtHaving").value=having;
                }
                else
                {
                    document.getElementById("txtHaving").value=document.getElementById("txtHaving").value+having;
                }
            }
            else
            {
                alert("Please Select Formula");
            }
            

      }

        function btnAnd_onclick() 
        {
            document.getElementById("txtHaving").value=document.getElementById("txtHaving").value+" and ";
        }

        function btnOr_onclick() 
        {
            document.getElementById("txtHaving").value=document.getElementById("txtHaving").value+" or ";
        }



        function btnRemove_onclick() 
        {
           if(document.getElementById("txtHaving").value=="" || document.getElementById("txtHaving").value==" ")
           {
                alert("No Having Clause Found")
           }            
            else
            {
                alert("The Having Clause Has Been Removed");
            }
            window.opener.document.getElementById("hidHaving").value="";
            document.getElementById("txtHaving").value="";
            
        }

        function selFormula_onclick() {
        
              if(document.getElementById("selFormula").value!="Select Formula")
                    {
                        var column=document.getElementById("selFormula").value;
                        var val=column.split(' AS ');
                        var tbl=document.getElementById("hidTblname").value;
                        tbl=tbl.replace("~",",");
                        var val1="Select "+val[0]+" from "+tbl;
                        ReportDesignerAjax.GetFormula1(val1, getColumn);
                    }
                    else
                    {
                        document.getElementById("txtValue").value="";
                    }
        }

    // -->
    </script>
</head>
<body onload="return onLoad();">
    <form id="frmHaving" runat="server">
    <div>
        <table >
            <caption style ="background-color:#0591D3">Having </caption>
            <tr>
                <td scope="col"  title="Formula" style="height: 27px; width: 82px; color : Black ">
                    <label for="selFormula" class="label" title="Formula">Formula:</label>
                </td>
                <td scope="col"  title="Select Formula" style ="color:black"   valign="top">
                                       <select id="selFormula" title="Select formula" style="width: 315px" class="dropdownlist" onclick="return selFormula_onclick()">
                                            <option value="Select Formula">Select Formula</option>                                            
                                    </select>
                                 </td>
            </tr>
          <%--  <tr>
                <tds cope="col"  title="Where" style="height: 27px; width: 82px;">
                                    <label for="selWhere" class="label" title="Column">
                                        Column:</label>
                                </td>
                                <td scope="col" colspan="2" title="Select Column" style="width: 321px; height: 27px;" valign="top" >
                                  <select id="selWhere" title="Select Column" style="width: 255px" class="dropdownlist" language="javascript" onclick="return selWhere_onclick()">
                                </select>
                                    <input id="btnMorecolumn" class="button" language="javascript" onclick="return btnMorecolumn_onclick()"
                                        style="width: 55px" title="Click to add more columns" type="button" value="More" /></td>
                                
                            </tr>--%>
                            
                            <tr>
                                
                                <td scope="col"  title="Select Condition" style="height: 27px; width: 82px; color : Black ">
                                    <label for="selCond" class="label" title="Select Condition">
                                        Condition:</label>
                                </td>
                                <td  colspan="2" scope ="colgroup" style="height: 27px; width: 321px; color : Black " valign="top">
                                <select id="selCond" title="Select Condition" style="width: 315px" class="dropdownlist">
                                    <option value="=">equals to</option>
                                    <option value="<>">not equal to</option>
                                    <option value=">">greater than</option>
                                    <option value="<">less than</option>
                                    <option value="greater than equal to">greater than equal to</option>
                                    <option value="<=">less than equal to</option>
                                </select>
                                </td>
                                
                            </tr>
                            <tr>
                                <td scope="col"  title="Select Value" style="height: 27px; width: 82px; color : Black ">
                                     <label for="selValue" class="label" title="Select Value"> Value:</label>
                                     <label for="txtValue" class="label" title="Select Value"></label>
                                </td>
                                <td  colspan="2" scope ="colgroup" style="height: 27px; width: 321px;" valign="top" title="Select Value">
                                <input value="" type="text" id="txtValue" class="textBox" style="width: 305px" />
                                <select  id="selValue" title="Select Value" style="width: 315px;display:none" class="dropdownlist">
                                </select>
                                </td>
                            </tr>
                            <tr>
                                <td scope="col"  title="Select Value" style="height: 27px; width: 82px; color : Black ">
                                    <label for="txtHaving" class="label" title="View"> View:</label>
                                </td>
                                <td>
                                    <table summary ="table">
                                        <tr>
                                            <td scope ="col">
                                                <textarea id="txtHaving" cols="" rows ="" class="textbox" title="Having Clause" style="width: 251px; height: 96px"></textarea>
                                            </td>
                                            <td valign="top" scope ="col" >
                                                <input id="btnOk" class="button" style="width: 55px" title="Click to View the Clause" type="button" value="Ok" language="javascript" onclick="return btnOk_onclick()" />
                                                <input id="btnAnd" class="button" style="width: 55px" title="And" type="button" value="And" language="javascript" onclick="return btnAnd_onclick()" />
                                                <input id="btnOr" class="button" style="width: 55px" title="Or" type="button" value="Or" language="javascript" onclick="return btnOr_onclick()" />
                                        </td>
                                            
                                        </tr>
                                    </table>
                                    
                                    </td>
                            </tr>
                            <tr>
                                <td style="width: 82px" scope ="col"></td>
                                <td  scope="col" title="Set Having Clause">
                                    <input type="button" class="button" value="Set Clause" id="btnSethaving" language="javascript" onclick="return btnSethaving_onclick()" title="Set the having clause" />
                                    <input type="button" class="button" value="Remove Clause" id="btnRemove" title="Remove the having clause" language="javascript" onclick="return btnRemove_onclick()" />
                                    <input id="btnClosetop" class="button" onclick="javascript:closeWindow();" style="width: 63px"
                                        title="Close" type="button" value="Close" />
                                </td>                                
                            </tr>
        </table>
    </div>
     <%-- hidden field declaration --%>
    <input type="hidden" runat="server" name="hidTblname" id="hidTblname" /> <%-- hidden tables --%>
    <%-- hidden field declaration ends --%>
    </form>
</body>
</html>
