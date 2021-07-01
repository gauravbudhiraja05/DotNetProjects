<%@ Page Language="VB" AutoEventWireup="false" CodeFile="whereClause.aspx.vb" Inherits="ReportDesigner_whereClause" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <title>Where</title>
    <link href="../App_Themes/Themes/StyleSheet.css" rel="stylesheet" />
    <script type="text/javascript">
     function onLoad()
        {
           if(window.opener.document.getElementById("hidWhere").value!="")  //reload value
           {
                document.getElementById("txtWhere").value=window.opener.document.getElementById("hidWhere").value;
           }
           var tbl=document.getElementById("hidTables").value;
           if(tbl!="")
           {
            var st=tbl.split("~");
            var i=0;
            var theSel=document.getElementById("selCol");
                for(i=0;i<=st.length-1;i++)
                {
                    ReportDesignerAjax.GetTableFields(st[i], pasteField);
                }
                 //Load existing formulas
                var exFor=window.opener.document.getElementById("hidDformula").value;
                if(exFor!="")
                {
                        var exFor1=exFor.split("~");
                        var i=0;
                      
                        var theSel=document.getElementById("selCol");
                        var cnt=theSel.length;
                        for(i=0;i<=exFor1.length-1;i++)
                        {
                            var nStr=exFor1[i].replace("$",".");
                            var newOpt1 = new Option(nStr, nStr);
                            theSel.options[cnt] = newOpt1;
                            cnt++;
                        }
                }
            //
           }
           else
           {
            alert("No columnname found. Add columns first");
           }
            
        }
         function pasteField(Response)       // Get columns
        {
            if (Response!="")
            {
                var res=Response.value;
                var resNew=res.split("~"); // Split all the columns
                var res2=resNew[0].split("$"); // Split to get the table name
                var i=0;
                var theSel=document.getElementById("selCol");
                var cnt=theSel.length;
                for(i=0;i<=resNew.length-1;i++)
                {
                    var nStr=resNew[i].replace("$",".");
                    var newOpt1 = new Option(nStr, nStr);
                    theSel.options[cnt] = newOpt1;
                    cnt++;
                }
             }
         }  
        
   // to insert text at caret position

  function insertText1() {
     
      var i=document.getElementById("selCol").selectedIndex;
        if (document.selection) {
            document.getElementById("txtWhere").focus();
            sel = document.selection.createRange();
            var d=document.getElementById("selCol").options[i].text;
            var d1=d.split(" AS ");
            if(d1.length>1)
            {
                sel.text = d1[1];
            }
            else
            {
               sel.text = d1[0];
               // sel.text = " "+document.getElementById("selCol").options[i].text+" ";
            }
        }         
    }
    function insertText2() {
     
      var i=document.getElementById("selCond").selectedIndex;
        if (document.selection) {
            document.getElementById("txtWhere").focus();
            sel = document.selection.createRange();
            if(document.getElementById("selCond").value=="greater than equal to")
            {
                sel.text = " >= ";
            }
            else if(document.getElementById("selCond").value=="between" || document.getElementById("selCond").value=="not between")
            {
                sel.text = " "+document.getElementById("selCond").value+" ";
            } 
            else
            {
                sel.text = " "+document.getElementById("selCond").value+" ";
            }    
        }         
    }
    function insertText3() {
     
      var i=document.getElementById("selVal").selectedIndex;
        if (document.selection) {
            document.getElementById("txtWhere").focus();
            sel = document.selection.createRange();
            sel.text = " '"+document.getElementById("selVal").options[i].text+"' ";
        }         
    }
    function fillValue()
    {
         
          var d=document.getElementById("selCol").value;
            var d1=d.split(" AS ");
            if(d1.length>1)
            {
                    var cmd ="select "+ d1[0]+" from "+document.getElementById("hidTables").value;
                    // execute query
                    ReportDesignerAjax.GetFormula(cmd,getFormula);
            }
            else
            {
                  var column=document.getElementById("selCol").value;
                  ReportDesignerAjax.GetColumnValue(column, getColumn);
            }
    }
    function getColumn(Response)  // fill column value to selValue
        {
            if(Response.value!="")
            {
               var ds = Response.value;
               document.getElementById("selVal").length=0;
				if(ds!= null && typeof(ds) == "object" && ds.Tables!=null)
				{
				  for(i=0;i<ds.Tables[0].Rows.length;i++)
				    {
					    document.getElementById("selVal").options[i]=new Option(ds.Tables[0].Rows[i].ColumnName,ds.Tables[0].Rows[i].ColumnName);
				    }
			    }
            }
        }
        function getFormula(Response)  // fill column value to selValue
        {
            if(Response.value!="")
            {
               var ds = Response.value;
               document.getElementById("selVal").length=0;
			   document.getElementById("selVal").options[0]=new Option(ds,ds);
				
            }
        }
        function btnSetformula_onclick() 
        {
          
            if(document.getElementById("txtWhere").value!="" || document.getElementById("txtWhere").value!=" ")
            {
                ReportDesignerAjax.swapFormula(document.getElementById("txtWhere").value,window.opener.document.getElementById("hidDformula").value,finalFormula);
                 alert("Where Clause Has Been Set.");
            }
            else
            {
                alert("Where Clause Is Empty");
            }
        }
        function finalFormula(response)
        {
            window.opener.Where(response.value+"`"+document.getElementById("hidTables").value);
//            window.parent.focus();
//            window.close();
         }
          function btnCloseformula_onclick() {
            window.parent.focus();
            window.close();
        }

        function addMorecolumns() {
            window.open("getTables.aspx?src=where","AddColumns","width=450,height=600,scrollbars=yes,status=yes");
        }
        function addColumns(cols)
        {
        
            // Prepare Final tables            
            var extbl=document.getElementById("hidTables").value;  // existing tables
            if(extbl!="")
            {
                var ntbl=cols.split("~"); //newTables
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
                            document.getElementById("hidTables").value=document.getElementById("hidTables").value+"~"+ntbl[j];
                        }
                    }
                }
            }
            else
            {
              document.getElementById("hidTables").value=cols;  
            }
            // ends
            var tb=cols.split("~");
            var i=0;
             for(i=0;i<=tb.length-1;i++)
            {
                ReportDesignerAjax.GetTableFields(tb[i], pasteField);
            }
        }

    function btnRemove_onclick() 
    {
        if(document.getElementById("txtWhere").value=="" || document.getElementById("txtWhere").value==" ")
        {
            alert("No Where Clause Found");
        }
        else
        {
            alert("Where clause is removed");
        }
        document.getElementById("txtWhere").value="";
        window.opener.document.getElementById("hidWhere").value="";
    }
  
        function setFormula(op)
        {
            //document.getElementById("txtWhere").value=document.getElementById("txtWhere").value+ op ;
            if (document.selection) {
                document.getElementById("txtWhere").focus();
                sel = document.selection.createRange();
                sel.text = op;
            }
        }
    function btnAnd_onclick() {
    //document.getElementById("txtWhere").value=document.getElementById("txtWhere").value+ " " + "AND" + " " ;
            if (document.selection) {
                document.getElementById("txtWhere").focus();
                sel = document.selection.createRange();
                sel.text = " " + "AND" + " " ;
            }
    }

    function btnOr_onclick() {
    //document.getElementById("txtWhere").value=document.getElementById("txtWhere").value+ " " + "OR" + " " ;
             if (document.selection) {
                document.getElementById("txtWhere").focus();
                sel = document.selection.createRange();
                sel.text = " " + "OR" + " " ;
            }
    }

    </script>
</head>
<body onload="onLoad();" style="background-color: aliceblue">
    <form id="frmWhere" runat="server">
    <div>
        <table style="width: 424px; height: 232px; ">
        <caption style ="background-color:#0591D3">Where</caption>
        <tr>           
               <td colspan="3" scope ="colgroup" >
                    <table style="border:solid 1px grey" summary ="table">                        
                        <tr>                            
                            <td style="width: 64px" scope ="col" >
                                <input id="btnJoin1" class="button" language="javascript" onclick="setFormula(' *= ')"
                                    style="width: 56px;" title="Click to add an operator" type="button"
                                    value="*=" />
                            </td>
                            <td scope ="col" >
                                <span style="color: #ff3333">  
                                 : is used to include all records from 'First' table andonly those records from 'Second' table where the joined fields from both tables are equal.
                                 
                                </span>
                            </td>
                        </tr>
                        
                         <tr>
                            <td style="width: 64px" scope ="col" >
                                 <input id="btnJoin2" class="button" language="javascript" onclick="setFormula(' =* ')"
                                    style="width: 56px;" title="Click to add an operator" type="button"
                                    value="=*" />
                            </td>
                            <td scope ="col" >
                                <span style="color: #ff3300">
                                 : is used to include all records from 'Second' table and only thoserecords from 'First' table where the joined fields from both tables are equal. </span>
                            </td>
                            </tr>
                            <tr>
                             <td colspan="2"  align="center" scope="colgroup"  >
                <input id="btnPlus" class="button"  onclick="setFormula('+')"
                    style="width: 56px;" title="Click to add an operator" type="button"
                    value="+" /><input id="btnMinus" class="button"  onclick="setFormula('-')"
                        style="width: 56px;" title="Click to add an operator" type="button"
                        value="-" /><input id="btnMultiply" class="button"  onclick="setFormula('*')"
                            style="width: 56px;" title="Click to add an operator" type="button"
                            value="*" /><input id="btnDivide" class="button" language="javascript" onclick="setFormula('/')"
                                style="width: 56px;" title="Click to add an operator" type="button"
                                value="/" />
                <input id="btnOpen" class="button" language="javascript" onclick="setFormula('(')"
                                    style="width: 56px;" title="Click to add an operator" type="button"
                                    value="(" /><input id="btnClose" class="button" language="javascript" onclick="setFormula(')')"
                                        style="width: 56px;" title="Click to add an operator" type="button"
                                        value=")" /><input id="btnAnd" class="button"  title="AND" type="button" value="AND" style="width: 56px" onclick="return btnAnd_onclick()" /><input id="btnOr" class="button"   title="OR" type="button" value="OR" style="width: 56px; height: 22px" onclick="return btnOr_onclick()" /></td>
                        </tr>
                    </table>
               </td>
        </tr>
            <tr>
                <td colspan="3" scope ="colgroup" >
                    <textarea id="txtWhere" rows="*,*" cols="*,*" style="width: 768px; height: 184px" class="textBox"></textarea>
                </td>
            </tr>
            <tr>
                <td class="CaptionTR" style="height: 20px; background-color:#0591D3" scope ="col" >
                    <label title="Select Columns" for="txtWhere">Select Columns</label>
                                
                </td>
                <td style="width: 252px; height: 20px;background-color:#0591D3"  class="CaptionTR" scope ="col" >
                    Select Condition
                    
                </td>
                <td  class="CaptionTR" style="height: 20px;background-color:#0591D3" scope ="col" >
                  <label title="Select Value" for="selCol">Select Value</label>                          
                </td>
            </tr>
            <tr>
                <td scope ="col" >
                    <select id="selCol" style="width: 280px" size="10" onchange="javascript:fillValue();" onclick="javascript:insertText1();"></select>                
                </td>
                <td style="width: 252px" scope ="col" >
                <label title="Select Value" for="selCond"></label>       
                    <select id="selCond" style="width: 208px" size="10" onclick="javascript:insertText2();">
                                    <option value="=">equals to</option>
                                    <option value="<>">not equal to</option>
                                    <option value=">">greater than</option>
                                    <option value="<">less than</option>
                                    <option value="greater than equal to">greater than equal to</option>
                                    <option value="<=">less than equal to</option>
                                    <option value="between">between</option>
                                    <option value="not between">not between</option>
                                    <option value="like">like</option>
                                    <option value="in">in</option>
                                    <option value="not in">not in</option>
                                    <option value="'@Date1@'">'@Date1@'</option>
                                    <option value="'@Date2@'">'@Date2@'</option>
                    </select>                
                </td>
                <td scope ="col" >
                 <label title="Select Value" for="selVal"></label>       
                    <select id="selVal" style="width: 280px" size="10" onclick="javascript:insertText3();"></select>                
                </td>
            </tr>
            <tr>
                <td colspan="3" scope ="colgroup"  style="height: 29px" align="center">
                    <input id="btnMorecolumns" class="button" language="javascript" onclick="return addMorecolumns()"
                        title="Clieck to add more columns" type="button" value="Add More Columns" />
                    <input id="btnSetformula" class="button" onclick="return btnSetformula_onclick()" style="width: 99px" title="Click to set where clause" type="button" value="Done" />
                    <input id="btnRemove" class="button" onclick="return btnRemove_onclick()" style="width: 99px" title="Click to remove Where clause" type="button" value="Remove" />
                    <input id="btnCloseformula" class="button" onclick="return btnCloseformula_onclick()" style="width: 99px" title="Click to Close the window " type="button" value="Close Window" /></td>
            </tr>
            <tr>
                <td colspan="3" scope ="colgroup" >
                    <span style="font-size: 10pt; color: #ff3366"> NOTE: Double Click to add desired columns,
                        values and conditions to the textarea. To set date use date variables '@Date1@'
                        for start date and '@Date2@' for end date</span>
                </td>
            </tr>
        </table>
    </div>
        <br />
        <input type="hidden" id="hidTables" name="hidTables" runat="server" />
    </form>
</body>
</html>
