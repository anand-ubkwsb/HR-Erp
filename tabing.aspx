<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="tabing.aspx.cs" Inherits="tabing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

   

    <script>
        function alertme() {
            swal("Insert Successfuly!", "record has been saved!!!", "success");
        }

        function alertDelete() {
            swal({
                title: "Are you sure?",
                text: "Once deleted, you will not be able to recover this User!",
                icon: "warning",
                buttons: true,
                dangerMode: true,
            })
            .then((willDelete) => {
                if (willDelete) {
                    swal("Poof! User has been deleted!", {
                        icon: "success",
                    });
                } else {
                    swal("User Record is safe!");
                }
            });
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
            height: 25px;
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

        .texthieht {
            height: 25px;
        }

        .auto-style4 {
            height: 20px;
            width: 90px;
            padding-bottom: 7px;
        }

        .auto-style5 {
            width: 169px;
            padding-bottom: 7px;
        }

        .auto-style6 {
            width: 113px;
        }

        .auto-style7 {
            height: 20px;
            padding-bottom: 7px;
            width: 287px;
        }


        /*tab style*/
        #tabmenu {
            clear: both;
            float: left;
            list-style: none;
            margin: 0px 10px 0 0px;
            border-bottom: 1px solid #333333; /*Remove to create tab effect*/
            width: 800px; /*If tab effect is not being used, this will define how long the bottom border will be*/
        }

            #tabmenu a {
                display: inline;
                float: left;
            }

            #tabmenu a {
                margin-right: 5px;
                margin-top:20px;
                color: #FFFFFF;
                display: block;
                float: left;
                height:40px;
                border-radius:10px 10px 0px 0px;
                font: 14px/100% Arial, Helvetica, sans-serif;
                line-height: 62px;
                text-decoration: none;
                vertical-align: middle;
                padding: 0 20px;
                background-image: url(nav-tab-bg.png);
                background-color: #1f4f8a; /*A basic background color is set incase the image will not load*/
                border: 1px solid #333333;
                border-bottom: none;
            }
        /*End tab style*/
        .auto-style9 {
            padding-bottom: 7px;
            width: 234px;
        }

        .auto-style10 {
            width: 113px;
            padding-bottom: 7px;
        }

        .auto-style11 {
            width: 287px;
        }

        .auto-style12 {
            padding-bottom: 7px;
            width: 287px;
        }

        .auto-style13 {
            height: 20px;
            width: 169px;
            padding-bottom: 7px;
        }

        .auto-style14 {
            width: 120px;
            float: left;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <div>
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="PeriodID" DataSourceID="sqldataPeriod">
           <Columns>
               <asp:BoundField DataField="PeriodID" HeaderText="PeriodID" InsertVisible="False" ReadOnly="True" SortExpression="PeriodID" />
               <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
               <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
               <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
               <asp:BoundField DataField="PeriodYearsSpan" HeaderText="PeriodYearsSpan" SortExpression="PeriodYearsSpan" />
               <asp:BoundField DataField="DbPath" HeaderText="DbPath" SortExpression="DbPath" />
               <asp:BoundField DataField="GUID" HeaderText="GUID" SortExpression="GUID" />
               <asp:BoundField DataField="EntryDate" HeaderText="EntryDate" SortExpression="EntryDate" />
               <asp:BoundField DataField="SysDate" HeaderText="SysDate" SortExpression="SysDate" />
               <asp:BoundField DataField="EntryUserId" HeaderText="EntryUserId" SortExpression="EntryUserId" />
               <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive" />
               <asp:BoundField DataField="Compid" HeaderText="Compid" SortExpression="Compid" />
           </Columns>
       </asp:GridView>
       <br />
       <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
       <br />
       <asp:SqlDataSource ID="sqldataPeriod" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT * FROM [SET_Period] WHERE ([PeriodID] = @PeriodID)">
           <SelectParameters>
               <asp:ControlParameter ControlID="TextBox1" DefaultValue="%" Name="PeriodID" PropertyName="Text" Type="Int32" />
           </SelectParameters>
       </asp:SqlDataSource>
   </div>

  


</asp:Content>

