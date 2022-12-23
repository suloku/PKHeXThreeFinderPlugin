# PKHeXThreeFinderPlugin

This project compiles a .dll compatible with [PKHeX](https://github.com/kwsch/PKHeX)'s plugin loader.

This plugin will add a dropdown menu option in "Tools" when a gen 9 savegame is loaded. After clickling, the program will run every pokémon in the savegame (party + boxes) in order to find a Dunsparce or Tandemaus whose encription constant is divisible by 100 with no remainder. This means the dunsparce/maushold will evolve into the 1/100 rare form (3 segmented / family of three).
A simple text output will tell you if any of the rare-form have been found and where. The report will be copied to the clipboard, so you can paste it onto any text editor.

note: the case were output is longer than screen height is not handled, if it happens just press enter/escape to close the textbox. You can still check the whole output as it was copied to the clipboard.