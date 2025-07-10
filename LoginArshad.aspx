<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginArshad.aspx.cs" Inherits="LoginArshad" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <title>AdminLTE 2 | Log in</title>
  <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
  <!-- Bootstrap 3.3.7 -->
  <link rel="stylesheet" href="../../bower_components/bootstrap/dist/css/bootstrap.min.css">
  <!-- Font Awesome -->
  <link rel="stylesheet" href="../../bower_components/font-awesome/css/font-awesome.min.css">
  <!-- Ionicons -->
  <link rel="stylesheet" href="../../bower_components/Ionicons/css/ionicons.min.css">
  <!-- Theme style -->
  <link rel="stylesheet" href="../../dist/css/AdminLTE.min.css">
  <!-- iCheck -->
  <link rel="stylesheet" href="../../plugins/iCheck/square/blue.css">

  <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->

  <!-- Google Font -->
  <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">
</head>
<body class="hold-transition login-page" runat="server">
<div class="login-box">
  <div class="login-logo">
    <a href="../../index2.html"><b>Admin</b>LTE</a>
  </div>
  <!-- /.login-logo -->
  <div class="login-box-body">
    <p class="login-box-msg">Sign in to start your session</p>

    <form  method="post" runat="server">
      <div class="form-group has-feedback">
        
<asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
        <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
      </div>
      <div class="form-group has-feedback">
       
          <asp:TextBox ID="txtPass" CssClass="form-control" runat="server"></asp:TextBox>
        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
      </div>
      <div class="row">
        <div class="col-xs-8">
          <div class="checkbox icheck">
            <label>
              <input type="checkbox"> Remember Me
            </label>
          </div>
        </div>
        <!-- /.col -->
        <div class="col-xs-4">
          
        <asp:Button ID="btnSubmit" CssClass="btn btn-primary btn-block btn-flat" runat="server" Text="Sign In" OnClick="btnSubmit_Click" />
        </div>
        <!-- /.col -->
      </div>
    </form>

    <div class="social-auth-links text-center">
      <p>- OR -</p>
      <a href="#" class="btn btn-block btn-social btn-facebook btn-flat"><i class="fa fa-facebook"></i> Sign in using
        Facebook</a>
      <a href="#" class="btn btn-block btn-social btn-google btn-flat"><i class="fa fa-google-plus"></i> Sign in using
        Google+</a>
    </div>
    <!-- /.social-auth-links -->

    <a href="#">I forgot my password</a><br>
    <a href="register.html" class="text-center">Register a new membership</a>

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


    <span>
        <div class="lalulintas">
            <span class="rambuhijau">Responsive Ad</span>
        </div>
        <style type="text/css">
            .lalulintas {
                width: 100%;
                height: 100px;
                background: #f11809;
                margin: 0 auto;
                position: relative;
            }

            .rambuhijau {
                background: #f8695f;
                position: absolute;
                display: block;
                color: rgba(0,0,0,0.2);
                text-align: center;
                text-transform: uppercase;
                letter-spacing: 2px;
                font-size: 180%;
                padding: 10px;
                margin: 5px;
                left: 0;
                right: 0;
                top: 0;
                bottom: 0;
                font-weight: 700;
                line-height: 4.4rem;
            }

            @media only screen and (max-width: 768px) {
                .rambuhijau {
                    font-size: 100%;
                }
            }
        </style>
    </span>


    <div style="max-width: 728px; height: 90px; margin: 0 auto; text-align: center; line-height: 90px; color: #000; text-transform: uppercase; font-size: 30px; font-weight: bold; cursor: pointer; border: 5px solid #ccc; box-sizing: border-box;">BANNER 728X90</div>
</body>
</html>
