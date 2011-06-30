<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test</title>
</head>
<body>
    <form id="form1" runat="server"> 
    <asp:Label ID="lblTitle" runat="server"></asp:Label>
    <div>
        <asp:Repeater ID="rptSubscribers" runat="server">
            <ItemTemplate>
            test
                Id:&nbsp;<asp:Label ID="lbl1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SubscriberId") %>' /><br />
                Email:&nbsp;<asp:Label ID="lbl2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PrimaryEmailAddress") %>' /><br />
                First Name:&nbsp;<asp:Label ID="lbl3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FirstName") %>' /><br />
                Last Name:&nbsp;<asp:Label ID="lbl14" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastName") %>' /><br /><br />
            </ItemTemplate>
        </asp:Repeater>
    <asp:Label ID="lblSome" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
