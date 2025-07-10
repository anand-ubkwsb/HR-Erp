<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="User_Co_Unit_Br_Fyear.aspx.cs" Inherits="User_Co_Unit_Br_Fyear" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style2 {
            height: 24px;
        }
    </style>

    <link href="tbl.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" >
    <div class="row">


        <%--  --%>
        <div class="col-md-12" style="display:flex;flex-flow:row wrap;justify-content:center; align-items:center;">
            <!-- Horizontal Form -->
            <div class="box box-solid shadowbox" style="width:40%;background-color: #026dbd; border-radius: 15px;">
                <div class="box-header with-border">
                    <h3 class="box-title" style="color: #fff; padding-left: 10px;"></h3>
                </div>
                <!-- /.box-header -->
                <!-- form start -->
                <form class="form-horizontal">

                    <div class="box-body" style="background-color:  #cddbf2;">


                        <div class="col-md-12">
                            
                                
                            <div class="table-responsive table-bordered">
                                <table class="table">
                                    <tr>
                                        <td class="auto-style2">
                                            <asp:Label ID="lblGoCompany" runat="server" Font-Size="18px" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <%--<tr>
                        <th class="auto-style2">Company:</th>
                        <td class="auto-style2">
                            <asp:Label ID="lblCompany" runat="server" Font-Size="18px"></asp:Label>
                        </td>
                    </tr>--%>
                                    <tr>
                                        <td>
                                            <asp:TreeView ID="TreeView1" runat="server" ImageSet="Arrows" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" ShowLines="True" Width="266px" >
                                                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                                <NodeStyle Font-Names="Verdana" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" Font-Underline="False" Font-Strikeout="False" />
                                                <ParentNodeStyle Font-Size="12pt" Font-Bold="true" Font-Strikeout="False" />
                                                <RootNodeStyle Font-Overline="False" Font-Size="12pt" Font-Bold="true" ForeColor="Blue" />

                                                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
                                            </asp:TreeView>
                                            </td>
                                    </tr>


                                </table>

                            </div>
                                  
                        </div>
                    </div>
                </form>
            </div>
        </div>



    </div>
    <!-- /.col -->

  
    <br />
    <br />
    <br />
    <div class="row" id="Div1" runat="server" visible="false">
        <div class="col-md-12 " style="display:flex; flex-flow:row wrap; justify-content:center; align-items:center;">


            <!-- Horizontal Form -->
            <div class="box box-solid shadowbox" style="width:40%;background-color: #026dbd; border-radius: 15px;">

                <form class="form-horizontal">

                    <div class="box-body" style="background-color: #cddbf2;">
                        <div class="col-md-12">
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>


                   

                </form>

            </div>


        </div>


    </div>



    





</asp:Content>

