<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TreeviewPage.aspx.vb" Inherits="TreeviewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
    <title>Untitled Page</title>
<script language="javascript" type="text/javascript">
// <!CDATA[



// ]]>
</script>
</head>
<body style="background-color:#59AFBB" alink="#996600">
    <form id="form1" runat="server">
    <div style="font-weight: bold; background-color:Transparent " id="DIV4" >
    <table>
				<tr>
					<td align="left" valign="top" style="width: 111px"  >
						<table class="grid">
							<tr>
								<td >
									<div id="Divdisplay" runat="server">
							<asp:TreeView id ="menu" runat="server" ExpandDepth="1"  ImageSet="Arrows" >
                                        <ParentNodeStyle Font-Bold="False" ForeColor="White"   />
                                        <HoverNodeStyle Font-Underline="True" ForeColor="Aqua" />
                                        <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" />
                                        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="White" HorizontalPadding="5px"
                                            NodeSpacing="0px" VerticalPadding="0px" />
                                    </asp:TreeView>
						

									<%--<iewc:treeview id="menu" runat="server"></iewc:treeview></div>--%>
									</div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table >
				<tr>
					<td style="width: 85px">
						<table class="grid">
							<tr>
								<td >
									<div id="Div1" runat="server">
											<asp:TreeView id ="BFI" runat="server" ExpandDepth="0" ImageSet="Arrows" ForeColor="White">
                            <ParentNodeStyle Font-Bold="False" ForeColor="White" />
                            <HoverNodeStyle Font-Underline="True" ForeColor="Aqua" />
                            <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" ForeColor="White" />
                            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="White" HorizontalPadding="5px"
                                NodeSpacing="0px" VerticalPadding="0px" />
                            <RootNodeStyle ForeColor="White" />
                        </asp:TreeView>
																	<%--	<iewc:treeview id="BFI" runat="server"></iewc:treeview>--%></div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table >
				<tr>
					<td  >
						<table class="grid" >
							<tr>
								<td>
									<div id="Div3" runat="server"><%--<iewc:treeview id="WRM" runat="server"></iewc:treeview>--%>
									<asp:TreeView id ="WRM" runat="server" ImageSet="Arrows" ExpandDepth="0">
                                        <ParentNodeStyle Font-Bold="False" ForeColor="White" />
                                        <HoverNodeStyle Font-Underline="True" ForeColor="Aqua" />
                                        <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" ForeColor="White" />
                                        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="White" HorizontalPadding="5px"
                                            NodeSpacing="0px" VerticalPadding="0px" />
                                        <RootNodeStyle ForeColor="White" />
                                    </asp:TreeView>
									</div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table >
				<tr>
					<td >
						<table class="grid" >
							<tr>
								<td style="width: 78px">
									<div id="Div2" runat="server"><%--<iewc:treeview id="WRM" runat="server"></iewc:treeview>--%>
									<asp:TreeView id ="QRC" runat="server" ImageSet="Arrows">
                                        <ParentNodeStyle Font-Bold="False" ForeColor="White" />
                                        <HoverNodeStyle Font-Underline="True" ForeColor="Aqua" />
                                        <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" ForeColor="White" />
                                        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="White" HorizontalPadding="5px"
                                            NodeSpacing="0px" VerticalPadding="0px" />
                                        <RootNodeStyle ForeColor="White" />
                                    </asp:TreeView>
									</div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
        
    
    </div>
    </form>
</body>
</html>
