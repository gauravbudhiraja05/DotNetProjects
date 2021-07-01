<%--<%-
ProjectName :-  IDMS
ModuleName:-    Advance Right Management
Page Tittle:-  Edit Menu rights form
Created on :-
Created By:-   Vikas & Jitendra

- %>--%>

<%@ Page AutoEventWireup="false" CodeFile="EditMenuRights.aspx.vb" Inherits="AdvanceRightsManagement_EditMenuRights"
    Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" %>

<asp:Content ID="lftplaceholder" runat="server" ContentPlaceHolderID="LeftPlaceHolder">
    <table>
        <tr>
            <td style="width: 206px;">
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="EditMenuRights" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script language="javascript" type="text/javascript">
			
			function SelectAll(id)
        {
           
            var grid = document.getElementById("<%= gvRights.ClientID %>");           
            var cell;
            
            if (grid.rows.length > 0)
            {
               
                for (i=1; i<grid.rows.length; i++)
                {
                   
                    cell = grid.rows[i].cells[4];
                    
                   for (j=0; j<cell.childNodes.length; j++)
                    {           
                       
                        if (cell.childNodes[j].type =="checkbox")
                        {
                        
                            cell.childNodes[j].checked = document.getElementById(id).checked;
                        }
                    }
                }
            }
        }
			

  
function TABLE1_onclick() {

}

function TABLE2_onclick() {

}

    </script>

    <table class="table" summary="used to contain controls for Edit Menu Rights " id="TABLE1" onclick="return TABLE1_onclick()" style="height: 852px">
        <caption class="caption" style ="background-color:#0591D3">Edit Menu Rights</caption>
        
        <tr>
            <td  colspan ="2" align="left" style ="color:black" >
                <asp:Label ID="lblRightFor" runat="server" AssociatedControlID="tbxUserId" CssClass="label"
                    Text="UserId" ToolTip="Rights for the user shown"></asp:Label>
                <asp:TextBox ID="tbxUserId" runat="server" CssClass="textbox" ToolTip="userid  of user whose rights are shown" ReadOnly="True" BackColor="#E0E0E0"></asp:TextBox></td>
                <td></td>
        </tr>
        <tr>
        </tr>
        <tr>
            <td colspan="4" align ="left"  >
                <asp:GridView ID="gvRights" runat="server" style ="color:black" AllowPaging="True" 
                    AutoGenerateColumns="False" ShowFooter="True" ToolTip="Rights of user are shown here ">
                    <Columns>
                       
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblAutoId" runat="server" Text="AutoId" Visible="false"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAutoIdItem" runat="server" Text='<%#Eval("Autoid") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblRights" runat="server" Text="Rights"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblRightsItem" runat="server" Text='<%#Eval("Rights") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblLinks" runat="server" Text="Links" CssClass ="link"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblLink" runat="server" Text='<%#Eval("Links") %>'  ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblSubLink1" runat="server" Text="SubLink1"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSubLnk" runat="server" Text='<%#Eval("SubLinks") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblSubLink2" runat="server" Text="SubLink2"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSubLnk2" runat="server" Text='<%#Eval("SubLinks1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                            <asp:Label ID="lbchkvisible" AssociatedControlID ="chkSelectAll" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkSelectAll" runat="server" Text="SelectAll" />
                            </HeaderTemplate>
                            <ItemTemplate>
                            <asp:Label ID="lbchkvisible" AssociatedControlID ="chkdel" runat ="server"  ></asp:Label>
                                <asp:CheckBox ID="chkdel" runat="server" />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" ForeColor="Blue"
                                    Text=" delete" ToolTip="Click to delete rights" Visible="true"></asp:LinkButton>
                            </FooterTemplate>
                        </asp:TemplateField>
                       
                    </Columns>
                    <PagerStyle BackColor="#337C84" ForeColor="White" />
                    <HeaderStyle BackColor="#377C84" ForeColor="White" />
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" />
                </asp:GridView>
                <asp:Panel ID="pandelete" runat="server" BackColor="whitesmoke" BorderColor="lightgrey"
                    BorderStyle="Outset" BorderWidth="1px" Height="32px" Style="z-index: 300; left: 400px;
                    position: absolute; top: 300px" Visible="False" Width="276px">
                    <table style="width: 273px; height: 40px" width="273" summary ="used to contain control for confirmation box">
                   <tr>
                    <td class ="tableHeader" style ="background-color:#0591D3">Confirm Deletion                   
                    </td>
                    </tr>
                        <tr>
                            <td align="center" style="height: 34px; color :Black ">
                                <strong>Are you sure, you want to cancel  right? </strong>
                                </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="cmdyes" runat="server" CssClass="button" Text="Yes" Width="38px" ToolTip="click this if you want to cancel rights" />
                                <asp:Button ID="cmdno" runat="server" CssClass="button" Text="No" Width="38px" ToolTip="click this if you do not want to cancel rights"   /></td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr></tr>
        <tr></tr>
              <tr>
        <td style="width: 170px; color :Black " >
            <asp:Label ID="lblTableHeader1" runat="server" CssClass="label" Text="Edit Rights here " ToolTip="Edit section of rights"
                ></asp:Label>
                </td>
                </tr>
                <tr></tr>
                <tr></tr>
        <tr>
            <td align ="left" style="width: 170px; color :Black "   >
                <asp:Label ID="lblChoose" runat="server" AssociatedControlID="lstmainhead" CssClass="label"
                    Text="Choose Rights" ToolTip="Choose rights"></asp:Label>
            </td>
            <td align ="left" style ="color:black"  >
                <asp:Label ID="lblSubLink1" runat="server" AssociatedControlID="lstSubhead2" CssClass="label"
                    Text="SubLink1" ToolTip="choose sublink"></asp:Label>
            </td>
           
        </tr>
        <tr>
            <td align ="left"  valign ="top" style="width: 170px; height: 146px"   >
                <asp:ListBox ID="lstmainhead" runat="server" CssClass="listBox" ToolTip="Select main link" AutoPostBack="True"></asp:ListBox>&nbsp;
                
           </td>
            <td align ="left"  valign ="top" style="height: 146px"  >
                <asp:ListBox ID="lstSubhead2" runat="server" AutoPostBack="True" CssClass="listBox"
                    SelectionMode="Multiple" ToolTip="Select sublink"></asp:ListBox>
            </td>
            <td style="height: 146px">
            </td>        
            
        </tr>
        <tr>
            <td align ="left" style="width: 170px; height: 18px; color :Black "   >
                <asp:Label ID="lblLink2" runat="server" AssociatedControlID="lstsubhead1" CssClass="label"
                    Text="Link1" ToolTip="Choose link"></asp:Label>
            </td>  
       
           
            <td align="left" style="height: 18px; color :Black " >
                <asp:Label ID="lblSubLink2" runat="server" AssociatedControlID="lstSubhead3" CssClass="label"
                    Text="SubLink2" ToolTip="Choose sublink"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left"   valign ="top" style="width: 170px; height: 150px" >
                <asp:ListBox ID="lstsubhead1" runat="server" AutoPostBack="True" CssClass="listBox"
                    SelectionMode="Multiple" ToolTip="Select sublink"></asp:ListBox>&nbsp;
            </td>
            <td align="left" valign ="top" style="height: 150px"   >
           
                <asp:ListBox ID="lstSubhead3" runat="server" CssClass="listBox" SelectionMode="Multiple" ToolTip="Select sublink" AutoPostBack="True"></asp:ListBox>
            </td>
        </tr>
       
        <tr>
        </tr>
        <tr>
            <td align="right" style="width: 170px; color :Black "  >
            <asp:Label ID="spa" runat ="server" Text ="vikasbahrdwajaaa" ForeColor ="White" Visible="False" ></asp:Label>
               <asp:Button ID="btnUpdateRights" runat="server" CssClass="button" Text="Update Rights"  ToolTip="click to update rights" ValidationGroup="final" /></td>
               <td></td>
        </tr>
        <tr><td align ="center"  colspan ="2">
            &nbsp;</td>
        <td></td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server" BackColor="whitesmoke" BorderColor="lightgrey"
                    BorderStyle="Outset" BorderWidth="1px" Height="32px" Style="z-index: 300; left: 410px;
                    position: absolute; top: 1002px" Visible="False" Width="276px">
                    <table style="width: 273px; height: 40px" width="273" summary ="used to contain control for confirmation box"  border ="2" id="TABLE2" onclick="return TABLE2_onclick()">
                    <tr>
                    <td class ="tableHeader" style ="background-color:#0591D3">Confirm Deletion                   
                    </td>
                    </tr>
                        <tr>
                            <td align="center" style="height: 34px; color :Black ">
                                <strong>Are you sure, you want to cancel all other assigned right?</strong></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="Button1" runat="server" CssClass="button" Text="Yes" Width="38px" ToolTip="Click YES  if you want to cancel rights or press (alt+y)" AccessKey="y" />
                                <asp:Button ID="Button2" runat="server" CssClass="button" Text="No" Width="38px" ToolTip="click NO  if you  do not cancel the rights or press(alt+n)" AccessKey="n" /></td>
                        </tr>
                    </table>
                </asp:Panel>
</asp:Content>
<%--<%-

Changed on :-
Created By:-   Vikas & Jitendra

- %>--%>
