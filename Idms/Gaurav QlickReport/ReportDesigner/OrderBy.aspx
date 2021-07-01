<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OrderBy.aspx.vb" Inherits="ReportDesigner_OrderBy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <title>Order By</title>
   <link href="../App_Themes/Themes/StyleSheet.css" rel="Stylesheet"  />
   <script language="javascript">
     function onLoad()
    {
       // load existing order by clause
        document.getElementById("txtOrderby").value=window.opener.document.getElementById("hidOrderby").value;
       // ends
        document.getElementById("hidTblname").value=window.opener.document.getElementById("hidTables").value;
        var strTblp=window.opener.document.getElementById("hidTables").value;         
           if(strTblp!="")
           {
            addColumns(strTblp);     
           }
           else
           {
            alert("No columns found. Please add columns to set formula.")
           }
    }
     function btnMorecolumn_onclick()
        {
            window.open("getTables.aspx?src=orderby","AddColumns")
        } 
    function addColumns(tblName)
    {
      // Prepare Final tables            
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
            // ends
         var whrSel=document.getElementById("selColumn");
          var tb=tblName.split("~");
          if(whrSel.length==0)
          {
            document.getElementById("selColumn").options[0]=new Option("--Select--","--Select Column--");
          }
         for(i=0;i<=tb.length-1;i++)
         {
         
           ReportDesignerAjax.GetTableFields(tb[i], fillColumn);
         }
    }
     function fillColumn(Response)  // fill columns to selColumn
        {
            if(Response.value!="")
            {
               var str=Response.value.split("~")
               var c=document.getElementById("selColumn").length;
             			
				    for(i=0;i<str.length;i++)
				    { 
				        document.getElementById("selColumn").options[c]=new Option(str[i],str[i]);
					    c=c+1;
				    }
			   
            }
        }
        function btnSethaving_onclick()
        {
      
            if(document.getElementById("txtOrderby").value!="")
            {
                alert("The Order By Clause has been set");
                
                window.opener.Orderby(document.getElementById("txtOrderby").value+"`"+document.getElementById("hidTblname").value);
//                window.parent.focus();
//                window.close();   
            window.opener.document.getElementById("hidOrderby").value=document.getElementById("txtOrderby").value;
            }
            else
            {
                alert("No  Order By Clause Found");
            }
        }
        function closeWindow()  // Close this window
        {
             window.parent.focus();
             window.self.close();
        }
        function btnRemove_onclick() 
        {
              if(document.getElementById("txtOrderby").value=="" || document.getElementById("txtOrderby").value==" ")
              {
                    alert("No Oder By Clause Found")
              }
              else
              {
                alert("The order by clause is removed");
              }
              document.getElementById("txtOrderby").value="";
              window.opener.document.getElementById("hidOrderby").value="";
             
        }


    function btnOk_onclick() 
    {
            if(document.getElementById("selColumn").value=="--Select--")
            {
                alert("Please select a column to set the order by clause");
                
            }
            else
            {
                var st="";
                if(document.getElementById("selOrder").value!="--Select--")
                {
                    st=document.getElementById("selColumn").value+" "+document.getElementById("selOrder").value;
                }
                else
                {
                    st=document.getElementById("selColumn").value;
                }
                
                if(document.getElementById("txtOrderby").value!="")
                {
                    document.getElementById("txtOrderby").value=document.getElementById("txtOrderby").value+","+st;
                }
                else
                {
                    document.getElementById("txtOrderby").value=st;
                }
            }
    }

   </script>
</head>
<body onload="return onLoad();" style="background-color: aliceblue">
    <form id="frmOrderby" runat="server">
    <div>
        <table style="width: 456px" >
        <caption style ="background-color:#0591D3">Order By</caption> 
            <tr>
                <td style="width: 99px; color : Black " scope ="col">
                    <label class="label" for="selColumn" title="Select Column">
                        Column:</label>
                </td>
                <td style="width: 351px" scope ="col">
                    <select id="selColumn" title="Select Column" class="dropdownlist" style="width: 347px;" >
                    </select>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td scope ="col" style ="color:black">
                     <label class="label" for="selOrder" title="Select Order">
                        Order:</label>
                </td>
                <td style="width: 351px" scope ="col">
                     <select id="selOrder" title="Select Order" style="width: 286px" class="dropdownlist" >
                                     <option value="--Select--"  >--Select Order--</option>
                                     <option value="desc">Descending</option>
                                     <option value="asc">Ascending</option>
                                </select>
                    <input id="btnOk" class="button" language="javascript" style="width: 55px" title="Ok" type="button" value="Ok" onclick="return btnOk_onclick()" /></td>
                
            </tr>
            <tr>
                <td scope ="col">
                </td>
                <td style="width: 351px" scope ="col">
                 <label class="label" for="txtOrderby" title="Select Order">
                        </label>
                    <textarea class="textbox" id="txtOrderby" name="txtOrderby" style="height: 72px; width: 341px;" title="Order by clause"></textarea></td>
            </tr>
              <tr>
                                 <td colspan="2"  scope="colgroup" title="Set Order by clause" align="center">
                                    <input type="button" class="button" value="Set Clause" id="btnSethaving" language="javascript" onclick="return btnSethaving_onclick()" title="Set Order by clause" />
                                    <input type="button" class="button" value="Remove Clause" id="btnRemove" language="javascript" onclick="return btnRemove_onclick()" title="Remove order by clause" />
                                    <input id="btnClosetop" class="button" onclick="javascript:closeWindow();" style="width: 63px" title="Close this window" type="button" value="Close" />
                                </td>                                
            </tr>
        </table>
    <input type="hidden" name="hidTblname" id="hidTblname" /> <%-- hidden tables --%>
    <%-- hidden field declaration ends --%>
    </div>
    </form>
</body>
</html>
