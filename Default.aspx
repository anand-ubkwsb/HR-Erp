<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div>
    <center>

        <asp:Button ID="btnDelete" runat="server" Text="Button"></asp:Button>
        <asp:TextBox ID="lblid" runat="server" Text=""></asp:TextBox>

    </center>
</div>
<link media="screen" rel="stylesheet" href="https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.css" />
<link media="screen" rel="stylesheet" href='https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.css' />
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bluebird/3.3.5/bluebird.min.js"></script>
<script type="text/javascript" src="https://cdn.jsdelivr.net/sweetalert2/6.3.8/sweetalert2.min.js"> </script>

<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
<script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />

<script type="text/javascript">
    $(function () {
        $("[id*=btnDelete]").on("click", function () {
            var id = $('[id*=lblid]').val();
            swal({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes',
                cancelButtonText: 'No',
                confirmButtonClass: 'btn btn-success',
                cancelButtonClass: 'btn btn-danger',
                buttonsStyling: false
            }).then(function (result) {
                if (result) {
                    $.ajax({
                        type: "POST",
                        url: "Default.aspx/DeleteClick",
                        data: "{id:" + id + "}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (r) {
                            if (r.d == "Deleted") {
                                
                                swal({
                                    type: 'error',
                                    icon: "dist/img/del.png",
                                    title: 'Deleted',
                                    text: 'Delete Data Successfully!'
                                }).then(function () {
                                    //window.location = "frm_SET_UserGrpFROM.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
                                   

                                });
                            }
                            else {
                                swal("Data Not Deleted", r.d, "success");
                            }
                        }
                    });
                }
            },
            function (dismiss) {
                if (dismiss == 'cancel') {
                    swal('Cancelled', 'No record Deleted', 'error');
                }
            });
            return false;
        });
    });
</script>
    </div>
    </form>
</body>
</html>
