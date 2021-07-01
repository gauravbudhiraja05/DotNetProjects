<%--Project Name: AutoWhiz Phase 2
    Module Name: Advance Report Designer
    Page Name: Group By
    Summary: To collect group by elements of  report.
    Created on: 10/03/08
    Created By: Usha Sehokand
    Last Modified On: 04/11/08
--%>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GroupBy.aspx.vb" Inherits="ReportDesigner_GroupBy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <title>Group By</title>
    <link href="../App_Themes/Themes/StyleSheet.css" rel="Stylesheet" />
<script language="javascript" type="text/javascript">
<!--
    function onLoad()
    {
  
    //        //Load existing formulas
        document.getElementById("hidTblname").value=window.opener.document.getElementById("hidTables").value;
        document.getElementById("hidFormulas").value=window.opener.document.getElementById("hidDformula").value;
        document.getElementById("hidGroupby").value=window.opener.document.getElementById("hidGroupby").value;
                
                /////////// call ajax to find groupby elements
                ReportDesignerAjax.formGroupby(document.getElementById("hidElem").value,document.getElementById("hidFormulas").value,document.getElementById("hidGroupby").value,myfinal);
                ///////////////////////////////////////////////////
                var exFor=window.opener.document.getElementById("hidDformula").value;
              
               if(exFor!="")
                {
                    var dd=exFor.split("~");                    
                    var tr=document.getElementById("selGroupby");
                    var u=0;
                    tr.length=0;
                    for(u=0;u<=dd.length-1;u++)
                    {
                       tr.options[u]=new Option(dd[u],dd[u]);
                    }
                }

          
           
               var strTblp=document.getElementById("hidTblname").value;         
               if(strTblp!="")
               {
                addColumns(strTblp);     
               }
               else
               {
                alert("No columns found. Please add columns to set the group by clause.")
               }   
    }
    
    function myfinal(res)
    {    
    
        if(res!=null)
        {
            document.getElementById("hidElem").value=res.value;
              if(document.getElementById("hidElem").value!="")
            {
                var dd=document.getElementById("hidElem").value.split("~");
                var tr=document.getElementById("selFinal");
                var u=0;
               tr.length=0;
                for(u=0;u<=dd.length-1;u++)
                {
                    tr.options[u]=new Option(dd[u],dd[u]);
                }
                fillLevels();
            }
        }
       
    }
    function fillLevels() // to fill levels of the subtotal
    {
   
          var tr=document.getElementById("selFinal");
          var trn=document.getElementById("selLevel");
          var y=0;
          for(y=0;y<=tr.length-1;y++)
          {
                 trn.options[y]=new Option(y,y);
          }
          trn.value=trn.length-1;
    }
    
    function addColumns(tblName)
    {
    
         var whrSel=document.getElementById("selGroupby");
         var tb=tblName.split("~");
         for(i=0;i<=tb.length-1;i++)
         {
         
           ReportDesignerAjax.GetTableFields(tb[i], fillWhere);
         }
    }
    function fillWhere(Response)  // fill column value to selGroupby
        {
         
           if(Response.value!="")
            {
            
               var str=Response.value.split("~")
               var c=document.getElementById("selGroupby").length;
             			
				    for(i=0;i<str.length;i++)
				    { 
				        var nStr=str[i].replace("$",".");
				        document.getElementById("selGroupby").options[c]=new Option(nStr,nStr);
					    c=c+1;
				    }
			   
            }
        }
  
        function closeWindow()
        {
            window.parent.focus();
            window.close();
        }
        function btnSetclause_onclick() 
        {
        
             
               if(document.getElementById("selFinal").length!=0)
               {
                var grpBy="";
                var t=0;
                var tr=document.getElementById("selFinal");
                for(t=0;t<=tr.length-1;t++)
                {
                    if(grpBy=="")
                    {
                        grpBy=tr.options[t].value;
                    }
                    else
                    {
                        grpBy=grpBy+","+tr.options[t].value;
                    }
                }   
                
                
                window.opener.Groupby(grpBy+"`"+document.getElementById("hidTblname").value);
                
                // set order by according to group by
                window.opener.Orderby(grpBy+"`"+document.getElementById("hidTblname").value);
                alert("The Group By clause has been set");
                window.opener.document.getElementById("hidSubtotal").value=document.getElementById("selLevel").value;
            }
            else
            {
                alert("No Group by clause found");
            }
          }

        function btnRemove_onclick() 
        {
            if(document.getElementById("selFinal").length==0)
                alert("No Group By clause found");
            else
            {  
            
                 document.getElementById("selFinal").length=0;
                 window.opener.document.getElementById("hidGroupby").value="";
                 window.opener.document.getElementById("hidShowtotal").value="";
                 
                 alert("The Group By clause is removed");   
             }
             
          }
        function btn1_onclick()
        {
       //debugger;
            var t=0;
            var ty=document.getElementById("selGroupby");
            for(t=0;t<=ty.length-1;t++)
            {
                if(ty.options[t].selected==true)
                {
                    var ln=document.getElementById("selFinal").length;
                    var g=0;
                    var b=0;
                    var tr=document.getElementById("selFinal");
                    for(g=0;g<=tr.length-1;g++)
                    {
                        if(tr.options[g].value==ty.options[t].value)
                        {
                            b=1;
                        }
                        
                    }
                    if(b==0)
                    {
                        var as=ty.options[t].value.split(" AS ");
                        var val=ty.options[t].value;
                        if(as.length>1)
                        {
                            val=as[0];
                        }
                        tr.options[ln]=new Option(val,val);
                        /// update subtotal levels
                        var tr1=document.getElementById("selLevel");
                        var ln1=document.getElementById("selLevel").length;                       
                        tr1.options[ln1]=new Option(ln1,ln1);
                    }
                }
            }
        }
        function btn2_onclick()
        {
            var t=0;
            var val=""
            var tr=document.getElementById("selFinal");
           
            for(t=0;t<=tr.length-1;t++)
            {
                if(tr.options[t].selected==true)
                {
                    val=tr.options[t].value;
                    tr.remove(t);     
                     /// update subtotal levels
                        var tr1=document.getElementById("selLevel");
                        var ln1=document.getElementById("selLevel").length;                       
                        tr1.remove(ln1-1);
                }
            }           
        }

// -->
</script>
</head>
<body onload="return onLoad();" style="background-color: aliceblue">
    <form id="frmGroupby" runat="server">
        <div>
             <table style="width: 511px">
                    <caption style ="background-color:#0591D3">Group By</caption>   
                    <tr>
                        <td scope="col" style ="color:black">
                            <label class="label" for="selFinal">Group By Fields</label>
                        </td>
                        <td style="width: 433px" scope="col">
                        </td>
                        <td scope="col" style ="color:black">
                            <label class="label" for="selGroupby">Use CTRL to select multiple columns</label>
                        </td>
                    </tr>
                    <tr>
                        <td scope="col">
                            <select id="selFinal" style="width: 354px" size="12" ></select>
                        </td>
                        <td style="width: 433px" scope="col">
                            <input class="button" title="<<" type="button" value="<<" id="btn<<" style="width: 50px" onclick="return btn1_onclick()" />
                            <br />        
                            <input type="button" class="button" value=">>" title=">>" id="btn>>" style="width:50px" onclick="return btn2_onclick()"/>
                        </td>
                        <td scope="col">
                            <select id="selGroupby" style="width: 354px" size="12" multiple="multiple" ></select>
                        </td>
                    </tr>   
                 
                    </table>
                    <table style="width: 776px;" summary ="table">
                    <tr>
                    <td style="height: 21px; width: 375px; color : Black " colspan="3" scope ="colgroup" >
                        <label class="label" for="selLevel">Show Sub Total Upto </label>
                        <select class="dropdownlist" id="selLevel" style="width: 116px"></select>
                     
                    </td>
                    
                    </tr>
                   <tr>
                            <td colspan="3" scope="colgroup" title="Set Clause" style="height: 27px; width: 375px;" align="center">
                                &nbsp;<input type="button" class="button" id="btnSetclause" value="Set Clause" onclick="return btnSetclause_onclick();" />
                                <input type="button" class="button" id="btnRemove" value="Remove"  title="Remove the group by clause" language="javascript" onclick="return btnRemove_onclick()" />
                                <input type="button" class="button" onclick="javascript:closeWindow();" id="btnClose" value="Close" />
                            </td>
                    </tr>   
                    
                          
             </table>
        </div>
         <%-- hidden field declaration --%>
      <input type="hidden" runat="server" name="hidTblname" id="hidTblname" /> <%-- hidden tables --%>
    <input type="hidden" runat="server" name="hidElem" id="hidElem" /> <%-- hidden tables --%>
     <input type="hidden" runat="server" name="hidGroupby" id="hidGroupby" /> <%-- hidden groupby --%>
     <input type="hidden" runat="server" name="hidFormulas" id="hidFormulas" /> <%-- hidden formulas --%>
    <input type="hidden" name="hidShowtotal" id="hidShowtotal" /><%-- hidden show total --%>
    <input type="hidden" name="ex" id="ex" runat="server" value="0" /> <%--for logical use--%>
    <%-- hidden field declaration ends --%>
    </form>
</body>
</html>
<%--
------------------ Change History -------------------------
None
--%>