<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="Welcome.aspx.vb" Inherits="QueryBuilder_Welcome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">
    <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
    <script language="javascript" type="text/javascript">
    
var fieldname
strLastPanName=""
var resNew
var i=0;
valget=""
//declare strings for div array
//array string of each div
strdivele0=""
strdivele1=""
strdivele2=""
strdivele3=""
//captures the div id to mactch whether button clicked or drop occered
divmousedown=""
divmouseup=""
strbtnid="" // keeps the btnid that was clicked last and after drop should be empty
btnclicked=""
var isTableFieldOpen;
window.isTableFieldOpen="";

function openformula()
{
var Table=document.getElementById("hidtablename").value;
    if (Table=="")
    {
    alert("Drag some column first");
    }
    else
    {
        window.open("formula.aspx?data="+btnclicked+"&Table="+Table,'Formula',"width=500px,height=500px");
    //        binddivs();
      }
}
//function setValues()
//{

//     if(document.getElementById("hiddept").value!="")
//     {      
//     var suvid=document.getElementById("hiddept").value;  
//        document.getElementById("=ddlDept.ClientID%>").value=document.getElementById("hiddept").value;
// 
//     } 
//     if(document.getElementById("hidclient").value!="")  
//     {
//        var suvic=document.getElementById("hidclient").value;
//        document.getElementById("=ddlClient.ClientID%>").value=document.getElementById("hidclient").value;
//     }
//     if(document.getElementById("hidlob").value!="")
//     {
//var suvil=document.getElementById("hidlob").value;
//        document.getElementById("=ddlLob.ClientID%>").value=document.getElementById("hidlob").value;
//     }
//     
//}


//function getclient() // function to bind client 
//{
// 
//    setValues();
//    AjaxSearchBind.bindClientOnDept(document.getElementById("=ddlDept.ClientID%>").value,fillclient);
//    getdata();
//    if (document.getElementById("=ddlLob.ClientId%>").value!="--Select--")
//    {
//        GetLOB();
//    }
//}
		
//function fillclient(Response)
//{
//  
//    for(i=document.getElementById("=ddlClient.ClientId%>").length;i>=0;i--)
//    {
//        document.getElementById("=ddlClient.ClientId%>").remove(i);
//    }
//    var ds = Response.value
//				
//    if(ds!= null && typeof(ds) == "object" && ds.Tables!=null)
//    {
//        document.getElementById("=ddlClient.ClientId%>").options[0]=new Option("--Select--");
//        for(i=0;i<ds.Tables[0].Rows.length;i++)
//        {
//            document.getElementById("=ddlClient.ClientId%>").options[i+1]=new Option(ds.Tables[0].Rows[i].ClientName,ds.Tables[0].Rows[i].AutoId);
//        }
//    }
//}

//function GetLOB() // function to bind LOB
//{
//    
//    setValues();
//    AjaxSearchBind.bindLobOnDeptClient(document.getElementById("=ddlDept.ClientID%>").value,document.getElementById("=ddlClient.ClientID%>").value,filllob);
//    getdata();
//}

//function filllob(Response)
//{
// 
//	for(i=document.getElementById("=ddlLob.ClientId%>").length;i>=0;i--)
//    {
//        document.getElementById("=ddlLob.ClientId%>").remove(i);
//    }
//    var ds = Response.value
//				
//    if(ds!= null && typeof(ds) == "object" && ds.Tables!=null)
//    {
//        document.getElementById("=ddlLob.ClientId%>").options[0]=new Option("--Select--");
//        for(i=0;i<ds.Tables[0].Rows.length;i++)
//        {
//            document.getElementById("=ddlLob.ClientId%>").options[i+1]=new Option(ds.Tables[0].Rows[i].LOBName,ds.Tables[0].Rows[i].AutoID);
//        }
//    }
//}

//function getdata()
//{


// var uid="=Session("userid") %>" ;
// var type= "=Session("typeofuser") %>" ;
// 
//     if (type == "Admin") 
//     {
//     QueryBuilder.tableForadmin(uid, document.getElementById("=ddlDept.ClientId%>").value,document.getElementById("=ddlClient.ClientId%>").value,document.getElementById("=ddlLob.ClientId%>").value,filltable)
//     }
////     else if (type == "User") 
//       else
//     {
//     
//    QueryBuilder.chkUserscope(uid, document.getElementById("<=ddlDept.ClientId%>").value,document.getElementById("=ddlClient.ClientId%>").value,document.getElementById("=ddlLob.ClientId%>").value,tabstatus)
//      }
////    QueryBuilder.Bind_Table(document.getElementById("<=ddlDept.ClientId%>").value,document.getElementById("<=ddlClient.ClientId%>").value,document.getElementById("<=ddlLob.ClientId%>").value,filltable)
//}
	function tabstatus()
	{

	 var uid="<%=Session("userid") %>" ;
 var type= "<%=Session("typeofuser") %>" ;
	 var table=document.getElementById("<%=ddlTablename.ClientID%>").value;
	    QueryBuilder.tableFornonlocal(uid,table,filltable)
	    
	}
function getdataForSavedQuery()
{


    //setValues();
      
    //var suvi1=document.getElementById("=ddlDept.ClientID%>").value;
    //var suvi2=document.getElementById("=ddlClient.ClientID%>").value;
    //var suvi3=document.getElementById("=ddlLob.ClientID%>").value;
    //QueryBuilder.Bind_Table(document.getElementById("<=ddlDept.ClientId%>").value,document.getElementById("<=ddlClient.ClientId%>").value,document.getElementById("<=ddlLob.ClientId%>").value,filltable)
    QueryBuilder.Bind_Table( document.getElementById("hiddept").value,document.getElementById("hidclient").value,document.getElementById("hidlob").value ,filltable)
}	
function getspansaved(tablename)
{
 QueryBuilder.GetSpanForSavedQuery(tablename,getdatapath)
}
function getdatapath(response)
{

document.getElementById("spanget").innerText=response.value;
}

function filltable(res)
{

    if(res.value!=null)
    {
         
        var result=res.value;
        var arrt=result.split(",");
        var newdiv = document.getElementById("<%=myDiv.ClientID%>");  
        var str="" ;
        var tab="";
        var str=str + '<table>';
        for(i=0;i<arrt.length;i++)
        {
            if(arrt[i]!="")
            {
            str = str + '<tr><td id='+arrt[i]+'><input type="button" title="Click to expand this table" class="buttonTable"  value="'+ arrt[i] +'" id="'+ arrt[i] +'" class="buttonTable" onclick="get_field('+arrt[i]+');"/></td></tr>'
            }
        }
        str=str + '</table>';
        document.getElementById("<%=myDiv.ClientID%>").innerHTML= str;
    }
}
function get_fieldforsavedQuery(tablename)
{

    document.getElementById("hidtablename").value= tablename;
    QueryBuilder.GetTableFields(tablename, pasteField);
}

function get_field(strcolnames)
{

    if(window.isTableFieldOpen=="")
    {
       
        var val=""
        if(document.getElementById("hidReportmode").value=="Edit")
        {
            val=strcolnames;
        }
        else
        {
            val=strcolnames.value;  
        }
        //strcolnames.disabled=true; 
         document.getElementById("hidtablename").value= val;
        QueryBuilder.GetTableFields(val, pasteField);
        //QueryBuilder.GetTableFields(val, pasteField);
        var al=QueryBuilder.GetDatetypeColumns(val)
         if(al != "")
         { 
           binddatecolumnname(al)
         }
        
    }
    else
    {
        document.getElementById(dname).innerHTML=window.isTableFieldOpen; 
        window.isTableFieldOpen = "";         
    }
}

function pasteField(response)
{

 
    if (response!="")
    {
        var res=response.value;
        
               
        if(res=="Null")
        {
            alert("No columnname found.");
        }   
        else
        {
           
            if (document.getElementById("hidcolstr").value!="")
            {
            
             var newdiv = document.getElementById(dname);
                newdiv.innerHTML=""
             //str=""
                          
             }
            
           
            resNew=res.split("$"); // Split all the columns 
            //var res2=resNew[0].split("$"); // Split to get the column name       
            //dname=res2[0]; // Search for the existing division of the table
          dname=  document.getElementById("hidtablename").value
             newdiv = document.getElementById(dname);
            str="" ;
            var str=str + '<table>';
            for(i=0;i<=resNew.length-1;i++)
            {
                //var col=resNew[i].split("$"); // Split to get the column name
                var tb="";
                 str = str + '<tr><td id='+resNew[i]+'><input type="button" title="'+resNew[i]+'" class="buttonField" style="height:20px;"  onmouseup="upmousetablecolum();" onmousedown= "setupdrag1('+i+');"  id="'+resNew[i]+'" value="'+resNew[i]+'"/></td></tr>';
            }
            var str=str + '</table>';
             window.isTableFieldOpen=newdiv.innerHTML;
                newdiv.innerHTML=newdiv.innerHTML + str;
            
            //document.getElementById("hidcolstr").value=res
        }  
        
    }
}

function fillPanName(DivObj)
{
    strLastPanName = DivObj
}

//	
function setupdrag1(i)
{


    valget=resNew[i]
    
    
}
function setupdragd(i)
{

//   alert(i.innerHTML);
//   strbtnid=

var valuetocheck=event.srcElement.id;
if (valuetocheck=="blank2" || valuetocheck=="blank1" || valuetocheck=="blank0" || valuetocheck=="blank3" )
{
}
else
{
     valget=event.srcElement.id;
     }
   // alert(divmousedown);
   
  setupdivdrag();
  //setupdrag1(valget);
}
function upmousetablecolum()
{


    valget="";
    
    
}
function handleDrop()
{


    var strcurrentdiv=strLastPanName.id
    if (divmousedown==strcurrentdiv)//id true handle as click of button
    {
     var Table=document.getElementById("hidtablename").value;
     if(strcurrentdiv=="blank2")
      {
         if (strdivele2=="")
            {
            strdivele2=btnclicked;
            }
         else
            {
            strdivele2=strdivele2+ ","+ btnclicked;
            }
     
      
      binddivs();
      }
      if(strcurrentdiv=="blank0")
      {
         if (strdivele0=="")
            {
            strdivele0=btnclicked;
            }
         else
            {
            strdivele0=strdivele0+ ","+ btnclicked;
            }
     
      window.open("where.aspx?data="+btnclicked+"&Table="+Table,'Where',"width=500px,height=500px");
      binddivs();
      }
        if(strcurrentdiv=="blank1")
      {
        if (strdivele1=="")
            {
            strdivele1=btnclicked;
            }
         else
            {
            strdivele1=strdivele1+ ","+ btnclicked;
            }
//      window.open("formula.aspx?data="+btnclicked+"&Table="+Table,'Formula',"width=500px,height=500px");
        binddivs();
      }
        if(strcurrentdiv=="blank3")
      {
        if (strdivele3=="")
            {
            strdivele3=btnclicked;
            }
         else
            {
            strdivele3=strdivele3+ ","+ btnclicked;
          }
          var bbbb="blank3"
       var old =btnclicked;
           var apnadata=btnclicked.split("(");
 
           if (apnadata.length>1)
           {
           var newdata=apnadata[1].split(")");
           btnclicked=newdata[0];
            
              }
         
      window.open("data.aspx?data="+btnclicked+"&old="+old+"&all="+document.getElementById("Showdata").value+"&btn="+btnclicked+"&div="+bbbb,'Data',"width=500px,height=500px");
        binddivs();
      }
        
          
    }
    
    else
    {
   
        if(divmousedown!="") 
            setupdivdrag();           
           
           adddivale();
            binddivs();
            
        
    }
     valget="";
    divmousedown="";
    divmouseup="";
    btnclicked=""
}
//Bind Div For Saved Query

function binddivsForSavedQuery()
{   
      
        bindblank0ForSavedQuery();
        bindblank1ForSavedQuery();
        bindblank2ForSavedQuery();
        bindblank3ForSavedQuery();
        FillSavedFormula();
       
}

function bindblank0ForSavedQuery()
{  

   if(document.getElementById("Savedwhere").value!="")
    {
        strdivele0=document.getElementById("Savedwhere").value;   
    }
    blank0.innerHTML="";
    var strblank0=strdivele0.split(",");
    var tb="";
    if (strdivele0!="")
    {
        for(i=0;i<=strblank0.length-1;i++)
        {
            tb=tb+'<input type="button"  class="buttonFieldquery" id="'+ strblank0[i] +' "  value="'+ strblank0[i] +' " onmousedown="capdivmousedown();" />';
        }
        blank0.innerHTML=blank0.innerHTML + tb;
    }

}

function bindblank1ForSavedQuery()
{

  
    if(document.getElementById("column").value!="")
    {
        strdivele1=document.getElementById("column").value;   
    }
    blank1.innerHTML="";
    var strblank1=strdivele1.split(",");
    var tb="";
    if (strdivele1!="")
    {
        for(i=0;i<=strblank1.length-1;i++)
        {
            tb=tb+'<input type="button"  class="buttonFieldquery" id="'+ strblank1[i] +' "  value="'+ strblank1[i] +' " onmousedown="capdivmousedown();" />';
        }
        blank1.innerHTML=blank1.innerHTML + tb;
    }

}

function bindblank2ForSavedQuery()
{

   if(document.getElementById("crdata").value!="")
    {
        strdivele2=document.getElementById("crdata").value;   
    }
  blank2.innerHTML="";
  
    var strblank2=strdivele2.split(",");
    var tb="";
    if (strdivele2!="")
    {
        for(i=0;i<=strblank2.length-1;i++)
        {
            tb=tb+'<input type="button"  class="buttonFieldquery" id="'+ strblank2[i] +' "  value="'+ strblank2[i] +' " onmousedown="capdivmousedown();" />';
        }
        blank2.innerHTML=blank2.innerHTML + tb;
    }

}

function bindblank3ForSavedQuery()
{

    if(document.getElementById("Showdata1").value!="")
    {
        strdivele3=document.getElementById("Showdata1").value;   
    }
    blank3.innerHTML="";
    var strblank3=strdivele3.split(",");
    var tb="";
    if (strdivele3!="")
    {
        for(i=0;i<=strblank3.length-1;i++)
        {
            tb=tb+'<input type="button"  class="buttonFieldquery" id="'+ strblank3[i] +' "  value="'+ strblank3[i] +' "  onmousedown="capdivmousedown();" />';
          
        }
    
        blank3.innerHTML=blank3.innerHTML + tb;
    }

}

//End

function binddivs()
{  
 
        btnclicked="";
        bindblank0();
        bindblank1();
        bindblank2();
        bindblank3();
       
}

function bindblank0()
{   

    blank0.innerHTML="";0
    var strblank0=strdivele0.split(",");
    //ranjit added start
    var ranjitadd=""
    for (y=0; y<=strblank0.length-1;y++)
    {
    var spl=strblank0[y].split("(");
   
        var forchk=spl[0].toLowerCase();
        if(spl.length>1)
        {
        var b="";
        }
            
                else
                {
                    if(ranjitadd=="")
                        {
                            ranjitadd=strblank0[y]
                        }
                        else
                        {
                        ranjitadd=ranjitadd + "," + strblank0[y]
                        }
                }
    }
    //code added start
    var chkwhere=ranjitadd.split(",");
    if (chkwhere.length>1)
    {
    alert("Where can contain only one column");
   
    }
    ranjitadd=chkwhere[0];
    ///stope
     var strblank0=ranjitadd.split(",");
     
     //stope
    var j= strblank0.length
    var last;
    var b=false;
    var allcolumn="";
  if (j>=2)
      {
      last=strblank0[strblank0.length-1];
    
      }
  for(p=0;p<=strblank0.length-2;p++)
  {  
  
              
               var chk=last.search(strblank0[p]) 
                 var l1=last.trim();;
                 var l2=strblank0[p].trim();;
         if(l1==l2)
             {
               if (chk!=-1)
               {
               b=true;
               }  
                chk=strblank0[p].search(last)                                                   
             if (chk!=-1)                                                           
               {   
               b=true;                                                            
                }
          }                                                                     
  }
  
  if (b==true)
  {
          for(k=0;k<=strblank0.length-2;k++)
          {
                if (allcolumn=="")
                {
                allcolumn=strblank0[k];
                }
                else
                 {
                       allcolumn=allcolumn + "," + strblank0[k];
                  }
            }
      }      
    if(b==false)
       {
       for(k=0;k<=strblank0.length-1;k++)
        {
            if (allcolumn=="")
            {
            allcolumn=strblank0[k];
            }
            else
             {
                   allcolumn=allcolumn + "," + strblank0[k];
              }
        }
      } 
  strdivele0=allcolumn;
   strblank0=allcolumn.split(",");
    document.getElementById("wheredata1").value=strdivele0;
    
    
    
    var tb="";
    if (strdivele0!="")
    {
        for(i=0;i<=strblank0.length-1;i++)
        {
       var tttrrr=strblank0[i].trim();
  
    tb=tb+'<input type="button"  class="buttonFieldquery" id="'+ tttrrr +' "  value="'+ tttrrr +' " onmouseup="delpreval();"  onmousedown="capdivmousedown();" />';
//            tb=tb+'<input type="button"  class="buttonFieldquery" id="'+ strblank0[i] +' "  value="'+ strblank0[i] +' " onmouseup="delpreval();"  onmousedown="capdivmousedown();" />';
//
//        onmouseup="upmousetablecolum();"     

        }
        blank0.innerHTML=blank0.innerHTML + tb;
    }

}

function bindblank1()
{

    blank1.innerHTML="";
    var strblank1=strdivele1.split(",");
    
    //ranjit added start
    var ranjitadd=""
    for (y=0; y<=strblank1.length-1;y++)
    {
    var spl=strblank1[y].split("(");
   
        var forchk=spl[0].toLowerCase();
        if(spl.length>1)
        {
        var b="";
        }
            
                else
                {
                    if(ranjitadd=="")
                        {
                            ranjitadd=strblank1[y]
                        }
                        else
                        {
                        ranjitadd=ranjitadd + "," + strblank1[y]
                        }
                }
    }
     var strblank1=ranjitadd.split(",");
     //stop
    var j= strblank1.length
    var last;
    var b=false;
    var allcolumn="";
  if (j>=2)
      {
      last=strblank1[strblank1.length-1];
    
      }
  for(p=0;p<=strblank1.length-2;p++)
  {  
  
              
               var chk=last.search(strblank1[p])
            var l1=last.trim();;
            var l2=strblank1[p].trim();;
         if(l1==l2)
             {    
               if (chk!=-1)
               {
               b=true;
               }  
                chk=strblank1[p].search(last)                                                   
                 if (chk!=-1)                                                           
                   {   
                   b=true;                                                            
                    } 
            }                                                                    
  }
  
  if (b==true)
  {
          for(k=0;k<=strblank1.length-2;k++)
          {
                if (allcolumn=="")
                {
                allcolumn=strblank1[k];
                }
                else
                 {
                       allcolumn=allcolumn + "," + strblank1[k];
                  }
            }
      }      
    if(b==false)
       {
       for(k=0;k<=strblank1.length-1;k++)
        {
            if (allcolumn=="")
            {
            allcolumn=strblank1[k];
            }
            else
             {
                   allcolumn=allcolumn + "," + strblank1[k];
              }
        }
      } 
  strdivele1=allcolumn;
   strblank1=allcolumn.split(",");
    document.getElementById("column").value=strdivele1;
    
    
    var tb="";
    if (strdivele1!="")
    {
        for(i=0;i<=strblank1.length-1;i++)
        {
         var tttrrr=strblank1[i].trim();
            tb=tb+'<input type="button"  class="buttonFieldquery" id="'+ tttrrr +' "  value="'+ tttrrr +' " onmouseup="delpreval();"  onmousedown="capdivmousedown();" />';
        }
//        onmouseup="delpreval();"
//        onmouseup="upmousetablecolum();"
        blank1.innerHTML=blank1.innerHTML + tb;
    }

}
//        var strvalue=checkvalue(strdivele2, strbtnid)
//                    if (strvalue==-1) //and check whether the element is not duplicated
//                    {
//                       strdivele2=strdivele2+ "," +strbtnid;
//                        alert('pakadlia');
//                    }
                    
//         alert(event.srcElement.id);
//        
//        alert(strblank2[i]);
//        var IdCum = event.srcElement.id
//        if (IdCum==strblank2[i])
//        {
//        alert('pakadlia');
//        }
function bindblank2()
{

  blank2.innerHTML="";
    var strblank2=strdivele2.split(",");
    //ranjit added start
    var ranjitadd=""
    for (y=0; y<=strblank2.length-1;y++)
    {
    var spl=strblank2[y].split("(");
   
        var forchk=spl[0].toLowerCase();
        if(spl.length>1)
        {
        var b="";
        }
            
                else
                {
                    if(ranjitadd=="")
                        {
                            ranjitadd=strblank2[y]
                        }
                        else
                        {
                        ranjitadd=ranjitadd + "," + strblank2[y]
                        }
                }
    }
     var strblank2=ranjitadd.split(",");
     //stope
//     alert(strdivele2);
    var j= strblank2.length
    var last;
    var b=false;
    var allcolumn="";
  if (j>=2)
      {
      last=strblank2[strblank2.length-1];
    
      }
  for(p=0;p<=strblank2.length-2;p++)
  {  
  
//             alert(strblank2[p] + "," + last);  
               var chk=last.search(strblank2[p]) 
               var l1=last.trim();;
                 var l2=strblank2[p].trim();;
         if(l1==l2)
             {
               if (chk!=-1)
               {
               b=true;
               }  
                chk=strblank2[p].search(last)                                                   
             if (chk!=-1)                                                           
               {   
               b=true;                                                            
                } 
          }                                                                    
  }
//  alert(b);
  if (b==true)
  {
          for(k=0;k<=strblank2.length-2;k++)
          {
                if (allcolumn=="")
                {
                allcolumn=strblank2[k];
                }
                else
                 {
                       allcolumn=allcolumn + "," + strblank2[k];
                  }
            }
      }      
    if(b==false)
       {
       for(k=0;k<=strblank2.length-1;k++)
        {
            if (allcolumn=="")
            {
            allcolumn=strblank2[k];
            }
            else
             {
                   allcolumn=allcolumn + "," + strblank2[k];
              }
        }
      } 
  strdivele2=allcolumn;
   strblank2=allcolumn.split(",");
   document.getElementById("crdata").value=strdivele2;
         var tb="";
            if (strdivele2!="")
            {
                for(i=0;i<=strblank2.length-1;i++)
                {
  var tttrrr=strblank2[i].trim();
  tb=tb+'<input type="button"  class="buttonFieldquery" id="'+ tttrrr +' "  value="'+ tttrrr +' " onmousedown="capdivmousedown(); "/>';
//                    tb=tb+'<input type="button"  class="buttonFieldquery" id="'+ strblank2[i] +' "  value="'+ strblank2[i] +' " onmousedown="capdivmousedown();"/>';
//             onmouseup="delpreval();"       onmousedown="capdivmousedown();"
                }
                blank2.innerHTML=blank2.innerHTML + tb;
            }
    

}

function bindblank3()
{
   
    blank3.innerHTML="";
    
    var strblank3=strdivele3.split(",");
    
    
    //ranjit added start
    var ranjitadd=""
    for (y=0; y<=strblank3.length-1;y++)
    {
    if (strblank3[y]=="")
    {
    return;
    }
    var spl=strblank3[y].split("(");
   
        var forchk=spl[0].toLowerCase();
        if(spl.length>1)
        {
                     if(ranjitadd=="")
                        {
                            ranjitadd=strblank3[y];
                        }
                        else
                        {
                        ranjitadd=ranjitadd + "," + strblank3[y];
                        }
            
        }
            
                else
                {
                    if(ranjitadd=="")
                        {
                            ranjitadd="Count(" + strblank3[y] + ")";
                        }
                        else
                        {
                        ranjitadd=ranjitadd + "," + "Count(" + strblank3[y] + ")";
                        }
                }
    }
     var strblank3=ranjitadd.split(",");
     //stope
    var j= strblank3.length
    var last;
    var b=false;
    var allcolumn="";
  if (j>=2)
      {
      last=strblank3[strblank3.length-1];
    
      }
  for(p=0;p<=strblank3.length-2;p++)
  {  
  
              
               var chk=last.search(strblank3[p]) 
               var l1=last.trim();;
                 var l2=strblank3[p].trim();;
         if(l1==l2)
             {
               if (chk!=-1)//if it  search
               {
               b=true;
               }  
                chk=strblank3[p].search(last)                                                   
             if (chk!=-1)                                                           
               {   
               b=true;                                                            
                }  
         }                                                                   
  }
  
  if (b==true)
  {
          for(k=0;k<=strblank3.length-2;k++)
          {
                if (allcolumn=="")
                {
                allcolumn=strblank3[k];
                }
                else
                 {
                       allcolumn=allcolumn + "," + strblank3[k];
                  }
            }
      }      
    if(b==false)
       {
       for(k=0;k<=strblank3.length-1;k++)
        {
            if (allcolumn=="")
            {
            allcolumn=strblank3[k];
            }
            else
             {
                   allcolumn=allcolumn + "," + strblank3[k];
              }
        }
      } 
  strdivele3=allcolumn;
   strblank3=allcolumn.split(",");
    
    document.getElementById("Showdata").value=strdivele3;
    
    
    
    var tb="";
    if (strdivele3!="")
    {
        for(i=0;i<=strblank3.length-1;i++)
        {
       var tttrrr=strblank3[i].trim();
        tb=tb+'<input type="button"  class="buttonFieldquery" id="'+ tttrrr +' "  value="'+ tttrrr +' " onmouseup="delpreval();"  onmousedown="capdivmousedown();" />';
//            tb=tb+'<input type="button"  class="buttonFieldquery" id="'+ strblank3[i] +' "  value="'+ strblank3[i] +' " onmouseup="delpreval();"  onmousedown="capdivmousedown();" />';
//onmouseup="delpreval();"
//           onmouseup="upmousetablecolum();"
        }
    
        blank3.innerHTML=blank3.innerHTML + tb;
    }

}

function delpreval()

{

// divmousedown=strLastPanName.id;
// strbtnid=event.srcElement.id
// if (divmousedown=="blank2")
//     {
//        if (strdivele2=="")
//            {
//            strdivele2=strbtnid;
//            }
//         else
//            {
//            strdivele2=strdivele2+ ","+ strbtnid;
//            }
//     }
 
  
strbtnid="";

}

function capdivmousedown()
{ 

    divmousedown=strLastPanName.id;
    strbtnid=event.srcElement.id
    strbtnid=strbtnid.trim();
   btnclicked=strbtnid;
   
//   var b=false;
//    var calall;
//    var temcol="";
//    var lasttoDelete=btnclicked;
//    if (divmousedown=="blank2")
//    {
//    
//   
//                     calall=strdivele2.split(",");
//        
//        
//                 for(i=0;i<=calall.length-1;i++)
//                   {
//                    b=false;
//                     var chk=calall[i].search(lasttoDelete) 
//                      if (chk!=-1)//if it  search
//                       {
//                       b=true;
//                       }  
//                     chk=lasttoDelete.search(calall[i]) 
//                        if (chk!=-1)//if it  search
//                       {
//                       b=true;
//                       }  
//                    if(b==false)
//                    {
//                    
//                        if(temcol=="")
//                        {
//                            temcol=calall[i].trim();
//                        }
//                        else
//                        {
//                            temcol=temcol+","+calall[i].trim();
//                        }
//                    }
//                }
//                    strdivele2=temcol;
//                  
//                   strblank2=temcol.split(",");
//                        temcol="";
//    
//    
//    
//    
//                 var tb="";
//            if (strdivele2!="")
//            {
//                        for(i=0;i<=strblank2.length-1;i++)
//                        {
//                       
//                            tb=tb+'<input type="button"  class="buttonFieldquery" id="'+ strblank2[i] +' "  value="'+ strblank2[i] +' "  onmousedown="capdivmousedown();"  />';
////                          onmouseup="delpreval();"

//                        }
//                    blank2.innerHTML=""
//                        blank2.innerHTML=blank2.innerHTML + tb;
//            }
//      }
//      if (divmousedown=="blank1")
//    {
//    
//   
//                     calall=strdivele1.split(",");
//        
//        
//                 for(i=0;i<=calall.length-1;i++)
//                   {
//                     var chk=calall[i].search(lasttoDelete) 
//                      if (chk!=-1)//if it  search
//                       {
//                       b=true;
//                       }  
//                     chk=lasttoDelete.search(calall[i]) 
//                        if (chk!=-1)//if it  search
//                       {
//                       b=true;
//                       }  
//                    if(b==false)
//                    {
//                    
//                        if(temcol=="")
//                        {
//                            temcol=calall[i].trim();
//                        }
//                        else
//                        {
//                            temcol=temcol+","+calall[i].trim();
//                        }
//                    }
//                }
//                    strdivele1=temcol;
//                  
//                   strblank1=temcol.split(",");
//                        temcol="";
//    
//    
//    
//    
//                 var tb="";
//            if (strdivele1!="")
//            {
//                        for(i=0;i<=strblank1.length-1;i++)
//                        {
//                       
//                            tb=tb+'<input type="button"  class="buttonFieldquery" id="'+ strblank1[i] +' "  value="'+ strblank1[i] +' " onmouseup="delpreval();"  onmousedown="capdivmousedown();" />';
////                          onmouseup="delpreval();"

//                        }
//                     blank1.innerHTML=""
//                     
//                        blank1.innerHTML=blank1.innerHTML + tb;
//            }
//      }
//      if (divmousedown=="blank0")
//    {
//    
//   
//                     calall=strdivele0.split(",");
//        
//        
//                 for(i=0;i<=calall.length-1;i++)
//                   {
//                     var chk=calall[i].search(lasttoDelete) 
//                      if (chk!=-1)//if it  search
//                       {
//                       b=true;
//                       }  
//                     chk=lasttoDelete.search(calall[i]) 
//                        if (chk!=-1)//if it  search
//                       {
//                       b=true;
//                       }  
//                    if(b==false)
//                    {
//                    
//                        if(temcol=="")
//                        {
//                            temcol=calall[i].trim();
//                        }
//                        else
//                        {
//                            temcol=temcol+","+calall[i].trim();
//                        }
//                    }
//                }
//                    strdivele0=temcol;
//                  
//                   strblank0=temcol.split(",");
//                        temcol="";
//    
//    
//    
//    
//                 var tb="";
//            if (strdivele0!="")
//            {
//                        for(i=0;i<=strblank0.length-1;i++)
//                        {
//                       
//                            tb=tb+'<input type="button"  class="buttonFieldquery" id="'+ strblank0[i] +' "  value="'+ strblank0[i] +' "  onmouseup="delpreval();"  onmousedown="capdivmousedown();" />';
////                          onmouseup="delpreval();"
//                        }
//                     blank0.innerHTML=""
//                        blank0.innerHTML=blank0.innerHTML + tb;
//            }
//      }
//       if (divmousedown=="blank3")
//    {
//    
//   
//                     calall=strdivele3.split(",");
//        
//        
//                 for(i=0;i<=calall.length-1;i++)
//                   {
//                     var chk=calall[i].search(lasttoDelete) 
//                      if (chk!=-1)//if it  search
//                       {
//                       b=true;
//                       }  
//                     chk=lasttoDelete.search(calall[i]) 
//                        if (chk!=-1)//if it  search
//                       {
//                       b=true;
//                       }  
//                    if(b==false)
//                    {
//                    
//                        if(temcol=="")
//                        {
//                            temcol=calall[i].trim();
//                        }
//                        else
//                        {
//                            temcol=temcol+","+calall[i].trim();
//                        }
//                    }
//                }
//                    strdivele3=temcol;
//                  
//                   strblank3=temcol.split(",");
//                        temcol="";
//    
//    
//    
//    
//                 var tb="";
//            if (strdivele3!="")
//            {
//                        for(i=0;i<=strblank3.length-1;i++)
//                        {
//                       
//                            tb=tb+'<input type="button"  class="buttonFieldquery" id="'+ strblank3[i] +' "  value="'+ strblank3[i] +' " onmouseup="delpreval();"  onmousedown="capdivmousedown();" />';
////                          onmouseup="delpreval();"
//                        }
//                     blank3.innerHTML=""
//                        blank3.innerHTML=blank3.innerHTML + tb;
//            }
//      }
}

function capdivmouseup()
{
divmouseup=strLastPanName.id;
}

function adddivale()
{
//valget=strbtnid;

    if (strLastPanName.id=="blank0")
    {
        if(strbtnid!="")//means button was pressed under div
        {
           if (strdivele0=="")//this is the first element to be droped
            {
                strdivele0=strbtnid; //concatenating the particular div's string
            }
            else
            {
                var strvalue=checkvalue(strdivele0, strbtnid)
                    if (strvalue==-1) //and check whether the element is not duplicated
                    {
                       strdivele0=strdivele0+ "," +strbtnid;
                    }
                
            }
            
            strbtnid="";
        }
        else //means button was pressed from menu
        {
            
            if(valget!="")
            {
                if(strdivele0=="") //div is empty and simply create the element in the div
                {                   
                        strdivele0=valget;
                }
                 else //div already contains some elements
                {
                    var strvalue=checkvalue(strdivele0, valget)
                    if (strvalue==-1) //and check whether the element is not duplicated
                    {
                        strdivele0=strdivele0 + "," + valget;
                    }
                }
                valget="";
            }
            //close of valget if
        } //close of else 
    }
    else if (strLastPanName.id=="blank1")
    {
        if(strbtnid!="")//means button was pressed under div
        {
           if (strdivele1=="")//this is the first element to be droped
            {
                strdivele1=strbtnid; //concatenating the particular div's string
            }
            else
            {
                var strvalue=checkvalue(strdivele1, strbtnid)
                    if (strvalue==-1) //and check whether the element is not duplicated
                    {
                       strdivele1=strdivele1+ "," +strbtnid;
                    }
                
            }
            
            strbtnid="";
        }
        else //means button was pressed from menu
        {
            if(valget!="")
            {
                if(strdivele1=="") //div is empty and simply create the element in the div
                {                    
                     strdivele1=valget;                 
                }
                 else //div already contains some elements
                {
                    var strvalue=checkvalue(strdivele1, valget)
                    if (strvalue==-1) //and check whether the element is not duplicated
                    {
                        strdivele1=strdivele1 + "," + valget;
                    }
                }
                valget="";
            }//close of valget if
            
        } //close of else 
    }
    else if(strLastPanName.id=="blank2")
    {
        if(strbtnid!="")//means button was pressed under div
        {
           if (strdivele2=="")//this is the first element to be droped
            {
                strdivele2=strbtnid; //concatenating the particular div's string
            }
            else
            {
                var strvalue=checkvalue(strdivele2, strbtnid)
                    if (strvalue==-1) //and check whether the element is not duplicated
                    {
                       strdivele2=strdivele2+ "," +strbtnid;
                    }
            }
            
            strbtnid="";
        }
        else //means button was pressed from menu
        {
            if (valget!="")
            {
                if(strdivele2=="") //div is empty and simply create the element in the div
                {
                    strdivele2=valget;
                }
                else //div already contains some elements
                {
                    var strvalue=checkvalue(strdivele2, valget);
                    if (strvalue==-1) //and check whether the element is not duplicated
                    {
                        strdivele2=strdivele2 + "," + valget;
                    }
                }
                valget="";
             }//close of valget if
             
        }//close of else 
    }//close of blank2 if
    else if (strLastPanName.id=="blank3")
    {
        if(strbtnid!="")//means button was pressed under div
        {
           if (strdivele3=="")//this is the first element to be droped
            {
                strdivele3=strbtnid; //concatenating the particular div's string
            }
            else
            {
                var strvalue=checkvalue(strdivele3, strbtnid)
                    if (strvalue==-1) //and check whether the element is not duplicated
                    {
                       strdivele3=strdivele3+ "," + strbtnid;
                    }
                
            }
            
            strbtnid="";
        }
        else //means button was pressed from menu
        {
            if(valget!="")
            {
                if(strdivele3=="") //div is empty and simply create the element in the div
                {
                    strdivele3=valget;
                }
                 else //div already contains some elements
                {
                    var strvalue=checkvalue(strdivele3, valget)
                    if (strvalue==-1) //and check whether the element is not duplicated
                    {
                        strdivele3=strdivele3 + "," + valget;
                    }
                }
                valget="";
            }//close of valget if
            
        } //close of else
    }//close of blank3 if
    
}

function checkvalue(chkstr, chkvalue)
{


    chkarr=chkstr.split(",");
    var flag=false;
    for(i=0;i<=chkarr.length-1;i++)
    {
        if (chkarr[i]==chkvalue)
            return 1;
    }
    return -1;
}

function setupdivdrag()
{

//alert(divmousedown);
//alert(btnclicked);
    removeItem(divmousedown,btnclicked)
}

String.prototype.trim = function() { return this.replace(/^\s+|\s+$/, ''); };

function removeItem(strdiv, strbtn) //is called when mouse is down
{

var temparr;
var tempstr="";
    //Add all Dropable divs as else if
    if (strdiv=="blank0") //which div is there on mousedown means blank2
    {
        if(document.getElementById("wheredata1").value!="")
        {
        var d = true
            strdivele0=document.getElementById("wheredata1").value; 
//              btnclicked=document.getElementById("wheredata1").value; 
//             var chk1=strdivele0.search(")");
//              if (chk1!=-1)
//                 {
//                 d=false;
//                 }
//                if (d==false) 
             
        }
        temparr=strdivele0.split(",");
        for(i=0;i<=temparr.length-1;i++)
        {
        var b =true;

             var chk=temparr[i].search(strbtn);
             var l1=temparr[i].trim();;
             var l2=strbtn.trim();;
            if(l1==l2)
             {
             if (chk!=-1)
                 {
                 b=false;
                
                 }
              chk=strbtn.search(temparr[i]);
              if (chk!=-1)
                 {
                 b=false;
                
                 }
          }
          if (b==true)
                 
         //if(temparr[i]!=strbtn)
            {
                if(tempstr=="")
                {
                    tempstr=temparr[i];
                }
                else
                {
                    tempstr=tempstr+","+temparr[i];
                }
            }
        }
        strdivele0=tempstr;
       strbtnid="";
        if(tempstr=="")
                {
      document.getElementById("wheredata").value="";
      }
        //bindblank2();
    }
    else if (strdiv=="blank1") //which div is there on mousedown means blank2
    {
        temparr="";
        if(document.getElementById("column").value!="")
        {
            strdivele1=document.getElementById("column").value; 
//            btnclicked=document.getElementById("column").value;   
        }
        temparr=strdivele1.split(",");
        for(i=0;i<=temparr.length-1;i++)
        {
         var b =true;
      
             var chk=temparr[i].search(strbtn);
             var l1=temparr[i].trim();;
             var l2=strbtn.trim();;
             if(l1==l2)
             {
             if (chk!=-1)
                 {
                 b=false;
                
                 }
              chk=strbtn.search(temparr[i]);
              if (chk!=-1)
                 {
                 b=false;
                
                 }
            }
          if (b==true)
           // if(temparr[i]!=strbtn)
            {
                if(tempstr=="")
                {
                    tempstr=temparr[i];
                }
                else
                {
                    tempstr=tempstr+","+temparr[i];
                }
            }
        }
        strdivele1=tempstr;
      strbtnid="";
        //bindblank2();
    }
    else if (strdiv=="blank2") //which div is there on mousedown means blank2
    {
        temparr="";
        if(document.getElementById("crdata").value!="")
        {
            strdivele2=document.getElementById("crdata").value;
//            btnclicked=document.getElementById("crdata").value;   
        }
        temparr=strdivele2.split(",");
        for(i=0;i<=temparr.length-1;i++)
        {
        if (temparr[i]==strbtn)
                {
                
                }
                else
                {
                 var b =true;
     
             var chk=temparr[i].search(strbtn);
             var l1=temparr[i].trim();;
             var l2=strbtn.trim();;
             if(l1==l2)
             {
             if (chk!=-1)
                 {
                 b=false;
                
                 }
              chk=strbtn.search(temparr[i]);
              if (chk!=-1)
                 {
                 b=false;
                
                 }
             }
          if (b==true)
            //if(temparr[i]!=strbtn)
            {
                if(tempstr=="")
                {
                    tempstr=temparr[i];
                }
                else
                {
                    tempstr=tempstr+","+temparr[i];
                }
            }
                }
        
        }
        strdivele2=tempstr;
    strbtnid="";
        //bindblank2();
    }
    else if (strdiv=="blank3") //which div is there on mousedown means blank3
    {
    var cumbtn="";
        temparr="";
        if(document.getElementById("Showdata").value!="")
        {
            if(document.getElementById("Showdata").value!="undefined")
            {
            strdivele3=document.getElementById("Showdata").value;
      
           cumbtn=document.getElementById("datashowforblnank3").value; 
           cumbtn="";
           document.getElementById("datashowforblnank3").value=""  
            }
        }
        temparr=strdivele3.split(",");
        for(i=0;i<=temparr.length-1;i++)
        {
             if (temparr[i]==strbtn)
                {
                
                }
                else
                {
                    var b =true;
                    
                         var chk=temparr[i].search(strbtn);
                         var l1=temparr[i].trim();;
                         var l2=strbtn.trim();;
                         if(l1==l2)
                            {
                             if (chk!=-1)
                                 {
                                 b=false;
                                
                                 }
                              chk=strbtn.search(temparr[i]);
                              if (chk!=-1)
                                 {
                                 b=false;
                                
                                 }
                         }
                             if (b==true)
                       // if(temparr[i]!=strbtn)
                        {
                            if(tempstr=="")
                            {
                                tempstr=temparr[i];
                            }
                            else
                            {
                                tempstr=tempstr+","+temparr[i];
                            }
                    }
                }
         
        }
         if(tempstr!="")
        {
         document.getElementById("Showdata").value = tempstr;
         }
        if(cumbtn=="undefined")  
        {    
        cumbtn="";
        }
        if(cumbtn!="")
        {
                if (tempstr=="")
                {
                strdivele3=cumbtn.trim();;
                    if(strdivele3!="")
                        {
                    document.getElementById("Showdata").value = strdivele3;
                        }
                }
                else
                {
                            if (tempstr==cumbtn)
                            {
                              strdivele3=cumbtn.trim();;
                            }
                            else
                            {
                           strdivele3=tempstr+","+cumbtn.trim();;
                           }
                                  if(strdivele3!="")
                                     {
                                document.getElementById("Showdata").value = strdivele3;
                                    }
                }
              
        }
        else
        {
             strdivele3=tempstr;
             document.getElementById("Showdata").value = tempstr;
//                    if(tempstr!="")
//                        {
//                        
//                        }
        }
    strbtnid="";
    }

}
//function removeItem(strdiv, strbtn) //is called when mouse is down
//{

//var temparr;
//var tempstr="";
//    //Add all Dropable divs as else if
//    if (strdiv=="blank0") //which div is there on mousedown means blank2
//    {
//        if(document.getElementById("wheredata1").value!="")
//        {
//            strdivele0=document.getElementById("wheredata1").value;   
//        }
//        temparr=strdivele0.split(",");
//        for(i=0;i<=temparr.length-1;i++)
//        {
//            if(temparr[i]!=strbtn)
//            {
//                if(tempstr=="")
//                {
//                    tempstr=temparr[i];
//                }
//                else
//                {
//                    tempstr=tempstr+","+temparr[i];
//                }
//            }
//        }
//        strdivele0=tempstr;
//        strbtnid="";
//        //bindblank2();
//    }
//    else if (strdiv=="blank1") //which div is there on mousedown means blank2
//    {
//        temparr="";
//        if(document.getElementById("column").value!="")
//        {
//            strdivele1=document.getElementById("column").value;   
//        }
//        temparr=strdivele1.split(",");
//        for(i=0;i<=temparr.length-1;i++)
//        {
//            if(temparr[i]!=strbtn)
//            {
//                if(tempstr=="")
//                {
//                    tempstr=temparr[i];
//                }
//                else
//                {
//                    tempstr=tempstr+","+temparr[i];
//                }
//            }
//        }
//        strdivele1=tempstr;
//        strbtnid="";
//        //bindblank2();
//    }
//    else if (strdiv=="blank2") //which div is there on mousedown means blank2
//    {
//    var b=false;
//        temparr="";
//        if(document.getElementById("crdata").value!="")
//        {
//            strdivele2=document.getElementById("crdata").value;   
//        }
//        temparr=strdivele2.split(",");
//        for(i=0;i<=temparr.length-1;i++)
//        {
//        var chk=temparr[i].search(strbtn) 
//        b=false;
//        alert(b);
//                if (chk!=-1)
//                {
//                 b=true
//                }
//                chk=strbtn.search(temparr[i])
//                if (chk!=-1)
//                {
//                 b=true
//                }
//            if(b==false)
//            {
//                if(tempstr=="")
//                {
//                    tempstr=temparr[i];
//                }
//                else
//                {
//                    tempstr=tempstr+","+temparr[i];
//                }
//            }
//        }
//        strdivele2=tempstr;
//        strbtnid="";
//        //bindblank2();
//    }
//    else if (strdiv=="blank3") //which div is there on mousedown means blank3
//    {
//        temparr="";
//        if(document.getElementById("Showdata").value!="")
//        {
//            strdivele2=document.getElementById("Showdata").value;   
//        }
//        temparr=strdivele3.split(",");
//        for(i=0;i<=temparr.length-1;i++)
//        {
//            if(temparr[i]!=strbtn)
//            {
//                if(tempstr=="")
//                {
//                    tempstr=temparr[i];
//                }
//                else
//                {
//                    tempstr=tempstr+","+temparr[i];
//                }
//            }
//        }
//        strdivele3=tempstr;
//        strbtnid="";
//    }

//}
function FillSavedFormula()
{

var tbl="";
 var arrForMula1=document.getElementById("formulaIds").value.split(",");
      for(j=0; j<=arrForMula1.length-1;j++)
      
        {
            if (arrForMula1[j]!="")
           {
               if (tbl=="")
               {
        tbl="<input type='button' value='"+arrForMula1[j]+"' id='"+arrForMula1[j]+"' onclick=FormulaToData(); />"
                }
                else
                {
                  tbl=tbl + "<input type='button' value='"+arrForMula1[j]+"' id='"+arrForMula1[j]+"' onclick=FormulaToData(); />"
                }
           }
        }
       document.getElementById("txtaFormula").innerHTML="";
       document.getElementById("txtaFormula").innerHTML=document.getElementById("txtaFormula").innerHTML+tbl;
}
function FormulaToData()
{

var ranjit;
var b=false;
var n= event.srcElement.id;
//alert(n);
//alert(document.getElementById("formulaIds").value);


document.getElementById("FormulaClicked").value;

   var arrForMula=document.getElementById("formulaIds").value.split(",");
   document.getElementById("formulaIds").value="";
    for(i=0; i<=arrForMula.length-1;i++)
    {
//        if (n==arrForMula[i])
//        {
//        }
//        else
//        {
            if(document.getElementById("formulaIds").value=="")
            {
            document.getElementById("formulaIds").value=arrForMula[i];
            }
            else
            {
            document.getElementById("formulaIds").value=document.getElementById("formulaIds").value +","+arrForMula[i];
            }
//        }
    
    }
    if(strdivele3=="")
    {
    strdivele3=n;
    }
    else
    {
    
    var chkdatablank3=strdivele3.split(",");
    for (nm=0;nm<=chkdatablank3.length-1;nm++)
        {
        var lr=chkdatablank3[nm].toLowerCase();
        lr=lr.trim();;
        n=n.toLowerCase();
        n=n.trim();;
        if (lr==n.toLowerCase())
            {
            b=true;
            }
           
       
        }
        if(b==false)
        {
         strdivele3=strdivele3 + "," + n;
        }
    }
    document.getElementById("Showdata").value=strdivele3;
    var tbl="";
     var arrForMula1=document.getElementById("formulaIds").value.split(",");
      for(j=0; j<=arrForMula1.length-1;j++)
      
        {
            if (arrForMula1[j]!="")
               {
                if (tbl=="")
                {
               tbl="<input type='button' title='Right click to remove the formula' value='"+arrForMula1[j]+"' id='"+arrForMula1[j]+"' ondblclick=FormulaToData(); oncontextmenu=delformula(); />"
                }
                else
                {
           
        tbl= tbl + "<input type='button' title='Right click to remove the formula' value='"+arrForMula1[j]+"' id='"+arrForMula1[j]+"' ondblclick=FormulaToData(); oncontextmenu=delformula(); />"
                    }
                }
        }
       document.getElementById("txtaFormula").innerHTML="";
       document.getElementById("txtaFormula").innerHTML=document.getElementById("txtaFormula").innerHTML+tbl;
   binddivs();

}
function delformula()
{
var n= event.srcElement.id;
 var arrForMula=document.getElementById("formulaIds").value.split(",");
   document.getElementById("formulaIds").value="";
    for(i=0; i<=arrForMula.length-1;i++)
    {
        if (n==arrForMula[i])
        {
        }
        else
       {
            if(document.getElementById("formulaIds").value=="")
            {
            document.getElementById("formulaIds").value=arrForMula[i];
            }
            else
            {
            document.getElementById("formulaIds").value=document.getElementById("formulaIds").value +","+arrForMula[i];
            }
       }
       
    
    }
    var tbl="";
    var arrForMula1=document.getElementById("formulaIds").value.split(",");
      for(j=0; j<=arrForMula1.length-1;j++)
      
        {
            if (arrForMula1[j]!="")
               {
                if (tbl=="")
                {
               tbl="<input type='button' value='"+arrForMula1[j]+"' title='Right click to remove the formula' id='"+arrForMula1[j]+"' onclick=FormulaToData(); oncontextmenu=delformula(); />"
                }
                else
                {
           
        tbl= tbl + "<input type='button' value='"+arrForMula1[j]+"' id='"+arrForMula1[j]+"' title='Right click to remove the formula' onclick=FormulaToData(); oncontextmenu=delformula(); />"
                    }
                }
        }
       document.getElementById("txtaFormula").innerHTML="";
       document.getElementById("txtaFormula").innerHTML=document.getElementById("txtaFormula").innerHTML+tbl;
}

function replacecoma(comastr)
{

var strarr=comastr.replace(",","$");
return strarr;

}
//modification for date 

function datasubmitt()
{
var date1=document.getElementById("txtStartdate").value;
var date2=document.getElementById("txtEnddate").value;
    if(date1==""  && date2=="")
    {
        datasubmit()
    }
    if(date1!=""  && date2!="" )
    {
        datasubmit()
    }
    if(date1!=""  && date2=="" )
    {
         alert("Enter End Date !!")
   
    }
    if(date1 == ""  && date2 != "" )
    {
        alert("Enter Start Date ")
    }
}
function datasubmit()
	{
	var date1=document.getElementById("txtStartdate").value;
	var date2=document.getElementById("txtEnddate").value;
	
	var tempi
	
	if (document.getElementById('<%=chkSubTotal.ClientID%>').checked==true)
	{
	  document.getElementById("chkcheck").value ="yes";
	}
	else
	{
	 document.getElementById("chkcheck").value ="no";
	}
	var strrow = strdivele2
	    //document.getElementById("crdata").value = replacecoma(strrow)
	    document.getElementById("crdata").value = strrow
	var strdata = strdivele3
	//document.getElementById("showdata").value = strdivele3
	//adding the Count Function in each column name

	    var arrstrs=strdata.split(",")
	    var tempnewstr=""
	    for(tempi=0; tempi<=arrstrs.length-1;tempi++)
	    {
	        var tempele= arrstrs[tempi]
	        if(tempnewstr=="")
	        {
	        
	            tempnewstr=tempele;
	        }
	        else
	        {
	            tempnewstr=tempnewstr + "," + tempele;
	        }
//	    
	    }
//	     var tempnewstr=document.getElementById("showdata").value;
	    //document.getElementById("showdata1").value = replacecoma(tempnewstr);
	    document.getElementById("Showdata1").value =tempnewstr;
	    document.getElementById("ShowdataSave").value = replacecoma(tempnewstr);
	    
	   
	var strcolumn=  strdivele1
//	var strcolumn=new Array();
//	strcolumn=
//	strcolumn=
	
	  //document.getElementById("column").value=replacecoma(strcolumn)
	  
	  document.getElementById("column").value=strdivele1
	  
	var strwhere=strdivele0
		  // document.getElementById("wheredata").value = replacecoma(strwhere)
		  
		if (document.getElementById("crdata").value=="")
		{
			alert("Please select row.")
			return false
		}

		else 
        {
            if(document.getElementById("chknewwindow").checked==true)
             document.forms[0].target ="_new";
             else
             document.forms[0].target ="FRMRESULST";
        }
          if(document.getElementById("Drildown").checked==true)
          {
        
    
            document.forms[0].action="ResultOutput1.aspx?startdate="+date1+"&enddate="+date2       
       
          }
          else
          {
             document.forms[0].action="ResultOutput.aspx?startdate="+date1+"&enddate="+date2
          }
        
       // document.forms[0].action="showdata.aspx"
        
// document.forms[0].action="Drildown.aspx"
        document.forms[0].submit();
       
        document.forms[0].target="_self";
        
	}
	
	
	
	//date colum Bind
function binddatecolumnname(tbl)
{
  //clear dropdown
  var dtcolumn=document.getElementById( "<%=ddlColumn.ClientID%>")
  var ll=document.getElementById( "<%=ddlColumn.ClientID%>")
  
    var len = ll.options.length;
    for(i=len-1; i>=0; i--)
    {
    ll.options.remove(i);
    }
   //Bind dropdown 
   var clmn=tbl.value.split(",")
    var optn = document.createElement("option");
        optn.text = "--select--";
        optn.value = "--select--";
        dtcolumn.options.add(optn);   
   for(i=0;i<clmn.length-1;i++)
   {
        var optn = document.createElement("option");
        optn.text = clmn[i];
        optn.value = clmn[i];
        ll.options.add(optn);        
   }
   ll.options[0].select=true
}
	
    </script>

    <table width="100%" border="0" align="center" cellspacing="1" summary="Query Builder">
        <tr>
            <td valign="top" width="100%" scope="rowgroup">
            <label for="ctl00_LeftPlaceHolder_ddltable"></label>
                <asp:DropDownList ID="ddlTablename" CssClass="leftdropdownlist" Style="width: 190px"
                    runat="server">
                </asp:DropDownList>
<%--<label for="ctl00_LeftPlaceHolder_ddlDept"></label>
                <asp:DropDownList ID="ddlDept" CssClass="leftdropdownlist" Style="width: 190px" runat="server" Width="200px">
                </asp:DropDownList>
                <br />
<label for="ctl00_LeftPlaceHolder_ddlClient"></label>
                <asp:DropDownList ID="ddlClient" CssClass="leftdropdownlist" Style="width: 190px"
                    runat="server">
                </asp:DropDownList>
<label for="ctl00_LeftPlaceHolder_ddlLob"></label>
                <select id="ddlLob" class="leftdropdownlist" style="width: 190px" name="cboLOB" runat="server">
                </select>--%>
            </td>
        </tr>
    </table>
    <div id="myDiv" style="overflow: auto; width: 100%; height: 530px" runat="server">
    </div>
    <input type="hidden" name="hidReportmode" value="new" id="hidReportmode" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">



function openSavedQueries1()
	{
	
		//childWindow=open (".aspx?cboLOB="+ document.frmquerybl.cboLOB.value+ "&cbodatatable=" + document.frmquerybl.hidtablename.value  ,"open","height=300,width=500,top=100,left=400,scrollbars=yes,location=no,toolbar=0,menubar=0,resizable=0");
		childWindow=open ("Shared.aspx?userid=" + "<%=Session("userid") %>"  ,"open","width=550,height=600,top=110,left=200,scrollbars=no,status=yes");
	  
	}

function Button2_onclick()
 {
location.href("welcome.aspx?val=103");
}

function Button1_onclick()
{
    window.open("SavedQueries.aspx","SavedQuery","width=600px,height=400px"); 
    //childWindow=open ("SavedQueries.aspx.aspx?cboLOB="+ document.frmquerybl.cboLOB.value+ "&cbodatatable=" + document.frmquerybl.hidtablename.value  ,"open","height=300,width=500,top=100,left=400,scrollbars=yes,location=no,toolbar=0,menubar=0,resizable=0");  
} 

function Button3_onclick()
{

    window.open("SaveQuery.aspx?hidtablename="+document.getElementById("hidtablename").value+"&chkPercentage="+document.getElementById("chkPercentage").value+"&hidFormulaName="+document.getElementById("hidFormulaName").value+"&whereData="+document.getElementById("wheredata").value+"&type="+document.getElementById("hidReportmode").value,"SaveQuery","width=600px,height=400px");
    //window.open("SaveQuery.aspx?Dept="+document.getElementById("<=ddlDept.ClientID%>").value+"&Client="+document.getElementById("<=ddlClient.ClientID%>").value+"&Lob="+document.getElementById("<=ddlLob.ClientID%>").value+"&hidtablename="+document.getElementById("hidtablename").value+"&chkPercentage="+document.getElementById("chkPercentage").value+"&hidFormulaName="+document.getElementById("hidFormulaName").value+"&whereData="+document.getElementById("whereData").value+,"SaveQuery","width=600px,height=400px");
}

function chkBoxPivot_onclick()
{
   
    document.getElementById("hidShowInPivot").value = document.getElementById("chkBoxPivot").checked;
}


    </script>

    <table id="tabmain" width="100%" border="0" cellspacing="0" style="border-bottom:solid 1px black;" summary="Query Builder">
        <tr>
           <td style="width: 297px" scope="col"><div id="spanget" style="color:Black; font-weight:bold"></div></td>
            <td style="width: 218px" valign="bottom" scope="col">
                <%--		<%'end if%>--%>
                 <label class="label" style="color:Black"><%=FormatDateTime(Now, 1)%></label>
            </td>
           
            <td>
                <table>
                    <tr>
                        <%--<td scope="col"><input type="button" value="Share Crosstab" class="button" onclick="openSavedQueries1();" /></td>--%>
                        <td scope="col"><input type="button" value="Open Crosstab"  class="button"  id="Button1" onclick="return Button1_onclick()" /></td>
                    </tr>
                </table>
            </td>
           </tr>
    </table>
    <table cellspacing="0" border="0">
        <tr>
            <td colspan="1" style="width: 450px"  scope="rowgroup">
                <table cellspacing="0" border="0" summary="">
                    <tr>
                        <td rowspan="2" valign="top"  scope="rowgroup">
                            <label id="lblWhere" class="label" for="blank0" style="color:Black">
                                Where</label>
                            <br />
                            <div id="blank0" class="divquery" onmouseover="fillPanName(this);" onmousedown="setupdragd(this)";  onmouseup="handleDrop();"
                                style="border: thin ridge #42969f;visibility: visible; width: 100px; height: 170px; background-color: white;overflow:auto">
                            </div>
                        </td>
                        <td rowspan="2"  valign="top" style="width: 266px"  scope="rowgroup">
                            <label id="lblRow" class="label" style="color:Black" for="blank2">
                                Row</label><br />
                            <div id="blank2" onmouseover="fillPanName(this);" onmousedown="setupdragd(this)";    onmouseup="handleDrop();" style="border: thin ridge #42969f;
                                visibility: visible; width: 100px; height: 170px; background-color: white;overflow:auto">
                            </div>
                        </td>
                        <td colspan="2" valign="top" style="height: 81px"  scope="colgroup">
                            <label id="lblColumn" class="label" style="color:Black" for="blank1">
                                Column
                            </label>
                            <br />
                            <div id="blank1" onmouseover="fillPanName(this);" onmousedown="setupdragd(this)";  onmouseup="handleDrop();" style="border: thin ridge #42969f;
                                visibility: visible; width: 450px; height: 50px; background-color: white;overflow:auto">
                            </div>
                        </td>
                        
                        
                    </tr>
                    <tr>
                        <td valign="top" style="height: 126px"   scope="col">
                            <label id="lblData" class="label" style="color:Black" for ="blank3">
                                Data</label><br />
                            <div id="blank3" onmouseover="fillPanName(this);" onmouseup="handleDrop();" onmousedown="setupdragd(this)";  style="border: thin ridge #42969f;
                                visibility: visible; width: 320px; height: 95px; background-color: white;overflow:auto">
                            </div>
                        </td>
                        <td valign="top" style="height: 126px">
                            <label id="Label1" class="label" style="color:Black" for="blank4">
                                Trash</label><br />
                            <div id="blank4" style="border: thin ridge #42969f; visibility: visible; width: 125px; height: 95px; left: 250px;
                                top: 15px; background-color: white;" onmouseup="handleDrop();" onmouseover="fillPanName(this);">
                            </div>
                        </td>
                    </tr>
                    
                    <tr>
                        <td><label for="chkSubTotal"></label>
                            <asp:CheckBox ID="chkSubTotal" runat="server" Text="Show SubTotal" style="color:Black" />
                        
                        </td>
                        <td colspan="2" scope="colgroup" style="color:Black">
                            <asp:Label ID="Label2"  runat="server" Visible="false" Text="(Rename Label)"></asp:Label>
    <label for="txtsubname"></label>                        
<input type="text" id="txtsubname" visible="false" name="txtsubname" runat="server" value="Sub Total" class="textBox" />
<label for="chknewwindow"></label>  
                            <input type="checkbox" id="chknewwindow" name="chknewwindow" style="color:Black" value="true" /> 
                            Open In New Window
                        </td>
                           
                       <%-- <td colspan="1">
<label for="chkBoxPivot"></label>  
                            <input type="checkbox" id="chkBoxPivot" name="chkBoxPivot" onclick="return chkBoxPivot_onclick()" />
                            Show In Pivot&nbsp;</td>--%>
                            <td colspan="1" scope="col" style="color:Black">
<label for="Drildown"></label>
                            <input type="checkbox" id="Drildown" name="chkBoxPivot"  style="color:Black" />
                            Drill Down&nbsp;</td>
                    </tr>
                    </table>
                 <table>
                <tr><td style="color:Black;">Date column</td><td> <label for="ctl00_MainPlaceHolder_ddlColumn"></label>
                                        <asp:DropDownList ID="ddlColumn" runat="server" CssClass="dropdownlist" Style="position: static"
                                            ToolTip="Date Columns">
                                        </asp:DropDownList></td></tr>
                </table>
                 <table summary ="table" style=" width:149%; height: 39px;" id="TABLE1" onclick="return TABLE1_onclick()" >
				                    <tr>
				                        <td style="width: 85px; height: 26px;" valign="top" scope="col">    
				                            <label  title="Start Date" style="color:#000000; font-size:10pt; font-family:Verdana " for="txtStartdate">Start Date:</label>				                        
				                        </td>
				                        
				                        <td valign="top" scope="col" style="width:150px; height: 26px;">
				                            <input type="text" id="txtStartdate" name="txtStartdate"  style="width:100%"/></td>
				                        <td style="width: 40px; height: 26px;" valign="top" scope="col">
                                            <img alt="Start Date" id="imgStartDate" onclick="ShowCalendar('txtStartdate');" src="../images/Calendar.gif"
                                                style="width: 17px; height: 21px" title="Click to select date" /></td>
				                        <td style="width: 85px; height: 26px;" valign="top" scope="col">    
				                            <label title="Start Date" style="color:#000000; font-size:10pt;font-family:Verdana" for="txtEnddate">End Date:</label>				                        
				                        </td>
				                        <td style="width: 158px; height: 26px;" valign="top" scope="col">
				                            <input type="text" id="txtEnddate" name="txtEnddate" class="text" />
				                        </td>
				                        <td style="width: 39px; height: 26px;" valign="top" scope="col">
                                            <img alt="End Date" id="imgEnddate" onclick="ShowCalendar('txtEnddate');" src="../images/Calendar.gif" style="width: 17px;
                                                height: 21px" title="Clieck to select date" /></td>
				                    </tr>
				                </table>
                <table width="100%" summary="">
                    <tr>
                        <td width="100%" colspan="4" valign="top">
                            <input type="button" value="Process" class="button" onclick="return datasubmitt()" id="Button4" style="width: 105px"/>
                            <input type="button" value="Reset" class="button" id="Button2" onclick="return Button2_onclick()" style="width: 84px" />
                            <input type="button" value="Save Crosstab" class="button" id="Button3" onclick="return Button3_onclick()" style="width: 107px" />
                             <input type="button" value="Set Formula" class="button" id="frmula" onclick="return openformula()" style="width: 110px" />
                             <input id="uploadtable" type="button" runat="server" visible="false" value="Upload Table"  onclick="window.open('\Uploadtable.aspx')" class="button" />
                            </td>
                    </tr>
                </table>
            </td>
            <td colspan="1" valign="top"   scope="col">
                <label id="lblformula" for="txtaFormula" class="label" style="color:Black">
                    Formula</label><br />
                <%--<div id="formula" style="width:100px; height:170px">
                    <textarea id="txtaFormula" name="txtaFormula" style="visibility: visible; width: 100px;
                        height: 165px; background-color: white; border-color: #42969f; border-style: ridge;
                        border-width: thin" rows="*,*" cols="*,*">
                    </textarea>
                    
                </div>--%>
                 <div id="txtaFormula" style="width:130px; height:170px; border: thin ridge #42969f; visibility: visible; overflow:scroll; scrollbar-face-color:white"></div>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2"   scope="colgroup">
                <table cellspacing="0" width="100%" border="0" class="grid">
                    <tr>
                        <td>
                            <iframe style="visibility: visible; width: 100%; height: 350px; background-color: white"
                               id="FRMRESULST"  frameborder="0"  name="FRMRESULST"  scrolling="auto" title="FRMRESULST"></iframe>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <input type="hidden" id="chkcheck" name="chkcheck" />
    
    <input type="hidden" id="reqType" name="reqType" />
    <input type="hidden" id="hidtablename" name="hidtablename" />
    <input type="hidden" id="crdata" name="crdata"/><!--crdata-->
    <input type="hidden" id="hidcolstr" name="hidcolstr" />
    <input type="hidden" id="Showdata" name="Showdata" />
     <input type="hidden" id="datashowforblnank3" name="datashowforblnank3" /><!--formula for blank3-->
    <input type="hidden" id="ShowdataSave" name="ShowdataSave" />
    <input type="hidden" id="Showdata1" name="Showdata1" /><!--all the  items of Data division-->
    <input type="hidden" id="column" name="column" />
    <input type="hidden" id="txtQueryNAme" name="txtQueryNAme" />
    <input type="hidden" id="queryname" name="queryname" />
     <input id="hidShowInPivot" name="hidShowInPivot" value="false" type="hidden" />
    <input type="hidden" id="wheredata" style="width:80%" name="wheredata"/><!--wheredata-->
	<input type="hidden" id="wheredata1" style="width:80%" name="wheredata1"/><!--wheredata1-->
	<input type="hidden" id="hidFormulaName" name="hidFormulaName" /><!-- hidFormulaName -->
	<input type="hidden" id="chkPercentage" name="chkPercentage"/><!-- Percentage Check -->
	<input type="hidden" id="hiddept" name="Department"/><!-- Department Name--> 
	<input type="hidden" id="hidclient" name="Client"/><!-- Client Name--> 
	<input type="hidden" id="hidlob" name="Lob"/><!-- Lob Name--> 
	<input type="hidden" id="formulaIds" name="formulaIds"/><!-- formula buttons--> 
    <input type="hidden" id="FormulaClicked" name="FormulaClicked"/><!-- formula buttons--> 
      <input type="hidden" id="SimpleData" name="SimpleData"/><!-- formula buttons--> 
     <input type="hidden" id="Savedwhere" name="Savedwhere"/><!-- formula buttons--> 
</asp:Content>

