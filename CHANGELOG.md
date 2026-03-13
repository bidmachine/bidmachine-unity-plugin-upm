# BidMachine Unity Plugin

## Changelog

### v3.6.0 (March 13, 2026)

+ Updated BidMachine Android SDK to v3.6.0
+ Updated BidMachine iOS SDK to v3.6.0
+ Changed minimum supported Unity version to v2021.3
+ Refactored Runtime and Editor scripts across all platforms
+ Added `AdPlacementConfig` and `BannerAdSize` support to request builders
+ Deprecated legacy banner request APIs and `IAdRequestListener`
+ Removed deprecated `UserPermissions` class
+ Renamed `BannerSize` members to IAB standard names; old names deprecated
+ Renamed public `BidMachine` fields to PascalCase; old names deprecated
+ Added `Preserve` attribute for Android callback methods
+ Fixed misc typos and bugs
