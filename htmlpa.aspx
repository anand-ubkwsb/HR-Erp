<%@ Page Language="C#" AutoEventWireup="true" CodeFile="htmlpa.aspx.cs" Inherits="htmlpa" %>

<script runat="server">


</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
      <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <style type="text/css">
        .messagealert {
            width: 100%;
            position: fixed;
            top: 0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }
    </style>
  <script>
        function alertme() {
            swal("Good job!", "You clicked the button!", "success");
        }
    </script>
    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

            setTimeout(function () {
                $("#alert_div").fadeTo(2000, 500).slideUp(500, function () {
                    $("#alert_div").remove();
                });
              }, 5000);//5000=5 seconds
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="messagealert" id="alert_container">
            </div>

            <div style="margin-top: 100px; text-align:center;">
                <asp:Button ID="btnSuccess" runat="server" Text="Submit" CssClass="btn btn-success"
                            OnClick="btnSuccess_Click" />
                <asp:Button ID="btnDanger" runat="server" Text="Danger" CssClass="btn btn-danger"
                            OnClick="btnDanger_Click" />
                <asp:Button ID="btnWarning" runat="server" Text="Warning" CssClass="btn btn-warning"
                            OnClick="btnWarning_Click" />
                <asp:Button ID="btnInfo" runat="server" Text="Info" CssClass="btn btn-info"
                            OnClick="btnInfo_Click" />
                <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

            </div>
        </div>




    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
<div>

    <asp:Menu ID="Menu1" runat="server" CssClass="nav navbar-nav navbar-left" Orientation="Horizontal">
        <Items>
            <asp:MenuItem Selected="True" Text="Home" Value="Home"></asp:MenuItem>
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



   


    <a href="frmlogin.aspx" onclick="sessionremove()" runat="server">clickme</a>


<script type='text/javascript'>

function sessionremove()
{
    sessionStorage.removeItem("userid");
}

</script>














    <asp:Label ID="lbl_time" runat="server" Text="Label"></asp:Label>

    <asp:Button ID="btn_time" runat="server" Text="time duration" OnClick="btn_time_Click" />









    

     </form>














    </body>
</html>