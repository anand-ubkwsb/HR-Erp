<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="dynamicfrm.aspx.cs" Inherits="dynamicfrm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function alertme() {
            swal("Insert Successfuly!", "record has been saved!!!", "success");
        }
    </script>
    
    <style type="text/css">
        .shadowbox {
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2), 0 6px 20px 0 rgba(0,0,0,0.19);
            background-color: #026dbd;
        }

        .gap {
            margin-bottom: 10px;
        }

        .example button {
            float: left;
            background-color: #4E3E55;
            color: white;
            border: none;
            box-shadow: none;
            font-size: 17px;
            font-weight: 500;
            font-weight: 600;
            border-radius: 3px;
            padding: 15px 35px;
            margin: 26px 5px 0 5px;
            cursor: pointer;
        }

            .example button:focus {
                outline: none;
            }

            .example button:hover {
                background-color: #33DE23;
            }

            .example button:active {
                background-color: #81ccee;
            }
    </style>


  

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="messagealert" id="alert_container">
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true">
        </asp:DropDownList>
        <br />
        <br />
        <br />
        <asp:DropDownList ID="DropDownList2" runat="server">
        </asp:DropDownList>
        <br />
    </div>

    <asp:placeholder id="placeholder1" runat="server"></asp:placeholder>
   
    <%--<div id="myalertsuccess" runat="server">
    <div class="alert alert-success" role="alert">
        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <strong>Success!</strong> <asp:Label ID="lblMSg" runat="server" Text="Label"></asp:Label>
    </div>
        </div>--%>

    <section class="content">
        <div class="row">
            <div class="col-lg-12">

                <div class="box box-solid shadowbox " style="border-radius: 15px;">
                    <div class="box-header with-border">
                        <h3 class="box-title" style="color: #fff; padding-left: 10px;">
                            <asp:Label ID="lblFrom_Title" runat="server" Text=""></asp:Label></h3>
                    </div>

                    <div class="form-row">

                        <div class="box-body" style="background-color: #cddbf2;">

                            <table id="tablecontent1" runat="server" style="width: 100%;" enableviewstate="false">
                            </table>


                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer text-center">



                            <asp:linkbutton id="btnInsert" cssclass="btn btn-app bg-navy" runat="server" onclick="btnInsert_Click1">
                     <i class="fa fa-save"></i>Insert
                 </asp:linkbutton>
                            <asp:linkbutton id="btnEdit" cssclass="btn btn-app bg-navy" runat="server">
                     <i class="fa fa-edit"></i>Edit
                 </asp:linkbutton>
                            <asp:linkbutton id="btnDelete" cssclass="btn btn-app bg-navy" runat="server">
                     <i class="fa fa-trash"></i>Delete
                 </asp:linkbutton>
                            <asp:linkbutton id="btnFind" cssclass="btn btn-app bg-navy" runat="server" onclick="btnFind_Click" data-toggle="modal" data-target="#modal-default1">
                     <i class="fa fa-search"></i>Find
                 </asp:linkbutton>
                            <asp:linkbutton id="btnCancel" cssclass="btn btn-app bg-navy" runat="server">
                     <i class="fa fa-remove"></i>Cancel
                 </asp:linkbutton>

                            <asp:linkbutton id="btnInsertField" cssclass="btn btn-app bg-navy" runat="server" data-toggle="modal" data-target="#modal-default">
                     <i class="fa fa-puzzle-piece"></i>Insert Field
                 </asp:linkbutton>


                        </div>
                        <!-- /.box-footer -->
                    </div>
                </div>
                <!-- /.box -->


            </div>
        </div>


    </section>
    <div class="modal fade" id="modal-default">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Insert New Field </h4>
                </div>
                <div class="modal-body">

                    <table style="width: 100%;">
                        <tr>
                            <td>Field Name</td>
                            <td>
                                <asp:textbox id="txtfieldname" cssclass="form-control gap" runat="server"></asp:textbox>
                            </td>

                        </tr>
                        <tr>
                            <td>field Type</td>
                            <td>
                                <asp:dropdownlist id="ddlFieldType" cssclass="form-control gap" runat="server" onselectedindexchanged="ddlFieldType_SelectedIndexChanged">
                                           </asp:dropdownlist>
                            </td>


                        </tr>
                        <tr>
                            <td>Field ID</td>
                            <td>
                                <asp:textbox id="txtField_ID" runat="server" cssclass="form-control gap"></asp:textbox>
                            </td>

                        </tr>

                        <tr>
                            <td>Hide Field</td>
                            <td>

                                <asp:radiobutton id="rbtn_Hide_Y" runat="server" text="YES" groupname="hideYorN" />

                                &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:radiobutton id="rbtn_Hide_No" runat="server" text="NO" groupname="hideYorN" />

                            </td>

                        </tr>
                    </table>


                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>

                    <asp:button id="btn_modal_add" cssclass="btn btn-primary" runat="server" text="Add Field" onclick="btn_modal_add_Click" />

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    <div class="modal fade" id="modal-default1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Insert New Field </h4>
                </div>
                <div class="modal-body">

                    <table style="width: 100%;">
                        <tr>
                            <td><strong>User ID :</strong> </td>
                            <td>
                                <asp:textbox id="TextBox1" cssclass="form-control gap" runat="server"></asp:textbox>
                            </td>

                        </tr>
                    </table>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>

                    <asp:button id="btn_Search" cssclass="btn btn-primary" runat="server" text="Search user" onclick="btn_Search_Click" />

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
   

    <asp:button id="btn_script" runat="server" text="script" OnClick="btn_script_Click" />

  

     <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>


</asp:Content>

