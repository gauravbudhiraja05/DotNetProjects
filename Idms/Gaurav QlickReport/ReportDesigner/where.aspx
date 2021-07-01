<%--Project Name: IDMS Phase 2
    Module Name: Advance Report Designer
    Page Name: setData
    Summary: To where data for each a report. It is a component of the Module Report Designer.
             This component is to be used by the welcome page primarily to generate and update reports.
    Created on: 29/04/08
    Created By: Usha Sehokand

--%>


<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Where.aspx.vb" Inherits="ReportDesigner_Where" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <title>Where</title>
    <link href="../App_Themes/Themes/StyleSheet.css" rel="stylesheet" />
    <script language="javascript">
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
            var theSel=document.getElementById("selWhere");
            var newOpt1 = new Option("where", "where");
                    theSel.options[0] = newOpt1;
                for(i=0;i<=st.length-1;i++)
                {
                    ReportDesignerAjax.GetTableFields(st[i], pasteField);
                }
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
                var theSel=document.getElementById("selWhere");
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
        
        function btnAnd_onclick() {
            document.getElementById("txtWhere").value=document.getElementById("txtWhere").value+ " " + "and" + " " ;
        }

        function btnOr_onclick() {
            document.getElementById("txtWhere").value=document.getElementById("txtWhere").value+ " " + "or" + " " ;
        }

        function selWhere_onclick()    //To fill column values
        {
            if(document.getElementById("selWhere").value!="where")
            {
                  var column=document.getElementById("selWhere").value;
                  ReportDesignerAjax.GetColumnValue(column, getColumn);
            }
        }
           function getColumn(Response)  // fill column value to selValue
        {
            if(Response.value!="")
            {
               var ds = Response.value;
               document.getElementById("selValue").length=0;
				if(ds!= null && typeof(ds) == "object" && ds.Tables!=null)
				{
				
				   	document.getElementById("selValue").options[0]=new Option("--Select--");
				    for(i=0;i<ds.Tables[0].Rows.length;i++)
				    {
					    document.getElementById("selValue").options[i+1]=new Option(ds.Tables[0].Rows[i].ColumnName,ds.Tables[0].Rows[i].ColumnName);
				    }
				    document.getElementById("lstValue").length=0;
				    for(i=0;i<ds.Tables[0].Rows.length;i++)
				    {
					    document.getElementById("lstValue").options[i]=new Option(ds.Tables[0].Rows[i].ColumnName,ds.Tables[0].Rows[i].ColumnName);
				    }
			    }
            }
        }

        function selCond_onclick() 
        {
                if(document.getElementById("selCond").value=="starts" || document.getElementById("selCond").value=="mid" || document.getElementById("selCond").value=="ends")
                {
                    document.getElementById("selValue").style.display="none";
                     document.getElementById("lstValue").style.display="none";
                     document.getElementById("divInfo").style.display="none";
                    document.getElementById("txtValue").style.display="block";
                     if(document.getElementById("selCond").value=="mid")
                    {
                        document.getElementById("txtValue").value="[string],start,end";
                    }
                    else
                    {
                        document.getElementById("txtValue").value="";
                    }
                }
                else if(document.getElementById("selCond").value=="in")
                {
                    document.getElementById("selValue").style.display="none";
                    document.getElementById("txtValue").style.display="none";  
                    document.getElementById("divInfo").style.display="block";     
                    document.getElementById("lstValue").style.display="block";  
                    
                }
                else
                {
                    document.getElementById("selValue").style.display="block";
                    document.getElementById("txtValue").style.display="none";       
                    document.getElementById("lstValue").style.display="none"; 
                    document.getElementById("divInfo").style.display="none";           
                }
        }

        function btnOkwhere_onclick() 
        {
            if(document.getElementById("selWhere").value!="where" && document.getElementById("selWhere").value!="")
            {
                if(document.getElementById("selValue").style.display=="block" && document.getElementById("selValue").value!="--Select--" && document.getElementById("selValue").value!="")
                {
                      document.getElementById("txtWhere").value=document.getElementById("txtWhere").value +" " + document.getElementById("selWhere").value +" " + document.getElementById("selCond").value+ " '" +document.getElementById("selValue").value +"'";
                }
                else if(document.getElementById("txtValue").style.display=="block" && document.getElementById("txtValue").value!="")
                {
                        if(document.getElementById("selCond").value=="starts")
                        {
                           document.getElementById("txtWhere").value=document.getElementById("txtWhere").value + " " + document.getElementById("selWhere").value +" like '" + document.getElementById("txtValue").value + "%'";
                        }
                        else if(document.getElementById("selCond").value=="ends")
                        {
                           document.getElementById("txtWhere").value=document.getElementById("txtWhere").value + " " + document.getElementById("selWhere").value +" like '%" + document.getElementById("txtValue").value + "'";
                        }
                       
                }
                 else if(document.getElementById("selCond").value=="in")
                        {
                            var j=0;
                            var val="";
                            var obj=document.getElementById("lstValue");
                            for(j=0;j<=obj.length-1;j++)
                            {
                                if(obj.options[j].selected==true)
                                {
                                    if(val=="")
                                    {
                                        val="'"+obj.options[j].value+"'";
                                    }
                                    else
                                    {
                                        val=val+",'"+obj.options[j].value+"'";
                                    }
                                }
                            }
                        
                           document.getElementById("txtWhere").value=document.getElementById("txtWhere").value + " " + document.getElementById("selWhere").value +" in(" + val + ")";
                        }
                else
                {
                    alert("Value Is Blank");
                }
             }
             else
             {
                alert("Select Where Column Name");
             }
        }

        function btnSetformula_onclick() 
        {
            if(document.getElementById("txtWhere").value!="" || document.getElementById("txtWhere").value!=" ")
            {
                window.opener.Where(document.getElementById("txtWhere").value+"`"+document.getElementById("hidTables").value);
//                window.parent.focus();
//                window.close();
            }
            else
            {
                alert("Where Clause Is Empty");
            }
        }

        function btnCloseformula_onclick() {
            window.parent.focus();
            window.close();
        }

        function addMorecolumns() {
            window.open("getTables.aspx?src=where","AddColumns","width=450,height=600,scrollbars=yes,status=yes");
        }
        function addColumns(strTbl)
        {
        
            // Prepare Final tables            
            var extbl=document.getElementById("hidTables").value;  // existing tables
            var ntbl=strTbl.split("~"); //newTables
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
            // ends
            var tb=strTbl.split("~");
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

    </script>
</head>
<body onload="return onLoad();" style="background-color: aliceblue">
    <form id="frmWhere" runat="server">
    <div>
     <table style="width: 375px" summary ="this table is foe where clause">
        <caption>Where</caption>
        <tr>
            <td colspan="3" scope ="colgroup" >
                <span style="font-size: 8pt; color: #ff3366">
                Prepare where clause here. Either use the dropdowns or use the text area to write
                condition(s). To set date use date variables '@Date1@' for start date and '@Date2@' for end date</span>
            
            </td>
        </tr>
        <tr>
                
                         <td scope="col"  title="Where" style="height: 27px; width: 1145px;">
                                    <label for="selWhere" class="label" title="Where">Where</label>
                                </td>
                                <td scope="col"  title="Select Where" style="width: 115px; height: 27px;" valign="top" >
                                  <select id="selWhere" title="Select Where" style="width: 207px" class="dropdownlist" language="javascript" onclick="return selWhere_onclick()" >
                                </select>
                                </td><td>
                                <input type="button" id="btnMorecolumns" class="button" value="More" title="Clieck to add more columns" style="width: 58px" language="javascript" onclick="return addMorecolumns()"  /></td>
                                
                            </tr>
                            
                            <tr>
                                
                                <td scope="col"  title="Select Condition" style="height: 27px; width: 1145px;">
                                    <label for="selCond" class="label" title="Select Condition">
                                        Condition:</label>
                                </td>
                                <td  colspan="2" style="height: 27px; width: 379px;" valign="top">
                                <select id="selCond" title="Select Condition" style="width: 207px" class="dropdownlist" language="javascript" onclick="return selCond_onclick()">
                                    <option value="=">equals to</option>
                                    <option value="<>">not equal to</option>
                                    <option value=">">greater than</option>
                                    <option value="<">less than</option>
                                    <option value="greater than equal to">greater than equal to</option>
                                    <option value="<=">less than equal to</option>
                                    <option value="starts"> starts with</option>
                                    <option value="ends">ends with</option>
                                    <option value="in">in</option>
                                </select>
                                </td>
                                
                            </tr>
                            <tr>
                                <td scope="col"  title="Select Value" style="height: 27px; width: 1145px;">
                                     <label for="selValue" class="label" title="Select Value"> Value:</label>
                                </td>
                                <td  colspan="2" scope ="colgroup"  style="height: 27px; width: 98px;" valign="top" title="Select Value">
                                <select id="selValue" title="Select Value" style="width: 207px; display: block;" class="dropdownlist" language="javascript">
                                </select>
                                 <label for="txtValue" class="label"></label>
                                    <input id="txtValue" class="textbox" style="width: 197px; display: none;" title="Enter Value" type="text" />
                                     <label for="lstValue" class="label"></label>
                                    <select id="lstValue" size="5" style="width: 203px; display: none;" multiple="multiple">
                                        <option selected="selected"></option>
                                    </select>
                                    <div id="divInfo" style="display: none; width: 201px; color: red; font-family: Verdana;
                                        height: 42px">
                                        <span style="font-size: 8pt">Use CTRL + Mouse Click to select more than one value</span>&nbsp;</div>
                                </td>
                            </tr>
                            <tr>
                                <td scope="col" title="View Fromula" style="width: 1145px"  >
                                    <label for="txtWhere" class="label" title="View formula">View where</label>
                                 </td>
                                 <td scope="col" title="View Fromula" style="width: 347px"  >
                                    <input type="button" id="btnOkwhere" class="button" value="Ok" title="Click to view where" language="javascript" onclick="return btnOkwhere_onclick()"  />
                                    
                                 </td>
                                <td style="width: 134px" scope ="col" ></td>
                            </tr>
                            <tr>                                
                                <td colspan="2" scope="col" title="View Formula" style="height: 68px" valign="top">
                                    <textarea style="WIDTH: 324px; HEIGHT: 57px" id="txtWhere" ></textarea>
                                </td>
                                <td valign="top" scope="col" title="Add more conditions"  style="width: 134px; height: 68px;" >
                                    <input type="button" id="btnAnd" class="button" value="And" title="And" style="width: 58px" language="javascript" onclick="return btnAnd_onclick()"  /><br />
                                    <input type="button" id="btnOr" class="button" value="Or" title="Or" style="width: 58px" language="javascript" onclick="return btnOr_onclick()"  />
                                </td>
                            </tr>                           
                            <tr>
                                <td colspan="3" scope="colgroup">
                                     <input type="button" id="btnSetformula" class="button" value="Done" title="Click to set where clause" style="width: 99px" language="javascript" onclick="return btnSetformula_onclick()"  />
                                     <input type="button" id="btnRemove" class="button" value="Remove" title="Click to remove Where clause" style="width: 99px" language="javascript" onclick="return btnRemove_onclick()"   />
                                    <input type="button" id="btnCloseformula" class="button" value="Close Window" title="Click to Close the window " style="width: 99px" language="javascript" onclick="return btnCloseformula_onclick()"  />
                                </td>
                            </tr>
                        </table>
        <br />
                    </div>
             <%-- hidden field declaration --%>
            <input type="hidden" id="hidTables" runat="server" name="hidTables" /><%-- hidden all tablename --%>
            <%-- hidden field declaration ends --%>
    </form>
</body>
</html>
