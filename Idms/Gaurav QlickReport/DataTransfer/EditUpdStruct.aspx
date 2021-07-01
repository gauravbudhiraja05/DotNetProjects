<%@ Page Language="VB" MasterPageFile="~/MasterPage/Masterpage.master" AutoEventWireup="false" CodeFile="EditUpdStruct.aspx.vb" Inherits="DataTransfer_EditUpdStruct" title="Update Structure" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceholder1" Runat="Server">
 <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
<script language="javascript" type ="text/javascript" >






 var arrQueryString ;
        window.arrQueryString = ["NULL","NULL","NULL","NULL"]; // Arrar For Finding Department,Client,lob of First Span
        
        var arrQueryString1;
        window.arrQueryString1=["NULL","NULL","NULL","NULL"]; // Arrar For Finding Department,Client,lob of Second Span
        
        var arrQueryString2;
        window.arrQueryString2=["NULL","NULL","NULL","NULL"]; // Arrary For Finding Department,Client,lob of Third Span
        
			
			
	   var arrFirstTabQueryString;
	   window.arrFirstTabQueryString=["NULL","NULL","NULL","NULL","NULL","NULL","NULL","NULL","NULL","NULL"];
	   
	   var arrJoinTabQueryString;
	   window.arrJoinTabQueryString=["NULL","NULL","NULL","NULL","NULL","NULL","NULL","NULL","NULL","NULL"];
		
		function getclient()
			
			{
			   var str1;
			   str1 = document.getElementById("<%=hidQueryString.ClientId %>").value;
			   window.arrQueryString = str1.split("#");
			   
			
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
	                       //alert(document.getElementById("<%=hidQueryString.ClientID %>").value)
                            
                         document.getElementById("<%=hidQueryString.ClientID %>").value += window.arrQueryString[i] + "#";
                            }
                           // alert(document.getElementById("<%=hidQueryString.ClientID %>").value)
					DataTransfer.bindclient(document.getElementById("<%=cbodept.ClientId %>").value,filclient)
					
					
					DataTransfer.bindepttab(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,document.getElementById("<%=hfUserId.ClientId %>").value,filtab)
				
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
			
			
			
			//'------------------------------------------------------------------------------------------------------
			function getclient1()
			{
			
			var str2;
			   str2 = document.getElementById("<%=hidQueryString1.ClientID %>").value;
			   window.arrQueryString = str2.split("#");
			   
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
				
					DataTransfer.bindclient(document.getElementById("<%=cbodept1.ClientId %>").value,filclient1)
					DataTransfer.bindepttab(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,document.getElementById("<%=hfUserId.ClientId %>").value,filtab1)
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
			var str3;
			   str3 = document.getElementById("<%=hidQueryString2.ClientID %>").value;
			   window.arrQueryString = str3.split("#");
				
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
                        //alert(window.arrQueryString2[1])
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
					DataTransfer.bindlob(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,fillob)
					
				}
				if (document.getElementById("<%=cboclient.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbodept.ClientId %>").selectedIndex!=0)
				{
					DataTransfer.bindepttab(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,document.getElementById("<%=hfUserId.ClientId %>").value,filtab)
				}
				else
				{
					DataTransfer.bindclienttab(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,document.getElementById("<%=hfUserId.ClientId %>").value,filtab)
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
				
				
				
					DataTransfer.bindlob(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,fillob1)
					
				}
				if (document.getElementById("<%=cboclient1.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbodept1.ClientId %>").selectedIndex!=0)
				{
					DataTransfer.bindepttab(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,document.getElementById("<%=hfUserId.ClientId %>").value,filtab1)
				}
				else
				{
					DataTransfer.bindclienttab(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,document.getElementById("<%=hfUserId.ClientId %>").value,filtab1)
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
					DataTransfer.bindlob(document.getElementById("<%=cbodept2.ClientId %>").value,document.getElementById("<%=cboclient2.ClientId %>").value,fillob2)
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
				if (document.getElementById("<%=cbolob.ClientId %>").selectedIndex==0)
				{
					for(i=document.getElementById("<%=cbotab1.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbotab1.ClientId %>").remove(i);
					}
					for(i=document.getElementById("<%=cbocolA1.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbocolA1.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA2.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA3.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA4.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA5.ClientId %>").remove(i);
						document.getElementById("<%=cbocol11.ClientId %>").remove(i);
						document.getElementById("<%=cbocol12.ClientId %>").remove(i);
						document.getElementById("<%=cbocol13.ClientId %>").remove(i);
						document.getElementById("<%=cbocol14.ClientId %>").remove(i);
						document.getElementById("<%=cbocol15.ClientId %>").remove(i);
						document.getElementById("<%=cbocol16.ClientId %>").remove(i);
						document.getElementById("<%=cbocol17.ClientId %>").remove(i);
						document.getElementById("<%=cbocol18.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA11.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA12.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA13.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA14.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA15.ClientId %>").remove(i);
						
					}
				}
				if (document.getElementById("<%=cbolob.ClientId %>").selectedIndex==0 && document.getElementById("<%=cboclient.ClientId %>").selectedIndex==0)
				{
					DataTransfer.bindepttab(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,document.getElementById("<%=hfUserId.ClientId %>").value,filtab)
				}
				else if(document.getElementById("<%=cbolob.ClientId %>").selectedIndex==0)
				{
					DataTransfer.bindclienttab(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,document.getElementById("<%=hfUserId.ClientId %>").value,filtab)
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
					DataTransfer.bindtable(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId %>").value,document.getElementById("<%=cbolob.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,document.getElementById("<%=hfUserId.ClientId %>").value,filtab)
				}
				
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
			
			//--------------------------------------------------------------------------------
			function gettab1()
			{
				if (document.getElementById("<%=cbolob1.ClientId %>").selectedIndex==0)
				{
					for(i=document.getElementById("<%=cbotab2.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbotab2.ClientId %>").remove(i);
					}
					for(i=document.getElementById("<%=cbocolB1.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbocolB1.ClientId %>").remove(i);
						document.getElementById("<%=cbocolB2.ClientId %>").remove(i);
						document.getElementById("<%=cbocolB3.ClientId %>").remove(i);
						document.getElementById("<%=cbocolB4.ClientId %>").remove(i);
						document.getElementById("<%=cbocolB5.ClientId %>").remove(i);
						document.getElementById("<%=cbocol21.ClientId %>").remove(i);
						document.getElementById("<%=cbocol22.ClientId %>").remove(i);
						document.getElementById("<%=cbocol23.ClientId %>").remove(i);
						document.getElementById("<%=cbocol24.ClientId %>").remove(i);
						document.getElementById("<%=cbocol25.ClientId %>").remove(i);
						document.getElementById("<%=cbocol26.ClientId %>").remove(i);
						document.getElementById("<%=cbocol27.ClientId %>").remove(i);
						document.getElementById("<%=cbocol28.ClientId %>").remove(i);
						document.getElementById("<%=cbocolB21.ClientId %>").remove(i);
						document.getElementById("<%=cbocolB22.ClientId %>").remove(i);
						document.getElementById("<%=cbocolB23.ClientId %>").remove(i);
						document.getElementById("<%=cbocolB24.ClientId %>").remove(i);
						document.getElementById("<%=cbocolB25.ClientId %>").remove(i);
						
					}
				}
				if (document.getElementById("<%=cbolob1.ClientId %>").selectedIndex==0 && document.getElementById("<%=cboclient1.ClientId %>").selectedIndex==0)
				{
					DataTransfer.bindepttab(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,document.getElementById("<%=hfUserId.ClientId %>").value,filtab1)
				}
				else if(document.getElementById("<%=cbolob1.ClientId %>").selectedIndex==0)
				{
					DataTransfer.bindclienttab(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,document.getElementById("<%=hfUserId.ClientId %>").value,filtab1)
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
			
					DataTransfer.bindtable(document.getElementById("<%=cbodept1.ClientId %>").value,document.getElementById("<%=cboclient1.ClientId %>").value,document.getElementById("<%=cbolob1.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,document.getElementById("<%=hfUserId.ClientId %>").value,filtab1)
				}
				
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
			
			
			//-------------------------------------------------------------------------------------------------------	
			
			
			function gettab1cols()
			{
				var cols = document.getElementById("<%=cbotab1.ClientId %>").value.split("$");
				var arrcols = cols[0].split(',');
				
					for(i=document.getElementById("<%=cbocol11.ClientId %>").length;i>=0;i--)
					{
						
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
						document.getElementById("<%=cbocolA11.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA12.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA13.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA14.ClientId %>").remove(i);
						document.getElementById("<%=cbocolA15.ClientId %>").remove(i);
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
					document.all["divdesc"].style.visibility = "hidden";
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
	                       //alert(document.getElementById("<%=hidQueryString.ClientID %>").value)
                            
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
					document.getElementById("<%=cbocolA11.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolA12.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolA13.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolA14.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolA15.ClientId %>").options[0]=new Option("--Select--");
					for(i=0;i<arrcols.length;i++)
					{
						
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
						document.getElementById("<%=cbocolA11.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolA12.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolA13.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolA14.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
						document.getElementById("<%=cbocolA15.ClientId %>").options[i+1]=new Option(arrcols[i],arrcols[i]);
					}
					document.getElementById("<%=txttab11.ClientId %>").value = "where    " + "  " + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol11.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab11.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab12.ClientId %>").value = "and    " + "  " + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol12.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab12.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab13.ClientId %>").value = "and    " + "  "  + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol13.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab13.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab14.ClientId %>").value = "and    " + "  "  + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol14.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab14.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab15.ClientId %>").value = "and    " + "  "  + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol15.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab15.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab16.ClientId %>").value = "and    " + "  "  + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol16.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab16.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab17.ClientId %>").value = "and    " + "  "  + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=cbocol17.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txttab17.ClientId %>").style.visibility = "visible";
					
					document.getElementById("<%=txttab18.ClientId %>").value = "and    " + "  "  + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
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
					document.all["divdesc"].style.visibility = "visible";
					
					document.getElementById("<%=txthead.ClientId %>").style.visibility = "visible";
					document.getElementById("<%=txtcon11.ClientId %>").value = "and    " + "  "  + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=txtcon12.ClientId %>").value = "and    " + "  "  + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=txtcon13.ClientId %>").value = "and    " + "  "  + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=txtcon14.ClientId %>").value = "and    " + "  "  + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=txtcon15.ClientId %>").value = "and    " + "  "  + document.getElementById("<%=cbotab1.ClientId %>").item(document.getElementById("<%=cbotab1.ClientId %>").selectedIndex).text + ".";
					document.getElementById("<%=divcon1.ClientId %>").style.visibility = "visible";
				}
			}
			
			
			function gettab2cols()
			{
				//var cols = document.getElementById("<%=cbotab2.ClientId %>").value.split("$");
				//var arrcols = cols[0].split(',');
				
				var cols1 = document.getElementById("<%=cbotab2.ClientId %>").value.split("$");
				var arrcols1 = cols1[0].split(',');
				
					for(i=document.getElementById("<%=cbocol21.ClientId %>").length;i>=0;i--)
					{
						
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
						document.getElementById("<%=cbocolB21.ClientId %>").remove(i);
						document.getElementById("<%=cbocolB22.ClientId %>").remove(i);
						document.getElementById("<%=cbocolB23.ClientId %>").remove(i);
						document.getElementById("<%=cbocolB24.ClientId %>").remove(i);
						document.getElementById("<%=cbocolB25.ClientId %>").remove(i);
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
                        //alert(window.arrQueryString[3])
                      }  
                    else
                     {
                         window.arrQueryString[3]="NULL";    
                      }
                        for(i=0;i<4;i++)
	                       {
	                      // alert(document.getElementById("<%=hidQueryString1.ClientID %>").value)
                            
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
					document.getElementById("<%=cbocolB21.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolB22.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolB23.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolB24.ClientId %>").options[0]=new Option("--Select--");
					document.getElementById("<%=cbocolB25.ClientId %>").options[0]=new Option("--Select--");
					
					for(i=0;i<arrcols1.length;i++)
					{
						
						document.getElementById("<%=cbocol21.ClientId %>").options[i+1]=new Option(arrcols1[i],arrcols1[i]);
						document.getElementById("<%=cbocol22.ClientId %>").options[i+1]=new Option(arrcols1[i],arrcols1[i]);
						document.getElementById("<%=cbocol23.ClientId %>").options[i+1]=new Option(arrcols1[i],arrcols1[i]);
						document.getElementById("<%=cbocol24.ClientId %>").options[i+1]=new Option(arrcols1[i],arrcols1[i]);
						document.getElementById("<%=cbocol25.ClientId %>").options[i+1]=new Option(arrcols1[i],arrcols1[i]);
						document.getElementById("<%=cbocol26.ClientId %>").options[i+1]=new Option(arrcols1[i],arrcols1[i]);
						document.getElementById("<%=cbocol27.ClientId %>").options[i+1]=new Option(arrcols1[i],arrcols1[i]);
						document.getElementById("<%=cbocol28.ClientId %>").options[i+1]=new Option(arrcols1[i],arrcols1[i]);
						document.getElementById("<%=cbocolB1.ClientId %>").options[i+1]=new Option(arrcols1[i],arrcols1[i]);
						document.getElementById("<%=cbocolB2.ClientId %>").options[i+1]=new Option(arrcols1[i],arrcols1[i]);
						document.getElementById("<%=cbocolB3.ClientId %>").options[i+1]=new Option(arrcols1[i],arrcols1[i]);
						document.getElementById("<%=cbocolB4.ClientId %>").options[i+1]=new Option(arrcols1[i],arrcols1[i]);
						document.getElementById("<%=cbocolB5.ClientId %>").options[i+1]=new Option(arrcols1[i],arrcols1[i]);
						document.getElementById("<%=cbocolB21.ClientId %>").options[i+1]=new Option(arrcols1[i],arrcols1[i]);
						document.getElementById("<%=cbocolB22.ClientId %>").options[i+1]=new Option(arrcols1[i],arrcols1[i]);
						document.getElementById("<%=cbocolB23.ClientId %>").options[i+1]=new Option(arrcols1[i],arrcols1[i]);
						document.getElementById("<%=cbocolB24.ClientId %>").options[i+1]=new Option(arrcols1[i],arrcols1[i]);
						document.getElementById("<%=cbocolB25.ClientId %>").options[i+1]=new Option(arrcols1[i],arrcols1[i]);
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
//					theform = document.Form1;
//				}
//				else {
//					theform = document.forms["Form1"];
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
			
	
			function chkvalidation()
			{	
			
			   
				if(document.getElementById("<%=cbodept.ClientId %>").selectedIndex==0)
				{
					alert("Please select Department for Table I")
					return false;
				}
				 
				else if(document.getElementById("<%=cbodept1.ClientId %>").selectedIndex==0)
				{
					alert("Please select Department for Table II")
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
				else if( (document.getElementById("<%=cbotab1.ClientId %>").selectedIndex==0) || (document.getElementById("<%=cbotab1.ClientId %>").selectedIndex==-1) )
				{
					alert("Please select Table I")
				return false;
				}
				else if( (document.getElementById("<%=cbotab2.ClientId %>").selectedIndex==0) || (document.getElementById("<%=cbotab2.ClientId %>").selectedIndex==-1) )
				{
					alert("Please select Table II")
				return false;
				}
				//validation for columns
				else if(document.getElementById("<%=cbocolA1.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbocolA2.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbocolA3.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbocolA4.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbocolA5.ClientId %>").selectedIndex==0)
				{
					alert("Please select columns from Table I")
				return false;
				}
				else if( document.getElementById("<%=cbocolA1.ClientId %>").selectedIndex != 0 && ((document.getElementById("<%=cbocolA1.ClientId %>").value == document.getElementById("<%=cbocolA2.ClientId %>").value) || (document.getElementById("<%=cbocolA1.ClientId %>").value == document.getElementById("<%=cbocolA3.ClientId %>").value) || (document.getElementById("<%=cbocolA1.ClientId %>").value == document.getElementById("<%=cbocolA4.ClientId %>").value) || (document.getElementById("<%=cbocolA1.ClientId %>").value == document.getElementById("<%=cbocolA5.ClientId %>").value)) )
				{
					alert("You can not select " + document.getElementById("<%=cbocolA1.ClientId %>").value + " more than one time.")
				return false;
				}
				else if( document.getElementById("<%=cbocolA2.ClientId %>").selectedIndex != 0 && ((document.getElementById("<%=cbocolA2.ClientId %>").value == document.getElementById("<%=cbocolA3.ClientId %>").value) || (document.getElementById("<%=cbocolA2.ClientId %>").value == document.getElementById("<%=cbocolA4.ClientId %>").value) || (document.getElementById("<%=cbocolA2.ClientId %>").value == document.getElementById("<%=cbocolA5.ClientId %>").value)) )
				{
					alert("You can not select " + document.getElementById("<%=cbocolA2.ClientId %>").value + " more than one time.")
				return false;
				}
				else if( document.getElementById("<%=cbocolA3.ClientId %>").selectedIndex != 0 && ((document.getElementById("<%=cbocolA3.ClientId %>").value == document.getElementById("<%=cbocolA4.ClientId %>").value) || (document.getElementById("<%=cbocolA3.ClientId %>").value == document.getElementById("<%=cbocolA5.ClientId %>").value)) )
				{
					alert("You can not select " + document.getElementById("<%=cbocolA3.ClientId %>").value + " more than one time.")
				return false;
				}
				else if( document.getElementById("<%=cbocolA4.ClientId %>").selectedIndex != 0 && ((document.getElementById("<%=cbocolA4.ClientId %>").value == document.getElementById("<%=cbocolA5.ClientId %>").value)) )
				{
					alert("You can not select " + document.getElementById("<%=cbocolA4.ClientId %>").value + " more than one time.")
				return false;
				}
				///////////
				else if(document.getElementById("<%=cbocolA1.ClientId %>").selectedIndex!=0 && document.getElementById("<%=cbocolB1.ClientId %>").selectedIndex==0)
				{
					alert("Please select columns from Table II")
				return false;
				}
				else if(document.getElementById("<%=cbocolA2.ClientId %>").selectedIndex!=0 && document.getElementById("<%=cbocolB2.ClientId %>").selectedIndex==0)
				{
					alert("Please select columns II corresponding to column " + document.getElementById("<%=cbocolA2.ClientId %>").value)
				return false;
				}
				else if(document.getElementById("<%=cbocolA3.ClientId %>").selectedIndex!=0 && document.getElementById("<%=cbocolB3.ClientId %>").selectedIndex==0)
				{
					alert("Please select columns II corresponding to column " + document.getElementById("<%=cbocolA3.ClientId %>").value)
				return false;
				}
				else if(document.getElementById("<%=cbocolA4.ClientId %>").selectedIndex!=0 && document.getElementById("<%=cbocolB4.ClientId %>").selectedIndex==0)
				{
					alert("Please select columns II corresponding to column " + document.getElementById("<%=cbocolA4.ClientId %>").value)
				return false;
				}
				else if(document.getElementById("<%=cbocolA5.ClientId %>").selectedIndex!=0 && document.getElementById("<%=cbocolB5.ClientId %>").selectedIndex==0)
				{
					alert("Please select columns II corresponding to column " + document.getElementById("<%=cbocolA5.ClientId %>").value)
				return false;
				}
				//validation for where clause
				else if (document.getElementById("<%=cbocol11.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbocol21.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbojoin1.ClientId %>").selectedIndex==0)
				{
					alert("Please select the columns and join in where clause to join the two tables")
				return false;
				}
				else if(document.getElementById("<%=cbocol11.ClientId %>").selectedIndex!=0 && document.getElementById("<%=cbocol21.ClientId %>").selectedIndex==0)
				{
					alert("Please select column from Table II in where clause")
				return false;
				}
				else if(document.getElementById("<%=cbocol12.ClientId %>").selectedIndex!=0 && document.getElementById("<%=cbocol22.ClientId %>").selectedIndex==0)
				{
					alert("Please select column from Table II in where clause")
				return false;
				}
				else if(document.getElementById("<%=cbocol13.ClientId %>").selectedIndex!=0 && document.getElementById("<%=cbocol23.ClientId %>").selectedIndex==0)
				{
					alert("Please select column from Table II in where clause")
				return false;
				}
				else if(document.getElementById("<%=cbodept2.ClientId %>").selectedIndex==0)
				{
					alert("Please select Department for Update Command")
				return false;
				}
				else
				{
					var client
					var lob
					if(document.getElementById("<%=cboclient2.ClientId %>").value!="" && document.getElementById("<%=cboclient2.ClientId %>").value!="--Select--")
					{
						client = document.getElementById("<%=cboclient2.ClientId %>").value
					}
					else
					{
						client = 0
					}
					if(document.getElementById("<%=cbolob2.ClientId %>").value!="" && document.getElementById("<%=cbolob2.ClientId %>").value!="--Select--")
					{
						lob = document.getElementById("<%=cbolob2.ClientId %>").value
					}
					else
					{
						lob = 0
					}
					
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
				
				// Function for finding select value of condition
				
				var conTab1Querystring;
				var conTab2Querystring;
				
				conTab1Querystring=document.getElementById("<%=cbocolA11.ClientId %>").value;
				conTab1Querystring =conTab1Querystring +"#" + document.getElementById("<%=cbocolA12.ClientId %>").value;
				conTab1Querystring =conTab1Querystring +"#" + document.getElementById("<%=cbocolA13.ClientId %>").value;
				conTab1Querystring =conTab1Querystring +"#" + document.getElementById("<%=cbocolA14.ClientId %>").value;
				conTab1Querystring =conTab1Querystring +"#" + document.getElementById("<%=cbocolA15.ClientId %>").value;
				
				document.getElementById("<%=hfconTab1QueryString.ClientId %>").value =conTab1Querystring;
				
				
				conTab2Querystring=document.getElementById("<%=cbocolB21.ClientId %>").value;
				conTab2Querystring =conTab2Querystring +"#" + document.getElementById("<%=cbocolB22.ClientId %>").value;
				conTab2Querystring =conTab2Querystring +"#" + document.getElementById("<%=cbocolB23.ClientId %>").value;
				conTab2Querystring =conTab2Querystring +"#" + document.getElementById("<%=cbocolB24.ClientId %>").value;
				conTab2Querystring =conTab2Querystring +"#" + document.getElementById("<%=cbocolB25.ClientId %>").value;
				
				document.getElementById("<%=hfconTab2QueryString.ClientId %>").value =conTab2Querystring;
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
			
			
			function chkvalidsave()
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
				else if( (document.getElementById("<%=cbotab1.ClientId %>").selectedIndex==0) || (document.getElementById("<%=cbotab1.ClientId %>").selectedIndex==-1) )
				{
					alert("Please select Table to update")
				return false;
				}
				else if( (document.getElementById("<%=cbotab2.ClientId %>").selectedIndex==0) || (document.getElementById("<%=cbotab2.ClientId %>").selectedIndex==-1) )
				{
					alert("Please select Table from update")
				return false;
				}
				//validation for columns
				else if(document.getElementById("<%=cbocolA1.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbocolA2.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbocolA3.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbocolA4.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbocolA5.ClientId %>").selectedIndex==0)
				{
					alert("Please select the columns to update.")
					return false;
					
				}
				else if( document.getElementById("<%=cbocolA1.ClientId %>").selectedIndex != 0 && ((document.getElementById("<%=cbocolA1.ClientId %>").value == document.getElementById("<%=cbocolA2.ClientId %>").value) || (document.getElementById("<%=cbocolA1.ClientId %>").value == document.getElementById("<%=cbocolA3.ClientId %>").value) || (document.getElementById("<%=cbocolA1.ClientId %>").value == document.getElementById("<%=cbocolA4.ClientId %>").value) || (document.getElementById("<%=cbocolA1.ClientId %>").value == document.getElementById("<%=cbocolA5.ClientId %>").value)) )
				{
					alert("You can not select " + document.getElementById("<%=cbocolA1.ClientId %>").value + " more than one time.")
				return false;
				}
				else if( document.getElementById("<%=cbocolA2.ClientId %>").selectedIndex != 0 && ((document.getElementById("<%=cbocolA2.ClientId %>").value == document.getElementById("<%=cbocolA3.ClientId %>").value) || (document.getElementById("<%=cbocolA2.ClientId %>").value == document.getElementById("<%=cbocolA4.ClientId %>").value) || (document.getElementById("<%=cbocolA2.ClientId %>").value == document.getElementById("<%=cbocolA5.ClientId %>").value)) )
				{
					alert("You can not select " + document.getElementById("<%=cbocolA2.ClientId %>").value + " more than one time.")
				return false;
				}
				else if( document.getElementById("<%=cbocolA3.ClientId %>").selectedIndex != 0 && ((document.getElementById("<%=cbocolA3.ClientId %>").value == document.getElementById("<%=cbocolA4.ClientId %>").value) || (document.getElementById("<%=cbocolA3.ClientId %>").value == document.getElementById("<%=cbocolA5.ClientId %>").value)) )
				{
					alert("You can not select " + document.getElementById("<%=cbocolA3.ClientId %>").value + " more than one time.")
				return false;
				}
				else if( document.getElementById("<%=cbocolA4.ClientId %>").selectedIndex != 0 && ((document.getElementById("<%=cbocolA4.ClientId %>").value == document.getElementById("<%=cbocolA5.ClientId %>").value)) )
				{
					alert("You can not select " + document.getElementById("<%=cbocolA4.ClientId %>").value + " more than one time.")
				return false;
				}
				///////////
				else if(document.getElementById("<%=cbocolA1.ClientId %>").selectedIndex!=0 && document.getElementById("<%=cbocolB1.ClientId %>").selectedIndex==0)
				{
					alert("Please select the column from second table to replace" + document.getElementById("<%=cbocolA1.ClientId %>").value)
				return false;
				}
				else if(document.getElementById("<%=cbocolA2.ClientId %>").selectedIndex!=0 && document.getElementById("<%=cbocolB2.ClientId %>").selectedIndex==0)
				{
					alert("Please select the column from second table to replace" + document.getElementById("<%=cbocolA2.ClientId %>").value)
				return false;
				}
				else if(document.getElementById("<%=cbocolA3.ClientId %>").selectedIndex!=0 && document.getElementById("<%=cbocolB3.ClientId %>").selectedIndex==0)
				{
					alert("Please select the column from second table to replace" + document.getElementById("<%=cbocolA3.ClientId %>").value)
				return false;
				}
				else if(document.getElementById("<%=cbocolA4.ClientId %>").selectedIndex!=0 && document.getElementById("<%=cbocolB4.ClientId %>").selectedIndex==0)
				{
					alert("Please select the column from second table to replace" + document.getElementById("<%=cbocolA4.ClientId %>").value)
				return false;
				}
				else if(document.getElementById("<%=cbocolA5.ClientId %>").selectedIndex!=0 && document.getElementById("<%=cbocolB5.ClientId %>").selectedIndex==0)
				{
					alert("Please select the column from second table to replace" + document.getElementById("<%=cbocolA5.ClientId %>").value)
				return false;
				}
				//validation for where clause
				else if (document.getElementById("<%=cbocol11.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbocol21.ClientId %>").selectedIndex==0 && document.getElementById("<%=cbojoin1.ClientId %>").selectedIndex==0)
				{
					alert("Please select the columns and join in where clause to join the two tables")
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
				else if(document.getElementById("<%=cbodept2.ClientId %>").selectedIndex==0)
				{
					alert("Please select department under which you have to save the structure")
				return false;
				}
				else if(blank(document.getElementById("<%=txtname.ClientId %>").value))
				{
					alert("Please enter name")
				return false;
				}
				else
				{
					var client
					var lob
					if(document.getElementById("<%=cboclient2.ClientId %>").value!="" && document.getElementById("<%=cboclient2.ClientId %>").value!="--Select--")
					{
						client = document.getElementById("<%=cboclient2.ClientId %>").value
					}
					else
					{
						client = 0
					}
					if(document.getElementById("<%=cbolob2.ClientId %>").value!="" && document.getElementById("<%=cbolob2.ClientId %>").value!="--Select--")
					{
						lob = document.getElementById("<%=cbolob2.ClientId %>").value
					}
					else
					{
						lob = 0
					}
					DataTransfer.chkstructname(document.getElementById("<%=txtname.ClientId %>").value,document.getElementById("<%=cbodept2.ClientId %>").value,client,lob,chkname)
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
				
				
				// Function for findinf select value of condition
				
				var conTab1Querystring;
				var conTab2Querystring;
				
				conTab1Querystring=document.getElementById("<%=cbocolA11.ClientId %>").value;
				conTab1Querystring =conTab1Querystring +"#" + document.getElementById("<%=cbocolA12.ClientId %>").value;
				conTab1Querystring =conTab1Querystring +"#" + document.getElementById("<%=cbocolA13.ClientId %>").value;
				conTab1Querystring =conTab1Querystring +"#" + document.getElementById("<%=cbocolA14.ClientId %>").value;
				conTab1Querystring =conTab1Querystring +"#" + document.getElementById("<%=cbocolA15.ClientId %>").value;
				
				document.getElementById("<%=hfconTab1QueryString.ClientId %>").value =conTab1Querystring;
				
				
				conTab2Querystring=document.getElementById("<%=cbocolB21.ClientId %>").value;
				conTab2Querystring =conTab2Querystring +"#" + document.getElementById("<%=cbocolB22.ClientId %>").value;
				conTab2Querystring =conTab2Querystring +"#" + document.getElementById("<%=cbocolB23.ClientId %>").value;
				conTab2Querystring =conTab2Querystring +"#" + document.getElementById("<%=cbocolB24.ClientId %>").value;
				conTab2Querystring =conTab2Querystring +"#" + document.getElementById("<%=cbocolB25.ClientId %>").value;
				
				document.getElementById("<%=hfconTab2QueryString.ClientId %>").value =conTab2Querystring;
			
			//****************
			
			var spana ;
			var spanb ;
			var spanc ;
			
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
			
			
			function chkname(res)
			{
			
				if(res.value=="Y")	
				{
					//alert("Structure name already exists.")
					var t=0;
				}
				else
				{
					__doPostBack('cmdsav','');
				}
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
		
		<input type="hidden" name="__EVENTTARGET"/><input type="hidden" name="__EVENTARGUMENT"/>
			<asp:panel id="above_where" runat="server">
			<table  align="center" class="table" style="width: 788px" >
				<caption style ="background-color:#67A897">Update Stucture</caption>
				<%--<tr>
					<th colspan="2">
						Update table&nbsp; 
					</th>
				</tr>--%>
				
				
				
				<tr>
					<td colspan="2" scope ="colgroup" style="height: 14px; color :Black " align="center"><strong>TO</strong></td>
					<td colspan="2" scope ="colgroup" style="height: 14px; color:Black " align="center"><strong>FROM</strong></td>
				</tr>
				<tr>
					<td colspan="3" scope ="colgroup">
						<table width="100%" summary ="Table">
						   
						    <tr>
						        <td scope ="col" style ="color:black" ><label for ="lblname" class="label">Command Name:</label></td>
						        <td scope ="col" style ="color:black" ><asp:Label ID="lblname" cssclass="label" Runat="server"></asp:Label></td>
						    </tr>
                            <tr>
                            <td>
                            <table width="100%" runat="server" visible="false" id="spandisplay3" >
                            <tr>
								<td scope ="col" class ="label" style="width: 133px; color :Black "><strong><asp:Label ID="lbl1" Text="Level 1" runat="server" ></asp:Label></strong></td>
								<td scope ="col"  ><asp:dropdownlist id="cbodept" tabindex="1" Runat="server" CssClass="dropdownlist" ToolTip="Select DepartMent"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td class="label" scope ="col" style ="color:black" ><strong><asp:Label ID="lbl2" Text="Level 2" runat="server" ></asp:Label> </strong></td>
								<td scope ="col" ><asp:dropdownlist id="cboclient" tabindex="2" Runat="server" CssClass="dropdownlist" ToolTip="Select Client"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td class="label" scope ="col"  style ="color:black" ><strong><asp:Label ID="lbl3" Text="Level 3" runat="server" ></asp:Label> </strong></td>
								<td scope ="col" ><asp:dropdownlist id="cbolob" tabindex="3" Runat="server" CssClass="dropdownlist" ToolTip="Select Lob"></asp:dropdownlist></td>
							</tr>
                            </table>
                            </td>
                            </tr>
							
							<tr>
								<td scope ="col"  class="label" style ="color:black"><label for ="ctl00_MainPlaceHolder_cbotab1" class="label">Table I:</label></td>
								<td scope ="col"><asp:dropdownlist id="cbotab1" tabindex="4" Runat="server" CssClass="dropdownlist" ToolTip="Select Table To Update"></asp:dropdownlist></td>
							</tr>
							<tr>
				           <td  valign="top" scope ="col" style ="color:black"  ><label class="label" for="Columns to update">Columns I
                               <br />
                               To update<br />
                           </label></td>
					        <td  valign="top" scope ="col"  >
<label for ="ctl00_MainPlaceHolder_cbocolA1" class="label"></label>
						        <asp:dropdownlist id="cbocolA1" tabindex="9" Runat="server" CssClass="dropdownlist"></asp:dropdownlist><br/>
	<label for ="ctl00_MainPlaceHolder_cbocolA2" class="label"></label>					        
<asp:dropdownlist id="cbocolA2" tabindex="11" Runat="server" CssClass="dropdownlist"></asp:dropdownlist><br/>
	<label for ="ctl00_MainPlaceHolder_cbocolA3" class="label"></label>					        
<asp:dropdownlist id="cbocolA3" tabindex="13" Runat="server" CssClass="dropdownlist"></asp:dropdownlist><br/>
				<label for ="ctl00_MainPlaceHolder_cbocolA4" class="label"></label>		        
<asp:dropdownlist id="cbocolA4" tabindex="15" Runat="server" CssClass="dropdownlist"></asp:dropdownlist><br/>
<label for ="ctl00_MainPlaceHolder_cbocolA5" class="label"></label>
						        <asp:dropdownlist id="cbocolA5" tabindex="17" Runat="server" CssClass="dropdownlist"></asp:dropdownlist></td>
					       
				        </tr>
						</table>
					</td>
					<td colspan="3" scope ="colgroup">
						<table width="100%" summary ="table">
						 <tr>
						        <td colspan="2" scope ="colgroup"></td>
						    </tr>
                            <tr>
                            <td>
                            <table width="100%" id="spandisplay2" runat="server" visible="false" >
                            <tr>
								<td class="label" style="width: 154px; color :Black " scope ="col" ><strong><asp:Label ID="lbl4" Text="Level 1" runat="server" ></asp:Label> </strong></td>
								<td scope ="col" style="width: 233px" ><asp:dropdownlist id="cbodept1" tabindex="5" Runat="server" CssClass="dropdownlist" ToolTip="Select DepartMent"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td class="label" scope ="col" style ="color:black"  ><strong><asp:Label ID="lbl5" Text="Level 2" runat="server" ></asp:Label> </strong></td>
								<td scope ="col" style="width: 233px" ><asp:dropdownlist id="cboclient1" tabindex="6" Runat="server" CssClass="dropdownlist" ToolTip="Select Client"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td class="label" style="width: 154px; color :Black " scope ="col" ><strong><asp:Label ID="lbl6" Text="Level 3" runat="server" ></asp:Label> </strong></td>
								<td scope ="col" style="width: 233px" ><asp:dropdownlist id="cbolob1" tabindex="7" Runat="server" CssClass="dropdownlist" ToolTip="Select Lob"></asp:dropdownlist></td>
							</tr>
                            </table>
                            </td>
                            </tr>
						  <tr>
								<td  class="label" scope ="col" style ="color:black" ><label for ="ctl00_MainPlaceHolder_cbotab2" class="label">Table II:</label></td>
								<td scope="col" style="width: 233px" ><asp:dropdownlist id="cbotab2" tabindex="8" Runat="server" CssClass="dropdownlist" ToolTip="Select Table From Update"></asp:dropdownlist></td>
							</tr>
							<tr>
							     <td  valign="top" style="width: 154px; color :Black " scope="col"   ><label for="cbocolB1" class="label">
                                     Columns II<br />
                                     Replace with columns<br />
                                 </label></td>
					        <td valign="top" scope="col" style="width: 233px" >
<label for ="ctl00_MainPlaceHolder_cbocolB1" class="label"></label>
						        <asp:dropdownlist id="cbocolB1" tabindex="10" Runat="server" CssClass="dropdownlist"></asp:dropdownlist><br/>
			<label for ="ctl00_MainPlaceHolder_cbocolB2" class="label"></label>			        
<asp:dropdownlist id="cbocolB2" tabindex="12" Runat="server" CssClass="dropdownlist"></asp:dropdownlist><br/>
						<label for ="ctl00_MainPlaceHolder_cbocolB3" class="label"></label>        
<asp:dropdownlist id="cbocolB3" tabindex="14" Runat="server" CssClass="dropdownlist"></asp:dropdownlist><br/>
<label for ="ctl00_MainPlaceHolder_cbocolB4" class="label"></label>  
						        <asp:dropdownlist id="cbocolB4" tabindex="16" Runat="server" CssClass="dropdownlist"></asp:dropdownlist><br/>
			<label for ="ctl00_MainPlaceHolder_cbocolB5" class="label"></label>			        
<asp:dropdownlist id="cbocolB5" tabindex="18" Runat="server" CssClass="dropdownlist"></asp:dropdownlist></td>
							</tr>
						</table>
					</td>
				</tr>
				
				
				<tr>
					<td colspan="6" scope ="colgroup" >&nbsp;</td>
				</tr>
			</table>
			<div id="divdesc" >
							<table style="width: 788px" summary ="Description of symbols to join the tables:">
								<tr>
									<td colspan="2" scope ="colgroup">&nbsp;<font color="navy"><strong>Description of symbols to join the tables:</strong></font></td>
								</tr>
								<tr>
									<td width="8%" scope ="col" >&nbsp;&nbsp; <font color="red">=</font></td>
									<td scope ="col" style="width: 658px"><font color="navy">: is used to include only rows where the joined fields from both 
											tables are equal.</font>
									</td>
								</tr>
								<tr>
									<td scope ="col" style ="color:black">&nbsp;&nbsp; <font color="red">*=</font></td>
									<td scope ="col" style="width: 658px" ><font color="navy">: is used to include all records from 'First' table and only 
											those records from 'Second' table where the joined fields from both tables are 
											equal.</font>
									</td>
								</tr>
								<tr>
									<td scope ="col" >&nbsp;&nbsp; <font color="red">= *</font></td>
									<td scope ="col" style="width: 658px" ><font color="navy">: is used to include all records from 'Second' table and only 
											those records from 'First' table where the joined fields from both tables are 
											equal.</font>
									</td>
								</tr>
							</table>
						</div>
				                       
				</asp:panel> 
			<table  style="width:100%" summary ="Table">
				<tr>
				<td  style="height: 26px" scope ="col" ></td></tr>
				<tr>
					<td colspan="1" align="right" scope ="colgroup" >
					<label for ="ctl00_MainPlaceHolder_txttab11" class="label"></label>	
					<input id="txttab11" readonly="readonly"  type="text" size="26" name="txttab11" runat="server" style="border-top-style: none; border-right-style: none; border-left-style: none; height: 18px; text-align: right; border-bottom-style: none"/>
					</td>
					<td colspan="1" scope ="colgroup" >
					<label for ="ctl00_MainPlaceHolder_cbocol11" class="label"></label>	
					<asp:dropdownlist id="cbocol11" tabindex="19" Runat="server" ToolTip="Select Column For Join"></asp:dropdownlist>
					</td>
					<td colspan="1" scope ="colgroup" style ="color:black" >
<label for ="ctl00_MainPlaceHolder_cbojoin1" class="label"></label>	
						<asp:dropdownlist id="cbojoin1" tabindex="20" Runat="server">
							<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
							<asp:ListItem Value="=">=</asp:ListItem>
							<asp:ListItem Value="*=">*=</asp:ListItem>
							<asp:ListItem Value="=*">=*</asp:ListItem>
							<asp:ListItem Value=">=">>=</asp:ListItem>
							<asp:ListItem Value="&gt=">>=</asp:ListItem>
							<asp:ListItem Value="<="><=</asp:ListItem>
						</asp:dropdownlist>
					</td>
					<td scope ="colgroup">
					<label for ="ctl00_MainPlaceHolder_txttab21" class="label"></label>
					<input id="txttab21"
							readonly="readonly"  type="text" size="25" name="txttab21" runat="server" style="border-top-style: none; border-right-style: none; border-left-style: none; height: 18px; text-align: right; border-bottom-style: none"/>
					<label for ="ctl00_MainPlaceHolder_cbocol21" class="label"></label>
<asp:dropdownlist id="cbocol21" tabindex="21" Runat="server" ToolTip="Select Column For Join"></asp:dropdownlist>
					</td>
				</tr>
				<tr>
					<td  colspan="1" scope ="colgroup" align="right">
					<label for ="ctl00_MainPlaceHolder_txttab12" class="label"></label>
					<input id="txttab12" readonly="readOnly"  type="text" size="26" name="txttab12" runat="server" style="border-top-style: none; border-right-style: none; border-left-style: none; height: 18px; text-align: right; border-bottom-style: none"/>							
						</td>
					<td colspan="1" scope ="colgroup">
					<label for ="ctl00_MainPlaceHolder_cbocol12" class="label"></label>
					<asp:dropdownlist id="cbocol12" tabindex="22" Runat="server" ToolTip="Select Column For Join"></asp:dropdownlist>
						</td>
					<td colspan="1" scope ="colgroup" style ="color:black">
					<label for ="ctl00_MainPlaceHolder_cbojoin2" class="label"></label>
						<asp:dropdownlist id="cbojoin2" tabindex="23" Runat="server">
							<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
							<asp:ListItem Value="=">=</asp:ListItem>
							<asp:ListItem Value="*=">*=</asp:ListItem>
							<asp:ListItem Value="=*">=*</asp:ListItem>
							<asp:ListItem Value=">=">>=</asp:ListItem>
							<asp:ListItem Value="<="><=</asp:ListItem>
						</asp:dropdownlist>
					</td>
					
					<td scope ="colgroup">
					<label for ="ctl00_MainPlaceHolder_txttab22" class="label"></label>	
					<input id="txttab22"
							readonly="readonly"  type="text" size="25" name="txttab22" runat="server" style="border-top-style: none; border-right-style: none; border-left-style: none; height: 18px; text-align: right; border-bottom-style: none"/>
						<label for ="ctl00_MainPlaceHolder_cbocol22" class="label"></label>	
						<asp:dropdownlist id="cbocol22" tabindex="24" Runat="server" ToolTip="Select Column For Join"></asp:dropdownlist>
					</td>
				</tr>
				<tr>
					<td colspan="1" align="right" scope ="colgroup">
					<label for ="ctl00_MainPlaceHolder_txttab13" class="label"></label>	
					<input id="txttab13" 
							readonly="readOnly"  type="text" size="26" name="txttab13" runat="server" style="border-top-style: none; border-right-style: none; border-left-style: none; height: 18px; text-align: right; border-bottom-style: none"/>
								</td>
					<td colspan="1" scope ="colgroup">
							<label for ="ctl00_MainPlaceHolder_cbocol13" class="label"></label>
							<asp:dropdownlist id="cbocol13" tabindex="25" Runat="server" ToolTip="Select Column For Join"></asp:dropdownlist>
							</td>
					<td colspan="1" scope ="colgroup">
	<label for ="ctl00_MainPlaceHolder_cbojoin3" class="label"></label>
						<asp:dropdownlist id="cbojoin3" tabindex="26" Runat="server">
							<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
							<asp:ListItem Value="=">=</asp:ListItem>
							<asp:ListItem Value="*=">*=</asp:ListItem>
							<asp:ListItem Value="=*">=*</asp:ListItem>
							<asp:ListItem Value=">=">>=</asp:ListItem>
							<asp:ListItem Value="<="><=</asp:ListItem>
						</asp:dropdownlist>
					</td>
					<td scope ="colgroup">
					<label for ="ctl00_MainPlaceHolder_txttab23" class="label"></label>
					<input id="txttab23" 
							readonly="readonly"  type="text" size="25" name="txttab23" runat="server" style="border-top-style: none; border-right-style: none; border-left-style: none; height: 18px; text-align: right; border-bottom-style: none"/>
			<label for ="ctl00_MainPlaceHolder_cbocol23" class="label"></label>				
<asp:dropdownlist id="cbocol23" tabindex="27" Runat="server" ToolTip="Select Column For Join"></asp:dropdownlist>
					</td>
				</tr>
				<tr>
					<td colspan="1" align="right" scope ="colgroup" >
                                        <label for ="ctl00_MainPlaceHolder_txttab14" class="label"></label>
                                           <input id="txttab14" 
							readonly="readOnly"  type="text" size="26" name="txttab14" runat="server" style="border-top-style: none; border-right-style: none; border-left-style: none; height: 18px; text-align: right; border-bottom-style: none"/>
								</td>
					<td colspan="1" scope ="colgroup">
					<label for ="ctl00_MainPlaceHolder_cbocol14" class="label"></label>
							<asp:dropdownlist id="cbocol14" tabindex="28" Runat="server" ToolTip="Select Column For Join"></asp:dropdownlist>
							</td>
					<td colspan="1" scope ="colgroup">
                                    <label for ="ctl00_MainPlaceHolder_cbojoin4" class="label"></label>
						<asp:dropdownlist id="cbojoin4" tabindex="29" Runat="server">
							<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
							<asp:ListItem Value="=">=</asp:ListItem>
							<asp:ListItem Value="*=">*=</asp:ListItem>
							<asp:ListItem Value="=*">=*</asp:ListItem>
							<asp:ListItem Value=">=">>=</asp:ListItem>
							<asp:ListItem Value="<="><=</asp:ListItem>
						</asp:dropdownlist>
					</td>
					<td scope ="colgroup">
					 <label for ="ctl00_MainPlaceHolder_txttab24" class="label"></label>
					<input id="txttab24" readonly="readonly"  type="text" size="25" name="txttab24" runat="server" style="border-top-style: none; border-right-style: none; border-left-style: none; height: 18px; text-align: right; border-bottom-style: none"/>
		<label for ="ctl00_MainPlaceHolder_cbocol24" class="label"></label>					
<asp:dropdownlist id="cbocol24" tabindex="30" Runat="server" ToolTip="Select Column For Join"></asp:dropdownlist>
					</td>
				</tr>
				<tr >
					<td colspan="1" align="right" scope ="colgroup">
					<label for ="ctl00_MainPlaceHolder_txttab15" class="label"></label>
					<input id="txttab15" 
							readonly="readOnly"  type="text" size="26" name="txttab15" runat="server" style="border-top-style: none; border-right-style: none; border-left-style: none; height: 18px; text-align: right; border-bottom-style: none"/>
								</td>
					<td colspan="1" scope ="colgroup">
                                                    <label for ="ctl00_MainPlaceHolder_cbocol15" class="label"></label>
							<asp:dropdownlist id="cbocol15" tabindex="31" Runat="server" ToolTip="Select Column For Join"></asp:dropdownlist>
							</td>
					<td colspan="1" scope ="colgroup">
 <label for ="ctl00_MainPlaceHolder_cbojoin5" class="label"></label>
						<asp:dropdownlist id="cbojoin5" tabindex="32" Runat="server">
							<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
							<asp:ListItem Value="=">=</asp:ListItem>
							<asp:ListItem Value="*=">*=</asp:ListItem>
							<asp:ListItem Value="=*">=*</asp:ListItem>
							<asp:ListItem Value=">=">>=</asp:ListItem>
							<asp:ListItem Value="<="><=</asp:ListItem>
						</asp:dropdownlist>
					</td>
					<td scope ="colgroup">
					<label for ="ctl00_MainPlaceHolder_txttab25" class="label"></label>
					<input id="txttab25" 
							readonly="readonly"  type="text" size="25" name="txttab25" runat="server" style="border-top-style: none; border-right-style: none; border-left-style: none; height: 18px; text-align: right; border-bottom-style: none"/>
				<label for ="ctl00_MainPlaceHolder_cbocol25" class="label"></label>			
<asp:dropdownlist id="cbocol25" tabindex="33" Runat="server" ToolTip="Select Column For Join"></asp:dropdownlist>
					</td>
				</tr>
				<tr>
					<td colspan="1" align="right" scope ="colgroup" >
					<label for ="ctl00_MainPlaceHolder_txttab16" class="label"></label>
					<input id="txttab16" readonly="readonly"  type="text" size="26" name="txttab16" runat="server" style="border-top-style: none; border-right-style: none; border-left-style: none; height: 18px; text-align: right; border-bottom-style: none"/>
					</td>
					<td colspan="1" scope ="colgroup">
							<label for ="ctl00_MainPlaceHolder_cbocol16" class="label"></label>
							<asp:dropdownlist id="cbocol16" tabindex="34" Runat="server" ToolTip="Select Column For Join"></asp:dropdownlist>
					</td>
					<td colspan="1" scope ="colgroup">
<label for ="ctl00_MainPlaceHolder_cbojoin6" class="label"></label>
						<asp:dropdownlist id="cbojoin6" tabindex="35" Runat="server">
							<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
							<asp:ListItem Value="=">=</asp:ListItem>
							<asp:ListItem Value="*=">*=</asp:ListItem>
							<asp:ListItem Value="=*">=*</asp:ListItem>
							<asp:ListItem Value=">=">>=</asp:ListItem>
							<asp:ListItem Value="<="><=</asp:ListItem>
						</asp:dropdownlist>
					</td >
					<td scope ="colgroup">
					<label for ="ctl00_MainPlaceHolder_txttab26" class="label"></label>
					<input id="txttab26" 
							readonly="readonly"  type="text" size="25" name="txttab26" runat="server" style="border-top-style: none; border-right-style: none; border-left-style: none; height: 18px; text-align: right; border-bottom-style: none"/>
<label for ="ctl00_MainPlaceHolder_cbocol26" class="label"></label>							
<asp:dropdownlist id="cbocol26" tabindex="36" Runat="server" ToolTip="Select Column For Join"/>
							</td>
				</tr>
				<tr>
					<td colspan="1" align="right" scope ="colgroup">
					<label for ="ctl00_MainPlaceHolder_txttab17" class="label"></label>
					<input id="txttab17" readonly="readonly"  type="text" size="26" name="txttab17" runat="server" style="border-top-style: none; border-right-style: none; border-left-style: none; height: 18px; text-align: right; border-bottom-style: none"/>
							</td>
					<td colspan="1" scope ="colgroup">
<label for ="ctl00_MainPlaceHolder_cbocol17" class="label"></label>
							<asp:dropdownlist id="cbocol17" tabindex="37" Runat="server" ToolTip="Select Column For Join"></asp:dropdownlist>
					</td>
					<td colspan="1" scope ="colgroup">
<label for ="ctl00_MainPlaceHolder_cbojoin7" class="label"></label>
						<asp:dropdownlist id="cbojoin7" tabindex="38" Runat="server">
							<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
							<asp:ListItem Value="=">=</asp:ListItem>
							<asp:ListItem Value="*=">*=</asp:ListItem>
							<asp:ListItem Value="=*">=*</asp:ListItem>
							<asp:ListItem Value=">=">>=</asp:ListItem>
							<asp:ListItem Value="<="><=</asp:ListItem>
						</asp:dropdownlist>
					</td>
					<td scope ="colgroup">
<label for ="ctl00_MainPlaceHolder_txttab27" class="label"></label>
					<input id="txttab27" type="text" size="25" name="txttab27" runat="server" style="border-top-style: none; border-right-style: none; border-left-style: none; height: 18px; text-align: right; border-bottom-style: none"/>
	<label for ="ctl00_MainPlaceHolder_cbocol27" class="label"></label>				
<asp:dropdownlist id="cbocol27" tabindex="39" Runat="server" ToolTip="Select Column For Join"></asp:dropdownlist>
					</td>
				</tr>
				<tr>
					<td colspan="1" align="right" scope ="colgroup">
						<label for ="ctl00_MainPlaceHolder_txttab18" class="label"></label>
					    <input id="txttab18" readonly="readOnly" type="text" size="26" name="txttab18" runat="server" style="border-top-style: none; border-right-style: none; border-left-style: none; height: 18px; text-align: right; border-bottom-style: none"/>
					</td>
					<td colspan="1" scope ="colgroup">
                                                <label for ="ctl00_MainPlaceHolder_cbocol18" class="label"></label>
						<asp:dropdownlist id="cbocol18" tabindex="40" Runat="server" ToolTip="Select Column For Join"></asp:dropdownlist>
					</td>
					<td colspan="1" scope ="colgroup">
                                                <label for ="ctl00_MainPlaceHolder_cbojoin8" class="label"></label>
						<asp:dropdownlist id="cbojoin8" tabindex="41" Runat="server">
							<asp:ListItem Value="--Select--" Selected="True">--Select--</asp:ListItem>
							<asp:ListItem Value="=">=</asp:ListItem>
							<asp:ListItem Value="*=">*=</asp:ListItem>
							<asp:ListItem Value="=*">=*</asp:ListItem>
							<asp:ListItem Value=">=">>=</asp:ListItem>
							<asp:ListItem Value="<="><=</asp:ListItem>
						</asp:dropdownlist>
					</td>
					<td scope ="colgroup">
                                        <label for ="ctl00_MainPlaceHolder_txttab28" class="label"></label>
					<input id="txttab28" readonly="readonly" type="text" size="25" name="txttab28" runat="server" style="border-top-style: none; border-right-style: none; border-left-style: none; height: 18px; text-align: right; border-bottom-style: none"/>
							<label for ="ctl00_MainPlaceHolder_cbocol28" class="label"></label>
							<asp:dropdownlist id="cbocol28" tabindex="42" Runat="server" ToolTip="Select Column For Join"></asp:dropdownlist>
					</td>
				</tr>
				<tr>
					<td colspan="4" scope ="colgroup">
					<label for ="ctl00_MainPlaceHolder_txthead" class="label"></label>
					<input id="txthead" class="label" readonly="readonly" type="text" size="33" value="Conditions:" name="txthead" runat="server" style="border-left-color: #ffffff; border-bottom-color: #ffffff; border-top-style: solid; border-top-color: #ffffff; border-right-style: solid; border-left-style: solid; border-right-color: #ffffff; border-bottom-style: solid; height: 12px;" /></td>
				</tr>
				<tr>
					<td colspan="4" scope ="colgroup">
						<div id="divcon1" style=" WIDTH: 100%" runat="server">
							<table width="100%" summary ="table">
								<tr>
									<td style="HEIGHT: 14px" colspan="2" scope ="colgroup">
										<label for="ctl00_MainPlaceHolder_txtcon11"></label>
									     <input id="txtcon11" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 18px; TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
											readonly="readonly" type="text" size="33" name="txtcon11" runat="server"/>
											<label for="ctl00_MainPlaceHolder_cbocolA11"></label>
										
<asp:dropdownlist id="cbocolA11" tabindex="43" Runat="server" ToolTip="Select Column For Condition"></asp:dropdownlist>
										<label for="ctl00_MainPlaceHolder_cbofunc11"></label>
										<asp:dropdownlist id="cbofunc11" tabindex="44" Runat="server">
											<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="=">=</asp:ListItem>
											<asp:ListItem Value="!=">!=</asp:ListItem>
											<asp:ListItem Value=">=">>=</asp:ListItem>
											<asp:ListItem Value="<="><=</asp:ListItem>
											<asp:ListItem Value=">">></asp:ListItem>
											<asp:ListItem Value="<"><</asp:ListItem>
										</asp:dropdownlist>
<label for="ctl00_MainPlaceHolder_txtval11"></label>
<input id="txtval11" style="WIDTH: 112px; HEIGHT: 18px" tabindex="45" type="text" size="13"
											name="txtval11" runat="server"/><input id="imageFromDate" title="Select Date" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 24px; CURSOR: hand; BORDER-BOTTOM: 0px solid"  
											onclick="ShowCalendar1(this.id);" tabindex="46" type="button" name="imageFromDate" size=""/>
									</td>
								</tr>
								<tr>
									<td colspan="2" scope ="colgroup">
									<label for ="ctl00_MainPlaceHolder_txtcon12" class="label"></label>
									<input id="txtcon12" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 18px; TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
											readonly="readonly" type="text" size="33" name="txtcon12" runat="server"/>
<label for="ctl00_MainPlaceHolder_cbocolA12"></label>									
<asp:dropdownlist id="cbocolA12" tabindex="47" Runat="server" ToolTip="Select Column For Condition"></asp:dropdownlist>
										<label for ="ctl00_MainPlaceHolder_cbofunc12" class="label"></label>
										<asp:dropdownlist id="cbofunc12" tabindex="48" Runat="server">
											<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="=">=</asp:ListItem>
											<asp:ListItem Value="!=">!=</asp:ListItem>
											<asp:ListItem Value=">=">>=</asp:ListItem>
											<asp:ListItem Value="<="><=</asp:ListItem>
											<asp:ListItem Value=">">></asp:ListItem>
											<asp:ListItem Value="<"><</asp:ListItem>
										</asp:dropdownlist>
<label for="ctl00_MainPlaceHolder_txtval12"></label>
<input id="txtval12" style="WIDTH: 112px; HEIGHT: 18px" tabindex="49" type="text" size="13"
											name="txtval12" runat="server"/><input id="imageFromDate1" title="Select Date" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 24px; CURSOR: hand; BORDER-BOTTOM: 0px solid; "
											onclick="ShowCalendar2(this.id);" tabindex="50" type="button" name="imageFromDate" size=""/>
									</td>
								</tr>
								<tr>
									<td colspan="2" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_txtcon13"></label>
									    <input id="txtcon13" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 18px; TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
											readonly="readonly" type="text" size="33" name="txtcon13" runat="server"/>
<label for="ctl00_MainPlaceHolder_cbocolA13"></label>									    
<asp:dropdownlist id="cbocolA13" tabindex="51" Runat="server" ToolTip="Select Column For Condition"></asp:dropdownlist>
			<label for="ctl00_MainPlaceHolder_cbofunc13"></label>								
<asp:dropdownlist id="cbofunc13" tabindex="52" Runat="server">
											<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="=">=</asp:ListItem>
											<asp:ListItem Value="!=">!=</asp:ListItem>
											<asp:ListItem Value=">=">>=</asp:ListItem>
											<asp:ListItem Value="<="><=</asp:ListItem>
											<asp:ListItem Value=">">></asp:ListItem>
											<asp:ListItem Value="<"><</asp:ListItem>
										</asp:dropdownlist>
<label for="ctl00_MainPlaceHolder_txtval13"></label>
<input id="txtval13" style="WIDTH: 112px; HEIGHT: 18px" tabindex="53" type="text" size="13"
											name="txtval13" runat="server"/><input id="imageFromDate2" title="Select Date" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 24px; CURSOR: hand; BORDER-BOTTOM: 0px solid; "
											onclick="ShowCalendar3(this.id);" tabindex="54" type="button" name="imageFromDate" size=""/>
									</td>
								</tr>
								<tr>
									<td colspan="2" scope ="colgroup">
<label for="ctl00_MainPlaceHolder_txtcon14"></label>
									    <input id="txtcon14" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 18px;  TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
											readonly="readonly" type="text" size="33" name="txtcon14" runat="server"/>
				<label for="ctl00_MainPlaceHolder_cbocolA14"></label>						
<asp:dropdownlist id="cbocolA14" tabindex="55" Runat="server" ToolTip="Select Column For Condition"></asp:dropdownlist>
<label for="ctl00_MainPlaceHolder_cbofunc14"></label>										
<asp:dropdownlist id="cbofunc14" tabindex="56" Runat="server">
											<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="=">=</asp:ListItem>
											<asp:ListItem Value="!=">!=</asp:ListItem>
											<asp:ListItem Value=">=">>=</asp:ListItem>
											<asp:ListItem Value="<="><=</asp:ListItem>
											<asp:ListItem Value=">">></asp:ListItem>
											<asp:ListItem Value="<"><</asp:ListItem>
										</asp:dropdownlist>
<label for="ctl00_MainPlaceHolder_txtval14"></label>
<input id="txtval14" style="WIDTH: 112px; HEIGHT: 18px" tabindex="57" type="text" size="13"
											name="txtval14" runat="server"/><input id="imageFromDate3" title="Select Date" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 24px; CURSOR: hand; BORDER-BOTTOM: 0px solid; "
											onclick="ShowCalendar4(this.id);" tabindex="58" type="button" name="imageFromDate" size=""/>
									</td>
								</tr>
								<tr>
									<td colspan="2" scope ="colgroup">
									<label for="ctl00_MainPlaceHolder_txtcon15"></label>
									    <input id="txtcon15" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 18px;  TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
											readonly="readonly" type="text" size="33" name="txtcon15" runat="server"/>
<label for="ctl00_MainPlaceHolder_cbocolA15"></label>										
<asp:dropdownlist id="cbocolA15" tabindex="59" Runat="server" ToolTip="Select Column For Condition"></asp:dropdownlist>
<label for="ctl00_MainPlaceHolder_cbofunc15"></label>										
<asp:dropdownlist id="cbofunc15" tabindex="60" Runat="server">
											<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="=">=</asp:ListItem>
											<asp:ListItem Value="!=">!=</asp:ListItem>
											<asp:ListItem Value=">=">>=</asp:ListItem>
											<asp:ListItem Value="<="><=</asp:ListItem>
											<asp:ListItem Value=">">></asp:ListItem>
											<asp:ListItem Value="<"><</asp:ListItem>
										</asp:dropdownlist>
<label for="ctl00_MainPlaceHolder_txtval15"></label>
<input id="txtval15" style="WIDTH: 112px; HEIGHT: 18px" tabindex="61" type="text" size="13"
											name="txtval15" runat="server"/><input id="imageFromDate4" title="Select Date" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 24px; CURSOR: hand; BORDER-BOTTOM: 0px solid;  "
											onclick="ShowCalendar5(this.id);" tabindex="62" type="button" name="imageFromDate" size=""/>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
				<tr>
					<td colspan="4" style="height: 186px">
						<div id="divcon2" style=" WIDTH: 100%" runat="server">
							<table width="100%" summary ="table">
								<tr>
									<td style="HEIGHT: 6px" colspan="2">
									<label for="ctl00_MainPlaceHolder_txtcon21"></label>
									    <input id="txtcon21" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 18px;  TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
											readonly="readonly" type="text" size="33" name="txtcon21" runat="server"/>
	<label for="ctl00_MainPlaceHolder_cbocolB21"></label>									
<asp:dropdownlist id="cbocolB21" tabindex="63" Runat="server" ToolTip="Select Column For Condition"></asp:dropdownlist>
								<label for="ctl00_MainPlaceHolder_cbofunc21"></label>		
								<asp:dropdownlist id="cbofunc21" tabindex="64" Runat="server">
											<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="=">=</asp:ListItem>
											<asp:ListItem Value="!=">!=</asp:ListItem>
											<asp:ListItem Value=">=">>=</asp:ListItem>
											<asp:ListItem Value="<="><=</asp:ListItem>
											<asp:ListItem Value=">">></asp:ListItem>
											<asp:ListItem Value="<"><</asp:ListItem>

										</asp:dropdownlist>
										<label for="ctl00_MainPlaceHolder_txtval21"></label>
<input id="txtval21" style="WIDTH: 112px; HEIGHT: 18px" tabindex="65" type="text" size="13"
											name="txtval21" runat="server"/><input id="imageFromDate5" title="Select Date" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 24px; CURSOR: hand; BORDER-BOTTOM: 0px solid; "
											onclick="ShowCalendar6(this.id);" tabindex="66" type="button" name="imageFromDate" size=""/>
									</td>
								</tr>
								<tr>
									<td colspan="2">
<label for="ctl00_MainPlaceHolder_txtcon22"></label>
									    <input id="txtcon22" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 18px;  TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
											readonly="readonly" type="text" size="33" name="txtcon22" runat="server"/>
					<label for="ctl00_MainPlaceHolder_cbocolB22"></label>					
<asp:dropdownlist id="cbocolB22" tabindex="67" Runat="server" ToolTip="Select Column For Condition"></asp:dropdownlist>
				<label for="ctl00_MainPlaceHolder_cbofunc22"></label>						
<asp:dropdownlist id="cbofunc22" tabindex="68" Runat="server">
											<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="=">=</asp:ListItem>
											<asp:ListItem Value="!=">!=</asp:ListItem>
											<asp:ListItem Value=">=">>=</asp:ListItem>
											<asp:ListItem Value="<="><=</asp:ListItem>
											<asp:ListItem Value=">">></asp:ListItem>
											<asp:ListItem Value="<"><</asp:ListItem>
										</asp:dropdownlist>
<label for="ctl00_MainPlaceHolder_txtval22"></label>	
<input id="txtval22" style="WIDTH: 112px; HEIGHT: 18px" tabindex="69" type="text" size="13"
											name="txtval22" runat="server"/><input id="imageFromDate6" title="Select Date" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 24px; CURSOR: hand; BORDER-BOTTOM: 0px solid;  "
											onclick="ShowCalendar7(this.id);" tabindex="70" type="button" name="imageFromDate" size=""/>
									</td>
								</tr>
								<tr>
									<td colspan="2">
<label for="ctl00_MainPlaceHolder_txtcon23"></label>
									    <input id="txtcon23" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 18px;  TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
											readonly="readonly" type="text" size="33" name="txtcon23" runat="server"/>
										<label for="ctl00_MainPlaceHolder_cbocolB23"></label>
<asp:dropdownlist id="cbocolB23" tabindex="71" Runat="server" ToolTip="Select Column For Condition"></asp:dropdownlist>
							<label for="ctl00_MainPlaceHolder_cbofunc23"></label>			
<asp:dropdownlist id="cbofunc23" tabindex="72" Runat="server">
											<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="=">=</asp:ListItem>
											<asp:ListItem Value="!=">!=</asp:ListItem>
											<asp:ListItem Value=">=">>=</asp:ListItem>
											<asp:ListItem Value="<="><=</asp:ListItem>
											<asp:ListItem Value=">">></asp:ListItem>
											<asp:ListItem Value="<"><</asp:ListItem>
										</asp:dropdownlist>
<label for="ctl00_MainPlaceHolder_txtval23"></label>
<input id="txtval23" style="WIDTH: 112px; HEIGHT: 18px" tabindex="73" type="text" size="13"
											name="txtval23" runat="server"/><input id="imageFromDate7" title="Select Date" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 24px; CURSOR: hand; BORDER-BOTTOM: 0px solid;  "
											onclick="ShowCalendar8(this.id);" tabindex="74" type="button" name="imageFromDate" size=""/>
									</td>
								</tr>
								<tr>
									<td colspan="2">
<label for="ctl00_MainPlaceHolder_txtcon24"></label>
									    <input id="txtcon24" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 18px;  TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
											readonly="readonly" type="text" size="33" name="txtcon24" runat="server"/>
			<label for="ctl00_MainPlaceHolder_cbocolB24"></label>						    
<asp:dropdownlist id="cbocolB24" tabindex="75" Runat="server" ToolTip="Select Column For Condition"></asp:dropdownlist>
		<label for="ctl00_MainPlaceHolder_cbofunc24"></label>								
<asp:dropdownlist id="cbofunc24" tabindex="76" Runat="server">
											<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="=">=</asp:ListItem>
											<asp:ListItem Value="!=">!=</asp:ListItem>
											<asp:ListItem Value=">=">>=</asp:ListItem>
											<asp:ListItem Value="<="><=</asp:ListItem>
											<asp:ListItem Value=">">></asp:ListItem>
											<asp:ListItem Value="<"><</asp:ListItem>
										</asp:dropdownlist>
										<label for ="ctl00_MainPlaceHolder_txtval24" class="label"></label>
										<input id="txtval24" style="WIDTH: 112px; HEIGHT: 18px" tabindex="77" type="text" size="13"
											name="txtval24" runat="server"/><input id="imageFromDate8" title="Select Date" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 24px; CURSOR: hand; BORDER-BOTTOM: 0px solid; "
											onclick="ShowCalendar9(this.id);" tabindex="78" type="button" name="imageFromDate" size=""/>
									</td>
								</tr>
								<tr>
									<td colspan="2">
<label for="ctl00_MainPlaceHolder_txtcon25"></label>	
									    <input id="txtcon25" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; HEIGHT: 18px;  TEXT-ALIGN: right; BORDER-BOTTOM-STYLE: none"
											readonly="readOnly"  type="text" size="33" name="txtcon25" runat="server"/>
							<label for="ctl00_MainPlaceHolder_cbocolB25"></label>			
<asp:dropdownlist id="cbocolB25" tabindex="79" Runat="server" ToolTip="Select Column For Condition"></asp:dropdownlist>
					<label for="ctl00_MainPlaceHolder_cbofunc25"></label>						
<asp:dropdownlist id="cbofunc25" tabindex="80" Runat="server">
											<asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
											<asp:ListItem Value="=">=</asp:ListItem>
											<asp:ListItem Value="!=">!=</asp:ListItem>
											<asp:ListItem Value=">=">>=</asp:ListItem>
											<asp:ListItem Value="<="><=</asp:ListItem>
											<asp:ListItem Value=">">></asp:ListItem>
											<asp:ListItem Value="<"><</asp:ListItem>
											</asp:dropdownlist>
											<label for="ctl00_MainPlaceHolder_txtval25"></label>
<input id="txtval25" style="WIDTH: 112px; HEIGHT: 18px" tabindex="81" type="text" size="13"
											name="txtval25" runat="server"/><input id="imageFromDate9" title="Select Date" style="BORDER-RIGHT: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 24px; CURSOR: hand; BORDER-BOTTOM: 0px solid; "
											onclick="ShowCalendar10();" tabindex="82" type="button" name="imageFromDate" size=""/>
									</td>
								</tr>
							</table>
						</div>
					</td>
				</tr>
		
				<tr >
				    <td colspan="4">
                        <asp:Label ID="lblmsg" runat="server" Font-Size="10pt" ForeColor="Red"></asp:Label></td>
				</tr>
                <tr>
                <td>
                <table ID="dcl" runat="server" visible="false" >
				<tr >
					<td colspan="1" style ="color:black" >
                        <strong><asp:Label ID="lbl7" Text="Level 1" runat="server" ></asp:Label> </strong>
					</td>
					<td colspan="5">
<label for="ctl00_MainPlaceHolder_cbodept2"></label>
					<asp:dropdownlist id="cbodept2" tabindex="70" Runat="server" CssClass="dropdownlist" ToolTip="Select DepartMent"></asp:dropdownlist>
					
					</td>
				</tr>
				<tr>
					<td colspan="1" style="height: 24px; color :Black " >
                       <strong><asp:Label ID="lbl8" Text="Level 2" runat="server" ></asp:Label> </strong>
                        <%--<asp:Label ID="Label1" runat="server" ForeColor="white" Text="vikasB"></asp:Label>--%>
                       </td>
                       <td colspan="5" style="height: 24px">
			<label for="ctl00_MainPlaceHolder_cboclient2"></label>
                        <asp:dropdownlist id="cboclient2" tabindex="71" Runat="server" CssClass="dropdownlist" ToolTip="Select Client"></asp:dropdownlist>
                        </td>
				</tr>
				<tr>
					<td colspan="1" style="height: 22px; color :Black " >
                        <strong><asp:Label ID="lbl9" Text="Level 3" runat="server" ></asp:Label> </strong>
                        <%--<asp:Label ID="lblspace" runat="server" ForeColor="white" Text="vikasBha"></asp:Label>--%>
                        </td>
                        <td colspan="5" style="height: 22px">
<label for="ctl00_MainPlaceHolder_cbolob2"></label>
                        <asp:dropdownlist id="cbolob2" tabindex="72" Runat="server" CssClass="dropdownlist"></asp:dropdownlist></td>
				</tr>
				</table>
                </td>
                </tr>
				
                <tr>
                    <%--<td colspan="1" style ="color:black">
                        <label for="ctl00_MainPlaceHolder_chklocal"  class="label" visible="false">Local</label></td>--%>
                    <td>
                        <asp:CheckBox ID="chklocal" runat="server" Visible="False" />
                    </td>
                </tr>
				<tr>
					<td colspan="1" style ="color:black" >
                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Name" ToolTip="Enter Name"></asp:Label>
						 </td>
						 <td colspan="3">
						<label for="ctl00_MainPlaceHolder_txtname"></label>
						 <asp:textbox id="txtname" tabindex="83" Runat="server" MaxLength="50" CssClass="text" ToolTip="Enter Name" Width="145px" ></asp:textbox>&nbsp;
						 <asp:Button ID="CmdSave" runat="server" Text="Save As" CssClass="button" OnClientClick ="return chkvalidsave();" Visible="False"  />&nbsp;
                        <asp:Button ID="Cmdup" runat="server" CssClass="button" Text="Update Table"  OnClientClick = " return chkvalidation();" Visible="False" />
                        </td>
				</tr>
			</table>
			
			<input id="hdSpanA" runat="server" type="hidden" title="Hidden Field for finding first span  Selected Values from Dropdown list"/>
			<input id="hdSpanB" runat="server" type="hidden" title="Hidden Field for finding second span Selected Values from Dropdown list"/>
			<input id ="hdSpan" runat ="server" type="hidden" title="Hidden Field for finding third span Selected Values from Dropdown list"/>
			<input id="hidQueryString" runat="server" type="hidden" title="Hidden Field for finding first span  Selected Values from Dropdown list"/>
			<input id="hidQueryString1" runat="server" type="hidden" title="Hidden Field for finding second span Selected Values from Dropdown list"/>
			<input id ="hidQueryString2" runat ="server" type="hidden" title="Hidden Field for finding third span Selected Values from Dropdown list"/>
			
			
			<input id="hfFirstTabQueryString" runat="server"  type="hidden" title="Hidden Field For Finding First Table Columns" />
			<input id ="hfSecondTabQueryString" runat="server" type ="hidden" title ="Hidden Field For Finding Second Table Columns" />
			<input id ="hfJoinTab1QueryString" runat="server" type ="hidden" title ="Hidden Field For Finding join Columns of First Table" />
			<input id ="hfJoinTab2QueryString" runat="server" type ="hidden" title ="Hidden Field For Finding join Columns of Second Table" />
			
			<input id="hfJoinTabQueryString" runat="server" type="hidden" title="Hidden Field For Join Table" />
			<input id ="hfconTab1QueryString" runat="server" type="hidden" title="Hidden filed for finding conditonal column of first table " />
			<input id="hfconTab2QueryString" runat="server" type="hidden" title="Hidden filed for finding conditonal column of second table " />
			
			 <asp:HiddenField ID="hfUserType" runat="server" />
    <asp:HiddenField ID="hfUserId" runat="server" />
    
  
</asp:Content>

