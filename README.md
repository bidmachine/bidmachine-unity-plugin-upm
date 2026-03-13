# BidMachine Unity Plugin

BidMachine Unity Plugin provides a cross-platform API for integrating [BidMachine](https://bidmachine.io/) ad exchange
into Unity projects. Supports Banner, Interstitial, and Rewarded ad formats on Android and iOS.

## Requirements

- Unity 2021.3+
- Android SDK 21+
- iOS 13.0+
- Xcode 15.3+

## Installation

Add the package via Unity Package Manager using the Git URL:

```
https://github.com/bidmachine/bidmachine-unity-plugin-upm.git#v3.6.0
```

## Quick Start

```csharp
BidMachine.SetLoggingEnabled(true);
BidMachine.SetTestMode(true);
BidMachine.Initialize("YOUR_SELLER_ID");
```

## Documentation

- [Integration Guide](Documentation~/Index.md)
- [Changelog](CHANGELOG.md)
- [Official Documentation](https://developers.bidmachine.io/sdk/general/unity/overview)

## License

[Apache License 2.0](LICENSE.md)
