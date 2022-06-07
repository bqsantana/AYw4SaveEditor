using System.Diagnostics;
using System.Text;

namespace Another_YW_4_Save_Editor
{
    public partial class Form1 : Form
    {
        Stream openedFile;
        MemoryStream workFile;
        SaveFileParams saveFileParams;
        public Form1()
        {
            InitializeComponent();
            //mainTabControl.Controls.Remove(tabPage2);
            //mainTabControl.Controls.Remove(tabPage3);
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            saveFileParams.misc.Money = Convert.ToInt32(moneyNbox.Value);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (openedFile = openFileDialog1.OpenFile())
                    {
                        workFile = new MemoryStream();

                        openedFile.Seek(0, SeekOrigin.Begin);
                        openedFile.CopyTo(workFile);

                        saveFileParams = new SaveFileParams();
                        saveFileParams.mapParams(openedFile);

                        if (yokaiListView.Items.Count > 0)
                            yokaiListView.Items.Clear();
                        if (foodItemList.Items.Count > 0)
                            foodItemList.Items.Clear();
                        if (mainCharacterViewList.Items.Count > 0)
                            mainCharacterViewList.Items.Clear();

                        moneyNbox.Value = saveFileParams.misc.Money;
                        gatchaDaily.Value = saveFileParams.misc.Gatcha.gatchaTries;
                        gatchaMax.Value = saveFileParams.misc.Gatcha.gatchaMaxTries;
                        mapCbox.SelectedIndex = new GetMap().pickMapIndex(saveFileParams.misc.LocalParams.Map);
                        positionXNbox.Value = (decimal)saveFileParams.misc.LocalParams.PositionX;
                        positionYNbox.Value = (decimal)saveFileParams.misc.LocalParams.PositionY;
                        positionZNbox.Value = (decimal)saveFileParams.misc.LocalParams.PositionZ;

                        nateNameTbox.Text = saveFileParams.misc.NateName;
                        katieNameTbox.Text = saveFileParams.misc.KatieName;
                        summerNameTbox.Text = saveFileParams.misc.SummerName;
                        toumaNameTbox.Text = saveFileParams.misc.ToumaName;
                        akinoriNameTbox.Text = saveFileParams.misc.AkinoriName;
                        jackNameTbox.Text = saveFileParams.misc.JackName;


                        foreach (YoKai yokai in saveFileParams.UserYoKaiList)
                        {
                            yokaiListView.Items.Add(new ListViewItem() { Text = new GetYokai().pickYokaiName(yokai.YoKai_Signature ?? "Invalid") });
                        }

                        foreach (MainCharacter character in saveFileParams.MainCharacterList)
                        {
                            mainCharacterViewList.Items.Add(new ListViewItem() { Text = new GetYokai().pickYokaiName(character.Character_Signature ?? "Invalid") });
                        }

                        foreach (Consumable food in saveFileParams.ConsumableList)
                        {
                            foodItemList.Items.Add(food.ID1.ToString()).SubItems.AddRange(new string[] {
                                food.ID2.ToString(),
                                food.Order.ToString(),
                                new GetConsumable().pickConsumableName(food.ItemSignature ?? "Invalid"),
                                food.Quantity.ToString()
                            });
                        }

                        yokaiListView.Items[0].Selected = true;
                        foodItemList.Items[0].Selected = true;
                        mainCharacterViewList.Items[0].Selected = true;

                        saveAsToolStripMenuItem.Enabled = true;
                        mainTabControl.Enabled = true;
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"Error message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in yokaiListView.SelectedItems)
            {
                if (isAdvancedList.Checked)
                    yokaiCbox.SelectedIndex = new GetYokai().pickYokaiIDNumber(saveFileParams.UserYoKaiList[item.Index].YoKai_Signature ?? "");
                else
                    yokaiCbox.SelectedIndex = new GetYokai().pickYokaiHealthyIndex(saveFileParams.UserYoKaiList[item.Index].YoKai_Signature ?? "");

                yokaiIdNbox.Value = new GetYokai().pickYokaiIDNumber(saveFileParams.UserYoKaiList[item.Index].YoKai_Signature ?? "");
                yokaiLevelNbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Level;
                yokaiYpNbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_YP;
                yokaiHpNbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_HP;
                yokaiExpNbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_XP;
                yokaiPgNbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_PG;
                yokaiTbox.Text = saveFileParams.UserYoKaiList[item.Index].YoKai_Name;
                yokaiId1Nbox.Value = saveFileParams.UserYoKaiList[item.Index].ID1;
                yokaiId2Nbox.Value = saveFileParams.UserYoKaiList[item.Index].ID2;
                yokaiOrderNbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Order;
                yokaiHpPlusNbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_HPplus;
                yokaiYpPlusNbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_YPplus;
                yokaiPdPlusNbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_PAplus;
                yokaiSdPlusNbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_SAplus;
                yokaiStPlusNbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_STplus;
                yokaiSpPlusNbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_SPplus;
                yokaiBAtkCbox.SelectedIndex = new GetSkill().pickYokaiSkill(saveFileParams.UserYoKaiList[item.Index].YoKai_Skill1 ?? "");
                yokaiSpSklCbox.SelectedIndex = new GetSkill().pickYokaiSkill(saveFileParams.UserYoKaiList[item.Index].YoKai_Skill2 ?? "");
                yokaiExSklNbox.SelectedIndex = new GetSkill().pickYokaiSkill(saveFileParams.UserYoKaiList[item.Index].YoKai_Skill3 ?? "");
                yokaiExSkl2Nbox.SelectedIndex = new GetSkill().pickYokaiSkill(saveFileParams.UserYoKaiList[item.Index].YoKai_Skill4 ?? "");
                yokaiExSkl3Nbox.SelectedIndex = new GetSkill().pickYokaiSkill(saveFileParams.UserYoKaiList[item.Index].YoKai_Skill5 ?? "");
                yokaiExSkl4Nbox.SelectedIndex = new GetSkill().pickYokaiSkill(saveFileParams.UserYoKaiList[item.Index].YoKai_Skill6 ?? "");
                yokaiUnknown1Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown1;
                yokaiUnknown2Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown2;
                yokaiUnknown3Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown3;
                yokaiUnknown4Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown4;
                yokaiUnknown5Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown5;
                yokaiUnknown6Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown6;
                yokaiUnknown7Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown7;
                yokaiUnknown8Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown8;
                yokaiUnknown9Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown9;
                yokaiUnknown10Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown10;
                yokaiUnknown11Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown11;
                yokaiUnknown12Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown12;
                yokaiUnknown13Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown13;
                yokaiUnknown14Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown14;
                yokaiUnknown15Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown15;
                yokaiUnknown16Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown16;
                yokaiUnknown17Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown17;
            }
        }

        private void mapCbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            saveFileParams.misc.LocalParams.Map = new SetMap().pickMapByte(mapCbox.SelectedIndex);
        }

        private void isAdvancedList_CheckedChanged(object sender, EventArgs e)
        {
            if (isAdvancedList.Checked)
            {
                yokaiCbox.Items.Clear();
                yokaiCbox.Items.Add("");
                yokaiCbox.Items.Add("Touma                                         ");
                yokaiCbox.Items.Add("Summer                                        ");
                yokaiCbox.Items.Add("Akinori                                       ");
                yokaiCbox.Items.Add("Akinori (Fit)                                 ");
                yokaiCbox.Items.Add("Jack                                          ");
                yokaiCbox.Items.Add("Nate                                          ");
                yokaiCbox.Items.Add("Katie                                         ");
                yokaiCbox.Items.Add("Himoji (Lightside)                            ");
                yokaiCbox.Items.Add("Himoji (Shadowside)                           ");
                yokaiCbox.Items.Add("Himoji Shadow Boss                            ");
                yokaiCbox.Items.Add("Pakkun (Lightside)                            ");
                yokaiCbox.Items.Add("Pakkun (Shadowside)                           ");
                yokaiCbox.Items.Add("Pakkun Shadow Boss                            ");
                yokaiCbox.Items.Add("Kyunshii (Lightside)                          ");
                yokaiCbox.Items.Add("Kyunshii (Shadowside)                         ");
                yokaiCbox.Items.Add("Kyunshii Shadow Boss                          ");
                yokaiCbox.Items.Add("Hare-onna (Lightside)                         ");
                yokaiCbox.Items.Add("Hare-onna (Shadowside)                        ");
                yokaiCbox.Items.Add("Choky (Lightside)                             ");
                yokaiCbox.Items.Add("Choky (Shadowside)                            ");
                yokaiCbox.Items.Add("Fubuki-hime (Lightside)                       ");
                yokaiCbox.Items.Add("Fubuki-hime (Shadowside)                      ");
                yokaiCbox.Items.Add("Fubuki-hime Shadow Boss                       ");
                yokaiCbox.Items.Add("Merameraion (Lightside)                       ");
                yokaiCbox.Items.Add("Merameraion (Shadowside)                      ");
                yokaiCbox.Items.Add("Merameraion Shadow Boss                       ");
                yokaiCbox.Items.Add("Orochi (Lightside)                            ");
                yokaiCbox.Items.Add("Orochi (Shadowside)                           ");
                yokaiCbox.Items.Add("Orochi Shadow Boss                            ");
                yokaiCbox.Items.Add("Honmaguro-taishou (Lightside)                 ");
                yokaiCbox.Items.Add("Honmaguro-taishou (Shadowside)                ");
                yokaiCbox.Items.Add("Honmaguro-taishou Shadow Boss                 ");
                yokaiCbox.Items.Add("Semicolon (Lightside)                         ");
                yokaiCbox.Items.Add("Semicolon (Shadowside)                        ");
                yokaiCbox.Items.Add("Semicolon Shadow Boss                         ");
                yokaiCbox.Items.Add("Komasan (Lightside)                           ");
                yokaiCbox.Items.Add("Komasan (Shadowside)                          ");
                yokaiCbox.Items.Add("Komajiro (Lightside)                          ");
                yokaiCbox.Items.Add("Komajiro (Shadowside)                         ");
                yokaiCbox.Items.Add("Komajiro Shadow Boss                          ");
                yokaiCbox.Items.Add("Banchou (Lightside)                           ");
                yokaiCbox.Items.Add("Banchou (Shadowside)                          ");
                yokaiCbox.Items.Add("Banchou Shadow Boss                           ");
                yokaiCbox.Items.Add("Seiryuu (Lightside)                           ");
                yokaiCbox.Items.Add("Seiryuu (Shadowside)                          ");
                yokaiCbox.Items.Add("Fuu-kun (Lightside)                           ");
                yokaiCbox.Items.Add("Fuu-kun (Shadowside)                          ");
                yokaiCbox.Items.Add("Fuu-kun Shadow Boss                           ");
                yokaiCbox.Items.Add("Rai-chan (Lightside)                          ");
                yokaiCbox.Items.Add("Rai-chan (Shadowside)                         ");
                yokaiCbox.Items.Add("Rai-chan Shadow Boss                          ");
                yokaiCbox.Items.Add("Hamham (Lightside)                            ");
                yokaiCbox.Items.Add("Hamham (Shadowside)                           ");
                yokaiCbox.Items.Add("Jibanyan (Lightside)                          ");
                yokaiCbox.Items.Add("Jibanyan (Shadowside)                         ");
                yokaiCbox.Items.Add("Uribou (Lightside)                            ");
                yokaiCbox.Items.Add("Uribou (Shadowside)                           ");
                yokaiCbox.Items.Add("Kyubi (Lightside)                             ");
                yokaiCbox.Items.Add("Kyubi (Shadowside)                            ");
                yokaiCbox.Items.Add("Kyubi Shadow Boss                             ");
                yokaiCbox.Items.Add("Charlie (Lightside)                           ");
                yokaiCbox.Items.Add("Charlie (Shadowside)                          ");
                yokaiCbox.Items.Add("Zundoumaru (Lightside)                        ");
                yokaiCbox.Items.Add("Zundoumaru (Shadowside)                       ");
                yokaiCbox.Items.Add("Zundoumaru Shadow Boss                        ");
                yokaiCbox.Items.Add("Ungaikyo (Lightside)                          ");
                yokaiCbox.Items.Add("Ungaikyo (Shadowside)                         ");
                yokaiCbox.Items.Add("Jinta (Lightside)                             ");
                yokaiCbox.Items.Add("Jinta (Shadowside)                            ");
                yokaiCbox.Items.Add("Jinta Shadow Boss                             ");
                yokaiCbox.Items.Add("Kantaro (Lightside)                           ");
                yokaiCbox.Items.Add("Kantaro (Shadowside)                          ");
                yokaiCbox.Items.Add("Kantaro Shadow Boss                           ");
                yokaiCbox.Items.Add("Kiborikkuma (Lightside)                       ");
                yokaiCbox.Items.Add("Kiborikkuma (Shadowside)                      ");
                yokaiCbox.Items.Add("Junior (Lightside)                            ");
                yokaiCbox.Items.Add("Junior (Shadowside)                           ");
                yokaiCbox.Items.Add("Micchy (Lightside)                            ");
                yokaiCbox.Items.Add("Micchy (Shadowside)                           ");
                yokaiCbox.Items.Add("Micchy Hyper (Lightside)                      ");
                yokaiCbox.Items.Add("Micchy Hyper (Shadowside)                     ");
                yokaiCbox.Items.Add("Hi no Shin (Lightside)                        ");
                yokaiCbox.Items.Add("Hi no Shin (Shadowside)                       ");
                yokaiCbox.Items.Add("Hungramps                                     ");
                yokaiCbox.Items.Add("Dimmy                                         ");
                yokaiCbox.Items.Add("Tattletell                                    ");
                yokaiCbox.Items.Add("Dismarelda                                    ");
                yokaiCbox.Items.Add("Hidabat                                       ");
                yokaiCbox.Items.Add("Frostina                                      ");
                yokaiCbox.Items.Add("Insomni                                       ");
                yokaiCbox.Items.Add("Insomni (Boss)                                ");
                yokaiCbox.Items.Add("Blizzaria                                     ");
                yokaiCbox.Items.Add("Damona                                        ");
                yokaiCbox.Items.Add("Little Charrmer                               ");
                yokaiCbox.Items.Add("Roughraff                                     ");
                yokaiCbox.Items.Add("Roughraff (Boss)                              ");
                yokaiCbox.Items.Add("Mochismo                                      ");
                yokaiCbox.Items.Add("Blazion                                       ");
                yokaiCbox.Items.Add("Blazion (Boss)                                ");
                yokaiCbox.Items.Add("Sgt. Burly                                    ");
                yokaiCbox.Items.Add("Venoct                                        ");
                yokaiCbox.Items.Add("Illuminoct                                    ");
                yokaiCbox.Items.Add("Shadow Venoct                                 ");
                yokaiCbox.Items.Add("Shogunyan                                     ");
                yokaiCbox.Items.Add("Snartle                                       ");
                yokaiCbox.Items.Add("Snartle (Boss)                                ");
                yokaiCbox.Items.Add("Arachnus                                      ");
                yokaiCbox.Items.Add("Arachnus (Boss)                               ");
                yokaiCbox.Items.Add("Komashura                                     ");
                yokaiCbox.Items.Add("Noko                                          ");
                yokaiCbox.Items.Add("Komasan                                       ");
                yokaiCbox.Items.Add("Komajiro                                      ");
                yokaiCbox.Items.Add("Happierre                                     ");
                yokaiCbox.Items.Add("Hovernyan                                     ");
                yokaiCbox.Items.Add("Reuknight                                     ");
                yokaiCbox.Items.Add("Reuknight Boss                                ");
                yokaiCbox.Items.Add("Corptain                                      ");
                yokaiCbox.Items.Add("Toadal Dude                                   ");
                yokaiCbox.Items.Add("Toadal Dude Boss                              ");
                yokaiCbox.Items.Add("Silver Lining                                 ");
                yokaiCbox.Items.Add("Manjimutt                                     ");
                yokaiCbox.Items.Add("Manjimutt Boss                                ");
                yokaiCbox.Items.Add("Jibanyan                                      ");
                yokaiCbox.Items.Add("Krystal Fox                                   ");
                yokaiCbox.Items.Add("Baku                                          ");
                yokaiCbox.Items.Add("Kyubi                                         ");
                yokaiCbox.Items.Add("Darkyubi                                      ");
                yokaiCbox.Items.Add("Master Nyada                                  ");
                yokaiCbox.Items.Add("Noway                                         ");
                yokaiCbox.Items.Add("Sandmeh                                       ");
                yokaiCbox.Items.Add("Mimikin                                       ");
                yokaiCbox.Items.Add("Mimikin Boss                                  ");
                yokaiCbox.Items.Add("Mirapo                                        ");
                yokaiCbox.Items.Add("Robonyan                                      ");
                yokaiCbox.Items.Add("Goldenyan                                     ");
                yokaiCbox.Items.Add("Wiglin                                        ");
                yokaiCbox.Items.Add("Steppa                                        ");
                yokaiCbox.Items.Add("Rhyth                                         ");
                yokaiCbox.Items.Add("Walkappa                                      ");
                yokaiCbox.Items.Add("Nosirs                                        ");
                yokaiCbox.Items.Add("Cornfused                                     ");
                yokaiCbox.Items.Add("Whisper                                       ");
                yokaiCbox.Items.Add("Swelton                                       ");
                yokaiCbox.Items.Add("Usapyon                                       ");
                yokaiCbox.Items.Add("Usapyon                                       ");
                yokaiCbox.Items.Add("Spoilerina                                    ");
                yokaiCbox.Items.Add("Sighborg Y                                    ");
                yokaiCbox.Items.Add("Wobblewok                                     ");
                yokaiCbox.Items.Add("Deadcool                                      ");
                yokaiCbox.Items.Add("Gargaros                                      ");
                yokaiCbox.Items.Add("Ogralus                                       ");
                yokaiCbox.Items.Add("Orcanos                                       ");
                yokaiCbox.Items.Add("Gilgaros                                      ");
                yokaiCbox.Items.Add("Shirokuma                                     ");
                yokaiCbox.Items.Add("Punkupine                                     ");
                yokaiCbox.Items.Add("Sorrypus                                      ");
                yokaiCbox.Items.Add("Jabow                                         ");
                yokaiCbox.Items.Add("Beetall                                       ");
                yokaiCbox.Items.Add("Cruncha                                       ");
                yokaiCbox.Items.Add("Rhinormous                                    ");
                yokaiCbox.Items.Add("Hornaplenty                                   ");
                yokaiCbox.Items.Add("Mad Mountain                                  ");
                yokaiCbox.Items.Add("Lava Lord                                     ");
                yokaiCbox.Items.Add("Faux Kappa                                    ");
                yokaiCbox.Items.Add("McKraken                                      ");
                yokaiCbox.Items.Add("Suu-san                                       ");
                yokaiCbox.Items.Add("Yamanba                                       ");
                yokaiCbox.Items.Add("Tamamo                                        ");
                yokaiCbox.Items.Add("Gyuuki                                        ");
                yokaiCbox.Items.Add("Narigama                                      ");
                yokaiCbox.Items.Add("Blobgoblin                                    ");
                yokaiCbox.Items.Add("Nekomata Neko'ou Bastet                       ");
                yokaiCbox.Items.Add("Kappa Kappa'ou Sagojou                        ");
                yokaiCbox.Items.Add("Zashiki-warashi Tengu'ou Kurama               ");
                yokaiCbox.Items.Add("Kawauso                                       ");
                yokaiCbox.Items.Add("Enma                                          ");
                yokaiCbox.Items.Add("Lord Ananta                                   ");
                yokaiCbox.Items.Add("Douketsu                                      ");
                yokaiCbox.Items.Add("Douketsu                                      ");
                yokaiCbox.Items.Add("Shutendoji                                    ");
                yokaiCbox.Items.Add("Ogu Togu Mogu                                 ");
                yokaiCbox.Items.Add("Nurarihyon                                    ");
                yokaiCbox.Items.Add("Fudou Myouou Boy                              ");
                yokaiCbox.Items.Add("Whisper                                       ");
                yokaiCbox.Items.Add("Enma Awakened                                 ");
                yokaiCbox.Items.Add("Yami Enma                                     ");
                yokaiCbox.Items.Add("Kaibyou Kamaitachi                            ");
                yokaiCbox.Items.Add("Neko'ou Bastet                                ");
                yokaiCbox.Items.Add("Kappa Kappa'ou Sagojou                        ");
                yokaiCbox.Items.Add("Zashiki-warashi Tengu'ou Kurama               ");
                yokaiCbox.Items.Add("Touma Omatsu                                  ");
                yokaiCbox.Items.Add("Touma Yoshitsune                              ");
                yokaiCbox.Items.Add("Touma Goemon                                  ");
                yokaiCbox.Items.Add("Touma Benkei                                  ");
                yokaiCbox.Items.Add("Suzaku (Sword Bearer)                         ");
                yokaiCbox.Items.Add("Genbu (Sword Bearer)                          ");
                yokaiCbox.Items.Add("Byakko (Sword Bearer)                         ");
                yokaiCbox.Items.Add("Kirin                                         ");
                yokaiCbox.Items.Add("Souryuu                                       ");
                yokaiCbox.Items.Add("Gunshin Susanoo                               ");
                yokaiCbox.Items.Add("Touma Fudou Myouou                            ");
                yokaiCbox.Items.Add("Touma Fudou Myouou Ten                        ");
                yokaiCbox.Items.Add("Touma Suzaku                                  ");
                yokaiCbox.Items.Add("Touma Genbu 2                                 ");
                yokaiCbox.Items.Add("Touma Byakko                                  ");
                yokaiCbox.Items.Add("Touma Ashura                                  ");
                yokaiCbox.Items.Add("Shuka Natsume (Summer)                        ");
                yokaiCbox.Items.Add("[DONT_WORK]                                   ");
                yokaiCbox.Items.Add("Jinta Shadow (boss)                           ");
                yokaiCbox.Items.Add("Micchy Shadow (boss)                          ");
                yokaiCbox.Items.Add("Micchy Eye Ball (boss)                        ");
                yokaiCbox.Items.Add("Jorogumo (boss)                               ");
                yokaiCbox.Items.Add("Shinmagunjin Fukurou (boss)                   ");
                yokaiCbox.Items.Add("Overseer                                      ");
                yokaiCbox.Items.Add("Overseer 2                                    ");
                yokaiCbox.Items.Add("Overseer 3                                    ");
                yokaiCbox.Items.Add("Diamond                                       ");
                yokaiCbox.Items.Add("Yami Enma                                     ");
                yokaiCbox.Items.Add("Enma                                          ");
                yokaiCbox.Items.Add("Maten Soranaki                                ");
                yokaiCbox.Items.Add("Gilgaros                                      ");
                yokaiCbox.Items.Add("Illuminoct                                    ");
                yokaiCbox.Items.Add("Blizzaria                                     ");
                yokaiCbox.Items.Add("Sgt. Burly                                    ");
                yokaiCbox.Items.Add("Damona                                        ");
                yokaiCbox.Items.Add("Manjimutt                                     ");
                yokaiCbox.Items.Add("Enma Awoken                                   ");
                yokaiCbox.Items.Add("Raidenryu                                     ");
                yokaiCbox.Items.Add("Fudou Myouou                                  ");
                yokaiCbox.Items.Add("Fudou Myouou                                  ");
                yokaiCbox.Items.Add("Suzaku (Celestial)                            ");
                yokaiCbox.Items.Add("Suzaku big                                    ");
                yokaiCbox.Items.Add("Genbu (Celestial)                             ");
                yokaiCbox.Items.Add("Genbu big                                     ");
                yokaiCbox.Items.Add("Byakko (Celestial)                            ");
                yokaiCbox.Items.Add("Byakko big                                    ");
                yokaiCbox.Items.Add("Ashura                                        ");
                yokaiCbox.Items.Add("Ashura big                                    ");
                yokaiCbox.Items.Add("Douketsu                                      ");
                yokaiCbox.Items.Add("Douketsu                                      ");
                yokaiCbox.Items.Add("Shutendoji                                    ");
                yokaiCbox.Items.Add("Yamamba                                       ");
                yokaiCbox.Items.Add("Tamamo no Mae                                 ");
                yokaiCbox.Items.Add("Shien                                         ");
                yokaiCbox.Items.Add("Shinma Kaira                                  ");
                yokaiCbox.Items.Add("Shinma Kaira                                  ");
                yokaiCbox.Items.Add("Jinta Shadow                                  ");
                yokaiCbox.Items.Add("Jinta Shadow                                  ");
                yokaiCbox.Items.Add("Jinta Shadow                                  ");
                yokaiCbox.Items.Add("Jinta Shadow                                  ");
                yokaiCbox.Items.Add("Mitsumata Nozuchi                             ");
                yokaiCbox.Items.Add("Mitsumata Nozuchi                             ");
                yokaiCbox.Items.Add("Mitsumata Nozuchi                             ");
                yokaiCbox.Items.Add("Mitsumata Nozuchi                             ");
                yokaiCbox.Items.Add("Micchy Eye Ball                               ");
                yokaiCbox.Items.Add("Micchy Eye Ball                               ");
                yokaiCbox.Items.Add("Micchy Eye Ball                               ");
                yokaiCbox.Items.Add("Micchy Eye Ball                               ");
                yokaiCbox.Items.Add("Jorogumo                                      ");
                yokaiCbox.Items.Add("Jorogumo                                      ");
                yokaiCbox.Items.Add("Jorogumo                                      ");
                yokaiCbox.Items.Add("Jorogumo                                      ");
                yokaiCbox.Items.Add("Shinmagunjin Fukurou                          ");
                yokaiCbox.Items.Add("Shinmagunjin Fukurou                          ");
                yokaiCbox.Items.Add("Shinmagunjin Fukurou                          ");
                yokaiCbox.Items.Add("Shinmagunjin Fukurou                          ");
                yokaiCbox.Items.Add("Overseer                                      ");
                yokaiCbox.Items.Add("Overseer                                      ");
                yokaiCbox.Items.Add("Overseer                                      ");
                yokaiCbox.Items.Add("Overseer                                      ");
                yokaiCbox.Items.Add("Overseer giant                                ");
                yokaiCbox.Items.Add("Overseer giant                                ");
                yokaiCbox.Items.Add("Diamond                                       ");
                yokaiCbox.Items.Add("Enma                                          ");
                yokaiCbox.Items.Add("Overseer giant                                ");
                yokaiCbox.Items.Add("Overseer giant                                ");
                yokaiCbox.Items.Add("Diamond                                       ");
                yokaiCbox.Items.Add("Enma                                          ");
                yokaiCbox.Items.Add("Overseer giant                                ");
                yokaiCbox.Items.Add("Overseer giant                                ");
                yokaiCbox.Items.Add("Diamond                                       ");
                yokaiCbox.Items.Add("Enma                                          ");
                yokaiCbox.Items.Add("Overseer giant                                ");
                yokaiCbox.Items.Add("Overseer giant                                ");
                yokaiCbox.Items.Add("Diamond                                       ");
                yokaiCbox.Items.Add("Enma                                          ");
                yokaiCbox.Items.Add("Maten Soranaki                                ");
                yokaiCbox.Items.Add("Maten Soranaki                                ");
                yokaiCbox.Items.Add("Maten Soranaki                                ");
                yokaiCbox.Items.Add("Maten Soranaki                                ");
                yokaiCbox.Items.Add("Maten Soranaki                                ");
                yokaiCbox.Items.Add("Gilgaros                                      ");
                yokaiCbox.Items.Add("Illuminoct                                    ");
                yokaiCbox.Items.Add("Blizzaria                                     ");
                yokaiCbox.Items.Add("Sgt. Burly                                    ");
                yokaiCbox.Items.Add("Damona                                        ");
                yokaiCbox.Items.Add("Manjimutt                                     ");
                yokaiCbox.Items.Add("Gilgaros                                      ");
                yokaiCbox.Items.Add("Illuminoct                                    ");
                yokaiCbox.Items.Add("Blizzaria                                     ");
                yokaiCbox.Items.Add("Sgt. Burly                                    ");
                yokaiCbox.Items.Add("Damona                                        ");
                yokaiCbox.Items.Add("Manjimutt                                     ");
                yokaiCbox.Items.Add("Gilgaros                                      ");
                yokaiCbox.Items.Add("Illuminoct                                    ");
                yokaiCbox.Items.Add("Blizzaria                                     ");
                yokaiCbox.Items.Add("Sgt. Burly                                    ");
                yokaiCbox.Items.Add("Damona                                        ");
                yokaiCbox.Items.Add("Manjimutt                                     ");
                yokaiCbox.Items.Add("Gilgaros                                      ");
                yokaiCbox.Items.Add("Illuminoct                                    ");
                yokaiCbox.Items.Add("Blizzaria                                     ");
                yokaiCbox.Items.Add("Sgt. Burly                                    ");
                yokaiCbox.Items.Add("Damona                                        ");
                yokaiCbox.Items.Add("Manjimutt                                     ");
                yokaiCbox.Items.Add("Gilgaros                                      ");
                yokaiCbox.Items.Add("Illuminoct                                    ");
                yokaiCbox.Items.Add("Blizzaria                                     ");
                yokaiCbox.Items.Add("Sgt. Burly                                    ");
                yokaiCbox.Items.Add("Damona                                        ");
                yokaiCbox.Items.Add("Manjimutt                                     ");
                yokaiCbox.Items.Add("Shien                                         ");
                yokaiCbox.Items.Add("Shien                                         ");
                yokaiCbox.Items.Add("Shien                                         ");
                yokaiCbox.Items.Add("Shien                                         ");
                yokaiCbox.Items.Add("Fudou Myouou                                  ");
                yokaiCbox.Items.Add("Fudou Myouou-kai                              ");
                yokaiCbox.Items.Add("Suzaku (Celestial)                            ");
                yokaiCbox.Items.Add("Suzaku big                                    ");
                yokaiCbox.Items.Add("Genbu (Celestial)                             ");
                yokaiCbox.Items.Add("Genbu big                                     ");
                yokaiCbox.Items.Add("Byakko (Celestial)                            ");
                yokaiCbox.Items.Add("Byakko big                                    ");
                yokaiCbox.Items.Add("Ashura                                        ");
                yokaiCbox.Items.Add("Yami Enma                                     ");
                yokaiCbox.Items.Add("Nekomata Neko'ou Bastet                       ");
                yokaiCbox.Items.Add("Kappa Kappa'ou Sagojou                        ");
                yokaiCbox.Items.Add("Zashiki-warashi Tengu'ou Kurama               ");
                yokaiCbox.Items.Add("McKraken                                      ");
                yokaiCbox.Items.Add("Seiryuu Shadow                                ");
                yokaiCbox.Items.Add("Jinta Shadow (boss)                           ");
                yokaiCbox.Items.Add("Hi no Shin Shadow                             ");
                yokaiCbox.Items.Add("Lord Ananta                                   ");
                yokaiCbox.Items.Add("Fuu-kun (Lightside)                           ");
                yokaiCbox.Items.Add("Fuu-kun (Shadowside)                          ");
                yokaiCbox.Items.Add("Rai-chan (Lightside)                          ");
                yokaiCbox.Items.Add("Rai-chan (Shadowside)                         ");
                yokaiCbox.Items.Add("Arachnus                                      ");
                yokaiCbox.Items.Add("Toadal Dude                                   ");
                yokaiCbox.Items.Add("Orochi (Lightside)                            ");
                yokaiCbox.Items.Add("Orochi (Shadowside)                           ");
                yokaiCbox.Items.Add("Kyubi (Lightside)                             ");
                yokaiCbox.Items.Add("Kyubi (Shadowside)                            ");
                yokaiCbox.Items.Add("Deadcool                                      ");
                yokaiCbox.Items.Add("Hovernyan                                     ");
                yokaiCbox.Items.Add("Little Charrmer                               ");
                yokaiCbox.Items.Add("Micchy (Lightside)                            ");
                yokaiCbox.Items.Add("Micchy (Shadowside)                           ");
                yokaiCbox.Items.Add("Fudou Myouou Boy                              ");
                yokaiCbox.Items.Add("Shogunyan                                     ");
                yokaiCbox.Items.Add("Komashura                                     ");
                yokaiCbox.Items.Add("Gilgaros                                      ");
                yokaiCbox.Items.Add("Hi no Shin Shadow                             ");
                yokaiCbox.Items.Add("Neko'ou Bastet                                ");
                yokaiCbox.Items.Add("Kappa'ou Sagojou                              ");
                yokaiCbox.Items.Add("Tengu'ou Kurama                               ");
                yokaiCbox.Items.Add("Lord Ananta Shadow                            ");
                yokaiCbox.Items.Add("Yasha Enma                                    ");
                yokaiCbox.Items.Add("Fukurou                                       ");
                yokaiCbox.Items.Add("Shuka                                         ");
                yokaiCbox.Items.Add("Gentou                                        ");
                yokaiCbox.Items.Add("Hakushu                                       ");
                yokaiCbox.Items.Add("Kuuten                                        ");
                yokaiCbox.Items.Add("Jinta (boss)                                  ");
                yokaiCbox.Items.Add("Jinta (boss)                                  ");
                yokaiCbox.Items.Add("Kakurenbou (Lightside)                        ");
                yokaiCbox.Items.Add("Kakurenbou (Shadowside)                       ");
                yokaiCbox.Items.Add("Hyakki-hime (Lightside)                       ");
                yokaiCbox.Items.Add("Hyakki-hime (Shadowside)                      ");
                yokaiCbox.Items.Add("Daniel (Lightside)                            ");
                yokaiCbox.Items.Add("Daniel (Shadowside)                           ");
                yokaiCbox.Items.Add("Itashikatanshi (Lightside)                    ");
                yokaiCbox.Items.Add("Itashikatanshi (Shadowside)                   ");
                yokaiCbox.Items.Add("Saya (Lightside)                              ");
                yokaiCbox.Items.Add("Saya (Shadowside)                             ");
                yokaiCbox.Items.Add("Rai Oton (Lightside)                          ");
                yokaiCbox.Items.Add("Rai Oton (Shadowside)                         ");
                yokaiCbox.Items.Add("Kage Orochi (Lightside)                       ");
                yokaiCbox.Items.Add("Kage Orochi (Shadowside)                      ");
                yokaiCbox.Items.Add("Bushinyan (Lightside)                         ");
                yokaiCbox.Items.Add("Bushinyan (Shadowside)                        ");
                yokaiCbox.Items.Add("Shurakoma (Lightside)                         ");
                yokaiCbox.Items.Add("Shurakoma (Shadowside)                        ");
                yokaiCbox.Items.Add("Tsuchigumo (Lightside)                        ");
                yokaiCbox.Items.Add("Tsuchigumo (Shadowside)                       ");
                yokaiCbox.Items.Add("Tsuchinoko (Lightside)                        ");
                yokaiCbox.Items.Add("Tsuchinoko (Shadowside)                       ");
                yokaiCbox.Items.Add("Ogama (Lightside)                             ");
                yokaiCbox.Items.Add("Ogama (Shadowside)                            ");
                yokaiCbox.Items.Add("Doyapon (Lightside)                           ");
                yokaiCbox.Items.Add("Doyapon (Shadowside)                          ");
                yokaiCbox.Items.Add("Inugami (Lightside)                           ");
                yokaiCbox.Items.Add("Inugami (Shadowside)                          ");
                yokaiCbox.Items.Add("Kezurin (Lightside)                           ");
                yokaiCbox.Items.Add("Kezurin (Shadowside)                          ");
                yokaiCbox.Items.Add("Robonyan 00 (Lightside)                       ");
                yokaiCbox.Items.Add("Robonyan 00 (Shadowside)                      ");
                yokaiCbox.Items.Add("Becchan (Lightside)                           ");
                yokaiCbox.Items.Add("Becchan (Shadowside)                          ");
                yokaiCbox.Items.Add("Dameboy (Lightside)                           ");
                yokaiCbox.Items.Add("Dameboy (Shadowside)                          ");
                yokaiCbox.Items.Add("Awevil                                        ");
                yokaiCbox.Items.Add("Demuncher                                     ");
                yokaiCbox.Items.Add("Slicenrice                                    ");
                yokaiCbox.Items.Add("Signiton                                      ");
                yokaiCbox.Items.Add("Molar Petite                                  ");
                yokaiCbox.Items.Add("Shmoopie                                      ");
                yokaiCbox.Items.Add("Lie-in Heart                                  ");
                yokaiCbox.Items.Add("Wazzat                                        ");
                yokaiCbox.Items.Add("Nekidspeed                                    ");
                yokaiCbox.Items.Add("Count Zapaway                                 ");
                yokaiCbox.Items.Add("B3-NK1                                        ");
                yokaiCbox.Items.Add("Rocky Badboya                                 ");
                yokaiCbox.Items.Add("Smogmella                                     ");
                yokaiCbox.Items.Add("Drizzelda                                     ");
                yokaiCbox.Items.Add("Poofessor                                     ");
                yokaiCbox.Items.Add("Ray O'Light                                   ");
                yokaiCbox.Items.Add("Legsit                                        ");
                yokaiCbox.Items.Add("Snottle                                       ");
                yokaiCbox.Items.Add("Jibanyan B                                    ");
                yokaiCbox.Items.Add("Komasan B                                     ");
                yokaiCbox.Items.Add("Usapyon B                                     ");
                yokaiCbox.Items.Add("Kukuri-hime                                   ");
                yokaiCbox.Items.Add("Azukiarai                                     ");
                yokaiCbox.Items.Add("Shien                                         ");
                yokaiCbox.Items.Add("Yamamba (Boss)                                ");
                yokaiCbox.Items.Add("Tamamo (Boss)                                 ");
                yokaiCbox.Items.Add("Fukurou                                       ");
                yokaiCbox.Items.Add("Shuka                                         ");
                yokaiCbox.Items.Add("Gentou                                        ");
                yokaiCbox.Items.Add("Hakushu                                       ");
                yokaiCbox.Items.Add("Kuuten                                        ");
                yokaiCbox.Items.Add("Yasha Enma                                    ");
                yokaiCbox.Items.Add("Kenshin Amaterasu                             ");
                yokaiCbox.Items.Add("Gesshin Tsukuyomi                             ");
                yokaiCbox.Items.Add("Touma Fudou Myouou-kai                        ");
                yokaiCbox.Items.Add("Himoji (Lightside)                            ");
                yokaiCbox.Items.Add("Himoji (Shadowside)                           ");
                yokaiCbox.Items.Add("Pakkun (Lightside)                            ");
                yokaiCbox.Items.Add("Pakkun (Shadowside)                           ");
                yokaiCbox.Items.Add("Kyunshii (Lightside)                          ");
                yokaiCbox.Items.Add("Kyunshii (Shadowside)                         ");
                yokaiCbox.Items.Add("Hare-onna (Lightside)                         ");
                yokaiCbox.Items.Add("Hare-onna (Shadowside)                        ");
                yokaiCbox.Items.Add("Choky (Lightside)                             ");
                yokaiCbox.Items.Add("Choky C(Shadowside)                           ");
                yokaiCbox.Items.Add("Fubuki-hime (Lightside)                       ");
                yokaiCbox.Items.Add("Fubuki-hime (Shadowside)                      ");
                yokaiCbox.Items.Add("Merameraion (Lightside)                       ");
                yokaiCbox.Items.Add("Merameraion (Shadowside)                      ");
                yokaiCbox.Items.Add("Orochi (Lightside)                            ");
                yokaiCbox.Items.Add("Orochi (Shadowside)                           ");
                yokaiCbox.Items.Add("Honmaguro-taishou (Lightside)                 ");
                yokaiCbox.Items.Add("Honmaguro-taishou (Shadowside)                ");
                yokaiCbox.Items.Add("Semicolon (Lightside)                         ");
                yokaiCbox.Items.Add("Semicolon (Shadowside)                        ");
                yokaiCbox.Items.Add("Komasan (Lightside)                           ");
                yokaiCbox.Items.Add("Komasan (Shadowside)                          ");
                yokaiCbox.Items.Add("Komajiro (Lightside)                          ");
                yokaiCbox.Items.Add("Komajiro (Shadowside)                         ");
                yokaiCbox.Items.Add("Banchou (Lightside)                           ");
                yokaiCbox.Items.Add("Banchou (Shadowside)                          ");
                yokaiCbox.Items.Add("Seiryuu (Lightside)                           ");
                yokaiCbox.Items.Add("Seiryuu (Shadowside)                          ");
                yokaiCbox.Items.Add("Fuu-kun (Lightside)                           ");
                yokaiCbox.Items.Add("Fuu-kun (Shadowside)                          ");
                yokaiCbox.Items.Add("Rai-chan (Lightside)                          ");
                yokaiCbox.Items.Add("Rai-chan (Shadowside)                         ");
                yokaiCbox.Items.Add("Hamham (Lightside)                            ");
                yokaiCbox.Items.Add("Hamham (Shadowside)                           ");
                yokaiCbox.Items.Add("Jibanyan (Lightside)                          ");
                yokaiCbox.Items.Add("Jibanyan (Shadowside)                         ");
                yokaiCbox.Items.Add("Uribou (Lightside)                            ");
                yokaiCbox.Items.Add("Uribou (Shadowside)                           ");
                yokaiCbox.Items.Add("Kyubi (Lightside)                             ");
                yokaiCbox.Items.Add("Kyubi (Shadowside)                            ");
                yokaiCbox.Items.Add("Charlie (Lightside)                           ");
                yokaiCbox.Items.Add("Charlie (Shadowside)                          ");
                yokaiCbox.Items.Add("Zundoumaru (Lightside)                        ");
                yokaiCbox.Items.Add("Zundoumaru (Shadowside)                       ");
                yokaiCbox.Items.Add("Ungaikyo (Lightside)                          ");
                yokaiCbox.Items.Add("Ungaikyo (Shadowside)                         ");
                yokaiCbox.Items.Add("Jinta (Lightside)                             ");
                yokaiCbox.Items.Add("Jinta (Shadowside)                            ");
                yokaiCbox.Items.Add("Kantaro (Lightside)                           ");
                yokaiCbox.Items.Add("Kantaro (Shadowside)                          ");
                yokaiCbox.Items.Add("Kiborikkuma (Lightside)                       ");
                yokaiCbox.Items.Add("Kiborikkuma (Shadowside)                      ");
                yokaiCbox.Items.Add("Junior (Lightside)                            ");
                yokaiCbox.Items.Add("Junior (Shadowside)                           ");
                yokaiCbox.Items.Add("Micchy (Lightside)                            ");
                yokaiCbox.Items.Add("Micchy (Shadowside)                           ");
                yokaiCbox.Items.Add("Hyper Micchy (Lightside)                      ");
                yokaiCbox.Items.Add("Hyper Micchy (Shadowside)                     ");
                yokaiCbox.Items.Add("Hi no Shin (Lightside)                        ");
                yokaiCbox.Items.Add("Hi no Shin (Shadowside)                       ");
                yokaiCbox.Items.Add("Hungramps                                     ");
                yokaiCbox.Items.Add("Dimmy                                         ");
                yokaiCbox.Items.Add("Tattletell                                    ");
                yokaiCbox.Items.Add("Dismarelda                                    ");
                yokaiCbox.Items.Add("Hidabat                                       ");
                yokaiCbox.Items.Add("Frostina                                      ");
                yokaiCbox.Items.Add("Insomni                                       ");
                yokaiCbox.Items.Add("Blizzaria                                     ");
                yokaiCbox.Items.Add("Damona                                        ");
                yokaiCbox.Items.Add("Little Charrmer                               ");
                yokaiCbox.Items.Add("Roughraff                                     ");
                yokaiCbox.Items.Add("Mochismo                                      ");
                yokaiCbox.Items.Add("Blazion                                       ");
                yokaiCbox.Items.Add("Sgt. Burly                                    ");
                yokaiCbox.Items.Add("Venoct                                        ");
                yokaiCbox.Items.Add("Illuminoct                                    ");
                yokaiCbox.Items.Add("Shadow Venoct                                 ");
                yokaiCbox.Items.Add("Shogunyan                                     ");
                yokaiCbox.Items.Add("Snartle                                       ");
                yokaiCbox.Items.Add("Arachnus                                      ");
                yokaiCbox.Items.Add("Komashura                                     ");
                yokaiCbox.Items.Add("Noko                                          ");
                yokaiCbox.Items.Add("Komasan                                       ");
                yokaiCbox.Items.Add("Komajiro                                      ");
                yokaiCbox.Items.Add("Happierre                                     ");
                yokaiCbox.Items.Add("Hovernyan                                     ");
                yokaiCbox.Items.Add("Reuknight                                     ");
                yokaiCbox.Items.Add("Corptain                                      ");
                yokaiCbox.Items.Add("Toadal Dude                                   ");
                yokaiCbox.Items.Add("Silver Lining                                 ");
                yokaiCbox.Items.Add("Manjimutt                                     ");
                yokaiCbox.Items.Add("Jibanyan                                      ");
                yokaiCbox.Items.Add("Krystal Fox                                   ");
                yokaiCbox.Items.Add("Baku                                          ");
                yokaiCbox.Items.Add("Kyubi                                         ");
                yokaiCbox.Items.Add("Darkyubi                                      ");
                yokaiCbox.Items.Add("Master Nyada                                  ");
                yokaiCbox.Items.Add("Noway                                         ");
                yokaiCbox.Items.Add("Sandmeh                                       ");
                yokaiCbox.Items.Add("Mimikin                                       ");
                yokaiCbox.Items.Add("Mirapo                                        ");
                yokaiCbox.Items.Add("Robonyan                                      ");
                yokaiCbox.Items.Add("Goldenyan                                     ");
                yokaiCbox.Items.Add("Wiglin                                        ");
                yokaiCbox.Items.Add("Steppa                                        ");
                yokaiCbox.Items.Add("Rhyth                                         ");
                yokaiCbox.Items.Add("Walkappa                                      ");
                yokaiCbox.Items.Add("Nosirs                                        ");
                yokaiCbox.Items.Add("Cornfused                                     ");
                yokaiCbox.Items.Add("Whisper                                       ");
                yokaiCbox.Items.Add("Swelton                                       ");
                yokaiCbox.Items.Add("Usapyon                                       ");
                yokaiCbox.Items.Add("Spoilerina                                    ");
                yokaiCbox.Items.Add("Sighborg Y                                    ");
                yokaiCbox.Items.Add("Wobblewok                                     ");
                yokaiCbox.Items.Add("Deadcool                                      ");
                yokaiCbox.Items.Add("Gargaros                                      ");
                yokaiCbox.Items.Add("Ogralus                                       ");
                yokaiCbox.Items.Add("Orcanos                                       ");
                yokaiCbox.Items.Add("Gilgaros                                      ");
                yokaiCbox.Items.Add("Shirokuma                                     ");
                yokaiCbox.Items.Add("Punkupine                                     ");
                yokaiCbox.Items.Add("Sorrypus                                      ");
                yokaiCbox.Items.Add("Jabow                                         ");
                yokaiCbox.Items.Add("Beetall                                       ");
                yokaiCbox.Items.Add("Cruncha                                       ");
                yokaiCbox.Items.Add("Rhinormous                                    ");
                yokaiCbox.Items.Add("Hornaplenty                                   ");
                yokaiCbox.Items.Add("Mad Mountain                                  ");
                yokaiCbox.Items.Add("Lava Lord                                     ");
                yokaiCbox.Items.Add("Faux Kappa                                    ");
                yokaiCbox.Items.Add("Suu-san                                       ");
                yokaiCbox.Items.Add("Yamanba                                       ");
                yokaiCbox.Items.Add("Tamamo                                        ");
                yokaiCbox.Items.Add("Gyuuki                                        ");
                yokaiCbox.Items.Add("Narigama                                      ");
                yokaiCbox.Items.Add("Blobgoblin                                    ");
                yokaiCbox.Items.Add("Nekomata/Gusto Neko'ou Bastet                 ");
                yokaiCbox.Items.Add("Kappa Kappa'ou Sagojou                        ");
                yokaiCbox.Items.Add("Zashiki-warashi Tengu'ou Kurama               ");
                yokaiCbox.Items.Add("Kawauso                                       ");
                yokaiCbox.Items.Add("Enma                                          ");
                yokaiCbox.Items.Add("Lord Ananta                                   ");
                yokaiCbox.Items.Add("Douketsu                                      ");
                yokaiCbox.Items.Add("Shutendoji                                    ");
                yokaiCbox.Items.Add("Ogu Togu Mogu                                 ");
                yokaiCbox.Items.Add("Nurarihyon                                    ");
                yokaiCbox.Items.Add("Fudou Myouou Boy                              ");
                yokaiCbox.Items.Add("Whisper                                       ");
                yokaiCbox.Items.Add("Enma Awakened                                 ");
                yokaiCbox.Items.Add("Yami Enma                                     ");
                yokaiCbox.Items.Add("Nekomata/Gusto Neko'ou Bastet                 ");
                yokaiCbox.Items.Add("Kappa Kappa'ou Sagojou                        ");
                yokaiCbox.Items.Add("Zashiki-warashi Tengu'ou Kurama               ");
                yokaiCbox.Items.Add("Fudou Myouou Ten                              ");
                yokaiCbox.Items.Add("Suzaku (Sword Bearer)                         ");
                yokaiCbox.Items.Add("Genbu (Sword Bearer)                          ");
                yokaiCbox.Items.Add("Byakko (Sword Bearer)                         ");
                yokaiCbox.Items.Add("Ashura (Sword Bearer)                         ");
                yokaiCbox.Items.Add("Kakurenbou (Lightside)                        ");
                yokaiCbox.Items.Add("Kakurenbou (Shadowside)                       ");
                yokaiCbox.Items.Add("Hyakki-hime (Lightside)                       ");
                yokaiCbox.Items.Add("Hyakki-hime (Shadowside)                      ");
                yokaiCbox.Items.Add("Daniel (Lightside)                            ");
                yokaiCbox.Items.Add("Daniel (Shadowside)                           ");
                yokaiCbox.Items.Add("Itashikatanshi (Lightside)                    ");
                yokaiCbox.Items.Add("Itashikatanshi (Shadowside)                   ");
                yokaiCbox.Items.Add("Saya (Lightside)                              ");
                yokaiCbox.Items.Add("Saya (Shadowside)                             ");
                yokaiCbox.Items.Add("Rai Oton (Lightside)                          ");
                yokaiCbox.Items.Add("Rai Oton (Shadowside)                         ");
                yokaiCbox.Items.Add("Kage Orochi (Lightside)                       ");
                yokaiCbox.Items.Add("Kage Orochi (Shadowside)                      ");
                yokaiCbox.Items.Add("Bushinyan (Lightside)                         ");
                yokaiCbox.Items.Add("Bushinyan (Shadowside)                        ");
                yokaiCbox.Items.Add("Shurakoma (Lightside)                         ");
                yokaiCbox.Items.Add("Shurakoma (Shadowside)                        ");
                yokaiCbox.Items.Add("Tsuchigumo (Lightside)                        ");
                yokaiCbox.Items.Add("Tsuchigumo (Shadowside)                       ");
                yokaiCbox.Items.Add("Tsuchinoko (Lightside)                        ");
                yokaiCbox.Items.Add("Tsuchinoko (Shadowside)                       ");
                yokaiCbox.Items.Add("Ogama (Lightside)                             ");
                yokaiCbox.Items.Add("Ogama (Shadowside)                            ");
                yokaiCbox.Items.Add("Doyapon (Lightside)                           ");
                yokaiCbox.Items.Add("Doyapon (Shadowside)                          ");
                yokaiCbox.Items.Add("Inugami (Lightside)                           ");
                yokaiCbox.Items.Add("Inugami (Shadowside)                          ");
                yokaiCbox.Items.Add("Kezurin (Lightside)                           ");
                yokaiCbox.Items.Add("Kezurin (Shadowside)                          ");
                yokaiCbox.Items.Add("Robonyan 00 (Lightside)                       ");
                yokaiCbox.Items.Add("Robonyan 00 (Shadowside)                      ");
                yokaiCbox.Items.Add("Becchan (Lightside)                           ");
                yokaiCbox.Items.Add("Becchan (Shadowside)                          ");
                yokaiCbox.Items.Add("Dameboy (Lightside)                           ");
                yokaiCbox.Items.Add("Dameboy (Shadowside)                          ");
                yokaiCbox.Items.Add("Awevil                                        ");
                yokaiCbox.Items.Add("Demuncher                                     ");
                yokaiCbox.Items.Add("Slicenrice                                    ");
                yokaiCbox.Items.Add("Signiton                                      ");
                yokaiCbox.Items.Add("Molar Petite                                  ");
                yokaiCbox.Items.Add("Shmoopie                                      ");
                yokaiCbox.Items.Add("Lie-in Heart                                  ");
                yokaiCbox.Items.Add("Wazzat                                        ");
                yokaiCbox.Items.Add("Nekidspeed                                    ");
                yokaiCbox.Items.Add("Count Zapaway                                 ");
                yokaiCbox.Items.Add("B3-NK1                                        ");
                yokaiCbox.Items.Add("Rocky Badboya                                 ");
                yokaiCbox.Items.Add("Smogmella                                     ");
                yokaiCbox.Items.Add("Drizzelda                                     ");
                yokaiCbox.Items.Add("Poofessor                                     ");
                yokaiCbox.Items.Add("Ray O'Light                                   ");
                yokaiCbox.Items.Add("Legsit                                        ");
                yokaiCbox.Items.Add("Snottle                                       ");
                yokaiCbox.Items.Add("Kukuri-hime                                   ");
                yokaiCbox.Items.Add("Azukiarai                                     ");
                yokaiCbox.Items.Add("Fukurou                                       ");
                yokaiCbox.Items.Add("Kenshin Amaterasu                             ");
                yokaiCbox.Items.Add("Gesshin Tsukuyomi                             ");
                yokaiCbox.Items.Add("Sproink                                       ");
                yokaiCbox.Items.Add("Sproink                                       ");
                yokaiCbox.Items.Add("Sproink                                       ");
                yokaiCbox.Items.Add("Sproink                                       ");
                yokaiCbox.Items.Add("Sproink                                       ");
                yokaiCbox.Items.Add("Sproink                                       ");
                yokaiCbox.Items.Add("Sproink                                       ");
                yokaiCbox.Items.Add("Sproink                                       ");
                yokaiCbox.Items.Add("Sproink                                       ");
                yokaiCbox.Items.Add("Sproink                                       ");
                yokaiCbox.Items.Add("Hoggles                                       ");
                yokaiCbox.Items.Add("Hoggles                                       ");
                yokaiCbox.Items.Add("Hoggles                                       ");
                yokaiCbox.Items.Add("Hoggles                                       ");
                yokaiCbox.Items.Add("Hoggles                                       ");
                yokaiCbox.Items.Add("Hoggles                                       ");
                yokaiCbox.Items.Add("Hoggles                                       ");
                yokaiCbox.Items.Add("Hoggles                                       ");
                yokaiCbox.Items.Add("Raidenryu                                     ");
                yokaiCbox.Items.Add("Raidenryu                                     ");
                yokaiCbox.Items.Add("Raidenryu                                     ");
                yokaiCbox.Items.Add("Wobblewok                                     ");
                yokaiCbox.Items.Add("Wobblewok                                     ");
                yokaiCbox.Items.Add("Wobblewok                                     ");
                yokaiCbox.Items.Add("Wobblewok                                     ");
                yokaiCbox.Items.Add("Wobblewok                                     ");
                yokaiCbox.Items.Add("Wobblewok                                     ");
                yokaiCbox.Items.Add("Gargaros                                      ");
                yokaiCbox.Items.Add("Gargaros                                      ");
                yokaiCbox.Items.Add("Gargaros                                      ");
                yokaiCbox.Items.Add("Gargaros                                      ");
                yokaiCbox.Items.Add("Gargaros                                      ");
                yokaiCbox.Items.Add("Gargaros                                      ");
                yokaiCbox.Items.Add("Ogralus                                       ");
                yokaiCbox.Items.Add("Ogralus                                       ");
                yokaiCbox.Items.Add("Ogralus                                       ");
                yokaiCbox.Items.Add("Ogralus                                       ");
                yokaiCbox.Items.Add("Ogralus                                       ");
                yokaiCbox.Items.Add("Ogralus                                       ");
                yokaiCbox.Items.Add("Orcanos                                       ");
                yokaiCbox.Items.Add("Orcanos                                       ");
                yokaiCbox.Items.Add("Orcanos                                       ");
                yokaiCbox.Items.Add("Orcanos                                       ");
                yokaiCbox.Items.Add("Orcanos                                       ");
                yokaiCbox.Items.Add("Orcanos                                       ");
                yokaiCbox.Items.Add("McKraken                                      ");
                yokaiCbox.Items.Add("McKraken                                      ");
                yokaiCbox.Items.Add("McKraken                                      ");
                yokaiCbox.Items.Add("McKraken                                      ");
                yokaiCbox.Items.Add("McKraken                                      ");
                yokaiCbox.Items.Add("McKraken                                      ");
                yokaiCbox.Items.Add("Demuncher                                     ");
                yokaiCbox.Items.Add("Demuncher                                     ");
                yokaiCbox.Items.Add("Demuncher                                     ");
                yokaiCbox.Items.Add("Demuncher                                     ");
                yokaiCbox.Items.Add("Demuncher                                     ");
                yokaiCbox.Items.Add("Demuncher                                     ");
                yokaiCbox.Items.Add("Hi no Shin Shadow                             ");
                yokaiCbox.Items.Add("Hi no Shin Shadow                             ");
                yokaiCbox.Items.Add("Hi no Shin Shadow                             ");
                yokaiCbox.Items.Add("Hi no Shin Shadow                             ");
                yokaiCbox.Items.Add("Hi no Shin Shadow                             ");
                yokaiCbox.Items.Add("Hi no Shin Shadow                             ");
                yokaiCbox.Items.Add("Fudou Myouou 1                                ");
                yokaiCbox.Items.Add("Fudou Myouou 2                                ");
                yokaiCbox.Items.Add("Fudou Myouou 3                                ");
                yokaiCbox.Items.Add("Suzaku (Sword Bearer)                         ");
                yokaiCbox.Items.Add("Suzaku (Celestial)                            ");
                yokaiCbox.Items.Add("Genbu (Sword Bearer)                          ");
                yokaiCbox.Items.Add("Genbu (Celestial)                             ");
                yokaiCbox.Items.Add("Byakko (Sword Bearer)                         ");
                yokaiCbox.Items.Add("Byakko (Celestial)                            ");
                yokaiCbox.Items.Add("Asura (Sword Bearer)                          ");
                yokaiCbox.Items.Add("Yamamba                                       ");
                yokaiCbox.Items.Add("Yamamba                                       ");
                yokaiCbox.Items.Add("Yamamba                                       ");
                yokaiCbox.Items.Add("Yamamba                                       ");
                yokaiCbox.Items.Add("Yamamba                                       ");
                yokaiCbox.Items.Add("Yamamba                                       ");
                yokaiCbox.Items.Add("Tamamo no Mae                                 ");
                yokaiCbox.Items.Add("Tamamo no Mae                                 ");
                yokaiCbox.Items.Add("Tamamo no Mae                                 ");
                yokaiCbox.Items.Add("Tamamo no Mae                                 ");
                yokaiCbox.Items.Add("Tamamo no Mae                                 ");
                yokaiCbox.Items.Add("Tamamo no Mae                                 ");
                yokaiCbox.Items.Add("Shien                                         ");
                yokaiCbox.Items.Add("Rocky Badboya                                 ");
                yokaiCbox.Items.Add("Snartle                                       ");
                yokaiCbox.Items.Add("Damona (Shadowside)                           ");
                yokaiCbox.Items.Add("Jinta (Shadowside)                            ");
                yokaiCbox.Items.Add("Dameboy (Shadowside)                          ");
                yokaiCbox.Items.Add("Bushinyan (Shadowside)                        ");
                yokaiCbox.Items.Add("Lie-in Heart                                  ");
                yokaiCbox.Items.Add("Signiton                                      ");
                yokaiCbox.Items.Add("Robonyan                                      ");
                yokaiCbox.Items.Add("Goldenyan                                     ");
                yokaiCbox.Items.Add("Sighborg Y                                    ");
                yokaiCbox.Items.Add("Robonyan00                                    ");
                yokaiCbox.Items.Add("Jibanyan B                                    ");
                yokaiCbox.Items.Add("Komasan B                                     ");
                yokaiCbox.Items.Add("Usapyon B                                     ");
                yokaiCbox.Items.Add("Hovernyan                                     ");
                yokaiCbox.Items.Add("Sgt. Burly                                    ");
                yokaiCbox.Items.Add("Slimamander (Shadowside) (Hyper)              ");
                yokaiCbox.Items.Add("Kenshin Amaterasu                             ");
                yokaiCbox.Items.Add("Gesshin Tsukuyomi                             ");
                yokaiCbox.Items.Add("Neko'ou Bastet                                ");
                yokaiCbox.Items.Add("Kappa'ou Sagojou                              ");
                yokaiCbox.Items.Add("Tengu'ou Kurama                               ");
                yokaiCbox.Items.Add("Tamamo                                        ");
                yokaiCbox.Items.Add("Hamham (Shadowside)                           ");
                yokaiCbox.Items.Add("Wiglin                                        ");
                yokaiCbox.Items.Add("Roughraff                                     ");
                yokaiCbox.Items.Add("Kakurenbou (Shadowside)                       ");
                yokaiCbox.Items.Add("Sproink                                       ");
                yokaiCbox.Items.Add("Legsit                                        ");
                yokaiCbox.Items.Add("Doyapon                                       ");
                yokaiCbox.Items.Add("Jibanyan B                                    ");
                yokaiCbox.Items.Add("Komasan B                                     ");
                yokaiCbox.Items.Add("Usapyon B                                     ");
                yokaiCbox.Items.Add("Fubuki-hime (Shadowside)                      ");
                yokaiCbox.Items.Add("Hyakki-hime (Shadowside)                      ");
                yokaiCbox.Items.Add("Touma                                         ");
                yokaiCbox.Items.Add("Summer                                        ");
                yokaiCbox.Items.Add("Akinori                                       ");
                yokaiCbox.Items.Add("Jack                                          ");
                yokaiCbox.Items.Add("Nate                                          ");
                yokaiCbox.Items.Add("Katie                                         ");
                yokaiCbox.Items.Add("Hailey Anne                                   ");
                yokaiCbox.Items.Add("Blonde Guy (Alt) (NPC)                        ");
                yokaiCbox.Items.Add("Formal Guy (Alt) (NPC)                        ");
                yokaiCbox.Items.Add("Blonde Guy (Alt) (NPC)                        ");
                yokaiCbox.Items.Add("Formal Guy (Alt) (NPC)                        ");
                yokaiCbox.Items.Add("Monk Guy                                      ");
                yokaiCbox.Items.Add("CowBoy Guy                                    ");
                yokaiCbox.Items.Add("Smart Monkey                                  ");
                yokaiCbox.Items.Add("Jinpei Jiba                                   ");
                yokaiCbox.Items.Add("Shiba Dog                                     ");
                yokaiCbox.Items.Add("Black Shiba Dog                               ");
                yokaiCbox.Items.Add("Yellow Shiba Dog                              ");
                yokaiCbox.Items.Add("Mitsue                                        ");
                yokaiCbox.Items.Add("Alien                                         ");
                yokaiCbox.Items.Add("Honmaguro-taishou (Shadowside) (No clouds)    ");
                yokaiCbox.Items.Add("Touma (Horse)                                 ");
                yokaiCbox.Items.Add("Touma (Horse) (Alt)                           ");
                yokaiCbox.Items.Add("Touma (Horse) (Alt)                           ");
                yokaiCbox.Items.Add("Katie (Horse)                                 ");
                yokaiCbox.Items.Add("Old Guy                                       ");
                yokaiCbox.Items.Add("Yamakasa Demon                                ");
                yokaiCbox.Items.Add("Yamakasa Demon                                ");
                yokaiCbox.Items.Add("Tsuchinoko (Normal)                           ");
                yokaiCbox.Items.Add("Tsuchinoko (Lightside)                        ");
                yokaiCbox.Items.Add("Tsuchinoko (Shadowside)                       ");
                foreach (ListViewItem item in yokaiListView.SelectedItems)
                    yokaiCbox.SelectedIndex = new GetYokai().pickYokaiIDNumber(saveFileParams.UserYoKaiList[item.Index].YoKai_Signature ?? "");
            }
            else
            {
                yokaiCbox.Items.Clear();
                yokaiCbox.Items.Add("");
                yokaiCbox.Items.Add("Arachnus                          ");
                yokaiCbox.Items.Add("Arachnus (Lightside)              ");
                yokaiCbox.Items.Add("Ashura                            ");
                yokaiCbox.Items.Add("Ashura big                        ");
                yokaiCbox.Items.Add("Awevil                            ");
                yokaiCbox.Items.Add("Azukiarai                         ");
                yokaiCbox.Items.Add("B3-NK1                            ");
                yokaiCbox.Items.Add("Baku                              ");
                yokaiCbox.Items.Add("Banchou (Lightside)               ");
                yokaiCbox.Items.Add("Becchan (Lightside)               ");
                yokaiCbox.Items.Add("Beetall                           ");
                yokaiCbox.Items.Add("Benkei                            ");
                yokaiCbox.Items.Add("Blazion                           ");
                yokaiCbox.Items.Add("Blizzaria                         ");
                yokaiCbox.Items.Add("Blobgoblin                        ");
                yokaiCbox.Items.Add("Bushinyan (Lightside)             ");
                yokaiCbox.Items.Add("Byakko                            ");
                yokaiCbox.Items.Add("Byakko                            ");
                yokaiCbox.Items.Add("Charlie (Lightside)               ");
                yokaiCbox.Items.Add("Choky (Lightside)                 ");
                yokaiCbox.Items.Add("Cornfused                         ");
                yokaiCbox.Items.Add("Corptain                          ");
                yokaiCbox.Items.Add("Count Zapaway                     ");
                yokaiCbox.Items.Add("Cruncha                           ");
                yokaiCbox.Items.Add("Dameboy (Lightside)               ");
                yokaiCbox.Items.Add("Damona                            ");
                yokaiCbox.Items.Add("Daniel (Lightside)                ");
                yokaiCbox.Items.Add("Darkyubi                          ");
                yokaiCbox.Items.Add("Deadcool                          ");
                yokaiCbox.Items.Add("Demuncher                         ");
                yokaiCbox.Items.Add("Dimmy                             ");
                yokaiCbox.Items.Add("Dismarelda                        ");
                yokaiCbox.Items.Add("Douketsu                          ");
                yokaiCbox.Items.Add("Doyapon (Lightside)               ");
                yokaiCbox.Items.Add("Drizzelda                         ");
                yokaiCbox.Items.Add("Enma                              ");
                yokaiCbox.Items.Add("Enma Awoken                       ");
                yokaiCbox.Items.Add("Faux Kappa                        ");
                yokaiCbox.Items.Add("Frostina                          ");
                yokaiCbox.Items.Add("Fubuki-hime (Lightside)           ");
                yokaiCbox.Items.Add("Fudou Myouou                      ");
                yokaiCbox.Items.Add("Fudou Myouou Boy                  ");
                yokaiCbox.Items.Add("Fudou Myouou Ten                  ");
                yokaiCbox.Items.Add("Fudou Myouou-kai                  ");
                yokaiCbox.Items.Add("Fukurou                           ");
                yokaiCbox.Items.Add("Fuu-kun (Lightside)               ");
                yokaiCbox.Items.Add("Gargaros                          ");
                yokaiCbox.Items.Add("Genbu                             ");
                yokaiCbox.Items.Add("Genbu 2                           ");
                yokaiCbox.Items.Add("Gentou                            ");
                yokaiCbox.Items.Add("Gesshin Tsukuyomi                 ");
                yokaiCbox.Items.Add("Gilgaros                          ");
                yokaiCbox.Items.Add("Goemon                            ");
                yokaiCbox.Items.Add("Goldenyan                         ");
                yokaiCbox.Items.Add("Gunshin Susanoo                   ");
                yokaiCbox.Items.Add("Gyuuki                            ");
                yokaiCbox.Items.Add("Hakushu                           ");
                yokaiCbox.Items.Add("Hamham (Lightside)                ");
                yokaiCbox.Items.Add("Happierre                         ");
                yokaiCbox.Items.Add("Hare-onna (Lightside)             ");
                yokaiCbox.Items.Add("Hi no Shin (Lightside)            ");
                yokaiCbox.Items.Add("Hidabat                           ");
                yokaiCbox.Items.Add("Himoji (Lightside)                ");
                yokaiCbox.Items.Add("Honmaguro-taishou (Lightside)     ");
                yokaiCbox.Items.Add("Hornaplenty                       ");
                yokaiCbox.Items.Add("Hovernyan                         ");
                yokaiCbox.Items.Add("Hungramps                         ");
                yokaiCbox.Items.Add("Hyakki-hime (Lightside)           ");
                yokaiCbox.Items.Add("Illuminoct                        ");
                yokaiCbox.Items.Add("Insomni                           ");
                yokaiCbox.Items.Add("Inugami (Lightside)               ");
                yokaiCbox.Items.Add("Itashikatanshi (Lightside)        ");
                yokaiCbox.Items.Add("Jabow                             ");
                yokaiCbox.Items.Add("Jibanyan  (Lightside)             ");
                yokaiCbox.Items.Add("Jibanyan                          ");
                yokaiCbox.Items.Add("Jibanyan B                        ");
                yokaiCbox.Items.Add("Jinta (Lightside)                 ");
                yokaiCbox.Items.Add("Junior (Lightside)                ");
                yokaiCbox.Items.Add("Kage Orochi (Lightside)           ");
                yokaiCbox.Items.Add("Kaibyou Kamaitachi                ");
                yokaiCbox.Items.Add("Kakurenbou (Lightside)            ");
                yokaiCbox.Items.Add("Kantaro (Lightside)               ");
                yokaiCbox.Items.Add("Kappa                             ");
                yokaiCbox.Items.Add("Kappa'ou Sagojou                  ");
                yokaiCbox.Items.Add("Kawauso                           ");
                yokaiCbox.Items.Add("Kenshin Amaterasu                 ");
                yokaiCbox.Items.Add("Kezurin (Lightside)               ");
                yokaiCbox.Items.Add("Kiborikkuma (Lightside)           ");
                yokaiCbox.Items.Add("Kirin                             ");
                yokaiCbox.Items.Add("Komajiro (Lightside)              ");
                yokaiCbox.Items.Add("Komajiro                          ");
                yokaiCbox.Items.Add("Komasan  (Lightside)              ");
                yokaiCbox.Items.Add("Komasan                           ");
                yokaiCbox.Items.Add("Komasan B                         ");
                yokaiCbox.Items.Add("Komashura                         ");
                yokaiCbox.Items.Add("Krystal Fox                       ");
                yokaiCbox.Items.Add("Kukuri-hime                       ");
                yokaiCbox.Items.Add("Kuuten                            ");
                yokaiCbox.Items.Add("Kyubi  (Lightside)                ");
                yokaiCbox.Items.Add("Kyubi                             ");
                yokaiCbox.Items.Add("Kyunshii (Lightside)              ");
                yokaiCbox.Items.Add("Lava Lord                         ");
                yokaiCbox.Items.Add("Legsit                            ");
                yokaiCbox.Items.Add("Lie-in Heart                      ");
                yokaiCbox.Items.Add("Little Charrmer                   ");
                yokaiCbox.Items.Add("Lord Ananta                       ");
                yokaiCbox.Items.Add("Mad Mountain                      ");
                yokaiCbox.Items.Add("Manjimutt                         ");
                yokaiCbox.Items.Add("Master Nyada                      ");
                yokaiCbox.Items.Add("McKraken                          ");
                yokaiCbox.Items.Add("Merameraion (Lightside)           ");
                yokaiCbox.Items.Add("Micchy (Lightside)                ");
                yokaiCbox.Items.Add("Micchy Hyper (Lightside)          ");
                yokaiCbox.Items.Add("Mimikin                           ");
                yokaiCbox.Items.Add("Mirapo                            ");
                yokaiCbox.Items.Add("Mochismo                          ");
                yokaiCbox.Items.Add("Molar Petite                      ");
                yokaiCbox.Items.Add("Narigama                          ");
                yokaiCbox.Items.Add("Nekidspeed                        ");
                yokaiCbox.Items.Add("Neko'ou Bastet                    ");
                yokaiCbox.Items.Add("Nekomata                          ");
                yokaiCbox.Items.Add("Noko                              ");
                yokaiCbox.Items.Add("Nosirs                            ");
                yokaiCbox.Items.Add("Noway                             ");
                yokaiCbox.Items.Add("Nurarihyon                        ");
                yokaiCbox.Items.Add("Ogama (Lightside)                 ");
                yokaiCbox.Items.Add("Ogralus                           ");
                yokaiCbox.Items.Add("Ogu Togu Mogu                     ");
                yokaiCbox.Items.Add("Omatsu                            ");
                yokaiCbox.Items.Add("Orcanos                           ");
                yokaiCbox.Items.Add("Orochi (Lightside)                ");
                yokaiCbox.Items.Add("Pakkun (Lightside)                ");
                yokaiCbox.Items.Add("Poofessor                         ");
                yokaiCbox.Items.Add("Punkupine                         ");
                yokaiCbox.Items.Add("Rai Oton (Lightside)              ");
                yokaiCbox.Items.Add("Rai-chan (Lightside)              ");
                yokaiCbox.Items.Add("Raidenryu                         ");
                yokaiCbox.Items.Add("Ray O'Light                       ");
                yokaiCbox.Items.Add("Reuknight                         ");
                yokaiCbox.Items.Add("Rhinormous                        ");
                yokaiCbox.Items.Add("Robonyan                          ");
                yokaiCbox.Items.Add("Robonyan 00 (Lightside)           ");
                yokaiCbox.Items.Add("Rocky Badboya                     ");
                yokaiCbox.Items.Add("Roughraff                         ");
                yokaiCbox.Items.Add("Sandmeh                           ");
                yokaiCbox.Items.Add("Saya (Lightside)                  ");
                yokaiCbox.Items.Add("Seiryuu (Lightside)               ");
                yokaiCbox.Items.Add("Semicolon (Lightside)             ");
                yokaiCbox.Items.Add("Sgt. Burly                        ");
                yokaiCbox.Items.Add("Shadow Venoct                     ");
                yokaiCbox.Items.Add("Shien                             ");
                yokaiCbox.Items.Add("Shirokuma                         ");
                yokaiCbox.Items.Add("Shmoopie                          ");
                yokaiCbox.Items.Add("Shogunyan                         ");
                yokaiCbox.Items.Add("Shuka                             ");
                yokaiCbox.Items.Add("Shuka Natsume                     ");
                yokaiCbox.Items.Add("Shurakoma (Lightside)             ");
                yokaiCbox.Items.Add("Shutendoji                        ");
                yokaiCbox.Items.Add("Sighborg Y                        ");
                yokaiCbox.Items.Add("Signiton                          ");
                yokaiCbox.Items.Add("Silver Lining                     ");
                yokaiCbox.Items.Add("Slicenrice                        ");
                yokaiCbox.Items.Add("Smogmella                         ");
                yokaiCbox.Items.Add("Snartle                           ");
                yokaiCbox.Items.Add("Snottle                           ");
                yokaiCbox.Items.Add("Sorrypus                          ");
                yokaiCbox.Items.Add("Souryuu                           ");
                yokaiCbox.Items.Add("Spoilerina                        ");
                yokaiCbox.Items.Add("Steppa                            ");
                yokaiCbox.Items.Add("Suu-san                           ");
                yokaiCbox.Items.Add("Suzaku                            ");
                yokaiCbox.Items.Add("Suzaku  2                         ");
                yokaiCbox.Items.Add("Swelton                           ");
                yokaiCbox.Items.Add("Tamamo                            ");
                yokaiCbox.Items.Add("Tattletell                        ");
                yokaiCbox.Items.Add("Tengu'ou Kurama                   ");
                yokaiCbox.Items.Add("Toadal Dude                       ");
                yokaiCbox.Items.Add("Tsuchigumo (Lightside)            ");
                yokaiCbox.Items.Add("Tsuchinoko (Lightside)            ");
                yokaiCbox.Items.Add("Ungaikyo (Lightside)              ");
                yokaiCbox.Items.Add("Uribou (Lightside)                ");
                yokaiCbox.Items.Add("Usapyon                           ");
                yokaiCbox.Items.Add("Usapyon (2)                       ");
                yokaiCbox.Items.Add("Usapyon B                         ");
                yokaiCbox.Items.Add("Venoct                            ");
                yokaiCbox.Items.Add("Walkappa                          ");
                yokaiCbox.Items.Add("Wazzat                            ");
                yokaiCbox.Items.Add("Whisper                           ");
                yokaiCbox.Items.Add("Whisper (Future)                  ");
                yokaiCbox.Items.Add("Wiglin                            ");
                yokaiCbox.Items.Add("Wobblewok                         ");
                yokaiCbox.Items.Add("Yamanba                           ");
                yokaiCbox.Items.Add("Yami Enma                         ");
                yokaiCbox.Items.Add("Yasha Enma                        ");
                yokaiCbox.Items.Add("Yoshitsune                        ");
                yokaiCbox.Items.Add("Zashiki-warashi                   ");
                yokaiCbox.Items.Add("Zundoumaru (Lightside)            ");
                foreach (ListViewItem item in yokaiListView.SelectedItems)
                    yokaiCbox.SelectedIndex = new GetYokai().pickYokaiHealthyIndex(saveFileParams.UserYoKaiList[item.Index].YoKai_Signature ?? "");
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in yokaiListView.SelectedItems)
            {

                saveFileParams.UserYoKaiList[item.Index].YoKai_Signature = new SetYokai().pickYokaiBytesFromIdIndex(Convert.ToInt16(yokaiIdNbox.Value));

                //yokaiIdNbox.Value = new GetYokai().pickYokaiIDNumber(saveFileParams.UserYoKaiList[item.Index].YoKai_Signature ?? "");
                saveFileParams.UserYoKaiList[item.Index].YoKai_Level = Convert.ToInt32(yokaiLevelNbox.Value);
                saveFileParams.UserYoKaiList[item.Index].YoKai_YP = Convert.ToInt32(yokaiYpNbox.Value);
                saveFileParams.UserYoKaiList[item.Index].YoKai_HP = Convert.ToInt32(yokaiHpNbox.Value);
                saveFileParams.UserYoKaiList[item.Index].YoKai_XP = Convert.ToInt32(yokaiExpNbox.Value);
                saveFileParams.UserYoKaiList[item.Index].YoKai_PG = Convert.ToInt32(yokaiPgNbox.Value);
                saveFileParams.UserYoKaiList[item.Index].YoKai_Name = yokaiTbox.Text;
                saveFileParams.UserYoKaiList[item.Index].YoKai_HPplus = Convert.ToInt32(yokaiHpPlusNbox.Value);
                saveFileParams.UserYoKaiList[item.Index].YoKai_YPplus = Convert.ToInt32(yokaiYpPlusNbox.Value);
                saveFileParams.UserYoKaiList[item.Index].YoKai_PAplus = Convert.ToInt32(yokaiPdPlusNbox.Value);
                saveFileParams.UserYoKaiList[item.Index].YoKai_SAplus = Convert.ToInt32(yokaiSdPlusNbox.Value);
                saveFileParams.UserYoKaiList[item.Index].YoKai_STplus = Convert.ToInt32(yokaiStPlusNbox.Value);
                saveFileParams.UserYoKaiList[item.Index].YoKai_SPplus = Convert.ToInt32(yokaiSpPlusNbox.Value);
                saveFileParams.UserYoKaiList[item.Index].YoKai_Skill1 = new SetSkill().pickSkillBytes(yokaiBAtkCbox.SelectedIndex);
                saveFileParams.UserYoKaiList[item.Index].YoKai_Skill2 = new SetSkill().pickSkillBytes(yokaiSpSklCbox.SelectedIndex);
                saveFileParams.UserYoKaiList[item.Index].YoKai_Skill3 = new SetSkill().pickSkillBytes(yokaiExSklNbox.SelectedIndex);
                saveFileParams.UserYoKaiList[item.Index].YoKai_Skill4 = new SetSkill().pickSkillBytes(yokaiExSkl2Nbox.SelectedIndex);
                saveFileParams.UserYoKaiList[item.Index].YoKai_Skill5 = new SetSkill().pickSkillBytes(yokaiExSkl3Nbox.SelectedIndex);
                saveFileParams.UserYoKaiList[item.Index].YoKai_Skill6 = new SetSkill().pickSkillBytes(yokaiExSkl4Nbox.SelectedIndex);
                saveFileParams.UserYoKaiList[item.Index].ID1 = Convert.ToInt32(yokaiId1Nbox.Value);
                saveFileParams.UserYoKaiList[item.Index].ID2 = Convert.ToInt32(yokaiId2Nbox.Value);
                saveFileParams.UserYoKaiList[item.Index].YoKai_Order = Convert.ToInt32(yokaiOrderNbox.Value);

                if (yokaiUnknown12Nbox.Value == 0)
                    saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown12 = 1;
                if (yokaiUnknown13Nbox.Value == 0)
                    saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown13 = 1;
                if (yokaiUnknown15Nbox.Value == 0)
                    saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown15 = 1;

                item.Text = new GetYokai().pickYokaiName(saveFileParams.UserYoKaiList[item.Index].YoKai_Signature ?? "Invalid");

                if (yokaiId1Nbox.Value == 0)
                {
                    bool idExist = false;
                    saveFileParams.UserYoKaiList[item.Index].ID1 = 4096 + item.Index;
                    for (int i = 1; i < 1000; i++)
                    {
                        foreach (ListViewItem itemForId in yokaiListView.Items)
                        {
                            if (i == saveFileParams.UserYoKaiList[itemForId.Index].ID2)
                            {
                                foreach (ListViewItem charaId in mainCharacterViewList.Items)
                                {
                                    if (i == saveFileParams.MainCharacterList[charaId.Index].ID2)
                                    {
                                        idExist = true;
                                    }
                                }
                            }
                        }
                        if (!idExist)
                        {
                            saveFileParams.UserYoKaiList[item.Index].ID2 = i;
                            saveFileParams.UserYoKaiList[item.Index].YoKai_Order = i;
                            item.Selected = false;
                            item.Selected = true;
                            return;
                        }
                        idExist = false;
                    }
                }
            }
        }

        private void gatchaDaily_ValueChanged(object sender, EventArgs e)
        {
            saveFileParams.misc.Gatcha.gatchaTries = Convert.ToInt16(gatchaDaily.Value);
        }

        private void gatchaMax_ValueChanged(object sender, EventArgs e)
        {
            saveFileParams.misc.Gatcha.gatchaMaxTries = Convert.ToInt16(gatchaMax.Value);
        }

        private void positionXNbox_ValueChanged(object sender, EventArgs e)
        {
            saveFileParams.misc.LocalParams.PositionX = Convert.ToSingle(positionXNbox.Value);
        }

        private void positionYNbox_ValueChanged(object sender, EventArgs e)
        {
            saveFileParams.misc.LocalParams.PositionY = Convert.ToSingle(positionYNbox.Value);
        }

        private void positionZNbox_ValueChanged(object sender, EventArgs e)
        {
            saveFileParams.misc.LocalParams.PositionZ = Convert.ToSingle(positionZNbox.Value);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in yokaiListView.SelectedItems)
            {
                saveFileParams.UserYoKaiList[item.Index].YoKai_Signature = new SetYokai().pickYokaiBytesFromIdIndex(0);
                saveFileParams.UserYoKaiList[item.Index].YoKai_Level = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_YP = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_HP = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_XP = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_PG = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_Name = "";
                saveFileParams.UserYoKaiList[item.Index].ID1 = 0;
                saveFileParams.UserYoKaiList[item.Index].ID2 = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_Order = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_HPplus = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_YPplus = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_PAplus = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_SAplus = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_STplus = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_SPplus = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_Skill1 = new SetSkill().pickSkillBytes(0);
                saveFileParams.UserYoKaiList[item.Index].YoKai_Skill2 = new SetSkill().pickSkillBytes(0);
                saveFileParams.UserYoKaiList[item.Index].YoKai_Skill3 = new SetSkill().pickSkillBytes(0);
                saveFileParams.UserYoKaiList[item.Index].YoKai_Skill4 = new SetSkill().pickSkillBytes(0);
                saveFileParams.UserYoKaiList[item.Index].YoKai_Skill5 = new SetSkill().pickSkillBytes(0);
                saveFileParams.UserYoKaiList[item.Index].YoKai_Skill6 = new SetSkill().pickSkillBytes(0);
                saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown1 = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown2 = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown3 = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown4 = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown5 = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown6 = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown7 = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown8 = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown9 = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown10 = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown11 = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown12 = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown13 = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown14 = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown15 = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown16 = 0;
                saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown17 = 0;
                item.Text = "Empty";
            }
        }

        private void toumaNameTbox_TextChanged(object sender, EventArgs e)
        {
            if (Encoding.UTF8.GetBytes(toumaNameTbox.Text).Count() < 24)
            {
                saveFileParams.misc.ToumaName = toumaNameTbox.Text;
            }
            else
            {
                toumaNameTbox.Text = saveFileParams.misc.ToumaName;
            }

        }

        private void summerNameTbox_TextChanged(object sender, EventArgs e)
        {
            if (Encoding.UTF8.GetBytes(summerNameTbox.Text).Count() < 24)
            {
                saveFileParams.misc.SummerName = summerNameTbox.Text;
            }
            else
            {
                summerNameTbox.Text = saveFileParams.misc.SummerName;
            }
        }

        private void akinoriNameTbox_TextChanged(object sender, EventArgs e)
        {
            if (Encoding.UTF8.GetBytes(akinoriNameTbox.Text).Count() < 24)
            {
                saveFileParams.misc.AkinoriName = akinoriNameTbox.Text;
            }
            else
            {
                akinoriNameTbox.Text = saveFileParams.misc.AkinoriName;
            }
        }

        private void jackNameTbox_TextChanged(object sender, EventArgs e)
        {
            if (Encoding.UTF8.GetBytes(jackNameTbox.Text).Count() < 24)
            {
                saveFileParams.misc.JackName = jackNameTbox.Text;
            }
            else
            {
                jackNameTbox.Text = saveFileParams.misc.JackName;
            }
        }

        private void nateNameTbox_TextChanged(object sender, EventArgs e)
        {
            if (Encoding.UTF8.GetBytes(nateNameTbox.Text).Count() < 24)
            {
                saveFileParams.misc.NateName = nateNameTbox.Text;
            }
            else
            {
                nateNameTbox.Text = saveFileParams.misc.NateName;
            }
        }

        private void katieNameTbox_TextChanged(object sender, EventArgs e)
        {
            if (Encoding.UTF8.GetBytes(katieNameTbox.Text).Count() < 24)
            {
                saveFileParams.misc.KatieName = katieNameTbox.Text;
            }
            else
            {
                katieNameTbox.Text = saveFileParams.misc.KatieName;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    workFile = saveFileParams.injectParams(workFile);
                    File.WriteAllBytes(saveFileDialog1.FileName, workFile.ToArray());
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"Error message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void yokaiCbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isAdvancedList.Checked)
                yokaiIdNbox.Value = new GetYokai().pickYokaiIDNumber(new SetYokai().pickYokaiBytesFromIdIndex(yokaiCbox.SelectedIndex));
            else
                yokaiIdNbox.Value = new GetYokai().pickYokaiIDNumber(new SetYokai().pickBytesFromHealthyIndex(yokaiCbox.SelectedIndex));
        }

        private void yokaiTbox_TextChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in yokaiListView.SelectedItems)
            {
                if (Encoding.UTF8.GetBytes(yokaiTbox.Text).Count() < 24)
                {
                    saveFileParams.UserYoKaiList[item.Index].YoKai_Name = yokaiTbox.Text;
                }
                else
                {
                    yokaiTbox.Text = saveFileParams.UserYoKaiList[item.Index].YoKai_Name;
                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string[] skills = new SetSkill().pickYokaiSkillsByYokaiId(Convert.ToInt32(yokaiIdNbox.Value));
            if (new SetSkill().pickYokaiSkillsByYokaiId(Convert.ToInt32(yokaiIdNbox.Value)).Length > 1)
            {
                yokaiBAtkCbox.SelectedIndex = new GetSkill().pickYokaiSkill(skills[0]);
                yokaiSpSklCbox.SelectedIndex = new GetSkill().pickYokaiSkill(skills[1]);
                yokaiExSklNbox.SelectedIndex = new GetSkill().pickYokaiSkill(skills[2]);
                yokaiExSkl2Nbox.SelectedIndex = new GetSkill().pickYokaiSkill(skills[3]);
                yokaiExSkl3Nbox.SelectedIndex = new GetSkill().pickYokaiSkill(skills[4]);
                yokaiExSkl4Nbox.SelectedIndex = new GetSkill().pickYokaiSkill(skills[5]);
            }
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void foodItemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in foodItemList.SelectedItems)
            {
                foodCbox.SelectedIndex = new GetConsumable().pickConsumableIndex(saveFileParams.ConsumableList[item.Index].ItemSignature ?? "");
                foodQtdNbox.Value = saveFileParams.ConsumableList[item.Index].Quantity;
            }
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void foodReplace_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in foodItemList.SelectedItems)
            {
                saveFileParams.ConsumableList[item.Index].ItemSignature = new SetConsumable().pickBytesFromIndex(foodCbox.SelectedIndex);
                saveFileParams.ConsumableList[item.Index].Quantity = Convert.ToInt32(foodQtdNbox.Value);

                if (saveFileParams.ConsumableList[item.Index].ID2 == 0)
                {
                    saveFileParams.ConsumableList[item.Index].ID1 = item.Index;
                    for (int i = 1; i < 500; i++)
                    {
                        bool foodIdExist = false;
                        foreach (Consumable food in saveFileParams.ConsumableList)
                        {
                            if (i == food.ID2) { foodIdExist = true; break; }
                        }
                        if (!foodIdExist)
                        {
                            saveFileParams.ConsumableList[item.Index].ID2 = i;
                            saveFileParams.ConsumableList[item.Index].Order = i; break;
                        };
                    }
                }

                item.SubItems.Clear(); item.SubItems.AddRange(new string[]
                {
                    saveFileParams.ConsumableList[item.Index].ID2.ToString(),
                    saveFileParams.ConsumableList[item.Index].Order.ToString(),
                    new GetConsumable().pickConsumableName(saveFileParams.ConsumableList[item.Index].ItemSignature ?? "Invalid"),
                    saveFileParams.ConsumableList[item.Index].Quantity.ToString()
                });
                item.Text = saveFileParams.ConsumableList[item.Index].ID1.ToString();
            }
        }

        private void foodRemove_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in foodItemList.SelectedItems)
            {
                saveFileParams.ConsumableList[item.Index].ID1 = 0;
                saveFileParams.ConsumableList[item.Index].ID2 = 0;
                saveFileParams.ConsumableList[item.Index].Order = 0;
                saveFileParams.ConsumableList[item.Index].ItemSignature = "00-00-00-00";
                saveFileParams.ConsumableList[item.Index].Quantity = 0;

                item.SubItems.Clear(); item.SubItems.AddRange(new string[]
                {
                    saveFileParams.ConsumableList[item.Index].ID2.ToString(),
                    saveFileParams.ConsumableList[item.Index].Order.ToString(),
                    new GetConsumable().pickConsumableName(saveFileParams.ConsumableList[item.Index].ItemSignature ?? "Invalid"),
                    saveFileParams.ConsumableList[item.Index].Quantity.ToString()
                });
                item.Text = saveFileParams.ConsumableList[item.Index].ID1.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void yokaiIdNbox_ValueChanged(object sender, EventArgs e)
        {
            if (isAdvancedList.Checked)
                yokaiCbox.SelectedIndex = Convert.ToInt32(yokaiIdNbox.Value);
            else
                yokaiCbox.SelectedIndex = new GetYokai().pickYokaiHealthyIndex(new SetYokai().pickYokaiBytesFromIdIndex((int)yokaiIdNbox.Value));


        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            characterIdNbox.Value = new GetYokai().pickYokaiIDNumber(new SetYokai().pickYokaiBytesFromIdIndex(mainCharacterCbox.SelectedIndex));
        }

        private void mainCharacterViewList_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in mainCharacterViewList.SelectedItems)
            {
                characterIdNbox.Value = new GetYokai().pickYokaiIDNumber(saveFileParams.MainCharacterList[item.Index].Character_Signature ?? "");
                mainCharacterCbox.SelectedIndex = Convert.ToInt32(characterIdNbox.Value);
                characterLevelNbox.Value = saveFileParams.MainCharacterList[item.Index].Character_Level;
                characterYpNbox.Value = saveFileParams.MainCharacterList[item.Index].Character_YP;
                characterHpNbox.Value = saveFileParams.MainCharacterList[item.Index].Character_HP;
                characterExpNbox.Value = saveFileParams.MainCharacterList[item.Index].Character_XP;
                characterPGNbox.Value = saveFileParams.MainCharacterList[item.Index].Character_PG;
                characterId1Nbox.Value = saveFileParams.MainCharacterList[item.Index].ID1;
                characterId2Nbox.Value = saveFileParams.MainCharacterList[item.Index].ID2;
                characterOrderIdNbox.Value = saveFileParams.MainCharacterList[item.Index].Character_Order;
                characterHpPlusNbox.Value = saveFileParams.MainCharacterList[item.Index].Character_HPplus;
                characterYpPlusNbox.Value = saveFileParams.MainCharacterList[item.Index].Character_YPplus;
                characterPdNbox.Value = saveFileParams.MainCharacterList[item.Index].Character_PAplus;
                characterSdNbox.Value = saveFileParams.MainCharacterList[item.Index].Character_SAplus;
                characterStNbox.Value = saveFileParams.MainCharacterList[item.Index].Character_STplus;
                characterSpNbox.Value = saveFileParams.MainCharacterList[item.Index].Character_SPplus;
                characterBAtkCbox.SelectedIndex = new GetSkill().pickYokaiSkill(saveFileParams.MainCharacterList[item.Index].Character_Skill1 ?? "");
                characterSpSklCbox.SelectedIndex = new GetSkill().pickYokaiSkill(saveFileParams.MainCharacterList[item.Index].Character_Skill2 ?? "");
                characterExSklCbox.SelectedIndex = new GetSkill().pickYokaiSkill(saveFileParams.MainCharacterList[item.Index].Character_Skill3 ?? "");
                characterExSkl2Cbox.SelectedIndex = new GetSkill().pickYokaiSkill(saveFileParams.MainCharacterList[item.Index].Character_Skill4 ?? "");
                characterExSkl3Cbox.SelectedIndex = new GetSkill().pickYokaiSkill(saveFileParams.MainCharacterList[item.Index].Character_Skill5 ?? "");
                characterExSkl4Cbox.SelectedIndex = new GetSkill().pickYokaiSkill(saveFileParams.MainCharacterList[item.Index].Character_Skill6 ?? "");
                //yokaiUnknown1Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown1;
                //yokaiUnknown2Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown2;
                //yokaiUnknown3Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown3;
                //yokaiUnknown4Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown4;
                //yokaiUnknown5Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown5;
                //yokaiUnknown6Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown6;
                //yokaiUnknown7Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown7;
                //yokaiUnknown8Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown8;
                //yokaiUnknown9Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown9;
                //yokaiUnknown10Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown10;
                //yokaiUnknown11Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown11;
                //yokaiUnknown12Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown12;
                //yokaiUnknown13Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown13;
                //yokaiUnknown14Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown14;
                //yokaiUnknown15Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown15;
                //yokaiUnknown16Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown16;
                //yokaiUnknown17Nbox.Value = saveFileParams.UserYoKaiList[item.Index].YoKai_Unknown17;
            }
        }

        private void characterIdNbox_ValueChanged(object sender, EventArgs e)
        {
            mainCharacterCbox.SelectedIndex = Convert.ToInt32(characterIdNbox.Value);
        }

        private void yokaiLevelNbox_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox16_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void yokaiBAtkCbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void saveMainCharacterChanges_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in mainCharacterViewList.SelectedItems)
            {
                saveFileParams.MainCharacterList[item.Index].Character_Signature = new SetYokai().pickYokaiBytesFromIdIndex(Convert.ToInt16(characterIdNbox.Value));
                saveFileParams.MainCharacterList[item.Index].Character_Level = Convert.ToInt32(characterLevelNbox.Value);
                saveFileParams.MainCharacterList[item.Index].Character_YP = Convert.ToInt32(characterYpNbox.Value);
                saveFileParams.MainCharacterList[item.Index].Character_HP = Convert.ToInt32(characterHpNbox.Value);
                saveFileParams.MainCharacterList[item.Index].Character_XP = Convert.ToInt32(characterExpNbox.Value);
                saveFileParams.MainCharacterList[item.Index].Character_PG = Convert.ToInt32(characterPGNbox.Value);
                saveFileParams.MainCharacterList[item.Index].Character_HPplus = Convert.ToInt32(characterHpPlusNbox.Value);
                saveFileParams.MainCharacterList[item.Index].Character_YPplus = Convert.ToInt32(characterYpPlusNbox.Value);
                saveFileParams.MainCharacterList[item.Index].Character_PAplus = Convert.ToInt32(characterPdNbox.Value);
                saveFileParams.MainCharacterList[item.Index].Character_SAplus = Convert.ToInt32(characterSdNbox.Value);
                saveFileParams.MainCharacterList[item.Index].Character_STplus = Convert.ToInt32(characterStNbox.Value);
                saveFileParams.MainCharacterList[item.Index].Character_SPplus = Convert.ToInt32(characterSpNbox.Value);
                saveFileParams.MainCharacterList[item.Index].Character_Skill1 = new SetSkill().pickSkillBytes(characterBAtkCbox.SelectedIndex);
                saveFileParams.MainCharacterList[item.Index].Character_Skill2 = new SetSkill().pickSkillBytes(characterSpSklCbox.SelectedIndex);
                saveFileParams.MainCharacterList[item.Index].Character_Skill3 = new SetSkill().pickSkillBytes(characterExSklCbox.SelectedIndex);
                saveFileParams.MainCharacterList[item.Index].Character_Skill4 = new SetSkill().pickSkillBytes(characterExSkl2Cbox.SelectedIndex);
                saveFileParams.MainCharacterList[item.Index].Character_Skill5 = new SetSkill().pickSkillBytes(characterExSkl3Cbox.SelectedIndex);
                saveFileParams.MainCharacterList[item.Index].Character_Skill6 = new SetSkill().pickSkillBytes(characterExSkl4Cbox.SelectedIndex);
                saveFileParams.MainCharacterList[item.Index].ID1 = Convert.ToInt32(characterId1Nbox.Value);
                saveFileParams.MainCharacterList[item.Index].ID2 = Convert.ToInt32(characterId2Nbox.Value);
                saveFileParams.MainCharacterList[item.Index].Character_Order = Convert.ToInt32(characterOrderIdNbox.Value);

                //if (yokaiUnknown12Nbox.Value == 0)
                //    saveFileParams.MainCharacterList[item.Index].Character_Unknown12 = 1;
                //if (yokaiUnknown13Nbox.Value == 0)
                //    saveFileParams.MainCharacterList[item.Index].Character_Unknown13 = 1;
                //if (yokaiUnknown15Nbox.Value == 0)
                //    saveFileParams.MainCharacterList[item.Index].Character_Unknown15 = 1;

                item.Text = new GetYokai().pickYokaiName(saveFileParams.MainCharacterList[item.Index].Character_Signature ?? "Invalid");

                if (characterId1Nbox.Value == 0 && characterId2Nbox.Value == 0)
                {
                    bool idExist = false;
                    saveFileParams.MainCharacterList[item.Index].ID1 = item.Index;
                    for (int i = 1; i < 1000; i++)
                    {
                        foreach (ListViewItem itemForId in yokaiListView.Items)
                        {
                            if (i == saveFileParams.UserYoKaiList[itemForId.Index].ID2)
                            {
                                foreach (ListViewItem charaId in mainCharacterViewList.Items)
                                {
                                    if (i == saveFileParams.MainCharacterList[charaId.Index].ID2)
                                    {
                                        idExist = true;
                                    }
                                }
                            }
                        }
                        if (!idExist)
                        {
                            saveFileParams.MainCharacterList[item.Index].ID2 = i;
                            saveFileParams.MainCharacterList[item.Index].Character_Order = i;
                            item.Selected = false;
                            item.Selected = true;
                            return;
                        }
                        idExist = false;
                    }
                }
            }
        }

        private void removeMainCharacter_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in mainCharacterViewList.SelectedItems)
            {
                saveFileParams.MainCharacterList[item.Index].Character_Signature = new SetYokai().pickYokaiBytesFromIdIndex(0);
                saveFileParams.MainCharacterList[item.Index].Character_Level = 0;
                saveFileParams.MainCharacterList[item.Index].Character_YP = 0;
                saveFileParams.MainCharacterList[item.Index].Character_HP = 0;
                saveFileParams.MainCharacterList[item.Index].Character_XP = 0;
                saveFileParams.MainCharacterList[item.Index].Character_PG = 0;
                saveFileParams.MainCharacterList[item.Index].ID1 = 0;
                saveFileParams.MainCharacterList[item.Index].ID2 = 0;
                saveFileParams.MainCharacterList[item.Index].Character_Order = 0;
                saveFileParams.MainCharacterList[item.Index].Character_HPplus = 0;
                saveFileParams.MainCharacterList[item.Index].Character_YPplus = 0;
                saveFileParams.MainCharacterList[item.Index].Character_PAplus = 0;
                saveFileParams.MainCharacterList[item.Index].Character_SAplus = 0;
                saveFileParams.MainCharacterList[item.Index].Character_STplus = 0;
                saveFileParams.MainCharacterList[item.Index].Character_SPplus = 0;
                saveFileParams.MainCharacterList[item.Index].Character_Skill1 = new SetSkill().pickSkillBytes(0);
                saveFileParams.MainCharacterList[item.Index].Character_Skill2 = new SetSkill().pickSkillBytes(0);
                saveFileParams.MainCharacterList[item.Index].Character_Skill3 = new SetSkill().pickSkillBytes(0);
                saveFileParams.MainCharacterList[item.Index].Character_Skill4 = new SetSkill().pickSkillBytes(0);
                saveFileParams.MainCharacterList[item.Index].Character_Skill5 = new SetSkill().pickSkillBytes(0);
                saveFileParams.MainCharacterList[item.Index].Character_Skill6 = new SetSkill().pickSkillBytes(0);
                //saveFileParams.MainCharacterList[item.Index].Character_Unknown1 = 0;
                //saveFileParams.MainCharacterList[item.Index].Character_Unknown2 = 0;
                //saveFileParams.MainCharacterList[item.Index].Character_Unknown3 = 0;
                //saveFileParams.MainCharacterList[item.Index].Character_Unknown4 = 0;
                //saveFileParams.MainCharacterList[item.Index].Character_Unknown5 = 0;
                //saveFileParams.MainCharacterList[item.Index].Character_Unknown6 = 0;
                //saveFileParams.MainCharacterList[item.Index].Character_Unknown7 = 0;
                //saveFileParams.MainCharacterList[item.Index].Character_Unknown8 = 0;
                //saveFileParams.MainCharacterList[item.Index].Character_Unknown9 = 0;
                //saveFileParams.MainCharacterList[item.Index].Character_Unknown10 = 0;
                //saveFileParams.MainCharacterList[item.Index].Character_Unknown11 = 0;
                //saveFileParams.MainCharacterList[item.Index].Character_Unknown12 = 0;
                //saveFileParams.MainCharacterList[item.Index].Character_Unknown13 = 0;
                //saveFileParams.MainCharacterList[item.Index].Character_Unknown14 = 0;
                //saveFileParams.MainCharacterList[item.Index].Character_Unknown15 = 0;
                //saveFileParams.MainCharacterList[item.Index].Character_Unknown16 = 0;
                //saveFileParams.MainCharacterList[item.Index].Character_Unknown17 = 0;
                item.Text = "Empty";
            }
        }

        private void characterSkillLoaderBtn_Click(object sender, EventArgs e)
        {
            string[] skills = new SetSkill().pickYokaiSkillsByYokaiId(Convert.ToInt32(characterIdNbox.Value));
            if (skills.Length > 1)
            {
                characterBAtkCbox.SelectedIndex = new GetSkill().pickYokaiSkill(skills[0]);
                characterSpSklCbox.SelectedIndex = new GetSkill().pickYokaiSkill(skills[1]);
                characterExSklCbox.SelectedIndex = new GetSkill().pickYokaiSkill(skills[2]);
                characterExSkl2Cbox.SelectedIndex = new GetSkill().pickYokaiSkill(skills[3]);
                characterExSkl3Cbox.SelectedIndex = new GetSkill().pickYokaiSkill(skills[4]);
                characterExSkl4Cbox.SelectedIndex = new GetSkill().pickYokaiSkill(skills[5]);
            }
        }

        private void donateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.buymeacoffee.com/bqsantana") { UseShellExecute = true });
        }
    }
}