<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" EnableEventValidation ="false" AutoEventWireup="false" CodeFile="CreateCrossTab.aspx.vb" Inherits="DataTransfer_CreateCrossTab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .dropdwonlist
        {}
        .style1
        {
            width: 237px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
<script language="javascript" type ="text/javascript" >
			
		var arrQueryString ;
        window.arrQueryString = ["NULL","NULL","NULL","NULL"]; // Array For Finding Department,Client,lob of First Span
        
        var arrQueryString1;
        window.arrQueryString1=["NULL","NULL","NULL","NULL"]; // Array For Finding Department,Client,lob of Second Span
        
        var arrQueryString2;
        window.arrQueryString2=["NULL","NULL","NULL","NULL"]; // Array For Finding Department,Client,lob of Third Span
        
			
			
	   var arrFirstTabQueryString;
	   window.arrFirstTabQueryString=["NULL","NULL","NULL","NULL","NULL","NULL","NULL","NULL","NULL","NULL"];
	   
	   var arrJoinTabQueryString;
	   window.arrJoinTabQueryString=["NULL","NULL","NULL","NULL","NULL","NULL","NULL","NULL","NULL","NULL"];
	   		
	    function getclient()
			
			{
			
				document.getElementById("<%=hidQueryString.ClientID %>").value = "";
			
				if (document.getElementById("<%=cbodept.ClientId %>").selectedIndex==0)
				{
					for(i=document.getElementById("<%=cboclient.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cboclient.ClientId %>").remove(i);
					}
					for(i=document.getElementById("<%=cbotab1.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbotab1.ClientId %>").remove(i);
					}
					for(i=document.getElementById("<%=cbolob.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbolob.ClientId %>").remove(i);
					}
					
					/*for(i=document.getElementById("lsttab1cols").length;i>=0;i--)
					{
						document.getElementById("lsttab1cols").remove(i);
						document.getElementById("cbocol11").remove(i);
						document.getElementById("cbocol12").remove(i);
						document.getElementById("cbocol13").remove(i);
						
					}*/					
					
					
					
					
				}
				else
				{
				
					 if(document.getElementById("<%=cbodept.ClientID %>").selectedIndex != 0)
                        {
                        document.getElementById("<%=hidQueryString.ClientID %>").value ="";
                        window.arrQueryString[0]= document.getElementById("<%=cbodept.ClientID %>").value;
                        //alert(window.arrQueryString[1])
                      }  
            else
                     {
                         window.arrQueryString[1]="NULL";    
                      }
            for(i=0;i<4;i++)
	                       {
	                      // alert(document.getElementById("<%=hidQueryString.ClientID %>").value)
                            
                         document.getElementById("<%=hidQueryString.ClientID %>").value += window.arrQueryString[i] + "#";
                            }
                            //alert(document.getElementById("<%=hidQueryString.ClientID %>").value)
				//	AjaxClass1.bindclient(document.getElementById("<%=cbodept.ClientId %>").value,filclient)
					DataTransfer.bindclient(document.getElementById("<%=cbodept.ClientId %>").value,filclient)
					
					
					//AjaxClass1.bindepttab(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,filtab)
				var userid="<%=Session("userid") %>"
                    DataTransfer.bindepttab(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid,filtab)
				    // empty the hidQueryString field
		      
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
			
			
			
//			'------------------------------------------------------------------------------------------------------
			function getclient1()
			{
				if (document.getElementById("<%=cbodept1.ClientId %>").selectedIndex==0)
				{
					for(i=document.getElementById("<%=cboclient1.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cboclient1.ClientId %>").remove(i);
					}
					for(i=document.getElementById("<%=cbotab2.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbotab2.ClientId %>").remove(i);
					}
					for(i=document.getElementById("<%=cbolob1.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbolob1.ClientId %>").remove(i);
					}
					
					/*for(i=document.getElementById("lsttab2cols").length;i>=0;i--)
					{
						document.getElementById("lsttab2cols").remove(i);
						document.getElementById("cbocol21").remove(i);
						document.getElementById("cbocol22").remove(i);
						document.getElementById("cbocol23").remove(i);
					}*/
				}
				else
				{
				 if(document.getElementById("<%=cbodept1.ClientID %>").selectedIndex != 0)
                        {
                        document.getElementById("<%=hidQueryString1.ClientID %>").value ="";
                        window.arrQueryString1[0]= document.getElementById("<%=cbodept1.ClientID %>").value;
                      }  
            else
                     {
                         window.arrQueryString1[1]="NULL";    
                      }
            for(i=0;i<4;i++)
	                       {
                            
                         document.getElementById("<%=hidQueryString1.ClientID %>").value += window.arrQueryString1[i] + "#";
                            }
				
					//AjaxClass1.bindclient(document.getElementById("<%=cbodept1.ClientId %>").value,filclient1)
					//AjaxClass1.bindepttab(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,filtab1)
				     var userid="<%=Session("userid") %>"

                    DataTransfer.bindclient(document.getElementById("<%=cbodept1.ClientId %>").value,filclient1)
					DataTransfer.bindepttab(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid,filtab1)
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
			
			//-------------------------------------------------------------------------------------------------------
			
			
			function getclient2()
			{
				if (document.getElementById("<%=cbodept2.ClientId %>").selectedIndex==0)
				{
					for(i=document.getElementById("<%=cboclient2.Clientid %>").length;i>=0;i--)
					{
						document.getElementById("<%=cboclient2.Clientid %>").remove(i);
					}
					for(i=document.getElementById("<%=cbolob2.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbolob2.ClientId %>").remove(i);
					}
				}
				else
				{
				     if(document.getElementById("<%=cbodept2.ClientID %>").selectedIndex != 0)
                        {
                        document.getElementById("<%=hidQueryString2.ClientID %>").value ="";
                        window.arrQueryString2[0]= document.getElementById("<%=cbodept2.ClientID %>").value;
                       // alert(window.arrQueryString2[1])
                      }  
            else
                     {
                         window.arrQueryString[1]="NULL";    
                      }
            for(i=0;i<4;i++)
	                       {
	                       //alert(document.getElementById("<%=hidQueryString2.ClientID %>").value)
                            
                         document.getElementById("<%=hidQueryString2.ClientID %>").value += window.arrQueryString2[i] + "#";
                            }
				
					//AjaxClass1.bindclient(document.getElementById("<%=cbodept2.ClientId %>").value,filclient2)
					DataTransfer.bindclient(document.getElementById("<%=cbodept2.ClientId %>").value,filclient2)
				}
			}
			function filclient2(res)
			{
				for(i=document.getElementById("<%=cboclient2.Clientid %>").length;i>=0;i--)
				{
					document.getElementById("<%=cboclient2.Clientid %>").remove(i);
				}
				var strdata = res.value
				var arrdata = strdata.split("$")
				var arrdata1
				document.getElementById("<%=cboclient2.Clientid %>").options[0]=new Option("--Select--");
				for(i=0;i<arrdata.length;i++)
				{
					arrdata1 = arrdata[i].split("#")
					document.getElementById("<%=cboclient2.Clientid %>").options[i+1]=new Option(arrdata1[1],arrdata1[0]);
				}
			}
			
			//--------------------------------------------------------------------------------------------------
			
			function getlob()
			{
				var userid="<%=Session("userid") %>"
				if (document.getElementById("<%=cboclient.ClientId %>").selectedIndex==0)
				{
					for(i=document.getElementById("<%=cbolob.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbolob.ClientId %>").remove(i);
					}
					for(i=document.getElementById("<%=cbotab1.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbotab1.ClientId %>").remove(i);
					}
					/*for(i=document.getElementById("lsttab1cols").length;i>=0;i--)
					{
						document.getElementById("lsttab1cols").remove(i);
						document.getElementById("cbocol11").remove(i);
						document.getElementById("cbocol12").remove(i);
						document.getElementById("cbocol13").remove(i);
					}*/
				}
				else
				{
				       if(document.getElementById("<%=cbodept.ClientID %>").selectedIndex != 0)
                            {
                             document.getElementById("<%=hidQueryString.ClientID %>").value="";
                            window.arrQueryString[1]= document.getElementById("<%=cboclient.ClientID %>").value
                             }  
            else
                         {
                            window.arrQueryString[1]="NULL";    
                          }
            for(i=0;i<4;i++)
	                            {
                                 
                                 document.getElementById("<%=hidQueryString.ClientID %>").value += window.arrQueryString[i] + "#";
                                 }
					AjaxClass1.bindlob(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,fillob)
				//DataTransfer.bindlob(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,fillob)
		
				}
				if (document.getElementById("<%=cboclient.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbodept.ClientId %>").selectedIndex!=0)
				{
					//AjaxClass1.bindepttab(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,filtab)
                   

                   DataTransfer.bindepttab(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid,filtab)
				
				}
				else
				{
					//AjaxClass1.bindclienttab(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,filtab)
	DataTransfer.bindclienttab(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid,filtab)

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
			
			//----------------------------------------------------------------------------------------------------
			
			function getlob1()
			{
				var userid="<%=Session("userid") %>"
				if (document.getElementById("<%=cboclient1.ClientId %>").selectedIndex==0)
				{
					for(i=document.getElementById("<%=cbolob1.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbolob1.ClientId %>").remove(i);
					}
					for(i=document.getElementById("<%=cbotab2.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbotab2.ClientId %>").remove(i);
					}
					/*for(i=document.getElementById("lsttab2cols").length;i>=0;i--)
					{
						document.getElementById("lsttab2cols").remove(i);
						document.getElementById("cbocol21").remove(i);
						document.getElementById("cbocol22").remove(i);
						document.getElementById("cbocol23").remove(i);
					}*/
				}
				else
				
				{
				   if(document.getElementById("<%=cbodept1.ClientID %>").selectedIndex != 0)
                            {
                             document.getElementById("<%=hidQueryString1.ClientID %>").value="";
                            window.arrQueryString1[1]= document.getElementById("<%=cboclient1.ClientID %>").value
                             }  
            else
                         {
                            window.arrQueryString1[1]="NULL";    
                          }
            for(i=0;i<4;i++)
	                            {
                                 
                                 document.getElementById("<%=hidQueryString1.ClientID %>").value += window.arrQueryString1[i] + "#";
                                 }
				
				
				
					AjaxClass1.bindlob(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,fillob1)
//DataTransfer.bindlob(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,fillob1)
					
				}
				if (document.getElementById("<%=cboclient1.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbodept1.ClientId %>").selectedIndex!=0)
				{
					//AjaxClass1.bindepttab(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,filtab1)
                     DataTransfer.bindepttab(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid,filtab1)

				}
				else
				{
					//AjaxClass1.bindclienttab(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,filtab1)
				DataTransfer.bindclienttab(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid,filtab1)

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
			
			//--------------------------------------------------------------------------------------------
			function getlob2()
			{
				if (document.getElementById("<%=cboclient2.Clientid %>").selectedIndex==0)
				{
					for(i=document.getElementById("<%=cbolob2.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbolob2.ClientId %>").remove(i);
					}
				}
				else
				{
				
				if(document.getElementById("<%=cboclient2.ClientID %>").selectedIndex != 0)
                            {
                             document.getElementById("<%=hidQueryString2.ClientID %>").value="";
                            window.arrQueryString2[2]= document.getElementById("<%=cbolob2.ClientID %>").value
                             }  
            else
                         {
                            window.arrQueryString2[2]="NULL";    
                          }
            for(i=0;i<4;i++)
	                            {
                                 
                                 document.getElementById("<%=hidQueryString2.ClientID %>").value += window.arrQueryString2[i] + "#";
                                 }
					AjaxClass1.bindlob(document.getElementById("<%=cbodept2.ClientId %>").value,document.getElementById("<%=cboclient2.Clientid %>").value,fillob2)
					//DataTransfer.bindlob(document.getElementById("<%=cbodept2.ClientId %>").value,document.getElementById("<%=cboclient2.Clientid %>").value,fillob2)
				}
			}
			function fillob2(res)
			{
				for(i=document.getElementById("<%=cbolob2.ClientId %>").length;i>=0;i--)
				{
					document.getElementById("<%=cbolob2.ClientId %>").remove(i);
				}
				var strdata = res.value
				var arrdata = strdata.split("$")
				var arrdata1
				document.getElementById("<%=cbolob2.ClientId %>").options[0]=new Option("--Select--");
				for(i=0;i<arrdata.length;i++)
				{
					arrdata1 = arrdata[i].split("#")
					document.getElementById("<%=cbolob2.ClientId %>").options[i+1]=new Option(arrdata1[1],arrdata1[0]);
				}
			}
			
			//-------------------------------------------------------------------------------------------
			function gettab()
			{
				var usertype="<%=Session("typeofuser") %>"
				var userid="<%=Session("userid") %>"
//				if (document.getElementById("<%=cbolob.ClientId %>").selectedIndex==0)
//				{
//					for(i=document.getElementById("<%=cbotab1.ClientId %>").length;i>=0;i--)
//					{
//						document.getElementById("<%=cbotab1.ClientId %>").remove(i);
//					}
//					for(i=document.getElementById("<%=cbotab2.ClientId %>").length;i>=0;i--)
//					{
//						document.getElementById("<%=cbotab2.ClientId %>").remove(i);
//					}
//					for(i=document.getElementById("<%=lsttab1cols.ClientId %>").length;i>=0;i--)
//					{
//						document.getElementById("<%=lsttab1cols.ClientId %>").remove(i);
//						document.getElementById("<%=cbocol11.ClientId %>").remove(i);
//						document.getElementById("<%=cbocol12.ClientId %>").remove(i);
//						document.getElementById("<%=cbocol13.ClientId %>").remove(i);
//					}
//					for(i=document.getElementById("<%=lsttab2cols.ClientId %>").length;i>=0;i--)
//					{
//						document.getElementById("<%=lsttab2cols.ClientId %>").remove(i);
//						document.getElementById("<%=cbocol21.ClientId %>").remove(i);
//						document.getElementById("<%=cbocol22.ClientId %>").remove(i);
//						document.getElementById("<%=cbocol23.ClientId %>").remove(i);
//					}
//				}
//				if (document.getElementById("<%=cbolob.ClientId %>").selectedIndex==0 && document.getElementById("<%=cboclient.ClientId %>").selectedIndex==0)
//				{
//					//AjaxClass1.bindepttab(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,filtab)
//                   //DataTransfer.bindepttab(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid,filtab)
//                   AjaxClass1.bindepttabview(document.getElementById("<%=cbodept.ClientId %>").value,usertype,userid,filtab)

//				}
//				else if(document.getElementById("<%=cbolob.ClientId %>").selectedIndex==0)
//				{
//					//AjaxClass1.bindclienttab(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,filtab)
//                    //DataTransfer.bindclienttab(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid,filtab)
//                    AjaxClass1.bindclienttabview(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId%>").value,usertype,userid,filtab)

//				}
//				else
//				
//				{
//				        
//				              if(document.getElementById("<%=cbodept.ClientID %>").selectedIndex != 0)
//                                {
//                                 document.getElementById("<%=hidQueryString.ClientID %>").value="";
//                                    window.arrQueryString[2]= document.getElementById("<%=cbolob.ClientID %>").value;
//              
//                               
//                            }  
//            else
//                     {
//                        window.arrQueryString[2]="NULL";    
//                     }
//            for(i=0;i<4;i++)
//	                        {
//                             
//                             document.getElementById("<%=hidQueryString.ClientID %>").value += window.arrQueryString[i] + "#";
//                             }
//					//AjaxClass1.bindtable(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,document.getElementById("<%=cbolob.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,filtab)
//				//DataTransfer.bindtable(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,document.getElementById("<%=cbolob.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid,filtab)
            AjaxClass1.bindtableview(60,0,0,usertype,userid,filtab)
//				}
				
			}
			function filtab(res)
			{
				for(i=document.getElementById("<%=cbotab1.ClientId %>").length;i>=0;i--)
				{
					document.getElementById("<%=cbotab1.ClientId %>").remove(i);
				}
				var strdata = res.value
				var arrdata = strdata.split("$")
				var arrdata1
				document.getElementById("<%=cbotab1.ClientId %>").options[0]=new Option("--Select--");
				for(i=0;i<arrdata.length;i++)
				{
					arrdata1 = arrdata[i].split("#")
					document.getElementById("<%=cbotab1.ClientId %>").options[i+1]=new Option(arrdata1[1],arrdata1[0] + "$" + arrdata1[1]);
				}
			}
			
			//------------------------------------------------------------------------------------------------
			
			function gettab1()
			{
				var usertype="<%=Session("typeofuser") %>"
				var userid="<%=Session("userid") %>"
//				if (document.getElementById("<%=cbolob1.ClientId %>").selectedIndex==0)
//				{
//					for(i=document.getElementById("<%=cbotab2.ClientId %>").length;i>=0;i--)
//					{
//						document.getElementById("<%=cbotab2.ClientId %>").remove(i);
//					}
//					for(i=document.getElementById("<%=lsttab2cols.ClientId %>").length;i>=0;i--)
//					{
//						document.getElementById("<%=lsttab2cols.ClientId %>").remove(i);
//						document.getElementById("<%=cbocol21.ClientId %>").remove(i);
//						document.getElementById("<%=cbocol22.ClientId %>").remove(i);
//						document.getElementById("<%=cbocol23.ClientId %>").remove(i);
//					}
//				}
//				if (document.getElementById("<%=cbolob1.ClientId %>").selectedIndex==0 && document.getElementById("<%=cboclient1.ClientId %>").selectedIndex==0)
//				{
//					//AjaxClass1.bindepttab(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,filtab1)
//				//commented on 11/11/08//DataTransfer.bindepttab(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid,filtab1)
//				AjaxClass1.bindepttabview(document.getElementById("<%=cbodept1.ClientId %>").value,usertype,userid,filtab1)

//				}
//				else if(document.getElementById("<%=cbolob1.ClientId %>").selectedIndex==0)
//				{
//					//AjaxClass1.bindclienttab(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,filtab1)
//				//commented on 11/11/08//DataTransfer.bindclienttab(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid,filtab1)
//                    AjaxClass1.bindclienttabview(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId%>").value,usertype,userid,filtab1)
//				}
//				else
//				{
//				        if(document.getElementById("<%=cbodept1.ClientID %>").selectedIndex != 0)
//                                {
//                                 document.getElementById("<%=hidQueryString1.ClientID %>").value="";
//                                    window.arrQueryString1[2]= document.getElementById("<%=cbolob1.ClientID %>").value;
//              
//                               
//                            }  
//            else
//                     {
//                        window.arrQueryString1[1]="NULL";    
//                     }
//            for(i=0;i<4;i++)
//	                        {
//                             
//                             document.getElementById("<%=hidQueryString.ClientID %>").value += window.arrQueryString1[i] + "#";
//                             }
//			
//					//AjaxClass1.bindtable(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,document.getElementById("<%=cbolob1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,filtab1)
//			    //commented on 11/11/08//DataTransfer.bindtable(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,document.getElementById("<%=cbolob1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid,filtab1)
                   AjaxClass1.bindtableview(60,0,0,usertype,userid,filtab1)
//				}
//				
			}
			function filtab1(res)
			{
				for(i=document.getElementById("<%=cbotab2.ClientId %>").length;i>=0;i--)
				{
					document.getElementById("<%=cbotab2.ClientId %>").remove(i);
				}
				var strdata = res.value
				var arrdata = strdata.split("$")
				var arrdata1
				document.getElementById("<%=cbotab2.ClientId %>").options[0]=new Option("--Select--");
				for(i=0;i<arrdata.length;i++)
				{
					arrdata1 = arrdata[i].split("#")
					document.getElementById("<%=cbotab2.ClientId %>").options[i+1]=new Option(arrdata1[1],arrdata1[0] + "$" + arrdata1[1]);
				}
		}
//		
             function gettab2()
			{
				var usertype="<%=Session("typeofuser") %>"
				var userid="<%=Session("userid") %>"
				if (document.getElementById("<%=cbolob.ClientId %>").selectedIndex==0)
				{
					for(i=document.getElementById("<%=cbotab1.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbotab1.ClientId %>").remove(i);
					}
					for(i=document.getElementById("<%=cbotab2.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbotab2.ClientId %>").remove(i);
					}
					for(i=document.getElementById("<%=lsttab1cols.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=lsttab1cols.ClientId %>").remove(i);
						document.getElementById("<%=cbocol11.ClientId %>").remove(i);
						document.getElementById("<%=cbocol12.ClientId %>").remove(i);
						document.getElementById("<%=cbocol13.ClientId %>").remove(i);
					}
					for(i=document.getElementById("<%=lsttab2cols.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=lsttab2cols.ClientId %>").remove(i);
						document.getElementById("<%=cbocol21.ClientId %>").remove(i);
						document.getElementById("<%=cbocol22.ClientId %>").remove(i);
						document.getElementById("<%=cbocol23.ClientId %>").remove(i);
					}
				}
				if (document.getElementById("<%=cbolob.ClientId %>").selectedIndex==0 && document.getElementById("<%=cboclient.ClientId %>").selectedIndex==0)
				{
					//AjaxClass1.bindepttab(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,filtab)
                   //DataTransfer.bindepttab(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid,filtab)
                   AjaxClass1.bindepttabview(document.getElementById("<%=cbodept.ClientId %>").value,usertype,userid,filtab2)

				}
				else if(document.getElementById("<%=cbolob.ClientId %>").selectedIndex==0)
				{
					//AjaxClass1.bindclienttab(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,filtab)
                    //DataTransfer.bindclienttab(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid,filtab)
                    AjaxClass1.bindclienttabview(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId%>").value,usertype,userid,filtab2)

				}
				else
				
				{
				        
				              if(document.getElementById("<%=cbodept.ClientID %>").selectedIndex != 0)
                                {
                                 document.getElementById("<%=hidQueryString.ClientID %>").value="";
                                    window.arrQueryString[2]= document.getElementById("<%=cbolob.ClientID %>").value;
              
                               
                            }  
            else
                     {
                        window.arrQueryString[2]="NULL";    
                     }
            for(i=0;i<4;i++)
	                        {
                             
                             document.getElementById("<%=hidQueryString.ClientID %>").value += window.arrQueryString[i] + "#";
                             }
					//AjaxClass1.bindtable(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,document.getElementById("<%=cbolob.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,filtab)
				//DataTransfer.bindtable(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,document.getElementById("<%=cbolob.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid,filtab)
                AjaxClass1.bindtableview(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId%>").value,document.getElementById("<%=cbolob.ClientId%>").value,usertype,userid,filtab2)
				}
				
			}
			function filtab2(res)
			{
				for(i=document.getElementById("<%=cbotab1.ClientId %>").length;i>=0;i--)
				{
					document.getElementById("<%=cbotab1.ClientId %>").remove(i);
				}
				var strdata = res.value
				var arrdata = strdata.split("$")
				var arrdata1
				document.getElementById("<%=cbotab1.ClientId %>").options[0]=new Option("--Select--");
				for(i=0;i<arrdata.length;i++)
				{
					arrdata1 = arrdata[i].split("#")
					document.getElementById("<%=cbotab1.ClientId %>").options[i+1]=new Option(arrdata1[1],arrdata1[0] + "$" + arrdata1[1]);
				}
			}
			
			//------------------------------------------------------------------------------------------------
			
			function gettab21()
			{
				var usertype="<%=Session("typeofuser") %>"
				var userid="<%=Session("userid") %>"
				if (document.getElementById("<%=cbolob1.ClientId %>").selectedIndex==0)
				{
					for(i=document.getElementById("<%=cbotab2.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbotab2.ClientId %>").remove(i);
					}
					for(i=document.getElementById("<%=lsttab2cols.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=lsttab2cols.ClientId %>").remove(i);
						document.getElementById("<%=cbocol21.ClientId %>").remove(i);
						document.getElementById("<%=cbocol22.ClientId %>").remove(i);
						document.getElementById("<%=cbocol23.ClientId %>").remove(i);
					}
				}
				if (document.getElementById("<%=cbolob1.ClientId %>").selectedIndex==0 && document.getElementById("<%=cboclient1.ClientId %>").selectedIndex==0)
				{
					//AjaxClass1.bindepttab(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,filtab1)
				//commented on 11/11/08//DataTransfer.bindepttab(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid,filtab1)
				AjaxClass1.bindepttabview(document.getElementById("<%=cbodept1.ClientId %>").value,usertype,userid,filtab21)

				}
				else if(document.getElementById("<%=cbolob1.ClientId %>").selectedIndex==0)
				{
					//AjaxClass1.bindclienttab(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,filtab1)
				//commented on 11/11/08//DataTransfer.bindclienttab(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid,filtab1)
                    AjaxClass1.bindclienttabview(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId%>").value,usertype,userid,filtab21)
				}
				else
				{
				        if(document.getElementById("<%=cbodept1.ClientID %>").selectedIndex != 0)
                                {
                                 document.getElementById("<%=hidQueryString1.ClientID %>").value="";
                                    window.arrQueryString1[2]= document.getElementById("<%=cbolob1.ClientID %>").value;
              
                               
                            }  
            else
                     {
                        window.arrQueryString1[1]="NULL";    
                     }
            for(i=0;i<4;i++)
	                        {
                             
                             document.getElementById("<%=hidQueryString.ClientID %>").value += window.arrQueryString1[i] + "#";
                             }
			
					//AjaxClass1.bindtable(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,document.getElementById("<%=cbolob1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,filtab1)
			    //commented on 11/11/08//DataTransfer.bindtable(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,document.getElementById("<%=cbolob1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid,filtab1)
                    AjaxClass1.bindtableview(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId%>").value,document.getElementById("<%=cbolob1.ClientId%>").value,usertype,userid,filtab21)
				}
				
			}
			function filtab21(res)
			{
				for(i=document.getElementById("<%=cbotab2.ClientId %>").length;i>=0;i--)
				{
					document.getElementById("<%=cbotab2.ClientId %>").remove(i);
				}
				var strdata = res.value
				var arrdata = strdata.split("$")
				var arrdata1
				document.getElementById("<%=cbotab2.ClientId %>").options[0]=new Option("--Select--");
				for(i=0;i<arrdata.length;i++)
				{
					arrdata1 = arrdata[i].split("#")
					document.getElementById("<%=cbotab2.ClientId %>").options[i+1]=new Option(arrdata1[1],arrdata1[0] + "$" + arrdata1[1]);
				}
			}	
		//-------------------------------------------------------------------------------------------------------	
			
			function gettab1cols()
			{
				
				var cols = document.getElementById("<%=cbotab1.ClientId %>").value.split("$");
				var arrcols = cols[0].split(',');
				
					for(i=document.getElementById("<%=lsttab1cols.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=lsttab1cols.ClientId %>").remove(i);
						document.getElementById("<%=cbocol11.ClientId %>").remove(i);
						document.getElementById("<%=cbocol12.ClientId %>").remove(i);
						document.getElementById("<%=cbocol13.ClientId %>").remove(i);
						document.getElementById("<%=cbocol14.ClientId %>").remove(i);
						document.getElementById("<%=cbocol15.ClientId %>").remove(i);
						document.getElementById("<%=cbocol16.ClientId %>").remove(i);
						document.getElementById("<%=cbocol17.ClientId %>").remove(i);
						document.getElementById("<%=cbocol18.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA1.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA2.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA3.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA4.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA5.ClientId %>").remove(i);
					}
				if (document.getElementById("<%=cbotab1.ClientId %>").selectedIndex==0)
				{
					document.getElementById("<%=cbocol11.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=txttab11.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbocol12.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=txttab12.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbocol13.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=txttab13.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbocol14.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=txttab14.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbocol15.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=txttab15.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbocol16.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=txttab16.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbocol17.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=txttab17.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbocol18.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=txttab18.ClientId %>").style.visibility = "hidden";
					
					document.getElementById("<%=cbojoin1.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbojoin2.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbojoin3.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbojoin4.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbojoin5.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbojoin6.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbojoin7.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbojoin8.ClientId %>").style.visibility = "hidden";
					//document.all["divdesc"].style.visibility = "hidden";
					document.getElementById("<%=divdesc.ClientID %>").style.visibility = "hidden";
					///////////////////////
					document.getElementById("<%=txthead.ClientID %>") .style.visibility = "hidden";
					document.getElementById("<%=divcon1.ClientId %>").style.visibility = "hidden";
								
				}
				else
				{
			
				    if(document.getElementById("<%=cbotab1.ClientID %>").selectedIndex != 0)
                        {
                        document.getElementById("<%=hidQueryString.ClientID %>").value ="";
                        window.arrQueryString[3]= document.getElementById("<%=cbotab1.ClientID %>").value;
                        //alert(window.arrQueryString[1])
                      }  
                    else
                     {
                         window.arrQueryString[3]="NULL";    
                      }
                        for(i=0;i<4;i++)
	                       {
	                      // alert(document.getElementById("<%=hidQueryString.ClientID %>").value)
                            
                         document.getElementById("<%=hidQueryString.ClientID %>").value += window.arrQueryString[i] + "#";
                            }
                             
					document.getElementById("<%=cbocol11.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol12.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol13.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol14.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol15.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol16.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol17.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol18.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolA1.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolA2.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolA3.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolA4.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolA5.ClientId %>").options[0]=new Option("--Select--");
					for(i=0;i<arrcols.length;i++)
					{
						document.getElementById("<%=lsttab1cols.ClientId %>").options[i]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol11.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol12.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol13.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol14.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol15.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol16.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol17.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol18.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolA1.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolA2.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolA3.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolA4.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolA5.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
					}
					document.getElementById("<%=txttab11.ClientId %>").value = "where    " + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol11.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab11.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab12.ClientId %>").value = "and    " + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol12.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab12.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab13.ClientId %>").value = "and    " + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol13.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab13.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab14.ClientId %>").value = "and    " + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol14.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab14.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab15.ClientId %>").value = "and    " + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol15.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab15.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab16.ClientId %>").value = "and    " + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol16.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab16.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab17.ClientId %>").value = "and    " + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol17.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab17.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab18.ClientId %>").value = "and    " + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol18.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab18.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=cbojoin1.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=cbojoin2.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=cbojoin3.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=cbojoin4.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=cbojoin5.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=cbojoin6.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=cbojoin7.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=cbojoin8.ClientId %>").style.visibility = "visible";
					//document.all["divdesc"].style.visibility = "visible";
					document.getElementById("<%=divdesc.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txthead.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txtcon11.ClientId %>").value = "and    " + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=txtcon12.ClientId %>").value = "and    " + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=txtcon13.ClientId %>").value = "and    " + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=txtcon14.ClientId %>").value = "and    " + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=txtcon15.ClientId %>").value = "and    " + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=divcon1.ClientId %>").style.visibility = "visible";
				}
			}
			
			
			
			function gettab2cols()
			{
				var cols = document.getElementById("<%=cbotab2.ClientId %>").value.split("$");
				var arrcols = cols[0].split(',');
				
					for(i=document.getElementById("<%=lsttab2cols.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=lsttab2cols.ClientId %>").remove(i);
						document.getElementById("<%=cbocol21.ClientId %>").remove(i);
						document.getElementById("<%=cbocol22.ClientId %>").remove(i);
						document.getElementById("<%=cbocol23.ClientId %>").remove(i);
						document.getElementById("<%=cbocol24.ClientId %>").remove(i);
						document.getElementById("<%=cbocol25.ClientId %>").remove(i);
						document.getElementById("<%=cbocol26.ClientId %>").remove(i);
						document.getElementById("<%=cbocol27.ClientId %>").remove(i);
						document.getElementById("<%=cbocol28.ClientId %>").remove(i);
						document.getElementById("<%=cbocolB1.ClientId %>").remove(i);
						document.getElementById("<%=cbocolB2.ClientId %>").remove(i);
						document.getElementById("<%=cbocolB3.ClientId %>").remove(i);
						document.getElementById("<%=cbocolB4.ClientId %>").remove(i);
						document.getElementById("<%=cbocolB5.ClientId %>").remove(i);
					}
				if (document.getElementById("<%=cbotab2.ClientId %>").selectedIndex==0)
				{
					document.getElementById("<%=cbocol21.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=txttab21.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbocol22.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=txttab22.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbocol23.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=txttab23.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbocol24.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=txttab24.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbocol25.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=txttab25.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbocol26.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=txttab26.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbocol27.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=txttab27.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=cbocol28.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=txttab28.ClientId %>").style.visibility = "hidden";
					/////////////////////////////
					document.getElementById("<%=divcon2.ClientId %>").style.visibility = "hidden";
					document.getElementById("<%=txthead.ClientId %>").style.visibility = "hidden";
				}
				else
				{
			
				    if(document.getElementById("<%=cbotab2.ClientID %>").selectedIndex != 0)
                        {
                        document.getElementById("<%=hidQueryString1.ClientID %>").value ="";
                        window.arrQueryString[3]= document.getElementById("<%=cbotab2.ClientID %>").value;
                       // alert(window.arrQueryString[3])
                      }  
                    else
                     {
                         window.arrQueryString[3]="NULL";    
                      }
                        for(i=0;i<4;i++)
	                       {
	                       //alert(document.getElementById("<%=hidQueryString1.ClientID %>").value)
                            
                         document.getElementById("<%=hidQueryString1.ClientID %>").value += window.arrQueryString[i] + "#";
                            }
				
				
					document.getElementById("<%=cbocol21.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol22.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol23.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol24.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol25.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol26.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol27.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocol28.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolB1.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolB2.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolB3.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolB4.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolB5.ClientId %>").options[0]=new Option("--Select--");
					for(i=0;i<arrcols.length;i++)
					{
						document.getElementById("<%=lsttab2cols.ClientId %>").options[i]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol21.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol22.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol23.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol24.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol25.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol26.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol27.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocol28.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolB1.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolB2.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolB3.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolB4.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolB5.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
					}
					document.getElementById("<%=txttab21.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab21.ClientId %>").value = document.getElementById("<%=cbotab2.ClientId %>").item(document.getElementById("<%=cbotab2.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol21.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab22.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab22.ClientId %>").value = document.getElementById("<%=cbotab2.ClientId %>").item(document.getElementById("<%=cbotab2.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol22.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab23.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab23.ClientId %>").value = document.getElementById("<%=cbotab2.ClientId %>").item(document.getElementById("<%=cbotab2.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol23.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab24.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab24.ClientId %>").value = document.getElementById("<%=cbotab2.ClientId %>").item(document.getElementById("<%=cbotab2.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol24.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab25.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab25.ClientId %>").value = document.getElementById("<%=cbotab2.ClientId %>").item(document.getElementById("<%=cbotab2.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol25.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab26.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab26.ClientId %>").value = document.getElementById("<%=cbotab2.ClientId %>").item(document.getElementById("<%=cbotab2.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol26.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab27.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab27.ClientId %>").value = document.getElementById("<%=cbotab2.ClientId %>").item(document.getElementById("<%=cbotab2.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol27.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab28.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab28.ClientId %>").value = document.getElementById("<%=cbotab2.ClientId %>").item(document.getElementById("<%=cbotab2.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol28.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txthead.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txtcon21.ClientId %>").value = "and    " + document.getElementById("<%=cbotab2.ClientId %>").item(document.getElementById("<%=cbotab2.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=txtcon22.ClientId %>").value = "and    " + document.getElementById("<%=cbotab2.ClientId %>").item(document.getElementById("<%=cbotab2.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=txtcon23.ClientId %>").value = "and    " + document.getElementById("<%=cbotab2.ClientId %>").item(document.getElementById("<%=cbotab2.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=txtcon24.ClientId %>").value = "and    " + document.getElementById("<%=cbotab2.ClientId %>").item(document.getElementById("<%=cbotab2.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=txtcon25.ClientId %>").value = "and    " + document.getElementById("<%=cbotab2.ClientId %>").item(document.getElementById("<%=cbotab2.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=divcon2.ClientId %>").style.visibility = "visible";
				}
				
			}
			
//			function __doPostBack(eventTarget, eventArgument)
//			{
//				var theform;
//				if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
//					theform = document.forms[0];
//				}
//				else {
//					theform = document.forms["forms[0]"];
//				}
//				theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
//				theform.__EVENTARGUMENT.value = eventArgument;
//				theform.submit();
//			} 
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
			
			// Finding First table Columns
			function getlsttab1colsValue()
			{
			  
			    var lsttab1cols = document.getElementById("<%=lsttab1cols.ClientID %>")
			    document.getElementById("<%=hfFirstTabQueryString.ClientID %>").value = "";
			    for(i=0;i<lsttab1cols.options.length;i++)
			    {
			        if(lsttab1cols.options[i].selected == true)
			            document.getElementById("<%=hfFirstTabQueryString.ClientID %>").value += lsttab1cols.options[i].value + "#";
			    }
			    //alert(document.getElementById("<%=hfFirstTabQueryString.ClientID %>").value);
			}
			
			//// Finding Second table Columns
			function gettab2colsValue()
			{
		
			var lsttab2cols=document.getElementById("<%=lsttab2cols.ClientId %>")
			document.getElementById("<%=hfSecondTabQueryString.ClientId %>").value="";
			for(i=0;i<lsttab2cols.options.length;i++)  
			{
			   if(lsttab2cols.options[i].selected==true)
			   document.getElementById("<%=hfSecondTabQueryString.ClientId %>").value += lsttab2cols.options[i].value + "#";
			   
			}
			//alert(document.getElementById("<%=hfSecondTabQueryString.ClientId %>").value )
			}
			
			
			function chkvalidation()
			{	
			
				
				
//				if(document.getElementById("<%=cbodept.ClientId %>").selectedIndex==0)
//				{
//					alert("Please select department for table A")
//					return false;
//				}
//				else if(document.getElementById("<%=cbodept1.ClientId %>").selectedIndex==0)
//				{
//					alert("Please select department for table B")
//					return false;
//				}
				/*else if(document.getElementById("cboclient").selectedIndex==0 || document.getElementById("cboclient").selectedIndex==-1)
				{
					alert("Please select client")
				}
				else if(document.getElementById("cbolob").selectedIndex==0 || document.getElementById("cbolob").selectedIndex==-1)
				{
					alert("Please select lob")
				}*/
			 if(((document.getElementById("<%=cbotab1.ClientId %>").selectedIndex==0) && (document.getElementById("<%=cbotab2.ClientId %>").selectedIndex==0)) || ((document.getElementById("<%=cbotab1.ClientId %>").selectedIndex==-1) && (document.getElementById("<%=cbotab2.ClientId %>").selectedIndex==-1)))
				{
					alert("Please select Table A Or Table B")
					return false;
				}
				else if((document.getElementById("<%=lsttab1cols.ClientId %>").selectedIndex==-1) && (document.getElementById("<%=lsttab2cols.ClientId %>").selectedIndex==-1))
				{
					alert("Please select Columns")
					return false;
				}
				else if(document.getElementById("<%=cbocol11.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbocol21.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbojoin1.ClientId %>").selectedIndex==0)
				{
					alert("Please select the columns and join in where clause to join the tables.")
				return false;
				}
				else if(document.getElementById("<%=cbocol11.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbocol21.ClientId %>").selectedIndex!=0)
				{
					alert("Please select column from table A in where clause")
					return false;
				}
				else if(document.getElementById("<%=cbocol12.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbocol22.ClientId %>").selectedIndex!=0)
				{
					alert("Please select column from table A in where clause")
					return false;
				}
				else if(document.getElementById("<%=cbocol13.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbocol23.ClientId %>").selectedIndex!=0)
				{
					alert("Please select column from table A in where clause")
					return false;
				}
				else if(document.getElementById("<%=cbocol11.ClientId %>").selectedIndex!=0 && document.getElementById("<%=cbocol21.ClientId %>").selectedIndex==0)
				{
					alert("Please select column from table B in where clause")
					return false;
				}
				else if(document.getElementById("<%=cbocol12.ClientId %>").selectedIndex!=0 && document.getElementById("<%=cbocol22.ClientId %>").selectedIndex==0)
				{
					alert("Please select column from table B in where clause")
					return false;
				}
				else if(document.getElementById("<%=cbocol13.ClientId %>").selectedIndex!=0 && document.getElementById("<%=cbocol23.ClientId %>").selectedIndex==0)
				{
					alert("Please select column from table B in where clause")
					return false;
				}
//				else if((document.getElementById("<%=cbodept2.ClientId %>").selectedIndex==0) && (document.getElementById("<%=cboclient2.Clientid %>").selectedIndex==0 || document.getElementById("<%=cboclient2.Clientid %>").selectedIndex==-1) && (document.getElementById("<%=cbolob2.ClientId %>").selectedIndex==0 || document.getElementById("<%=cbolob2.ClientId %>").selectedIndex==-1))
//				{
//					alert("Please select department or client or lob under which you have to save the table.")
//					return false;
//				}
				else if(blank(document.getElementById("<%=txtname.ClientId %>").value))
				{
					alert("Please fill table name.")
					return false;
				}
				
				
				// Fuction for Finding Selected Value For Join
				
				var jointab1string;
				var jointab2string;
				
				
			            jointab1string =document.getElementById("<%=cbocol11.ClientId %>").value; 
			            jointab1string	=jointab1string	+ "#"+ document.getElementById("<%=cbocol12.ClientId %>").value
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocol13.ClientId %>").value;
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocol14.ClientId %>").value;
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocol15.ClientId %>").value;
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocol16.ClientId %>").value;
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocol17.ClientId %>").value;
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocol18.ClientId %>").value;
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocolA1.ClientId %>").value;
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocolA2.ClientId %>").value;
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocolA3.ClientId %>").value;
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocolA4.ClientId %>").value;
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocolA5.ClientId %>").value;
				
				document.getElementById("<%=hfJoinTab1QueryString.ClientId %>").value = jointab1string;
				
				jointab2string=document.getElementById("<%=cbocol21.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocol22.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocol23.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocol24.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocol25.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocol26.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocol27.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocol28.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocolB1.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocolB2.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocolB3.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocolB4.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocolB5.ClientId %>").value;
				
				
				document.getElementById("<%=hfJoinTab2QueryString.ClientId %>").value = jointab2string;
				
				
			//spans of  tables
			var spana,spanb,spanc;
			
			spana=60
			//document.getElementById("<%=cbodept.ClientId %>").item(document.getElementById("<%=cbodept.ClientId %>").selectedIndex).text
			spana= spana +"#"+0
			spana= spana +"#"+0
			document.getElementById("<%=hdSpanA.ClientId %>").value=spana
			
			
			spanb=60
			spanb= spanb +"#"+0
			spanb= spanb +"#"+0
			document.getElementById("<%=hdSpanB.ClientId %>").value=spanb
			
			
			
			spanc=60
			spanc= spanc +"#"+0
			spanc= spanc +"#"+0
			document.getElementById("<%=hdSpan.ClientId %>").value=spanc
				}  
				 
		function chkvalidation2()
			{	
			
			if(document.getElementById("<%=cbodept.ClientId %>").selectedIndex==0)
				{
					alert("Please select department for table A")
					return false;
				}
				else if(document.getElementById("<%=cbodept1.ClientId %>").selectedIndex==0)
				{
					alert("Please select department for table B")
					return false;
				}
				/*else if(document.getElementById("cboclient").selectedIndex==0 || document.getElementById("cboclient").selectedIndex==-1)
				{
					alert("Please select client")
				}
				else if(document.getElementById("cbolob").selectedIndex==0 || document.getElementById("cbolob").selectedIndex==-1)
				{
					alert("Please select lob")
				}*/
				else if(((document.getElementById("<%=cbotab1.ClientId %>").selectedIndex==0) && (document.getElementById("<%=cbotab2.ClientId %>").selectedIndex==0)) || ((document.getElementById("<%=cbotab1.ClientId %>").selectedIndex==-1) && (document.getElementById("<%=cbotab2.ClientId %>").selectedIndex==-1)))
				{
					alert("Please select Table A Or Table B")
					return false;
				}
				else if((document.getElementById("<%=lsttab1cols.ClientId %>").selectedIndex==-1) && (document.getElementById("<%=lsttab2cols.ClientId %>").selectedIndex==-1))
				{
					alert("Please select Columns")
					return false;
				}
				else if(document.getElementById("<%=cbocol11.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbocol21.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbojoin1.ClientId %>").selectedIndex==0)
				{
					alert("Please select the columns and join in where clause to join the tables.")
				return false;
				}
				else if(document.getElementById("<%=cbocol11.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbocol21.ClientId %>").selectedIndex!=0)
				{
					alert("Please select column from table A in where clause")
					return false;
				}
				else if(document.getElementById("<%=cbocol12.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbocol22.ClientId %>").selectedIndex!=0)
				{
					alert("Please select column from table A in where clause")
					return false;
				}
				else if(document.getElementById("<%=cbocol13.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbocol23.ClientId %>").selectedIndex!=0)
				{
					alert("Please select column from table A in where clause")
					return false;
				}
				else if(document.getElementById("<%=cbocol11.ClientId %>").selectedIndex!=0 && document.getElementById("<%=cbocol21.ClientId %>").selectedIndex==0)
				{
					alert("Please select column from table B in where clause")
					return false;
				}
				else if(document.getElementById("<%=cbocol12.ClientId %>").selectedIndex!=0 && document.getElementById("<%=cbocol22.ClientId %>").selectedIndex==0)
				{
					alert("Please select column from table B in where clause")
					return false;
				}
				else if(document.getElementById("<%=cbocol13.ClientId %>").selectedIndex!=0 && document.getElementById("<%=cbocol23.ClientId %>").selectedIndex==0)
				{
					alert("Please select column from table B in where clause")
					return false;
				}
				else if((document.getElementById("<%=cbodept2.ClientId %>").selectedIndex==0) && (document.getElementById("<%=cboclient2.Clientid %>").selectedIndex==0 || document.getElementById("<%=cboclient2.Clientid %>").selectedIndex==-1) && (document.getElementById("<%=cbolob2.ClientId %>").selectedIndex==0 || document.getElementById("<%=cbolob2.ClientId %>").selectedIndex==-1))
				{
					alert("Please select department or client or lob under which you have to save the table.")
					return false;
				}
				else if(blank(document.getElementById("<%=txtname.ClientId %>").value))
				{
					alert("Please fill table name.")
					return false;
				}
				
				
				// Fuction for Finding Selected Value For Join
				
				var jointab1string;
				var jointab2string;
				
				
			            jointab1string =document.getElementById("<%=cbocol11.ClientId %>").value; 
			            jointab1string	=jointab1string	+ "#"+ document.getElementById("<%=cbocol12.ClientId %>").value
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocol13.ClientId %>").value;
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocol14.ClientId %>").value;
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocol15.ClientId %>").value;
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocol16.ClientId %>").value;
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocol17.ClientId %>").value;
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocol18.ClientId %>").value;
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocolA1.ClientId %>").value;
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocolA2.ClientId %>").value;
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocolA3.ClientId %>").value;
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocolA4.ClientId %>").value;
						jointab1string	=jointab1string	+ "#"+document.getElementById("<%=cbocolA5.ClientId %>").value;
				
				document.getElementById("<%=hfJoinTab1QueryString.ClientId %>").value = jointab1string;
				
				jointab2string=document.getElementById("<%=cbocol21.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocol22.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocol23.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocol24.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocol25.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocol26.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocol27.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocol28.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocolB1.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocolB2.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocolB3.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocolB4.ClientId %>").value;
				jointab2string =jointab2string + "#" + document.getElementById("<%=cbocolB5.ClientId %>").value;
				
				
				document.getElementById("<%=hfJoinTab2QueryString.ClientId %>").value = jointab2string;
				
				
			//spans of  tables
			var spana,spanb,spanc;
			
			spana=document.getElementById("<%=cbodept.ClientId %>").value
			//document.getElementById("<%=cbodept.ClientId %>").item(document.getElementById("<%=cbodept.ClientId %>").selectedIndex).text
			spana= spana +"#"+document.getElementById("<%=cboclient.ClientId %>").value
			spana= spana +"#"+document.getElementById("<%=cbolob.ClientId %>").value
			document.getElementById("<%=hdSpanA.ClientId %>").value=spana
			
			
			spanb=document.getElementById("<%=cbodept1.ClientId %>").value
			spanb= spanb +"#"+document.getElementById("<%=cboclient1.ClientId %>").value
			spanb= spanb +"#"+document.getElementById("<%=cbolob1.ClientId %>").value
			document.getElementById("<%=hdSpanB.ClientId %>").value=spanb
			
			
			
			spanc=document.getElementById("<%=cbodept2.ClientId %>").value
			spanc= spanc +"#"+document.getElementById("<%=cboclient2.ClientId %>").value
			spanc= spanc +"#"+document.getElementById("<%=cbolob2.ClientId %>").value
			document.getElementById("<%=hdSpan.ClientId %>").value=spanc
				} 
	
			function ShowCalendar1()
			{
				document.getElementById("<%=txtval11.ClientId %>").value = window.showModalDialog('/AutoWhiz/Calendar/Calendar.htm',document.getElementById("<%=txtval11.ClientId %>").value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			}
			function ShowCalendar2()
			{
				document.getElementById("<%=txtval12.ClientId %>").value = window.showModalDialog('/AutoWhiz/Calendar/Calendar.htm',document.getElementById("<%=txtval12.ClientId %>").value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			}
			function ShowCalendar3()
			{
				document.getElementById("<%=txtval13.ClientId %>").value = window.showModalDialog('/AutoWhiz/Calendar/Calendar.htm',document.getElementById("<%=txtval13.ClientId %>").value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			}
			function ShowCalendar4()
			{
				document.getElementById("<%=txtval14.ClientId %>").value = window.showModalDialog('/AutoWhiz/Calendar/Calendar.htm',document.getElementById("<%=txtval14.ClientId %>").value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			}
			function ShowCalendar5()
			{
				document.getElementById("<%=txtval15.ClientId %>").value = window.showModalDialog('/AutoWhiz/Calendar/Calendar.htm',document.getElementById("<%=txtval15.ClientId %>").value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			}
			function ShowCalendar6()
			{
				document.getElementById("<%=txtval21.Clientid %>").value = window.showModalDialog('/AutoWhiz/Calendar/Calendar.htm',document.getElementById("<%=txtval21.Clientid %>").value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			}
			function ShowCalendar7()
			{
				document.getElementById("<%=txtval22.Clientid %>").value = window.showModalDialog('/AutoWhiz/Calendar/Calendar.htm',document.getElementById("<%=txtval22.Clientid %>").value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			}
			function ShowCalendar8()
			{
				document.getElementById("<%=txtval23.Clientid %>").value = window.showModalDialog('/Autowhiz/Calendar/Calendar.htm',document.getElementById("<%=txtval23.Clientid %>").value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			}
			function ShowCalendar9()
			{
				document.getElementById("<%=txtval24.Clientid %>").value = window.showModalDialog('/AutoWhiz/Calendar/Calendar.htm',document.getElementById("<%=txtval24.Clientid %>").value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			}
			function ShowCalendar10()
			{
				document.getElementById("<%=txtval25.Clientid %>").value = window.showModalDialog('/AutoWhiz/Calendar/Calendar.htm',document.getElementById("<%=txtval25.Clientid %>").value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
			}
			
			
			


</script>

    &nbsp;<input type="hidden" name="__EVENTTARGET"/><input type="hidden" name="__EVENTARGUMENT"/>
			<table width="90%" align="center" class ="table" summary ="table">
				<caption style ="background-color:#67A897">Merge tables</caption>
				<%--<tr>
					<th style="HEIGHT: 19px" colspan="2" >
						&nbsp;
					</th>
				</tr>--%>
				<tr>
                    <td colspan="6" scope="colgroup" >
                        &nbsp;</td>
				</tr>
				<tr>
                    <td colspan="3" scope ="colgroup" >
						<table width="70%" summary ="select the span" runat="server" id="spandisplay1" visible="false">
							<tr>
								<td class="label" scope ="col" style ="color:black" ><strong><asp:Label ID="lbl1" Text="Level 1" runat="server" ></asp:Label></strong></td>
								<td scope ="col" class="style1">
					
									<asp:dropdownlist id="cbodept" tabindex="1"  Runat="server" 
                                        CssClass="dropdwonlist" Width="144px" ></asp:dropdownlist>
								</td>
							</tr>
							<tr>
                               <td class="label" scope ="col" style ="color:black"><strong><asp:Label ID="lbl2" Text="Level 2" runat="server" ></asp:Label> </strong></td>
								<td scope ="col" class="style1">
						              <asp:dropdownlist id="cboclient"  tabindex="2" Runat="server" CssClass="dropdwonlist" Width="144px"></asp:dropdownlist>
								</td>
							</tr>
							<tr>
                               <td class="label" scope ="col" style ="color:black"><strong><asp:Label ID="lbl3" Text="Level 3" runat="server" ></asp:Label> </strong></td>
								<td scope ="col" class="style1"><asp:dropdownlist id="cbolob"  tabindex="3" Runat="server" CssClass="dropdwonlist" Width="144px"></asp:dropdownlist>
                                </td> 
                             </tr>
                             </table>
                             </td>
                             <td><input class="button" id="gettable" title="Gettable" visible="false" style="WIDTH: 100px; HEIGHT: 21px" onclick="javascript:gettab();gettab1();"
										tabindex="6" type="button" runat="server" value="Gettable" name="GetTable"/></td>
                               <td colspan="3" scope ="colgroup" >
						<table width="100%" summary ="Select The span" runat="server" visible="false" id="spandisplay2" >
							<tr>
								<td class="label" scope="col" style ="color:black" ><strong><asp:Label ID="lbl4" Text="Level 1" runat="server" ></asp:Label> </strong></td>
								<td scope="col" >
									<asp:dropdownlist id="cbodept1" tabindex="1" Runat="server" CssClass="dropdwonlist" Width="144px"></asp:dropdownlist>
								</td>
							</tr>
							<tr>
								<td class="label" scope="col" style ="color:black" ><strong><asp:Label ID="lbl5" Text="Level 2" runat="server" ></asp:Label> </strong></td>
								<td scope ="col">
									<asp:dropdownlist id="cboclient1" tabindex="2" Runat="server" CssClass="dropdwonlist" Width="144px"></asp:dropdownlist>
								</td>
							</tr>
							<tr>
								<td class="label" scope ="col" style ="color:black"><strong><asp:Label ID="lbl6" Text="Level 3" runat="server" ></asp:Label> </strong></td>
								<td scope ="col">
									<asp:dropdownlist id="cbolob1"  tabindex="3" Runat="server" CssClass="dropdwonlist" Width="144px"></asp:dropdownlist>
								</td>
							</tr>
						</table>
					</td>
							<tr>
								<td style="WIDTH: 30%; HEIGHT: 18px; color :Black" class="label" scope ="col" >
								<label for="ctl00_MainPlaceHolder_cbotab1">Table A:</label></td>
								<td scope ="col">
									<asp:dropdownlist id="cbotab1" tabindex="4" Runat="server" CssClass="dropdwonlist" Width="144px"></asp:dropdownlist>
								</td>
                                <td></td>   
                                <td style="HEIGHT: 18px; color :Black" class="label" scope ="col"><label for="ctl00_MainPlaceHolder_cbotab2">Table B:</label></td>
								<td scope ="col">
									<asp:dropdownlist id="cbotab2" tabindex="5" Runat="server" CssClass="dropdwonlist" Width="144px"></asp:dropdownlist>
								</td>
							</tr>
						
					<tr>
                    <td colspan="6" style="height: 18px" scope ="colgroup" ></td>
				</tr>
				<tr>
                    <td align="center" colspan="3" style ="color:black" scope ="col"><label for="ctl00_MainPlaceHolder_lsttab1cols"><strong>Columns of Table A</strong></label><br/>
						<asp:listbox id="lsttab1cols" tabindex="6" Runat="server" Height="141px" SelectionMode="Multiple"
							Width="216px"  CssClass ="listBox"></asp:listbox></td>
                    <td align="center" colspan="3" scope ="col" style ="color:black"><label for="ctl00_MainPlaceHolder_lsttab2cols"><strong>Columns of Table </strong></label><br/>
						<asp:listbox id="lsttab2cols" tabindex="7" Runat="server" Height="141px" SelectionMode="Multiple"
							Width="216px" CssClass ="listBox"></asp:listbox></td>
				</tr>
				<tr>
                    <td align="right" colspan="1" scope ="colgroup" >
<label for="ctl00_MainPlaceHolder_txttab11"></label>
					<input id="txttab11" style="VISIBILITY: hidden; WIDTH: 192px; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 18px;  TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
							readonly="readonly"  type="text" size="26" name="txttab11" runat="server"/>
							</td>
                    <td colspan="1" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_cbocol11"></label>
							<asp:dropdownlist id="cbocol11" style="VISIBILITY: hidden" tabindex="8" Runat="server" EnableViewState="true"></asp:dropdownlist> 
							</td>
                    <td colspan="1" scope ="colgroup" style ="color:black">
<label for="ctl00_MainPlaceHolder_cbojoin1"></label>	
						<asp:DropDownList ID="cbojoin1" style="VISIBILITY: hidden" Runat="server" CssClass="dropdwonlist">
							<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
							<asp:ListItem Value="=">=</asp:ListItem>
							<asp:ListItem Value="*=">*=</asp:ListItem>
							<asp:ListItem Value="=*">=*</asp:ListItem>
							<asp:ListItem Value="<%=str %>">>=</asp:ListItem>
							<asp:ListItem Value="<="><=</asp:ListItem>
						</asp:DropDownList>
					</td>
                    <td colspan="3" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_txttab21"></label>
					<input id="txttab21" style="VISIBILITY: hidden; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none;  TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
							readonly ="readOnly" type="text" size="32" name="txttab21" runat="server"/>
							<label for="ctl00_MainPlaceHolder_cbocol21"></label>
							<asp:dropdownlist id="cbocol21" style="VISIBILITY: hidden" tabindex="9" Runat="server" CssClass="dropdwonlist" EnableViewState="true"></asp:dropdownlist>
					</td>
				</tr>
				<tr>
                    <td align="right" colspan="1" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_txttab12"></label>

					<input id="txttab12" style="VISIBILITY: hidden; WIDTH: 192px; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 18px;  TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
							readonly="readonly" type="text" size="26" name="txttab12" runat="server"/>
							</td>
                    <td colspan="1" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_cbocol12"></label>
                        <asp:dropdownlist id="cbocol12" style="VISIBILITY: hidden" tabindex="10" Runat="server" CssClass="dropdwonlist"></asp:dropdownlist> 
                        </td>
                    <td colspan="1" scope ="colgroup" style ="color:black">
<label for="ctl00_MainPlaceHolder_cbojoin2"></label>

                        <asp:DropDownList ID="cbojoin2" style="VISIBILITY: hidden" Runat="server">
							<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
							<asp:ListItem Value="=">=</asp:ListItem>
							<asp:ListItem Value="*=">*=</asp:ListItem>
							<asp:ListItem Value="=*">=*</asp:ListItem>
							<asp:ListItem Value="<%=str %>">>=</asp:ListItem>
							<asp:ListItem Value="<="><=</asp:ListItem>
						</asp:DropDownList>
					</td>
                    <td colspan="3" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_txttab22"></label>

					<input id="txttab22" style="VISIBILITY: hidden; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none;  TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
							readonly="readonly" type="text" size="32" name="txttab22" runat="server"/>
							<label for="ctl00_MainPlaceHolder_cbocol22"></label>
							<asp:dropdownlist id="cbocol22" style="VISIBILITY: hidden" tabindex="11" Runat="server" CssClass="dropdwonlist"></asp:dropdownlist>
					</td>
				</tr>
				<tr>
                    <td align="right" colspan="1" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_txttab13"></label>

					<input id="txttab13" style="VISIBILITY: hidden; WIDTH: 192px; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 18px;  TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
							readonly="readonly" type="text" size="26" name="txttab13" runat="server"/>
							</td>
                    <td colspan="1" scope ="colgroup">	
<label for="ctl00_MainPlaceHolder_cbocol13"></label>
						
							<asp:dropdownlist id="cbocol13" style="VISIBILITY: hidden" tabindex="12" Runat="server" CssClass="dropdwonlist"></asp:dropdownlist> 
							</td>
                    <td colspan="1" scope ="colgroup" style ="color:black">
<label for="ctl00_MainPlaceHolder_cbojoin3"></label>

                        <asp:DropDownList ID="cbojoin3" style="VISIBILITY: hidden" Runat="server">
							<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
							<asp:ListItem Value="=">=</asp:ListItem>
							<asp:ListItem Value="*=">*=</asp:ListItem>
							<asp:ListItem Value="=*">=*</asp:ListItem>
							<asp:ListItem Value="<%=str %>">>=</asp:ListItem>
							<asp:ListItem Value="<="><=</asp:ListItem>
						</asp:DropDownList>
					</td>
                    <td colspan="3" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_txttab23"></label>

					<input id="txttab23" style="VISIBILITY: hidden; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none;  TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
							readonly="readonly" type="text" size="32" name="txttab23" runat="server"/>
							<label for="ctl00_MainPlaceHolder_cbocol23"></label>
							<asp:dropdownlist id="cbocol23" style="VISIBILITY: hidden" tabindex="13" Runat="server" CssClass="dropdwonlist"></asp:dropdownlist>
					</td>
				</tr>
				<tr>
                    <td align="right" colspan="1" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_txttab14"></label>
					<input id="txttab14" style="VISIBILITY: hidden; WIDTH: 192px; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 18px;  TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
							readonly="readonly" type="text" size="26" name="txttab14" runat="server"/>
							</td>
                    <td colspan="1" scope ="colgroup">	
<label for="ctl00_MainPlaceHolder_cbocol14"></label>
						
							<asp:dropdownlist id="cbocol14" style="VISIBILITY: hidden" tabindex="12" Runat="server" CssClass="dropdwonlist"></asp:dropdownlist>
							</td>
                    <td colspan="1" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_cbojoin4"></label>

                        <asp:DropDownList ID="cbojoin4" style="VISIBILITY: hidden" Runat="server">
							<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
							<asp:ListItem Value="=">=</asp:ListItem>
							<asp:ListItem Value="*=">*=</asp:ListItem>
							<asp:ListItem Value="=*">=*</asp:ListItem>
							<asp:ListItem Value="<%=str %>">>=</asp:ListItem>
							<asp:ListItem Value="<="><=</asp:ListItem>
						</asp:DropDownList>
					</td>
                    <td colspan="3" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_txttab24"></label>

					<input id="txttab24" style="VISIBILITY: hidden; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none;  TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
							readonly="readonly" type="text" size="32" name="txttab24" runat="server"/>
							<label for="ctl00_MainPlaceHolder_cbocol24"></label>
							<asp:dropdownlist id="cbocol24" style="VISIBILITY: hidden" tabindex="13" Runat="server"/>
					</td>
				</tr>
				<tr>
                    <td align="right" colspan="1" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_txttab15"></label>

                        <input id="txttab15" style="VISIBILITY: hidden; WIDTH: 192px; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 18px;  TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
							readonly="readonly" type="text" size="26" name="txttab15" runat="server"/>
							</td>
                    <td colspan="1" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_cbocol15"></label>

                        <asp:dropdownlist id="cbocol15" style="VISIBILITY: hidden" tabindex="12" Runat="server"/> 
                        </td>
                    <td colspan="1" scope ="colgroup" style ="color:black"> 
                    <label for="ctl00_MainPlaceHolder_cbojoin5"></label>
                       
						<asp:DropDownList ID="cbojoin5" style="VISIBILITY: hidden" Runat="server">
							<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
							<asp:ListItem Value="=">=</asp:ListItem>
							<asp:ListItem Value="*=">*=</asp:ListItem>
							<asp:ListItem Value="=*">=*</asp:ListItem>
							<asp:ListItem Value="<%=str %>">>=</asp:ListItem>
							<asp:ListItem Value="<="><=</asp:ListItem>
						</asp:DropDownList>
					</td>					
                    <td colspan="3" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_txttab25"></label>

					<input id="txttab25" style="VISIBILITY: hidden; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none;  TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
							readonly="readonly" type="text" size="32" name="txttab25" runat="server"/>
							 <label for="ctl00_MainPlaceHolder_cbocol25"></label>
							<asp:dropdownlist id="cbocol25" style="VISIBILITY: hidden" tabindex="13" Runat="server"></asp:dropdownlist>
					</td>
				</tr>
				<tr>
                    <td align="right" colspan="1" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_txttab16"></label>

					<input id="txttab16" style="VISIBILITY: hidden; WIDTH: 192px; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 18px;  TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
							readonly="readonly" type="text" size="26" name="txttab16" runat="server"/>
							</td>
                    <td colspan="1" scope ="colgroup">	
<label for="ctl00_MainPlaceHolder_cbocol16"></label>

							<asp:dropdownlist id="cbocol16" style="VISIBILITY: hidden" tabindex="12" Runat="server"/>
							</td>
                    <td colspan="1" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_cbojoin6"></label>
                        <asp:DropDownList ID="cbojoin6" style="VISIBILITY: hidden" Runat="server">
							<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
							<asp:ListItem Value="=">=</asp:ListItem>
							<asp:ListItem Value="*=">*=</asp:ListItem>
							<asp:ListItem Value="=*">=*</asp:ListItem>
							<asp:ListItem Value="<%=str %>">>=</asp:ListItem>
							<asp:ListItem Value="<="><=</asp:ListItem>
						</asp:DropDownList>
					</td>
                    <td colspan="3" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_txttab26"></label>

						<input id="txttab26" readonly="readonly" style="VISIBILITY: hidden;BORDER-TOP-STYLE: none;BORDER-RIGHT-STYLE: none;BORDER-LEFT-STYLE: none;TEXT-ALIGN: right;BORDER-BOTTOM-STYLE: none"
							type="text" size="32" name="txttab26" runat="server"/>
						 <label for="ctl00_MainPlaceHolder_cbocol26"></label>	
							<asp:dropdownlist id="cbocol26" style="VISIBILITY: hidden" Runat="server" tabindex="13"></asp:dropdownlist>
					</td>
				</tr>
				<tr>
                    <td align="right" colspan="1" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_txttab17"></label>


						<input id="txttab17" readonly="readonly" style="VISIBILITY: hidden;WIDTH: 192px;BORDER-TOP-STYLE: none;BORDER-RIGHT-STYLE: none;BORDER-LEFT-STYLE: none;HEIGHT: 18px;TEXT-ALIGN: right;BORDER-BOTTOM-STYLE: none"
							type="text" size="26" name="txttab17" runat="server"/>
							</td> 
                        <td colspan="1" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_cbocol17"></label>

							<asp:dropdownlist id="cbocol17" style="VISIBILITY: hidden" Runat="server" tabindex="12"></asp:dropdownlist>
							</td>
                        <td colspan="1" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_cbojoin7"></label>


						<asp:DropDownList style="VISIBILITY: hidden" ID="cbojoin7" Runat="server">
							<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
							<asp:ListItem Value="=">=</asp:ListItem>
							<asp:ListItem Value="*=">*=</asp:ListItem>
							<asp:ListItem Value="=*">=*</asp:ListItem>
							<asp:ListItem Value="<%=str %>">>=</asp:ListItem>   
							<asp:ListItem Value="<="><=</asp:ListItem>
						</asp:DropDownList>
					</td>
                    <td colspan="3" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_txttab27"></label>

						<input id="txttab27" readonly="readonly" style="VISIBILITY: hidden;BORDER-TOP-STYLE: none;BORDER-RIGHT-STYLE: none;BORDER-LEFT-STYLE: none;TEXT-ALIGN: right;BORDER-BOTTOM-STYLE: none"
							type="text" size="32" name="txttab27" runat="server"/>
							 <label for="ctl00_MainPlaceHolder_cbocol27"></label>
							<asp:dropdownlist id="cbocol27" style="VISIBILITY: hidden" Runat="server" tabindex="13"/>
					</td>
				</tr>
				<tr>
                    <td align="right" colspan="1" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_txttab18"></label>

						<input id="txttab18" readonly="readonly" style="VISIBILITY: hidden;WIDTH: 192px;BORDER-TOP-STYLE: none;BORDER-RIGHT-STYLE: none;BORDER-LEFT-STYLE: none;HEIGHT: 18px;TEXT-ALIGN: right;BORDER-BOTTOM-STYLE: none"
							type="text" size="26" name="txttab18" runat="server"/>
							</td>
                    <td colspan="1" scope ="colgroup">
							<label for="ctl00_MainPlaceHolder_cbocol18"></label>					
							<asp:dropdownlist id="cbocol18" style="VISIBILITY: hidden" Runat="server" tabindex="12"></asp:dropdownlist>
							</td>
                    <td colspan="1" scope ="colgroup">
							<label for="ctl00_MainPlaceHolder_cbojoin8"></label>						
							<asp:DropDownList style="VISIBILITY: hidden" ID="cbojoin8" Runat="server">
							<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
							<asp:ListItem Value="=">=</asp:ListItem>
							<asp:ListItem Value="*=">*=</asp:ListItem>
							<asp:ListItem Value="=*">=*</asp:ListItem>
							<asp:ListItem Value="<%=str %>">>=</asp:ListItem>
							<asp:ListItem Value="<="><=</asp:ListItem>
						</asp:DropDownList>
					</td>
                    <td colspan="3" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_txttab28"></label>
						<input id="txttab28" readonly="readonly" style="VISIBILITY: hidden;BORDER-TOP-STYLE: none;BORDER-RIGHT-STYLE: none;BORDER-LEFT-STYLE: none;HEIGHT: 18px;TEXT-ALIGN: right;BORDER-BOTTOM-STYLE: none"
							type="text" size="32" name="txttab28" runat="server"/>
							<label for="ctl00_MainPlaceHolder_cbocol28"></label>
							<asp:dropdownlist id="cbocol28" style="VISIBILITY: hidden" Runat="server" tabindex="13"></asp:dropdownlist>
					</td>
				</tr>
				<tr>
                    <td colspan="6" style="height: 132px" scope ="colgroup">
						<div id="divdesc" style="Visibility:hidden;" runat="server">
							<table width="100%" summary ="Description of Symbols">
								<tr>
									<td colspan="2" scope ="colgroup">&nbsp;<font color="navy"><strong>Description of symbols to join the tables:</strong></font></td>
								</tr>
								<tr>
									<td width="8%" scope ="col" style ="color:black">&nbsp;
										<font color="red">" = "</font></td><td><font color="navy">: is used to include 
											only rows where the joined fields from both tables are equal.</font>
									</td>
								</tr>
								<tr>
									<td scope="col" style ="color:black" >&nbsp;
										<font color="red">" *= "</font></td><td><font color="navy">: is used to include all records 
											from 'First' table and only those records from 'Second' table where the joined 
											fields from both tables are equal.</font>
									</td>
								</tr>
								<tr>
									<td scope="col" style ="color:black" >&nbsp;
										<font color="red">" =* "</font></td><td><font color="navy">: is used to include all records 
											from 'Second' table and only those records from 'First' table where the joined 
											fields from both tables are equal.</font>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<tr>
                    <td colspan="6" style="height: 30px" scope="colgroup" >
<label for="ctl00_MainPlaceHolder_txthead"></label>

					<input id="txthead" style="VISIBILITY: hidden;BORDER-TOP-STYLE: none;BORDER-RIGHT-STYLE: none;BORDER-LEFT-STYLE: none;HEIGHT: 18px;BORDER-BOTTOM-STYLE: none"
							type="text"   name="txthead" runat="server" value="Conditions:"  class ="label" />
							</td>
				</tr>
				<tr>
                    <td colspan="6" scope ="colgroup" >
						<div id="divcon1" runat="server" style="VISIBILITY:hidden;WIDTH:100%">
							<table width="100%" summary ="table">
								<tr>
									<td colspan="2" scope ="colgroup" style ="color:black">
									<label for="ctl00_MainPlaceHolder_txtcon11"></label>
										<input id="txtcon11" readonly="readonly" style="BORDER-TOP-STYLE: none;BORDER-RIGHT-STYLE: none;BORDER-LEFT-STYLE: none;HEIGHT: 18px;TEXT-ALIGN: right;BORDER-BOTTOM-STYLE: none"
											type="text" size="33" name="txtcon11" runat="server"/>

<label for="ctl00_MainPlaceHolder_cbocolA1"></label>
<asp:dropdownlist id="cbocolA1" Runat="server" tabindex="12"></asp:dropdownlist>
	<label for="ctl00_MainPlaceHolder_cbofunc11"></label>
								
	<asp:DropDownList ID="cbofunc11" Runat="server">
											<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="=">=</asp:ListItem>
											<asp:ListItem Value="!=">!=</asp:ListItem>
											<asp:ListItem Value="<%=str %>">>=</asp:ListItem>
											<asp:ListItem Value="<="><=</asp:ListItem>
											<asp:ListItem Value=">">></asp:ListItem>
											<asp:ListItem Value="<"><</asp:ListItem>
										</asp:DropDownList>
										<label for="ctl00_MainPlaceHolder_txtval11"></label>

										<input type="text" id="txtval11" name="txtval11" runat="server" style="WIDTH: 112px; HEIGHT: 18px"
											size="13"/>

<input id="imageFromDate" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(/AutoWhiz/Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px; CURSOR: hand; BORDER-BOTTOM: 0px solid;  "
											onclick="ShowCalendar1(this.id);" tabindex="1" type="button" name="imageFromDate" title="Select Date"/>
									</td>
								</tr>
								<tr>
									<td colspan="2" scope ="colgroup" style ="color:black">
										<label for="ctl00_MainPlaceHolder_txtcon12"></label>

										<input id="txtcon12" readonly="readonly" style="BORDER-TOP-STYLE: none;BORDER-RIGHT-STYLE: none;BORDER-LEFT-STYLE: none;HEIGHT: 18px;TEXT-ALIGN: right;BORDER-BOTTOM-STYLE: none"
											type="text" size="33" name="txtcon12" runat="server"/>
<label for="ctl00_MainPlaceHolder_cbocolA2"></label>
<asp:dropdownlist id="cbocolA2" Runat="server" tabindex="12"></asp:dropdownlist>
										<label for="ctl00_MainPlaceHolder_cbofunc12"></label>

										<asp:DropDownList ID="cbofunc12" Runat="server">
											<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="=">=</asp:ListItem>
											<asp:ListItem Value="!=">!=</asp:ListItem>
											<asp:ListItem Value="<%=str %>">>=</asp:ListItem>
											<asp:ListItem Value="<="><=</asp:ListItem>
											<asp:ListItem Value=">">></asp:ListItem>
											<asp:ListItem Value="<"><</asp:ListItem>
										</asp:DropDownList>
<label for="ctl00_MainPlaceHolder_txtval12"></label>
										
<input type="text" id="txtval12" name="txtval12" runat="server" style="WIDTH: 112px; HEIGHT: 18px"
											size="13"/><input id="imageFromDate1" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(/AutoWhiz/Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px; CURSOR: hand; BORDER-BOTTOM: 0px solid;  "
											onclick="ShowCalendar2(this.id);" tabindex="1" type="button" name="imageFromDate" title="Select Date"/>
									</td>
								</tr>
								<tr>
									<td colspan="2" scope ="colgroup" style ="color:black">
	<label for="ctl00_MainPlaceHolder_txtcon13"></label>
									
<input id="txtcon13" readonly="readonly" style="BORDER-TOP-STYLE: none;BORDER-RIGHT-STYLE: none;BORDER-LEFT-STYLE: none;HEIGHT: 18px;TEXT-ALIGN: right;BORDER-BOTTOM-STYLE: none"
											type="text" size="33" name="txtcon13" runat="server"/>
<label for="ctl00_MainPlaceHolder_cbocolA3"></label>
<asp:dropdownlist id="cbocolA3" Runat="server" tabindex="12"></asp:dropdownlist>
										<label for="ctl00_MainPlaceHolder_cbofunc13"></label>
										<asp:DropDownList ID="cbofunc13" Runat="server">
											<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="=">=</asp:ListItem>
											<asp:ListItem Value="!=">!=</asp:ListItem>
											<asp:ListItem Value="<%=str %>">>=</asp:ListItem>
											<asp:ListItem Value="<="><=</asp:ListItem>
											<asp:ListItem Value=">">></asp:ListItem>
											<asp:ListItem Value="<"><</asp:ListItem>
										</asp:DropDownList>
										<label for="ctl00_MainPlaceHolder_txtval13"></label>
										<input type="text" id="txtval13" name="txtval13" runat="server" style="WIDTH: 112px; HEIGHT: 18px"
											size="13"/><input id="imageFromDate2" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(/AutoWhiz/Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px; CURSOR: hand; BORDER-BOTTOM: 0px solid; "
											onclick="ShowCalendar3(this.id);" tabindex="1" type="button" name="imageFromDate" title="Select Date"/>
									</td>
								</tr>
								<tr>
									<td colspan="2" scope ="colgroup" style ="color:black">
										<label for="ctl00_MainPlaceHolder_txtcon14"></label>
										<input id="txtcon14" readonly="readonly" style="BORDER-TOP-STYLE: none;BORDER-RIGHT-STYLE: none;BORDER-LEFT-STYLE: none;HEIGHT: 18px;TEXT-ALIGN: right;BORDER-BOTTOM-STYLE: none"
											type="text" size="33" name="txtcon14" runat="server"/>
<label for="ctl00_MainPlaceHolder_cbocolA4"></label>
<asp:dropdownlist id="cbocolA4" Runat="server" tabindex="12"></asp:dropdownlist>
										<label for="ctl00_MainPlaceHolder_cbofunc14"></label>
											<asp:DropDownList ID="cbofunc14" Runat="server">
											<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="=">=</asp:ListItem>
											<asp:ListItem Value="!=">!=</asp:ListItem>
											<asp:ListItem Value="<%=str %>">>=</asp:ListItem>
											<asp:ListItem Value="<="><=</asp:ListItem>
											<asp:ListItem Value=">">></asp:ListItem>
											<asp:ListItem Value="<"><</asp:ListItem>
										</asp:DropDownList>
										<label for="ctl00_MainPlaceHolder_txtval14"></label>
										<input type="text" id="txtval14" name="txtval14" runat="server" style=" WIDTH: 112px; HEIGHT: 18px"
											size="13"/><input id="imageFromDate3" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(/Autowhiz/Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px; CURSOR: hand; BORDER-BOTTOM: 0px "
											onclick="ShowCalendar4(this.id);" tabindex="1" type="button" name="imageFromDate" title="Select Date"/>
									</td>
								</tr>
								<tr>
									<td colspan="2" scope ="colgroup" style ="color:black">
										<label for="ctl00_MainPlaceHolder_txtcon15"></label>
										<input id="txtcon15" readonly="readonly" style="BORDER-TOP-STYLE: none;BORDER-RIGHT-STYLE: none;BORDER-LEFT-STYLE: none;HEIGHT: 18px;TEXT-ALIGN: right;BORDER-BOTTOM-STYLE: none"
											type="text" size="33" name="txtcon15" runat="server"/>
<label for="ctl00_MainPlaceHolder_cbocolA5"></label>
<asp:dropdownlist id="cbocolA5" Runat="server" tabindex="12"></asp:dropdownlist>
										<label for="ctl00_MainPlaceHolder_cbofunc15"></label>
										<asp:DropDownList ID="cbofunc15" Runat="server">
											<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="=">=</asp:ListItem>
											<asp:ListItem Value="!=">!=</asp:ListItem>
											<asp:ListItem Value="<%=str %>">>=</asp:ListItem>
											<asp:ListItem Value="<="><=</asp:ListItem>
											<asp:ListItem Value=">">></asp:ListItem>
											<asp:ListItem Value="<"><</asp:ListItem>
										</asp:DropDownList>
										<label for="ctl00_MainPlaceHolder_txtval15"></label>
										<input type="text" id="txtval15" name="txtval15" runat="server" style="WIDTH: 112px; HEIGHT: 18px"
											size="13"/><input id="imageFromDate4" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(/AutoWhiz/Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px; CURSOR: hand; BORDER-BOTTOM: 0px solid;  "
											onclick="ShowCalendar5(this.id);" tabindex="1" type="button" name="imageFromDate" title="Select Date"/>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<tr>
                    <td colspan="6">
						<div id="divcon2" runat="server" style="VISIBILITY:hidden;WIDTH:100%">
							<table width="100%" summary ="table">
								<tr>
									<td colspan="2" scope ="colgroup" style ="color:black">
										<label for="ctl00_MainPlaceHolder_txtcon21"></label>
										<input id="txtcon21" readonly="readonly" style="BORDER-TOP-STYLE: none;BORDER-RIGHT-STYLE: none;BORDER-LEFT-STYLE: none;HEIGHT: 18px;TEXT-ALIGN: right;BORDER-BOTTOM-STYLE: none"
											type="text" size="33" name="txtcon21" runat="server"/>
<label for="ctl00_MainPlaceHolder_cbocolB1"></label>
<asp:dropdownlist id="cbocolB1" Runat="server" tabindex="13"></asp:dropdownlist>
										<label for="ctl00_MainPlaceHolder_cbofunc21"></label>
											<asp:DropDownList ID="cbofunc21" Runat="server">
											<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="=">=</asp:ListItem>
											<asp:ListItem Value="!=">!=</asp:ListItem>
											<asp:ListItem Value="<%=str %>">>=</asp:ListItem>
											<asp:ListItem Value="<="><=</asp:ListItem>
											<asp:ListItem Value=">">></asp:ListItem>
											<asp:ListItem Value="<"><</asp:ListItem>
										</asp:DropDownList>
										<label for="ctl00_MainPlaceHolder_txtval21"></label>
										<input type="text" id="txtval21" name="txtval21" runat="server" style="WIDTH: 112px; HEIGHT: 18px"
											size="13"/><input id="imageFromDate5" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(/AutoWhiz/Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px; CURSOR: hand; BORDER-BOTTOM: 0px solid;"
											onclick="ShowCalendar6(this.id);" tabindex="1" type="button" name="imageFromDate" title="Select Date"/>
									</td>
								</tr>
								<tr>
									<td colspan="2" scope ="colgroup" style ="color:black">
										<label for="ctl00_MainPlaceHolder_txtcon22"></label>
										<input id="txtcon22" readonly="readonly" style="BORDER-TOP-STYLE: none;BORDER-RIGHT-STYLE: none;BORDER-LEFT-STYLE: none;HEIGHT: 18px;TEXT-ALIGN: right;BORDER-BOTTOM-STYLE: none"
											type="text" size="33" name="txtcon22" runat="server"/>
<label for="ctl00_MainPlaceHolder_cbocolB2"></label>
<asp:dropdownlist id="cbocolB2" Runat="server" tabindex="13"></asp:dropdownlist>
										<label for="ctl00_MainPlaceHolder_cbofunc22"></label>
											<asp:DropDownList ID="cbofunc22" Runat="server">
											<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="=">=</asp:ListItem>
											<asp:ListItem Value="!=">!=</asp:ListItem>
											<asp:ListItem Value="<%=str %>">>=</asp:ListItem>
											<asp:ListItem Value="<="><=</asp:ListItem>
											<asp:ListItem Value=">">></asp:ListItem>
											<asp:ListItem Value="<"><</asp:ListItem>
										</asp:DropDownList>
										
<label for="ctl00_MainPlaceHolder_txtval22"></label>
										<input type="text" id="txtval22" name="txtval22" runat="server" style="WIDTH: 112px; HEIGHT: 18px"
											size="13"/><input id="imageFromDate6" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(/AutoWhiz/Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px; CURSOR: hand; BORDER-BOTTOM: 0px solid; "
											onclick="ShowCalendar7(this.id);" tabindex="1" type="button" name="imageFromDate" title="Select Date"/>
									</td>
								</tr>
								<tr>
									<td colspan="2" scope ="colgroup" style ="color:black">
							<label for="ctl00_MainPlaceHolder_txtcon23"></label>
										<input id="txtcon23" readonly="readonly" style="BORDER-TOP-STYLE: none;BORDER-RIGHT-STYLE: none;BORDER-LEFT-STYLE: none;HEIGHT: 18px;TEXT-ALIGN: right;BORDER-BOTTOM-STYLE: none"
											type="text" size="33" name="txtcon23" runat="server"/>
											<label for="ctl00_MainPlaceHolder_cbocolB3"></label>
											<asp:dropdownlist id="cbocolB3" Runat="server" tabindex="13"></asp:dropdownlist>
										<label for="ctl00_MainPlaceHolder_cbofunc23"></label>
										<asp:DropDownList ID="cbofunc23" Runat="server">
											<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="=">=</asp:ListItem>
											<asp:ListItem Value="!=">!=</asp:ListItem>
											<asp:ListItem Value="<%=str %>">>=</asp:ListItem>
											<asp:ListItem Value="<="><=</asp:ListItem>
											<asp:ListItem Value=">">></asp:ListItem>
											<asp:ListItem Value="<"><</asp:ListItem>
										</asp:DropDownList>
										<label for="ctl00_MainPlaceHolder_txtval23"></label>
										<input type="text" id="txtval23" name="txtval23" runat="server" style="WIDTH: 112px; HEIGHT: 18px"
											size="13"/><input id="imageFromDate7" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(/AutoWhiz/Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px; CURSOR: hand; BORDER-BOTTOM: 0px solid; "
											onclick="ShowCalendar8(this.id);" tabindex="1" type="button" name="imageFromDate" title="Select Date"/>
									</td>
								</tr>
								<tr>
									<td colspan="2" scope ="colgroup" style ="color:black">
										<label for="ctl00_MainPlaceHolder_txtcon24"></label>
										<input id="txtcon24" readonly="readonly" style="BORDER-TOP-STYLE: none;BORDER-RIGHT-STYLE: none;BORDER-LEFT-STYLE: none;HEIGHT: 18px;TEXT-ALIGN: right;BORDER-BOTTOM-STYLE: none"
											type="text" size="33" name="txtcon24" runat="server"/>
											<label for="ctl00_MainPlaceHolder_cbocolB4"></label>
											<asp:dropdownlist id="cbocolB4" Runat="server" tabindex="13"></asp:dropdownlist>
										<label for="ctl00_MainPlaceHolder_cbofunc24"></label>
										<asp:DropDownList ID="cbofunc24" Runat="server">
											<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="=">=</asp:ListItem>
											<asp:ListItem Value="!=">!=</asp:ListItem>
											<asp:ListItem Value="<%=str %>">>=</asp:ListItem>
											<asp:ListItem Value="<="><=</asp:ListItem>
											<asp:ListItem Value=">">></asp:ListItem>
											<asp:ListItem Value="<"><</asp:ListItem>
										</asp:DropDownList>
										<label for="ctl00_MainPlaceHolder_txtval24"></label>
										<input type="text" id="txtval24" name="txtval24" runat="server" style="WIDTH: 112px; HEIGHT: 18px"
											size="13"/><input id="imageFromDate8" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(/AutoWhiz/Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px; CURSOR: hand; BORDER-BOTTOM: 0px solid; "
											onclick="ShowCalendar9(this.id);" tabindex="1" type="button" name="imageFromDate" title="Select Date"/>
									</td>
								</tr>
								<tr>
									<td colspan="2" scope ="colgroup" style ="color:black">
										<label for="ctl00_MainPlaceHolder_txtcon25"></label>
										<input id="txtcon25" readonly="readonly" style="BORDER-TOP-STYLE: none;BORDER-RIGHT-STYLE: none;BORDER-LEFT-STYLE: none;HEIGHT: 18px;TEXT-ALIGN: right;BORDER-BOTTOM-STYLE: none"
											type="text" size="33" name="txtcon25" runat="server"/>
<label for="ctl00_MainPlaceHolder_cbocolB5"></label>
<asp:dropdownlist id="cbocolB5" Runat="server" tabindex="13"></asp:dropdownlist>
										<label for="ctl00_MainPlaceHolder_cbofunc25"></label>
											<asp:DropDownList ID="cbofunc25" Runat="server">
											<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="=">=</asp:ListItem>
											<asp:ListItem Value="!=">!=</asp:ListItem>
											<asp:ListItem Value="<%=str %>">>=</asp:ListItem>
											<asp:ListItem Value="<="><=</asp:ListItem>
											<asp:ListItem Value=">">></asp:ListItem>
											<asp:ListItem Value="<"><</asp:ListItem>
										</asp:DropDownList>
										<label for="ctl00_MainPlaceHolder_txtval25"></label>
										<input type="text" id="txtval25" name="txtval25" runat="server" style="WIDTH: 112px; HEIGHT: 18px"
											size="13"/><input id="imageFromDate9" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(/AutoWhiz/Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px; CURSOR: hand; BORDER-BOTTOM: 0px solid; "
											onclick="ShowCalendar10(this.id);" tabindex="1" type="button" name="imageFromDate" title="Select Date"/>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<tr>
					<td colspan="2" scope ="colgroup">&nbsp;
                    </td>
				</tr>
				<tr>
                <td>
                <table runat="server" id="savespanmul" visible="false" >
                <tr>			
                       <td colspan="1" scope ="colgroup" style ="color:black">
                        <strong><asp:Label ID="lbl7" Text="Level 1" runat="server" ></asp:Label></strong>
                        </td>
                    <td colspan="5" scope ="colgroup">
                        <label for="ctl00_MainPlaceHolder_cbodept2"></label>
                        <asp:DropDownList ID="cbodept2" runat="server" CssClass="dropdownlist" TabIndex="1">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="1" scope ="colgroup" style ="color:black">
                       <strong><asp:Label ID="lbl8" Text="Level 2" runat="server" ></asp:Label> </strong>
                        </td>
                    <td colspan="5" scope ="colgroup">
 <label for="ctl00_MainPlaceHolder_cboclient2"></label>
                        <asp:DropDownList ID="cboclient2"  runat="server" CssClass="dropdownlist" TabIndex="2">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td colspan="1" scope ="colgroup" style ="color:black">
                       <strong><asp:Label ID="lbl9" Text="Level 3" runat="server" ></asp:Label> </strong>
                        </td>
                    <td colspan="3" scope ="colgroup">                     
                         <label for="ctl00_MainPlaceHolder_cbolob2"></label>
                        <asp:DropDownList ID="cbolob2" runat="server" CssClass="dropdownlist" TabIndex="3">
                        </asp:DropDownList>
                        </td>
                        
                </tr>
                </table>
                </td>
                </tr>
				<tr>
                    <td colspan="1" scope ="colgroup" style ="color:black">
                        <asp:Label ID="Enter" runat="server" CssClass="label" Text="Enter table Name"></asp:Label>
                        </td>
                    <td colspan="5" scope ="colgroup">  
			 <label for="ctl00_MainPlaceHolder_txtname"></label>                      
                        <asp:TextBox ID="txtname" runat="server" CssClass="textbox" MaxLength="50" TabIndex="14"></asp:TextBox>
                        <asp:Button ID="cmdcreate" runat="server" CssClass="button" Visible="false"   OnClientClick="return chkvalidation();"
                            Style="width: 144px; height: 20px" TabIndex="15" Text="Create and Update" />
                            <asp:Button ID="cmdcreatemul" runat="server" Visible="false" CssClass="button" OnClientClick="return chkvalidation2();"
                            Style="width: 144px; height: 20px" TabIndex="15" Text="Create and Update" />
                            					<%--<asp:Button ID="btnShow" runat="server" BackColor="White" BorderStyle="None" Enabled="False" ForeColor="White" Width="1px" CausesValidation="False" />
                                    <input id="btnCheck" class="button" type="button" value="Go" onclick="return chkvalidation()" />
                                
                                --%>
                                <%--<asp:button id="cmdcreate" Runat="server" Width="100px" Enabled="false"  CssClass="button"></asp:button>--%>
                   </td>
				
				</tr>
	
		
				<tr>
					<td colspan="6" scope ="colgroup">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="6" scope ="colgroup" style ="color:black"><font color="red">Note: Use Ctrl key to select or deselect the multiple 
							selection.</font></td>
				</tr>
			</table>
    <input id="hdSpanA" runat="server" title="Hidden Field for finding first span  Selected Values from Dropdown list"
        type="hidden" />
    <input id="hdSpanB" runat="server" title="Hidden Field for finding second span Selected Values from Dropdown list"
        type="hidden" />
    <input id="hdSpan" runat="server" title="Hidden Field for finding third span Selected Values from Dropdown list"
        type="hidden" />
			<input id="hidQueryString" runat="server" type="hidden" title="Hidden Field for finding first span  Selected Values from Dropdown list"/>
			<input id="hidQueryString1" runat="server" type="hidden" title="Hidden Field for finding second span Selected Values from Dropdown list"/>
			<input id ="hidQueryString2" runat ="server" type="hidden" title="Hidden Field for finding third span Selected Values from Dropdown list"/>
			<input id="hfFirstTabQueryString" runat="server"  type="hidden" title="Hidden Field For Finding First Table Columns" />
			<input id ="hfSecondTabQueryString" runat="server" type ="hidden" title ="Hidden Field For Finding Second Table Columns" />
			<input id ="hfJoinTab1QueryString" runat="server" type ="hidden" title ="Hidden Field For Finding join Columns of First Table" />
			<input id ="hfJoinTab2QueryString" runat="server" type ="hidden" title ="Hidden Field For Finding join Columns of Second Table" />
			<input id="hfJoinTabQueryString" runat="server" type="hidden" title="Hidden Field For Join Table" />
			
			 <asp:HiddenField ID="hfUserType" runat="server" />
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>

