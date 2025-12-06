<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RenewRegistration.aspx.cs" Inherits="MotorRegSln.RenewRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">

    <h3>Renew Vehicle Registration</h3>
    <hr />

    <asp:Label ID="lblMessage" runat="server" CssClass="fw-bold"></asp:Label>

    <!-- VEHICLE DETAILS -->
    <div class="card mb-4 p-3">
        <h5>Vehicle Information</h5>
        <div class="row">
            <div class="col-md-4">
                <strong>Plate:</strong> <asp:Label ID="lblPlate" runat="server" />
            </div>

            <div class="col-md-4">
                <strong>Chassis:</strong> <asp:Label ID="lblChassis" runat="server" />
            </div>

            <div class="col-md-4">
                <strong>Make & Model:</strong> <asp:Label ID="lblMakeModel" runat="server" />
            </div>

            <div class="col-md-4 mt-2">
                <strong>Year:</strong> <asp:Label ID="lblYear" runat="server" />
            </div>
        </div>
    </div>

    <!-- INSURANCE STATUS -->
    <div class="card mb-4 p-3">
        <h5>Insurance Status</h5>

        <div class="mb-2">
            <strong>Status:</strong>
            <asp:Label ID="lblInsuranceStatus" runat="server" CssClass="fw-bold" />
        </div>

        <div class="mb-2">
            <strong>Valid Until:</strong>
            <asp:Label ID="lblInsuranceDate" runat="server" />
        </div>
    </div>

    <!-- FITNESS STATUS -->
    <div class="card mb-4 p-3">
        <h5>Fitness Status</h5>

        <div class="mb-2">
            <strong>Status:</strong>
            <asp:Label ID="lblFitnessStatus" runat="server" CssClass="fw-bold" />
        </div>

        <div class="mb-2">
            <strong>Valid Until:</strong>
            <asp:Label ID="lblFitnessDate" runat="server" />
        </div>
    </div>

    <!-- RENEWAL OPTIONS -->
    <div class="card p-3 mb-4">
        <h5>Renewal Options</h5>

        <asp:RadioButtonList ID="rblDuration" runat="server" RepeatDirection="Horizontal" CssClass="mt-2">
            <asp:ListItem Text="6 Months" Value="6"></asp:ListItem>
            <asp:ListItem Text="12 Months" Value="12"></asp:ListItem>
        </asp:RadioButtonList>

        <asp:Button ID="btnRenew" runat="server" Text="Confirm Renewal"
            CssClass="btn btn-success mt-3"
            OnClick="btnRenew_Click" />
    </div>

</div>

</asp:Content>
