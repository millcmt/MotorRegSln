<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MotorRegSln.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

  

    <div class="d-flex justify-content-center align-items-start" style="min-height: 80vh;">
        <div class="card p-4 shadow rounded" style="width: 100%; max-width: 400px; margin-top: 50px;">
            
        <h3 class="text-center mb-4">Login</h3>

            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mb-2 d-block"></asp:Label>

            <!-- Username Field -->
            <div class="form-floating mb-3 position-relative">
                <span class="position-absolute" style="left: 12px; top: 50%; transform: translateY(-50%); color: #6c757d;">
                    <i class="bi bi-person-fill"></i>
                </span>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control ps-5" placeholder="Username"></asp:TextBox>
                <label for="txtUsername">Username</label>
        </div>

            <!-- Password Field -->
            <div class="form-floating mb-3 position-relative">
                <span class="position-absolute" style="left: 12px; top: 50%; transform: translateY(-50%); color: #6c757d;">
                    <i class="bi bi-lock-fill"></i>
                </span>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control ps-5" placeholder="Password"></asp:TextBox>
                <label for="txtPassword">Password</label>
        </div>

            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary w-100" OnClick="btnLogin_Click" 
                        style="transition: all 0.2s;" 
                        onmouseover="this.style.transform='scale(1.02)';" 
                        onmouseout="this.style.transform='scale(1)';" />

        <div class="text-center mt-3">
            <a href="Register.aspx">Don't have an account? Register</a>
        </div>

        </div>
    </div>




</asp:Content>
