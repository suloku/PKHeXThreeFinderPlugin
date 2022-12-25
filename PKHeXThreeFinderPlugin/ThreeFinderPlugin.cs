using System;
using PKHeX.Core;
using System.Windows.Forms;
using System.Collections.Generic;

namespace PKHeXThreeFinderPlugin
{
    public class ThreeFinderPlugin : IPlugin
    {
        public string Name => nameof(ThreeFinderPlugin);
        public int Priority => 1; // Loading order, lowest is first.

        // Initialized on plugin load
        public ISaveFileProvider SaveFileEditor { get; private set; } = null!;
        public IPKMView PKMEditor { get; private set; } = null!;

        private ToolStripMenuItem ctrl = new ToolStripMenuItem("");

        public void Initialize(params object[] args)
        {
            Console.WriteLine($"Loading {Name}...");
            SaveFileEditor = (ISaveFileProvider)Array.Find(args, z => z is ISaveFileProvider);
            PKMEditor = (IPKMView)Array.Find(args, z => z is IPKMView);
            var menu = (ToolStrip)Array.Find(args, z => z is ToolStrip);
            LoadMenuStrip(menu);

        }

        private void LoadMenuStrip(ToolStrip menuStrip)
        {
            var items = menuStrip.Items;
            if (!(items.Find("Menu_Tools", false)[0] is ToolStripDropDownItem tools))
                throw new ArgumentException(nameof(menuStrip));
            AddPluginControl(tools);
        }

        private void AddPluginControl(ToolStripDropDownItem tools)
        {

            ctrl = new ToolStripMenuItem(Name);
            tools.DropDownItems.Add(ctrl);
            ctrl.Image = Properties.Resources.icon;
            ctrl.Visible = SaveFileEditor.SAV is SAV9SV;
            ctrl.Click += FindThreesomeClick;
        }
        private void FindThreesomeClick(object sender, EventArgs e)
        {
            FindThreesome();
        }
        private void FindThreesome()
        {
            var sav = SaveFileEditor.SAV;
            var dunsparce_count = 0;
            var dunsparce_3_count = 0;
            var tandemaus_count = 0;
            var tandemaus_3_count = 0;
            var output_message = "";

            if (sav.Generation != 9)
            {
                MessageBox.Show("Plugin only works on generation 9 savegames!");
            }
            else
            {
                int i;
                //Run trough all pokémon in the party
                for (i=0; i<sav.PartyCount; i++)
                {
                    //Dunsparce check
                    PKM current_party_mon = sav.GetPartySlotAtIndex(i);
                    if (current_party_mon.Species == (int)Species.Dunsparce)
                    {
                        dunsparce_count++;
                        if (current_party_mon.EncryptionConstant % 100 == 0)
                        {
                            dunsparce_3_count++;
                            output_message += "Three segmented Dunsparce found in party slot " + i.ToString() + "\n";
                        }
                        
                    }
                    //Tandemaus check
                    else if (current_party_mon.Species == (int)Species.Tandemaus)
                    {
                        tandemaus_count++;
                        if (current_party_mon.EncryptionConstant % 100 == 0)
                        {
                            tandemaus_3_count++;
                            output_message += "Family of Three Tandemaus found in party slot " + i.ToString() + "\n";
                        }
                    }
                }
                //Run trough all pokémon in the box storage
                for (i=0; i<sav.BoxCount*30; i++)
                {
                    PKM current_box_mon = sav.GetBoxSlotAtIndex(i);
                    if (current_box_mon.Species == (int)Species.Dunsparce)
                    {
                        dunsparce_count++;
                        if (current_box_mon.EncryptionConstant % 100 == 0)
                        {
                            dunsparce_3_count++;
                            output_message += "Three segmented Dunsparce found in box #" + (i / 30).ToString() + " (" + sav.GetBoxName(i / 30) + ") slot " + (i % 30).ToString() + "\n";
                        }
                    }
                    else if (current_box_mon.Species == (int)Species.Tandemaus)
                    {
                        tandemaus_count++;
                        if (current_box_mon.EncryptionConstant % 100 == 0)
                        {
                            tandemaus_3_count++;
                            output_message += "Family of Three Tandemaus found in box #" + (i / 30).ToString() + " (" + sav.GetBoxName(i / 30) + ") slot " + (i % 30).ToString() + "\n";
                        }
                    }
                }
                if (output_message.Length > 0)
                {
                    output_message += "\n\n";
                }
                output_message += "Total Dunsparce found: " + dunsparce_count.ToString() + "\n";
                output_message += "Total Three segmented Dunsparce found: " + dunsparce_3_count.ToString() + "\n";
                output_message += "\n";
                output_message += "Total Tandemaus found: " + tandemaus_count.ToString() + "\n";
                output_message += "Total Family of Three Tandemaus found: " + tandemaus_3_count.ToString() + "\n";

                var display_message = output_message + "\n\n                Copy results to clipboard?                ";
                DialogResult dialog_result = MessageBox.Show(display_message, Name, MessageBoxButtons.OKCancel);
                if (dialog_result == DialogResult.OK) {
                    Clipboard.SetText(output_message);
                }

                //SaveFileEditor.ReloadSlots();
            }

        }

        public void NotifySaveLoaded()
        {
            Console.WriteLine($"{Name} was notified that a Save File was just loaded.");
            ctrl.Visible = SaveFileEditor.SAV is SAV9SV;

        }

        public bool TryLoadFile(string filePath)
        {
            Console.WriteLine($"{Name} was provided with the file path, but chose to do nothing with it.");
            return false; // no action taken
        }
    }
}
