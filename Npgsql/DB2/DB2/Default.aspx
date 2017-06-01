

<!DOCTYPE html>
<script runat="server">

    Protected Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub
</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Привет!</h1>
        <p>Это новая веб-форма!</p>
    </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" Width="103px" />
        <asp:Table ID="Table1" runat="server" Height="134px" Width="368px">
        </asp:Table>
    </form>
</body>
</html>