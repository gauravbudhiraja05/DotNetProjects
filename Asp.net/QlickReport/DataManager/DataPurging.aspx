<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="DataPurging.aspx.vb" Inherits="DataManager_DataPurging" title="Data Purge" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">


 <%--//document.getElementById("<%=txtFormula.ClientID%>").value=document.getElementById("<%=txtFormula.ClientID%>").value+ value1+op;--%>

  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />

<script language="javascript" type="text/javascript">

function getclient() // function to bind client 
  {
 
        AjaxSearchBind.bindClientOnDept(document.getElementById("<%=ddlDept.ClientID%>").value,fillclient);
        bindlisttab()
        document.getElementById("<%= hidClientId.ClientId %>").value = document.getElementById("<%=ddlClient.ClientId%>").value 
        if (document.getElementById("<%=ddlLob.ClientId%>").value!="--Select--")
        {
        GetLOB()
        clearvalue()
        
        }
  
 
}
		
function fillclient(Response)
	{
	  			for(i=document.getElementById("<%=ddlClient.ClientId%>").length;i>=0;i--)
				{
					document.getElementById("<%=ddlClient.ClientId%>").remove(i);
				}
				
				
				var ds = Response.value
				
				if(ds!= null && typeof(ds) == "object" && ds.Tables!=null)
				{
				   	document.getElementById("<%=ddlClient.ClientId%>").options[0]=new Option("--Select--");
				    for(i=0;i<ds.Tables[0].Rows.length;i++)
				    {
					
					    document.getElementById("<%=ddlClient.ClientId%>").options[i+1]=new Option(ds.Tables[0].Rows[i].ClientName,ds.Tables[0].Rows[i].AutoId);
				    }
			    }
			    //else
			   // {
			        //alert('No client found!');
			   // }
			}
function GetLOB() // function to bind LOB
    {
        AjaxSearchBind.bindLobOnDeptClient(document.getElementById("<%=ddlDept.ClientID%>").value,document.getElementById("<%=ddlClient.ClientID%>").value,filllob);
        document.getElementById("<%= hidClientId.ClientId %>").value = document.getElementById("<%=ddlClient.ClientId%>").value;
        bindlisttab()
        clearvalue()
        
        
    }

function filllob(Response)
{
	for(i=document.getElementById("<%=ddlLob.ClientId%>").length;i>=0;i--)
				{
					document.getElementById("<%=ddlLob.ClientId%>").remove(i);
				}
				
                
				var ds = Response.value
				
				if(ds!= null && typeof(ds) == "object" && ds.Tables!=null)
				{
				   	document.getElementById("<%=ddlLob.ClientId%>").options[0]=new Option("--Select--");
				    for(i=0;i<ds.Tables[0].Rows.length;i++)
				    {
				
					    document.getElementById("<%=ddlLob.ClientId%>").options[i+1]=new Option(ds.Tables[0].Rows[i].LOBName,ds.Tables[0].Rows[i].AutoID);
				    }
			    }
			    
			   			    		    
}


function bindlisttab() // function to bind table in DropDown List

			{
		var check="<%=Session("typeofuser") %>";
				
			if(check == "Admin")
			{
			DataManagerAjax.user_datatable(document.getElementById("<%=ddlDept.ClientID%>").value,document.getElementById("<%=ddlClient.ClientID%>").value,document.getElementById("<%=ddlLob.ClientID%>").value ,"<%=session("userid")%>",filllisttab);
		document.getElementById("<%= hidLobId.ClientId %>").value = document.getElementById("<%=ddlLob.ClientId%>").value 
			}
			else
			{
			DataManagerAjax.dind_listpurg(document.getElementById("<%=ddlDept.ClientID%>").value,document.getElementById("<%=ddlClient.ClientID%>").value,document.getElementById("<%=ddlLob.ClientID%>").value,"<%=session("userid")%>",filllisttab);
			        document.getElementById("<%= hidLobId.ClientId %>").value = document.getElementById("<%=ddlLob.ClientId%>").value 
                }
          }
			
		function filllisttab(Response)
			{
			
			
			for(i=document.getElementById("<%=ddlTable.ClientId%>").length;i>=0;i--)
				{
					document.getElementById("<%=ddlTable.ClientId%>").remove(i);
				}
				
				var ds = Response.value
				
				if(ds!= null && typeof(ds) == "object" && ds.Tables!=null)
				{
				  	document.getElementById("<%=ddlTable.ClientId%>").options[0]=new Option("--Select--");
				    for(i=0;i<ds.Tables[0].Rows.length;i++)
				    {
					
					    document.getElementById("<%=ddlTable.ClientId%>").options[i+1]=new Option(ds.Tables[0].Rows[i].TableName,ds.Tables[0].Rows[i].TableName);
					    
				    }
				   
				   
			}
			
			
	}
    function bindlisttab1() // function to bind table in DropDown List

			{
		var check="<%=Session("typeofuser") %>";
				
			if(check == "Admin")
			{
			DataManagerAjax.user_datatable(60,0,0 ,"<%=session("userid")%>",filllisttab1);
		document.getElementById("<%= hidLobId.ClientId %>").value = 0 
			}
			else
			{
			DataManagerAjax.dind_listpurg(60,0,0,"<%=session("userid")%>",filllisttab1);
			        document.getElementById("<%= hidLobId.ClientId %>").value = 0 
                }
          }
			
		function filllisttab1(Response)
			{
			
			
			for(i=document.getElementById("<%=ddlTable.ClientId%>").length;i>=0;i--)
				{
					document.getElementById("<%=ddlTable.ClientId%>").remove(i);
				}
				
				var ds = Response.value
				
				if(ds!= null && typeof(ds) == "object" && ds.Tables!=null)
				{
				  	document.getElementById("<%=ddlTable.ClientId%>").options[0]=new Option("--Select--");
				    for(i=0;i<ds.Tables[0].Rows.length;i++)
				    {
					
					    document.getElementById("<%=ddlTable.ClientId%>").options[i+1]=new Option(ds.Tables[0].Rows[i].TableName,ds.Tables[0].Rows[i].TableName);
					    
				    }
				   
				   
			}
			
			
	}    
	function bindtabcol() // function to bind Column of selected Table
	{
	
	    
		
	    DataManagerAjax.dind_Collist(document.getElementById("<%=ddlTable.ClientID%>").value,filltabcol);
	
		
    }

	
function filltabcol(Response)
{
            clearvalue()
           // debugger;
                    
            for(i=document.getElementById("<%=cbofdata.ClientId%>").length;i>=0;i--)
				{
					document.getElementById("<%=cbofdata.ClientId%>").remove(i);
				}
            
				var arrdata = Response.value;
				
                 var columnname = arrdata.split(",");
												
				for(i=0;i<arrdata.length;i++)
				{
					if(columnname[i]!=null)
									
					//document.getElementById("<%=cbofdata.ClientID%>").options[i+1]=newOption(ds.Tables[0].Rows[i].visiblecolumn,ds.Tables[0].Rows[i].visiblecolumn);
					document.getElementById("<%=cbofdata.ClientID%>").options[i]=new Option(columnname[i],columnname[i]);
					
				}
				
				 document.getElementById("<%=hidTablename.ClientID%>").value=document.getElementById("<%=ddlTable.ClientID%>").value;

}

 function cbofdata_onclick() // Function to get value on Formul Field
        {
         if ((document.getElementById("<%=txtFormula.ClientID%>").value)=="")
            {
            document.getElementById("<%=txtFormula.ClientID%>").value=document.getElementById("<%=cbofdata.ClientID%>").value;
             }
        
            else
            {
            document.getElementById("<%=txtFormula.ClientID%>").value=document.getElementById("<%=txtFormula.ClientID%>").value+" "+ document.getElementById("<%=cbofdata.ClientID%>").value;
            }
        
        }
        
 function setFormula(op)
        {
         
         var chk=op.search("Lik")
         
    if (chk!=-1)
    {
     
    op="Like('%value%')";
    }
    
//				if (!blank(op))
//				{
					if ((document.getElementById("<%=txtFormula.ClientID%>").value)=="")
					{
						document.getElementById("<%=txtFormula.ClientID%>").value= document.getElementById("<%=txtFormula.ClientID%>").value+  op
					}
					else 
					{
						document.getElementById("<%=txtFormula.ClientID%>").value = document.getElementById("<%=txtFormula.ClientID%>").value + " " + op
					}
				//}
           
        }
        
   function rowcount()//Function to show no. of row affected
   {
     document.getElementById("<%=divPurbcp.ClientID%>").style.visibility="hidden";
         if(document.getElementById("<%=ddlDept.ClientID%>").value=="--Select--")
       {
           alert("Please Select Department!!!")
       }
       
       else if((document.getElementById("<%=ddlTable.ClientID%>").value=="--Select--")|| (document.getElementById("<%=ddlTable.ClientID%>").value==""))
       {
            alert("Please Select Table Name!!!")
       }
        else if(document.getElementById("<%=txtFormula.ClientID%>").value=="")      
        {
            alert("Please Apply Condition!!!")
        }
       else
       {

    DataManagerAjax.Rowcount(document.getElementById("<%=ddlTable.ClientID%>").value,document.getElementById("<%=txtFormula.ClientID%>").value,getrow);
         }
   
   }
   
   
   function getrow(Response)
   {
        var rowcount=Response.value
        var finalvalue=rowcount.split(",")
        if (finalvalue[0]=="f")
        {
        alert(finalvalue[1])
        }
        else if(rowcount=="0")
        {
       
         document.getElementById("<%=lblshow.ClientID%>").value=+ rowcount + " Row(s) are going to be affected!!! ";
         document.getElementById("btnshowrows").style.visibility="hidden";
         document.getElementById("<%=divshow.ClientID%>").style.visibility="visible";
        
        }
        else
        {
        document.getElementById("<%=lblshow.ClientID%>").value=+ rowcount + " Row(s) are going to get affected. Click View button to view the affected rows";
        document.getElementById("<%=divshow.ClientID%>").style.visibility="visible";
         document.getElementById("btnshowrows").style.visibility="visible";
           
        }
        
   }
   function rowcount1()//Function to show no. of row affected
   {
     document.getElementById("<%=divPurbcp.ClientID%>").style.visibility="hidden";
         if((document.getElementById("<%=ddlTable.ClientID%>").value=="--Select--")|| (document.getElementById("<%=ddlTable.ClientID%>").value==""))
       {
            alert("Please Select Table Name!!!")
       }
        else if(document.getElementById("<%=txtFormula.ClientID%>").value=="")      
        {
            alert("Please Apply Condition!!!")
        }
       else
       {

    DataManagerAjax.Rowcount(document.getElementById("<%=ddlTable.ClientID%>").value,document.getElementById("<%=txtFormula.ClientID%>").value,getrow1);
         }
   
   }
   
   
   function getrow1(Response)
   {
        var rowcount=Response.value
        var finalvalue=rowcount.split(",")
        if (finalvalue[0]=="f")
        {
        alert(finalvalue[1])
        }
        else if(rowcount=="0")
        {
       
         document.getElementById("<%=lblshow.ClientID%>").value=+ rowcount + " Row(s) are going to be affected!!! ";
         document.getElementById("btnshowrows").style.visibility="hidden";
         document.getElementById("<%=divshow.ClientID%>").style.visibility="visible";
        
        }
        else
        {
        document.getElementById("<%=lblshow.ClientID%>").value=+ rowcount + " Row(s) are going to get affected. Click View button to view the affected rows";
        document.getElementById("<%=divshow.ClientID%>").style.visibility="visible";
         document.getElementById("btnshowrows").style.visibility="visible";
           
        }
        
   }
   

   
function View()  //function to show grid view data on FinalizeData.aspx
{
    
    var tablename=document.getElementById("<%=ddlTable.ClientID%>").value;
    var formula=document.getElementById("<%=txtFormula.ClientID%>").value;
            if (formula=="")
            {
         alert("Conditions Has Been Deleted!!!");
             }
         else
         {
   window.open("FinalizeData.aspx?Tablename="+ tablename+ "&Formula=" + formula,"View","width=600px,height=500px,scrollbars=yes");
    document.getElementById("<%=divPurbcp.ClientID%>").style.visibility="visible";
        }
}

function bcpdata() // function to  redirect the page to blank.aspx
{

    var tablename=document.getElementById("<%=ddlTable.ClientID%>").value;
    var formula=document.getElementById("<%=txtFormula.ClientID%>").value;
    if (formula=="")
    {
  alert("Conditions Has Been Deleted!!!");
    }
    else
    {
    window.open("blank.aspx?Tablename="+ tablename+ "&Formula=" + formula + "&Deptid=" + document.getElementById("<%=ddlDept.ClientID%>").value + "&Clientid=" + document.getElementById("<%=ddlClient.ClientID%>").value + "&Lobid=" + document.getElementById("<%=ddlLOB.ClientID%>").value,"Bcp","width=600px,height=500px");
   // window.open("FinalizeData.aspx?Tablename="+ tablename+ "&Formula=" + formula,"View","width=600px,height=500px,scrollbars=yes");
    document.getElementById("<%=pnlpurgebcp.ClientID%>").style.visibility="hidden";
    
    document.getElementById("<%=btnPurge.ClientID%>").style.visibility="hidden";
    document.getElementById("<%=btnPurgbcp.ClientID%>").style.visibility="hidden";
    
    //window.opener.close;
//    window.location.href  = 'DataPurging.aspx';
    }
     
}


function showpurge() //function to show/hide  panel
{
    
    document.getElementById("<%=pnlpurgebcp.ClientID%>").style.visibility="hidden";
    document.getElementById("<%=Pnlpurge.ClientID%>").style.visibility="visible";
    

  
}

function purgebcpshow() //function to show panel pnlpurgebcp
{
    document.getElementById("<%=pnlpurgebcp.ClientID%>").style.visibility="visible";
    document.getElementById("<%=Pnlpurge.ClientID%>").style.visibility="hidden";


}

function purgeno() // Function to close Panel
{
    //document.getElementById("Pnlpurge").style.visibility="hidden";
    document.getElementById("<%=Pnlpurge.ClientID%>").style.visibility="hidden";
}


function purgebcpno() // Function to close Panel
{
    //document.getElementById("pnlpurgebcp").style.visibility="hidden";
    document.getElementById("<%=pnlpurgebcp.ClientID%>").style.visibility="hidden";
    //document.getElementById("<%=Pnlpurge.ClientID%>").style.visibility="hidden";
    
}


function clearvalue()// Function to Clear Controls
{
 
            for(i=document.getElementById("<%=cbofdata.ClientId%>").length;i>=0;i--)
				{
					document.getElementById("<%=cbofdata.ClientId%>").remove(i);
				}
				
			document.getElementById("<%=txtFormula.ClientId%>").value=""
			document.getElementById("<%=divPurbcp.ClientID%>").style.visibility="hidden";
			document.getElementById("<%=divshow.ClientID%>").style.visibility="hidden";
			

}

</script>

<table cellpadding="0"  cellspacing="0" class="table" id="TABLE1" summary="Displaying Span and  Tables "> 
<caption>Data Purge</caption>
    <tr>
        <td scope="row" colspan="6"  dir="ltr"></td>
    </tr>
    <tr>
        <td   scope="row" dir="ltr" align="center" >
        <table id="spandisplay" runat="server" visible="false">
        <tr>
        <td>
        <%--<label id="lblDept" for="ctl00_MainPlaceHolder_ddlDept"   class="label" title="Select Department">Department</label>--%>
        <asp:label ID="lblDept" Text="Department" runat="server"></asp:label>
        </td>
        <td  scope="row"  dir="ltr" title="Select Department">
        <asp:DropDownList ID="ddlDept" runat="server" CssClass="dropdownlist"  ></asp:DropDownList>
         </td> 
         <td  scope="row"  align="center"  dir="ltr" title="select Client" >
        <%--<label id="lblClient" for="ctl00_MainPlaceHolder_ddlClient"  class="label" title="Select Client">Client</label>--%>
        <asp:label ID="lblClient" Text="Client" runat="server"  Width="100px"></asp:label>
        </td>
         <td  scope="row" align="center"  dir="ltr">
        <asp:DropDownList ID="ddlClient" runat="server"  ToolTip="Select Client"  CssClass="dropdownlist" ></asp:DropDownList>
        </td>
        <td  scope="row" align="center" dir="ltr" style="width: 53px">
        <%-- <label id="lblLob" for="ctl00_MainPlaceHolder_ddlLob"  title="Select LOB" class="label">LOB</label>--%>
         <asp:label ID="lblLob" Text="LOB" runat="server" ></asp:label>
        </td>
         <td dir="ltr" scope="row" title="Select LOB">
        <asp:DropDownList ID="ddlLob" runat="server" CssClass="dropdownlist" ToolTip="Select LOB"></asp:DropDownList>
        </td>
        </tr>
        </table>
        </td>
        <td>
            <input id="get_table" type="button" value="Get Tables" onclick="bindlisttab1()" 
                class="button" runat="server"/></td>
    </tr>
    <tr>
        <td style="height:20px" colspan="6"></td>
    </tr>
    <tr>
        <td scope="row" align="center"><label id="ldlTable" for="ctl00_MainPlaceHolder_ddlTable"  title="Select Table" class="label">Select Table</label></td>
        <td>
        <asp:DropDownList ID="ddlTable" CssClass="dropdownlist"  ToolTip="Select Table" runat="server"></asp:DropDownList>
        </td>
     
    </tr>
 </table>
<table cellpadding="0"  cellspacing="0"  width="90%" style="margin-left:30px; margin-right:30px"  summary="Contains Condition Span">
    
    <tr>
        <td style="height:30px" colspan="6">
        
        </td>
    </tr>
</table>
 <table id="Formulatab" cellpadding="0"  cellspacing="0"  width="90%" style="margin-left:30px; margin-right:30px"  summary="Formula Bar">
        <caption>Where</caption>
      
       <tr>
       
               <td >
			    <%--<input id="btnPlus" class="button" type="button" value="+" style="WIDTH:25px; height: 25px;" lang="javascript" onclick="setFormula('+')" title="Click to add an operator"/>
		         <input id="btnMinus" class="button" type="button" value="-" style="WIDTH:25px; height: 25px;" lang="javascript" onclick="setFormula('-')" title="Click to add an operator"/> --%>   
			    <input id="btnLetn" class="button" type="button" value="<" style="WIDTH:25px; height: 25px" lang="javascript" onclick="setFormula('<')" title="Click to add an operator"/>
				<input id="btnGtn" class="button" type="button" value=">" style="WIDTH:25px; height: 25px;" lang="javascript" onclick="setFormula('>')" title="Click to add an operator"/>   
				 <%--<input id="btnMultiply" class="button" type="button" value="*" style="WIDTH:25px; height: 25px;" lang="javascript" onclick="setFormula('*')" title="Click to add an operator"/>--%>
				 <input id="btnAnd" class="button" type="button" value="&" style="WIDTH:25px; height: 25px;" lang="javascript" onclick="setFormula('and')" title="Click to add an operator"/>   
				<input id="btnOr" class="button" type="button" value="||" style="WIDTH:25px; height: 25px;" lang="javascript" onclick="setFormula('or')" title="Click to add an operator"/>    
				  <input id="btnOpen" class="button" type="button" value="(" style="WIDTH:25px; height: 25px;" lang="javascript" onclick="setFormula('(')" title="Click to add an operator"/>
				  <input id="btnClose" class="button" type="button" value=")" style="WIDTH:25px; height: 25px;" lang="javascript" onclick="setFormula(')')" title="Click to add an operator"/>   
				  <input id="btnEqual" class="button" type="button" value="=" style="WIDTH:25px; height: 25px;" lang="javascript" onclick="setFormula('=')" title="Click to add an operator"/>   
				  <input id="btnLike" class="button" type="button" value="Like" style="WIDTH:50px; height: 25px;" lang="javascript" onclick="setFormula('Like% ')" title="Click to add an operator"/>    
				 <input id="btnBetween" class="button" type="button" value="Between" style="WIDTH:70px; height: 25px;" lang="javascript" onclick="setFormula('Between')" title="Click to add an operator"/>  
				  
		    </td>
			 	            
            <td style="width:45%; height: 34px;" scope="row">
            <label id ="lbladdcolumn1" for="ctl00_MainPlaceHolder_txtFormula" class="label">Click to add columns to the formula fields:</label>
            </td>
        </tr>
        <tr>
      	    <td style="width: 32px;" title="Formula" scope="col" valign="top">
		    <textarea id="txtFormula" style="width:340px;height:150px; background-color:white" cols="1"  rows="100" class="textbox" runat="server"></textarea>        
		    </td>
		    <td>
        <label id ="lbladdcolumn12" for="ctl00_MainPlaceHolder_cbofdata" class="label"></label>
         
            <select id="cbofdata"  runat="server" name="cbofdata" style="BORDER-TOP-STYLE: outset; BORDER-RIGHT-STYLE: outset; BORDER-LEFT-STYLE: outset; LIST-STYLE-TYPE:disc ; BORDER-BOTTOM-STYLE: outset; width: 300px;height:150px" lang="javascript" onclick="return cbofdata_onclick()" size="12"></select>                    
           </td>
					                    
        
        </tr>
     
 </table>

<table cellpadding="0"  cellspacing="0"  width="90%" style="margin-left:30px; margin-right:30px"  summary="Contain Process Button">
    <tr>
        <td style="height:15px" colspan="6">
        
        </td>
    </tr>
    <tr>
        <td  align="left">
         <input type="button" id="btnProcess"  value="Process" runat="server" title="Click to Apply Condition" onclick="javascript:rowcount();" class="button"/>
         <input type="button" id="btnProcess_singleuser"  value="Process" visible="false" runat="server"   title="Click to Apply Condition" onclick="javascript:rowcount1();" class="button"/></td>
        
          
    </tr>
    <tr>
        <td style="height:30px" colspan="6">
        
        </td>
    </tr>
</table>
<div id="divshow" runat="server" style="border-style:solid;border-width: thin; border-color:#42969f;margin-left:30px;margin-right:30px; width:90%; visibility:hidden" >
    <table style="width:90%" cellpadding="0" cellspacing="0" summary="To show no. of row affected">
        <tr>

            <td  align="center"  scope="row">
            <label  for="ctl00_MainPlaceHolder_lblshow" id="lblpaging12" title="show" ></label>
            <input  type="text"  readonly="readonly"  id="lblshow" runat="server" style="width:100%;border-style:none;text-align:center"  class="textbox"/>
            </td>
        </tr>
        <tr>
            <td scope="row"></td>
        </tr>
        <tr>
            <td align="center" scope="row">
            <input type="button" id="btnshowrows"  value="view"    class="button"   onclick="javascript:View();" title="Click to View All Data which are going to be affected" />
            </td>
        </tr>
    </table>
</div>

<%--<div id="divPagingvalue" runat="server"  style="margin-left:30px;margin-right:30px; width:90%;height:20%; visibility:hidden">
    <table cellpadding="0"  cellspacing="0"  width="90%" style="margin-left:30px; margin-right:30px"  summary="Displaying Span and ListBox to show Tables and columns">
        <tr>
             <td>
                <label  for="txtpageing" id="lblpaging" title="Show" >Paging</label></td>
                <td><input type="text" id="txtpageing" runat="server" />
         </td>
         <td >
               <input type="button" id="btnAPaging"  value="Apply Paging" runat="server"  class="button"/>
                </td>

        </tr>
        <tr>
    
            <td style="height:30px"></td>
         </tr>

    </table>
</div>--%>
<%--<div id="divgrd" runat="server"  style="margin-left:30px;margin-right:30px; width:90%;height:20%;overflow:scroll">
<table cellpadding="0"  cellspacing="0"  width="90%" style="margin-left:30px; margin-right:30px"  summary="Displaying Span and ListBox to show Tables and columns">
    <tr>
        <td>
        <asp:GridView ID="grdshow" AllowPaging="true"  AutoGenerateColumns="true" CssClass="datagridpurge"  runat="server" >
        
        </asp:GridView>
        
        </td>
    </tr>


</table>
</div>--%>
<div id="Pnlpurge" runat="server" style="Z-INDEX: 101; LEFT: 481px;  POSITION: absolute;bottom:251px; border-style:outset; border-width:1px; width:272px; height:68px; visibility:hidden" > 
    <table width="100px" style="width:272px; height:78px ; background-color:#f5f5f5; border-top-width: 1px; border-left-width: 1px; border-left-color: #f5f5f5; border-bottom-width: 1px; border-bottom-color: #f5f5f5; border-top-color: #f5f5f5; border-right-width: 1px; border-right-color: #f5f5f5;" summary="To show Message in panel Pnlpurge">
        <tr>
            <td style="height: 19px" scope="row"></td>
        </tr>
        <tr>
            <td  align="center"><label id="lblPurge" for="Pnlpurge" class="label" title="show">Are You sure You want to Purge Data Without Taking Backup!!!</label>
            </td>
        </tr>
        <tr>
            <td style="height: 18px" scope="row"></td>
        </tr>
        <tr>
            <td align="center" style="height: 27px" scope="row">
            <input type="button" id="btnpurgey" value="Yes"  class="buttonnew" runat="server" title="Click to Delete Data" style="width: 59px"/>
            <input type="button" id="btnpurgen" value="No"  class="buttonnew" runat="server"   onclick="purgeno();" title="Click to Cancel Purging of Data" style="width: 51px"/>
            </td>
        </tr>
    </table>
</div>
<div id="divPurbcp" runat="server" style="margin-left:30px;margin-right:30px; width:90%; visibility:hidden" >
    <table cellpadding="0"  cellspacing="0"  width="90%" style="margin-left:30px; margin-right:30px"  summary="Displaying Span and ListBox to show Tables and columns">

    <tr>
        <td style="height:30px" scope ="row">

        </td>
   </tr>
    <tr>
        <td align="center" scope="row">
        <input type="button" id="btnPurge" runat="server" value="Purge"  title="Click To Purge Data" onclick="javascript:showpurge();" class="button"/>
        <input type="button" id="btnPurgbcp" runat="server"  value="Purge/Backup"   title="Click to Purge Data With BackUp" onclick="javascript:purgebcpshow();" class="button"/>
        </td>
    </tr>

</table>
</div>
<div id="pnlpurgebcp"   runat="server"  style="Z-INDEX: 101; LEFT: 482px;  POSITION: absolute;bottom:242px; border-style:outset; border-width:1px; width:272px; height:1px; visibility:hidden"  >
    <table width="100%" style="border-style:none; border-width:1px; width:272px; height:84px ; border-color: #f5f5f5; background-color:#f5f5f5" summary="To show message in panel pnlpurgebcp">
        <tr>
            <td style="width:20px; height: 17px;" scope="row"></td>
        </tr>
        <tr>
            <td scope="row"></td>
        </tr>
        <tr>
            <td  align="center" style="height: 34px" scope="row"><label id="lblPurgebackup" for="pnlpurgebcp" class="label" title="show">Are you sure you want to  Purge/Backup Data !!!</label>
            </td>
        </tr>
        <tr>
            <td scope="row"></td>
        </tr>
        <tr>
            <td align="center" valign="middle" scope="row">
            <input type="button" id="btnpubcy" value="Yes"  class="buttonnew" runat="server" onclick="javascript:bcpdata();" title="Click to purge and BackUp" style="width: 59px"/>
            <input type="button"  id="btnpubcn" value="No"  class="buttonnew" runat="server"   onclick="purgebcpno();"  title="Click to Cancel purge and BackUp" style="width: 51px"/>
            </td>
        </tr>
    </table>
</div>
<table summary="Contain Hidden Fields">
    <tr>
        <td scope="row">
         <input type="hidden" id="hidTablename" runat="server" />
         <input type="hidden" id="hidQuery" runat="server" />
         <input type="hidden" id="hidpagevalue" runat="server" />
        </td>
    </tr>
     <tr>
        <td scope="row">
         <input type="hidden" id="hidClientId" runat="server" />
         <input type="hidden" id="hidLobId" runat="server" />
        
        </td>
    </tr>

</table>
    <br />
   
</asp:Content>

