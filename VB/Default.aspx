<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<link rel="stylesheet" type="text/css" href="Content/styles.css">
	<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
	<script type="text/javascript" src="Content/scripts.js"></script>
</head>
<body>
	<form id="form1" runat="server">
		<div class="container">
			<div class="navigation">
				<dx:ASPxNavBar ID="ItemsSelector" runat="server" AllowSelectItem="true" ClientInstanceName="navbar" AutoCollapse="true" SyncSelectionMode="None" >    
					<ItemStyle SelectedStyle-BackColor="Orange" />                                    
					<ClientSideEvents HeaderClick="OnHeaderClick" ItemClick="OnNavBarItemClick" />
				</dx:ASPxNavBar>
			</div>
			<div id="contentContainer" class="content" runat="server">
			</div>
		</div>
	</form>
</body>
</html>