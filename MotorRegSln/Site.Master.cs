using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotorRegSln
{
    public partial class SiteMaster : MasterPage
    {
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bool isLoggedIn = Session["UserId"] != null;

                navLogin.Visible = !isLoggedIn;
                navLogout.Visible = isLoggedIn;
                

                if (isLoggedIn)
                {
                 

                    // Admin
                    navAdmin.Visible = (Session["Role"] != null &&
                                        Session["Role"].ToString() == "Admin");
                }
                else
                {
                    navAdmin.Visible = false;
                }
            }
        }

    }
}