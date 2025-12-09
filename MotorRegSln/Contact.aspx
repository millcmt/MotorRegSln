<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="MotorRegSln.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-5">
        <div class="row">
            <div class="col-lg-8 mx-auto">
                <h1 class="display-4 mb-4 text-center">Contact Us</h1>

                <div class="card shadow-sm">
                    <div class="card-body p-5">
                        <div class="row">
                            <div class="col-md-6 mb-4 mb-md-0">
                                <h4 class="mb-4">Get In Touch</h4>

                                <div class="mb-3">
                                    <i class="fas fa-map-marker-alt text-primary me-2"></i>
                                    <strong>Address:</strong><br />
                                    12 Grave Rd,<br />
                                    Kingston, Jamaica
                                </div>

                                <div class="mb-3">
                                    <i class="fas fa-phone text-primary me-2"></i>
                                    <strong>Phone:</strong> (876) 555-MVRS
                                </div>

                                <div class="mb-3">
                                    <i class="fas fa-envelope text-primary me-2"></i>
                                    <strong>Email:</strong> support@motorreg.gov.jm
                                </div>

                                <div class="mb-3">
                                    <i class="fas fa-clock text-primary me-2"></i>
                                    <strong>Hours:</strong> Mon–Fri: 8AM–4PM
                                </div>
                            </div>

                            <div class="col-md-6">
                                <h4 class="mb-4">Send Us a Message</h4>

                                <form>
                                    <div class="mb-3">
                                        <input type="text" class="form-control" placeholder="Your Name" required />
                                    </div>

                                    <div class="mb-3">
                                        <input type="email" class="form-control" placeholder="Your Email" required />
                                    </div>

                                    <div class="mb-3">
                                        <textarea class="form-control" rows="4" placeholder="Your Message" required></textarea>
                                    </div>

                                    <button type="submit" class="btn btn-primary w-100">
                                        <i class="fas fa-paper-plane me-2"></i>Send Message
                                    </button>
                                </form>
                            </div>

                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
