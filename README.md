# ListenDir
Simple program, which listens the specified directories and sends Telegram notifications when a new file was created.<br />

Notifications are being sent using the Telegram API via TLSharp (c)

### TLSharp
Unofficial Telegram (http://telegram.org) client library implemented in C#.<br />
https://github.com/sochix/TLSharp<br />
Copyright (c) 2015 Ilya Pirozhenko http://www.sochix.ru/<br />

## Quick Telegram API Configuration
Telegram API isn't that easy to start. You need to do some configuration first.

1. Create a [developer account](https://my.telegram.org/) in Telegram. 
1. Goto [API development tools](https://my.telegram.org/apps) and copy **API_ID** and **API_HASH** from your account.
1. When user is authenticated, TLSharp creates special file called _session.dat_. In this file TLSharp store all information needed for user session. So you need to authenticate user every time the _session.dat_ file is corrupted or removed.

## Configuration
### Open ListenDir.exe.config and set:
key="FilesExtension" value="trx" - extension for files to listen<br />
key="TestResultsDir" value="C:\\test,C:\\test1,C:\\test2" - comma-separated full paths to folders<br />
key="DateTimeFormat" value="MM-dd-yyyy HH-mm-ss"<br />
key="NewFilesSearchTimeout" value="600" - timeout in seconds<br />
key="ResultsLogFolder" value=".\Logs" - full path to folder with results list<br />
key="ResultsLogFile" value="TestResults.log" - results file name<br />
key="ApiId" value="**API_ID**" - your Telegram API ID<br />
key="ApiHash" value="**API_HASH**" - your Telegram API hash<br />
key="UserPhoneNumber" value="375291223344" - phone number of user, which will send the notifications<br />
key="RecipientType" value="channel" - You can choose "channel" or "user"<br />
key="RecipientName" value="TestTestTestChannel" - User or cannel name<br />
key="CodeFromTelegram" value="12345" - Telegram authorization code. You shouldn't change this value manually<br />

### If you dont have Telegram code - don't panic!<br />
#### Start the program, and in a minute you will receive the authorization code via SMS.<br />
Enter it carefully in console, and if the code is valid, the authorization will succeed and the file *_session.dat_* will be created. You have to enter Telegram code in console only once - on the first program start. Don't delete or replace the *_session.dat_* file!
