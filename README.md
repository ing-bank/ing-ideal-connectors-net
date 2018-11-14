# ing-ideal-connectors-net
Opensource tools and API to connect webshops and merchants to ING using iDeal

[![MyGet Release](https://img.shields.io/myget/ckolabs/v/iDealAdvancedConnector.svg)](https://www.myget.org/feed/ckolabs/package/nuget/iDealAdvancedConnector)


## Checkout.com related notes

This is a spike to port [iDealAdvancedConnector](iDealAdvancedConnector) to .NET Standard 2.0. All the XML related classes compile, [](iDealAdvancedConnector/Connector.cs) needs some more attention. Most notably in the following areas.

- Configuration
- Certificate loading
- HTTP requests (we want to use HttpClient)
