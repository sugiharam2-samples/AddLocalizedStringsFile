# How to add Localizable.strings automatically on iOS build

I would like to add `Localizable.strings` in the post process when building iOS.  
The purpose is to localize push notifications on the app side.

I noticed that `Localization package` outputs `InfoPlist.strings`.  
So I implemented this using the Editor code used there.

```
Localization 1.0.0-pre.9
Packages/com.unity.localization/Editor/Platform/iOS/BuildPlayerIOS.cs
Packages/com.unity.localization/Editor/Platform/iOS/PBXProjectExtensions.cs
Packages/com.unity.localization/Editor/Platform/iOS/Player.cs
```

Here is a project made with the smallest code.  
https://github.com/sugiharam2-samples/AddLocalizedStringsFile

However, it works fine only if the file name is InfoPlist.strings, otherwise it gives unusual results.

![ss01](https://user-images.githubusercontent.com/5576470/120090086-c6a68d80-c13a-11eb-8030-2369b2fa5316.png)

![ss02](https://user-images.githubusercontent.com/5576470/120090109-f0f84b00-c13a-11eb-891d-80f829cf3d8c.png)

![ss03](https://user-images.githubusercontent.com/5576470/120090111-f48bd200-c13a-11eb-9aba-22f50b2fed73.png)

For some reason, only `InfoPlist.strings` succeeds, even if I change the number of localized files and the order of the processes to be added.

![ss04](https://user-images.githubusercontent.com/5576470/120090112-f786c280-c13a-11eb-977c-46cb39b1c73c.png)

Looking at the `project.pbxproj` as text, there are these differences.

```
/* Begin PBXVariantGroup section */
        5623C57B17FDCB0900090B9E /* InfoPlist.strings */ = {
            isa = PBXVariantGroup;
            children = (
                5623C57C17FDCB0900090B9E /* en */,
                EEC3435EABD84166AA007B3B /* ja */,
            );
            name = InfoPlist.strings;
            sourceTree = "<group>";
        };
        9EE44C32BA0A7898A2CD7FCC /* Localizable.strings */ = {
            isa = PBXVariantGroup;
            children = (
                38364D61A72D3F90B77B4596 /* en */,
                74294D4599ADE29EED013266 /* ja */,
            );
            path = Localizable.strings;
            sourceTree = "<absolute>";
        };
```

Development environment
- Unity: 2021.1.6f1
- Xcode: 12.5 (12E262)
- macOS: Big Sur 11.3.1 (20E241)

How to solve this?  
Or is there another good way?

Thank you!
