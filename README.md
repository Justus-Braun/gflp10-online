# gflp10-online
GFLP10-Online FiveM Server

To boot up server first create a setup.cfg with the following and add an licenseKey
```
## You CAN edit the following:
sv_maxclients 48
set steam_webApiKey "none"
sets tags "default, deployer"

## You MAY edit the following:
sv_licenseKey ""
sv_hostname "GFLP10-Online built with CFX Default by Tabarra!"
sets sv_projectName "[CFX Default] GFLP10-Online"
sets sv_projectDesc "Recipe for the base resources required to run a minimal FiveM server."
sets locale "root-AQ" 
```

Also create an roles.cfg file and add following
```
## Add system admins
add_ace group.admin command allow # allow all commands
add_ace group.admin command.quit deny # but don't allow quit
````

create config.json in \resources\[source]\[disocrd]\discordroles containing
```
{
  "debug": false,
  "version": "1.0.2",
  "discordData": {
    "token": "",
    "guild": ""
  }
}
```
