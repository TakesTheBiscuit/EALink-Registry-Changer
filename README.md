# EALink-Registry-Changer
EALink-Registry-Changer

Origin games client inserts or updates regkey at start up
```
HKEY_CLASSES_ROOT\link2ea\shell\open\command
```

It overwrite the value of (Default) to:
```
"C:\Program Files (x86)\Origin\EALink.exe" "%1" "%2" "%3" "%4" "%5" "%6" "%7" "%8" "%9"
```

This will not allow https://github.com/TakesTheBiscuit/SWBF2-Mod-Loader to kick in.

This tool `EALink-Registry-Changer`, when running, will monitor a machine for an instance of a process named `Origin`, and once it detects it, will overwrite the value of the aforementioned registry key with value from the settings file (see `bin\Release\EALink-Registry-Changer.exe.config`)

## Settings

- hideTheApp: True (hide the window) or False (see when origin running was detected)
- registryValue: What value do you want to set (n.b suggested value: `<value>"C:\Program Files (x86)\Origin\BattlefrontIIMOU.exe" "%1" "%2" "%3" "%4" "%5" "%6" "%7" "%8" "%9""</value>`)

