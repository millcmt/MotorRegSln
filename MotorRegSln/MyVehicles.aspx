<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyVehicles.aspx.cs" Inherits="MotorRegSln.MyVehicles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h3 class="mb-4">My Vehicles</h3>

    <asp:Button ID="btnLinkVehicle" runat="server" Text="Link a Vehicle" CssClass="btn btn-primary mb-4" OnClick="btnLinkVehicle_Click" />

    <asp:Repeater ID="rptVehicles" runat="server" OnItemCommand="rptVehicles_ItemCommand">
        <ItemTemplate>
            <div class="card mb-3 p-3" style="max-width:100%;">
                <div class="d-flex">
                
                    <!-- Car Image -->
                    <img src="/classic-car-silhouette-removebg-preview.png" class="img-fluid" 
                         style="width:140px;height:auto;margin-right:20px;" />

                    <!-- Vehicle Info -->
                    <div class="flex-grow-1">
                        <h5><%# Eval("Make") %> <%# Eval("Model") %> (<%# Eval("Year") %>)</h5>

                        <p>
                            <strong>Plate:</strong> <%# Eval("PlateNumber") %><br />
                            <strong>Chassis:</strong> <%# Eval("ChassisNumber") %><br />
                            <strong>Last Expiry:</strong> <%# Eval("LastExpiry", "{0:yyyy-MM-dd}") %>
                        </p>

                        <asp:Button ID="btnRenew" runat="server" Text="Renew Registration" 
                            CssClass="btn btn-success"
                            CommandName="Renew" CommandArgument='<%# Eval("VehicleId") %>' />
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <asp:Label ID="lblMsg" runat="server" CssClass="text-danger"></asp:Label>

</asp:Content>
