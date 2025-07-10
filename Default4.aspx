<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default4.aspx.cs" Inherits="Default4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

 <link rel="stylesheet" href="../../bower_components/bootstrap/dist/css/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../../bower_components/font-awesome/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="../../bower_components/Ionicons/css/ionicons.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="../../dist/css/AdminLTE.min.css" />
    <!-- AdminLTE Skins. Choose a skin from the css/skins
       folder instead of downloading all of them to reduce the load. -->
    <link rel="stylesheet" href="../../dist/css/skins/_all-skins.min.css" />
    <style>
        .menus{
            color:aqua;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadSkinManager ID="RadSkinManager1" runat="server" RenderMode="Classic" EnableEmbeddedSkins="false" ShowChooser="True" Skin="Blue"></telerik:RadSkinManager>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <div class="modal">
                                    
                                    <div class="center">
                                        <img alt="" src="dist/img/ajax-loader.gif" />
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            
                            <ContentTemplate>

                                <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click1" />
                                <br />
                               
    

                                <telerik:RadMenu RenderMode="Classic" ID="RadMenu1" CssClass="menus" runat="server" Flow="Horizontal" EnableEmbeddedSkins="False">
                                 </telerik:RadMenu>
                               



                                <br />



                                <label class="switch">
                                    <input type="checkbox" data-toggle="push-menu" />
                                    <span class="slider round"></span>
                                </label>
                                

                                </ContentTemplate>
                                </asp:UpdatePanel>
   
        
   
    </form>
</body>
</html>