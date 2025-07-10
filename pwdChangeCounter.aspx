<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pwdChangeCounter.aspx.cs" Inherits="pwdChangeCounter" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <style>
       .countertext{
           margin-top:180px;
          margin-left:40%;
          
       }
       .alignsmall{
           margin-left:60px;
       }
   </style>
    <title></title>
 <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
  <!-- Bootstrap 3.3.7 -->
  <link rel="stylesheet" href="../../bower_components/bootstrap/dist/css/bootstrap.min.css" />
  <!-- Font Awesome -->
  <link rel="stylesheet" href="../../bower_components/font-awesome/css/font-awesome.min.css"  />
  <!-- Ionicons -->
  <link rel="stylesheet" href="../../bower_components/Ionicons/css/ionicons.min.css" />
  <!-- Theme style -->
  <link rel="stylesheet" href="../../dist/css/AdminLTE.min.css" />
  <!-- iCheck -->
  <link rel="stylesheet" href="../../plugins/iCheck/square/blue.css" />
    <link href="Style.css" rel="stylesheet" />
  <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->

  <!-- Google Font -->
  <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
</head>
<body class="hold-transition login-page bgimg" style="height:100px;" >
    <form runat="server" id="form1">
<div class="login-box">
  <div class="login-logo" >
    <a href="../../index2.html" style="color:#fff"><b>Change Password</b></a>
  </div>
  <!-- /.login-logo -->
  <div class="login-box-body">
    <p class="login-box-msg" style="font-size:24px; padding-bottom:40px; color:#000">REMANING PASSWORD DAYS COUNTER</p>
       <div class="form-group has-feedback">
           
           <asp:Label ID="lblcounterdays" runat="server" Text="0" CssClass="countertext" Font-Bold="True" Font-Size="65px" ForeColor="Black"></asp:Label>
           <br />
           <asp:Label ID="Label2" runat="server" Text="Days are left for password change" Font-Size="15px" ForeColor="Black" CssClass="alignsmall"></asp:Label>

        <%--<span class="glyphicon glyphicon-user form-control-feedback"></span>--%>
      </div>

   </div>
  <!-- /.login-box-body -->
</div>
<!-- /.login-box -->

<!-- jQuery 3 -->
<script src="../../bower_components/jquery/dist/jquery.min.js"></script>
<!-- Bootstrap 3.3.7 -->
<script src="../../bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
<!-- iCheck -->
<script src="../../plugins/iCheck/icheck.min.js"></script>
        </form>
</body>
</html>