<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="MotorRegSln.AdminDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">

    <h2>Admin Dashboard</h2>
    <hr />

    <asp:Label ID="lblMessage" runat="server" CssClass="fw-bold"></asp:Label>

  <%--   VEHICLES TABLE --%>
    <div class="card p-3 mb-4">
        <div class="d-flex justify-content-between">
            <h4>All Vehicles</h4>

            <asp:Button ID="btnAddVehicle" runat="server"
                Text="Add New Vehicle"
                CssClass="btn btn-success"
                OnClick="btnAddVehicle_Click" />
        </div>

        <asp:GridView ID="gvVehicles" runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-striped"
            DataKeyNames="VehicleId"
            OnRowEditing="gvVehicles_RowEditing"
            OnRowCancelingEdit="gvVehicles_RowCancelingEdit"
            OnRowUpdating="gvVehicles_RowUpdating"
            OnRowCommand="gvVehicles_RowCommand">

            <Columns>
                <asp:BoundField DataField="PlateNumber" HeaderText="Plate" />
                <asp:BoundField DataField="ChassisNumber" HeaderText="Chassis" />
                <asp:BoundField DataField="Make" HeaderText="Make" />
                <asp:BoundField DataField="Model" HeaderText="Model" />
                <asp:BoundField DataField="Year" HeaderText="Year" />

               <%--  Edit button --%>
                <asp:CommandField ShowEditButton="True" />

                <%-- Select vehicle button --%>
                <asp:TemplateField HeaderText="Manage">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnSelect" runat="server"
                            Text="Open"
                            CssClass="btn btn-primary btn-sm"
                            CommandName="SelectVehicle"
                            CommandArgument='<%# Eval("VehicleId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

    </div>

</div>

    <%-- VEHICLE DETAILS PANEL --%>
<asp:Panel ID="pnlDetails" runat="server" CssClass="card p-3" Visible="false">

    <h3>Vehicle Details</h3>
    <asp:Label ID="lblVehicleHeader" runat="server" CssClass="fw-bold" />

    <hr />

    <%-- INSURANCE --%>
    <h4>Insurance</h4>
    Status: <asp:Label ID="lblInsStatus" runat="server" /><br />
    Valid Until: <asp:Label ID="lblInsDate" runat="server" /><br />

    <label>New Insurance Date (yyyy-mm-dd)</label>
    <asp:TextBox ID="txtNewInsDate" runat="server" CssClass="form-control w-25" />

    <asp:Calendar ID="calInsurance" runat="server" OnSelectionChanged="calInsurance_SelectionChanged"
        CssClass="border mt-2"></asp:Calendar>

    <asp:Button ID="btnUpdateInsurance" runat="server" Text="Update Insurance"
        CssClass="btn btn-primary mt-2" OnClick="btnUpdateInsurance_Click" />

    <hr />

    <%-- FITNESS --%>
    <h4>Fitness</h4>
    Status: <asp:Label ID="lblFitStatus" runat="server" /><br />
    Valid Until: <asp:Label ID="lblFitDate" runat="server" /><br />

    <label>New Fitness Date (yyyy-mm-dd)</label>
    <asp:TextBox ID="txtNewFitDate" runat="server" CssClass="form-control w-25" />

    <asp:Calendar ID="calFitness" runat="server" OnSelectionChanged="calFitness_SelectionChanged"
        CssClass="border mt-2"></asp:Calendar>

    <asp:Button ID="btnUpdateFitness" runat="server" Text="Update Fitness"
        CssClass="btn btn-primary mt-2" OnClick="btnUpdateFitness_Click" />

    <hr />

    <%-- REGISTRATION HISTORY --%>
    <h4>Registration History</h4>
    <asp:GridView ID="gvHistory" runat="server" CssClass="table table-bordered"></asp:GridView>

    <h5 class="mt-3">Add Registration (Admin Override)</h5>

    <asp:DropDownList ID="ddlMonths" runat="server" CssClass="form-control w-25 d-inline-block">
        <asp:ListItem Value="6">6 Months</asp:ListItem>
        <asp:ListItem Value="12">12 Months</asp:ListItem>
    </asp:DropDownList>

    <asp:Button ID="btnAddReg" runat="server" Text="Add Registration"
        CssClass="btn btn-success mt-2" OnClick="btnAddReg_Click" />

</asp:Panel>

    <!-- ADD VEHICLE PANEL -->
<asp:Panel ID="pnlAddVehicle" runat="server" CssClass="card p-3 mb-4" Visible="false">

    <h4>Add New Vehicle</h4>
    <hr />

    <div class="row">
        <div class="col-md-4 mb-2">
            <label>Plate Number</label>
            <asp:TextBox ID="txtPlate" runat="server" CssClass="form-control" />
        </div>

        <div class="col-md-4 mb-2">
            <label>Chassis Number</label>
            <asp:TextBox ID="txtChassis" runat="server" CssClass="form-control" />
        </div>

        <div class="col-md-4 mb-2">
            <label>Make</label>
            <asp:TextBox ID="txtMake" runat="server" CssClass="form-control" />
        </div>

        <div class="col-md-4 mb-2">
            <label>Model</label>
            <asp:TextBox ID="txtModel" runat="server" CssClass="form-control" />
        </div>

        <div class="col-md-4 mb-2">
            <label>Year</label>
            <asp:TextBox ID="txtYear" runat="server" CssClass="form-control" />
        </div>
    </div>

    <asp:Button ID="btnSaveVehicle" runat="server"
        Text="Save Vehicle"
        CssClass="btn btn-success mt-3"
        OnClick="btnSaveVehicle_Click" />

</asp:Panel>


</asp:Content>
