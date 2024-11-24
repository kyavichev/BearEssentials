# BearEssentials

## Adding to a Unity project
- Open Packages/manifest.json
- Add this package to the list of the dependencies:
```json
  "com.bears.bearessentials": "https://github.com/kyavichev/BearEssentials.git?path=/BearEssentials/Assets",
```

## Using Versioned URL
While it is fine to use the above the URL, it is unclear when Unity will check if there is an update for this package, so I prefer to use a versioned URL:
```json
  "com.bears.bearessentials": "https://github.com/kyavichev/BearEssentials.git?path=/BearEssentials/Assets#bear-essentials-1.0.4",
```
