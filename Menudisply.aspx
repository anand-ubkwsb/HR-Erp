<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Menudisply.aspx.cs" Inherits="Menudisply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


    <style >

        .textstyle{
            padding-left:30px;
        }
        .MenuBar { font-family: "Trebuchet MS", Arial, Helvetica, sans-serif; }

        .StaticMenuItem {
            /*#1999cf bdeafe*/
            background-color: #1999cf;
            -moz-border-radius: 1px;
            -webkit-border-radius: 1px;
            font: 14pt calibri;
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            margin: auto;
            vertical-align: middle;
            background-repeat: repeat-x;
            height: 70px;
            text-align: center;
            color: white;
            padding: 5px;
        }

.StaticMenuItemStyle { color: #ffffff; padding: 20px; padding-left:25px; }

 
.StaticHoverStyle { background-color: #b6e390; color: #5f5f5f; }

.StaticSelectedStyle { background-color: #ffe99f; color: #5f5f5f; }

        .DynamicMenuItemStyle {
            background-color: #bdeafe;
            border-width: 1px;
            border-style: solid;
            border-color: #000000;
            -moz-border-radius: 1px;
            -webkit-border-radius: 1px;
            font: 13pt calibri;
            margin: auto;
            border-left: 0px solid #c1c1c1;
            border-right: 0px solid #c1c1c1;
            border-top: 0px solid #c1c1c1;
            border-bottom: 1px solid #c1c1c1;
            border-spacing: 0px;
            vertical-align: middle;
            background-repeat: repeat-x;
            height: 50px;
            text-align: left;
            color: #5f5f5f;
            padding: 5px;
        }
        /*5f5f5f*/

.DynamicHoverStyle { background-color: #eca74c; color: #ffffff; }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <header class="main-header">

                <nav class="collapse navbar-collapse pull-left">
                    <!-- Sidebar toggle button-->
         
                    <div class="container">
                      
                       <%--<a href="#menuBar_SkipLink" style="position:absolute;left:-10000px;top:auto;width:1px;height:1px;overflow:hidden;">Skip Navigation Links</a>--%>
                            <asp:Menu ID="menuBar" runat="server" Orientation="Horizontal" Width="100%" CssClass="navbar navbar-inverse textstyle" MaximumDynamicDisplayLevels="10" StaticMenuItemStyle-HorizontalPadding="25">
                                <StaticMenuStyle CssClass="nav navbar-nav navbar-left" />
                                <StaticMenuItemStyle CssClass="nav navbar-nav navbar-left" />
                                <StaticHoverStyle CssClass="StaticHoverStyle" />
                                <StaticSelectedStyle CssClass="StaticSelectedStyle" />
                                <DynamicMenuItemStyle CssClass="DynamicMenuItemStyle " />
                                <DynamicHoverStyle CssClass="DynamicHoverStyle" />
                            </asp:Menu>
            
                    </div>

                </nav>
            </header>
    <br />
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />


</asp:Content>

