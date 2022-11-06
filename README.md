# sb-status

A simple ASP.NET Core Web App using a [reverse proxy](https://github.com/microsoft/reverse-proxy) to provide automatic redirection to working [Sponsorblock](https://sponsor.ajay.app/) servers in case the official server is kaput.

## Backup Servers

There are two backup servers currently.

1. sponsorblock.kavin.rocks
    - Lang: Rust
    - Repo: [TeamPiped/sponsorblock-mirror](https://github.com/TeamPiped/sponsorblock-mirror)
2. sb.doubleuu.win
    - Lang: Go
    - Repo: [wereii/gosb](https://github.com/wereii/gosb)

### Self-Hosting

I host this as an Azure App Service with custom domain binding.  
Using Cloudflare as my DNS, I have a CNAME recored proxied to the azure url.

### Official Links

You can point your Sponsorblock extension to: [sponsorblock.hankmccord.dev](https://sponsorblock.hankmccord.dev)
