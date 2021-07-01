<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="EditView.aspx.vb" Inherits="DataTransfer_EditViewt" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
<script language="javascript"  type ="text/javascript" >
			// 
			function ShowCalendar1()
			{
				document.getElementById("<%=txtval11.ClientId %>").value = window.showModalDialog('../Calendar/Calendar.htm',document.getElementById("<%=txtval11.ClientId%>").value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			}
			function ShowCalendar2()
			{
				document.getElementById("<%=txtval12.ClientId %>").value = window.showModalDialog('../Calendar/Calendar.htm',document.getElementById("<%=txtval12.ClientId %>").value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			}
			function ShowCalendar3()
			{
				document.getElementById("<%=txtval13.ClientId %>").value = window.showModalDialog('../Calendar/Calendar.htm',document.getElementById("<%=txtval13.ClientId %>").value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			}
			function ShowCalendar4()
			{
				document.getElementById("<%=txtval14.ClientId %>").value = window.showModalDialog('../Calendar/Calendar.htm',document.getElementById("<%=txtval14.ClientId %>").value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			}
			function ShowCalendar5()
			{
				document.getElementById("<%=txtval15.ClientId %>").value = window.showModalDialog('../Calendar/Calendar.htm',document.getElementById("<%=txtval15.ClientId %>").value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			}
			function ShowCalendar6()
			{
				document.getElementById("<%=txtval16.ClientId %>").value = window.showModalDialog('../Calendar/Calendar.htm',document.getElementById("<%=txtval16.ClientId %>").value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			}
			function ShowCalendar7()
			{
				document.getElementById("<%=txtval17.ClientId %>").value = window.showModalDialog('../Calendar/Calendar.htm',document.getElementById("<%=txtval17.ClientId %>").value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			}
			function ShowCalendar8()
			{
				document.getElementById("<%=txtval18.ClientId %>").value = window.showModalDialog('../Calendar/Calendar.htm',document.getElementById("<%=txtval18.ClientId %>").value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			}
			function ShowCalendar9()
			{
				document.getElementById("<%=txtval19.ClientId %>").value = window.showModalDialog('../Calendar/Calendar.htm',document.getElementById("<%=txtval19.ClientId %>").value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			}
			function ShowCalendar10()
			{
				document.getElementById("<%=txtval20.ClientId %>").value = window.showModalDialog('../Calendar/Calendar.htm',document.getElementById("<%=txtval20.ClientId %>").value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			}
			//TO CHK LOCAL NONLOCAL
			function chksapnval()
			{
			    if (document.getElementById("<%=cbodept1.ClientId %>").selectedIndex==0)
				    {
				    
				    }
				    else
				    {
			        var usertype1="<%=Session("typeofuser") %>";
					var userid1="<%=Session("userid") %>";
					
					AjaxClass1.Check_span_Local(usertype1,userid1,document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,document.getElementById("<%=cbolob1.ClientId %>").value)
					
				    }
			}
			function chkspan(valcum)
			{
			 	
			        var strdata = valcum.value;
			      
			    if (strdata=="NO")
			    {
			      
			    document.getElementById("<%=chkLocalView.ClientId %>").checked=false;
			    document.getElementById("<%=chkLocalView.ClientId %>").disabled=true;
			    }
			    else
			    {
			  
//			    document.getElementById("<%=chkLocalView.ClientId %>").disabled=false;
			    }
			
			}
			//TO CHK LOCAL NONLOCAL
			function getclient()
			{
				if (document.getElementById("<%=cbodept.ClientId %>").selectedIndex==0)
				{
					for(i=document.getElementById("<%=cboclient.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cboclient.ClientId %>").remove(i);
					}
					for(i=document.getElementById("<%=lsttab1.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=lsttab1.ClientId %>").remove(i);
					}
					for(i=document.getElementById("<%=cbolob.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbolob.ClientId %>").remove(i);
					}
				}
				else
				{
					AjaxClass1.bindclient(document.getElementById("<%=cbodept.ClientId %>").value,filclient)
					var usertype="<%=Session("typeofuser") %>"
					var userid="<%=Session("userid") %>"
					AjaxClass1.bindepttabview(document.getElementById("<%=cbodept.ClientId %>").value,usertype,userid,filtab)
				}
			}
			function filclient(res)
			{
				for(i=document.getElementById("<%=cboclient.ClientId %>").length;i>=0;i--)
				{
					document.getElementById("<%=cboclient.ClientId %>").remove(i);
				}
				var strdata = res.value
				var arrdata = strdata.split("$")
				var arrdata1
				document.getElementById("<%=cboclient.ClientId %>").options[0]=new Option("--Select--");
				for(i=0;i<arrdata.length;i++)
				{
					arrdata1 = arrdata[i].split("#")
					document.getElementById("<%=cboclient.ClientId %>").options[i+1]=new Option(arrdata1[1],arrdata1[0]);
				}
			}
			function getlob()
			{
				var usertype="<%=Session("typeofuser") %>"
				var userid="<%=Session("userid") %>"
				if (document.getElementById("<%=cboclient.ClientId %>").selectedIndex==0)
				{
					for(i=document.getElementById("<%=cbolob.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbolob.ClientId %>").remove(i);
					}
					for(i=document.getElementById("<%=lsttab1.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=lsttab1.ClientId %>").remove(i);
					}
				}
				else
				{
					AjaxClass1.bindlob(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,fillob)
					
				}
				if (document.getElementById("<%=cboclient.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbodept.ClientId %>").selectedIndex!=0)
				{
					AjaxClass1.bindepttabview(document.getElementById("<%=cbodept.ClientId %>").value,usertype,userid,filtab)
				}
				else
				{
					AjaxClass1.bindclienttabview(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,usertype,userid,filtab)
				}
			}
			function fillob(res)
			{
				for(i=document.getElementById("<%=cbolob.ClientId %>").length;i>=0;i--)
				{
					document.getElementById("<%=cbolob.ClientId %>").remove(i);
				}
				var strdata = res.value
				var arrdata = strdata.split("$")
				var arrdata1
				document.getElementById("<%=cbolob.ClientId %>").options[0]=new Option("--Select--");
				for(i=0;i<arrdata.length;i++)
				{
					arrdata1 = arrdata[i].split("#")
					document.getElementById("<%=cbolob.ClientId %>").options[i+1]=new Option(arrdata1[1],arrdata1[0]);
				}
			}
			function gettab()
			{
				var usertype="<%=Session("typeofuser") %>"
				var userid="<%=Session("userid") %>"
//				if (document.getElementById("<%=cbolob.ClientId %>").selectedIndex==0)
//				{
//				
//				for(i=document.getElementById("<%=lsttab1.ClientId %>").length;i>=0;i--)
//					{
//						document.getElementById("<%=lsttab1.ClientId %>").remove(i);
//					}
//				}
//				else if (document.getElementById("<%=cbolob.ClientId %>").selectedIndex==0 && document.getElementById("<%=cboclient.ClientId %>").selectedIndex==0)
//				{
//					AjaxClass1.bindepttabview(document.getElementById("<%=cbodept.ClientId %>").value,usertype,userid,filtab)
//				}
//				else if(document.getElementById("<%=cbolob.ClientId %>").selectedIndex==0)
//				{
//					AjaxClass1.bindclienttabview(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,usertype,userid,filtab)
//				}
//				else
//				{
					AjaxClass1.bindtableview(60,0,0,usertype,userid,filtab)
//				}
			}
			function filtab(res)
			{
				for(i=document.getElementById("<%=lsttab1.ClientId %>").length;i>=0;i--)
				{
					document.getElementById("<%=lsttab1.ClientId %>").remove(i);
				}
				var strdata = res.value
				var arrdata = strdata.split("$")
				var arrdata1
				for(i=0;i<arrdata.length;i++)
				{
					arrdata1 = arrdata[i].split("#")
					document.getElementById("<%=lsttab1.ClientId %>").options[i]=new Option(arrdata1[1],arrdata1[0] + "$" + arrdata1[1]);
				}
			}
            function gettab2()
			{
				var usertype="<%=Session("typeofuser") %>"
				var userid="<%=Session("userid") %>"
				if (document.getElementById("<%=cbolob.ClientId %>").selectedIndex==0)
				{
				
				for(i=document.getElementById("<%=lsttab1.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=lsttab1.ClientId %>").remove(i);
					}
				}
				else if (document.getElementById("<%=cbolob.ClientId %>").selectedIndex==0 && document.getElementById("<%=cboclient.ClientId %>").selectedIndex==0)
				{
					AjaxClass1.bindepttabview(document.getElementById("<%=cbodept.ClientId %>").value,usertype,userid,filtab)
				}
				else if(document.getElementById("<%=cbolob.ClientId %>").selectedIndex==0)
				{
					AjaxClass1.bindclienttabview(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,usertype,userid,filtab)
				}
				else
				{
					AjaxClass1.bindtableview(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,document.getElementById("<%=cbolob.ClientId %>").value,usertype,userid,filtab)
				}
			}
			function getclient1()
			{
		
				if (document.getElementById("<%=cbodept1.ClientId %>").selectedIndex==0)
				{
					for(i=document.getElementById("<%=cboclient1.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cboclient1.ClientId %>").remove(i);
					}
					for(i=document.getElementById("<%=cbolob1.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbolob1.ClientId %>").remove(i);
					}
					 var usertype1="<%=Session("typeofuser") %>";
					var userid1="<%=Session("userid") %>";
					AjaxClass1.Check_span_Local(usertype1,userid1,"0","0","0",chkspan)
				}
				else
				{
				for(i=document.getElementById("<%=cbolob1.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbolob1.ClientId %>").remove(i);
					}
				 var usertype1="<%=Session("typeofuser") %>";
					var userid1="<%=Session("userid") %>";
				AjaxClass1.Check_span_Local(usertype1,userid1,document.getElementById("<%=cbodept1.ClientId %>").value,"0","0",chkspan)
					AjaxClass1.bindclient(document.getElementById("<%=cbodept1.ClientId %>").value,filclient1)
				}
			}
			function filclient1(res)
			{
				for(i=document.getElementById("<%=cboclient1.ClientId %>").length;i>=0;i--)
				{
					document.getElementById("<%=cboclient1.ClientId %>").remove(i);
				}
				var strdata = res.value
				var arrdata = strdata.split("$")
				var arrdata1
				document.getElementById("<%=cboclient1.ClientId %>").options[0]=new Option("--Select--");
				for(i=0;i<arrdata.length;i++)
				{
					arrdata1 = arrdata[i].split("#")
					document.getElementById("<%=cboclient1.ClientId %>").options[i+1]=new Option(arrdata1[1],arrdata1[0]);
				}
			}
			function chklocalenable()
			{
			if (document.getElementById("<%=cboclient1.ClientId %>").selectedIndex==0)
				{
				 var usertype1="<%=Session("typeofuser") %>";
					var userid1="<%=Session("userid") %>";
				AjaxClass1.Check_span_Local(usertype1,userid1,document.getElementById("<%=cbodept1.ClientId %>").value,"0","0",chkspan)
				}
				else
				{
				
			if (document.getElementById("<%=cbolob1.ClientId %>").selectedIndex==0)
				{
				 var usertype1="<%=Session("typeofuser") %>";
					var userid1="<%=Session("userid") %>";
				AjaxClass1.Check_span_Local(usertype1,userid1,document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,"0",chkspan)
				}
				else
				{
				 var usertype1="<%=Session("typeofuser") %>";
					var userid1="<%=Session("userid") %>";
				AjaxClass1.Check_span_Local(usertype1,userid1,document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,document.getElementById("<%=cbolob1.ClientId %>").value,chkspan)
				}
				}
				
			}
			function getlob1()
			{
				
				if (document.getElementById("<%=cboclient1.ClientId %>").selectedIndex==0)
				{
					for(i=document.getElementById("<%=cbolob1.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbolob1.ClientId %>").remove(i);
					}
					var dept=document.getElementById("<%=cbodept1.ClientId %>").value
					var usertype1="<%=Session("typeofuser") %>";
					var userid1="<%=Session("userid") %>";
					AjaxClass1.Check_span_Local(usertype1,userid1,dept,"0","0",chkspan)
				}
				else
				{
				 var usertype1="<%=Session("typeofuser") %>";
					var userid1="<%=Session("userid") %>";
					
				     AjaxClass1.Check_span_Local(usertype1,userid1,document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,"0",chkspan)
				
					AjaxClass1.bindlob(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,fillob1)
				}
			}
			function fillob1(res)
			{
				for(i=document.getElementById("<%=cbolob1.ClientId %>").length;i>=0;i--)
				{
					document.getElementById("<%=cbolob1.ClientId %>").remove(i);
				}
				var strdata = res.value
				var arrdata = strdata.split("$")
				var arrdata1
				document.getElementById("<%=cbolob1.ClientId %>").options[0]=new Option("--Select--");
				for(i=0;i<arrdata.length;i++)
				{
					arrdata1 = arrdata[i].split("#")
					document.getElementById("<%=cbolob1.ClientId %>").options[i+1]=new Option(arrdata1[1],arrdata1[0]);
				}
			}
			function getseldata()
			{
			 
				document.getElementById("<%=txtcols.ClientId %>").value = "";
				document.getElementById("<%=txtgroup.ClientId %>").value = "";
				document.getElementById("<%=txtwherecon.ClientId %>").value = "";
				document.getElementById("<%=txtwherejoin.ClientId %>").value = "";
				/////get selected columns
				for(i=0;i<document.getElementById("<%=lsttab2cols.ClientId %>").length;i++)
				{
					if(document.getElementById("<%=lsttab2cols.ClientId %>").item(i).selected==true)
					{
						if(blank(document.getElementById("<%=txtcols.ClientId %>").value))
						{
							document.getElementById("<%=txtcols.ClientId %>").value = document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value
						}
						else
						{
							document.getElementById("<%=txtcols.ClientId %>").value = document.getElementById("<%=txtcols.ClientId %>").value + "," + document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value
						}
					}
				}
				/////get selected grouped columns
				for(i=0;i<document.getElementById("<%=lstgroup.ClientId %>").length;i++)
				{
					if(document.getElementById("<%=lstgroup.ClientId %>").item(i).selected==true)
					{
						if(blank(document.getElementById("<%=txtgroup.ClientId %>").value))
						{
							document.getElementById("<%=txtgroup.ClientId %>").value = document.getElementById("<%=lstgroup.ClientId %>").item(i).value
						}
						else
						{
							document.getElementById("<%=txtgroup.ClientId %>").value = document.getElementById("<%=txtgroup.ClientId %>").value + "," + document.getElementById("<%=lstgroup.ClientId %>").item(i).value
						}
					}
				}
				//////get selected joins
				var join
				if((document.getElementById("<%=cbocol11.ClientId %>").value!="" && document.getElementById("<%=cbocol11.ClientId %>").value!="--Select--") && (document.getElementById("<%=cbocol21.ClientId %>").value!="" && document.getElementById("<%=cbocol21.ClientId %>").value!="--Select--"))
				{
					join = document.getElementById("<%=cbocol11.ClientId %>").value + "#" + document.getElementById("<%=cbocol21.ClientId %>").value
				}
				if((document.getElementById("<%=cbocol12.ClientId %>").value!="" && document.getElementById("<%=cbocol12.ClientId %>").value!="--Select--") && (document.getElementById("<%=cbocol22.ClientId %>").value!="" && document.getElementById("<%=cbocol22.ClientId %>").value!="--Select--"))
				{
					join = join + "$" + document.getElementById("<%=cbocol12.ClientId %>").value + "#" + document.getElementById("<%=cbocol22.ClientId %>").value
				}
				if((document.getElementById("<%=cbocol13.ClientId %>").value!="" && document.getElementById("<%=cbocol13.ClientId %>").value!="--Select--")  && (document.getElementById("<%=cbocol23.ClientId %>").value!="" && document.getElementById("<%=cbocol23.ClientId %>").value!="--Select--"))
				{
					join = join + "$" + document.getElementById("<%=cbocol13.ClientId %>").value + "#" + document.getElementById("<%=cbocol23.ClientId %>").value
				}
				if((document.getElementById("<%=cbocol14.ClientId %>").value!="" && document.getElementById("<%=cbocol14.ClientId %>").value!="--Select--") && (document.getElementById("<%=cbocol24.ClientId %>").value!="" && document.getElementById("<%=cbocol24.ClientId %>").value!="--Select--"))
				{
					join = join + "$" + document.getElementById("<%=cbocol14.ClientId %>").value + "#" + document.getElementById("<%=cbocol24.ClientId %>").value
				}
				if((document.getElementById("<%=cbocol15.ClientId %>").value!="" && document.getElementById("<%=cbocol15.ClientId %>").value!="--Select--") && (document.getElementById("<%=cbocol25.ClientId %>").value!="" && document.getElementById("<%=cbocol25.ClientId %>").value!="--Select--"))
				{
					join = join + "$" + document.getElementById("<%=cbocol15.ClientId %>").value + "#" + document.getElementById("<%=cbocol25.ClientId %>").value
				}
				if((document.getElementById("<%=cbocol16.ClientId %>").value!="" && document.getElementById("<%=cbocol16.ClientId %>").value!="--Select--") && (document.getElementById("<%=cbocol26.ClientId %>").value!="" && document.getElementById("<%=cbocol26.ClientId %>").value!="--Select--"))
				{
					join = join + "$" + document.getElementById("<%=cbocol16.ClientId %>").value + "#" + document.getElementById("<%=cbocol26.ClientId %>").value
				}
				if((document.getElementById("<%=cbocol17.ClientId %>").value!="" && document.getElementById("<%=cbocol17.ClientId %>").value!="--Select--") && (document.getElementById("<%=cbocol27.ClientId %>").value!="" && document.getElementById("<%=cbocol27.ClientId %>").value!="--Select--"))
				{
					join = join + "$" + document.getElementById("<%=cbocol17.ClientId %>").value + "#" + document.getElementById("<%=cbocol27.ClientId %>").value
				}
				if((document.getElementById("<%=cbocol18.ClientId %>").value!="" && document.getElementById("<%=cbocol18.ClientId %>").value!="--Select--") && (document.getElementById("<%=cbocol28.ClientId %>").value!="" && document.getElementById("<%=cbocol28.ClientId %>").value!="--Select--"))
				{
					join = join + "$" + document.getElementById("<%=cbocol18.ClientId %>").value + "#" + document.getElementById("<%=cbocol28.ClientId %>").value
				}
				if((document.getElementById("<%=cbocol19.ClientId %>").value!="" && document.getElementById("<%=cbocol19.ClientId %>").value!="--Select--") && (document.getElementById("<%=cbocol29.ClientId %>").value!="" && document.getElementById("<%=cbocol29.ClientId %>").value!="--Select--"))
				{
					join = join + "$" + document.getElementById("<%=cbocol19.ClientId %>").value + "#" + document.getElementById("<%=cbocol29.ClientId %>").value
				}
				if((document.getElementById("<%=cbocol20.ClientId %>").value!="" && document.getElementById("<%=cbocol20.ClientId %>").value!="--Select--") && (document.getElementById("<%=cbocol30.ClientId %>").value!="" && document.getElementById("<%=cbocol30.ClientId %>").value!="--Select--"))
				{
					join = join + "$" + document.getElementById("<%=cbocol20.ClientId %>").value + "#" + document.getElementById("<%=cbocol30.ClientId %>").value
				}
				document.getElementById("<%=txtwherejoin.ClientId %>").value = join;
				//////get selected conditions
				var con
				if((document.getElementById("<%=cbocolA1.ClientId %>").value!="" && document.getElementById("<%=cbocolA1.ClientId %>").value!="--Select--"))
				{
					con = document.getElementById("<%=cbocolA1.ClientId %>").value
				}
				if((document.getElementById("<%=cbocolA2.ClientId %>").value!="" && document.getElementById("<%=cbocolA2.ClientId %>").value!="--Select--"))
				{
					con = con + "$" + document.getElementById("<%=cbocolA2.ClientId %>").value
				}
				if((document.getElementById("<%=cbocolA3.ClientId %>").value!="" && document.getElementById("<%=cbocolA3.ClientId %>").value!="--Select--"))
				{
					con = con + "$" + document.getElementById("<%=cbocolA3.ClientId %>").value
				}
				if((document.getElementById("<%=cbocolA4.ClientId %>").value!="" && document.getElementById("<%=cbocolA4.ClientId %>").value!="--Select--"))
				{
					con = con + "$" + document.getElementById("<%=cbocolA4.ClientId %>").value
				}
				if((document.getElementById("<%=cbocolA5.ClientId %>").value!="" && document.getElementById("<%=cbocolA5.ClientId %>").value!="--Select--"))
				{
					con = con + "$" + document.getElementById("<%=cbocolA5.ClientId %>").value
				}
				if((document.getElementById("<%=cbocolA6.ClientId %>").value!="" && document.getElementById("<%=cbocolA6.ClientId %>").value!="--Select--"))
				{
					con = con + "$" + document.getElementById("<%=cbocolA6.ClientId %>").value
				}
				if((document.getElementById("<%=cbocolA7.ClientId %>").value!="" && document.getElementById("<%=cbocolA7.ClientId %>").value!="--Select--"))
				{
					con = con + "$" + document.getElementById("<%=cbocolA7.ClientId %>").value
				}
				if((document.getElementById("<%=cbocolA8.ClientId %>").value!="" && document.getElementById("<%=cbocolA8.ClientId %>").value!="--Select--"))
				{
					con = con + "$" + document.getElementById("<%=cbocolA8.ClientId %>").value
				}
				if((document.getElementById("<%=cbocolA9.ClientId %>").value!="" && document.getElementById("<%=cbocolA9.ClientId %>").value!="--Select--"))
				{
					con = con + "$" + document.getElementById("<%=cbocolA9.ClientId %>").value
				}
				if((document.getElementById("<%=cbocolA10.ClientId %>").value!="" && document.getElementById("<%=cbocolA10.ClientId %>").value!="--Select--"))
				{
					con = con + "$" + document.getElementById("<%=cbocolA10.ClientId %>").value
				}
				document.getElementById("<%=txtwherecon.ClientId %>").value = con;
							
			}
			function setdata()
			{
				////set columns
				var cols = document.getElementById("<%=txtcols.ClientId %>").value;
				var arrcols = cols.split(",");
				for(i=0;i<arrcols.length;i++)
				{
					for(j=0;j<document.getElementById("<%=lsttab2cols.ClientId %>").length;j++)
					{
						if(arrcols[i]==document.getElementById("<%=lsttab2cols.ClientId %>").item(j).value)	
						{
							document.getElementById("<%=lsttab2cols.ClientId %>").item(j).selected = true
						}
					}
				}
				////set grouprd columns
				var grp = document.getElementById("<%=txtgroup.ClientId %>").value;
				var arrgrp = grp.split(",");
				for(i=0;i<arrgrp.length;i++)
				{
					for(j=0;j<document.getElementById("<%=lstgroup.ClientId %>").length;j++)
					{
						if(arrgrp[i]==document.getElementById("<%=lstgroup.ClientId %>").item(j).value)	
						{
							document.getElementById("<%=lstgroup.ClientId %>").item(j).selected = true
						}
					}
				}
				////set joins
				var wherejoin = document.getElementById("<%=txtwherejoin.ClientId %>").value;
				var arrjoin = wherejoin.split("$");
				for(i=0;i<arrjoin.length;i++)
				{ 
					var arrdata = arrjoin[i].split("#");
					switch(i)
					{
						case 0:
							for(j=0;j<document.getElementById("<%=cbocol11.ClientId %>").length;j++)
							{
								if(document.getElementById("<%=cbocol11.ClientId %>").item(j).value == arrdata[0])
								{ 
									document.getElementById("<%=cbocol11.ClientId %>").item(j).selected = true
								}
								if(document.getElementById("<%=cbocol21.ClientId %>").item(j).value == arrdata[1])
								{
									document.getElementById("<%=cbocol21.ClientId %>").item(j).selected = true
								}
							}
							break;
						case 1:
							for(j=0;j<document.getElementById("<%=cbocol12.ClientId %>").length;j++)
							{
								if(document.getElementById("<%=cbocol12.ClientId %>").item(j).value == arrdata[0])
								{ 
									document.getElementById("<%=cbocol12.ClientId %>").item(j).selected = true
								}
								if(document.getElementById("<%=cbocol22.ClientId %>").item(j).value == arrdata[1])
								{
									document.getElementById("<%=cbocol22.ClientId %>").item(j).selected = true
								}
							}
							break;
						case 2:
							for(j=0;j<document.getElementById("<%=cbocol13.ClientId %>").length;j++)
							{
								if(document.getElementById("<%=cbocol13.ClientId %>").item(j).value == arrdata[0])
								{ 
									document.getElementById("<%=cbocol13.ClientId %>").item(j).selected = true
								}
								if(document.getElementById("<%=cbocol23.ClientId %>").item(j).value == arrdata[1])
								{
									document.getElementById("<%=cbocol23.ClientId %>").item(j).selected = true
								}
							}
							break;
						case 3:
							for(j=0;j<document.getElementById("<%=cbocol14.ClientId %>").length;j++)
							{
								if(document.getElementById("<%=cbocol14.ClientId %>").item(j).value == arrdata[0])
								{ 
									document.getElementById("<%=cbocol14.ClientId %>").item(j).selected = true
								}
								if(document.getElementById("<%=cbocol24.ClientId %>").item(j).value == arrdata[1])
								{
									document.getElementById("<%=cbocol24.ClientId %>").item(j).selected = true
								}
							}
							break;
						case 4:
							for(j=0;j<document.getElementById("<%=cbocol15.ClientId %>").length;j++)
							{
								if(document.getElementById("<%=cbocol15.ClientId %>").item(j).value == arrdata[0])
								{ 
									document.getElementById("<%=cbocol15.ClientId %>").item(j).selected = true
								}
								if(document.getElementById("<%=cbocol25.ClientId %>").item(j).value == arrdata[1])
								{
									document.getElementById("<%=cbocol25.ClientId %>").item(j).selected = true
								}
							}
							break;
						case 5:
							for(j=0;j<document.getElementById("<%=cbocol16.ClientId %>").length;j++)
							{
								if(document.getElementById("<%=cbocol16.ClientId %>").item(j).value == arrdata[0])
								{ 
									document.getElementById("<%=cbocol16.ClientId %>").item(j).selected = true
								}
								if(document.getElementById("<%=cbocol26.ClientId %>").item(j).value == arrdata[1])
								{
									document.getElementById("<%=cbocol26.ClientId %>").item(j).selected = true
								}
							}
							break;
						case 6:
							for(j=0;j<document.getElementById("<%=cbocol17.ClientId %>").length;j++)
							{
								if(document.getElementById("<%=cbocol17.ClientId %>").item(j).value == arrdata[0])
								{ 
									document.getElementById("<%=cbocol17.ClientId %>").item(j).selected = true
								}
								if(document.getElementById("<%=cbocol27.ClientId %>").item(j).value == arrdata[1])
								{
									document.getElementById("<%=cbocol27.ClientId %>").item(j).selected = true
								}
							}
							break;
						case 7:
							for(j=0;j<document.getElementById("<%=cbocol18.ClientId %>").length;j++)
							{
								if(document.getElementById("<%=cbocol18.ClientId %>").item(j).value == arrdata[0])
								{ 
									document.getElementById("<%=cbocol18.ClientId %>").item(j).selected = true
								}
								if(document.getElementById("<%=cbocol28.ClientId %>").item(j).value == arrdata[1])
								{
									document.getElementById("<%=cbocol28.ClientId %>").item(j).selected = true
								}
							}
							break;
						case 8:
							for(j=0;j<document.getElementById("<%=cbocol19.ClientId %>").length;j++)
							{
								if(document.getElementById("<%=cbocol19.ClientId %>").item(j).value == arrdata[0])
								{ 
									document.getElementById("<%=cbocol19.ClientId %>").item(j).selected = true
								}
								if(document.getElementById("<%=cbocol29.ClientId %>").item(j).value == arrdata[1])
								{
									document.getElementById("<%=cbocol29.ClientId %>").item(j).selected = true
								}
							}
							break;
						case 9:
							for(j=0;j<document.getElementById("<%=cbocol20.ClientId %>").length;j++)
							{
								if(document.getElementById("<%=cbocol20.ClientId %>").item(j).value == arrdata[0])
								{ 
									document.getElementById("<%=cbocol20.ClientId %>").item(j).selected = true
								}
								if(document.getElementById("<%=cbocol30.ClientId %>").item(j).value == arrdata[1])
								{
									document.getElementById("<%=cbocol30.ClientId %>").item(j).selected = true
								}
							}
							break;
					}
				}
				////set conditions
				var wherecon = document.getElementById("<%=txtwherecon.ClientId %>").value;
				var arrcon = wherecon.split("$");
				for(i=0;i<arrcon.length;i++)
				{ 
					var arrdata1 = arrcon[i].split("#");
					switch(i)
					{
						case 0:
							for(j=0;j<document.getElementById("<%=cbocolA1.ClientId %>").length;j++)
							{
								if(document.getElementById("<%=cbocolA1.ClientId %>").item(j).value == arrdata1[0])
								{ 
									document.getElementById("<%=cbocolA1.ClientId %>").item(j).selected = true
								}
							}
							break;
						case 1:
							for(j=0;j<document.getElementById("<%=cbocolA2.ClientId %>").length;j++)
							{
								if(document.getElementById("<%=cbocolA2.ClientId %>").item(j).value == arrdata1[0])
								{ 
									document.getElementById("<%=cbocolA2.ClientId %>").item(j).selected = true
								}
							}
							break;
						case 2:
							for(j=0;j<document.getElementById("<%=cbocolA3.ClientId %>").length;j++)
							{
								if(document.getElementById("<%=cbocolA3.ClientId %>").item(j).value == arrdata1[0])
								{ 
									document.getElementById("<%=cbocolA3.ClientId %>").item(j).selected = true
								}
							}
							break;
						case 3:
							for(j=0;j<document.getElementById("<%=cbocolA4.ClientId %>").length;j++)
							{
								if(document.getElementById("<%=cbocolA4.ClientId %>").item(j).value == arrdata1[0])
								{ 
									document.getElementById("<%=cbocolA4.ClientId %>").item(j).selected = true
								}
							}
							break;
						case 4:
							for(j=0;j<document.getElementById("<%=cbocolA5.ClientId %>").length;j++)
							{
								if(document.getElementById("<%=cbocolA5.ClientId %>").item(j).value == arrdata1[0])
								{ 
									document.getElementById("<%=cbocolA5.ClientId %>").item(j).selected = true
								}
							}
							break;
						case 5:
							for(j=0;j<document.getElementById("<%=cbocolA6.ClientId %>").length;j++)
							{
								if(document.getElementById("<%=cbocolA6.ClientId %>").item(j).value == arrdata1[0])
								{ 
									document.getElementById("<%=cbocolA6.ClientId %>").item(j).selected = true
								}
							}
							break;
						case 6:
							for(j=0;j<document.getElementById("<%=cbocolA7.ClientId %>").length;j++)
							{
								if(document.getElementById("<%=cbocolA7.ClientId %>").item(j).value == arrdata1[0])
								{ 
									document.getElementById("<%=cbocolA7.ClientId %>").item(j).selected = true
								}
							}
							break;
						case 7:
							for(j=0;j<document.getElementById("<%=cbocolA8.ClientId %>").length;j++)
							{
								if(document.getElementById("<%=cbocolA8.ClientId %>").item(j).value == arrdata1[0])
								{ 
									document.getElementById("<%=cbocolA8.ClientId %>").item(j).selected = true
								}
							}
							break;
						case 8:
							for(j=0;j<document.getElementById("<%=cbocolA9.ClientId %>").length;j++)
							{
								if(document.getElementById("<%=cbocolA9.ClientId %>").item(j).value == arrdata1[0])
								{ 
									document.getElementById("<%=cbocolA9.ClientId %>").item(j).selected = true
								}
							}
							break;
						case 9:
							for(j=0;j<document.getElementById("<%=cbocolA10.ClientId %>").length;j++)
							{
								if(document.getElementById("<%=cbocolA10.ClientId %>").item(j).value == arrdata1[0])
								{ 
									document.getElementById("<%=cbocolA10.ClientId %>").item(j).selected = true
								}
							}
							break;
					}
				}
			}
			function addtable()
			{
				
				if(document.getElementById("<%=lsttab1.ClientId %>").selectedIndex != -1)
				{
					
					getseldata();
					var i = document.getElementById("<%=lsttab2.ClientId %>").length;
					var arrdata = document.getElementById("<%=lsttab1.ClientId %>").value
					var arrdata1 = arrdata.split("$")
					var tabdata 
					var tabdata1 
					var bool=0
					for(i=0;i<=document.getElementById("<%=lsttab2.ClientId %>").length-1;i++)
					{
						tabdata = document.getElementById("<%=lsttab2.ClientId %>").options[i].value;
						tabdata1 = tabdata.split("$");
						if(tabdata1[1]==arrdata1[1])
						{
							bool=1
							break;
						}
					}
					if(bool==1)
					{
						alert("This table is already selected.")
					}
					else
					{
						document.getElementById("<%=lsttab2.ClientId %>").options[i] = new Option(arrdata1[1],arrdata1[0] + "$" + arrdata1[1]);
					}
					for(i=document.getElementById("<%=lsttab2cols.ClientId %>").length;i>=0;i--)
						{
							document.getElementById("<%=lsttab2cols.ClientId %>").remove(i);
							document.getElementById("<%=lstgroup.ClientId %>").remove(i);
							document.getElementById("<%=cbocol11.ClientId %>").remove(i);
							document.getElementById("<%=cbocol12.ClientId %>").remove(i);
							document.getElementById("<%=cbocol13.ClientId %>").remove(i);
							document.getElementById("<%=cbocol14.ClientId %>").remove(i);
							document.getElementById("<%=cbocol15.ClientId %>").remove(i);
							document.getElementById("<%=cbocol16.ClientId %>").remove(i);
							document.getElementById("<%=cbocol17.ClientId %>").remove(i);
							document.getElementById("<%=cbocol18.ClientId %>").remove(i);
							document.getElementById("<%=cbocol19.ClientId %>").remove(i);
							document.getElementById("<%=cbocol20.ClientId %>").remove(i);
							document.getElementById("<%=cbocol21.ClientId %>").remove(i);
							document.getElementById("<%=cbocol22.ClientId %>").remove(i);
							document.getElementById("<%=cbocol23.ClientId %>").remove(i);
							document.getElementById("<%=cbocol24.ClientId %>").remove(i);
							document.getElementById("<%=cbocol25.ClientId %>").remove(i);
							document.getElementById("<%=cbocol26.ClientId %>").remove(i);
							document.getElementById("<%=cbocol27.ClientId %>").remove(i);
							document.getElementById("<%=cbocol28.ClientId %>").remove(i);
							document.getElementById("<%=cbocol29.ClientId %>").remove(i);
							document.getElementById("<%=cbocol30.ClientId %>").remove(i);
							document.getElementById("<%=cbocolA1.ClientId %>").remove(i);
							document.getElementById("<%=cbocolA2.ClientId %>").remove(i);
							document.getElementById("<%=cbocolA3.ClientId %>").remove(i);
							document.getElementById("<%=cbocolA4.ClientId %>").remove(i);
							document.getElementById("<%=cbocolA5.ClientId %>").remove(i);
							document.getElementById("<%=cbocolA6.ClientId %>").remove(i);
							document.getElementById("<%=cbocolA7.ClientId %>").remove(i);
							document.getElementById("<%=cbocolA8.ClientId %>").remove(i);
							document.getElementById("<%=cbocolA9.ClientId %>").remove(i);
							document.getElementById("<%=cbocolA10.ClientId %>").remove(i);
						}
						document.getElementById("<%=cbocol11.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocol12.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocol13.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocol14.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocol15.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocol16.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocol17.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocol18.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocol19.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocol20.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocol21.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocol22.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocol23.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocol24.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocol25.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocol26.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocol27.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocol28.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocol29.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocol30.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocolA1.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocolA2.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocolA3.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocolA4.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocolA5.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocolA6.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocolA7.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocolA8.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocolA9.ClientId %>").options[0]=new Option("--Select--");
						document.getElementById("<%=cbocolA10.ClientId %>").options[0]=new Option("--Select--");
					for(j=0;j<=document.getElementById("<%=lsttab2.ClientId %>").length-1;j++)
					{
						tabdata = document.getElementById("<%=lsttab2.ClientId %>").options[j].value;
						tabdata1 = tabdata.split("$");
						var str = document.getElementById("<%=lsttab2.ClientId %>").options[j].value
						str=str.replace(/,,/g,"")
						var cols = str.split("$");
						var arrcol = cols[0].split(',');
							
						var arrcols= new Array()
						  for(i=0;i<arrcol.length;i++)
						    {
						       if(arrcol[i]!='' || arrcol[i]!="")
						         {
						           arrcols[i]=arrcol[i]
						         }											
						    }
						
						for(i=0;i<arrcols.length;i++)
						{
							arrcols[i] = tabdata1[1] + "." + arrcols[i];
							var k = document.getElementById("<%=lsttab2cols.ClientId %>").length;
							document.getElementById("<%=lsttab2cols.ClientId %>").options[k]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=lstgroup.ClientId %>").options[k]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocol11.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocol12.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocol13.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocol14.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocol15.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocol16.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocol17.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocol18.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocol19.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocol20.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocol21.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocol22.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocol23.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocol24.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocol25.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocol26.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocol27.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocol28.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocol29.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocol30.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocolA1.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocolA2.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocolA3.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocolA4.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocolA5.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocolA6.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocolA7.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocolA8.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocolA9.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
							document.getElementById("<%=cbocolA10.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						}
					}
					document.getElementById("<%=divcon.ClientId %>").style.visibility = "visible";
					setdata();
				}
				
			}
			function getListFormulaLenth()  //setListFormula()
			{
		
			var lenth;
			lenth=window.document.getElementById("<%=lstformula.ClientId %>").lenth
			
			}
			function UpdateFormula(selname,selvalue)
			{
			 
			var flag=0
		    	for(i=0;i<=document.getElementById("<%=lstformula.ClientId %>").length-1;i++)
			    {
			        if(document.getElementById("<%=lstformula.ClientId %>").item(i).text==selname)
			        {
			            document.getElementById("<%=lstformula.ClientId %>").item(i).value=selvalue;
			            flag=1
			        }
			    }
			    if(flag==0)
			    {
			    alert("Formula Name doesn't Exist.");
			    }
			    
			}
			function setListFormula(ObjOption)
			{
		
		document.getElementById("<%=lstformula.ClientId %>").options.add(ObjOption)
			}
			function setListGroup(ObjOption)
			{
		
		document.getElementById("<%=lstgroup.ClientId %>").options.add(ObjOption)
			}
			
			function removetable()
			{
				if(document.getElementById("<%=lsttab1.ClientId %>").selectedIndex != -1)
				{
					getseldata();
					if(document.getElementById("<%=lsttab2.ClientId %>").selectedIndex != -1)
					{
						document.getElementById("<%=lsttab2.ClientId %>").remove(document.getElementById("<%=lsttab2.ClientId %>").selectedIndex);
					}
					var tabdata 
					var tabdata1 
					for(i=document.getElementById("<%=lsttab2cols.ClientId %>").length;i>=0;i--)
						{
							document.getElementById("<%=lsttab2cols.ClientId %>").remove(i);
						document.getElementById("<%=lstgroup.ClientId %>").remove(i);
						document.getElementById("<%=cbocol11.ClientId %>").remove(i);
						document.getElementById("<%=cbocol12.ClientId %>").remove(i);
						document.getElementById("<%=cbocol13.ClientId %>").remove(i);
						document.getElementById("<%=cbocol14.ClientId %>").remove(i);
						document.getElementById("<%=cbocol15.ClientId %>").remove(i);
						document.getElementById("<%=cbocol16.ClientId %>").remove(i);
						document.getElementById("<%=cbocol17.ClientId %>").remove(i);
						document.getElementById("<%=cbocol18.ClientId %>").remove(i);
						document.getElementById("<%=cbocol19.ClientId %>").remove(i);
						document.getElementById("<%=cbocol20.ClientId %>").remove(i);
						document.getElementById("<%=cbocol21.ClientId %>").remove(i);
						document.getElementById("<%=cbocol22.ClientId %>").remove(i);
						document.getElementById("<%=cbocol23.ClientId %>").remove(i);
						document.getElementById("<%=cbocol24.ClientId %>").remove(i);
						document.getElementById("<%=cbocol25.ClientId %>").remove(i);
						document.getElementById("<%=cbocol26.ClientId %>").remove(i);
						document.getElementById("<%=cbocol27.ClientId %>").remove(i);
						document.getElementById("<%=cbocol28.ClientId %>").remove(i);
						document.getElementById("<%=cbocol29.ClientId %>").remove(i);
						document.getElementById("<%=cbocol30.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA1.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA2.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA3.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA4.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA5.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA6.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA7.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA8.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA9.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA10.ClientId %>").remove(i);
						}
						document.getElementById("<%=cbocol11.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol12.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol13.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol14.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol15.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol16.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol17.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol18.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol19.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol20.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol21.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol22.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol23.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol24.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol25.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol26.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol27.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol28.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol29.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol30.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolA1.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolA2.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolA3.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolA4.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolA5.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolA6.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolA7.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolA8.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolA9.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolA10.ClientId %>").options[0]=new Option("--Select--");
					for(j=0;j<=document.getElementById("<%=lsttab2.ClientId %>").length-1;j++)
					{
						tabdata = document.getElementById("<%=lsttab2.ClientId %>").options[j].value;
						tabdata1 = tabdata.split("$");
						var str = document.getElementById("<%=lsttab2.ClientId %>").options[j].value
						var cols = str.split("$");
						var arrcols = cols[0].split(',');
						for(i=0;i<arrcols.length;i++)
						{
							arrcols[i] = tabdata1[1] + "." + arrcols[i];
							var k = document.getElementById("<%=lsttab2cols.ClientId %>").length;
						document.getElementById("<%=lsttab2cols.ClientId %>").options[k]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=lstgroup.ClientId %>").options[k]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol11.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol12.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol13.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol14.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol15.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol16.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol17.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol18.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol19.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol20.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol21.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol22.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol23.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol24.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol25.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol26.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol27.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol28.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol29.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol30.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolA1.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolA2.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolA3.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolA4.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolA5.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolA6.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolA7.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolA8.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolA9.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolA10.ClientId %>").options[k+1]=new Option(arrcols[i],arrcols[i]);
						}
					}
					if(document.getElementById("<%=lsttab2.ClientId %>").length==0)
					{
						document.getElementById("<%=divcon.ClientId %>").style.visibility = "hidden";
					}
					setdata();
				}
			}
			function __doPostBack(eventTarget, eventArgument)
			{
				var theform;
				if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
					theform = document.forms[0];
				}
				else {
					theform = document.forms[0];
				}
				theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
				theform.__EVENTARGUMENT.value = eventArgument;
				theform.submit();
			} 
			function chkvalidname(s)
			{
				var slen = s.length
				var bool=0
				var str = "= &%*/-+\!@#$%^*':;<>,.?{}[]"
				var i
				for(j=0;j<str.length;j++)
				{
					for(i=0;i<slen;i++)
					{
						if(s.charAt(i)==str.charAt(j))
						{
							bool=1
						}
					}
					if(bool==1)
					{
						return true
					}
				}
				return false
			}
			function blank(s)
			{
				var slen = s.length
				var i
				for(i=0;i<slen;i++)
				{
				if(s.charAt(i)!=" ")return false
				}
				return true
			}
			var Flag=1
			var cnt=0
			function chkvalidation()
			{
				
				if(document.getElementById("<%=lsttab2.ClientId %>").length==0) 
				{
					alert("Please add the table to Selected Tables combobox")
				}
				/*else if(document.getElementById("lsttab2cols").selectedIndex==-1)
				{
					alert("Please select columns")
				}*/
				/*else if(document.getElementById("cbocol11").selectedIndex==0 && document.getElementById("cbocol21").selectedIndex==0 && document.getElementById("cbojoin1").selectedIndex==0)
				{
					alert("Please select the columns and join in where clause to join the tables.")
				}*/
//				else if((document.getElementById("<%=cbodept1.ClientId %>").selectedIndex==0) && (document.getElementById("<%=cboclient1.ClientId %>").selectedIndex==0 || document.getElementById("<%=cboclient1.ClientId %>").selectedIndex==-1) && (document.getElementById("<%=cbolob1.ClientId %>").selectedIndex==0 || document.getElementById("<%=cbolob1.ClientId %>").selectedIndex==-1))
//				{
//					alert("Please select the span to create the view.")
//				}
				
				//**********************************changes made by vini***********************************************************
				else if(Flag==0)
				{
					alert("Please enter names for the columns selected.")
					var str=""
					cnt = 0
					document.getElementById("<%=divname.ClientId %>").innerHTML = ""
					str = "<table width=100%>"
					
					strNewColumn = "<%=colname%>"
					var strHeadingValue1 = "<%=heading %>"
					if (strNewColumn =="")
					{
						
						arrNewColumnlen = "1"
					}
					else
					{
						
						arrNewColumn = strNewColumn.split(",")
						arrNewColumnlen = arrNewColumn.length
					}
					
					//var strNewHead = "<%=heading%>"
					//var strNewFormula = "<%=formula%>"
					// changes by suvi
				
					var strNewHead=""
					 
					document.getElementById("<%=txt1stformula.ClientId %>").value="";
					for(i=0;i<=document.getElementById("<%=lstformula.ClientId %>").length-1;i++)
					{
					      document.getElementById("<%=txt1stformula.ClientId %>").value+= document.getElementById("<%=lstformula.ClientId %>").item(i).value + ","; 
					      strNewHead+= document.getElementById("<%=lstformula.ClientId %>").item(i).text + "," 
				    }
					  var strNewFormula= document.getElementById("<%=txt1stformula.ClientId %>").value
					  //changes by me
					var arrNewHead = strNewHead.split(",")
					var strHeadingValue = strHeadingValue1.split(",")
					var arrNewFormula = strNewFormula.split(",")
					var chkVal;
					var lstvall="";
					for(var g=0;g<document.getElementById("<%=lsttab2cols.ClientId %>").length;g++)
					{
					   if(document.getElementById("<%=lsttab2cols.ClientId %>").item(g).selected==true)
						{
				            lstvall+=document.getElementById("<%=lsttab2cols.ClientId %>").item(g).value+",";
						}
					}
					var lstvalf=lstvall.split(",");
					var ls=0;
//					for(i=0;i<document.getElementById("<%=lsttab2cols.ClientId %>").length;i++)
//                  	{
//    					
//                        if(document.getElementById("<%=lsttab2cols.ClientId %>").item(i).selected==true)
//					    {
//					       var splitstring=lstvalf[ls].split(".");
//					       cnt=cnt+1
//					       str = str + "<tr><td width=35%>" + cnt + ". " + document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value + ":</td><td><label for=txtcol" + cnt + "></label><input type=text id=txtcol" + cnt + " name=txtcol" + cnt + " maxlength=50 value=" + splitstring[1] + "></td></tr>"
//					       ls=ls+1;
//					    }
//    					
//					}
					
					for(i=0;i<document.getElementById("<%=lsttab2cols.ClientId %>").length;i++)
					
					{
					  
						if(document.getElementById("<%=lsttab2cols.ClientId %>").item(i).selected==true)
						{
						   
							cnt=cnt+1
							var j=0;
							chkVal="F";
							var name = document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value
							
							var arrname = name.split(".")
							
							for(j=0;j<arrNewColumnlen;j++)
							{
									//alert("in loop")
									if(arrNewColumnlen[j]==document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value)
									{
									  str = str + "<tr><td width=35%>" + cnt + ". " + document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value + ":</td><td><label for=txtcol" + cnt + "></label><input type=text id=txtcol" + cnt + " name=txtcol" + cnt + " maxlength=50 value=" + arrNewHead[j]+ "></td></tr>"
									  chkVal="P";
									
							}
						}
						if(chkVal=="F")
						{
							str = str + "<tr><td width=35%>" + cnt + ". " + document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value + ":</td><td><label for=txtcol" + cnt + "></label><input type=text id=txtcol" + cnt + " name=txtcol" + cnt + " maxlength=50 value=" + strHeadingValue[ls] + "></td></tr>"
							 ls=ls+1;	
								//changed by atul above to get previous input value str = str + "<tr><td width=35%>" + cnt + ". " + document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value + ":</td><td><input type=text id=txtcol" + cnt + " name=txtcol" + cnt + " maxlength=50 value=" + arrname[1] + "></td></tr>"
						}
					}
					}
					
					
					
					for(i=0;i<document.getElementById("<%=lstformula.ClientId %>").length;i++)
					{
						var j=0;
						var hedCount=0;
						chkVal="F";
						 for(j=0;j<arrNewFormula.length;j++)
					     {
						   if(arrNewFormula[j]==document.getElementById("<%=lstformula.ClientId %>").item(i).value)
						   {
						    
							 cnt = cnt+1;
							 n = i+1;
							 if((arrNewColumnlen=="1")&&(strNewColumn ==""))
							 {
								
								
								str = str + "<tr><td>" + cnt + ". " + document.getElementById("<%=lstformula.ClientId %>").item(i).value + ":</td><td><label for=txtcol" + cnt + "></label><input type=text id=txtcol" + cnt + " name=txtcol" + cnt + " value=" + arrNewHead[j] + " maxlength=50></td></tr>"
								chkVal="P";
							 }
							 else
							 {
							    str = str + "<tr><td>" + cnt + ". " + document.getElementById("<%=lstformula.ClientId %>").item(i).value + ":</td><td><label for=txtcol" + cnt + "></label><input type=text id=txtcol" + cnt + " name=txtcol" + cnt + " value=" + arrNewHead[j] + " maxlength=50></td></tr>"
								chkVal="P";
							 }
							}
						}
						if(chkVal=="F")
						{
						 
							cnt = cnt+1
							n = i+1;
							str = str + "<tr><td>" + cnt + ". " + document.getElementById("<%=lstformula.ClientId %>").item(i).value + ":</td><td><label for=txtcol" + cnt + "></label><input type=text id=txtcol" + cnt + " name=txtcol" + cnt + " maxlength=50></td></tr>"
							
						}
					}
					str = str + "</table>"
					document.getElementById("<%=divname.ClientId %>").innerHTML = str;
					Flag=1
				}
				//****************************************end Changes******************************************************
				
				else if(Flag=1)
				{
					var strmsg = ""
					bool=0
					for(i=1;i<=cnt;i++)
					{
						var j = i+88
					
						var suvi=document.forms[0].elements[j].value;
						//if(blank(document.forms[0].elements[j].value))
						if(blank(document.getElementById("txtcol" + i ).value))
						{
							strmsg = strmsg + "Please enter name for column " + i + "\n"
						}
						//else if(chkvalidname(document.forms[0].elements[j].value))
						else if(chkvalidname(document.getElementById("txtcol" + i ).value))
						{
							strmsg = strmsg + "Special characters or spaces are not allowed in the name for column " + i + "\n"
						}
						else 
						{
							for(k=1;k<=cnt;k++)		
							{
								if(i!=k)
								{
									//if(document.forms[0].elements[j].value==document.forms[0].elements[k+88].value)
									if(document.getElementById("txtcol" + i ).value==document.forms[0].elements[k].value)
									{
										//alert(document.Form1.elements[j].name + "=" + document.Form1.elements[k+92].name)
										bool=1
									}
								}
							}
						}
					}
					if(!blank(strmsg))
			
					{
						
						alert(strmsg)
					}
					else if(bool==1)
					{	
						alert("Two columns can not have the same name.")
					}
					else
					{
						for(i=0;i<=document.getElementById("<%=lsttab2.ClientId %>").length-1;i++)
						{
							document.getElementById("<%=lsttab2.ClientId %>").item(i).selected=true
						}
						for(i=0;i<=document.getElementById("<%=lstformula.ClientId %>").length-1;i++)
						{
							var arrformula = document.getElementById("<%=lstformula.ClientId %>").item(i).value.split(",")
							for(j=0;j<=arrformula.length-1;j++)
							{
								document.getElementById("<%=lstformula.ClientId %>").item(i).value = document.getElementById("<%=lstformula.ClientId %>").item(i).value.replace(",","~~")
							}
							document.getElementById("<%=lstformula.ClientId %>").item(i).selected=true
						}
						//__doPostBack('cmdcreate','');
						
						//***************
					
							document.getElementById("<%=txt1sttabcolvalue.ClientId %>").value = "";
					for(i=0;i<document.getElementById("<%=lsttab2cols.ClientId %>").length;i++)
					{
						if(document.getElementById("<%=lsttab2cols.ClientId %>").item(i).selected==true)
						{
					
					document.getElementById("<%=txt1sttabcolvalue.ClientId %>").value+= document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value + ",";
					 }
					 }
			
					 
					
					
					
					 
				
				
			    document.getElementById("<%=txt1stformula.ClientId %>").value=="";
			    //document.getElementById("<%=txt1stformula.ClientId %>").value="<%=formula%>";
			    var strlst=""
			    assign_value_to_listbox1();
			    for(i=0;i<=document.getElementById("<%=lstformula.ClientId %>").length-1;i++)
					{
					
					       strlst= strlst + document.getElementById("<%=lstformula.ClientId %>").item(i).value + ","
					}
		    
				var formname1 =  document.getElementById("<%=txt1stformula.ClientId %>").value
				if (strlst==formname1)
				{
							var arrformulaname = formname1.split(",")
							var arrNewformulalen= arrformulaname.length
							for(j=0;j<arrNewformulalen-1;j++)
							{
							document.getElementById("<%=lstformula.ClientId %>").item(j).value= arrformulaname[j];
							}
							
				}
				else 
				{
				       var arrformulaname = strlst.split(",")
					   var arrNewformulalen= arrformulaname.length
					    for(j=0;j<arrNewformulalen-1;j++)
							{
							    document.getElementById("<%=lstformula.ClientId %>").item(j).value= arrformulaname[j];
				                document.getElementById("<%=txt1stformula.ClientId %>").value="";
					            for(i=0;i<=document.getElementById("<%=lstformula.ClientId %>").length-1;i++)
					                {
					                    document.getElementById("<%=txt1stformula.ClientId %>").value+= document.getElementById("<%=lstformula.ClientId %>").item(i).value + ","; 
					                }
				            }
				  }
					
					  // Finding the name of selected table
					 
					var tabname = "";
				for(i=0;i<document.getElementById("<%=lsttab2.ClientId %>").length;i++)
				{
					if(tabname!="")
					{
						tabname = tabname + "," + document.getElementById("<%=lsttab2.ClientId %>").item(i).text;
					}
					else
					{
						tabname = document.getElementById("<%=lsttab2.ClientId %>").item(i).text;
					}
				}
						var formula = document.getElementById("<%=txt1stformula.ClientId %>").value
					var arrdata=formula.split("+");
					var aliasformula="";
					for(var i=0;i<arrdata.length;i++)
					{
					    if(aliasformula=="")
					       {
					        aliasformula=arrdata[i]
					       }
					     else
					       {
					        aliasformula=aliasformula+" $ "+arrdata[i];
					        }
					}
					// collecting join condition information in single variable 
					
					
					 var joindata='null';
					if(document.getElementById("<%=cbocol11.ClientId %>").value=="")
					{
                    joindata="--Select--";
					}
					else
					{
					joindata=document.getElementById("<%=cbocol11.ClientId %>").value;
					}
					if(document.getElementById("<%=cbocol12.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol12.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol13.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol13.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol14.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol14.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol15.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol15.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol16.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol16.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol17.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol17.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol18.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol18.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol19.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol19.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol20.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol20.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol21.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol21.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol22.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol22.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol23.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol23.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol24.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol24.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol25.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol25.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol26.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol26.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol27.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol27.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol28.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol28.ClientId %>").value
					
					}
					if(document.getElementById("<%=cbocol29.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol29.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol30.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol30.ClientId %>").value;
					
					}
				
					joindata =joindata+"$"+document.getElementById("<%=cbojoin1.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin2.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin3.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin4.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin5.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin6.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin7.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin8.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin9.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin10.ClientId %>").value;

              
                var conditionsdata='null';
					if(document.getElementById("<%=cbocolA1.ClientId %>").value=="")
					{
                    conditionsdata="--Select--";
					}
					else
					{
					conditionsdata=document.getElementById("<%=cbocolA1.ClientId %>").value;
					}
					if(document.getElementById("<%=cbocolA2.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA2.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA3.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA3.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA4.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA4.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA5.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA5.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA6.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA6.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA7.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA7.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA8.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA8.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA9.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA9.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA10.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA10.ClientId %>").value;
					
					}
					conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc11.ClientId %>").value
				conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc12.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc13.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc14.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc15.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc16.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc17.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc18.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc19.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc20.ClientId %>").value
		//******************************
	 
	
	if(document.getElementById("<%=txtval11.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval11.ClientId %>").value
	}
	if(document.getElementById("<%=txtval12.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval12.ClientId %>").value
	}
	if(document.getElementById("<%=txtval13.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval13.ClientId %>").value
	}
	if(document.getElementById("<%=txtval14.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval14.ClientId %>").value
	}
	if(document.getElementById("<%=txtval15.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval15.ClientId %>").value
	}
	if(document.getElementById("<%=txtval16.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval16.ClientId %>").value
	}
	if(document.getElementById("<%=txtval17.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval17.ClientId %>").value
	}
	if(document.getElementById("<%=txtval18.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval18.ClientId %>").value
	}
	if(document.getElementById("<%=txtval19.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval19.ClientId %>").value
	}
	if(document.getElementById("<%=txtval20.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval20.ClientId %>").value
	}
				//******************************************************
			// +"&lstformula="+document.getElementById("<%=txt1stformula.ClientId %>").value
			// +"&lstformula="+aliasformula
			 
//			var ViewType
//			if(document.getElementById("<%=chkLocalView.ClientId %>").checked==true)
//			{
//			ViewType="Local";
//			}
//			else
//			{
//			ViewType="NonLocal";
//			}
			var viewname;
			viewname=document.getElementById("<%=lblname1.ClientId %>").value;
						//***********
						
						//change for groupby
						 var list=document.getElementById("<%=lstgroup.ClientId %>");
	                var len=list.length;
			
					
					
					var gpby =document.getElementById("<%=txtgroup.ClientId %>");
					gpby.value="";
					
					for (var i=0;i< list.length;i++)
                        {
                            if (list.options[i].selected==true)
                            {
                                if(gpby.value=="")
                                {
                                gpby.value=" group by " + list.item(i).value;
                               }
                               else
                               {
                               gpby.value=gpby.value + "," + list.item(i).value;
                               }
                               
                            }
                       }
                    document.getElementById("<%=txtgroup.ClientId %>").value=gpby.value;
					var a = document.getElementById("<%=lstgroup.ClientId %>").value
					//change for groupby	
					var suvidha=document.getElementById("<%=txtgroup.ClientId %>").value;
					document.forms[0].target = "_new";
						//document.Form1.target = "frmexec";
						 
					document.forms[0].action = "ExecView.aspx?type=update&recid=" + <%=Request("recid")%>
					+"&lstformula="+aliasformula//document.getElementById("<%=txt1stformula.ClientId %>").value
				
					+"&lsttab2cols="+document.getElementById("<%=txt1sttabcolvalue.ClientId %>").value
					+"&txtcol="+document.getElementById("<%=txtcols.ClientId %>").value
					+"&viewname="+document.getElementById("<%=lblname1.ClientId %>").value
                                        +"&lstgroup="+document.getElementById("<%=lstgroup.ClientId %>").value
					+"&lsttab2="+tabname
					+"&cbolob1="+0
					+"&cboclient1="+0
					+"&cbodept1="+60
					+"&joindata="+joindata
					+"&conditionsdata="+conditionsdata
					+"&ViewType="+'NonLocal'
					+"&groupby="+ document.getElementById("<%=txtgroup.ClientId %>").value
					
					
						document.forms[0].submit();
						
					}
				}
			}
			function chkname()
			{ 
			
				if(document.getElementById("<%=lsttab2.ClientId %>").length==0) 
				{
					alert("Please put the table(s) in the listbox of selected tables.")
				}
				else if(document.getElementById("<%=lsttab2cols.ClientId %>").selectedIndex==-1)
				{
					alert("Please select columns")
				}
//				else if((document.getElementById("<%=cbodept1.ClientId %>").selectedIndex==0) && (document.getElementById("cboclient1").selectedIndex==0 || document.getElementById("cboclient1").selectedIndex==-1) && (document.getElementById("cbolob1").selectedIndex==0 || document.getElementById("cbolob1").selectedIndex==-1))
//				{
//					alert("Please select department or client or lob under which you have to save the table.")
//				}
				else if(Flag==0)
				{
				 
					//alert("Please enter names for the columns selected.")
					var str=""
					cnt = 0
					document.getElementById("<%=divname.ClientId %>").innerHTML = ""
					str = "<table width=100%>"
					for(i=0;i<document.getElementById("<%=lsttab2cols.ClientId %>").length;i++)
					{
						if(document.getElementById("<%=lsttab2cols.ClientId %>").item(i).selected==true)
						{
							cnt=cnt+1
							var name = document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value
							var arrname = name.split(".")
							str = str + "<tr><td width=35%>" + cnt + ". " + document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value + ":</td><td><label for=txtcol" + cnt + "></label><input type=text id=txtcol" + cnt + " name=txtcol" + cnt + " maxlength=50 value=" + arrname[1] + "></td></tr>"
						}
					}
						
					for(i=0;i<document.getElementById("<%=lstformula.ClientId %>").length;i++)
					{
						cnt = cnt+1
						str = str + "<tr><td>" + cnt + ". " + document.getElementById("<%=lstformula.ClientId %>").item(i).value + ":</td><td><label for=txtcol" + cnt + "></label><input type=text id=txtcol" + cnt + " name=txtcol" + cnt + " maxlength=50></td></tr>"
					}
					
					str = str + "</table>"
					document.getElementById("<%=divname.ClientId %>").innerHTML = str;
					Flag=1
				}
				else if(Flag==1)
				{
					var strmsg = ""
					bool=0
						for(i=1;i<=cnt;i++)
					{
						var j = i+88
					
						//if(blank(document.forms[0].elements[j].value))
						if(blank(document.getElementById("txtcol" + i ).value))
						{
					//		strmsg = strmsg + "Please enter name for column " + i + "\n"
						}
						else if(chkvalidname(document.getElementById("txtcol" + i ).value))
						//else if(chkvalidname(document.forms[0].elements[j].value))
						{
							strmsg = strmsg + "Special characters or spaces are not allowed in the name for column " + i + "\n"
						}
						else 
						{
							for(k=1;k<=cnt;k++)		
							{
								if(i!=k)
								{
									//if(document.forms[0].elements[j].value==document.forms[0].elements[k+88].value)
									if(document.getElementById("txtcol" + i ).value==document.forms[0].elements[k].value)
									{
										//alert(document.Form1.elements[j].name + "=" + document.Form1.elements[k+92].name)
										bool=1
									}
								}
							}
						}
					}
					if(!blank(strmsg))
					{
						
						//alert(strmsg)
					}
					else if(bool==1)
					{	
						alert("Two columns can not have the same name.")
					}
					else if(blank(document.getElementById("<%=txtname.ClientId %>").value))
					{
						alert("Please enter view name")
					}
					else
					{ 
					
						AjaxClass1.chkViewName(document.getElementById("<%=txtname.ClientId %>").value,chkview)
						
					}
				}
			}
			function chkview(res)
			{
				
				if(res.value=="Y")
				{
					alert("View Name already exists. Please enter another name.")
				}
				else
				{
					for(i=0;i<=document.getElementById("<%=lsttab2.ClientId %>").length-1;i++)
					{
							document.getElementById("<%=lsttab2.ClientId %>").item(i).selected=true
					}
					for(i=0;i<=document.getElementById("<%=lstformula.ClientId %>").length-1;i++)
					{
						var arrformula = document.getElementById("<%=lstformula.ClientId %>").item(i).value.split(",")
						for(j=0;j<=arrformula.length-1;j++)
						{
							document.getElementById("<%=lstformula.ClientId %>").item(i).value = document.getElementById("<%=lstformula.ClientId %>").item(i).value.replace(",","~~")
						}
						document.getElementById("<%=lstformula.ClientId %>").item(i).selected=true
					}
						//__doPostBack('cmdsav','');
						document.forms[0].target = "_new";
						//document.Form1.target = "frmexec";
						//gathering variables value
						// Fuction for assinging isttablecolumns value to hidden field 
					
					document.getElementById("<%=txt1sttabcolvalue.ClientId %>").value = "";
					for(i=0;i<document.getElementById("<%=lsttab2cols.ClientId %>").length;i++)
					{
						if(document.getElementById("<%=lsttab2cols.ClientId %>").item(i).selected==true)
						{
					
					document.getElementById("<%=txt1sttabcolvalue.ClientId %>").value+= document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value + ",";
					 }
					 }
			
					 
					
					
					
					 // Fuction for assiging formula value to hidden field
					 document.getElementById("<%=txt1stformula.ClientId %>").value="";
					for(i=0;i<=document.getElementById("<%=lstformula.ClientId %>").length-1;i++)
					{
					    if(document.getElementById("<%=lstformula.ClientId %>").item(i).selected==true)
					    {
					       document.getElementById("<%=txt1stformula.ClientId %>").value+= document.getElementById("<%=lstformula.ClientId %>").item(i).value + ","; 
					     }
					  }
					
					 
					  
					  
					  // Finding the name of selected table
					 
					var tabname = "";
				for(i=0;i<document.getElementById("<%=lsttab2.ClientId %>").length;i++)
				{
					if(tabname!="")
					{
						tabname = tabname + "," + document.getElementById("<%=lsttab2.ClientId %>").item(i).text;
					}
					else
					{
						tabname = document.getElementById("<%=lsttab2.ClientId %>").item(i).text;
					}
				}
				/////aaaaaaaaaaaaaaaaaaaaaaaaaaaa
				 
						var formula = document.getElementById("<%=txt1stformula.ClientId %>").value
					var arrdata=formula.split("+");
					var aliasformula="";
					for(var i=0;i<arrdata.length;i++)
					{
					    if(aliasformula=="")
					       {
					        aliasformula=arrdata[i]
					       }
					     else
					       {
					        aliasformula=aliasformula+" $ "+arrdata[i];
					        }
					}
					// collecting join condition information in single variable 
					
					 var joindata='null';
					if(document.getElementById("<%=cbocol11.ClientId %>").value=="")
					{
                    joindata="--Select--";
					}
					else
					{
					joindata=document.getElementById("<%=cbocol11.ClientId %>").value;
					}
					if(document.getElementById("<%=cbocol12.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol12.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol13.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol13.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol14.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol14.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol15.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol15.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol16.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol16.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol17.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol17.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol18.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol18.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol19.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol19.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol20.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol20.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol21.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol21.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol22.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol22.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol23.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol23.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol24.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol24.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol25.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol25.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol26.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol26.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol27.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol27.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol28.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol28.ClientId %>").value
					
					}
					if(document.getElementById("<%=cbocol29.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol29.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol30.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol30.ClientId %>").value;
					
					}
				
					joindata =joindata+"$"+document.getElementById("<%=cbojoin1.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin2.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin3.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin4.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin5.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin6.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin7.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin8.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin9.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin10.ClientId %>").value;

              
                var conditionsdata='null';
					if(document.getElementById("<%=cbocolA1.ClientId %>").value=="")
					{
                    conditionsdata="--Select--";
					}
					else
					{
					conditionsdata=document.getElementById("<%=cbocolA1.ClientId %>").value;
					}
					if(document.getElementById("<%=cbocolA2.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA2.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA3.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA3.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA4.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA4.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA5.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA5.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA6.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA6.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA7.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA7.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA8.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA8.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA9.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA9.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA10.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA10.ClientId %>").value;
					
					}
					conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc11.ClientId %>").value
				conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc12.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc13.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc14.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc15.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc16.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc17.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc18.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc19.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc20.ClientId %>").value
		//******************************
	 
	
	if(document.getElementById("<%=txtval11.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval11.ClientId %>").value
	}
	if(document.getElementById("<%=txtval12.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval12.ClientId %>").value
	}
	if(document.getElementById("<%=txtval13.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval13.ClientId %>").value
	}
	if(document.getElementById("<%=txtval14.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval14.ClientId %>").value
	}
	if(document.getElementById("<%=txtval15.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval15.ClientId %>").value
	}
	if(document.getElementById("<%=txtval16.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval16.ClientId %>").value
	}
	if(document.getElementById("<%=txtval17.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval17.ClientId %>").value
	}
	if(document.getElementById("<%=txtval18.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval18.ClientId %>").value
	}
	if(document.getElementById("<%=txtval19.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval19.ClientId %>").value
	}
	if(document.getElementById("<%=txtval20.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval20.ClientId %>").value
	}
				//******************************************************
			// +"&lstformula="+document.getElementById("<%=txt1stformula.ClientId %>").value
			// +"&lstformula="+aliasformula
			
//			var ViewType
//			if(document.getElementById("<%=chkLocalView.ClientId %>").checked==true)
//			{
//			ViewType="Local";
//			}
//			else
//			{
//			ViewType="NonLocal";
//			}
						//***********
					
					    var list=document.getElementById("<%=lstgroup.ClientId %>");
	                var len=list.length;
			
					
					
					var gpby =document.getElementById("<%=groupby.ClientId %>");
					gpby.value="";
					
					for (var i=0;i< list.length;i++)
                        {
                            if (list.options[i].selected==true)
                            {
                                if(gpby.value=="")
                                {
                                gpby.value=" group by " + list.item(i).value;
                               }
                               else
                               {
                               gpby.value=gpby.value + "," + list.item(i).value;
                               }
                               
                            }
                       }
                   document.getElementById("<%=groupby.ClientId %>").value=gpby.value;
					
					
					
					
						document.forms[0].action = "ExecView.aspx?type=create"
						+"&lstformula="+aliasformula//document.getElementById("<%=txt1stformula.ClientId %>").value
					+"&txtname="+document.getElementById("<%=txtname.ClientId %>").value
					+"&lsttab2cols="+document.getElementById("<%=txt1sttabcolvalue.ClientId %>").value
	           +"&viewname="+document.getElementById("<%=lblname1.ClientId %>").value					
               +"&txtcol="+document.getElementById("<%=txtcols.ClientId %>").value
					+"&lstgroup="+document.getElementById("<%=txtgroup.ClientId %>").value
					+"&lsttab2="+tabname
					+"&cbolob1="+0
					+"&cboclient1="+0
					+"&cbodept1="+60
					+"&joindata="+joindata
					+"&conditionsdata="+conditionsdata
					+"&ViewType="+'NonLocal'
					+"&groupby="+ document.getElementById("<%=groupby.ClientId %>").value
					
					document.forms[0].submit();
				}
			}
			function winformula()
			{
		
			Flag=0;
				var tabname = "";
				for(i=0;i<document.getElementById("<%=lsttab2.ClientId %>").length;i++)
				{
					
					
				//made changes by vini''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''	
					if(tabname == "")
					{
					    tabname = document.getElementById("<%=lsttab2.ClientId %>").item(i).text;
						
					}
					else
					{
						tabname = tabname + "," + document.getElementById("<%=lsttab2.ClientId %>").item(i).text;
					}
				//end''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''	
					
				}
				var passdata 
				var formula = ""
				var formula1= ""
								//changes made by vini''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
				
				window.open("viewformula.aspx?"+ passdata ,"Formula" ,"height=400,width=400,top=100,left=400,scrollbars=yes,status=yes")
				
				//end''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
				//assign_value_to_listbox();
				for(i=0;i<document.getElementById("<%=lstformula.ClientId %>").length;i++)
				{
					var arrformula = document.getElementById("<%=lstformula.ClientId %>").item(i).value.split(",")
					var arrformulatext = document.getElementById("<%=lstformula.ClientId %>").item(i).text.split(",")
					for(j=0;j<=arrformula.length-1;j++)
					{
						document.getElementById("<%=lstformula.ClientId %>").item(i).value = document.getElementById("<%=lstformula.ClientId %>").item(i).value.replace(",","~~")
					}
					for(j=0;j<=arrformulatext.length-1;j++)
					{
						document.getElementById("<%=lstformula.ClientId %>").item(i).text = document.getElementById("<%=lstformula.ClientId %>").item(i).text.replace(",","~~")
					}
					if(formula=="")
					{
						formula = document.getElementById("<%=lstformula.ClientId %>").item(i).value;
					}
					else
					{
						formula = formula + "," + document.getElementById("<%=lstformula.ClientId %>").item(i).value;
					}
					if(formula1=="")
					{
						formula1 = document.getElementById("<%=lstformula.ClientId %>").item(i).text;
					}
					else
					{
						formula1 = formula1 + "," + document.getElementById("<%=lstformula.ClientId %>").item(i).text;
					}
				}
				
				var arrformula = formula.split("+")
				for(i=0;i<arrformula.length;i++)
				{
					formula = formula.replace("+","|~|")
				}
				
				
				//formula = document.Form1.txtformula1.value + "," + document.Form1.txtformula2.value + "," + document.Form1.txtformula3.value
				var typeofpage="Edit"
				passdata ="data=" + formula + "&datatext=" + formula1  + "&tabname=" + tabname + "&type=" + typeofpage
				window.open("viewformula.aspx?"+ passdata ,"Formula" ,"height=400,width=400,top=100,left=400,scrollbars=yes,status=yes")
			}
			function assign_value_to_listbox()
			{
 
					document.getElementById("<%=txt1stformula.ClientId %>").value="<%=formula%>";
			var strlst=""
			strlst=document.getElementById("<%=txt1stformula.ClientId %>").value
			
			var arrformulaname = strlst.split(",")
			var arrNewformulalen= arrformulaname.length
			if (arrformulaname=="")
			{
			}
			else
			{
			
				for(j=0;j<=arrNewformulalen-1;j++)
						{
						    document.getElementById("<%=lstformula.ClientId %>").item(j).value= arrformulaname[j];
						}
			}			
//					
			}
			function assign_value_to_listbox1()
			{

			//document.getElementById("<%=txt1stformula.ClientId %>").value="<%=formula%>";
			var strlst=""
			strlst=document.getElementById("<%=txt1stformula.ClientId %>").value
			
			var arrformulaname = strlst.split(",")
			var arrNewformulalen= arrformulaname.length
			if (arrformulaname=="")
			{
			}
			else
			{
			
				for(j=0;j<=arrNewformulalen-2;j++)
						{
						    document.getElementById("<%=lstformula.ClientId %>").item(j).value= arrformulaname[j];
						}
			}			
//					
			}

			function delete_formula()
			{
			 
			assign_value_to_listbox1();
				Flag=0;
					if(document.getElementById("<%=lstformula.ClientId %>").selectedIndex != -1)
				{
					var i
					for(i=0;i<document.getElementById("<%=lstgroup.ClientId %>").length;i++)
					{
						if(document.getElementById("<%=lstformula.ClientId %>").item(document.getElementById("<%=lstformula.ClientId %>").selectedIndex).value == document.getElementById("<%=lstgroup.ClientId %>").item(i).value)
						{
							document.getElementById("<%=lstgroup.ClientId %>").remove(i);
						}
					}
					document.getElementById("<%=lstformula.ClientId %>").remove(document.getElementById("<%=lstformula.ClientId %>").selectedIndex);
				}
			}
			
			function lstcol_onchange()
			{
				Flag=0;
			}
		//function they are related with viewFormula page..........	
			
			function chkname2()
			{ 
			
				if(document.getElementById("<%=lsttab2.ClientId %>").length==0) 
				{
					alert("Please put the table(s) in the listbox of selected tables.")
				}
				else if(document.getElementById("<%=lsttab2cols.ClientId %>").selectedIndex==-1)
				{
					alert("Please select columns")
				}
				else if((document.getElementById("<%=cbodept1.ClientId %>").selectedIndex==0) && (document.getElementById("cboclient1").selectedIndex==0 || document.getElementById("cboclient1").selectedIndex==-1) && (document.getElementById("cbolob1").selectedIndex==0 || document.getElementById("cbolob1").selectedIndex==-1))
				{
					alert("Please select department or client or lob under which you have to save the table.")
				}
				else if(Flag==0)
				{
				 
					//alert("Please enter names for the columns selected.")
					var str=""
					cnt = 0
					document.getElementById("<%=divname.ClientId %>").innerHTML = ""
					str = "<table width=100%>"
					for(i=0;i<document.getElementById("<%=lsttab2cols.ClientId %>").length;i++)
					{
						if(document.getElementById("<%=lsttab2cols.ClientId %>").item(i).selected==true)
						{
							cnt=cnt+1
							var name = document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value
							var arrname = name.split(".")
							str = str + "<tr><td width=35%>" + cnt + ". " + document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value + ":</td><td><label for=txtcol" + cnt + "></label><input type=text id=txtcol" + cnt + " name=txtcol" + cnt + " maxlength=50 value=" + arrname[1] + "></td></tr>"
						}
					}
						
					for(i=0;i<document.getElementById("<%=lstformula.ClientId %>").length;i++)
					{
						cnt = cnt+1
						str = str + "<tr><td>" + cnt + ". " + document.getElementById("<%=lstformula.ClientId %>").item(i).value + ":</td><td><label for=txtcol" + cnt + "></label><input type=text id=txtcol" + cnt + " name=txtcol" + cnt + " maxlength=50></td></tr>"
					}
					
					str = str + "</table>"
					document.getElementById("<%=divname.ClientId %>").innerHTML = str;
					Flag=1
				}
				else if(Flag==1)
				{
					var strmsg = ""
					bool=0
						for(i=1;i<=cnt;i++)
					{
						var j = i+88
					
						//if(blank(document.forms[0].elements[j].value))
						if(blank(document.getElementById("txtcol" + i ).value))
						{
					//		strmsg = strmsg + "Please enter name for column " + i + "\n"
						}
						else if(chkvalidname(document.getElementById("txtcol" + i ).value))
						//else if(chkvalidname(document.forms[0].elements[j].value))
						{
							strmsg = strmsg + "Special characters or spaces are not allowed in the name for column " + i + "\n"
						}
						else 
						{
							for(k=1;k<=cnt;k++)		
							{
								if(i!=k)
								{
									//if(document.forms[0].elements[j].value==document.forms[0].elements[k+88].value)
									if(document.getElementById("txtcol" + i ).value==document.forms[0].elements[k].value)
									{
										//alert(document.Form1.elements[j].name + "=" + document.Form1.elements[k+92].name)
										bool=1
									}
								}
							}
						}
					}
					if(!blank(strmsg))
					{
						
						//alert(strmsg)
					}
					else if(bool==1)
					{	
						alert("Two columns can not have the same name.")
					}
					else if(blank(document.getElementById("<%=txtname.ClientId %>").value))
					{
						alert("Please enter view name")
					}
					else
					{ 
					
						AjaxClass1.chkViewName(document.getElementById("<%=txtname.ClientId %>").value,chkview2)
						
					}
				}
			}
			function chkview2(res)
			{
				
				if(res.value=="Y")
				{
					alert("View Name already exists. Please enter another name.")
				}
				else
				{
					for(i=0;i<=document.getElementById("<%=lsttab2.ClientId %>").length-1;i++)
					{
							document.getElementById("<%=lsttab2.ClientId %>").item(i).selected=true
					}
					for(i=0;i<=document.getElementById("<%=lstformula.ClientId %>").length-1;i++)
					{
						var arrformula = document.getElementById("<%=lstformula.ClientId %>").item(i).value.split(",")
						for(j=0;j<=arrformula.length-1;j++)
						{
							document.getElementById("<%=lstformula.ClientId %>").item(i).value = document.getElementById("<%=lstformula.ClientId %>").item(i).value.replace(",","~~")
						}
						document.getElementById("<%=lstformula.ClientId %>").item(i).selected=true
					}
						//__doPostBack('cmdsav','');
						document.forms[0].target = "_new";
						//document.Form1.target = "frmexec";
						//gathering variables value
						// Fuction for assinging isttablecolumns value to hidden field 
					
					document.getElementById("<%=txt1sttabcolvalue.ClientId %>").value = "";
					for(i=0;i<document.getElementById("<%=lsttab2cols.ClientId %>").length;i++)
					{
						if(document.getElementById("<%=lsttab2cols.ClientId %>").item(i).selected==true)
						{
					
					document.getElementById("<%=txt1sttabcolvalue.ClientId %>").value+= document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value + ",";
					 }
					 }
			
					 
					
					
					
					 // Fuction for assiging formula value to hidden field
					 document.getElementById("<%=txt1stformula.ClientId %>").value="";
					for(i=0;i<=document.getElementById("<%=lstformula.ClientId %>").length-1;i++)
					{
					    if(document.getElementById("<%=lstformula.ClientId %>").item(i).selected==true)
					    {
					       document.getElementById("<%=txt1stformula.ClientId %>").value+= document.getElementById("<%=lstformula.ClientId %>").item(i).value + ","; 
					     }
					  }
					
					 
					  
					  
					  // Finding the name of selected table
					 
					var tabname = "";
				for(i=0;i<document.getElementById("<%=lsttab2.ClientId %>").length;i++)
				{
					if(tabname!="")
					{
						tabname = tabname + "," + document.getElementById("<%=lsttab2.ClientId %>").item(i).text;
					}
					else
					{
						tabname = document.getElementById("<%=lsttab2.ClientId %>").item(i).text;
					}
				}
				/////aaaaaaaaaaaaaaaaaaaaaaaaaaaa
				 
						var formula = document.getElementById("<%=txt1stformula.ClientId %>").value
					var arrdata=formula.split("+");
					var aliasformula="";
					for(var i=0;i<arrdata.length;i++)
					{
					    if(aliasformula=="")
					       {
					        aliasformula=arrdata[i]
					       }
					     else
					       {
					        aliasformula=aliasformula+" $ "+arrdata[i];
					        }
					}
					// collecting join condition information in single variable 
					
					 var joindata='null';
					if(document.getElementById("<%=cbocol11.ClientId %>").value=="")
					{
                    joindata="--Select--";
					}
					else
					{
					joindata=document.getElementById("<%=cbocol11.ClientId %>").value;
					}
					if(document.getElementById("<%=cbocol12.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol12.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol13.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol13.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol14.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol14.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol15.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol15.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol16.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol16.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol17.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol17.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol18.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol18.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol19.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol19.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol20.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol20.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol21.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol21.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol22.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol22.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol23.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol23.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol24.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol24.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol25.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol25.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol26.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol26.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol27.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol27.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol28.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol28.ClientId %>").value
					
					}
					if(document.getElementById("<%=cbocol29.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol29.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol30.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol30.ClientId %>").value;
					
					}
				
					joindata =joindata+"$"+document.getElementById("<%=cbojoin1.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin2.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin3.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin4.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin5.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin6.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin7.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin8.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin9.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin10.ClientId %>").value;

              
                var conditionsdata='null';
					if(document.getElementById("<%=cbocolA1.ClientId %>").value=="")
					{
                    conditionsdata="--Select--";
					}
					else
					{
					conditionsdata=document.getElementById("<%=cbocolA1.ClientId %>").value;
					}
					if(document.getElementById("<%=cbocolA2.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA2.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA3.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA3.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA4.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA4.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA5.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA5.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA6.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA6.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA7.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA7.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA8.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA8.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA9.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA9.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA10.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA10.ClientId %>").value;
					
					}
					conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc11.ClientId %>").value
				conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc12.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc13.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc14.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc15.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc16.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc17.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc18.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc19.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc20.ClientId %>").value
		//******************************
	 
	
	if(document.getElementById("<%=txtval11.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval11.ClientId %>").value
	}
	if(document.getElementById("<%=txtval12.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval12.ClientId %>").value
	}
	if(document.getElementById("<%=txtval13.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval13.ClientId %>").value
	}
	if(document.getElementById("<%=txtval14.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval14.ClientId %>").value
	}
	if(document.getElementById("<%=txtval15.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval15.ClientId %>").value
	}
	if(document.getElementById("<%=txtval16.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval16.ClientId %>").value
	}
	if(document.getElementById("<%=txtval17.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval17.ClientId %>").value
	}
	if(document.getElementById("<%=txtval18.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval18.ClientId %>").value
	}
	if(document.getElementById("<%=txtval19.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval19.ClientId %>").value
	}
	if(document.getElementById("<%=txtval20.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval20.ClientId %>").value
	}
				//******************************************************
			// +"&lstformula="+document.getElementById("<%=txt1stformula.ClientId %>").value
			// +"&lstformula="+aliasformula
			
			var ViewType="NonLocal"
//			if(document.getElementById("<%=chkLocalView.ClientId %>").checked==true)
//			{
//			ViewType="Local";
//			}
//			else
//			{
//			ViewType="NonLocal";
//			}
						//***********
					
					    var list=document.getElementById("<%=lstgroup.ClientId %>");
	                var len=list.length;
			
					
					
					var gpby =document.getElementById("<%=groupby.ClientId %>");
					gpby.value="";
					
					for (var i=0;i< list.length;i++)
                        {
                            if (list.options[i].selected==true)
                            {
                                if(gpby.value=="")
                                {
                                gpby.value=" group by " + list.item(i).value;
                               }
                               else
                               {
                               gpby.value=gpby.value + "," + list.item(i).value;
                               }
                               
                            }
                       }
                   document.getElementById("<%=groupby.ClientId %>").value=gpby.value;
					
					
					
					
						document.forms[0].action = "ExecView.aspx?type=create"
						+"&lstformula="+aliasformula//document.getElementById("<%=txt1stformula.ClientId %>").value
					+"&txtname="+document.getElementById("<%=txtname.ClientId %>").value
					+"&lsttab2cols="+document.getElementById("<%=txt1sttabcolvalue.ClientId %>").value
	           +"&viewname="+document.getElementById("<%=lblname1.ClientId %>").value					
               +"&txtcol="+document.getElementById("<%=txtcols.ClientId %>").value
					+"&lstgroup="+document.getElementById("<%=txtgroup.ClientId %>").value
					+"&lsttab2="+tabname
					+"&cbolob1="+document.getElementById("<%=cbolob1.ClientId %>").value
					+"&cboclient1="+document.getElementById("<%=cboclient1.ClientId %>").value
					+"&cbodept1="+document.getElementById("<%=cbodept1.ClientId %>").value
					+"&joindata="+joindata
					+"&conditionsdata="+conditionsdata
					+"&ViewType="+ViewType
					+"&groupby="+ document.getElementById("<%=groupby.ClientId %>").value
					
					document.forms[0].submit();
				}
			}
            function chkvalidation2()
			{
				
				if(document.getElementById("<%=lsttab2.ClientId %>").length==0) 
				{
					alert("Please add the table to Selected Tables combobox")
				}
				/*else if(document.getElementById("lsttab2cols").selectedIndex==-1)
				{
					alert("Please select columns")
				}*/
				/*else if(document.getElementById("cbocol11").selectedIndex==0 && document.getElementById("cbocol21").selectedIndex==0 && document.getElementById("cbojoin1").selectedIndex==0)
				{
					alert("Please select the columns and join in where clause to join the tables.")
				}*/
				else if((document.getElementById("<%=cbodept1.ClientId %>").selectedIndex==0) && (document.getElementById("<%=cboclient1.ClientId %>").selectedIndex==0 || document.getElementById("<%=cboclient1.ClientId %>").selectedIndex==-1) && (document.getElementById("<%=cbolob1.ClientId %>").selectedIndex==0 || document.getElementById("<%=cbolob1.ClientId %>").selectedIndex==-1))
				{
					alert("Please select the span to create the view.")
				}
				
				//**********************************changes made by vini***********************************************************
				else if(Flag==0)
				{
					alert("Please enter names for the columns selected.")
					var str=""
					cnt = 0
					document.getElementById("<%=divname.ClientId %>").innerHTML = ""
					str = "<table width=100%>"
					
					strNewColumn = "<%=colname%>"
					var strHeadingValue1 = "<%=heading %>"
					if (strNewColumn =="")
					{
						
						arrNewColumnlen = "1"
					}
					else
					{
						
						arrNewColumn = strNewColumn.split(",")
						arrNewColumnlen = arrNewColumn.length
					}
					
					//var strNewHead = "<%=heading%>"
					//var strNewFormula = "<%=formula%>"
					// changes by suvi
				
					var strNewHead=""
					 
					document.getElementById("<%=txt1stformula.ClientId %>").value="";
					for(i=0;i<=document.getElementById("<%=lstformula.ClientId %>").length-1;i++)
					{
					      document.getElementById("<%=txt1stformula.ClientId %>").value+= document.getElementById("<%=lstformula.ClientId %>").item(i).value + ","; 
					      strNewHead+= document.getElementById("<%=lstformula.ClientId %>").item(i).text + "," 
				    }
					  var strNewFormula= document.getElementById("<%=txt1stformula.ClientId %>").value
					  //changes by me
					var arrNewHead = strNewHead.split(",")
					var strHeadingValue = strHeadingValue1.split(",")
					var arrNewFormula = strNewFormula.split(",")
					var chkVal;
					var lstvall="";
					for(var g=0;g<document.getElementById("<%=lsttab2cols.ClientId %>").length;g++)
					{
					   if(document.getElementById("<%=lsttab2cols.ClientId %>").item(g).selected==true)
						{
				            lstvall+=document.getElementById("<%=lsttab2cols.ClientId %>").item(g).value+",";
						}
					}
					var lstvalf=lstvall.split(",");
					var ls=0;
//					for(i=0;i<document.getElementById("<%=lsttab2cols.ClientId %>").length;i++)
//                  	{
//    					
//                        if(document.getElementById("<%=lsttab2cols.ClientId %>").item(i).selected==true)
//					    {
//					       var splitstring=lstvalf[ls].split(".");
//					       cnt=cnt+1
//					       str = str + "<tr><td width=35%>" + cnt + ". " + document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value + ":</td><td><label for=txtcol" + cnt + "></label><input type=text id=txtcol" + cnt + " name=txtcol" + cnt + " maxlength=50 value=" + splitstring[1] + "></td></tr>"
//					       ls=ls+1;
//					    }
//    					
//					}
					
					for(i=0;i<document.getElementById("<%=lsttab2cols.ClientId %>").length;i++)
					
					{
					  
						if(document.getElementById("<%=lsttab2cols.ClientId %>").item(i).selected==true)
						{
						   
							cnt=cnt+1
							var j=0;
							chkVal="F";
							var name = document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value
							
							var arrname = name.split(".")
							
							for(j=0;j<arrNewColumnlen;j++)
							{
									//alert("in loop")
									if(arrNewColumnlen[j]==document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value)
									{
									  str = str + "<tr><td width=35%>" + cnt + ". " + document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value + ":</td><td><label for=txtcol" + cnt + "></label><input type=text id=txtcol" + cnt + " name=txtcol" + cnt + " maxlength=50 value=" + arrNewHead[j]+ "></td></tr>"
									  chkVal="P";
									
							}
						}
						if(chkVal=="F")
						{
							str = str + "<tr><td width=35%>" + cnt + ". " + document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value + ":</td><td><label for=txtcol" + cnt + "></label><input type=text id=txtcol" + cnt + " name=txtcol" + cnt + " maxlength=50 value=" + strHeadingValue[ls] + "></td></tr>"
							 ls=ls+1;	
								//changed by atul above to get previous input value str = str + "<tr><td width=35%>" + cnt + ". " + document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value + ":</td><td><input type=text id=txtcol" + cnt + " name=txtcol" + cnt + " maxlength=50 value=" + arrname[1] + "></td></tr>"
						}
					}
					}
					
					
					
					for(i=0;i<document.getElementById("<%=lstformula.ClientId %>").length;i++)
					{
						var j=0;
						var hedCount=0;
						chkVal="F";
						 for(j=0;j<arrNewFormula.length;j++)
					     {
						   if(arrNewFormula[j]==document.getElementById("<%=lstformula.ClientId %>").item(i).value)
						   {
						    
							 cnt = cnt+1;
							 n = i+1;
							 if((arrNewColumnlen=="1")&&(strNewColumn ==""))
							 {
								
								
								str = str + "<tr><td>" + cnt + ". " + document.getElementById("<%=lstformula.ClientId %>").item(i).value + ":</td><td><label for=txtcol" + cnt + "></label><input type=text id=txtcol" + cnt + " name=txtcol" + cnt + " value=" + arrNewHead[j] + " maxlength=50></td></tr>"
								chkVal="P";
							 }
							 else
							 {
							    str = str + "<tr><td>" + cnt + ". " + document.getElementById("<%=lstformula.ClientId %>").item(i).value + ":</td><td><label for=txtcol" + cnt + "></label><input type=text id=txtcol" + cnt + " name=txtcol" + cnt + " value=" + arrNewHead[j] + " maxlength=50></td></tr>"
								chkVal="P";
							 }
							}
						}
						if(chkVal=="F")
						{
						 
							cnt = cnt+1
							n = i+1;
							str = str + "<tr><td>" + cnt + ". " + document.getElementById("<%=lstformula.ClientId %>").item(i).value + ":</td><td><label for=txtcol" + cnt + "></label><input type=text id=txtcol" + cnt + " name=txtcol" + cnt + " maxlength=50></td></tr>"
							
						}
					}
					str = str + "</table>"
					document.getElementById("<%=divname.ClientId %>").innerHTML = str;
					Flag=1
				}
				//****************************************end Changes******************************************************
				
				else if(Flag=1)
				{
					var strmsg = ""
					bool=0
					for(i=1;i<=cnt;i++)
					{
						var j = i+88
					
						var suvi=document.forms[0].elements[j].value;
						//if(blank(document.forms[0].elements[j].value))
						if(blank(document.getElementById("txtcol" + i ).value))
						{
							strmsg = strmsg + "Please enter name for column " + i + "\n"
						}
						//else if(chkvalidname(document.forms[0].elements[j].value))
						else if(chkvalidname(document.getElementById("txtcol" + i ).value))
						{
							strmsg = strmsg + "Special characters or spaces are not allowed in the name for column " + i + "\n"
						}
						else 
						{
							for(k=1;k<=cnt;k++)		
							{
								if(i!=k)
								{
									//if(document.forms[0].elements[j].value==document.forms[0].elements[k+88].value)
									if(document.getElementById("txtcol" + i ).value==document.forms[0].elements[k].value)
									{
										//alert(document.Form1.elements[j].name + "=" + document.Form1.elements[k+92].name)
										bool=1
									}
								}
							}
						}
					}
					if(!blank(strmsg))
			
					{
						
						alert(strmsg)
					}
					else if(bool==1)
					{	
						alert("Two columns can not have the same name.")
					}
					else
					{
						for(i=0;i<=document.getElementById("<%=lsttab2.ClientId %>").length-1;i++)
						{
							document.getElementById("<%=lsttab2.ClientId %>").item(i).selected=true
						}
						for(i=0;i<=document.getElementById("<%=lstformula.ClientId %>").length-1;i++)
						{
							var arrformula = document.getElementById("<%=lstformula.ClientId %>").item(i).value.split(",")
							for(j=0;j<=arrformula.length-1;j++)
							{
								document.getElementById("<%=lstformula.ClientId %>").item(i).value = document.getElementById("<%=lstformula.ClientId %>").item(i).value.replace(",","~~")
							}
							document.getElementById("<%=lstformula.ClientId %>").item(i).selected=true
						}
						//__doPostBack('cmdcreate','');
						
						//***************
					
							document.getElementById("<%=txt1sttabcolvalue.ClientId %>").value = "";
					for(i=0;i<document.getElementById("<%=lsttab2cols.ClientId %>").length;i++)
					{
						if(document.getElementById("<%=lsttab2cols.ClientId %>").item(i).selected==true)
						{
					
					document.getElementById("<%=txt1sttabcolvalue.ClientId %>").value+= document.getElementById("<%=lsttab2cols.ClientId %>").item(i).value + ",";
					 }
					 }
			
					 
					
					
					
					 
				
				
			    document.getElementById("<%=txt1stformula.ClientId %>").value=="";
			    //document.getElementById("<%=txt1stformula.ClientId %>").value="<%=formula%>";
			    var strlst=""
			    assign_value_to_listbox1();
			    for(i=0;i<=document.getElementById("<%=lstformula.ClientId %>").length-1;i++)
					{
					
					       strlst= strlst + document.getElementById("<%=lstformula.ClientId %>").item(i).value + ","
					}
		    
				var formname1 =  document.getElementById("<%=txt1stformula.ClientId %>").value
				if (strlst==formname1)
				{
							var arrformulaname = formname1.split(",")
							var arrNewformulalen= arrformulaname.length
							for(j=0;j<arrNewformulalen-1;j++)
							{
							document.getElementById("<%=lstformula.ClientId %>").item(j).value= arrformulaname[j];
							}
							
				}
				else 
				{
				       var arrformulaname = strlst.split(",")
					   var arrNewformulalen= arrformulaname.length
					    for(j=0;j<arrNewformulalen-1;j++)
							{
							    document.getElementById("<%=lstformula.ClientId %>").item(j).value= arrformulaname[j];
				                document.getElementById("<%=txt1stformula.ClientId %>").value="";
					            for(i=0;i<=document.getElementById("<%=lstformula.ClientId %>").length-1;i++)
					                {
					                    document.getElementById("<%=txt1stformula.ClientId %>").value+= document.getElementById("<%=lstformula.ClientId %>").item(i).value + ","; 
					                }
				            }
				  }
					
					  // Finding the name of selected table
					 
					var tabname = "";
				for(i=0;i<document.getElementById("<%=lsttab2.ClientId %>").length;i++)
				{
					if(tabname!="")
					{
						tabname = tabname + "," + document.getElementById("<%=lsttab2.ClientId %>").item(i).text;
					}
					else
					{
						tabname = document.getElementById("<%=lsttab2.ClientId %>").item(i).text;
					}
				}
						var formula = document.getElementById("<%=txt1stformula.ClientId %>").value
					var arrdata=formula.split("+");
					var aliasformula="";
					for(var i=0;i<arrdata.length;i++)
					{
					    if(aliasformula=="")
					       {
					        aliasformula=arrdata[i]
					       }
					     else
					       {
					        aliasformula=aliasformula+" $ "+arrdata[i];
					        }
					}
					// collecting join condition information in single variable 
					
					
					 var joindata='null';
					if(document.getElementById("<%=cbocol11.ClientId %>").value=="")
					{
                    joindata="--Select--";
					}
					else
					{
					joindata=document.getElementById("<%=cbocol11.ClientId %>").value;
					}
					if(document.getElementById("<%=cbocol12.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol12.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol13.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol13.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol14.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol14.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol15.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol15.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol16.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol16.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol17.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol17.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol18.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol18.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol19.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol19.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol20.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol20.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol21.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol21.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol22.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol22.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol23.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol23.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol24.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol24.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol25.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol25.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol26.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol26.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol27.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol27.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol28.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol28.ClientId %>").value
					
					}
					if(document.getElementById("<%=cbocol29.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol29.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocol30.ClientId %>").value=="")
					{
                    joindata=joindata+"$"+"--Select--";
					}
					else
					{
					joindata =joindata+"$"+document.getElementById("<%=cbocol30.ClientId %>").value;
					
					}
				
					joindata =joindata+"$"+document.getElementById("<%=cbojoin1.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin2.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin3.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin4.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin5.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin6.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin7.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin8.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin9.ClientId %>").value;
					joindata =joindata+"$"+document.getElementById("<%=cbojoin10.ClientId %>").value;

              
                var conditionsdata='null';
					if(document.getElementById("<%=cbocolA1.ClientId %>").value=="")
					{
                    conditionsdata="--Select--";
					}
					else
					{
					conditionsdata=document.getElementById("<%=cbocolA1.ClientId %>").value;
					}
					if(document.getElementById("<%=cbocolA2.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA2.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA3.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA3.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA4.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA4.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA5.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA5.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA6.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA6.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA7.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA7.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA8.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA8.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA9.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA9.ClientId %>").value;
					
					}
					if(document.getElementById("<%=cbocolA10.ClientId %>").value=="")
					{
                    conditionsdata=conditionsdata+"$"+"--Select--";
					}
					else
					{
					conditionsdata =conditionsdata+"$"+document.getElementById("<%=cbocolA10.ClientId %>").value;
					
					}
					conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc11.ClientId %>").value
				conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc12.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc13.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc14.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc15.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc16.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc17.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc18.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc19.ClientId %>").value
		conditionsdata=conditionsdata+"$"+document.getElementById("<%=cbofunc20.ClientId %>").value
		//******************************
	 
	
	if(document.getElementById("<%=txtval11.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval11.ClientId %>").value
	}
	if(document.getElementById("<%=txtval12.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval12.ClientId %>").value
	}
	if(document.getElementById("<%=txtval13.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval13.ClientId %>").value
	}
	if(document.getElementById("<%=txtval14.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval14.ClientId %>").value
	}
	if(document.getElementById("<%=txtval15.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval15.ClientId %>").value
	}
	if(document.getElementById("<%=txtval16.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval16.ClientId %>").value
	}
	if(document.getElementById("<%=txtval17.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval17.ClientId %>").value
	}
	if(document.getElementById("<%=txtval18.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval18.ClientId %>").value
	}
	if(document.getElementById("<%=txtval19.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval19.ClientId %>").value
	}
	if(document.getElementById("<%=txtval20.ClientId %>").value=="")
	{
	conditionsdata=conditionsdata+"$"+"--Select--"
	}
	else
	{
	conditionsdata=conditionsdata+"$"+document.getElementById("<%=txtval20.ClientId %>").value
	}
				//******************************************************
			// +"&lstformula="+document.getElementById("<%=txt1stformula.ClientId %>").value
			// +"&lstformula="+aliasformula
			 
			var ViewType="NonLocal"
//			if(document.getElementById("<%=chkLocalView.ClientId %>").checked==true)
//			{
//			ViewType="Local";
//			}
//			else
//			{
//			ViewType="NonLocal";
//			}
			var viewname;
			viewname=document.getElementById("<%=lblname1.ClientId %>").value;
						//***********
						
						//change for groupby
						 var list=document.getElementById("<%=lstgroup.ClientId %>");
	                var len=list.length;
			
					
					
					var gpby =document.getElementById("<%=txtgroup.ClientId %>");
					gpby.value="";
					
					for (var i=0;i< list.length;i++)
                        {
                            if (list.options[i].selected==true)
                            {
                                if(gpby.value=="")
                                {
                                gpby.value=" group by " + list.item(i).value;
                               }
                               else
                               {
                               gpby.value=gpby.value + "," + list.item(i).value;
                               }
                               
                            }
                       }
                    document.getElementById("<%=txtgroup.ClientId %>").value=gpby.value;
					var a = document.getElementById("<%=lstgroup.ClientId %>").value
					//change for groupby	
					var suvidha=document.getElementById("<%=txtgroup.ClientId %>").value;
					document.forms[0].target = "_new";
						//document.Form1.target = "frmexec";
						 
					document.forms[0].action = "ExecView.aspx?type=update&recid=" + <%=Request("recid")%>
					+"&lstformula="+aliasformula//document.getElementById("<%=txt1stformula.ClientId %>").value
				
					+"&lsttab2cols="+document.getElementById("<%=txt1sttabcolvalue.ClientId %>").value
					+"&txtcol="+document.getElementById("<%=txtcols.ClientId %>").value
					+"&viewname="+document.getElementById("<%=lblname1.ClientId %>").value
                                        +"&lstgroup="+document.getElementById("<%=lstgroup.ClientId %>").value
					+"&lsttab2="+tabname
					+"&cbolob1="+document.getElementById("<%=cbolob1.ClientId %>").value
					+"&cboclient1="+document.getElementById("<%=cboclient1.ClientId %>").value
					+"&cbodept1="+document.getElementById("<%=cbodept1.ClientId %>").value
					+"&joindata="+joindata
					+"&conditionsdata="+conditionsdata
					+"&ViewType="+ViewType
					+"&groupby="+ document.getElementById("<%=txtgroup.ClientId %>").value
					
					
						document.forms[0].submit();
						
					}
				}
			}
			
			
			
			
function cmdsaveas_onclick() {

}

</script>
		<body>
			<table width="90%" align="center" class="table">
				<caption>Edit View</caption>
				
				<tr>
					<td style="height: 14px" scope="col" ><strong>View Name</strong></td>
					<td colspan="3" style="height: 14px" scope ="colgroup" ><asp:label id="lblname" Runat="server"></asp:label></td>
				</tr>
				<tr>
                <td>
                <table id="spandisplaymul" class="table" runat="server" visible="false" >
                <tr>
                <td width="20%" scope="col"><strong><asp:Label ID="lbl1" Text="Level 1" runat="server" ></asp:Label> </strong></td>
					<td colspan="3" scope ="colgroup"><asp:dropdownlist id="cbodept" tabindex="1" Runat="server" CssClass ="dropdownlist"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td width="20%" scope ="col" ><strong><asp:Label ID="lbl2" Text="Level 2" runat="server" ></asp:Label> </strong></td>
					<td colspan="3" scope ="colgroup"><asp:dropdownlist id="cboclient" tabindex="2" Runat="server" CssClass ="dropdownlist"></asp:dropdownlist></td>
				</tr> 
				<tr>
					<td scope ="col" ><strong><asp:Label ID="lbl3" Text="Level 3" runat="server" ></asp:Label> </strong></td>
					<td colspan="3" scope ="colgroup"><asp:dropdownlist id="cbolob" tabindex="3" Runat="server" CssClass ="dropdownlist"></asp:dropdownlist>
				    </td>
                </tr>
                </table>
                </td>
                <td><input class="button" id="Gettable" title="GetTable" style="WIDTH: 100px; HEIGHT: 21px" runat="server" visible="false"  onclick="javascript:gettab();"
										tabindex="6" type="button" value="GetTable" name="GetTable"/></td>
				</tr>	
				<tr>
					<td colspan="4" scope ="colgroup">
						<table width="100%" summary ="table">
							<tr>
								<td style="WIDTH: 226px" scope ="col" ><label for="ctl00_MainPlaceHolder_lsttab1" ><strong>Select Tables</strong></label><br/>
									<select id="lsttab1" style="WIDTH: 224px; HEIGHT: 136px" tabindex="5" multiple="true" name="lsttab1"
										runat="server" type="select-multiple">
									</select>
								</td>
								<td style="WIDTH: 53px" align="center" scope ="col" ><input class="button" id="cmdadd" title="Add" style="WIDTH: 26px; HEIGHT: 21px" onclick="javascript:addtable();"
										tabindex="6" type="button" value=">>" name="cmdadd"/>
									<br/>
									<input class="button" id="cmddel" title="Remove" style="WIDTH: 26px; HEIGHT: 21px" onclick="javascript:removetable();"
										tabindex="7" type="button" value="<<" name="cmddel"/>
								</td>
								<td scope ="col" ><label for="ctl00_MainPlaceHolder_lsttab2" ><strong>Selected Tables</strong></label><br/>
									<select id="lsttab2"  style="WIDTH: 224px; HEIGHT: 136px" multiple="true" tabindex="8"  name="lsttab2"
										runat="server">
									</select>
								</td>
							</tr>
							<tr>
								<td colspan="3" scope ="colgroup"><label for="ctl00_MainPlaceHolder_lsttab2cols" ><strong>Choose Columns</strong></label><br/>
									<select id="lsttab2cols"  style="HEIGHT: 136px" tabindex="9"  multiple="true"   name="lsttab2cols"
										runat="server"  onchange="javascript:lstcol_onchange();">
									</select>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="4" scope ="colgroup">
						<div id="divcon" runat="server">
							<table width="100%" summary ="table">
								<tr>
									<td valign="top" colspan="3" scope ="colgroup"><br />
										<input class="button" id="cmdformula1" onclick="javascript:winformula();" type="button"
											value="Formula" name="cmdformula1"/>&nbsp;<a id="lnkformula1" href="javascript:delete_formula()" name="lnkformula1"><img title="Delete formula" src="../images/delete_16.gif" border="0" alt="not supported"/></a><br/>
										<br/>
										<label for="ctl00_MainPlaceHolder_lstformula" ></label>
										<select id="lstformula" style="HEIGHT: 136px" multiple="true" name="lstformula" runat="server">
										</select>
									</td>
								</tr>
								<tr>

									
									<td colspan="3" scope ="colgroup"><strong>Group By</strong><br />
									<label for="ctl00_MainPlaceHolder_lstgroup" ></label>
										<select id="lstgroup" style="HEIGHT: 136px"  class="listbox" tabindex="9" multiple="true"  name="lstgroup" runat="server"
											>
										</select>
									</td>
								</tr>
								<tr>
									<td colspan="3" scope ="colgroup">
										<div id="divdesc">
											<table width="100%" summary ="table">
												<tr>
													<td colspan="2" scope ="colgroup">&nbsp;<font color="navy"><strong>Description of symbols to join the tables:</strong></font></td>
												</tr>
												<tr>
													<td width="8%" scope ="colgroup">&nbsp;&nbsp; <font color="red">=</font></td>
													<td  scope ="col" ><font color="navy">: is used to include only rows where the joined fields from both 
															tables are equal.</font>
													</td>
												</tr>
												<tr>
													<td scope ="col"  >&nbsp;&nbsp; <font color="red">*=</font></td>
													<td><font color="navy">: is used to include all records from 'First' table and only 
															those records from 'Second' table where the joined fields from both tables are 
															equal.</font>
													</td>
												</tr>
												<tr>
													<td scope ="col" >&nbsp;&nbsp; <font color="red">= *</font></td>
													<td scope ="col" ><font color="navy">: is used to include all records from 'Second' table and only 
															those records from 'First' table where the joined fields from both tables are 
															equal.</font>
													</td>
												</tr>
											</table>
										</div>
									</td>
								</tr>
								<tr>
									<td colspan="3" scope ="colgroup">
									<label for="ctl00_MainPlaceHolder_txthead1"></label>
									<input id="txthead1" style="VISIBILITY: hidden; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 18px; BORDER-BOTTOM-STYLE: none"
											readonly="readOnly"  type="text" size="33" value="Joins:" name="txthead1" runat="server"/></td>
								</tr>
								<tr>
									<td colspan="3" scope ="colgroup">
										<table summary ="table">
												<tr>
													<td scope ="col"  ><strong>where</strong>
													</td>
													<td style="width: 119px" scope ="col" >
													<label for="ctl00_MainPlaceHolder_cbocol11" ></label>													
													<asp:dropdownlist id="cbocol11" tabindex="10" Runat="server"></asp:dropdownlist>
													</td>
													<td style="width: 148px" scope ="col"  >
													<label for="ctl00_MainPlaceHolder_cbojoin1" ></label>
														<asp:DropDownList ID="cbojoin1" Runat="server">
															<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
															<asp:ListItem Value="=">=</asp:ListItem>
															<asp:ListItem Value="*=">*=</asp:ListItem>
															<asp:ListItem Value="=*">=*</asp:ListItem>
															<asp:ListItem Value="<%=str1 %>">>=</asp:ListItem>
															<asp:ListItem Value="<="><=</asp:ListItem>
															<asp:ListItem Value="<>"><></asp:ListItem>
                                                            
														</asp:DropDownList>
													</td>
													<td style="width: 148px" scope="col" >
													<label for="ctl00_MainPlaceHolder_cbocol21" ></label>
													<asp:dropdownlist id="cbocol21" tabindex="11" Runat="server"></asp:dropdownlist></td>
												</tr>
												<tr>
													<td scope ="col" >and
                                                    </td>
                                                    <td style="width: 119px" scope ="col" >
                                                    <label for="ctl00_MainPlaceHolder_cbocol12" ></label>
													<asp:dropdownlist id="cbocol12" tabindex="12" Runat="server"></asp:dropdownlist>
													</td>
													<td style="width: 148px" scope ="col"  >
													<label for="ctl00_MainPlaceHolder_cbojoin2" ></label>
														<asp:DropDownList ID="cbojoin2" Runat="server">
															<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
															<asp:ListItem Value="=">=</asp:ListItem>
															<asp:ListItem Value="*=">*=</asp:ListItem>
															<asp:ListItem Value="=*">=*</asp:ListItem>
															<asp:ListItem Value="<%=str1 %>">>=</asp:ListItem>
															<asp:ListItem Value="<="><=</asp:ListItem>
															<asp:ListItem Value="<>"><></asp:ListItem>
														</asp:DropDownList>
													</td>
													<td style="width: 148px" scope ="col" >
													<label for="ctl00_MainPlaceHolder_cbocol22" ></label>
													<asp:dropdownlist id="cbocol22" tabindex="13" Runat="server"></asp:dropdownlist></td>
												</tr>
												<tr>
													<td scope ="col" >and
                                                    </td>
                                                    <td style="width: 119px" scope ="col" >
                                                    <label for="ctl00_MainPlaceHolder_cbocol13" ></label>
													<asp:dropdownlist id="cbocol13" tabindex="14" Runat="server"></asp:dropdownlist>
													</td>
													<td style="width: 148px"  scope ="col" >
													 <label for="ctl00_MainPlaceHolder_cbojoin3" ></label>
														<asp:DropDownList ID="cbojoin3" Runat="server">
															<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
															<asp:ListItem Value="=">=</asp:ListItem>
															<asp:ListItem Value="*=">*=</asp:ListItem>
															<asp:ListItem Value="=*">=*</asp:ListItem>
															<asp:ListItem Value="<%str1 %>">>=</asp:ListItem>
															<asp:ListItem Value="<="><=</asp:ListItem>
															<asp:ListItem Value="<>"><></asp:ListItem>
														</asp:DropDownList>
													</td>
													<td style="width: 148px" scope ="col" >
													<label for="ctl00_MainPlaceHolder_cbocol23" ></label>
													<asp:dropdownlist id="cbocol23" tabindex="15" Runat="server"></asp:dropdownlist></td>
												</tr>
												<tr>
													<td scope ="col" >and
                                                    </td>
                                                    <td style="width: 119px" scope ="col" >
                                                    <label for="ctl00_MainPlaceHolder_cbocol14" ></label>
													<asp:dropdownlist id="cbocol14" tabindex="16" Runat="server"></asp:dropdownlist>
													</td>
													<td style="WIDTH: 148px" scope ="col" >
													<label for="ctl00_MainPlaceHolder_cbojoin4" ></label>
														<asp:DropDownList ID="cbojoin4" Runat="server">
															<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
															<asp:ListItem Value="=">=</asp:ListItem>
															<asp:ListItem Value="*=">*=</asp:ListItem>
															<asp:ListItem Value="=*">=*</asp:ListItem>
															<asp:ListItem Value="<%=str1 %>">>=</asp:ListItem>
															<asp:ListItem Value="<="><=</asp:ListItem>
															<asp:ListItem Value="<>"><></asp:ListItem>
														</asp:DropDownList>
													</td>
													<td style="width: 148px" scope ="col" >
													<label for="ctl00_MainPlaceHolder_cbocol24" ></label>
													<asp:dropdownlist id="cbocol24" tabindex="17" Runat="server"></asp:dropdownlist></td>
												</tr>
												<tr>
													<td scope ="col" >and
                                                    </td>
                                                    <td style="width: 119px" scope ="col" >
                                                    <label for="ctl00_MainPlaceHolder_cbocol15" ></label>
													<asp:dropdownlist id="cbocol15" tabindex="18" Runat="server"></asp:dropdownlist>
													</td>
													<td style="width: 148px" scope ="col"  >
													<label for="ctl00_MainPlaceHolder_cbojoin5" ></label>
														<asp:DropDownList ID="cbojoin5" Runat="server">
															<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
															<asp:ListItem Value="=">=</asp:ListItem>
															<asp:ListItem Value="*=">*=</asp:ListItem>
															<asp:ListItem Value="=*">=*</asp:ListItem>
															<asp:ListItem Value="<%=str1 %>">>=</asp:ListItem>
															<asp:ListItem Value="<="><=</asp:ListItem>
															<asp:ListItem Value="<>"><></asp:ListItem>
														</asp:DropDownList>
													</td>
													<td style="width: 148px" scope ="col" >
													<label for="ctl00_MainPlaceHolder_cbocol25" ></label>
													<asp:dropdownlist id="cbocol25" tabindex="19" Runat="server"></asp:dropdownlist></td>
												</tr>
												<tr>
													<td scope ="col" >and
                                                    </td>
                                                    <td style="width: 119px" scope ="col" >
                                                    <label for="ctl00_MainPlaceHolder_cbocol16" ></label>
													<asp:dropdownlist id="cbocol16" tabindex="20" Runat="server"></asp:dropdownlist>
													</td>
													<td style="width: 148px" scope ="col"  >
													<label for="ctl00_MainPlaceHolder_cbojoin6" ></label>
														<asp:DropDownList ID="cbojoin6" Runat="server">
															<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
															<asp:ListItem Value="=">=</asp:ListItem>
															<asp:ListItem Value="*=">*=</asp:ListItem>
															<asp:ListItem Value="=*">=*</asp:ListItem>
															<asp:ListItem Value="<%=str1 %>">>=</asp:ListItem>
															<asp:ListItem Value="<="><=</asp:ListItem>
															<asp:ListItem Value="<>"><></asp:ListItem>
														</asp:DropDownList>
													</td>
													<td style="width: 148px" scope ="col" >
													<label for="ctl00_MainPlaceHolder_cbocol26" ></label>
													<asp:dropdownlist id="cbocol26" tabindex="21" Runat="server"></asp:dropdownlist></td>
												</tr>
												<tr>
													<td scope ="col" >and
                                                    </td>
                                                    <td style="width: 119px" scope ="col" >
                                                    <label for="ctl00_MainPlaceHolder_cbocol17" ></label>
													<asp:dropdownlist id="cbocol17" tabindex="22" Runat="server"></asp:dropdownlist>
													</td>
													<td style="width: 148px" scope ="col"  >
													<label for="ctl00_MainPlaceHolder_cbojoin7" ></label>
														<asp:DropDownList ID="cbojoin7" Runat="server">
															<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
															<asp:ListItem Value="=">=</asp:ListItem>
															<asp:ListItem Value="*=">*=</asp:ListItem>
															<asp:ListItem Value="=*">=*</asp:ListItem>
															<asp:ListItem Value="<%=str1 %>">>=</asp:ListItem>
															<asp:ListItem Value="<="><=</asp:ListItem>
															<asp:ListItem Value="<>"><></asp:ListItem>
														</asp:DropDownList>
													</td>
													<td style="width: 148px" scope ="col" >
													<label for="ctl00_MainPlaceHolder_cbocol27" ></label>
													<asp:dropdownlist id="cbocol27" tabindex="23" Runat="server"></asp:dropdownlist></td>
												</tr>
												<tr>
													<td scope ="col" >and
                                                    </td>
                                                    <td style="width: 119px" scope ="col" >
                                                    <label for="ctl00_MainPlaceHolder_cbocol18" ></label>
													<asp:dropdownlist id="cbocol18" tabindex="24" Runat="server"></asp:dropdownlist>
													</td>
													<td style="width: 148px" scope ="col"  >
													<label for="ctl00_MainPlaceHolder_cbojoin8" ></label>
														<asp:DropDownList ID="cbojoin8" Runat="server">
															<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
															<asp:ListItem Value="=">=</asp:ListItem>
															<asp:ListItem Value="*=">*=</asp:ListItem>
															<asp:ListItem Value="=*">=*</asp:ListItem>
															<asp:ListItem Value="<%=str1 %>">>=</asp:ListItem>
															<asp:ListItem Value="<="><=</asp:ListItem>
															<asp:ListItem Value="<>"><></asp:ListItem>
														</asp:DropDownList>
													</td>
													<td style="width: 148px" scope ="col" >
													<label for="ctl00_MainPlaceHolder_cbocol28" ></label>
													<asp:dropdownlist id="cbocol28" tabindex="25" Runat="server"></asp:dropdownlist></td>
												</tr>
												<tr>
													<td scope ="col" >and
                                                    </td>
                                                    <td style="width: 119px" scope ="col" >
                                                    <label for="ctl00_MainPlaceHolder_cbocol19" ></label>
													<asp:dropdownlist id="cbocol19" tabindex="26" Runat="server"></asp:dropdownlist>
													</td>
													<td style="width: 148px" scope ="col"  >
													<label for="ctl00_MainPlaceHolder_cbojoin9" ></label>
														<asp:DropDownList ID="cbojoin9" Runat="server">
															<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
															<asp:ListItem Value="=">=</asp:ListItem>
															<asp:ListItem Value="*=">*=</asp:ListItem>
															<asp:ListItem Value="=*">=*</asp:ListItem>
															<asp:ListItem Value="<%=str1 %>">>=</asp:ListItem>
															<asp:ListItem Value="<="><=</asp:ListItem>
															<asp:ListItem Value="<>"><></asp:ListItem>
														</asp:DropDownList>
													</td>
													<td style="width: 148px" scope ="col" >
													<label for="ctl00_MainPlaceHolder_cbocol29" ></label>
													<asp:dropdownlist id="cbocol29" tabindex="27" Runat="server"></asp:dropdownlist></td>
												</tr>
												<tr>
													<td scope ="col" >and
                                                    </td>
                                                    <td style="width: 119px" scope ="col" >
                                                    <label for="ctl00_MainPlaceHolder_cbocol20" ></label>
													<asp:dropdownlist id="cbocol20" tabindex="28" Runat="server"></asp:dropdownlist>
													</td>
													<td style="width: 148px" scope ="col"  >
													 <label for="ctl00_MainPlaceHolder_cbojoin10" ></label>
														<asp:DropDownList ID="cbojoin10" Runat="server">
															<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
															<asp:ListItem Value="=">=</asp:ListItem>
															<asp:ListItem Value="*=">*=</asp:ListItem>
															<asp:ListItem Value="=*">=*</asp:ListItem>
															<asp:ListItem Value="<%=str1 %>">>=</asp:ListItem>
															<asp:ListItem Value="<="><=</asp:ListItem>
															<asp:ListItem Value="<>"><></asp:ListItem>
														</asp:DropDownList>
													</td>
													<td style="width: 148px" scope ="col" >
													 <label for="ctl00_MainPlaceHolder_cbocol30" ></label>
													<asp:dropdownlist id="cbocol30" tabindex="29" Runat="server"></asp:dropdownlist></td>
												</tr>
												<tr>
													<td colspan="3" scope ="colgroup" >
													<label for="ctl00_MainPlaceHolder_txthead2" ></label>
													<input id="txthead2" style="VISIBILITY: hidden; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 18px; BORDER-BOTTOM-STYLE: none"
															readonly="readonly"  type="text" size="33" value="Conditions:" name="txthead2" runat="server"/></td>
												</tr>
												<tr>
													<td style="height: 28px" scope ="col"  >and
                                                    </td>
                                                    <td style="height: 28px" scope ="col"  >
                                                    <label for="ctl00_MainPlaceHolder_cbocolA1" ></label>
													<asp:dropdownlist id="cbocolA1" tabindex="30" Runat="server"></asp:dropdownlist>
                                                    </td>
                                                    <td style="height: 28px" scope ="col" >
                                                    <label for="ctl00_MainPlaceHolder_cbofunc11" ></label>
														<asp:dropdownlist id="cbofunc11" tabindex="31" Runat="server">
															<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
															<asp:ListItem Value="=">=</asp:ListItem>
															<asp:ListItem Value="!=">!=</asp:ListItem>
															<asp:ListItem Value="<%=str1 %>">>=</asp:ListItem>
															<asp:ListItem Value="<="><=</asp:ListItem>
															<asp:ListItem Value=">">></asp:ListItem>
															<asp:ListItem Value="<"><</asp:ListItem>
															<asp:ListItem Value="<>"><></asp:ListItem>
															<asp:ListItem Value="LIKE">LIKE</asp:ListItem>
														</asp:dropdownlist>
                                                        </td>
                                                        <td style="width: 148px; height: 28px;" scope ="col" >
                                                          <label for="ctl00_MainPlaceHolder_txtval11" ></label>
														<input id="txtval11" style="WIDTH: 112px; HEIGHT: 18px" tabindex="32" type="text" size="13"
															name="txtval11" runat="server" maxlength="50"/>                                              
                                                        	<input id="imageFromDate" title="Select Date" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px; CURSOR: hand; BORDER-BOTTOM: 0px solid; "
															onclick="ShowCalendar1(this.id);" tabindex="33" type="button" name="imageFromDate"/>
													</td>
												</tr>
												<tr>
													<td scope ="col"  >and
                                                    </td>
                                                    <td style="width: 119px" scope ="col" >
                                                    <label for="ctl00_MainPlaceHolder_cbocolA2" ></label>
													<asp:dropdownlist id="cbocolA2" tabindex="34" Runat="server"></asp:dropdownlist>
                                                    </td>
                                                    <td scope ="col" >	
                                                    <label for="ctl00_MainPlaceHolder_cbofunc12" ></label>												
														<asp:dropdownlist id="cbofunc12" tabindex="35" Runat="server">
															<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
															<asp:ListItem Value="=">=</asp:ListItem>
															<asp:ListItem Value="!=">!=</asp:ListItem>
															<asp:ListItem Value="<%=str1 %>">>=</asp:ListItem>
															<asp:ListItem Value="<="><=</asp:ListItem>
															<asp:ListItem Value=">">></asp:ListItem>
															<asp:ListItem Value="<"><</asp:ListItem>
															<asp:ListItem Value="<>"><></asp:ListItem>
															<asp:ListItem Value="LIKE">LIKE</asp:ListItem>
														</asp:dropdownlist>
                                                    </td>
                                                    <td style="width: 148px" scope ="col" >  
                                                    <label for="ctl00_MainPlaceHolder_txtval12" ></label>                                                  
														<input id="txtval12" style="WIDTH: 112px; HEIGHT: 18px" tabindex="36" type="text" size="13"
															name="txtval12" runat="server" maxlength="50"/>                                                    
                                                    	<input id="imageFromDate1" title="Select Date" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px; CURSOR: hand; BORDER-BOTTOM: 0px solid; "
															onclick="ShowCalendar2(this.id);" tabindex="37" type="button" name="imageFromDate"/>
													</td>
												</tr>
												<tr>
													<td scope ="col">and
                                                    </td>
                                                    <td scope ="col"  >
                                                     <label for="ctl00_MainPlaceHolder_cbocolA3" ></label>
													<asp:dropdownlist id="cbocolA3" tabindex="38" Runat="server"></asp:dropdownlist>
													</td>
													<td scope="col" >
													<label for="ctl00_MainPlaceHolder_cbofunc13" ></label>
														<asp:dropdownlist id="cbofunc13" tabindex="39" Runat="server">
															<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
															<asp:ListItem Value="=">=</asp:ListItem>
															<asp:ListItem Value="!=">!=</asp:ListItem>
															<asp:ListItem Value="<%=str1 %>">>=</asp:ListItem>
															<asp:ListItem Value="<="><=</asp:ListItem>
															<asp:ListItem Value=">">></asp:ListItem>
															<asp:ListItem Value="<"><</asp:ListItem>
															<asp:ListItem Value="<>"><></asp:ListItem>
															<asp:ListItem Value="LIKE">LIKE</asp:ListItem>
														</asp:dropdownlist>
                                                    </td>
                                                    <td style="width: 148px" scope ="col"  >
                                                    <label for="ctl00_MainPlaceHolder_txtval13" ></label>
														<input id="txtval13" style="WIDTH: 112px; HEIGHT: 18px" tabindex="40" type="text" size="13"
															name="txtval13" runat="server" maxlength="50"/>
                                                             
															<input id="imageFromDate2" title="Select Date" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px; CURSOR: hand; BORDER-BOTTOM: 0px solid;  "
															onclick="ShowCalendar3(this.id);" tabindex="41" type="button" name="imageFromDate"/>
													</td>
												</tr>
												<tr>
													<td scope ="col"  >and
                                                    </td>
                                                    <td style="width: 119px" scope ="col" >
                                                    <label for="ctl00_MainPlaceHolder_cbocolA4" ></label>
													<asp:dropdownlist id="cbocolA4" tabindex="42" Runat="server"></asp:dropdownlist>
                                                    </td>
                                                    <td scope ="col" >
                                                    <label for="ctl00_MainPlaceHolder_cbofunc14" ></label>
														<asp:dropdownlist id="cbofunc14" tabindex="43" Runat="server">
															<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
															<asp:ListItem Value="=">=</asp:ListItem>
															<asp:ListItem Value="!=">!=</asp:ListItem>
															<asp:ListItem Value="<%=str1 %>">>=</asp:ListItem>
															<asp:ListItem Value="<="><=</asp:ListItem>
															<asp:ListItem Value=">">></asp:ListItem>
															<asp:ListItem Value="<"><</asp:ListItem>
															<asp:ListItem Value="<>"><></asp:ListItem>
															<asp:ListItem Value="LIKE">LIKE</asp:ListItem>
														</asp:dropdownlist>
														</td>
														<td style="width: 148px" scope="col" >
														 <label for="ctl00_MainPlaceHolder_txtval14" ></label>
														<input id="txtval14" style="WIDTH: 112px; HEIGHT: 18px" tabindex="44" type="text" size="13"
															name="txtval14" runat="server" maxlength="50"/>
                                                   		<input id="imageFromDate3" title="Select Date" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px; CURSOR: hand; BORDER-BOTTOM: 0px solid; "
															onclick="ShowCalendar4(this.id);" tabindex="45" type="button" name="imageFromDate"/>
													</td>
												</tr>
												<tr>
													<td  scope ="col" >and
                                                    </td>
                                                    <td style="width: 119px" scope ="col" >
                                                     <label for="ctl00_MainPlaceHolder_cbocolA5" ></label>
													<asp:dropdownlist id="cbocolA5" tabindex="46" Runat="server"></asp:dropdownlist>
                                                    </td>
                                                    <td scope ="col">
                                                    <label for="ctl00_MainPlaceHolder_cbofunc15" ></label>
														<asp:dropdownlist id="cbofunc15" tabindex="47" Runat="server">
															<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
															<asp:ListItem Value="=">=</asp:ListItem>
															<asp:ListItem Value="!=">!=</asp:ListItem>
															<asp:ListItem Value="<%=str1 %>">>=</asp:ListItem>
															<asp:ListItem Value="<="><=</asp:ListItem>
															<asp:ListItem Value=">">></asp:ListItem>
															<asp:ListItem Value="<"><</asp:ListItem>
															<asp:ListItem Value="<>"><></asp:ListItem>
															<asp:ListItem Value="LIKE">LIKE</asp:ListItem>
														</asp:dropdownlist>
                                                    </td>
                                                    <td style="width: 148px" scope ="col" >
                                                     <label for="ctl00_MainPlaceHolder_txtval15" ></label>
														<input id="txtval15" style="WIDTH: 112px; HEIGHT: 18px" tabindex="48" type="text" size="13"
															name="txtval15" runat="server" maxlength="50"/>
															<input id="imageFromDate4" title="Select Date" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px; CURSOR: hand; BORDER-BOTTOM: 0px solid; "
															onclick="ShowCalendar5(this.id);" tabindex="49" type="button" name="imageFromDate"/>
													</td>
												</tr>
												<tr>
													<td scope ="col" >and
                                                    </td>
                                                    <td style="width: 119px" scope ="col">
                                                     <label for="ctl00_MainPlaceHolder_cbocolA6" ></label>
													<asp:dropdownlist id="cbocolA6" tabindex="50" Runat="server"></asp:dropdownlist>
                                                    </td>
                                                    <td scope ="col">
                                                     <label for="ctl00_MainPlaceHolder_cbofunc16" ></label>
														<asp:dropdownlist id="cbofunc16" tabindex="51" Runat="server">
															<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
															<asp:ListItem Value="=">=</asp:ListItem>
															<asp:ListItem Value="!=">!=</asp:ListItem>
															<asp:ListItem Value="<%=str1 %>">>=</asp:ListItem>
															<asp:ListItem Value="<="><=</asp:ListItem>
															<asp:ListItem Value=">">></asp:ListItem>
															<asp:ListItem Value="<"><</asp:ListItem>
															<asp:ListItem Value="<>"><></asp:ListItem>
															<asp:ListItem Value="LIKE">LIKE</asp:ListItem>
														</asp:dropdownlist>
                                                    </td>
                                                    <td style="width: 148px" scope ="col">
                                                     <label for="ctl00_MainPlaceHolder_txtval16" ></label>
														<input id="txtval16" style="WIDTH: 112px; HEIGHT: 18px" tabindex="52" type="text" size="13"
															name="txtval16" runat="server" maxlength="50"/>
                                                    	<input id="imageFromDate5" title="Select Date" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px; CURSOR: hand; BORDER-BOTTOM: 0px solid; "
															onclick="ShowCalendar6(this.id);" tabindex="53" type="button" name="imageFromDate"/>
													</td>
												</tr>
												<tr>
													<td scope ="col" >and
                                                    </td>
                                                    <td style="width: 119px" scope ="col">
                                                    <label for="ctl00_MainPlaceHolder_cbocolA7" ></label>
													<asp:dropdownlist id="cbocolA7" tabindex="54" Runat="server"></asp:dropdownlist>
                                                    </td>
                                                    <td scope ="col">
                                                    <label for="ctl00_MainPlaceHolder_cbofunc17" ></label>
														<asp:dropdownlist id="cbofunc17" tabindex="55" Runat="server">
															<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
															<asp:ListItem Value="=">=</asp:ListItem>
															<asp:ListItem Value="!=">!=</asp:ListItem>
															<asp:ListItem Value="<%=str1 %>">>=</asp:ListItem>
															<asp:ListItem Value="<="><=</asp:ListItem>
															<asp:ListItem Value=">">></asp:ListItem>
															<asp:ListItem Value="<"><</asp:ListItem>
															<asp:ListItem Value="<>"><></asp:ListItem>
															<asp:ListItem Value="LIKE">LIKE</asp:ListItem>
														</asp:dropdownlist>
                                                    </td>
                                                    <td style="width: 148px" scope ="col">
                                                      <label for="ctl00_MainPlaceHolder_txtval17" ></label>
														<input id="txtval17" style="WIDTH: 112px; HEIGHT: 18px" tabindex="56" type="text" size="13"
															name="txtval17" runat="server" maxlength="50"/>
                                                    	<input id="imageFromDate6" title="Select Date" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px; CURSOR: hand; BORDER-BOTTOM: 0px solid;  "
															onclick="ShowCalendar7(this.id);" tabindex="57" type="button" name="imageFromDate"/>
													</td>
												</tr>
												<tr>
													<td scope ="col" >and
                                                    </td>
                                                    <td style="width: 119px" scope ="col">
                                                     <label for="ctl00_MainPlaceHolder_cbocolA8" ></label>
													<asp:dropdownlist id="cbocolA8" tabindex="58" Runat="server"></asp:dropdownlist>
                                                    </td>
                                                    <td scope ="col">
                                                    <label for="ctl00_MainPlaceHolder_cbofunc18" ></label>
														<asp:dropdownlist id="cbofunc18" tabindex="59" Runat="server">
															<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
															<asp:ListItem Value="=">=</asp:ListItem>
															<asp:ListItem Value="!=">!=</asp:ListItem>
															<asp:ListItem Value="<%=str1 %>">>=</asp:ListItem>
															<asp:ListItem Value="<="><=</asp:ListItem>
															<asp:ListItem Value=">">></asp:ListItem>
															<asp:ListItem Value="<"><</asp:ListItem>
															<asp:ListItem Value="<>"><></asp:ListItem>
															<asp:ListItem Value="LIKE">LIKE</asp:ListItem>
														</asp:dropdownlist>
                                                    </td>
                                                    <td style="width: 148px" scope ="col">
                                                    <label for="ctl00_MainPlaceHolder_txtval18" ></label>
														<input id="txtval18" style="WIDTH: 112px; HEIGHT: 18px" tabindex="60" type="text" size="13"
															name="txtval18" runat="server" maxlength="50"/><input id="imageFromDate7" title="Select Date" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px; CURSOR: hand; BORDER-BOTTOM: 0px solid; "
															onclick="ShowCalendar8(this.id);" tabindex="61" type="button" name="imageFromDate"/>
													</td>
												</tr>
												<tr>
													<td  scope ="col">and
                                                    </td>
                                                    <td scope ="col" >
                                                    <label for="ctl00_MainPlaceHolder_cbocolA9" ></label>
													<asp:dropdownlist id="cbocolA9" tabindex="62" Runat="server"></asp:dropdownlist>
                                                    </td>
                                                    <td scope ="col">
                                                     <label for="ctl00_MainPlaceHolder_cbofunc19" ></label>
														<asp:dropdownlist id="cbofunc19" tabindex="63" Runat="server">
															<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
															<asp:ListItem Value="=">=</asp:ListItem>
															<asp:ListItem Value="!=">!=</asp:ListItem>
															<asp:ListItem Value="<%=str1 %>">>=</asp:ListItem>
															<asp:ListItem Value="<="><=</asp:ListItem>
															<asp:ListItem Value=">">></asp:ListItem>
															<asp:ListItem Value="<"><</asp:ListItem>
															<asp:ListItem Value="<>"><></asp:ListItem>
															<asp:ListItem Value="LIKE">LIKE</asp:ListItem>
														</asp:dropdownlist>
                                                    </td>
                                                    <td style="width: 148px" scope ="col" >
                                                    <label for="ctl00_MainPlaceHolder_txtval19" ></label>
														<input id="txtval19" style="WIDTH: 112px; HEIGHT: 18px" tabindex="64" type="text" size="13"
															name="txtval19" runat="server" maxlength="50"/><input id="imageFromDate8" title="Select Date" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px; CURSOR: hand; BORDER-BOTTOM: 0px solid; "
															onclick="ShowCalendar9(this.id);" tabindex="65" type="button" name="imageFromDate"/>
													</td>
												</tr>
												<tr>
													<td scope ="col" >and
                                                    </td>
                                                    <td scope ="col" >
                                                    <label for="ctl00_MainPlaceHolder_cbocolA10" ></label>
													<asp:dropdownlist id="cbocolA10" tabindex="66" Runat="server"></asp:dropdownlist>
                                                    </td>
                                                    <td scope ="col">
                                                    <label for="ctl00_MainPlaceHolder_cbofunc20" ></label>
														<asp:dropdownlist id="cbofunc20" tabindex="67" Runat="server">
															<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
															<asp:ListItem Value="=">=</asp:ListItem>
															<asp:ListItem Value="!=">!=</asp:ListItem>
															<asp:ListItem Value="<%=str1 %>">>=</asp:ListItem>
															<asp:ListItem Value="<="><=</asp:ListItem>
															<asp:ListItem Value=">">></asp:ListItem>
															<asp:ListItem Value="<"><</asp:ListItem>
															<asp:ListItem Value="<>"><></asp:ListItem>
															<asp:ListItem Value="LIKE">LIKE</asp:ListItem>
														</asp:dropdownlist>
                                                    </td>
                                                    <td style="width: 148px" scope ="col">
                                                      <label for="ctl00_MainPlaceHolder_txtval20" ></label>
														<input id="txtval20" style="WIDTH: 112px; HEIGHT: 18px" tabindex="68" type="text" size="13"
															name="txtval20" runat="server" maxlength="50"/><input id="imageFromDate9" title="Select Date" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px; CURSOR: hand; BORDER-BOTTOM: 0px solid; "
															onclick="ShowCalendar10(this.id);" tabindex="69" type="button" name="imageFromDate"/>
													</td>
												</tr>
										</table>
									</td>
								</tr>
								
							</table>
						</div>
					</td>
				</tr>
				<tr>
					<td colspan="4">&nbsp;</td>
				</tr>
                <tr>
                <td>
                <table runat="server" visible="false" id="savespandisplay" class="table" >
                <tr>
					<td scope ="col"><strong><asp:Label ID="lbl4" Text="Level 1" runat="server" ></asp:Label> </strong>
					</td>
					<td colspan="3" scope ="colgroup" ><asp:dropdownlist id="cbodept1" tabindex="70" Runat="server" CssClass="dropdownlist"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td scope ="col"><strong><asp:Label ID="lbl5" Text="Level 2" runat="server" ></asp:Label> </strong>
					<td colspan="3" scope ="colgroup"><asp:dropdownlist id="cboclient1" tabindex="71" Runat="server" CssClass="dropdownlist"></asp:dropdownlist></td>
				</tr>
				<tr>
					<td scope ="col"><strong><asp:Label ID="lbl6" Text="Level 3" runat="server"></asp:Label></strong> 
					</td>
					<td colspan="3" scope ="colgroup"><asp:dropdownlist id="cbolob1" tabindex="72" Runat="server" CssClass="dropdownlist"></asp:dropdownlist></td>
				</tr>
                </table>
                </td>
                </tr>
			    <tr>
					<td colspan="3" scope ="colgroup">
						<div id="divname" runat="server"></div>
					</td>
				</tr>
				<tr>
				   <%--<td colspan="1" scope ="colgroup">
				<asp:Label ID="lblLocalView" Text="View Type" runat="server" Font-Bold="True"></asp:Label>
				   </td>--%>
				   <td scope ="col" >
				   <label for="ctl00_MainPlaceHolder_chkLocalView"  visible="false" ></label>
				  <asp:CheckBox ID="chkLocalView" runat="server" visible="false"  />
				   </td>
				</tr>
				<tr>
					<td colspan="4" scope ="colgroup" class="style1">
					<asp:Label ID="lblview" runat="server" Font-Bold="True">View Name:</asp:Label>
					<label for="ctl00_MainPlaceHolder_txtname" ></label>
						<asp:TextBox ID="txtname" Runat="server" MaxLength="50" Width="160px"></asp:TextBox>&nbsp;
                        <input type="button" id="cmdsaveas" name="cmdsaveas" onclick="javascript:chkname();"  value="Save As" class="button" runat="server" visible="false" style="WIDTH: 144px; HEIGHT: 20px"  />&nbsp;
							<input class="button" runat="server" id="cmdcreatetab" style="WIDTH: 144px; HEIGHT: 20px" onclick="javascript:chkvalidation();"
							tabindex="74" visible="false"   type="button" value="Save View"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="button" id="cmdsaveasmul" name="cmdsaveas" runat="server" visible="false" onclick="javascript:chkname2();" value="Save As" class="button" style="WIDTH: 144px; HEIGHT: 20px"  />&nbsp;
							<input class="button" runat="server" id="cmdcreatetabmul" style="WIDTH: 144px; HEIGHT: 20px" visible="false" onclick="javascript:chkvalidation2();"
							tabindex="74" type="button" value="Save View"/>
					</td>
				</tr>
               <%-- <tr>
                <td colspan="6" scope="colgroup">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="button" id="cmdsaveasmul" name="cmdsaveas" runat="server" visible="false" onclick="javascript:chkname2();" value="Save As" class="button" style="WIDTH: 144px; HEIGHT: 20px"  />&nbsp;
							<input class="button" runat="server" id="cmdcreatetabmul" style="WIDTH: 144px; HEIGHT: 20px" visible="false" onclick="javascript:chkvalidation2();"
							tabindex="74" type="button" value="Save View"/>
							<asp:button id="Button3" style="VISIBILITY: hidden" Runat="server" Text="Update View" CssClass="button"
							Height="20px" Width="144px"></asp:button>
                </td>  
                </tr>--%>
				<tr>
					<td colspan="4" scope ="colgroup">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="4" scope ="colgroup"><font color="red">Note: Use Ctrl key to select or deselect the multiple 
							selection.</font>
					</td>
				</tr>
				<tr>
					<td scope ="col" >
						<input type="hidden" id="txtcols" name="txtcols" runat="server"/> <input type="hidden" id="txtgroup" name="txtgroup" runat="server"/>
						<input type="hidden" id="txtwherejoin" name="txtwherejoin" runat="server"/> <input type="hidden" id="txtwherecon" name="txtwherecon" runat="server"/>
						<input id="txt1sttabcolvalue" type="hidden" name="txt1sttabvalue" runat="server" />
						<input id="txt2ndtabcolvalue" type="hidden" name="txt1sttabvalue" runat="server" />
						<input id="txt1stformula" type ="hidden" name ="firstformula" runat="server" />
  <input id="groupby" type ="hidden" name ="groupby" runat="server" />
						<!--<input type="hidden" id="txtformula1" name="txtformula1" runat="server"> <input type="hidden" id="txtformula2" name="txtformula2" runat="server">
						<input type="hidden" id="txtformula3" name="txtformula3" runat="server"> <input type="hidden" id="txtformula4" name="txtformula4" runat="server">
						<input type="hidden" id="txtformula5" name="txtformula5" runat="server"> <input type="hidden" id="txtformula6" name="txtformula6" runat="server">
						<input type="hidden" id="txtformula7" name="txtformula7" runat="server"> <input type="hidden" id="txtformula8" name="txtformula8" runat="server">
						<input type="hidden" id="txtformula9" name="txtformula9" runat="server"> <input type="hidden" id="txtformula10" name="txtformula10" runat="server">-->
						<input type="hidden" id="lblname1" name="lblname1" runat="server"/>
						 <iframe id="frmexec"  runat="server" title ="exec" style="VISIBILITY:hidden;WIDTH:256px;HEIGHT:56px">
						</iframe>
					</td>
					
				</tr>
			</table>
			</body>
			<input type="hidden" name="__EVENTTARGET"/>
			<input type="hidden" name="__EVENTARGUMENT"/>
</asp:Content>

<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
        {
            height: 50px;
        }
    </style>
</asp:Content>


