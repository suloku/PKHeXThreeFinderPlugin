# PKHeXThreeFinderPlugin

[PKHeX](https://github.com/kwsch/PKHeX) plugin to help finding Dunsparce/Tandemaus in the savegame that evolve into the rare 1/100 chance form.

## How to use

Put the plugin into the plugins folder in your PKHeX directory, then access it from the Tools menu (option is only available if a gen 9 scarlet/violet savegame is loaded, it will need to be updated for any upcomming games).
After clickling, the program will run trough every pokémon in the savegame (party + boxes) in order to find a Dunsparce or Tandemaus whose encription constant is divisible by 100 with no remainder. This means the Dunsparce/Tandemaus will evolve into the 1/100 rare form (3 segmented / family of three).
A simple text output will tell you if any of the rare-form have been found and exactly where. The report will be copied to the clipboard, so you can paste it onto any text editor.

note: the case were output is longer than screen height is not handled, which would happen if you have a lot of the rare pokémon in your savegame, if it happens just press enter/escape to close the textbox. You can still check the whole output as it was copied to the clipboard.

## Credits
Kaphotics for [PKHeX](https://github.com/kwsch/PKHeX) and [PKHeXPluginExample](https://github.com/kwsch/PKHeXPluginExample)
BlackShark for [PKHeXMirageIslandPlugin](https://github.com/Bl4ckSh4rk/PKHeXMirageIslandPlugin)
