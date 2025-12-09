<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="MotorRegSln.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-5">
        <div class="row">
            <div class="col-lg-8 mx-auto text-center">
                <h1 class="display-3 fw-bold mb-4">About the Motor Vehicle Registration System</h1>
                <p class="lead mb-5">
                    The Motor Vehicle Registration Portal provides a simple, secure, and convenient way 
                    for motorists to view, link, and renew their vehicle registrations online. 
                    The system connects directly with authorized Insurance and Fitness services 
                    to ensure each renewal meets all national requirements.
                </p>
            </div>
        </div>

        <div class="row mt-5">
            <div class="col-md-6 mb-4">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-body text-center p-4">
                        <i class="fas fa-car feature-icon"></i>
                        <h4 class="fw-bold mt-3">Easy Online Renewal</h4>
                        <p class="text-muted">
                            Renew your registration from anywhere at any time. 
                            The system is designed for fast and hassle-free updates.
                        </p>
                    </div>
                </div>
            </div>

            <div class="col-md-6 mb-4">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-body text-center p-4">
                        <i class="fas fa-check-circle feature-icon"></i>
                        <h4 class="fw-bold mt-3">Automated Verification</h4>
                        <p class="text-muted">
                            The portal automatically checks your vehicle’s Insurance and Fitness status 
                            through secure web services before processing your renewal.
                        </p>
                    </div>
                </div>
            </div>

            <div class="col-md-6 mb-4">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-body text-center p-4">
                        <i class="fas fa-shield-alt feature-icon"></i>
                        <h4 class="fw-bold mt-3">Secure and Reliable</h4>
                        <p class="text-muted">
                            Your personal and vehicle information is protected using modern security standards 
                            to ensure confidentiality and system reliability.
                        </p>
                    </div>
                </div>
            </div>

            <div class="col-md-6 mb-4">
                <div class="card border-0 shadow-sm h-100">
                    <div class="card-body text-center p-4">
                        <i class="fas fa-tachometer-alt feature-icon"></i>
                        <h4 class="fw-bold mt-3">Designed for Convenience</h4>
                        <p class="text-muted">
                            Whether you manage one vehicle or many, the platform helps you stay organized 
                            by providing quick access to all your registration details.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
