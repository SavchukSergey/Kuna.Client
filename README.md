# Kuna.Client
Api client library for kuna.io

```c#
var client = new KunaClient().UseKey(new KunaKey("pub", "pri")).UseMarket("btcuah");
var trades = await client.GetMyTradesAsync();
```