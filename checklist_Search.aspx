<%@ Page Language="C#" AutoEventWireup="true" CodeFile="checklist_Search.aspx.cs" Inherits="checklist_Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <style type="text/css">
        .PromptCSS
        {
            color:DodgerBlue;
            font-size:large;
            font-style:italic;
            font-weight:bold;
            background-color:AliceBlue;
            height:25px;
            }
    </style>

     <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <title>AdminLTE 2 | Advanced form elements</title>



  

 
      <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../bower_components/searchable/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>



<script type="text/javascript">
 $(document).ready(function () {
   $("#<%=DropDownList3.ClientID%>").searchable({
      maxListSize: 200, // if list size are less than maxListSize, show them all
      maxMultiMatch: 300, // how many matching entries should be displayed
      exactMatch: false, // Exact matching on search
      wildcards: true, // Support for wildcard characters (*, ?)
      ignoreCase: true, // Ignore case sensitivity
      latency: 200, // how many millis to wait until starting search
      warnMultiMatch: 'top {0} matches ...',
      warnNoMatch: 'no matches ...',
      zIndex: 'auto'
          });
       });

    </script>


</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>



    <%--    <a href="#" data-toggle="tooltip"  title="asdsadasd">Hover over me</a>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <asp:DropDownList ID="DropDownList1" CssClass="select"   Width="250px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"></asp:DropDownList>
        
                <div style="margin-left: 40px">
        <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged1"></asp:DropDownList>
    --%>
                <br />
                    <br />
                    <asp:DropDownList ID="myselect" runat="server">
            <asp:ListItem>Select</asp:ListItem>
            <asp:ListItem>venki</asp:ListItem>
            <asp:ListItem>venu</asp:ListItem>
            <asp:ListItem>charles ven</asp:ListItem>
            <asp:ListItem>venuzila</asp:ListItem>
            <asp:ListItem>veron philender</asp:ListItem>
            <asp:ListItem>india</asp:ListItem>
            <asp:ListItem>indianven</asp:ListItem>
            <asp:ListItem>vesta</asp:ListItem>
        </asp:DropDownList>  
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
       
        <asp:LinkButton ID="LinkButton1" data-toggle="tooltip"  title="asdsadasd" runat="server">LinkButton</asp:LinkButton>        
         <asp:DropDownList ID="DropDownList3" runat="server"  OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"></asp:DropDownList>        
               
                 <br /><br />
                <br />
                
                
                 <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
               
                
       
              </ContentTemplate>
            </asp:UpdatePanel>





        



<%--<script>
  $(function () {
    //Initialize Select2 Elements
      $('.select').chosen()
})
</script>--%>


    </form>
</body>
</html>
