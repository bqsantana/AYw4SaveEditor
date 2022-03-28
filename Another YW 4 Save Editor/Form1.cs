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
            mainTabControl.Controls.Remove(tabPage2);
            mainTabControl.Controls.Remove(tabPage3);
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

                        yokaiListView.Items[0].Selected = true;

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
                yokaiCbox.Items.Add("Touma ");
                yokaiCbox.Items.Add("Summer ");
                yokaiCbox.Items.Add("Akinori       ");
                yokaiCbox.Items.Add("Akinori (Fit)   ");
                yokaiCbox.Items.Add("Jack ");
                yokaiCbox.Items.Add("Nate ");
                yokaiCbox.Items.Add("Katie ");
                yokaiCbox.Items.Add("Himoji (Lightside)   ");
                yokaiCbox.Items.Add("Himoji Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Himoji Shadow (Boss)     ");
                yokaiCbox.Items.Add("Pakkun/Gaburiel (Lightside)     ");
                yokaiCbox.Items.Add("Pakkun Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Pakkun Shadow (Boss)     ");
                yokaiCbox.Items.Add("Kyunshii (Lightside)   ");
                yokaiCbox.Items.Add("Kyunshii Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Kyunshii Shadow (Boss)   ");
                yokaiCbox.Items.Add("Hare-onna/Ame-onna (Lightside)     ");
                yokaiCbox.Items.Add("Hare-onna Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Choky (Lightside)   ");
                yokaiCbox.Items.Add("Choky Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Fubuki-hime (Lightside)   ");
                yokaiCbox.Items.Add("Fubuki-hime Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Fubuki-hime Shadow (Boss)    ");
                yokaiCbox.Items.Add("Merameraion (Lightside)   ");
                yokaiCbox.Items.Add("Merameraion Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Merameraion Shadow (Boss)    ");
                yokaiCbox.Items.Add("Orochi (Lightside)   ");
                yokaiCbox.Items.Add("Orochi Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Orochi Shadow (Boss)     ");
                yokaiCbox.Items.Add("Honmaguro-taishou (Lightside)     ");
                yokaiCbox.Items.Add("Honmaguro-taishou Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Honmaguro-taishou Shadow (Boss)    ");
                yokaiCbox.Items.Add("Semicolon (Lightside)   ");
                yokaiCbox.Items.Add("Semicolon Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Semicolon Shadow (Boss)  ");
                yokaiCbox.Items.Add("Komasan (Lightside)   ");
                yokaiCbox.Items.Add("Komasan Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Komajiro (Lightside)   ");
                yokaiCbox.Items.Add("Komajiro Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Komajiro Shadow (Boss)   ");
                yokaiCbox.Items.Add("Banchou (Lightside)   ");
                yokaiCbox.Items.Add("Banchou Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Banchou Shadow (Boss)    ");
                yokaiCbox.Items.Add("Seiryuu (Lightside)   ");
                yokaiCbox.Items.Add("Seiryuu Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Fuu-kun (Lightside)   ");
                yokaiCbox.Items.Add("Fuu-kun Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Fuu-kun Shadow (Boss)    ");
                yokaiCbox.Items.Add("Rai-chan (Lightside)   ");
                yokaiCbox.Items.Add("Rai-chan Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Rai-chan Shadow (Boss)   ");
                yokaiCbox.Items.Add("Hamham (Lightside)   ");
                yokaiCbox.Items.Add("Hamham Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Jibanyan (Lightside)   ");
                yokaiCbox.Items.Add("Jibanyan Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Uribou (Lightside)   ");
                yokaiCbox.Items.Add("Uribou Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Kyubi (Lightside)   ");
                yokaiCbox.Items.Add("Kyubi Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Kyubi Shadow (Boss)  ");
                yokaiCbox.Items.Add("Charlie (Lightside)   ");
                yokaiCbox.Items.Add("Charlie Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Zundoumaru (Lightside)   ");
                yokaiCbox.Items.Add("Zundoumaru Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Zundoumaru Shadow (Boss) ");
                yokaiCbox.Items.Add("Ungaikyo (Lightside)   ");
                yokaiCbox.Items.Add("Ungaikyo Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Jinta (Lightside)   ");
                yokaiCbox.Items.Add("Jinta Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Jinta Shadow (Boss)  ");
                yokaiCbox.Items.Add("Kantaro (Lightside)   ");
                yokaiCbox.Items.Add("Kantaro Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Kantaro Shadow (Boss)    ");
                yokaiCbox.Items.Add("Kiborikkuma (Lightside)   ");
                yokaiCbox.Items.Add("Kiborikkuma Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Junior/Burning Dragon (Lightside)     ");
                yokaiCbox.Items.Add("Junior Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Micchy (Lightside)   ");
                yokaiCbox.Items.Add("Micchy Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Micchy Hyper (Lightside)   ");
                yokaiCbox.Items.Add("Micchy Hyper Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Hi no Shin (Lightside)   ");
                yokaiCbox.Items.Add("Hi no Shin Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Hungramps (Normal)  ");
                yokaiCbox.Items.Add("Dimmy (Normal)      ");
                yokaiCbox.Items.Add("Tattletell (Normal)  ");
                yokaiCbox.Items.Add("Dismarelda (Normal)  ");
                yokaiCbox.Items.Add("Hidabat (Normal)  ");
                yokaiCbox.Items.Add("Frostina (Normal)  ");
                yokaiCbox.Items.Add("Insomni (Normal)  ");
                yokaiCbox.Items.Add("Insomni (Boss)   ");
                yokaiCbox.Items.Add("Blizzaria (Normal)  ");
                yokaiCbox.Items.Add("Damona (Normal)      ");
                yokaiCbox.Items.Add("Little Charrmer (Normal)  ");
                yokaiCbox.Items.Add("Roughraff (Normal)  ");
                yokaiCbox.Items.Add("Roughraff (Boss)     ");
                yokaiCbox.Items.Add("Mochismo (Normal)  ");
                yokaiCbox.Items.Add("Blazion (Normal)  ");
                yokaiCbox.Items.Add("Blazion (Boss)   ");
                yokaiCbox.Items.Add("Sgt. Burly (Normal)  ");
                yokaiCbox.Items.Add("Venoct (Normal)      ");
                yokaiCbox.Items.Add("Illuminoct (Normal)  ");
                yokaiCbox.Items.Add("Shadow Venoct (Normal)  ");
                yokaiCbox.Items.Add("Shogunyan (Normal)  ");
                yokaiCbox.Items.Add("Snartle (Normal)  ");
                yokaiCbox.Items.Add("Snartle (Boss)   ");
                yokaiCbox.Items.Add("Arachnus (Normal)  ");
                yokaiCbox.Items.Add("Arachnus (Boss)      ");
                yokaiCbox.Items.Add("Komashura (Normal)  ");
                yokaiCbox.Items.Add("Noko (Normal)      ");
                yokaiCbox.Items.Add("Komasan (Normal)  ");
                yokaiCbox.Items.Add("Komajiro (Normal)  ");
                yokaiCbox.Items.Add("Happierre (Normal)  ");
                yokaiCbox.Items.Add("Hovernyan (Normal)  ");
                yokaiCbox.Items.Add("Reuknight (Normal)  ");
                yokaiCbox.Items.Add("Reuknight (Boss)     ");
                yokaiCbox.Items.Add("Corptain (Normal)  ");
                yokaiCbox.Items.Add("Toadal Dude (Normal)  ");
                yokaiCbox.Items.Add("Toadal Dude (Boss)   ");
                yokaiCbox.Items.Add("Silver Lining (Normal)  ");
                yokaiCbox.Items.Add("Manjimutt (Normal)  ");
                yokaiCbox.Items.Add("Manjimutt (Boss)     ");
                yokaiCbox.Items.Add("Jibanyan (Normal)  ");
                yokaiCbox.Items.Add("Krystal Fox (Normal)  ");
                yokaiCbox.Items.Add("Baku (Normal)      ");
                yokaiCbox.Items.Add("Kyubi (Normal)      ");
                yokaiCbox.Items.Add("Darkyubi (Normal)  ");
                yokaiCbox.Items.Add("Master Nyada (Normal)  ");
                yokaiCbox.Items.Add("Noway (Normal)      ");
                yokaiCbox.Items.Add("Sandmeh (Normal)  ");
                yokaiCbox.Items.Add("Mimikin (Normal)  ");
                yokaiCbox.Items.Add("Mimikin (Boss)   ");
                yokaiCbox.Items.Add("Mirapo (Normal)      ");
                yokaiCbox.Items.Add("Robonyan (Normal)  ");
                yokaiCbox.Items.Add("Goldenyan (Normal)  ");
                yokaiCbox.Items.Add("Wiglin (Normal)      ");
                yokaiCbox.Items.Add("Steppa (Normal)      ");
                yokaiCbox.Items.Add("Steppa (Normal)      ");
                yokaiCbox.Items.Add("Walkappa (Normal)  ");
                yokaiCbox.Items.Add("Nosirs (Normal)      ");
                yokaiCbox.Items.Add("Cornfused (Normal)  ");
                yokaiCbox.Items.Add("Whisper (Normal)  ");
                yokaiCbox.Items.Add("Swelton (Normal)  ");
                yokaiCbox.Items.Add("Usapyon (Normal)  ");
                yokaiCbox.Items.Add("Usapyon (Normal)  ");
                yokaiCbox.Items.Add("Spoilerina (Normal)  ");
                yokaiCbox.Items.Add("Sighborg Y (Normal)  ");
                yokaiCbox.Items.Add("Wobblewok (Normal)  ");
                yokaiCbox.Items.Add("Deadcool (Normal)  ");
                yokaiCbox.Items.Add("Gargaros (Normal)  ");
                yokaiCbox.Items.Add("Ogralus (Normal)  ");
                yokaiCbox.Items.Add("Orcanos (Normal)  ");
                yokaiCbox.Items.Add("Gilgaros (Normal)  ");
                yokaiCbox.Items.Add("Shirokuma (Normal)  ");
                yokaiCbox.Items.Add("Punkupine (Normal)  ");
                yokaiCbox.Items.Add("Sorrypus (Normal)  ");
                yokaiCbox.Items.Add("Jabow (Normal)      ");
                yokaiCbox.Items.Add("Beetall (Normal)  ");
                yokaiCbox.Items.Add("Cruncha (Normal)  ");
                yokaiCbox.Items.Add("Rhinormous (Normal)  ");
                yokaiCbox.Items.Add("Hornaplenty (Normal)  ");
                yokaiCbox.Items.Add("Mad Mountain (Normal)  ");
                yokaiCbox.Items.Add("Lava Lord (Normal)  ");
                yokaiCbox.Items.Add("Faux Kappa (Normal)  ");
                yokaiCbox.Items.Add("McKraken (Normal)  ");
                yokaiCbox.Items.Add("Suu-san (Normal)  ");
                yokaiCbox.Items.Add("Yamanba (Normal)  ");
                yokaiCbox.Items.Add("Tamamo (Normal)      ");
                yokaiCbox.Items.Add("Gyuuki (Normal)      ");
                yokaiCbox.Items.Add("Narigama (Normal)  ");
                yokaiCbox.Items.Add("Blobgoblin (Normal)  ");
                yokaiCbox.Items.Add("Nekomata (Normal)  ");
                yokaiCbox.Items.Add("Kappa (Normal)      ");
                yokaiCbox.Items.Add("Zashiki-warashi (Normal)  ");
                yokaiCbox.Items.Add("Kawauso (Normal)  ");
                yokaiCbox.Items.Add("Enma (Normal)      ");
                yokaiCbox.Items.Add("Lord Ananta (Normal)  ");
                yokaiCbox.Items.Add("Douketsu (Normal)  ");
                yokaiCbox.Items.Add("Douketsu (Normal)  ");
                yokaiCbox.Items.Add("Shutendoji (Normal)  ");
                yokaiCbox.Items.Add("Ogu Togu Mogu (Normal)  ");
                yokaiCbox.Items.Add("Nurarihyon (Normal)  ");
                yokaiCbox.Items.Add("Fudou Myouou Boy (Normal)  ");
                yokaiCbox.Items.Add("Whisper (Normal)  ");
                yokaiCbox.Items.Add("Enma Awoken   ");
                yokaiCbox.Items.Add("Yami Enma       ");
                yokaiCbox.Items.Add("Kaibyou Kamaitachi (Normal)  ");
                yokaiCbox.Items.Add("Neko'ou Bastet (Normal)  ");
                yokaiCbox.Items.Add("Kappa'ou Sagojou (Normal)  ");
                yokaiCbox.Items.Add("Tengu'ou Kurama (Normal)  ");
                yokaiCbox.Items.Add("Omatsu ");
                yokaiCbox.Items.Add("Yoshitsune       ");
                yokaiCbox.Items.Add("Goemon ");
                yokaiCbox.Items.Add("Benkei ");
                yokaiCbox.Items.Add("Suzaku ");
                yokaiCbox.Items.Add("Genbu ");
                yokaiCbox.Items.Add("Byakko ");
                yokaiCbox.Items.Add("Kirin ");
                yokaiCbox.Items.Add("Souryuu       ");
                yokaiCbox.Items.Add("Gunshin Susanoo   ");
                yokaiCbox.Items.Add("Fudou Myouou   ");
                yokaiCbox.Items.Add("Fudou Myouou Ten   ");
                yokaiCbox.Items.Add("Suzaku ");
                yokaiCbox.Items.Add("Genbu 2       ");
                yokaiCbox.Items.Add("Byakko ");
                yokaiCbox.Items.Add("Ashura ");
                yokaiCbox.Items.Add("Shuka Natsume   ");
                yokaiCbox.Items.Add("[DONT_WORK] [DONT_WORK]   ");
                yokaiCbox.Items.Add("Jinta Shadow (Boss) (Boss)      ");
                yokaiCbox.Items.Add("Micchy Shadow (Boss) (Boss)      ");
                yokaiCbox.Items.Add("Micchy Eye Ball (Boss) (Boss)      ");
                yokaiCbox.Items.Add("Jorogumo (Boss) (Boss)    ");
                yokaiCbox.Items.Add("Shinmagunjin Fukurou (Boss) (Boss)  ");
                yokaiCbox.Items.Add("Overseer (Boss)    ");
                yokaiCbox.Items.Add("Overseer 2 (Boss)    ");
                yokaiCbox.Items.Add("Overseer 3 (Boss)    ");
                yokaiCbox.Items.Add("[DONT_WORK] [DONT_WORK]   ");
                yokaiCbox.Items.Add("Yami Enma       ");
                yokaiCbox.Items.Add("Enma ");
                yokaiCbox.Items.Add("Maten Soranaki (Boss)    ");
                yokaiCbox.Items.Add("Gilgaros (Boss)    ");
                yokaiCbox.Items.Add("Illuminoct (Boss)    ");
                yokaiCbox.Items.Add("Blizzaria (Boss)    ");
                yokaiCbox.Items.Add("Sgt. Burly (Boss)    ");
                yokaiCbox.Items.Add("Damona (Boss)    ");
                yokaiCbox.Items.Add("Manjimutt (Boss)    ");
                yokaiCbox.Items.Add("Enma Awoken   ");
                yokaiCbox.Items.Add("Raidenryu       ");
                yokaiCbox.Items.Add("Fudou Myouou   ");
                yokaiCbox.Items.Add("Fudou Myouou   ");
                yokaiCbox.Items.Add("Suzaku ");
                yokaiCbox.Items.Add("Suzaku big       ");
                yokaiCbox.Items.Add("Genbu ");
                yokaiCbox.Items.Add("Genbu big       ");
                yokaiCbox.Items.Add("Byakko ");
                yokaiCbox.Items.Add("Byakko big       ");
                yokaiCbox.Items.Add("Ashura ");
                yokaiCbox.Items.Add("Ashura big       ");
                yokaiCbox.Items.Add("Douketsu (Normal)  ");
                yokaiCbox.Items.Add("Douketsu (Normal)  ");
                yokaiCbox.Items.Add("Shutendoji (Normal)  ");
                yokaiCbox.Items.Add("Yamamba (Boss)    ");
                yokaiCbox.Items.Add("Tamamo no Mae (Boss)    ");
                yokaiCbox.Items.Add("Shien (Boss)    ");
                yokaiCbox.Items.Add("Shinma Kaira (Boss)    ");
                yokaiCbox.Items.Add("Shinma Kaira (Boss)    ");
                yokaiCbox.Items.Add("Jinta Shadow (Boss)    ");
                yokaiCbox.Items.Add("Jinta Shadow (Boss)    ");
                yokaiCbox.Items.Add("Jinta Shadow (Boss)    ");
                yokaiCbox.Items.Add("Jinta Shadow (Boss)    ");
                yokaiCbox.Items.Add("Mitsumata Nozuchi (Boss)    ");
                yokaiCbox.Items.Add("Mitsumata Nozuchi (Boss)    ");
                yokaiCbox.Items.Add("Mitsumata Nozuchi (Boss)    ");
                yokaiCbox.Items.Add("Mitsumata Nozuchi (Boss)    ");
                yokaiCbox.Items.Add("Micchy Eye Ball (Boss)    ");
                yokaiCbox.Items.Add("Micchy Eye Ball (Boss)    ");
                yokaiCbox.Items.Add("Micchy Eye Ball (Boss)    ");
                yokaiCbox.Items.Add("Micchy Eye Ball (Boss)    ");
                yokaiCbox.Items.Add("Jorogumo (Boss)    ");
                yokaiCbox.Items.Add("Jorogumo (Boss)    ");
                yokaiCbox.Items.Add("Jorogumo (Boss)    ");
                yokaiCbox.Items.Add("Jorogumo (Boss)    ");
                yokaiCbox.Items.Add("Shinmagunjin Fukurou (Boss)      ");
                yokaiCbox.Items.Add("Shinmagunjin Fukurou (Boss)      ");
                yokaiCbox.Items.Add("Shinmagunjin Fukurou (Boss)      ");
                yokaiCbox.Items.Add("Shinmagunjin Fukurou (Boss)      ");
                yokaiCbox.Items.Add("Overseer (Boss)    ");
                yokaiCbox.Items.Add("Overseer (Boss)    ");
                yokaiCbox.Items.Add("Overseer (Boss)    ");
                yokaiCbox.Items.Add("Overseer (Boss)    ");
                yokaiCbox.Items.Add("Overseer giant (Boss)    ");
                yokaiCbox.Items.Add("Overseer giant (Boss)    ");
                yokaiCbox.Items.Add("[DONT_WORK] [DONT_WORK]   ");
                yokaiCbox.Items.Add("Enma ");
                yokaiCbox.Items.Add("Overseer giant (Boss)    ");
                yokaiCbox.Items.Add("Overseer giant (Boss)    ");
                yokaiCbox.Items.Add("[DONT_WORK] [DONT_WORK]   ");
                yokaiCbox.Items.Add("Enma ");
                yokaiCbox.Items.Add("Overseer giant (Boss)    ");
                yokaiCbox.Items.Add("Overseer giant (Boss)    ");
                yokaiCbox.Items.Add("[DONT_WORK] [DONT_WORK]   ");
                yokaiCbox.Items.Add("Enma ");
                yokaiCbox.Items.Add("Overseer giant (Boss)    ");
                yokaiCbox.Items.Add("Overseer giant (Boss)    ");
                yokaiCbox.Items.Add("[DONT_WORK] [DONT_WORK]   ");
                yokaiCbox.Items.Add("Enma ");
                yokaiCbox.Items.Add("Maten Soranaki (Boss)    ");
                yokaiCbox.Items.Add("Maten Soranaki (Boss)    ");
                yokaiCbox.Items.Add("Maten Soranaki (Boss)    ");
                yokaiCbox.Items.Add("Maten Soranaki (Boss)    ");
                yokaiCbox.Items.Add("Maten Soranaki (Boss)    ");
                yokaiCbox.Items.Add("Gilgaros (Normal)  ");
                yokaiCbox.Items.Add("Illuminoct (Normal)  ");
                yokaiCbox.Items.Add("Blizzaria (Normal)  ");
                yokaiCbox.Items.Add("Sgt. Burly (Normal)  ");
                yokaiCbox.Items.Add("Damona (Normal)      ");
                yokaiCbox.Items.Add("Manjimutt (Normal)  ");
                yokaiCbox.Items.Add("Gilgaros (Normal)  ");
                yokaiCbox.Items.Add("Illuminoct (Normal)  ");
                yokaiCbox.Items.Add("Blizzaria (Normal)  ");
                yokaiCbox.Items.Add("Sgt. Burly (Normal)  ");
                yokaiCbox.Items.Add("Damona (Normal)      ");
                yokaiCbox.Items.Add("Manjimutt (Normal)  ");
                yokaiCbox.Items.Add("Gilgaros (Normal)  ");
                yokaiCbox.Items.Add("Illuminoct (Normal)  ");
                yokaiCbox.Items.Add("Blizzaria (Normal)  ");
                yokaiCbox.Items.Add("Sgt. Burly (Normal)  ");
                yokaiCbox.Items.Add("Damona (Normal)      ");
                yokaiCbox.Items.Add("Manjimutt (Normal)  ");
                yokaiCbox.Items.Add("Gilgaros (Normal)  ");
                yokaiCbox.Items.Add("Illuminoct (Normal)  ");
                yokaiCbox.Items.Add("Blizzaria (Normal)  ");
                yokaiCbox.Items.Add("Sgt. Burly (Normal)  ");
                yokaiCbox.Items.Add("Damona (Normal)      ");
                yokaiCbox.Items.Add("Manjimutt (Normal)  ");
                yokaiCbox.Items.Add("Gilgaros (Normal)  ");
                yokaiCbox.Items.Add("Illuminoct (Normal)  ");
                yokaiCbox.Items.Add("Blizzaria (Normal)  ");
                yokaiCbox.Items.Add("Sgt. Burly (Normal)  ");
                yokaiCbox.Items.Add("Damona (Normal)      ");
                yokaiCbox.Items.Add("Manjimutt (Normal)  ");
                yokaiCbox.Items.Add("Shien (Boss)    ");
                yokaiCbox.Items.Add("Shien (Boss)    ");
                yokaiCbox.Items.Add("Shien (Boss)    ");
                yokaiCbox.Items.Add("Shien (Boss)    ");
                yokaiCbox.Items.Add("Fudou Myouou   ");
                yokaiCbox.Items.Add("Fudou Myouou-kai   ");
                yokaiCbox.Items.Add("Suzaku ");
                yokaiCbox.Items.Add("Suzaku big       ");
                yokaiCbox.Items.Add("Genbu ");
                yokaiCbox.Items.Add("Genbu big       ");
                yokaiCbox.Items.Add("Byakko ");
                yokaiCbox.Items.Add("Byakko big       ");
                yokaiCbox.Items.Add("Ashura ");
                yokaiCbox.Items.Add("Yami Enma       ");
                yokaiCbox.Items.Add("Neko'ou Bastet (Normal)  ");
                yokaiCbox.Items.Add("Kappa'ou Sagojou (Normal)  ");
                yokaiCbox.Items.Add("Tengu'ou Kurama (Normal)  ");
                yokaiCbox.Items.Add("McKraken (Normal)  ");
                yokaiCbox.Items.Add("Seiryuu Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Jinta Shadow (Boss) (Boss)      ");
                yokaiCbox.Items.Add("Hi no Shin Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Lord Ananta (Normal)  ");
                yokaiCbox.Items.Add("Fuu-kun (Lightside)   ");
                yokaiCbox.Items.Add("Fuu-kun Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Rai-chan (Lightside)   ");
                yokaiCbox.Items.Add("Rai-chan Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Arachnus  (Lightside)   ");
                yokaiCbox.Items.Add("Toadal Dude (Lightside)   ");
                yokaiCbox.Items.Add("Orochi (Lightside)   ");
                yokaiCbox.Items.Add("Orochi Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Kyubi (Lightside)   ");
                yokaiCbox.Items.Add("Kyubi Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Deadcool (Normal)  ");
                yokaiCbox.Items.Add("Hovernyan (Normal)  ");
                yokaiCbox.Items.Add("Little Charrmer (Normal)  ");
                yokaiCbox.Items.Add("Micchy (Lightside)   ");
                yokaiCbox.Items.Add("Micchy Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Fudou Myouou Boy (Normal)  ");
                yokaiCbox.Items.Add("Shogunyan (Normal)  ");
                yokaiCbox.Items.Add("Komashura (Normal)  ");
                yokaiCbox.Items.Add("Gilgaros (Normal)  ");
                yokaiCbox.Items.Add("Hi no Shin Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Neko'ou Bastet (Normal)  ");
                yokaiCbox.Items.Add("Kappa'ou Sagojou (Normal)  ");
                yokaiCbox.Items.Add("Tengu'ou Kurama (Normal)  ");
                yokaiCbox.Items.Add("Lord Ananta Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Yasha Enma       ");
                yokaiCbox.Items.Add("Fukurou (Normal)  ");
                yokaiCbox.Items.Add("Shuka (Normal)      ");
                yokaiCbox.Items.Add("Gentou (Normal)      ");
                yokaiCbox.Items.Add("Hakushu (Normal)  ");
                yokaiCbox.Items.Add("Kuuten (Normal)      ");
                yokaiCbox.Items.Add("Jinta (Lightside)   ");
                yokaiCbox.Items.Add("Jinta Shadow (Boss) (Boss)      ");
                yokaiCbox.Items.Add("Kakurenbou (Lightside)   ");
                yokaiCbox.Items.Add("Kakurenbou Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Hyakki-hime (Lightside)   ");
                yokaiCbox.Items.Add("Hyakki-hime Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Daniel/Big Danny (Lightside)     ");
                yokaiCbox.Items.Add("Daniel/Big Danny Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Itashikatanshi (Lightside)   ");
                yokaiCbox.Items.Add("Itashikatanshi Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Saya (Lightside)   ");
                yokaiCbox.Items.Add("Saya/Makenki Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Rai Oton (Lightside)   ");
                yokaiCbox.Items.Add("Rai Oton Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Kage Orochi (Lightside)   ");
                yokaiCbox.Items.Add("Kage Orochi Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Bushinyan (Lightside)   ");
                yokaiCbox.Items.Add("Bushinyan Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Shurakoma (Lightside)   ");
                yokaiCbox.Items.Add("Shurakoma Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Tsuchigumo (Lightside)   ");
                yokaiCbox.Items.Add("Tsuchigumo Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Tsuchinoko (Lightside)   ");
                yokaiCbox.Items.Add("Tsuchinoko Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Ogama (Lightside)   ");
                yokaiCbox.Items.Add("Ogama Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Doyapon (Lightside)   ");
                yokaiCbox.Items.Add("Doyapon Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Inugami (Lightside)   ");
                yokaiCbox.Items.Add("Inugami Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Kezurin (Lightside)   ");
                yokaiCbox.Items.Add("Kezurin Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Robonyan 00 (Lightside)   ");
                yokaiCbox.Items.Add("Robonyan 00 Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Becchan (Lightside)   ");
                yokaiCbox.Items.Add("Becchan Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Dameboy (Lightside)   ");
                yokaiCbox.Items.Add("Dameboy Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Awevil (Normal)      ");
                yokaiCbox.Items.Add("Demuncher (Normal)  ");
                yokaiCbox.Items.Add("Slicenrice (Normal)  ");
                yokaiCbox.Items.Add("Signiton (Normal)  ");
                yokaiCbox.Items.Add("Molar Petite (Normal)  ");
                yokaiCbox.Items.Add("Shmoopie (Normal)  ");
                yokaiCbox.Items.Add("Lie-in Heart (Normal)  ");
                yokaiCbox.Items.Add("Wazzat (Normal)      ");
                yokaiCbox.Items.Add("Nekidspeed (Normal)  ");
                yokaiCbox.Items.Add("Count Zapaway (Normal)  ");
                yokaiCbox.Items.Add("B3-NK1 (Normal)      ");
                yokaiCbox.Items.Add("Rocky Badboya (Normal)  ");
                yokaiCbox.Items.Add("Smogmella (Normal)  ");
                yokaiCbox.Items.Add("Drizzelda (Normal)  ");
                yokaiCbox.Items.Add("Poofessor (Normal)  ");
                yokaiCbox.Items.Add("Ray O'Light (Normal)  ");
                yokaiCbox.Items.Add("Legsit (Normal)      ");
                yokaiCbox.Items.Add("Snottle (Normal)  ");
                yokaiCbox.Items.Add("Jibanyan B (Normal)  ");
                yokaiCbox.Items.Add("Komasan B (Normal)  ");
                yokaiCbox.Items.Add("Usapyon B (Normal)  ");
                yokaiCbox.Items.Add("Kukuri-hime (Normal)  ");
                yokaiCbox.Items.Add("Azukiarai (Normal)  ");
                yokaiCbox.Items.Add("Shien (Normal)      ");
                yokaiCbox.Items.Add("Fukurou (Normal)  ");
                yokaiCbox.Items.Add("Shuka (Normal)      ");
                yokaiCbox.Items.Add("Gentou (Normal)      ");
                yokaiCbox.Items.Add("Hakushu (Normal)  ");
                yokaiCbox.Items.Add("Kuuten (Normal)      ");
                yokaiCbox.Items.Add("Yasha Enma       ");
                yokaiCbox.Items.Add("Kenshin Amaterasu (Normal)  ");
                yokaiCbox.Items.Add("Gesshin Tsukuyomi (Normal)  ");
                yokaiCbox.Items.Add("Fudou Myouou-kai   ");
                yokaiCbox.Items.Add("Himoji (Lightside)   ");
                yokaiCbox.Items.Add("Himoji Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Pakkun (Lightside)   ");
                yokaiCbox.Items.Add("Pakkun Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Kyunshii (Lightside)   ");
                yokaiCbox.Items.Add("Kyunshii Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Hare-onna (Lightside)   ");
                yokaiCbox.Items.Add("Hare-onna Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Choky (Lightside)   ");
                yokaiCbox.Items.Add("Choky Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Fubuki-hime (Lightside)   ");
                yokaiCbox.Items.Add("Fubuki-hime Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Merameraion (Lightside)   ");
                yokaiCbox.Items.Add("Merameraion Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Orochi (Lightside)   ");
                yokaiCbox.Items.Add("Orochi Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Honmaguro-taishou (Lightside)     ");
                yokaiCbox.Items.Add("Honmaguro-taishou Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Semicolon (Lightside)   ");
                yokaiCbox.Items.Add("Semicolon Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Komasan (Lightside)   ");
                yokaiCbox.Items.Add("Komasan Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Komajiro (Lightside)   ");
                yokaiCbox.Items.Add("Komajiro Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Banchou (Lightside)   ");
                yokaiCbox.Items.Add("Banchou Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Seiryuu (Lightside)   ");
                yokaiCbox.Items.Add("Seiryuu Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Fuu-kun (Lightside)   ");
                yokaiCbox.Items.Add("Fuu-kun Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Rai-chan (Lightside)   ");
                yokaiCbox.Items.Add("Rai-chan Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Hamham (Lightside)   ");
                yokaiCbox.Items.Add("Hamham Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Jibanyan (Lightside)   ");
                yokaiCbox.Items.Add("Jibanyan Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Uribou (Lightside)   ");
                yokaiCbox.Items.Add("Uribou Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Kyubi (Lightside)   ");
                yokaiCbox.Items.Add("Kyubi Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Charlie (Lightside)   ");
                yokaiCbox.Items.Add("Charlie Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Zundoumaru (Lightside)   ");
                yokaiCbox.Items.Add("Zundoumaru Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Ungaikyo (Lightside)   ");
                yokaiCbox.Items.Add("Ungaikyo Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Jinta (Lightside)   ");
                yokaiCbox.Items.Add("Jinta Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Kantaro (Lightside)   ");
                yokaiCbox.Items.Add("Kantaro Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Kiborikkuma (Lightside)   ");
                yokaiCbox.Items.Add("Kiborikkuma Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Junior (Lightside)   ");
                yokaiCbox.Items.Add("Junior Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Micchy (Lightside)   ");
                yokaiCbox.Items.Add("Micchy Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Hyper Micchy (Lightside)   ");
                yokaiCbox.Items.Add("Hyper Micchy Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Hi no Shin (Lightside)   ");
                yokaiCbox.Items.Add("Hi no Shin Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Hungramps (Normal)  ");
                yokaiCbox.Items.Add("Dimmy (Normal)      ");
                yokaiCbox.Items.Add("Tattletell (Normal)  ");
                yokaiCbox.Items.Add("Dismarelda (Normal)  ");
                yokaiCbox.Items.Add("Hidabat (Normal)  ");
                yokaiCbox.Items.Add("Frostina (Normal)  ");
                yokaiCbox.Items.Add("Insomni (Normal)  ");
                yokaiCbox.Items.Add("Blizzaria (Normal)  ");
                yokaiCbox.Items.Add("Damona (Normal)      ");
                yokaiCbox.Items.Add("Little Charrmer (Normal)  ");
                yokaiCbox.Items.Add("Roughraff (Normal)  ");
                yokaiCbox.Items.Add("Mochismo (Normal)  ");
                yokaiCbox.Items.Add("Blazion (Normal)  ");
                yokaiCbox.Items.Add("Sgt. Burly (Normal)  ");
                yokaiCbox.Items.Add("Venoct (Normal)      ");
                yokaiCbox.Items.Add("Illuminoct (Normal)  ");
                yokaiCbox.Items.Add("Shadow Venoct (Normal)  ");
                yokaiCbox.Items.Add("Shogunyan (Normal)  ");
                yokaiCbox.Items.Add("Snartle (Normal)  ");
                yokaiCbox.Items.Add("Arachnus (Normal)  ");
                yokaiCbox.Items.Add("Komashura (Normal)  ");
                yokaiCbox.Items.Add("Noko (Normal)      ");
                yokaiCbox.Items.Add("Komasan (Normal)  ");
                yokaiCbox.Items.Add("Komajiro (Normal)  ");
                yokaiCbox.Items.Add("Happierre (Normal)  ");
                yokaiCbox.Items.Add("Hovernyan (Normal)  ");
                yokaiCbox.Items.Add("Reuknight (Normal)  ");
                yokaiCbox.Items.Add("Corptain (Normal)  ");
                yokaiCbox.Items.Add("Toadal Dude (Normal)  ");
                yokaiCbox.Items.Add("Silver Lining (Normal)  ");
                yokaiCbox.Items.Add("Manjimutt (Normal)  ");
                yokaiCbox.Items.Add("Jibanyan (Normal)  ");
                yokaiCbox.Items.Add("Krystal Fox (Normal)  ");
                yokaiCbox.Items.Add("Baku (Normal)      ");
                yokaiCbox.Items.Add("Kyubi (Normal)      ");
                yokaiCbox.Items.Add("Darkyubi (Normal)  ");
                yokaiCbox.Items.Add("Master Nyada (Normal)  ");
                yokaiCbox.Items.Add("Noway (Normal)      ");
                yokaiCbox.Items.Add("Sandmeh (Normal)  ");
                yokaiCbox.Items.Add("Mimikin (Normal)  ");
                yokaiCbox.Items.Add("Mirapo (Normal)      ");
                yokaiCbox.Items.Add("Robonyan (Normal)  ");
                yokaiCbox.Items.Add("Goldenyan (Normal)  ");
                yokaiCbox.Items.Add("Wiglin (Normal)      ");
                yokaiCbox.Items.Add("Steppa (Normal)      ");
                yokaiCbox.Items.Add("Rhyth (Normal)      ");
                yokaiCbox.Items.Add("Walkappa (Normal)  ");
                yokaiCbox.Items.Add("Nosirs (Normal)      ");
                yokaiCbox.Items.Add("Cornfused (Normal)  ");
                yokaiCbox.Items.Add("Whisper (Normal)  ");
                yokaiCbox.Items.Add("Swelton (Normal)  ");
                yokaiCbox.Items.Add("Usapyon (Normal)  ");
                yokaiCbox.Items.Add("Spoilerina (Normal)  ");
                yokaiCbox.Items.Add("Sighborg Y (Normal)  ");
                yokaiCbox.Items.Add("Wobblewok (Normal)  ");
                yokaiCbox.Items.Add("Deadcool (Normal)  ");
                yokaiCbox.Items.Add("Gargaros (Normal)  ");
                yokaiCbox.Items.Add("Ogralus (Normal)  ");
                yokaiCbox.Items.Add("Orcanos (Normal)  ");
                yokaiCbox.Items.Add("Gilgaros (Normal)  ");
                yokaiCbox.Items.Add("Shirokuma (Normal)  ");
                yokaiCbox.Items.Add("Punkupine (Normal)  ");
                yokaiCbox.Items.Add("Sorrypus (Normal)  ");
                yokaiCbox.Items.Add("Jabow (Normal)      ");
                yokaiCbox.Items.Add("Beetall (Normal)  ");
                yokaiCbox.Items.Add("Cruncha (Normal)  ");
                yokaiCbox.Items.Add("Rhinormous (Normal)  ");
                yokaiCbox.Items.Add("Hornaplenty (Normal)  ");
                yokaiCbox.Items.Add("Mad Mountain (Normal)  ");
                yokaiCbox.Items.Add("Lava Lord (Normal)  ");
                yokaiCbox.Items.Add("Faux Kappa (Normal)  ");
                yokaiCbox.Items.Add("Suu-san (Normal)  ");
                yokaiCbox.Items.Add("Yamanba (Normal)  ");
                yokaiCbox.Items.Add("Tamamo (Normal)      ");
                yokaiCbox.Items.Add("Gyuuki (Normal)      ");
                yokaiCbox.Items.Add("Narigama (Normal)  ");
                yokaiCbox.Items.Add("Blobgoblin (Normal)  ");
                yokaiCbox.Items.Add("Nekomata/Gusto (Normal)  ");
                yokaiCbox.Items.Add("Kappa (Normal)      ");
                yokaiCbox.Items.Add("Zashiki-warashi (Normal)  ");
                yokaiCbox.Items.Add("Kawauso (Normal)  ");
                yokaiCbox.Items.Add("Enma (Normal)      ");
                yokaiCbox.Items.Add("Lord Ananta (Normal)  ");
                yokaiCbox.Items.Add("Douketsu (Normal)  ");
                yokaiCbox.Items.Add("Shutendoji (Normal)  ");
                yokaiCbox.Items.Add("Ogu Togu Mogu (Normal)  ");
                yokaiCbox.Items.Add("Nurarihyon (Normal)  ");
                yokaiCbox.Items.Add("Fudou Myouou Boy (Normal)  ");
                yokaiCbox.Items.Add("Whisper (Normal)  ");
                yokaiCbox.Items.Add("Enma Awoken   ");
                yokaiCbox.Items.Add("Yami Enma       ");
                yokaiCbox.Items.Add("Neko'ou Bastet (Normal)  ");
                yokaiCbox.Items.Add("Kappa'ou Sagojou (Normal)  ");
                yokaiCbox.Items.Add("Tengu'ou Kurama (Normal)  ");
                yokaiCbox.Items.Add("Fudou Myouou Ten   ");
                yokaiCbox.Items.Add("Suzaku ");
                yokaiCbox.Items.Add("Genbu ");
                yokaiCbox.Items.Add("Byakko ");
                yokaiCbox.Items.Add("Ashura ");
                yokaiCbox.Items.Add("Kakurenbou (Lightside)   ");
                yokaiCbox.Items.Add("Kakurenbou Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Hyakki-hime (Lightside)   ");
                yokaiCbox.Items.Add("Hyakki-hime Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Daniel (Lightside)   ");
                yokaiCbox.Items.Add("Daniel Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Itashikatanshi (Lightside)   ");
                yokaiCbox.Items.Add("Itashikatanshi Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Saya (Lightside)   ");
                yokaiCbox.Items.Add("Saya Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Rai Oton (Lightside)   ");
                yokaiCbox.Items.Add("Rai Oton Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Kage Orochi (Lightside)   ");
                yokaiCbox.Items.Add("Kage Orochi Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Bushinyan (Lightside)   ");
                yokaiCbox.Items.Add("Bushinyan Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Shurakoma (Lightside)   ");
                yokaiCbox.Items.Add("Shurakoma Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Tsuchigumo (Lightside)   ");
                yokaiCbox.Items.Add("Tsuchigumo Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Tsuchinoko (Lightside)   ");
                yokaiCbox.Items.Add("Tsuchinoko Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Ogama (Lightside)   ");
                yokaiCbox.Items.Add("Ogama Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Doyapon (Lightside)   ");
                yokaiCbox.Items.Add("Doyapon Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Inugami (Lightside)   ");
                yokaiCbox.Items.Add("Inugami Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Kezurin (Lightside)   ");
                yokaiCbox.Items.Add("Kezurin Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Robonyan 00 (Lightside)   ");
                yokaiCbox.Items.Add("Robonyan 00 Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Becchan (Lightside)   ");
                yokaiCbox.Items.Add("Becchan Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Dameboy (Lightside)   ");
                yokaiCbox.Items.Add("Dameboy Shadow (Shadowside)  ");
                yokaiCbox.Items.Add("Awevil (Normal)      ");
                yokaiCbox.Items.Add("Demuncher (Normal)  ");
                yokaiCbox.Items.Add("Slicenrice (Normal)  ");
                yokaiCbox.Items.Add("Signiton (Normal)  ");
                yokaiCbox.Items.Add("Molar Petite (Normal)  ");
                yokaiCbox.Items.Add("Shmoopie (Normal)  ");
                yokaiCbox.Items.Add("Lie-in Heart (Normal)  ");
                yokaiCbox.Items.Add("Wazzat (Normal)      ");
                yokaiCbox.Items.Add("Nekidspeed (Normal)  ");
                yokaiCbox.Items.Add("Count Zapaway (Normal)  ");
                yokaiCbox.Items.Add("B3-NK1 (Normal)      ");
                yokaiCbox.Items.Add("Rocky Badboya (Normal)  ");
                yokaiCbox.Items.Add("Smogmella (Normal)  ");
                yokaiCbox.Items.Add("Drizzelda (Normal)  ");
                yokaiCbox.Items.Add("Poofessor (Normal)  ");
                yokaiCbox.Items.Add("Ray O'Light (Normal)  ");
                yokaiCbox.Items.Add("Legsit (Normal)      ");
                yokaiCbox.Items.Add("Snottle (Normal)  ");
                yokaiCbox.Items.Add("Kukuri-hime (Normal)  ");
                yokaiCbox.Items.Add("Azukiarai (Normal)  ");
                yokaiCbox.Items.Add("Fukurou (Normal)  ");
                yokaiCbox.Items.Add("Kenshin Amaterasu (Normal)  ");
                yokaiCbox.Items.Add("Gesshin Tsukuyomi (Normal)  ");
                yokaiCbox.Items.Add("Sproink (Boss)    ");
                yokaiCbox.Items.Add("Sproink (Boss)    ");
                yokaiCbox.Items.Add("Hoggles (Boss)    ");
                yokaiCbox.Items.Add("Hoggles (Boss)    ");
                yokaiCbox.Items.Add("Wobblewok (Normal)  ");
                yokaiCbox.Items.Add("Wobblewok (Normal)  ");
                yokaiCbox.Items.Add("Gargaros (Normal)  ");
                yokaiCbox.Items.Add("Gargaros (Normal)  ");
                yokaiCbox.Items.Add("Ogralus (Normal)  ");
                yokaiCbox.Items.Add("Ogralus (Normal)  ");
                yokaiCbox.Items.Add("Orcanos (Normal)  ");
                yokaiCbox.Items.Add("Orcanos (Normal)  ");
                yokaiCbox.Items.Add("McKraken (Normal)  ");
                yokaiCbox.Items.Add("McKraken (Normal)  ");
                yokaiCbox.Items.Add("Demuncher (Normal)  ");
                yokaiCbox.Items.Add("Demuncher (Normal)  ");
                yokaiCbox.Items.Add("Hi no Shin Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Hi no Shin Shadow (Shadowside)    ");
                yokaiCbox.Items.Add("Fudou Myouou-kai   ");
                yokaiCbox.Items.Add("Rocky Badboya (Normal)  ");
                yokaiCbox.Items.Add("Touma ");
                yokaiCbox.Items.Add("Summer ");
                yokaiCbox.Items.Add("Akinori       ");
                yokaiCbox.Items.Add("Jack ");
                yokaiCbox.Items.Add("Nate ");
                yokaiCbox.Items.Add("Katie ");
                yokaiCbox.Items.Add("Tae ");
                yokaiCbox.Items.Add("Smart Guy       ");
                yokaiCbox.Items.Add("Monk Guy       ");
                yokaiCbox.Items.Add("Alien ");
                yokaiCbox.Items.Add("CowBoy Guy       ");
                yokaiCbox.Items.Add("Dog Dog       ");
                yokaiCbox.Items.Add("Jinpei Jiba   ");
                yokaiCbox.Items.Add("Mitsue ");
                yokaiCbox.Items.Add("Old Guy       ");
                yokaiCbox.Items.Add("Smart Monkey      ");
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

                yokaiIdNbox.Value = new GetYokai().pickYokaiIDNumber(saveFileParams.UserYoKaiList[item.Index].YoKai_Signature ?? "");
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
                    for (int i = 11; i < 400; i++)
                    {
                        foreach (ListViewItem itemForId in yokaiListView.Items)
                        {
                            if (i == saveFileParams.UserYoKaiList[itemForId.Index].ID2)
                            {
                                idExist = true;
                            }
                        }
                        if (!idExist) {
                            saveFileParams.UserYoKaiList[item.Index].ID2 = i;
                            saveFileParams.UserYoKaiList[item.Index].YoKai_Order = i;
                            item.Selected = false;
                            item.Selected = true;
                            return; 
                        }
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
    }
}