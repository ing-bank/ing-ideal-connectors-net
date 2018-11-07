# ing-ideal-connectors-net
Opensource tools and API to connect webshops and merchants to ING using iDeal

[![Traves CI Build status](https://travis-ci.org/vladimir-aleksandrov-cko/ing-ideal-connectors-net.svg?branch=master)](https://travis-ci.org/vladimir-aleksandrov-cko/ing-ideal-connectors-net#)

[![Appveyor CI Build status](https://ci.appveyor.com/api/projects/status/asev7lip46v816eo/branch/master?svg=true)](https://ci.appveyor.com/project/vladimir-aleksandrov-cko/ing-ideal-connectors-net/branch/master)

[![MyGet Release](https://img.shields.io/myget/ckolabs/vpre/iDealAdvancedConnector.svg)](https://www.myget.org/feed/ckolabs/package/nuget/iDealAdvancedConnector)


## Checkout.com related notes

This is a spike to port [iDealAdvancedConnector](iDealAdvancedConnector) to .NET Standard 2.0. All the XML related classes compile, [](iDealAdvancedConnector/Connector.cs) needs some more attention. Most notably in the following areas.

- Configuration
- Certificate loading
- HTTP requests (we want to use HttpClient)
