<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyVehicles.aspx.cs" Inherits="MotorRegSln.MyVehicles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h3 class="mb-4">My Vehicles</h3>

    <asp:Button ID="btnLinkVehicle" runat="server" Text="Link a Vehicle" CssClass="btn btn-primary mb-4" OnClick="btnLinkVehicle_Click" />

    <asp:Repeater ID="rptVehicles" runat="server" OnItemCommand="rptVehicles_ItemCommand">
        <ItemTemplate>
        <div class="card mb-4 shadow-sm rounded" style="max-width: 100%; overflow: hidden;">
            <div class="row g-0 align-items-center">
                
                <!-- Vehicle Image -->
                <div class="col-md-3">
                    <img src="/classic-car-silhouette-removebg-preview.png" 
                         class="img-fluid" 
                         style="width: 100%; height: 160px; object-fit: cover;" />
                </div>

                    <!-- Vehicle Info -->
                <div class="col-md-9 p-3 d-flex justify-content-between align-items-center">

                    <!-- Info -->
                    <div>
                        <h5 class="card-title mb-1"><%# Eval("Make") %> <%# Eval("Model") %> (<%# Eval("Year") %>)</h5>
                        <small class="text-muted">
                            <strong>Plate:</strong> <%# Eval("PlateNumber") %> &nbsp;|&nbsp;
                            <strong>Chassis:</strong> <%# Eval("ChassisNumber") %> &nbsp;|&nbsp;
                            <strong>Last Expiry:</strong> <%# Eval("LastExpiry", "{0:yyyy-MM-dd}") %>
                        </small>
                    </div>

                    <!-- Button -->
                    <div>
                        <asp:Button ID="btnRenew" runat="server" Text="Renew Registration" 
                            CssClass="btn btn-success"
                            CommandName="Renew" CommandArgument='<%# Eval("VehicleId") %>' />
                    </div>

                </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <asp:Button ID="btnLinkVehicle" runat="server" Text="Link a Vehicle" CssClass="btn btn-primary mb-4" OnClick="btnLinkVehicle_Click" />

    <asp:Label ID="lblMsg" runat="server" CssClass="text-danger"></asp:Label>

</asp:Content>
