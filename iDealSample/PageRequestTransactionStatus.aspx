<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageRequestTransactionStatus.aspx.cs"
    Inherits="ING.iDealSample.PageRequestTransactionStatus" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Panel ID="PanelFuncParms" runat="server" Width="800px" BorderStyle="Solid">
            Functie-parameters:<br />
            <asp:Label ID="LabelTransactionIdText" runat="server" Text="Transaction Id:" Width="151px"></asp:Label>
            <asp:TextBox ID="TextBoxTransactionIdValue" runat="server" Width="351px"></asp:TextBox>
        </asp:Panel>
        &nbsp;
        <asp:Panel ID="PanelConfParms" runat="server" Width="800px" BorderStyle="Solid">
            Vanuit configuratie:<br />
            <asp:Label ID="LabelMerchantIdText" runat="server" Text="Merchant Id:" Width="151px"></asp:Label>
            <asp:Label ID="LabelMerchantIdValue" runat="server" Width="351px"></asp:Label>
            <br />
            <asp:Label ID="LabelSubIdText" runat="server" Text="Sub Id:" Width="151px"></asp:Label>
            <asp:Label ID="LabelSubIdValue" runat="server" Width="351px"></asp:Label>
        </asp:Panel>
        &nbsp;
        <asp:Panel ID="PanelAction" runat="server" Width="800px" BorderStyle="Solid" HorizontalAlign="Center">
            <asp:Button ID="ButtonRequestTransactionStatus" runat="server" OnClick="ButtonRequestTransactionStatus_Click"
                Text="Request Transaction Status" />
        </asp:Panel>
        &nbsp;
        <asp:Panel ID="PanelResult" runat="server" Width="800px" BorderStyle="Solid">
            Resultaat:<br />
            <asp:Label ID="LabelAcquirerIdText" runat="server" Text="Acquirer Id:" Width="251px"></asp:Label>
            <asp:Label ID="LabelAcquirerIdValue" runat="server" Width="351px"></asp:Label>
            <br />
            <asp:Label ID="LabelTransactionStatusText" runat="server" Text="Transaction Status:"
                Width="251px"></asp:Label>
            <asp:Label ID="LabelTransactionStatusValue" runat="server" Width="351px"></asp:Label>
            <br />
            <asp:Label ID="LabelConsumerNameText" runat="server" Text="Consumer Name:" Width="251px"></asp:Label>
            <asp:Label ID="LabelConsumerNameValue" runat="server" Width="351px"></asp:Label>
            <br />
            <asp:Label ID="LabelConsumerIBANText" runat="server" Text="Consumer IBAN:"
                Width="251px"></asp:Label>
            <asp:Label ID="LabelConsumerIBANValue" runat="server" Width="351px"></asp:Label>
            <br />
            <asp:Label ID="LabelConsumerBICText" runat="server" Text="Consumer BIC:" Width="251px"></asp:Label>
            <asp:Label ID="LabelConsumerBICValue" runat="server" Width="351px"></asp:Label>
            <br />
            <asp:Label ID="LabelAmountText" runat="server" Text="Amount:" Width="251px"></asp:Label>
            <asp:Label ID="LabelAmountValue" runat="server" Width="538px"></asp:Label>
            <br />
            <asp:Label ID="LabelCurrencyText" runat="server" Text="Currency:"  Width="251px"></asp:Label>
            <asp:Label ID="LabelCurrencyValue" runat="server" Width="538px"></asp:Label>
            <br />
            <asp:Label ID="LabelSignatureText" runat="server" Text="Signature Value:<br /><br /><br /><br /><br /><br /><br /><br />"
                Width="251px"></asp:Label>
            <asp:Label ID="LabelSignatureValue" runat="server" Width="538px"></asp:Label>
            <br />
            <asp:Label ID="LabelFingerprintText" Visible="false" runat="server" Text="Fingerprint:" Width="251px"></asp:Label>
            <asp:Label ID="LabelFingerprintValue" Visible="false" runat="server" Width="351px"></asp:Label>
        </asp:Panel>
        &nbsp;
        <asp:Panel ID="PanelNext" runat="server" BorderStyle="Solid" HorizontalAlign="center"
            Width="800px">
            <asp:Label ID="LabelErrorValue" runat="server" Width="800px"></asp:Label>&nbsp;
        </asp:Panel>
    </div>
</asp:Content>
