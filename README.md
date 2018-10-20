# ListenDir
Simple program, which listens the specified directories and sends notifications when a new file was created.
Notifications are being sent using the Telegram API via TLSharp library.

## Quick Telegram API Configuration
Telegram API isn't that easy to start. You need to do some configuration first.

1. Create a [developer account](https://my.telegram.org/) in Telegram. 
1. Goto [API development tools](https://my.telegram.org/apps) and copy **API_ID** and **API_HASH** from your account. 
1. Open ListenDir.exe.config and set:
<add key="ApiId" value="**API_ID**" />
<add key="ApiHash" value="**API_HASH**" />
