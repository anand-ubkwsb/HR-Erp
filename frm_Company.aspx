<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_Company.aspx.cs" Inherits="frm_Company" %>

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

        .fas {
            top: 0px;
            display: inline;
            font: normal normal normal 14px/1 FontAwesome;
            font-size: 20px;
            text-rendering: auto;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }

        .fa-save, .fa-edit, .fa-trash, .fa-search, .fa-remove {
            font-size: 14px;
        }

        .backcolorlbl {
            height: 32px;
            width: 250px;
            /*background-color:#fbfcf2;*/
            background-color: #eeeeee;
        }

        .padspace {
            padding-bottom: 7px;
        }

        .auto-style1 {
            height: 20px;
            padding-bottom: 7px;
        }

        .auto-style3 {
            width: 173px;
            padding-bottom: 7px;
        }

        .auto-style4 {
            height: 20px;
            width: 239px;
            padding-bottom: 7px;
        }

        .auto-style5 {
            width: 239px;
            padding-bottom: 7px;
        }

        .auto-style6 {
            width: 173px;
        }

        .auto-style7 {
            height: 20px;
            padding-bottom: 7px;
            width: 296px;
        }

        .auto-style8 {
            padding-bottom: 7px;
            width: 296px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%-- Form Start --%>
    <section class="content">
        <div class="row">
            <div class="col-lg-12">

                <div class="box box-solid shadowbox " style="border-radius: 15px;">
                    <div class="box-header with-border">
                        <h3 class="box-title" style="color: #fff; padding-left: 10px;">User Manager Form</h3>


                        <asp:LinkButton ID="btnInsert" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnInsert_Click" ValidationGroup="changest">
                     <i class="fas fa-save"></i>&nbsp Add
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnSave" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnSave_Click">
                     <i class="fas fa-save"></i>&nbsp Save
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnEdit" Height="40" CssClass="btn btn-app " runat="server" data-toggle="modal" data-target="#modal_edit" Style="left: 180px; background-color: #1f4f8a; color: white;">
                     <i class="fas fa-edit"></i>&nbsp Edit
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnUpdate" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnUpdate_Click">
                     <i class="fas fa-edit"></i>&nbsp Update
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnDelete" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" data-toggle="modal" data-target="#modal_delete">
                     <i class="fas fa-trash"></i>&nbsp Delete
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnFind" Height="40" CssClass="btn btn-app " runat="server" data-toggle="modal" data-target="#modal_find" Style="left: 180px; background-color: #1f4f8a; color: white;">
                     <i class="fas fa-search"></i>&nbsp Find
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnCancel" Height="40" CssClass="btn btn-app" runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnCancel_Click">
                     <i class="fas fa-remove"></i>&nbsp Cancel
                        </asp:LinkButton>


                    </div>

                    <div class="form-row">

                        <div class="box-body" style="background-color: #cddbf2;">

                            <table id="tablecontent1" runat="server" style="width: 100%;">
                                <tr>
                                    <td class="auto-style6"><strong>User ID:</strong></td>
                                    <td class="auto-style7">
                                        <div class="backcolorlbl">
                                            <asp:Label ID="lblUserId" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Black"></asp:Label>
                                        </div>
                                    </td>
                                    <td class="auto-style4"><strong>Login Name</strong></td>
                                    <td class="auto-style1 ">
                                        <asp:TextBox ID="txtLoginName" CssClass="form-control" Width="250px" runat="server" Font-Bold="False" Font-Italic="True" ForeColor="Black"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3"><strong>Password</strong></td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtPassword" CssClass="form-control" Width="250px" runat="server" TextMode="Password" Font-Bold="False" Font-Italic="True" ForeColor="Black"></asp:TextBox>
                                    </td>
                                    <td class="auto-style5"><strong>Entry Date</strong></td>
                                    <td class="padspace">
                                        <div class="backcolorlbl">
                                            <asp:Label ID="lblEntryDate" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Black"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3"><strong>Date OF Start Work</strong></td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtDateOFStartWork" CssClass="form-control" Width="250px" runat="server" Font-Bold="False" Font-Italic="True" ForeColor="Black"></asp:TextBox>
                                    </td>
                                    <td class="auto-style5"><strong>System Date</strong></td>
                                    <td class="padspace">
                                        <div class="backcolorlbl">
                                            <asp:Label ID="lblSystemDate" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Black"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3"><strong>Remarks</strong></td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtRemarks" CssClass="form-control" Width="250px" runat="server" Font-Bold="False" Font-Italic="True" ForeColor="Black"></asp:TextBox>
                                    </td>
                                    <td class="auto-style5"><strong>Display Area</strong></td>
                                    <td class="padspace">
                                        <asp:Image ID="imgDisplayArea" runat="server" Height="85px" Width="74px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3"><strong>User Name</strong></td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtUserName" CssClass="form-control" Width="250px" runat="server" Font-Bold="False" Font-Italic="True" ForeColor="Black"></asp:TextBox>
                                    </td>
                                    <td class="auto-style5"><strong>Department</strong></td>
                                    <td class="padspace">
                                        <asp:TextBox ID="txtDepartment" CssClass="form-control" Width="250px" runat="server" Font-Bold="False" Font-Italic="True" ForeColor="Black"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3"><strong>Employee</strong></td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtEmployee" CssClass="form-control" Width="250px" runat="server" Font-Bold="False" Font-Italic="True" ForeColor="Black"></asp:TextBox>
                                    </td>
                                    <td class="auto-style5"><strong>Active</strong></td>
                                    <td class="padspace">
                                        <asp:TextBox ID="txtActive" CssClass="form-control" Width="250px" runat="server" Font-Bold="False" Font-Italic="True" ForeColor="Black"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3"><strong>Allowed Wrong Attempts</strong></td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtAllowedWrongAttempts" CssClass="form-control" Width="250px" runat="server" Font-Bold="False" Font-Italic="True" ForeColor="Black"></asp:TextBox>
                                    </td>
                                    <td class="auto-style5"><strong>No of Wrongs Attempts</strong></td>
                                    <td>
                                        <div class="backcolorlbl">
                                            <asp:Label ID="lblNoOfWrongsAttempts" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Black"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3"><strong>Force Password Change</strong></td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtForcePasswordChange" CssClass="form-control" Width="250px" runat="server" Font-Bold="False" Font-Italic="True" ForeColor="Black"></asp:TextBox>
                                    </td>
                                    <td class="auto-style5"><strong>Change Password Days</strong></td>
                                    <td class="padspace">
                                        <div class="backcolorlbl">
                                            <asp:Label ID="lblChangePasswordDays" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Black"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3"><strong>Remaining Days to Change Pwd</strong></td>
                                    <td class="auto-style8">
                                        <div class="backcolorlbl">
                                            <asp:Label ID="lblRemaingDaysToChangePwd" runat="server" Font-Bold="True" Font-Italic="True" ForeColor="Black"></asp:Label>
                                        </div>
                                    </td>
                                    <td class="auto-style5"><strong>Ip Address</strong></td>
                                    <td class="padspace">
                                        <asp:TextBox ID="txtIpAddress" CssClass="form-control" Width="250px" runat="server" Font-Bold="False" Font-Italic="True" ForeColor="Black"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style5"><strong>Login Status</strong></td>
                                    <td class="padspace">
                                        <asp:TextBox ID="txtLoginStatus" CssClass="form-control" Width="250px" runat="server" Font-Bold="False" Font-Italic="True" ForeColor="Black"></asp:TextBox>
                                    </td>
                                    <td class="auto-style3"><strong>Employee Left Date</strong></td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtEmployeeLeftDate" CssClass="form-control" Width="250px" runat="server" Font-Bold="False" Font-Italic="True" ForeColor="Black"></asp:TextBox>
                                    </td>
                                </tr>

                            </table>




                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer">

                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

                        </div>
                        <!-- /.box-footer -->
                    </div>
                </div>
                <!-- /.box -->


            </div>
        </div>

    </section>
    <%-- Form End --%>


</asp:Content>

