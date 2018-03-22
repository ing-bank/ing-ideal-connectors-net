<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageRequestTransaction.aspx.cs"
    MasterPageFile="~/Site.Master" Inherits="ING.iDealSample.PageRequestTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Panel ID="PanelFuncParms" runat="server" Width="800px" BorderStyle="Solid">
            Functie-parameters:<br />
            <asp:Label ID="LabelIssuerIdText" runat="server" Text="Issuer Id:" Width="151px"></asp:Label>
            <asp:TextBox ID="TextBoxIssuerIdValue" runat="server" Width="351px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxIssuerIdValue"
                ErrorMessage="RequiredFieldValidator" Display="Dynamic">IssuerId is mandatory</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxIssuerIdValue"
                ErrorMessage="RegularExpressionValidator" ValidationExpression="[A-Z]{6,6}[A-Z2-9][A-NP-Z0-9]([A-Z0-9]{3,3}){0,1}"
                Display="Dynamic">Format for issuer id not correct?</asp:RegularExpressionValidator><br />
            <asp:Label ID="LabelPurchaseIdText" runat="server" Text="Purchase Id:" Width="151px"></asp:Label>
            <asp:TextBox ID="TextBoxPurchaseIdValue" runat="server" Width="351px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxPurchaseIdValue"
                ErrorMessage="RequiredFieldValidator" Display="Dynamic">PuchaseId is mandatory</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBoxPurchaseIdValue"
                ErrorMessage="RegularExpressionValidator" Display="Dynamic" ValidationExpression="[a-zA-Z0-9]+">PuchaseId contains illegal characters</asp:RegularExpressionValidator><br />
            <asp:Label ID="LabelAmountText" runat="server" Text="Amount:" Width="151px"></asp:Label>
            <asp:TextBox ID="TextBoxAmountValue" runat="server" Width="351px" MaxLength="12"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxAmountValue"
                ErrorMessage="RequiredFieldValidator" Display="Dynamic">Amount is mandatory</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBoxAmountValue"
                ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d+(\.\d{1,2})?$" Display="Dynamic">Amount is not a number</asp:RegularExpressionValidator><br />            
            <asp:Label ID="LabelDescriptionText" runat="server" Text="Description:" Width="151px"></asp:Label>
            <asp:TextBox ID="TextBoxDescriptionValue" runat="server" Width="351px" MaxLength="32"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxDescriptionValue"
                ErrorMessage="RequiredFieldValidator" Display="Dynamic">Description is mandatory</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TextBoxDescriptionValue"
                ErrorMessage="RegularExpressionValidator" ValidationExpression="^[-A-Za-z0-9= %*+,./&@\&quot;':;?()$]*$"
                Display="Dynamic">Description contains illegal characters</asp:RegularExpressionValidator><br />
            <asp:Label ID="LabelEntranceCodeText" runat="server" Text="Entrance Code:" Width="151px"></asp:Label>
            <asp:TextBox ID="TextBoxEntranceCodeValue" runat="server" Width="351px" MaxLength="40"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBoxEntranceCodeValue"
                ErrorMessage="RequiredFieldValidator" Display="Dynamic">Entrance Code is mandatory</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="TextBoxEntranceCodeValue"
                ErrorMessage="RegularExpressionValidator" ValidationExpression="^[-A-Za-z0-9= %*+,./&@\&quot;':;?()$]*$"
                Display="Dynamic">Entrance Code contains illegal characters</asp:RegularExpressionValidator><br />
            <asp:Label ID="LabelExpirationPeriodText" runat="server" Text="Expiration Period:"
                Width="151px"></asp:Label>
            <asp:TextBox ID="TextBoxExpirationPeriodValue" runat="server" Width="351px" MaxLength="20"></asp:TextBox>
            <br />
            <asp:Label ID="LabelMerchantReturnUrlText" runat="server" Text="Merchant Return Url:"
                Width="151px"></asp:Label>
            <asp:TextBox ID="TextBoxMerchantReturnUrlValue" runat="server" Width="351px" MaxLength="512"></asp:TextBox>
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
            <asp:Button ID="ButtonRequestTransaction" runat="server" OnClick="ButtonRequestTransaction_Click"
                Text="Request Transaction" />
        </asp:Panel>
        &nbsp;
        <asp:Panel ID="PanelResult" runat="server" Width="800px" BorderStyle="Solid">
            Resultaat:<br />
            <asp:Label ID="LabelAcquirerIdText" runat="server" Text="Acquirer Id:" Width="151px"></asp:Label>
            <asp:Label ID="LabelAcquirerIdValue" runat="server" Width="351px"></asp:Label>
            <br />
            <asp:Label ID="LabelTransactionIdText" runat="server" Text="Transaction Id:" Width="151px"></asp:Label>
            <asp:Label ID="LabelTransactionIdValue" runat="server" Width="351px"></asp:Label>
            <br />
            <asp:Label ID="LabelIssuerAuthenticationUrlText" runat="server" Text="Issuer Authentication Url:"
                Width="151px"></asp:Label>
            <asp:Label ID="LabelIssuerAuthenticationUrlValue" runat="server" Width="351px"></asp:Label>
        </asp:Panel>
        &nbsp;
        <asp:Panel ID="PanelNext" runat="server" BorderStyle="Solid" HorizontalAlign="center"
            Width="800px">
            <asp:Label ID="LabelErrorValue" runat="server" Width="800px"></asp:Label>&nbsp;
            <asp:Button ID="ButtonIssuerAuthentication" runat="server" Enabled="False" OnClick="ButtonIssuerAuthentication_Click"
                Text="Issuer Authentication" />
        </asp:Panel>
    </div>
</asp:Content>
