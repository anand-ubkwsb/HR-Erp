<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmlogin.aspx.cs" Inherits="frmlogin"  EnableEventValidation="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <style>
        .border-control {
            padding-top:3px;
            padding-bottom:4px;

        border-top:2px solid grey;
        border-bottom:2px solid grey;
        border-right:2px solid grey;
        }
        .text_Field {
        min-width:350px;
        outline:none;
        border-right:none;

        }
        .imgstyle{
            margin-top:-10px;
           float:right;
        }
    </style>
    <title>ERP System</title>
    <!-- Vinod Change here>
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
  <link rel="stylesheet" href="Style.css"  />

    <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->    
  <!--[if lt IE 9]>
  
  <![endif]-->
  <!-- Google Font -->
  <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
</head>
<body class="hold-transition login-page bgimg" style="height:100px;" >
    <form runat="server" id="form1" defaultbutton="btnLogin">
<div class="login-box " style="width:450px;"">
  <div class="login-logo" >
    <a href="../../index2.html" style="color:#fff"><b>ERP</b>System</a>
  </div>
  <!-- /.login-logo -->
  <div class="login-box-body" style="height:400px; padding-top:50px;" >
    <p class="login-box-msg" style="font-size:24px; padding-bottom:40px; color:#000">Sign In</p>

   
      <div class="form-group " style="height:50px;">
          <input  type="text" id="txtLoginname" runat="server" class="form-control-sm text_Field" /><span class="glyphicon glyphicon-user border-control"></span>
        <%--<asp:TextBox ID="txtLoginname" runat="server"  CssClass="form-control text_Field" placeholder="User name" AutoPostBack="True" OnTextChanged="txtLoginname_TextChanged"></asp:TextBox>--%>
        <%--<span class="glyphicon glyphicon-user form-control-feedback"></span>--%>
      </div>
      <div class="form-group ">
          <input type="password" id="txtPwd"  class="form-control-sm text_Field" runat="server" name="name" value="" />
          <span class="glyphicon glyphicon-lock border-control"></span>
          <%--<asp:TextBox ID="txtPwd" TextMode="Password" runat="server" CssClass="form-control form-control-sm text_Field" placeholder="Password" TabIndex="1"></asp:TextBox>--%>
          
        
      </div>
      <div class="row">
        <div class="col-xs-8">
          <div class="checkbox icheck">
            <label>
                <asp:CheckBox ID="chk_remember" runat="server" TabIndex="2" /> Remember Me
            </label>
          </div>
        </div>
        <!-- /.col -->
        <div class="col-xs-4">
         <asp:Button ID="btnLogin" runat="server" Text="Sign In"  CssClass="btn btn-primary btn-block btn-flat" OnClick="btnLogin_Click" Font-Size="18px" TabIndex="3"/>
           
             <%-- <asp:Button ID="Button1" runat="server" Text="date In"  CssClass="btn btn-success btn-block btn-flat"  Font-Size="18px" OnClick="Button1_Click"/>--%>
          
        </div>
        <!-- /.col -->
      </div>
 
    <a  href="PasswordChange.aspx">Change password</a><br/>
    <a href="pwdChangeCounter.aspx" class="text-center">Remaining Password Days counter </a>
       <asp:Image ID="Image1" runat="server" Width="100px" Height="100px" CssClass="img-thumbnail imgstyle"/>

     <br /><br /> <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
      

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
<script>
  $(function () {
    $('input').iCheck({
      checkboxClass: 'icheckbox_square-blue',
      radioClass: 'iradio_square-blue',
      increaseArea: '20%' /* optional */
    });
  });
</script>
        </form>
</body>
</html>
