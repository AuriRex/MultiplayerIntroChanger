# MultiplayerIntroChanger

Customise the Beat Saber multiplayer intro animations text and sound effects as seen in this [video](https://streamable.com/nfkw5a).

[![](https://cdn.discordapp.com/attachments/369815362696380416/769690954008625173/Beat_Saber_dZXyY0szJA.png)](https://streamable.com/nfkw5a "Video")

The text is changeable from within the games "MP Intro" menu and the sound effects have to be placed in a folder in `./UserData/MultiplayerIntroSounds/`

Folder structure: (all files are optional and if it's not found the default sound will play instead!)
```
./UserData/
 - MultiplayerIntroSounds/
   - YourFolderNameHere/
     - Buildup.ogg
     - Ready.ogg
     - Set.ogg
     - Go.ogg
     - Icon.png
```
[Example sound replacement](https://cdn.discordapp.com/attachments/369815362696380416/769690541788889159/ExampleMPIntroSound.zip)

Note on the default sounds:  
the Buildup clip plays first and lasts for the entire duration of the animation  
the Ready, Set and Go clips play on their respective text popups
