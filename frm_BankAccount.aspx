<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_BankAccount.aspx.cs" Inherits="frm_BankAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <section class="content">
        <div class="row">
            <div class="col-lg-12">

                <div class="box box-solid shadowbox " style="border-radius: 15px;">
                    <div class="box-header with-border">
                        <h3 class="box-title" style="color: #fff; padding-left: 10px;">
                           Bank Account Form</h3>
                    </div>

                    <div class="form-row">

                        <div class="box-body" style="background-color: #cddbf2;">

                            <table id="tablecontent1" runat="server" style="width: 100%;">
                            </table>


                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer text-center">



                            <asp:linkbutton id="btnInsert" cssclass="btn btn-app bg-navy" runat="server" >
                     <i class="fa fa-save"></i>Insert
                 </asp:linkbutton>
                            <asp:linkbutton id="btnEdit" cssclass="btn btn-app bg-navy" runat="server">
                     <i class="fa fa-edit"></i>Edit
                 </asp:linkbutton>
                            <asp:linkbutton id="btnDelete" cssclass="btn btn-app bg-navy" runat="server">
                     <i class="fa fa-trash"></i>Delete
                 </asp:linkbutton>
                            <asp:linkbutton id="btnFind" cssclass="btn btn-app bg-navy" runat="server"  data-toggle="modal" data-target="#modal-default1">
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
</asp:Content>

