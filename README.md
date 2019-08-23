# Kuna.Client
Api client library for kuna.io

## Table Of Contents

- [License](#license)
- [Kuna REST API Reference](#kuna-rest-api-reference)
- [Examples](#examples)
    - [Get my trades](#get-my-trades)
    - [Place sell order](#place-sell-order)
    - [Place buy order](#place-buy-order)
    - [Get market data](#get-market-data)
    
## License

MIT License

## Kuna REST API Reference
https://kuna.io/documents/api

## Examples

### Get my trades

```c#
var client = new KunaClient().UseKey(new KunaKey("pub", "pri")).UseMarket("btcuah");
var trades = await client.GetMyTradesAsync();

```

### Place sell order

```c#
var client = new KunaClient().UseKey(new KunaKey("pub", "pri")).UseMarket("btcuah");
var order = await client.PlaceSellOrderAsync(1m, 260000m);

```

### Place buy order

```c#
var client = new KunaClient().UseKey(new KunaKey("pub", "pri")).UseMarket("btcuah");
var order = await client.PlaceBuyOrderAsync(1m, 260000m);

```

### Get market data
```c#
var client = new KunaClient().UseKey(new KunaKey("pub", "pri")).UseMarket("btcuah");
var marketData = await client.GetMarketDataAsync();
```
