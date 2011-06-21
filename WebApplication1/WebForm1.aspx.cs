using System;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblSome.Text = "Hello New user";
            //todo: something to do later
            lblSome.Text += ". And Hello Amey Bordikar";
        }
    }
}