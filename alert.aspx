<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"  CodeFile="alert.aspx.cs" Inherits="alert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="example">
        <button id="b3">A success message!</button>
    </div>
   
     
        <script>
            document.getElementById('b3').onclick = function () {
                swal("Good job!", "You clicked the button!", "success");
            };
        </script>
  

    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />






    <div>

    <asp:Menu ID="Menu1" runat="server" CssClass="nav navbar-nav navbar-left" Orientation="Horizontal">
        <Items>
            <asp:MenuItem  Text="Home"  Value="Home" ></asp:MenuItem>
            <asp:MenuItem Text="About us" Value="About us"></asp:MenuItem>
            <asp:MenuItem Text="Gallery" Value="Gallery"></asp:MenuItem>
            <asp:MenuItem Text="Portfolio" Value="Portfolio">
                <asp:MenuItem Text="Product" Value="Product"></asp:MenuItem>
                <asp:MenuItem Text="Documents" Value="Documents"></asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="Contact Us" Value="Contact Us"></asp:MenuItem>
        </Items>
    </asp:Menu>

</div>



    <%-- sadas --%>
    <div>

        <a href="#ContentPlaceHolder1_Menu1_SkipLink" style="position: absolute; left: -10000px; top: auto; width: 1px; height: 1px; overflow: hidden;">Skip Navigation Links</a>
        <div class="nav navbar-nav navbar-left" id="ContentPlaceHolder1_Menu1">
            <ul class="level1">
                <li><a class="level1 selected" href="#" onclick="__doPostBack(&#39;ctl00$ContentPlaceHolder1$Menu1&#39;,&#39;Home&#39;)">Home</a></li>
                <li><a class="level1" href="#" onclick="__doPostBack(&#39;ctl00$ContentPlaceHolder1$Menu1&#39;,&#39;About us&#39;)">About us</a></li>
                <li><a class="level1" href="#" onclick="__doPostBack(&#39;ctl00$ContentPlaceHolder1$Menu1&#39;,&#39;Gallery&#39;)">Gallery</a></li>
                <li><a class="popout level1" href="#" onclick="__doPostBack(&#39;ctl00$ContentPlaceHolder1$Menu1&#39;,&#39;Portfolio&#39;)">Portfolio</a><ul class="level2">
                    <li><a class="level2" href="#" onclick="__doPostBack(&#39;ctl00$ContentPlaceHolder1$Menu1&#39;,&#39;Portfolio\\Product&#39;)">Product</a></li>
                    <li><a class="level2" href="#" onclick="__doPostBack(&#39;ctl00$ContentPlaceHolder1$Menu1&#39;,&#39;Portfolio\\Documents&#39;)">Documents</a></li>
                </ul>
                </li>
                <li><a class="level1" href="#" onclick="__doPostBack(&#39;ctl00$ContentPlaceHolder1$Menu1&#39;,&#39;Contact Us&#39;)">Contact Us</a></li>
            </ul>
        </div>
        <a id="ContentPlaceHolder1_Menu1_SkipLink"></a>

    </div>

    <%-- dasdasd --%>










































    <%-- menu --%>


    <ul class="sidebar-menu" data-widget="tree">
        <li class="header">MAIN MENU</li>

        <a href="#menubar1_SkipLink" style="position: absolute; left: -10000px; top: auto; width: 1px; height: 1px; overflow: hidden;">Skip Navigation Links</a>
        <div class="sidebar-menu" id="menubar1">
            <ul class="level1 sidebar-menu ">
                <li><a class="level1 sidebar-menu " href="?UserID=359b6016-315c-4f0b-83df-cdbfcc4206f9&amp;UsergrpID=862767be-cb6e-4df3-8181-03c3df1d1906">Hospital Management System</a></li>
                <li><a class="level1 sidebar-menu " href="?UserID=359b6016-315c-4f0b-83df-cdbfcc4206f9&amp;UsergrpID=862767be-cb6e-4df3-8181-03c3df1d1906">Reports</a></li>
                <li><a class="level1 sidebar-menu " href="?UserID=359b6016-315c-4f0b-83df-cdbfcc4206f9&amp;UsergrpID=862767be-cb6e-4df3-8181-03c3df1d1906">School Management System</a></li>
                <li><a class="popout level1 sidebar-menu " href="?UserID=359b6016-315c-4f0b-83df-cdbfcc4206f9&amp;UsergrpID=862767be-cb6e-4df3-8181-03c3df1d1906">Production System</a><ul class="level2">
                    <li><a class="popout level2 DynamicMenuItemStyle" href="?UserID=359b6016-315c-4f0b-83df-cdbfcc4206f9&amp;UsergrpID=862767be-cb6e-4df3-8181-03c3df1d1906">Production System - Confectionary</a><ul class="level3">
                        <li><a class="level3 DynamicMenuItemStyle" href="?UserID=359b6016-315c-4f0b-83df-cdbfcc4206f9&amp;UsergrpID=862767be-cb6e-4df3-8181-03c3df1d1906">sub menu product</a></li>
                    </ul>
                    </li>
                    <li><a class="level2 DynamicMenuItemStyle" href="dynamicfrm.aspx?UserID=359b6016-315c-4f0b-83df-cdbfcc4206f9&amp;UsergrpID=862767be-cb6e-4df3-8181-03c3df1d1906">Production System - Petroleum</a></li>
                </ul>
                </li>
                <li><a class="level1 sidebar-menu " href="#" onclick="__doPostBack(&#39;ctl00$menubar1&#39;,&#39;&lt;li>&lt;a href = \&#39;frmlogin.aspx\&#39;>&lt;i class=\&#39;fa fa-dashboard\&#39;>&lt;/i>&lt;span>Logout&lt;/span>&lt;/a>&lt;/li>&#39;)">
                    <li><a href='frmlogin.aspx'><i class='fa fa-dashboard'></i><span>Logout</span></a></li>
                </a></li>
            </ul>
        </div>
        <a id="menubar1_SkipLink"></a>
                                 
                      
                           
                        
                        </ul>

    <%-- menu --%>















    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
</asp:Content>

