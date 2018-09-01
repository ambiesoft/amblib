# C# library


## Save and load form location and size
```
// save location and size
HashIni^ ini = Profile::ReadAll(IniPath);
AmbLib::SaveFormXYWH(this, "option", ini);
...
// load location and size, then apply it
AmbLib::LoadFormXYWH(this, "option", ini);
```
