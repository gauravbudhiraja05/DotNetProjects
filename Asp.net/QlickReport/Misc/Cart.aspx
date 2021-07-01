<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="Cart.aspx.vb" Inherits="Misc_Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
<table style="margin-left:150px;margin-top:100px; background-image:url(../images/cent.jpg) ; background-repeat:no-repeat; width: 490px; height: 274px;">
<caption style ="background-color:#67A897">Product Cart</caption>
<tr>
    <td>
        <div>
                <table style="margin-left:10px">
                    <tr>
                        <td style="width: 180px;height:30px;" class="label">Purchased Product :</td>
                        <td style="width: 138px;height:30px;"><asp:Label runat="Server" ForeColor="Blue" cssclass="label" ID="productcode"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 180px;height:30px;" class="label">Product Cost:</td>
                        <td style="width: 138px;height:30px;"> <asp:Label runat="server" ID="productcost" ForeColor="Blue"   cssclass="label"  Width="243px"></asp:Label></td>
                     </tr>
                     <tr>
                        <td colspan="2" style="height: 18px" align="center"><asp:Button ID="procedpayment" Text="Proceed Payment" runat="server" CssClass="button"/> 
                        </td>
                     </tr>
                </table>
        </div>
    </td>
</tr>
</table>
</asp:Content>

