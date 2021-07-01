<%--Project Name: IDMS Phase 2
    Module Name: Advance Report Designer
    Page Name: setData
    Summary: To set data for each individual object of a report. It is a component of the Module Report Designer.
             This component is to be used by the welcome page primarily to generate and update reports.
    Created on: 29/04/08
    Created By: Usha Sheokand

--%>

<%@ Page Language="VB" AutoEventWireup="false" ValidateRequest="false"  CodeFile="setData.aspx.vb" Inherits="ReportDesigner_setData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head id="Head1" runat="server">
    <title>Columns</title>
    <link href="../App_Themes/Themes/StyleSheet.css" rel="stylesheet" />

    <script language="javascript" src="../js/202pop.js" type="text/javascript"></script> <%--To allow the user to chose among a wide range of colors--%>
    <script language="JavaScript" type="text/javascript" src="../js/picker.js"></script>   <%--To allow the user to chose among a wide range of colors--%>
    <script type="text/javascript" language="javascript">
      
var sno=0;
var gVal="";
         function onLoad()
        {
           // onload data
     
           document.getElementById("hidFormulacnt").value=window.opener.document.getElementById("hidFormulacnt").value;
           var ob=document.getElementById("hidObjname").value;
           var h=window.opener.document.getElementById(ob).style.height; // height of the object         
           var w=window.opener.document.getElementById(ob).style.width;  // width of the object
           var bc=window.opener.document.getElementById(ob).style.backgroundColor;  // bcolor of the object
           var fc=window.opener.document.getElementById(ob).style.color;  // fcolor of the object
           document.getElementById("sample_4").style.backgroundColor=bc;
           document.getElementById("sample_3").style.backgroundColor=fc;
           
         // to set current style of the object
            if(window.opener.document.getElementById(ob).style.textDecoration=="underline")
            {
                document.getElementById("chkUnderline").checked=true;
            }
            if(window.opener.document.getElementById(ob).style.fontStyle=="italic")
            {
                document.getElementById("chkItalic").checked=true;
            }
            if(window.opener.document.getElementById(ob).style.fontWeight=="bold")
            {
                document.getElementById("chkBold").checked=true;
            }
            var fs=window.opener.document.getElementById(ob).style.fontSize;          
            if(fs!="")
            {
                fs=fs.replace("pt","");
                document.getElementById("ddlFontsize").value=fs;
            }
            var ff=window.opener.document.getElementById(ob).style.fontFamily;
            if(ff!="")
            {
                document.getElementById("ddlFontfamily").value=ff;
            }
         ////////////////
            // to set current height & width of the object
            if(h!="")
             {
            
               var s=h.replace("px","");
               document.getElementById("txtHeight").value=s;
             }
             if(w!="")
             {
                var s=w.replace("px","");
                document.getElementById("txtWidth").value=s;
             }
             if(document.getElementById("hidSource").value=="details")
             {
                document.getElementById("tblFordetails").style.display="block";
                document.getElementById("tblForother").style.display="none";
             }
             else
             {
                document.getElementById("tblFordetails").style.display="none";
//                document.getElementById("tblForother").style.display="block";
             }
             if(document.getElementById("hidSource").value=="details")
             {
                var itm=document.getElementById("hidObjvalue").value;
                 itm=itm.replace("[","");
                 itm=itm.replace("]","");
                document.getElementById("lblbutton").innerHTML="Object Name: "+itm;
             }
             else
             {
                document.getElementById("lblbutton").innerHTML="Object Name: "+document.getElementById("hidObjname").value;
             }
        }
        function blank(s) //Check for blank field
	    {
			var slen = s.length
			var i;
				for(i=0;i<slen;i++)
				{
				if(s.charAt(i)!=" ")return false
				}
				return true
		}
		
        function showFormat() // To enable object format division
        {
        
            document.getElementById('divFormula').style.display='none';
            document.getElementById("divDetailscolorcond").style.display='none';
            document.getElementById('divFormat').style.display='block';
            var img1=document.getElementById('imgFormat');
            img1.src="../images/forDG.jpg";
            img1=document.getElementById('imgFormula');
            img1.src="../images/formulaG.jpg";
            img1=document.getElementById('imgColorcon');
            img1.src="../images/colG.jpg";
        }
        function showFormula()  // To enable formula division
        {
           
            document.getElementById('divFormat').style.display='none';
            document.getElementById("divDetailscolorcond").style.display='none';
            document.getElementById('divFormula').style.display='block';
            var img1=document.getElementById('imgFormat');
            img1.src="../images/forG.jpg";
            img1=document.getElementById('imgFormula');
            img1.src="../images/formulaDG.jpg";
            img1=document.getElementById('imgColorcon');
            img1.src="../images/colG.jpg";
            var strTblp=document.getElementById("hidTblname").value; 
            var tb=strTblp.split("~");
            var i=0;
            var temp="";
           
            
            /// load table columns                        
            var theSel=document.getElementById("selColumn");
           // var whrSel=document.getElementById("selWhere");
           
            if(theSel.length==0)
            {                  
//                var newOpt1 = new Option('Select Column','Select Column');
//                theSel.options[0] = newOpt1;            
//                var newOpt = new Option('where','where');
//                whrSel.options[0] = newOpt;
                
                for(i=0;i<=tb.length-1;i++)
                {
                    ReportDesignerAjax.GetTableFields(tb[i], pasteField);
                }
            }
            if(document.getElementById("hidSource").value=="details")
            {
                var myVal=document.getElementById("hidObjvalue").value;
                var gh=myVal.split(".");
                document.getElementById("hidExformula").value=myVal;
                
                if(gh.length>1)
                {
                    document.getElementById("txtCondition").value=myVal;
                    document.getElementById("txtFormulaName").value=gh[1];
                    //document.getElementById("changecolorcondition").value=gh[1];
                    
                }
                else
                {
                    var tnm=myVal;
                    tnm=tnm.replace("[","");
                    tnm=tnm.replace("]","");
                    var fir=window.opener.document.getElementById("hidDformula").value;
                    var fir1=fir.split("~");
                    var cbn=0;
                    for(cbn=0;cbn<=fir1.length-1;cbn++)
                    {
                        var fir3=fir1[cbn].split(" AS ");
                        var fir4=fir3[1].replace("[","");
                        fir4=fir4.replace("]","");
                        if(fir4==tnm)
                        {
                            document.getElementById("txtCondition").value=fir3[0];
                            document.getElementById("hidExformula").value=fir1[cbn];
                        }
                        
                    }
                   
                    document.getElementById("txtFormulaName").value=tnm;
                    //document.getElementById("changecolorcondition").value=tnm;   
                }
            }
            // if header/footer
            else
            {
                 var nm=document.getElementById("hidObjname").value;
                 var src="hidHformula";
                 if(window.opener.document.getElementById("divType").value=="footer")
                 {
                    src="hidFformula";
                 }
                 var formu=window.opener.document.getElementById(src).value;
                 var formu1=formu.split("~");
                 var u=0;
                 for(u=0;u<=formu1.length-1;u++)
                 {
                     var tbn=formu1[u].split("@#@");
                     if(tbn[0]==nm)
                     {
                         document.getElementById("txtSql").value=tbn[1];
                     }
                     
                 }
                
            }
        }
        // to know number of already existing color conditions
        function existingColor(btnObj)
        {     
           
            var p=0;
//            var fgh=btnObj.replace("[","");
//            btnObj=fgh.replace("]","");
            var val=document.getElementById("hidColcon").value; // val=btn1@#@condition1#@#Formula/cell^condition^format,condition2#@#..... ~ btn2@#@....
             var sel=document.getElementById("selName");
             sel.length=0;
            if(val!="")
            {
                var val1=val.split("~"); 
                var y=0;
                for(y=0;y<=val1.length-1;y++)
                {
                    var sp=val1[y].split("@#@");
                    if(sp[0]==btnObj) // if button found
                    {
                        // check for existing conditions
                        sel.length=0;
                            var val2=sp[1].split("##");
                            var t=0;
                            for(t=0;t<=val2.length-1;t++)
                            {
                                var val3=val2[t].split("#@#");
                                var f=0;
                                sel.options[sel.length]=new Option(val3[0],val3[0]);
                            }
                        //
                    }
                }
               if(sel.length!=0)
               {
                 var el=sel.options[sel.length-1].value;
                el=el.replace("Condition","");
                p=parseInt(el);
               }
              
               
            }
            // add new condition serial number
            
            p=p+1;
            sno=p;
            el="Condition"+p;
            sel.options[sel.length]=new Option(el,el);
            sel.value=el;
        }
        //
        function showColorcon()  // To enable Color Condition division
        {
            document.getElementById('divFormula').style.display='none';
            document.getElementById('divFormat').style.display='none';     
              
            if(document.getElementById("hidSource").value=="details")  // if the object is a native of the details apne
            {
                 document.getElementById('divDetailscolorcond').style.display='block';
                 document.getElementById('txtCell').style.display='none';    
                 document.getElementById('selCellmulti').style.display='none';    
                 document.getElementById('selCellvalue').style.display='block';    
                  if(document.getElementById("hidSource").value=="details")
                 {                   
                    var itm=document.getElementById("hidObjvalue").value;
                        itm=itm.replace("[","");
                        itm=itm.replace("]","");
                   
                     document.getElementById("spanCell").innerHTML="Object Name: "+itm;
                 }
                 else
                 {
                     document.getElementById("spanCell").innerHTML="Object Name: "+document.getElementById("hidObjname").value;
                 }
                 //document.getElementById("spanCell").innerHTML=document.getElementById("hidObjvalue").value;   
                 document.getElementById("hidColcon").value=window.opener.document.getElementById("hidDcolcon").value;          
                  getCellvalue(); // fill column values into <select>          
                  existingColor(document.getElementById("hidObjname").value);    
                  //existingColor(document.getElementById("hidObjvalue").value);
                
                
                 // fill column names into <select>
                var strTblp=document.getElementById("hidTblname").value; 
                var tb=strTblp.split("~");
                var i=0;
//                var colSel=document.getElementById("selColumnname");
//                if(colSel.length==0)
//                {                  
//                    var newOpt1 = new Option('Select Column','Select Column');
//                    colSel.options[0]= newOpt1;                        
//                    for(i=0;i<=tb.length-1;i++)
//                    {
//                        ReportDesignerAjax.GetTableFields(tb[i], fillColumnname);
//                    }
//                }
            }
            else  // otherwise
            {
                 if(document.getElementById("hidSource").value=="header") 
                 {
                    document.getElementById("hidColcon").value=window.opener.document.getElementById("hidHcolorcon").value;          
                   
                 }
                 else
                 {
                    document.getElementById("hidColcon").value=window.opener.document.getElementById("hidFcolorcon").value;          
                    
                 }
                 document.getElementById('divDetailscolorcond').style.display='block';
                 document.getElementById('selElements').style.display='block';
                 document.getElementById('selConditionon').style.display='none';
                 document.getElementById('txtCell').style.display='block';    
                 document.getElementById('selCellmulti').style.display='none';    
                 document.getElementById('selCellvalue').style.display='none';  
                 document.getElementById("spanCell").innerHTML=document.getElementById('hidObjname').value;
                 existingColor(document.getElementById("hidObjname").value);
                 //existingColor(document.getElementById("hidObjname").value);
               
            }

            
            var img1=document.getElementById('imgFormat');
            img1.src="../images/forG.jpg";
            img1=document.getElementById('imgFormula');
            img1.src="../images/formulaG.jpg";
            img1=document.getElementById('imgColorcon');
            img1.src="../images/colDG.jpg";
        }
        

           
       function btnFormatdone_onclick()     ////////////  Set format /////////
       {
     
        var style="";
        style=document.getElementById("<%=hidObjname.ClientID%>").value+">";
        style=style+"font-size:";
        style=style+ document.getElementById("<%=ddlFontsize.ClientID%>").value+"pt;";
        style=style+"font-family:";
        style=style+ document.getElementById("<%=ddlFontfamily.ClientID%>").value;
       
//        if(document.getElementById("hidForecolor").value!="")
//        {
//            style=style+";color:"; 
//            style=style+document.getElementById("hidForecolor").value;
//        }
//        else
//         {
//            style=style+";color:black"; 
//            
//        }
         //**********  fromating of font color of element created

        var gg=document.getElementById("sample_3").style.backgroundColor
        if(gg=="")
        {
             style=style+";color:black"; 

        }else
        {

                    style=style+";color:"; 
                    style=style+gg;
        }
         
         //**********  fromating of background color of element created
            style=style+";background-color:"; 
            style=style+document.getElementById("sample_4").style.backgroundColor;
            style=style+";border:solid 1px "+document.getElementById("sample_4").style.backgroundColor;
            
            
//         document.getElementById("sample_4").style.backgroundColor=bc;
//           document.getElementById("sample_3").style.backgroundColor=fc;
//        if(document.getElementById("hidBackcolor").value!="")
//        {
//            style=style+";background-color:"; 
//            style=style+document.getElementById("hidBackcolor").value;
//            style=style+";border:solid 1px "+document.getElementById("hidBackcolor").value;
//        }
        
        
        if(document.getElementById("chkBold").checked==true)
        {
             style=style+";font-weight:bold"; 
        }
        else
        {                    
            style=style+";font-weight:normal"; 
        }
        if(document.getElementById("chkItalic").checked==true)
        {
             style=style+";font-style:italic";                
        }    
        else
        {
             style=style+";font-style:normal"; 
        }
        if(document.getElementById("chkUnderline").checked==true)
        {
             style=style+";text-decoration:underline";                
        }
        else
        {
            style=style+";text-decoration:none";                
        }
        if(document.getElementById("txtHeight").value!="" )
        {
            style=style+";height:"+document.getElementById("txtHeight").value+"px";
        }
        if(document.getElementById("txtWidth").value!="" && parseInt(document.getElementById("txtWidth").value)<=730)
        {
            style=style+";width:"+document.getElementById("txtWidth").value+"px";
        }
        else if(document.getElementById("txtWidth").value!=""  && parseInt(document.getElementById("txtWidth").value)>730)
        {
            style=style+";width:730px";
        }
        
             window.opener.updateFormat(style);//Update the parentwindow
             alert("Format Has Been Applied");
        
       }
        function setFormula(ope) // set formula
       {
            if (!blank(ope))
				{
					
					if (blank(document.getElementById("txtFormula").value))
					{
						//document.getElementById("txtFormula").value = document.getElementById("txtFormula").value +  ope;
						alert("Invalid Operation");
					}
					else 
					{
						document.getElementById("txtFormula").value = document.getElementById("txtFormula").value +  ope;
					}
				}            
       }
   
       function chkNumeric(txtObj) // to chk the entered value is numeric
       {
               if (isNaN(txtObj.value) == true)
				{
					txtObj.value = "";
					alert("Enter only numeric value!!");
					txtObj.value="0";
					txtObj.focus();
				}
       }
       function addNumeric() //to add numeric value to the txtFormula
       {
           document.getElementById("txtFormula").value = document.getElementById("txtFormula").value +"+"+document.getElementById("txtaddformula").value ;    
       }
         function setColorcon() // set Color condition
       {
            
       }
        
        function btnMorecolumn_onclick() // open getTables to add more columns
        {
            window.open("getTables.aspx?src=setData","AddColumns","width=450,height=600,scrollbars=yes,status=yes");
        }

        function showColumns(strTbl)  // add columns to the dropdown from gettables.aspx
        {
            var tb=strTbl.split("~");
            var i=0;
            var j=0;
            if(document.getElementById("hidTblall").value!="")
            {
                var temp=document.getElementById("hidTblall").value;
                var tempN=temp.split(",")
                 //// prepare final tables
                for(i=0;i<=tb.length-1;i++)
                {
                    for(j=0;j<=tempN.length-1;j++)
                    
                    if(tb[i]!=tempN)
                    {                   
                        document.getElementById("hidTblall").value=document.getElementById("hidTblall").value+","+tb[i];             
                    }                
                }
            }
            else
            {
                 for(i=0;i<=tb.length-1;i++)
                {
                    if(document.getElementById("hidTblall").value=="")
                    {
                        document.getElementById("hidTblall").value= tb[i];
                     }
                     else
                     {
                        document.getElementById("hidTblall").value= document.getElementById("hidTblall").value+","+ tb[i];
                     }
                }
            }
            for(i=0;i<=tb.length-1;i++)
            {
                ReportDesignerAjax.GetTableFields(tb[i], pasteField);
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
                var theSel=document.getElementById("selColumn");
                var cnt=theSel.length;
                for(i=0;i<=resNew.length-1;i++)
                {
                    var nStr=resNew[i].replace("$",".");
                    var newOpt1 = new Option(nStr, nStr);
                    theSel.options[cnt]= newOpt1;
                    cnt++;
                }
                theSel.value=document.getElementById("hidObjvalue").value;
                
             }
        }

        function cbofdata_onclick()  //Select data
        {
               document.getElementById("txtFormula").value=document.getElementById("txtFormula").value+document.getElementById("cbofdata").value;
        }
        function showObjects(theObj) // Show objects
        {       
                document.getElementById("txtVal").value="";
                document.getElementById("selFsize").style.visibility="hidden";
                toFill=theObj;
                fillObjects();
        }
        function btnOk_onclick()  // color condition done
        {
            document.getElementById("selFsize").style.visibility="visible";
            document.getElementById(toFill).value=document.getElementById(toFill).value+document.getElementById("txtVal").value;                   
        }
     
        function fillCombo(Response)   // fill combo with columns
        { 
             if (Response!="")
            {
                var res=Response.value;
                var resNew=res.split("~"); // Split all the columns
                var res2=resNew[0].split("$"); // Split to get the table name
                var i=0;
                var theSel=document.getElementById("selVal");
                theSel.options.length=0;
                for(i=0;i<=resNew.length-1;i++)
                {
                    var nStr=resNew[i].replace("$",".");
                    var newOpt1 = new Option(nStr, nStr);
                    theSel.options[i] = newOpt1;
                }                     
             }
        }       
        
       
        function closeWin()  // close window
        {
            window.parent.focus();
            window.self.close();
        }

        function selData() // Select Data
        {
            document.getElementById("txtVal").value=document.getElementById("txtVal").value+document.getElementById("selVal").value;
        }

        function selCondition_onchange()  // Set condition
        {        
        
           if(document.getElementById("selCondition").value=="between" || document.getElementById("selCondition").value=="not between")
            {
              
                document.getElementById("btnPickval1").style.visibility="visible";
                document.getElementById("txtVal1").style.visibility="visible";
            }
            else
            {
                document.getElementById("btnPickval1").style.visibility="hidden";
                document.getElementById("txtVal1").style.visibility="hidden";
            }

        }


       

        function btnSetformula_onclick() 
        {
       
             if(document.getElementById("txtFormulaname").value=="" || document.getElementById("txtCondition").value=="")
            {
                alert("Either Formula Or Formula Name Is Empty");
            }
            else
            {
           
                // use ajax to update formula for existing, check for existing formula name in case of new formula
                    var formula=document.getElementById("hidObjname").value+"@#@"+document.getElementById("txtCondition").value+" AS ["+document.getElementById("txtFormulaname").value+"]";
                    //document.getElementById("changecolorcondition").value=document.getElementById("txtCondition").value;
                    ReportDesignerAjax.updateFormulaobjects(window.opener.document.getElementById("btnEx").value,formula,document.getElementById("hidExformula").value,window.opener.document.getElementById("hidDformula").value,finale);
                
            }
        }
       function finale(response)
       {
 
            if(response.value!=null || response.value!="undefined")
            {
                var sd=response.value.split(":");
                 var formula=document.getElementById("hidObjname").value+"@#@["+document.getElementById("txtFormulaname").value+"]";
                if(sd[1]=="new")
                {
                    document.getElementById("hidExformula").value=document.getElementById("txtCondition").value+" AS ["+document.getElementById("txtFormulaname").value+"]";
                    window.opener.document.getElementById("hidDformula").value=sd[0];
                    //added
//                    var edm=window.opener.document.getElementById("hidColorcondition").value;
//                    var chkvald=document.getElementById("changecolorcondition").value;
//                    edm=edm.replace(document.getElementById("changecolorcondition").value,sd[0]);
//                    window.opener.document.getElementById("hidColorcondition").value=edm;
                    //
                    window.opener.updateFormula(formula);
                    alert("The Supplied Formula Has Been Set.")
                }
                else if(sd[1]=="updated")
                {
                    document.getElementById("hidExformula").value=document.getElementById("txtCondition").value+" AS ["+document.getElementById("txtFormulaname").value+"]";
                    window.opener.document.getElementById("hidDformula").value=sd[0];
                    window.opener.updateFormula(formula);
                       //added
//                    var edm=window.opener.document.getElementById("hidColorcondition").value;
//                    var chkvald=document.getElementById("changecolorcondition").value;
//                    edm=edm.replace(document.getElementById("changecolorcondition").value,sd[0]);
//                    window.opener.document.getElementById("hidColorcondition").value=edm;
                    //
                    alert("The Formula Has Been Updated.");
                }
                else if(sd[1]=="already")
                {
                    alert("Formula Name Already Existing.");
                }
            }
       }
        function selWhere_onclick()          //fetch values corresponding to column
        {
//            if(document.getElementById("selWhere").value!="where")
//            {
//            
//                var column=document.getElementById("selWhere").value;
//                ReportDesignerAjax.GetColumnValue(column, getColumn);
//                //ReportDesignerAjax.GetColumnValue(column,getColumn);
//            }
        }
        function getColumn(Response)  // fill column value to selValue
        {
//            if(Response.value!="")
//            {
//               var ds = Response.value;
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
//            }
        }

   
    function btnOkwhere_onclick()
    {
//    var where="";
//        if(document.getElementById("selWhere").value!="where")
//            {
//                if(document.getElementById("selValue").value!="--select--")
//                {
//                    var con="";
//                    if(document.getElementById("selCond").value=="greater than equal to")
//                    {
//                        con=">=";
//                    }
//                    else
//                    {
//                        con=document.getElementById("selCond").value;
//                    }
//                    where=document.getElementById("selWhere").value+con+"'"+document.getElementById("selValue").value+"'";                    
//                    if(document.getElementById("txtWhere").value!="")
//                    {
//                        document.getElementById("txtWhere").value=document.getElementById("txtWhere").value+where;
//                    }
//                    else
//                    {
//                        document.getElementById("txtWhere").value=where;
//                    }
//                    
//                    
//                }
//                else
//                {
//                    alert("Select Value");
//                }
//             }
    } 

    function btnOkcondition_onclick() {
    
    var str="";
    var i=0;
    var b=0;
        if(document.getElementById("selFormula").value!="Select Formula")
        {
            if(document.getElementById("selColumn").value!="Select Column")
            {
             /////// collect tablename to form select query later
             var temp=document.getElementById("selColumn").value;
             var tbl=temp.split(".");
             var ob=document.getElementById("hidFormulatbl").value.split("'");
             
              if(document.getElementById("hidFormulatbl").value=="")
              {
                document.getElementById("hidFormulatbl").value=tbl[0];
                
              }
              else
              {
                for(i=0;i<=ob.length-1;i++)
                {
                    if(tbl[0]==ob[i])
                    {
                        b=1;      // if tablename already exists, then set boolean to 1
                    }
                }
                if(b==0)// if boolean is 0 then assign the tablename
                {
                    document.getElementById("hidFormulatbl").value=document.getElementById("hidFormulatbl").value+","+tbl[0];
                }    
              }
              
              ///////// table collection ends
                str=document.getElementById("selFormula").value+"("+document.getElementById("selColumn").value+")";
                   if(document.getElementById("txtCondition").value!="")
                    {
                        document.getElementById("txtCondition").value=document.getElementById("txtCondition").value+str;
                    }
                    else
                    {
                        document.getElementById("txtCondition").value=str;
                    }           
                
              
            }
            else
            {
                alert("Select Column to apply formula");
            }
        }
        else
        {
            alert("Select formula to apply");
        }
    }
    function addOperator(op)
    {
        document.getElementById("txtCondition").value=document.getElementById("txtCondition").value+op;
    }
    function btnAnd_onclick() 
    {
       // document.getElementById("txtWhere").value=document.getElementById("txtWhere").value+" and ";
    }

    function btnOr_onclick() 
    {
       // document.getElementById("txtWhere").value=document.getElementById("txtWhere").value+" or ";
    }
    function addForcell(op)  // add operatore to the color condition textbox txtForcell
    {
        document.getElementById("txtForcell").value=document.getElementById("txtForcell").value+op;
    }
    
    function closeWindow() 
    {
        window.parent.focus();
        window.self.close();
    }

    function selForcondition_onclick()
    {
        if(document.getElementById("hidSource").value=="details") 
            {
               if(document.getElementById("selConditionon").value=="cell")
               {
                    if(document.getElementById("selCellcondition").value=="in" || document.getElementById("selCellcondition").value=="between" || document.getElementById("selCellcondition").value=="not between")
                    {
                        document.getElementById("selCellvalue").style.display="none";
                        document.getElementById("txtCell").style.display="none";
                        document.getElementById("txtCell").value="";
                        document.getElementById("selCellmulti").style.display="block";
                    }
                    else if(document.getElementById("selCellcondition").value=="starts" || document.getElementById("selCellcondition").value=="ends")
                     {
                        document.getElementById("selCellvalue").style.display="none";
                        document.getElementById("txtCell").style.display="block";
                        document.getElementById("txtCell").value="";
                        document.getElementById("selCellmulti").style.display="none";
                    }
                    else
                     {
                        document.getElementById("selCellvalue").style.display="block";
                        document.getElementById("txtCell").style.display="none";
                        document.getElementById("txtCell").value="";
                        document.getElementById("selCellmulti").style.display="none";
                    }
               }
        }
    }
    
     function getCellvalue()          //fetch values corresponding to column
        {
            if(document.getElementById("hidObjvalue").value!="" && document.getElementById("hidObjvalue").value!=" " )
                {
                ////////
                var column="";
                var myVal=document.getElementById("hidObjvalue").value;
                    var gh=myVal.split(".");
                    
                    if(gh.length>1)
                    {
                        column=myVal;
                          if(column!="")
                          {                   
                            ReportDesignerAjax.GetColumnValue(column, fillCellvalue);
                            //ReportDesignerAjax.GetColumnValue(column,getColumn);
                          } 
                    }
                    else
                    {
                        var tnm=myVal;
                        tnm=tnm.replace("[","");
                        tnm=tnm.replace("]","");
                        var fir=window.opener.document.getElementById("hidDformula").value;
                        var fir1=fir.split("~");
                        var cbn=0;
                        for(cbn=0;cbn<=fir1.length-1;cbn++)
                        {
                            var fir3=fir1[cbn].split(" AS ");
                            var fir4=fir3[1].replace("[","");
                            fir4=fir4.replace("]","");
                            if(fir4==tnm)
                            {
                                var tbl=document.getElementById("hidTblname").value;
                                tbl=tbl.replace("~",",");
                                  tbl=tbl.replace("~",",");
                                column="Select "+fir3[0]+" from "+tbl;
                            }
                            
                        }
                        if(column!="")
                          {                   
                            ReportDesignerAjax.GetFormula(column, fillCellvalue);
                            //ReportDesignerAjax.GetColumnValue(column,getColumn);
                          } 
                    }    
                ///////////////
                
                    
                }
        }
        function fillCellvalue(Response)  // fill column value to selValue
        {
    
            if(Response.value!="")
            {
               var i=0;
               var ds = Response.value;
               document.getElementById("selCellvalue").length=0;
               document.getElementById("selCellmulti").length=0;
				if(ds!= null && typeof(ds) == "object" && ds.Tables!=null)
				{
				
				   	document.getElementById("selCellvalue").options[0]=new Option("--Select--","--Select--");
				    for(i=0;i<ds.Tables[0].Rows.length;i++)
				    {
					    document.getElementById("selCellvalue").options[i+1]=new Option(ds.Tables[0].Rows[i].ColumnName,ds.Tables[0].Rows[i].ColumnName);
				    }
				     for(i=0;i<ds.Tables[0].Rows.length;i++)
				    {
					    document.getElementById("selCellmulti").options[i]=new Option(ds.Tables[0].Rows[i].ColumnName,ds.Tables[0].Rows[i].ColumnName);
				    }
			    }
			    else if(ds!= null && typeof(ds) == "string")
				{
				
				   	document.getElementById("selCellvalue").options[0]=new Option("--Select--","--Select--");
				   
					document.getElementById("selCellvalue").options[1]=new Option(ds,ds);
				   
					document.getElementById("selCellmulti").options[0]=new Option(ds,ds);
				   
			    }
			    if(document.getElementById("selCellvalue").length!=0 && gVal!="")
			    {
			        document.getElementById("selCellvalue").value=gVal;
			        gVal="--Select--";
			    }
            }
        }
    function selConditionon_onclick() 
    {
        if(document.getElementById("hidSource").value=="details") 
        {
                if(document.getElementById("selConditionon").value=="cell")
                {
                    document.getElementById("divFormulacon").style.display='none';
                    document.getElementById("divCell").style.display='block';
                    document.getElementById("trVal").style.display='block';
                    getCellvalue();
                }
                else
                {
                    document.getElementById("trVal").style.display='none';
                    document.getElementById("divFormulacon").style.display='block';
                    document.getElementById("divCell").style.display='none';
                }
        }
    }

   function btnDetialscon_onclick() 
    {
   
        if(document.getElementById("hidB").value=="")
        {
            alert("Provide Back Color");
        }
        else
        {
               var val="";
               var srcMe="";
               if(document.getElementById("hidSource").value=="details") 
                   {
                   srcMe=document.getElementById("hidObjvalue").value;
                    if(document.getElementById("selConditionon").value=="cell")
                    {
                       
                       var obj=document.getElementById("selCellmulti");
                       if(document.getElementById("selCellcondition").value=="between" || document.getElementById("selCellcondition").value=="not between")
                        {
                               var t=0;
                               var k=0;
                               var tmp=""
                               for(t=0;t<=obj.length-1;t++)
                               {
                                    if(obj[t].slected==true && k<2)
                                    {
                                        k=k+1;
                                        if(tmp=="")
                                        tmp=obj[t].value;
                                        else
                                        tmp=tmp+","+obj[t].value;
                                    }
                                    
                               }    
                               if(tmp!="" && k==2)
                               {
                                    val=document.getElementById("selName").value+"#@#cell^"+document.getElementById("selCellcondition").value+"^"+tmp;  // prepare condition
                               }
                               else
                               {
                                    ("Please select two values");
                               }        
                        }
                           else if(document.getElementById("selCellcondition").value=="in")
                           {
                                        var j=0;
                                                                   
                                        for(j=0;j<=obj.length-1;j++)
                                        {
                                            if(obj.options[j].selected==true)
                                            {
                                                if(val=="")
                                                {
                                                    val=obj.options[j].value;
                                                }
                                                else
                                                {
                                                    val=val+","+obj.options[j].value;
                                                }
                                            }
                                        }
                                        if(val!="" || val!=" ")
                                        {
                                            val=document.getElementById("selName").value+"#@#cell^"+document.getElementById("selCellcondition").value+"^"+val;  // prepare condition
                                        }
                                        else
                                        {
                                            alert("Please select value(s)");
                                        }
                          }
                        else if(document.getElementById("selCellcondition").value=="starts" || document.getElementById("selCellcondition").value=="ends")
                         {
                            if(document.getElementById("txtCell").value!="")
                            {
                                val=document.getElementById("selName").value+"#@#cell^"+document.getElementById("selCellcondition").value+"^"+document.getElementById("txtCell").value;
                            }
                            else
                            {
                                alert("Please enter value");
                            }
                         }
                        else
                         {
                            if(document.getElementById("selCellvalue").value!="")
                            {
                               var con=document.getElementById("selCellcondition").value;
                                if(con=="greater than equal to")
                                con=">"+"=";
                                val=document.getElementById("selName").value+"#@#cell^"+con+"^"+document.getElementById("selCellvalue").value;
                            }
                            else
                            {
                                alert("No color condition found");
                            }
                            
                        }           
                    }   
                    else // if the condition is based upon formula
                    {         
                         
                       if(document.getElementById("txtForformula").value!="" || document.getElementById("txtForformula").value!=" ")
                       {
                             var tbl=document.getElementById("hidTblname").value;
                             tbl=tbl.replace("~",",");
                             tbl=tbl.replace("~",",");
                             tbl=tbl.replace("~",",");
                             tbl=tbl.replace("~",",");
                             val=document.getElementById("selName").value+"#@#formula^Select top 1 'a' as abc from "+tbl+" where "+document.getElementById("txtForformula").value;
                       }
                       else
                       {
                            alert("No color condition found");
                       }          
                    } 
                     
                }
                else  // if header/footer elements are in consideration
                {
                   srcMe=document.getElementById("hidObjname").value; 
                    var con=document.getElementById("selCellcondition").value;
                            if(con=="greater than equal to")
                                con=">=";
                    if(document.getElementById("selElements").value=="fixed")
                    {                    
                            val=document.getElementById("selName").value+"#@#fixed^"+con+"^"+document.getElementById("txtCell").value;
                            
                    }
                    else if(document.getElementById("selElements").value=="formula")
                    {
                              var tbl=document.getElementById("hidTblname").value;
                              tbl=tbl.replace("~",",");
                             tbl=tbl.replace("~",",");
                             tbl=tbl.replace("~",",");
                             tbl=tbl.replace("~",",");
                             val=document.getElementById("selName").value+"#@#formula^Select top 1 'a' as abc  from temp where "+document.getElementById("txtForformula").value;
                    }
                    else
                    {
                          val=document.getElementById("selName").value+"#@#objects^"+con+"^"+document.getElementById("selCellvalue").value;
                    }
                }
                if(val!="")
                       {
//                              var src="";
//                            if(document.getElementById("hidSource").value=="details")
//                            {
//                                var scr=document.getElementById("hidObjvalue").value;
//                           
//                                    src=scr.replace("[","");
//                                    src=src.replace("]","");
//                             
//                                
//                            }
//                            else
//                            {
                                src=document.getElementById("hidObjname").value;
//                            }
                             var f=collectFormat();  // call a function to store format value
                             val=val+"^"+f;  // add format value to the condition
                             ReportDesignerAjax.updateColorcondition(document.getElementById("hidColcon").value,document.getElementById("selName").value,val,src,colConfinale);
        //                    if(document.getElementById("hidColcon").value=="" || document.getElementById("hidColcon").value==" ") // finally assign value
        //                    {
        //                        document.getElementById("hidColcon").value=srcMe+"@#@"+val;                        
        //                    }
        //                    else
        //                    {
        //                        document.getElementById("hidColcon").value=document.getElementById("hidColcon").value+"~"+srcMe+"@#@"+val;
        //                    }
                            alert("Color Condition added");
                            refresh();
                            var tyyy=document.getElementById("selName").value;
                            tyyy=tyyy.replace("Condition","");
                            if(tyyy==sno.toString())  // generate new condition serial number
                            {
                                var sel11=document.getElementById("selName");
                                sno=sno+1;
                                var sdf="Condition"+sno;
                                sel11.options[sel11.length]=new Option(sdf,sdf);
                                sel11.value=sdf;
                            }
                            
                        }
        }
    }
// refresh color condition values
    function refresh()
    {
        document.getElementById("selCellcondition").value="=";
        document.getElementById("selCellvalue").value="--Select--";
        document.getElementById("txtCell").value="";
        document.getElementById("txtForformula").value="";
        document.getElementById("fSize").value="8";
        document.getElementById("fontFamilycon").value="Verdana";
        document.getElementById("chkBold").checked=false;
        document.getElementById("chkItalic").checked=false;
        document.getElementById("chkUnderline").checked=false;
        document.getElementById("Sample_1").style.backgroundColor="#ffffff";
        document.getElementById("Sample_2").style.backgroundColor="#ffffff";
        document.getElementById("hidF").value="";
        document.getElementById("hidB").value="";
    }
//////////
    function btnDeletecon_onclick()   // delete color condition
    {
       var tyyy=document.getElementById("selName").value;
        tyyy=tyyy.replace("Condition","");
       if(tyyy==sno.toString())  // generate new condition serial number
        {
            alert("This Condition Name Has Not Been Added.");
        }
       else
       {
            if(document.getElementById("selName").length==0 || document.getElementById("selName").value=="")
            alert("No color condition found");
            else
            {
                var src="";
//                if(document.getElementById("hidSource").value=="details")
//                {
//                    src=document.getElementById("hidObjvalue").value;
//                }
//                else
//                {
                    src=document.getElementById("hidObjname").value;
//                }
                ReportDesignerAjax.deleteColorcondition(document.getElementById("hidColcon").value,document.getElementById("selName").value,src,colConfinale);
                alert("Color condition deleted");
                existingColor(document.getElementById("hidObjname").value);
                //existingColor(document.getElementById("hidObjvalue").value);
                refresh();
            }
        } 
    }
    function colConfinale(response)
    {
   
        if(response.value!=null)
        {
        var a=response.value;
            document.getElementById("hidColcon").value=response.value;
            
        }
    }

    function btnClosecon_onclick() {
   
        window.opener.updateColorcon(document.getElementById("hidColcon").value);
//        window.parent.focus();
//        window.close();
    }
        function btnCloseconsaved_onclick()
        {
            window.parent.focus();
        window.close();
        }

    function Button3_onclick() {
        window.open("getTables.aspx?src=colCon","AddColumns","width=450,height=600,scrollbars=yes,status=yes");
    }
    function addColorcolumns(strTbl)  // add columns to the dropdown from gettables.aspx
    {
           var tb=strTbl.split("~");
           var i=0;
           var j=0;
            if(document.getElementById("hidTblall").value!="")
            {
                var temp=document.getElementById("hidTblall").value;
                var tempN=temp.split(",")
                 //// prepare final tables
                for(i=0;i<=tb.length-1;i++)
                {
                    for(j=0;j<=tempN.length-1;j++)
                    
                    if(tb[i]!=tempN)
                    {                   
                        document.getElementById("hidTblall").value=sdocument.getElementById("hidTblall").value+","+tb[i];             
                    }                
                }
            }
            else
            {
                 for(i=0;i<=tb.length-1;i++)
                {
                    if(document.getElementById("hidTblall").value=="")
                    {
                        document.getElementById("hidTblall").value= tb[i];
                     }
                     else
                     {
                        document.getElementById("hidTblall").value= document.getElementById("hidTblall").value+","+ tb[i];
                     }
                }
            }
            for(i=0;i<=tb.length-1;i++)
            {
                ReportDesignerAjax.GetTableFields(tb[i], fillColumnname);
            }
        }
        function addOp(opr)
        {
            if(document.getElementById("txtForformula").value!="" || document.getElementById("txtForformula").value!=" ") 
            {
                document.getElementById("txtForformula").value=document.getElementById("txtForformula").value+opr;
            }
            else
            {
                alert("Invalid attempt to add an operator");
            }
        }
        function collectFormat()
        {
               var format="font-size:"+document.getElementById("fSize").value+"pt;";
               format=format+"font-family:"+document.getElementById("fontFamilycon").value;
               if(document.getElementById("chkB").checked==true)
               {
                    format=format+";font-weight:bold";
               }
               else
               {
                    format=format+";font-weight:normal";
               }
               if(document.getElementById("chkI").checked==true)
               {
                    format=format+";font-style:italic";
               }
               else
               {
                    format=format+";font-style:none";
               }
               if(document.getElementById("chkU").checked==true)
               {
                    format=format+";text-decoration:underline";
               }
               else
               {
                    format=format+";text-decoration:none";
               }
               if(document.getElementById("hidF").value!="")
               {
                    format=format+";color:"+document.getElementById("hidF").value;
               }
               if(document.getElementById("hidB").value!="")
               {
                    format=format+";background-color:"+document.getElementById("hidB").value;
               }
            return format;
        }
//        function btnForformula_onclick() 
//        {
//            if(document.getElementById("selColumnname").value!="--Select--")
//            {
//                document.getElementById("txtForformula").value=document.getElementById("txtForformula").value+document.getElementById("selMath").value+"("+document.getElementById("selColumnname").value+")";
//                var tbl=document.getElementById("selColumnname").value.split("$");
//                if(document.getElementById("hidSubtable").value=="")
//                    document.getElementById("hidSubtable").value=tbl[0];
//                else
//                {
//                    var b=0;
//                    var tmp=document.getElementById("hidSubtable").value.split(",");
//                    var i=0;
//                    for(i=0;i<=tmp.length-1;i++)
//                    {
//                        if(tmp[i]==tbl[0])
//                        b=1;
//                    }
//                    if(b==0)
//                        document.getElementById("hidSubtable").value=document.getElementById("hidSubtable").value+","+tbl[0];          
//                }
//            }
//            else
//            alert("Please select a coulmn name");
//        }

        function selElements_onclick() 
        {
            if(document.getElementById("selElements").value=="fixed")
            {
                document.getElementById("divCell").style.display='block';
                document.getElementById("txtCell").style.display='block';
                document.getElementById("selCellvalue").style.display='none';
                document.getElementById("divCell").style.display='block';
                document.getElementById("divFormulacon").style.display='none';
            }
            else if(document.getElementById("selElements").value=="formula")
            {
                 document.getElementById("divCell").style.display='none';
                 document.getElementById("divFormulacon").style.display='block';
                 document.getElementById("divCell").style.display='none';
                 
            }
            else
            {
                document.getElementById("divCell").style.display='block';
                document.getElementById("txtCell").style.display='none';
                document.getElementById("divCell").style.display='block';
                
                document.getElementById("selCellvalue").style.display='block';
                document.getElementById("divFormulacon").style.display='none';
                var d=document.getElementById("hidObjall").value;
                document.getElementById("selCellvalue").length=0;
                if(d!="" || d!=" ")
                {                    
                    var d1=d.split("~");
                    var t=0;
                    for(t=0;t<=d1.length-1;t++)
                    {
                        document.getElementById("selCellvalue").options[t]=new Option(d1[t],d1[t]);
                    }
                }
            }
        }

    function btnOtherdone_onclick()  // to set formula to header & footer elements
    {
  
        if(document.getElementById("txtSql").value!="")
        {       
            
            var source="hidFformula";
            if(window.opener.document.getElementById("divType").value=="header")
            {
                source="hidHformula";
            }
            // use ajax to update/add formula
            ReportDesignerAjax.updateDeleteFormulas(document.getElementById("hidObjname").value,document.getElementById("txtSql").value,window.opener.document.getElementById(source).value,upDel);
            
            
        }
        else
        {
            alert("No SQL Statement Found.");
        }
    }
    // updated/deleted formulas of header/footer returned
    function upDel(res)
    {
        if(res.value!=null || res.value!="undefined")
        {
            if(res.value=="Syntax Error")
            {
                 alert("Formula Cannot Be Saved. Supplied Formula Is Not Valid");
            }
            else if(res.value=="No Alias")
            {
                alert("Formula Cannot Be Saved. No Alias Provided.")
            }
            else
            {
                var fnm=res.value.split("^^");
                var source="hidFformula";
                if(window.opener.document.getElementById("divType").value=="header")
                {
                    source="hidHformula";
                }
                window.opener.updateFormula(document.getElementById("hidObjname").value+"@#@"+fnm[1]+"");
                window.opener.document.getElementById(source).value=fnm[0];
               if(fnm[1]!="")
               {
                     alert("Supplied Formula Has Been Set");
               }
             }
        }
    }
    function selName_onclick() 
    {
        
        var t=document.getElementById("selName").value; // condition name
        var ty=t.replace("Condition","");
        var y=parseInt(ty);
//        if(document.getElementById("hidSource").value=="details")
//        {
//            btnObj=document.getElementById("hidObjvalue").value;
//        }
//        else
//        {
           btnObj=document.getElementById("hidObjname").value;
//        }
        // if btn onj is formula
         
//                btnObj=btnObj.replace("[","");
//                btnObj=btnObj.replace("]","");
          
        /////////////////////
        if(y<sno)
        {
            // fill existing condition for updation or deletion
            document.getElementById("btnDetialscon").value="Update";
             document.getElementById("imgEdit").style.display="block";
             document.getElementById("imgNew").style.display="none";
             var val=document.getElementById("hidColcon").value; // val=btn1@#@condition1#@#Formula/cell^condition^format,condition2#@#..... ~ btn2@#@....
             var sel=document.getElementById("selName");
            if(val!="")
            {
                var val1=val.split("~"); 
                var y=0;
                for(y=0;y<=val1.length-1;y++)
                {
                    var sp=val1[y].split("@#@");
                    if(sp[0]==btnObj) // if button found
                    {
                    var cFormat="";
                        // check for existing conditions
                            var val2=sp[1].split("##");
                            var t1=0;
                            for(t1=0;t1<=val2.length-1;t1++)
                            {
                                var val3=val2[t1].split("#@#");
                                if(val3[0]==t) // if condition name matches
                                {
                                    var nxt=val3[1].split("^");
                                    var n=0;
                                    
                                        if(document.getElementById("hidSource").value=="details")
                                        {
                                            document.getElementById("selConditionon").value=nxt[0];
                                            selConditionon_onclick();
                                            if(nxt[0]=="cell")
                                            {
                                                if(nxt[1]==">=")
                                                {
                                                    document.getElementById("selCellcondition").value="greater than equal to";
                                                }
                                                else
                                                {
                                                    document.getElementById("selCellcondition").value=nxt[1];
                                                }
                                                selForcondition_onclick();
                                                if(document.getElementById("selCellValue").style.display=="block")
                                                {
                                                    gVal=nxt[2];
                                                    document.getElementById("selCellValue").value=nxt[2];
                                                }
                                                else if(document.getElementById("txtCell").style.display=="block")
                                                {
                                                    document.getElementById("txtCell").value=nxt[2];
                                                }
                                                else
                                                {
                                                    var k=0;
                                                    // split value and assign to multiselect
                                                }
                                                cFormat=nxt[3];
                                            }
                                            else // if formula is selected
                                            {
                                                var gh=nxt[1].split(" where ");
                                                if(gh.length>1)
                                                {
                                                    document.getElementById("txtForformula").value=gh[1];
                                                }
                                                cFormat=nxt[2];
                                            }
                                        }
                                        else // if target is header/ footer element
                                        {
                                            document.getElementById("selElements").value=nxt[0];
                                            selElements_onclick();
                                        }
                                    
                                }
                            }
                        //
                    }
                }
            }
            //'''''''' fill format
            if(cFormat!="")
            {
                    var as=cFormat.split(";");
                    var h0=0;
                    for(h0=0;h0<=as.length-1;h0++)
                    {
                       var st1=as[h0].split(":");
                      if(st1[0]=="font-size")
                      {
                        var bnm=st1[1].replace("pt","");
                        document.getElementById("fSize").value=bnm;
                      }
                      else if(st1[0]=="font-family")
                      {
                        document.getElementById("fontFamilycon").value=st1[1];
                      }
                       else if(st1[0]=="font-weight")
                       {
                          if(st1[1]=="bold")
                            document.getElementById("chkB").checked=true;
                           else
                            document.getElementById("chkB").checked=false;
                    }
                   else if(st1[0]=="font-style")
                   {
                           if(st1[1]=="italic")
                            document.getElementById("chkI").checked=true;
                           else
                            document.getElementById("chkI").checked=false;
                   }
                   else if(st1[0]=="text-decoration")
                   {
                        if(st1[1]=="underline")
                            document.getElementById("chkU").checked=true;
                           else
                            document.getElementById("chkU").checked=false;
                   }
                  
                   else if(st1[0]=="color")
                   {
                        document.getElementById("hidF").value=st1[1];
                        document.getElementById("sample_1").style.backgroundColor=st1[1];
                   }
                   else if(st1[0]=="background-color")
                   {
                        document.getElementById("hidB").value=st1[1];
                        document.getElementById("sample_2").style.backgroundColor=st1[1];
                   }
                   
                }
            }
            //''''''''''''''''''''
            //
        }
        else  // if condition is new
        {
            document.getElementById("imgEdit").style.display="none";
            document.getElementById("imgNew").style.display="block";
            document.getElementById("btnDetialscon").value="Add>>";
             refresh();
        }
    }

    // remove formula from header/footer element
    function btnRemoveformula_onclick()
    {
           if(document.getElementById("txtSql").value!="")
           {
                var formula=document.getElementById("hidObjname").value+"@#@";
                var source="hidFformula";
                if(window.opener.document.getElementById("divType").value=="header")
                {
                    source="hidHformula";
                }
                // use ajax to update/add formula
                ReportDesignerAjax.updateDeleteFormulas(document.getElementById("hidObjname").value,"",window.opener.document.getElementById(source).value,upDel);
                window.opener.updateFormula(formula);
                document.getElementById("txtSql").value="";
                alert("Formula Deleted.");
            }
            else
            {
                alert("No Formula Found");
            }
    }
    </script>
</head> 
<body onload="onLoad();">
    <form id="formSetdata" dir="ltr" lang="en" title="Set Data" runat="server">
    <div id="divSetdata" >
           <div align="center" style="width:100%;height:25px;"><label id="lblbutton" for="tblSetdata"  class="label"></label></div> 
          <table id="tblSetdata" title="Set data" summary="This table holds the divisions to set the format, formula and color condition on an object" style="width: 402px">
            <tr>
                <td colspan="3" scope ="colgroup" >
                    <table>
                        <tr>
                            <td style="width: 121px" title="Format" scope="col">
                                <img id="imgFormat" src="../images/forDG.jpg" onclick="return showFormat();" alt="Set Format" title="Click to set format" />
                            </td>
                            <td style="width: 122px" title="Formula" scope="col">
                                <img id="imgFormula" src="../images/formulaG.jpg" onclick="return showFormula();" alt="Set Formula" title="Click to set formula" />
                            </td>
                            <td style="width: 162px" title="Color Condition" scope="col">
                                <img id="imgColorcon" src="../images/colG.jpg" onclick="return showColorcon();" alt="Set Color Condition" title="Click to set color condition" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" scope ="colgroup" >
                                <hr style="height:2px;color:#00cccc" />
                            </td>
                        </tr>
                    </table>
                
                </td>
                
            </tr>
            <tr>
                <td colspan="3" title="Format" scope="colgroup">
                
                    <div id="divFormat" title="Format" style="display:block;">
                        <table id="tblFormat" title="Format" summary="holds the format options">
                            <tr>
                                <td style="width: 109px; color : Black " title="Font-Size" scope="col" class="label">
                                    <label for="ddlFontsize" title="Font-Size">Font Size:</label> 
                                </td>
                                <td style="width: 269px" title="Select Font-Size" scope="col">
                                    <select runat="server"  id="ddlFontsize" style="width: 55px;" title="Select font size" class="dropdownlist">
                                        <option>8</option>
                                        <option>10</option>
                                        <option>12</option>
                                        <option>14</option>
                                        <option>16</option>
                                        <option>18</option>
                                        <option>20</option>
                                        <option>24</option>
                                        <option>28</option>
                                        <option>32</option>
                                        <option>36</option>
                                        <option>40</option>
                                        <option>48</option>
                                        <option>56</option>
                                        <option>72</option>
                                    </select></td>
                            </tr>
                            <tr>
                                <td style="width: 109px; color : Black " title="Font-Family" scope="col" class="label">
                                    <label for="ddlFontfamily" title="Font-Family">Family:</label> 
                                </td>
                                <td style="width: 269px" title="Select Font-Family" scope="col">
                                    <asp:dropdownlist ID="ddlFontfamily"  width="125px" runat="server" ToolTip="Select font family" CssClass="dropdownlist"/>
                                </td>
                            </tr>
                             <tr>
                                <td style="width: 109px; height: 26px; color : Black " scope="col" title="Width" valign="top" class="label">
                                    <label for="txtWidth" title="Width">Width:</label>
                                </td>
                                <td scope ="col"  style="width: 269px; height: 26px;" title="Enter Width" valign="top">
                                    <input id="txtWidth" name="Width" title="Enter width" onblur="chkNumeric(this)" style="width: 50px; height: 13px;" maxlength="3" class="textBox" />
                                    px
                               </td>
                            </tr>   
                             <tr>
                                <td style="width: 109px; color : Black " valign="top" title="Height" scope="col" class="label">
                                    <label for="txtHeight" title="Height">Height:</label>
                                </td>
                                <td title="Enter Height" scope="col">
                                    <input id="txtHeight" name="Height" title="Enter height" onblur="chkNumeric(this)" style="width: 50px; height: 13px;" maxlength="3" class="textBox" />
                                    px                                    
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 109px" scope="col">
                                </td>
                                <td style="width: 269px; color : Black " title="Text appearance" scope="col">
                                     <label for="chkBold" ></label>
                                    <input id="chkBold" title="Bold" type="checkbox" style="width: 25px" /><span style="font-family: Arial Black;font-size:10pt"><strong>B</strong></span>
                                     <label for="chkItalic" ></label>
                                    <input id="chkItalic" title="Italic" type="checkbox" style="width: 23px; font-size: 10pt;" /><em><span style="font-family: Arial Black;font-size:10pt">I</span></em>&nbsp;
                                     <label for="chkUnderline" ></label>
                                     <input id="chkUnderline" title="Underline" type="checkbox" style="width: 23px" /><span
                                                                        style="font-family: Arial Black; text-decoration: underline;font-size:10pt">U</span>                                                                       
                                     <img id="imgForecolor" align="bottom" alt="Pick Fore Color" onclick="pickerPopup202('hidForecolor','sample_3');" src="../images/frcolor1.jpg" />
                                     <input id="sample_3" style="width:10px;"  title="Fore Color" readonly="readOnly"/>
                                     <img id="imgBackcolor" align="bottom" alt="Pick Back Color" onclick="pickerPopup202('hidBackcolor','sample_4');" src="../images/bkcolor1.jpg" />
                                    <input id="sample_4" style="width: 10px;" title="Back Color" readonly="readOnly" />
                                </td>
                                
                            </tr>
                            
                             <tr>
                                <td style="width: 109px"  scope="col">
                                </td>
                                <td style="width: 269px"  scope="col">
                                     
                                </td>
                            </tr>                            
                             <tr>
                                <td style="width: 109px"  scope="col">
                                </td>
                                <td style="width: 269px" title="Save Formatting" scope="col">
                                    <input type="button" title="Click to save formatting" class="button" value="Ok" id="btnFormatdone"  onclick="return btnFormatdone_onclick()" />
                                    <input type="button" id="btnCloseformat" class="button" onclick="return closeWindow();" value="Close Window" title="Click to Close the window " style="width: 99px"  /></td>
                            </tr>
                        </table>
                    </div>
                    <div id="divFormula" style="display:none;" title="Formula">
                        <table summary="set formula on object" id="tblFordetails" style="width: 400px">
                            <tr>
                                <td scope="col"  title="Select Formula" style="width: 322px; color : Black ">
                                    <label for="selFormula" class="label" title="Select Formula">Select Formula</label>
                                </td>
                                <td scope="col"  title="Select Column" style="width: 347px; color : Black " >
                                    <label for="selColumn" class="label" title="Select Column">Select Column</label>
                                </td>
                                <td scope="col"  title="" style="width: 134px">
                                    
                                </td>
                            </tr>
                            <tr>
                                <td scope="col"  title="Select Formula" style="width: 322px; height: 27px; color : Black " valign="top">
                                       <select id="selFormula" title="Select formula" style="width: 110px" class="dropdownlist">
                                            <option value="Select Formula">Select Formula</option>
                                            <option value="Max">Max of</option>
                                            <option value="Min">Min of</option>
                                            <option value="Count">Count of</option>
                                           <option value="Sum">Sum of</option>
                                            <option value="Avg">Average</option>
                                    </select>
                                 </td>
                                 <td scope="col" title="Select Column" style="height: 27px; width: 347px;" valign="top">
                                      <select id="selColumn" title="Select Column" style="width: 209px" class="dropdownlist">
                                      </select>
                                 </td>
                                 <td scope="col"  title="Add more columns" style="height: 27px; width: 134px;" valign="top">
                                    <input id="btnMorecolumn" class="button" onclick="return btnMorecolumn_onclick()"
                                        style="width: 58px" title="Click to add more columns" type="button" value="More" /></td>
                            </tr>
                            <tr>
                                <td  scope="col" style="width: 322px; color : Black " >
                                    <label for="txtCondition" title="To view the condition click on the ok button" class="label">
                                        View condition</label></td>
                                <td scope="col" title="Click to view condition" style="width: 347px" >
                                    <input id="btnOkcondition" type="button" class="button" value="Ok"  onclick="return btnOkcondition_onclick()"  />
                                </td>
                                <td style="width: 134px"></td>
                            </tr>
                            <tr>
                                <td  scope="colgroup" colspan="2" style="height: 27px">
                                    <textarea id="txtCondition" style="width: 322px; height: 70px" class="textBox"></textarea></td>
                                <td scope="col"  style="height: 27px; width: 134px;" valign="top">
                                    <input id="btnPlus" class="button"  onclick="addOperator('+')"
                                        style="width: 25px; height: 25px;" title="Click to add an operator" type="button" value="+" /><input id="btnMinus" class="button"  onclick="addOperator('-')"
                                        style="width: 25px; height: 25px;" title="Click to add an operator" type="button" value="-" /><br />
                                    <input id="btnMultiply" class="button"  onclick="addOperator('*')"
                                        style="width: 25px; height: 25px;" title="Click to add an operator" type="button" value="*" /><input id="btnDivide" class="button"  onclick="addOperator('/')"
                                        style="width: 25px; height: 25px;" title="Click to add an operator" type="button" value="/" />
                                    <br />
                                    <input id="btnOpen" class="button" onclick="addOperator('(')"
                                        style="width: 25px; height: 25px;" title="Click to add an operator" type="button" value="(" /><input id="btnClose" class="button"  onclick="addOperator(')')"
                                        style="width: 25px; height: 25px;" title="Click to add an operator" type="button" value=")" /></td>
                            </tr>
                     
                            <tr>
                                <td scope="col" title="Enter formula name" style="width: 322px; color : Black ">
                                    <label for="txtFormulaname" title="Enter formula name" class="label">Formula Name:</label>
                                </td>
                                <td colspan="2" scope="colgroup" title="Enter formula name">
                                    <input type="text" class="textBox" title="Enter formula name" id="txtFormulaname" style="width: 207px" maxlength="30" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" scope="colgroup">
                                     <input type="button" id="btnSetformula" class="button" value="Set Formula" title="Click to set formula "  onclick="return btnSetformula_onclick()" style="width: 99px"  />                                    
                                    <input type="button" id="btnCloseformula" class="button" value="Close Window" title="Click to Close the window " style="width: 99px"  onclick="return closeWindow();"  />
                                </td>
                            </tr>
                        </table>
                        <table id="tblForOther" style="width: 399px" summary ="table">
                            <tr>
                                <td style="height: 17px; color : Black " scope ="col">
                                    <label class="label" for="txtSql">Write Your Own SQL Query:</label>
                                    <br />
                                     <span><span style="color: red">** </span>The formula must be any aggregate function. Please provide an alias to represent
                                    the formula as [formula name].</span></td>
                            </tr>
                            <tr>
                                <td scope ="col">
                                    <textarea id="txtSql" cols="*,*" rows="*,*" style="width: 385px; height: 181px" class="textBox"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td scope ="col">
                                    <input type="button" id="btnOtherdone" value="Set Formula" class="button" title="Click To Set Formula" onclick="return btnOtherdone_onclick()"/>
                                    <input type="button" id="btnRemove" class="button" value="Remove Formula" title="Click To Remove Formula "  onclick="return btnRemoveformula_onclick()"  />
                                    <input type="button" id="btnOtherclose" value="Close Window" class="button" title="Close Window" onclick="return closeWindow();"/>
                                </td>
                            </tr>
                        </table>
                    </div>
                   
                   
                    <div id="divDetailscolorcond" style="display:none;">
                        <table style="width: 393px;">
                                <tr>
                                    <td scope="col" title="" style="width: 755px; height: 24px; color : Black ">
                                        <label class="label" for="selConditionon" title="Condition on">Condition on</label>
                                    </td>
                                    <td scope="col" title="" style="height: 24px; width: 227px; color : Black ">
                                        <select id="selConditionon" style="width: 200px" class="dropdownlist" onclick="return selConditionon_onclick()"> 
                                            <option value="cell">Column Value</option>
                                            <option value="formula">Formula</option>
                                        </select>
                                        <label class="label" for="selElements" ></label>
                                        <select id="selElements" style="width: 200px;display:none; color : Black " class="dropdownlist" onclick="return selElements_onclick()">
                                            <option value="fixed">Fixed Value</option>
                                            <%--<option value="formula">Formula</option>--%>
                                            <option value="objects">Objects</option>
                                        </select>
                                    </td>
                                    <td scope ="col">
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td style="width: 755px; color : Black " scope ="col">
                                        <label class="label" for="selName" title="Condition on">Condition Name</label>
                                    </td>
                                    <td style="width: 227px" scope ="col">
                                        <select id="selName" class="dropdownlist" style="width: 201px" language="javascript" onclick="return selName_onclick()" ></select>
                                    </td>
                                     <td scope ="col">
                                        <img src="../images/nw.jpg" style="width: 40px" align="baseline" alt="New Condition" title="New Condition" id="imgNew" /><img style="display:none;width: 30px" src="../images/edit.jpg" align="baseline" alt="Edit Condition" title="Edit Condition" id="imgEdit" />
                                    </td>
                                   
                                </tr>
                                 <tr id="trVal">
                                     <td style="width: 755px; height: 24px;" scope ="col">
                                         <span id="spanCell" class="label"></span>
                                     </td>
                                     <td style="height: 24px; width: 227px; color : Black " scope ="col">
                                     <label class="label" for="selCellcondition" ></label>
                                         <select id="selCellcondition" style="width: 200px" class="dropdownlist" onclick="return selForcondition_onclick()">
                                              <option value="=">equals to</option>
                                              <option value="<>">not equals to</option>
                                              <option value=">">greater than</option>
                                              <option value="<">less than</option>
                                              <option value="greater than equal to">greater than equal to</option>
                                              <option value="<=">less than equals to</option>
                                             <%-- <option value="in">in</option>
                                              <option value="between">between</option>
                                              <option value="not between">not between</option>--%>
                                              <%--<option value="starts">starts with</option>
                                              <option value="ends">ends with</option>--%>
                                          </select>
                                       </td>
                                     <td scope ="col"></td>
                                </tr>
                                <tr>
                                    <td scope="col" title="" colspan="2">
                                            <div id="divCell">
                                                <table style="width: 394px">                                                   
                                                    <tr>
                                                        <td style="width: 176px; color : Black " valign="top" scope ="col">
                                                            <label for ="txtCell" class="label">Value</label>
                                                        </td>
                                                        <td style="width: 244px" align="right" scope ="col">
                                                        <label for ="selCellvalue" class="label"></label>
                                                            <select id="selCellvalue" style="width: 200px" class="dropdownlist">
                                                            </select>
                                                            <input type="text" id="txtCell" class="textBox" style="display: none; width: 192px"/>
                                                             <label for ="selCellmulti" class="label"></label>
                                                            <select id="selCellmulti" class="dropdownlist" multiple="multiple" style="display: none; width: 200px; height: 133px" ></select>
                                                        </td>
                                                    </tr>
                                                </table>            
                                            </div>
                                            <div id="divFormulacon" style="display:none;">
                                                <table style="width: 388px">
                                                   <tr>
                                                    <td style="width: 175px; color : Black " scope ="col">
                                                        <label class="label" for ="txtForformula">Write formula here</label>
                                                    </td>
                                                   </tr>
                                                   <tr>
                                                        <td colspan="2"  valign="top" style="height: 23px; width: 193px;" align="right" scope ="col">
                                                            <textarea id="txtForformula" title="Prepare formula" cols="*,*" rows="*,*" class="textBox" style="width: 377px; height: 200px"></textarea>                                                            
                                                            
                                                            
                                                        </td>
                                                   </tr>
                                                 </table>    
                                                <table  id="tbObject"  style="width: 385px;display:none;">
                                                        <tr>
                                                            <td style="width: 165px; color : Black " scope ="col">
                                                                <label class="label" for ="selObject">Object Name</label>
                                                            </td>
                                                            <td style="width: 261px" scope ="col">
                                                                <select class="dropdownlist" id="selObject" name="selObject" style="width: 199px">
                                                                </select>
                                                            </td>
                                                        </tr>
                                                </table>                                        
                                            </div>
                                    </td>
                                </tr>
                                <tr>
                                                    <td colspan= "2" title="Format" style="layout-grid:line; " scope="colgroup" valign="bottom">
                                                   <label class="label" for ="fSize"></label>
                                                    <select runat="server" id="fSize" style="width: 40px;" title="Select font size" class="dropdownlist">
                                                        <option>8</option>
                                                        <option>10</option>
                                                        <option>12</option>
                                                        <option>14</option>
                                                        <option>16</option>
                                                        <option>18</option>
                                                        <option>20</option>
                                                        <option>24</option>
                                                        <option>28</option>
                                                        <option>32</option>
                                                        <option>36</option>
                                                        <option>40</option>
                                                        <option>48</option>
                                                        <option>56</option>
                                                        <option>72</option>
                                                    </select>
                                                    <label class="label" for ="fontFamilycon"></label>
                                                        <asp:dropdownlist ID="fontFamilycon"  width="125px" runat="server" ToolTip="Select font family" CssClass="dropdownlist"/>
                                                        <label class="label" for ="chkB"></label>
                                                        <input id="chkB" title="Bold" type="checkbox" style="width: 25px" /><span style="font-family: Arial Black;font-size:10pt"><strong>B</strong></span>
                                                        <label class="label" for ="chkI"></label>
                                                        <input id="chkI" title="Italic" type="checkbox" style="width: 23px; font-size: 10pt;" /><em><span style="font-family: Arial Black;font-size:10pt">I</span></em>&nbsp;
                                                        <label class="label" for ="chkU"></label>
                                                        <input id="chkU" title="Underline" type="checkbox" style="width: 23px" /><span style="font-family: Arial Black; text-decoration: underline;font-size:10pt">U</span>
                                                        <img id="imgF" align="bottom" alt="Pick Fore Color" onclick="pickerPopup202('hidF','sample_1');" src="../images/frcolor1.jpg" />
                                                        <input id="sample_1" style="width:10px;" title="Fore Color"  type="text" value="" readonly="readOnly" />
                                                        <img id="imgB" align="bottom" alt="Pick Back Color" onclick="pickerPopup202('hidB','sample_2');"  src="../images/bkcolor1.jpg" />
                                                        <input id="sample_2" style="width: 10px;" title="Back Color" type="text" value="" readonly="readOnly" /> 
                                                   </td>
                                            </tr>
                                            <tr>
                                                  <td colspan="2" title="" scope="colgroup" style="height: 24px">                                                      
                                                       <input type="button" id="btnDetialscon" value="Add>>" class="button" title="Click to add condition" onclick="return btnDetialscon_onclick()" style="width: 81px"  />
                                                       <input type="button" id="btnDeletecon" value="Delete" class="button" title="Click to delete condition" onclick="return btnDeletecon_onclick()" style="width: 91px"  />
                                                       <input type="button" id="btnClosecon" value="Save" class="button" title="Click to save the condition" onclick="return btnClosecon_onclick()" style="width: 94px" />  
                                                       <input type="button" id="btnCloseme" value="Close" class="button" title="Click to save the condition" onclick="return btnCloseconsaved_onclick()" style="width: 103px" />                                                        
                                                  </td>  
                                            </tr>
                        </table>
                    
                    </div>
              </td>
            </tr>
       </table> 
    </div>
    <%-- Hidden Fields Declared--%>
     <input type="hidden" name="hidForecolor"  id="hidForecolor" /> <%-- hidden fore color --%>
     <input type="hidden" name="hidBackcolor"  id="hidBackcolor" /> <%-- hidden back color --%>
     <input type="hidden" name="hidColforecolor"  id="hidColforecolor" /> <%-- hidden fore color --%>
     <input type="hidden" name="hidColbackcolor"  id="hidColbackcolor" /> <%-- hidden back color --%>
     <input type="hidden" runat="server" name="hidObjname" id="hidObjname" /> <%-- hidden object name --%>
     <input type="hidden" runat="server" name="hidFormulatbl" id="hidFormulatbl" /> <%-- hidden tables for formula field --%>
     <input type="hidden" runat="server" name="hidTblname" id="hidTblname" /> <%-- hidden tables --%>
     <input type="hidden" runat="server" name="hidTblall" id="hidTblall" /> <%-- hidden all tables --%>
     <input type="hidden" runat="server" name="hidObjall" id="hidObjall" /> <%-- hidden all objects --%>
     <input type="hidden" runat="server" name="hidObjvalue" id="hidObjvalue" /> <%-- hidden object value --%>
     <input type="hidden" runat="server" name="hidSource" id="hidSource" /> <%-- hidden source of the object --%>
     <input type="hidden" runat="server" name="hidWhere" id="hidWhere" /> <%-- hidden where condition for formula --%>
     <input type="hidden" name="hidConno" value="" id="hidConno" /> <%-- hidden count of color conditions --%>
     <input type="hidden" name="hidColcon"  id="hidColcon" /> <%-- hidden all color condition as a string --%>
      <input type="hidden" name="hidF"  id="hidF" /> <%-- hidden forecolor for color condition --%>
      <input type="hidden" name="hidB"  id="hidB" /> <%-- hidden backcolor for color condition --%>
      <input type="hidden" name="hidSubtable" id="hidSubtable" /> <%-- hidden tables for color condition --%>
      <input type="hidden" name="hidFormulacnt" id="hidFormulacnt"/><%-- hidden formula name counter for details' objects --%>
     <input type="hidden" name="hidExformula" id="hidExformula"/><%-- hidden existing formula for details' objects --%>
    <%-- Hidden Fields Declaration ends--%>
    <input type="hidden" name="changecolorcondition" id="changecolorcondition" /><%--Ranjit changed bcoz color conditions are not working with previous modified formula--%>
    </form>
                                               
</body>
</html>
