<%--Project Name: AutoWhiz Phase 2
    Module Name: Advance Report Designer
    Page Name: Save Report
    Summary: Save new report and update existing reports
    Created on: 10/03/08
    Created By: Usha Sehokand
    Last Modified On: 01/11/08

--%>

<%@ Page Language="VB" AutoEventWireup="false" validaterequest="false"  CodeFile="SaveReport.aspx.vb" Inherits="ReportDesigner_SaveReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
  	<title>SaveReport</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../App_Themes/Themes/StyleSheet.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		
		    function getClient() // function to bind client 
            {
                document.getElementById("ddlClient").length=0;
                document.getElementById("ddlLob").length=0;
                AjaxSearchBind.bindClientOnDept(document.getElementById("ddlDepartment").value,fillclient);
             
            }
            		
            function fillclient(Response)
	        {
      				
				  var ds = Response.value;
        				
				  if(ds!= null && typeof(ds) == "object" && ds.Tables!=null)
				    {
				     document.getElementById("ddlClient").options[0]=new Option("--Select--","0");
				            for(i=0;i<ds.Tables[0].Rows.length;i++)
				            {
				               document.getElementById("ddlClient").options[i+1]=new Option(ds.Tables[0].Rows[i].ClientName,ds.Tables[0].Rows[i].AutoID);
				            }
			        }
			            
			  }
        			
        		    
            function getLOB() // function to bind LOB
            {
                document.getElementById("ddlLob").length=0;
                alert(document.getElementById("ddlClient").value);
                AjaxSearchBind.bindLobOnDeptClient(document.getElementById("ddlDepartment").value,document.getElementById("ddlClient").value,filllob);
                
            }

            function filllob(Response)
            {
    	                   
				   var ds = Response.value;
            				
				  if(ds!= null && typeof(ds) == "object" && ds.Tables!=null)
				  {
				        document.getElementById("ddlLob").options[0]=new Option("--Select--","0");
				        for(i=0;i<ds.Tables[0].Rows.length;i++)
				        {
            	             document.getElementById("ddlLob").options[i+1]=new Option(ds.Tables[0].Rows[i].LOBName,ds.Tables[0].Rows[i].AutoID);
				        }
			      }        			    
    			          
            }
            function Done()
            {
          
                window.opener.document.getElementById("hidReportname").value="<%=repName1%>";
                window.opener.document.getElementById("lblCaption").innerHTML=" Report Name : "+"<%=repName1%>";
                window.opener.document.getElementById("hidReporttype").value="<%=repType%>";
                window.opener.document.getElementById("hidReportscope").value="<%=repscope%>";
                window.opener.document.getElementById("hidDepartment").value="<%=dept%>";
                window.opener.document.getElementById("hidClient").value="<%=client%>";
                window.opener.document.getElementById("hidLob").value="<%=lob%>";
                window.opener.document.getElementById("hidDatetable").value='<%=tblDate%>';
                window.opener.document.getElementById("hidReportmode").value="Edit";
                window.opener.document.getElementById("hidRecordid").value='<%=recordid%>';
                window.opener.doneSave();
                window.parent.focus();
                window.close();
            }
		
		    function fillLoad()  // fetch all the values from the welcome.aspx page
		    {
		     document.getElementById("hidlevelsave").value=window.opener.document.getElementById("hidSubtotal").value;
		              var a=window.opener.document.getElementById("hidSubtotal").value;
//		        document.getElementById("chkNonlocal").checked=true;
		        if(window.opener.document.getElementById("hidReportmode").value=="Edit")
		        {
		          
		            document.getElementById("hidRecordid").value=window.opener.document.getElementById("hidRecordid").value;
		            document.getElementById("txtReportname").value=window.opener.document.getElementById("hidReportname").value;
		            document.getElementById("hidReportmode").value="Edit";
		            document.getElementById("hidReportname").value = window.opener.document.getElementById("hidReportname").value;         
		        }
		        if(window.opener.document.getElementById("hidReporttype").value=="Summarized")
		        {
		            document.getElementById("chksummarized").checked=true;
		            document.getElementById("chksimple").checked=false;
		        }
		        else
		        {
		            document.getElementById("chksimple").checked=true;
		            document.getElementById("chksummarized").checked=false;
		        }
		        document.getElementById("hidFormulas").value=window.opener.document.getElementById("hidDformula").value; //get all formulas
		        document.getElementById("hidTables").value=window.opener.document.getElementById("hidTables").value; //get hidden Tables
		        document.getElementById("hidHpos").value=window.opener.document.getElementById("hidHpos").value; //get header elements
		        document.getElementById("hidDpos").value=window.opener.document.getElementById("hidDpos").value;  // get details elements
		        document.getElementById("hidFpos").value=window.opener.document.getElementById("hidFpos").value;   // get footer elements
		        document.getElementById("hidHformat").value=window.opener.document.getElementById("hidHformat").value;    //  get header elements format
		        document.getElementById("hidColumnformat").value=window.opener.document.getElementById("hidDformat").value;    //  get details elements format
		        document.getElementById("hidHformula").value=window.opener.document.getElementById("hidHformula").value;    //  get header elements formula
		        document.getElementById("hidFormula").value=window.opener.document.getElementById("hidFormula").value;    //  get report formula
                document.getElementById("hidFormulaname").value=window.opener.document.getElementById("hidFormulaname").value;    //  get report formula name
                document.getElementById("hidWhere").value=window.opener.document.getElementById("hidWhere").value;    //  get report wheredata               
                document.getElementById("hidGroupby").value=window.opener.document.getElementById("hidGroupby").value;    //  get report group by
                document.getElementById("hidOrderby").value=window.opener.document.getElementById("hidOrderby").value;    //  get report order by
                document.getElementById("hidHaving").value=window.opener.document.getElementById("hidHaving").value;    //  get report having
                document.getElementById("hidGroupby").value=window.opener.document.getElementById("hidGroupby").value;    //  get report group by
                document.getElementById("hidColorcondition").value=window.opener.document.getElementById("hidColorcondition").value;    //  get details color condition
                document.getElementById("hidFformula").value=window.opener.document.getElementById("hidFformula").value;    //  get footer elements formula
		        document.getElementById("hidFformat").value=window.opener.document.getElementById("hidFformat").value;    //get footer elements format
		        document.getElementById("hidHcolorcon").value=window.opener.document.getElementById("hidHcolorcon").value;    // get header elements color condition
		        document.getElementById("hidFcolorcon").value=window.opener.document.getElementById("hidFcolorcon").value;    // get footer elements color condition
		        document.getElementById("hidHeight").value=window.opener.document.getElementById("hidHeight").value;    // get height of header & footer
		        document.getElementById("hidHeaderformat").value=window.opener.document.getElementById("hidHeaderformat").value;    // get header basic format
		        document.getElementById("hidDFormula").value=window.opener.document.getElementById("hidDformula").value;    // get formula
		        document.getElementById("hidDetailsformat").value=window.opener.document.getElementById("hidDetailsformat").value;    // get details basic format
		        document.getElementById("hidFooterformat").value=window.opener.document.getElementById("hidFooterformat").value;    // get footer basic format
		        var ddlTable=document.getElementById("ddlTablename");
		        ddlTable.length=0;
		        ddlTable.options[0]=new Option("--select--","0");
		        var str=document.getElementById("hidTables").value.split("~");
		        var i=0;
		        var count;
		        for(i=0;i<=str.length-1;i++)
		        {
		            ddlTable.options[i+1]=new Option(str[i],str[i]);
		        }
		        if(document.getElementById("hidDatecon").value!="")
		        {
		            document.getElementById("ddlTablename").value=document.getElementById("hidDatecon").value;
		        }
		       
		        document.getElementById("btnEx3").value=window.opener.document.getElementById("btnEx").value;
		        
		        
		    }
        function ddlTablename_onclick() 
        {
                document.getElementById("hidDatecon").value=document.getElementById("ddlTablename").value;
                if(document.getElementById("hidDatecon").value=="--select--" || document.getElementById("hidDatecon").value=="--Select--" || document.getElementById("hidDatecon").value=="0")
                document.getElementById("hidDatecon").value=""; 
//                if(document.getElementById("hidDatecon").value!="")
//                {
//                    var str=document.getElementById("ddlTablename").value;
//                    "<%=datetable%>"=str;
//                }
        }

function TABLE1_onclick() {

}


        </script>
</head>
<body onload="return fillLoad();">
	<form id="formSavrreport" runat="server">
	    <table style="width: 447px; height: auto; display: block; position: static;" id="TABLE1" language="javascript" onclick="return TABLE1_onclick()">
		           <caption style ="background-color:#67A897">Save Report</caption>
                   <tr><td>
		            <table runat="server" visible="false" id="spandisplay">
                       <tr>
                     <td  scope="col" title="Department" style="width: 138px; height: 24px; color : Black ">
                            <asp:Label ID="lbl1" runat="server" CssClass="label"   Text="Select Level 1"  ></asp:Label> 
		                </td>
		                <td style="height: 24px" scope ="col">
		                <asp:DropDownList runat="server" ToolTip="Select Department" CssClass="dropdownlist" ID="ddlDepartment" AutoPostBack="True"></asp:DropDownList>
		                </td>
		            </tr>
		            <tr>
		                <td scope="col" title="Client" style="width: 138px; color : Black ">
                            <asp:Label ID="lbl2" runat="server" CssClass="label"  Text="Select Level 2"  ></asp:Label> 
		                </td>
		                <td scope="col" title="Select Client" >
		                    <asp:DropDownList runat="server" CssClass="dropdownlist" ID="ddlClient" AutoPostBack="True"></asp:DropDownList>
                            <asp:Label ID="lblError" runat="server" CssClass="label" ForeColor="Red" Height="93px"
                                Style="z-index: 100; left: 315px; position: absolute; top: 43px"
                                Width="134px"></asp:Label>
		                </td>
		            </tr>
		            <tr>
		                <td  scope="col" title="LOB" style="width: 138px; color : Black ">
                            <asp:Label ID="lbl3" runat="server" CssClass="label"  Text="Select Level 3"  ></asp:Label> 
		                </td>
		                <td  scope="col" title="Select LOB">
		                    <asp:DropDownList runat="server" CssClass="dropdownlist" ID="ddlLob" AutoPostBack="True"></asp:DropDownList>
		                </td>
		            </tr>
                    </table>
                   </td></tr>
		            <tr>
		                <td  scope="col" title="Table name to apply Date condition" style="width: 138px; color : Black ">
		                    <label class="label" for="ddlTablename" title="Table name to apply Date condition"> Table to apply Date
                                condition:</label>
		                </td>
		                <td  scope="col" title="Select Table name to apply Date condition" valign="top">
		                    <%--<asp:DropDownList runat="server" ID="ddlTablename" CssClass="dropdownlist"></asp:DropDownList>--%>
		                    <select id="ddlTablename" class="dropdownlist" language="javascript" onclick="return ddlTablename_onclick()" onclick="return ddlTablename_onclick()"></select>
                           
		                </td>
		            </tr>
		            <tr>
		                <td  scope="col" title="Report Name" style="width: 138px; color : Black ">
		                    <label class="label" title="Report Name" for="txtReportname">Report Name:</label>
		                </td>
		                <td  scope="col" title="Enter Report Name">
		                    <asp:TextBox runat="server" ToolTip="Enter Report Name" ID="txtReportname" CssClass="textBox" Width="250px" MaxLength="200"></asp:TextBox></td>
		            </tr>
		            
		            <tr>
		                 <td scope="col" title="Type of the report" style="width: 138px; color : Black ">
		                    <label for="chksimple" class="label" title="Report type">Save As:</label>
		                </td>
		                <td  scope="col"  title="Select type of the report">  
		                    <table style="position:static;" summary ="Table">
		                        <tr>
		                         <td scope ="col">
        		                    
                                    <input type="radio"  value="Simple" runat="server" name="saveType" title="Simple" id="chksimple" style="position: static" />
                                    </td><td style="width: 70px" scope ="col" >
                                    <label  for="chkSimple" class="label" title="Save the report as simple">Simple</label>
                                 </td>
                                  <td scope ="col">
                                    <input type="radio" value="Summarized" runat="server" name="saveType" title="Summarized" id="chksummarized" style="position: static" />
                                    </td><td scope ="col" style ="color:black">
                                    <label for="chkSummarized" class="label" title="Save the report as summarized">Summarized</label>
                                    </td><td>
		                        </td>
		                     </tr>
		                  </table>                          
                            </td>
		            </tr>
		            
		            <tr>
                         <td colspan="2" scope="colgroup" title="Save Report">
		                    <asp:Button runat="server" ID="btnSavenewMul" Visible="false" Text="Save As New" cssclass="button" />&nbsp;<asp:Button runat="server" ID="btnSavenew" Text="Save As New" Visible="false" cssclass="button" /><asp:Button runat="server" ID="btnUpdate" Text="Update" cssclass="button"  /><input type=button ID="btnClose" value="Close" class="button"  onclick="window.close()" /></td>
		                <td colspan="2" scope="colgroup" title="Save Report">
		                    &nbsp;</td>
		            </tr>
		    </table>
        &nbsp;<%--- Declaration of the hidden elements -----%><input type="hidden" name="hidTables" runat="server"  id="hidTables" /><%-- hidden tables--%>
         <input type="hidden" name="hidlevelsave" runat="server"   id="hidlevelsave" /><%-- hidden level for summarized--%>
		        <input type="hidden" name="hidHpos" runat="server"  id="hidHpos" /><%-- hidden header elements--%>
		        <input type="hidden" name="hidDpos" runat="server"  id="hidDpos" /><%-- hidden details elements--%>
		        <input type="hidden" name="hidFpos" runat="server"  id="hidFpos" /><%-- hidden footer elements--%>
		        <input type="hidden" name="hidHformat" runat="server"  id="hidHformat" /><%-- hidden header elements format--%>
		        <input type="hidden" name="hidFformat" runat="server"  id="hidFformat" /><%-- hidden footer elements format--%>
		        <input type="hidden" name="hidHformula" runat="server"  id="hidHformula" /><%-- hidden header elements formula--%>
		        <input type="hidden" name="hidFformula" runat="server"  id="hidFformula" /><%-- hidden footer elements formula--%>
		        <input type="hidden" name="hidHcolorcon" runat="server"  id="hidHcolorcon" /><%-- hidden header elements color condition--%>
		        <input type="hidden" name="hidFcolorcon" runat="server"  id="hidFcolorcon" /><%-- hidden footer elements color condition--%>
		        <input type="hidden" name="hidColumnformat" runat="server"  id="hidColumnformat" /><%-- hidden columnwise format--%>
		        <input type="hidden" name="hidWhere" runat="server"  id="hidWhere" /><%-- hidden where condition--%>
                <input type="hidden" name="hidGroupby" runat="server"  id="hidGroupby" /><%-- hidden group by condition--%>
                <input type="hidden" name="hidJoin" runat="server"  id="hidJoin" /><%-- hidden join condition--%>
                <input type="hidden" name="hidFormula" runat="server"  id="hidFormula" /><%-- hidden formula condition--%>
                <input type="hidden" name="hidFormulaname" runat="server"  id="hidFormulaname" /><%-- hidden formula name--%>
                <input type="hidden" name="hidOrderby" runat="server"  id="hidOrderby" /><%-- hidden orderby condition--%>
                <input type="hidden" name="hidHaving" runat="server"  id="hidHaving" /><%-- hidden having condition--%>
                <input type="hidden" name="hidColorcondition" runat="server"  id="hidColorcondition" /><%-- hidden color condition--%>
                <input type="hidden" name="hidHeight" runat="server"  id="hidHeight" /><%-- hidden height of header & footer--%>
                <input type="hidden" name="hidHeaderformat" id="hidHeaderformat" runat="server"/><%-- hidden basic formatting of header --%>
                <input type="hidden" name="hidDetailsformat" id="hidDetailsformat" runat="server"/><%-- hidden basic formatting of details pane --%>
                <input type="hidden" name="hidFooterformat" id="hidFooterformat" runat="server"/><%-- hidden basic formatting of footer --%>
                <input type="hidden" name="hidDatecon" id="hidDatecon" runat="server" />
                <input type="hidden" name="hidDFormula" id="hidDFormula" runat="server" /><%-- hidden formula --%>
                <input type="hidden" name="hidFormulas" id="hidFormulas" runat="server" /><%-- all formulas --%>
                <input type="hidden" runat="server" name="hidReportmode" id="hidReportmode" /> 
                <input type="hidden" runat="server" name="hidReportname" id="hidReportname"/>
                <input type="hidden" runat="server" name="hidRecordid" id="hidRecordid"/>
                <input type="hidden"  name="btnEx3" id="btnEx3" runat="server" />
                  <input type="hidden" runat="server" name="hidDept" id="hidDept"/>
                   <input type="hidden" runat="server" name="hiddept" id="hidCli"/>
                    <input type="hidden" runat="server" name="hiddept" id="hidLo"/>
		    <%--- Declaration of the hidden elements ends -----%>	
    </form>
 </body>
</html>
<%--
Change History: None
--%>