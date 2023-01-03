<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Async="true" Inherits="EthereumChecker.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Address:"></asp:Label>
            <asp:TextBox ID="tbxAdress" runat="server"></asp:TextBox>
            <asp:Label ID="Label3" runat="server" Text="Date"></asp:Label>
            <asp:TextBox ID="tbxTime" runat="server" TextMode="Date"></asp:TextBox>
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Search Balance" />
            <asp:Label ID="lblBalance" runat="server" Text="Balance:"></asp:Label>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Starting Block:"></asp:Label>
            <asp:TextBox ID="tbxStartingBlock" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" Text="Search Transactions" OnClick="Button1_Click" />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
                <Columns>
                    <asp:BoundField DataField="TransactionID" HeaderText="TransactionID" />
                    <asp:BoundField DataField="AdressFrom" HeaderText="From Adress" />
                    <asp:BoundField DataField="AdressTo" HeaderText="To Adress" />
                    <asp:BoundField DataField="BlockNumber" HeaderText="BlockID" />
                    <asp:BoundField DataField="Value" HeaderText="Value" />
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                <RowStyle BackColor="White" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
