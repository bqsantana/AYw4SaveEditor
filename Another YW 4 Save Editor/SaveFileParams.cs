namespace Another_YW_4_Save_Editor
{
    internal class SaveFileParams
    {
        public Misc misc { get; set; } = new Misc();
        public List<Consumable> ConsumableList { get; set; } = new List<Consumable>();
        public List<Consumable> GenericSoulList { get; set; } = new List<Consumable>();
        public List<Equipment> EquipmentList { get; set; } = new List<Equipment>();
        public List<YoKaiSoul> YoKaiSoulList { get; set; } = new List<YoKaiSoul>();
        public List<YoKai> UserYoKaiList { get; set; } = new List<YoKai>();
        public List<YoKai> SellingYoKaiList { get; set; } = new List<YoKai> { };
        public List<MainCharacter> MainCharacterList { get; set; } = new List<MainCharacter>();
        public int TotalItemQuantity1 { get; set; }
        public List<int> characterID2List { get; set; }


        public void mapParams(Stream str)
        {
            GetByteValue getByteValue = new GetByteValue();
            int pontualOffset = 0;

            //MISC --------------------------------------------------------------------------------/////////////////////////////////

            misc.LocalParams.PositionX = getByteValue.ExtractByteToFloat(str, 131);
            misc.LocalParams.PositionY = getByteValue.ExtractByteToFloat(str, 135);
            misc.LocalParams.PositionZ = getByteValue.ExtractByteToFloat(str, 139);
            misc.LocalParams.Map = getByteValue.ExtractByteArrayToString(str, 167, 4);

            misc.Money = getByteValue.ExtractByteToInt(str, 203, 4);

            misc.NateName = getByteValue.ExtractByteToString(str, 282, 24);
            misc.KatieName = getByteValue.ExtractByteToString(str, 318, 24);
            misc.SummerName = getByteValue.ExtractByteToString(str, 354, 24);
            misc.ToumaName = getByteValue.ExtractByteToString(str, 390, 24);
            misc.AkinoriName = getByteValue.ExtractByteToString(str, 426, 24);
            misc.JackName = getByteValue.ExtractByteToString(str, 462, 24);

            misc.MoneySpent = getByteValue.ExtractByteToInt(str, 1040, 4);

            misc.Gatcha.gatchaTries = getByteValue.ExtractByteToInt(str, 2082, 1);
            misc.Gatcha.gatchaMaxTries = getByteValue.ExtractByteToInt(str, 2083, 1);

            //FOODS/CONSUMABLE ---------------------------------------------------------------------//////////////////////////////////

            pontualOffset = 76579;

            for (int i = 0; i < 500; i++)
            {
                ConsumableList.Add(new Consumable()
                {
                    ID1 = getByteValue.ExtractByteToInt(str, pontualOffset, 2),
                    ID2 = getByteValue.ExtractByteToInt(str, pontualOffset + 2, 2),
                    ItemSignature = getByteValue.ExtractByteArrayToString(str, pontualOffset + 12, 4),
                    Order = getByteValue.ExtractByteToInt(str, pontualOffset + 24, 4),
                    Quantity = getByteValue.ExtractByteToInt(str, pontualOffset + 36, 2)
                });
                pontualOffset = pontualOffset + 54;
            }

            //EQUIPMENTS ------------------------------------------------------------------------///////////////////////////////////

            pontualOffset = 103587;

            for (int i = 0; i < 1000; i++)
            {
                EquipmentList.Add(new Equipment()
                {
                    ID1 = getByteValue.ExtractByteToInt(str, pontualOffset, 2),
                    ID2 = getByteValue.ExtractByteToInt(str, pontualOffset + 2, 2),
                    ItemSignature = getByteValue.ExtractByteArrayToString(str, pontualOffset + 12, 4),
                    Order = getByteValue.ExtractByteToInt(str, pontualOffset + 24, 4),
                    Quantity = getByteValue.ExtractByteToInt(str, pontualOffset + 36, 2),
                    Equipped = getByteValue.ExtractByteToInt(str, pontualOffset + 46, 1)
                });
                pontualOffset = pontualOffset + 63;
            }

            //GENERIC SOUL ---------------------------------------------------------------------////////////////////////////////////

            pontualOffset = 958227;

            for (int i = 0; i < 100; i++)
            {
                GenericSoulList.Add(new Consumable()
                {
                    ID1 = getByteValue.ExtractByteToInt(str, pontualOffset, 2),
                    ID2 = getByteValue.ExtractByteToInt(str, pontualOffset + 2, 2),
                    ItemSignature = getByteValue.ExtractByteArrayToString(str, pontualOffset + 12, 4),
                    Order = getByteValue.ExtractByteToInt(str, pontualOffset + 24, 4),
                    Quantity = getByteValue.ExtractByteToInt(str, pontualOffset + 36, 2)
                });
                pontualOffset = pontualOffset + 54;
            }

            //YOKAI SOUL ---------------------------------------------------------------------------------//////////////////////////////

            pontualOffset = 963635;

            for (int i = 0; i < 500; i++)
            {
                YoKaiSoulList.Add(new YoKaiSoul()
                {
                    ID1 = getByteValue.ExtractByteToInt(str, pontualOffset, 2),
                    ID2 = getByteValue.ExtractByteToInt(str, pontualOffset + 2, 2),
                    ItemSignature = getByteValue.ExtractByteArrayToString(str, pontualOffset + 12, 4),
                    Order = getByteValue.ExtractByteToInt(str, pontualOffset + 24, 4),
                    WhiteQuantity = getByteValue.ExtractByteToInt(str, pontualOffset + 36, 2),
                    RedQuantity = getByteValue.ExtractByteToInt(str, pontualOffset + 38, 2),
                    GoldQuantity = getByteValue.ExtractByteToInt(str, pontualOffset + 40, 2),
                    ItemFlag1 = getByteValue.ExtractByteToInt(str, pontualOffset + 50, 1),
                    ItemFlag2 = getByteValue.ExtractByteToInt(str, pontualOffset + 51, 1),
                    ItemFlag3 = getByteValue.ExtractByteToInt(str, pontualOffset + 52, 1),
                    ItemFlag4 = getByteValue.ExtractByteToInt(str, pontualOffset + 61, 1),
                    ItemFlag5 = getByteValue.ExtractByteToInt(str, pontualOffset + 62, 1),
                    ItemFlag6 = getByteValue.ExtractByteToInt(str, pontualOffset + 63, 1),
                });
                pontualOffset = pontualOffset + 80;
            }

            //PARTY ---------------------------------------------------------------------------------------////////////////////////////

            pontualOffset = 166627;

            for (int i = 0; i < 6; i++)
            {
                MainCharacterList.Add(new MainCharacter()
                {
                    ID1 = getByteValue.ExtractByteToInt(str, pontualOffset, 2),
                    ID2 = getByteValue.ExtractByteToInt(str, pontualOffset + 2, 2),
                    Character_Name = getByteValue.ExtractByteToString(str, pontualOffset + 28, 24),
                    Character_Signature = getByteValue.ExtractByteArrayToString(str, pontualOffset + 72, 4),
                    Character_Skill1 = getByteValue.ExtractByteArrayToString(str, pontualOffset + 84, 4),
                    Character_Skill2 = getByteValue.ExtractByteArrayToString(str, pontualOffset + 88, 4),
                    Character_Skill3 = getByteValue.ExtractByteArrayToString(str, pontualOffset + 92, 4),
                    Character_Skill4 = getByteValue.ExtractByteArrayToString(str, pontualOffset + 96, 4),
                    Character_Skill5 = getByteValue.ExtractByteArrayToString(str, pontualOffset + 100, 4),
                    Character_Skill6 = getByteValue.ExtractByteArrayToString(str, pontualOffset + 104, 4),
                    Character_XP = getByteValue.ExtractByteToInt(str, pontualOffset + 132, 4),
                    Character_HP = getByteValue.ExtractByteToInt(str, pontualOffset + 144, 4),
                    Character_YP = getByteValue.ExtractByteToInt(str, pontualOffset + 156, 4),
                    Character_PG = getByteValue.ExtractByteToInt(str, pontualOffset + 168, 4),
                    Character_Level = getByteValue.ExtractByteToInt(str, pontualOffset + 180, 4),
                    Character_Flag1 = getByteValue.ExtractByteToInt(str, pontualOffset + 204, 2),
                    Character_HPplus = getByteValue.ExtractByteToInt(str, pontualOffset + 214, 2),
                    Character_YPplus = getByteValue.ExtractByteToInt(str, pontualOffset + 216, 2),
                    Character_STplus = getByteValue.ExtractByteToInt(str, pontualOffset + 218, 2),
                    Character_SPplus = getByteValue.ExtractByteToInt(str, pontualOffset + 220, 2),
                    Character_PAplus = getByteValue.ExtractByteToInt(str, pontualOffset + 222, 2),
                    Character_SAplus = getByteValue.ExtractByteToInt(str, pontualOffset + 224, 2),
                    Character_Unknown1 = getByteValue.ExtractByteToInt(str, pontualOffset + 254, 1),
                    Character_Unknown2 = getByteValue.ExtractByteToInt(str, pontualOffset + 255, 1),
                    Character_Unknown3 = getByteValue.ExtractByteToInt(str, pontualOffset + 256, 1),
                    Character_Unknown4 = getByteValue.ExtractByteToInt(str, pontualOffset + 257, 1),
                    Character_Unknown5 = getByteValue.ExtractByteToInt(str, pontualOffset + 258, 1),
                    Character_Unknown6 = getByteValue.ExtractByteToInt(str, pontualOffset + 259, 1),
                    Character_Unknown7 = getByteValue.ExtractByteToInt(str, pontualOffset + 268, 4),
                    Character_Unknown8 = getByteValue.ExtractByteToInt(str, pontualOffset + 292, 4),
                    Character_Unknown9 = getByteValue.ExtractByteToInt(str, pontualOffset + 317, 2),
                    Character_Unknown10 = getByteValue.ExtractByteToInt(str, pontualOffset + 330, 2),
                    Character_Order = getByteValue.ExtractByteToInt(str, pontualOffset + 344, 4),
                    Character_Unknown11 = getByteValue.ExtractByteToInt(str, pontualOffset + 356, 4),
                    Character_Unknown12 = getByteValue.ExtractByteToInt(str, pontualOffset + 380, 4),
                    Character_Unknown13 = getByteValue.ExtractByteToInt(str, pontualOffset + 389, 1),
                    Character_Unknown14 = getByteValue.ExtractByteToInt(str, pontualOffset + 398, 1),
                    Character_Unknown15 = getByteValue.ExtractByteToInt(str, pontualOffset + 416, 1),
                    Character_Unknown16 = getByteValue.ExtractByteToInt(str, pontualOffset + 425, 1),
                    Character_Unknown17 = getByteValue.ExtractByteToInt(str, pontualOffset + 452, 1)
                });
                pontualOffset = pontualOffset + 469;
            }

            //USER YOKAI ---------------------------------------------------------------------------------------////////////////////////////

            pontualOffset = 169449;

            for (int i = 0; i < 400; i++)
            {
                UserYoKaiList.Add(new YoKai()
                {
                    ID1 = getByteValue.ExtractByteToInt(str, pontualOffset, 2),
                    ID2 = getByteValue.ExtractByteToInt(str, pontualOffset + 2, 2),
                    YoKai_Name = getByteValue.ExtractByteToString(str, pontualOffset + 28, 24),
                    YoKai_Signature = getByteValue.ExtractByteArrayToString(str, pontualOffset + 72, 4),
                    YoKai_Skill1 = getByteValue.ExtractByteArrayToString(str, pontualOffset + 84, 4),
                    YoKai_Skill2 = getByteValue.ExtractByteArrayToString(str, pontualOffset + 88, 4),
                    YoKai_Skill3 = getByteValue.ExtractByteArrayToString(str, pontualOffset + 92, 4),
                    YoKai_Skill4 = getByteValue.ExtractByteArrayToString(str, pontualOffset + 96, 4),
                    YoKai_Skill5 = getByteValue.ExtractByteArrayToString(str, pontualOffset + 100, 4),
                    YoKai_Skill6 = getByteValue.ExtractByteArrayToString(str, pontualOffset + 104, 4),
                    YoKai_XP = getByteValue.ExtractByteToInt(str, pontualOffset + 132, 4),
                    YoKai_HP = getByteValue.ExtractByteToInt(str, pontualOffset + 144, 4),
                    YoKai_YP = getByteValue.ExtractByteToInt(str, pontualOffset + 156, 4),
                    YoKai_PG = getByteValue.ExtractByteToInt(str, pontualOffset + 168, 4),
                    YoKai_Level = getByteValue.ExtractByteToInt(str, pontualOffset + 180, 4),
                    YoKai_Flag1 = getByteValue.ExtractByteToInt(str, pontualOffset + 204, 2),
                    YoKai_HPplus = getByteValue.ExtractByteToInt(str, pontualOffset + 214, 2),
                    YoKai_YPplus = getByteValue.ExtractByteToInt(str, pontualOffset + 216, 2),
                    YoKai_STplus = getByteValue.ExtractByteToInt(str, pontualOffset + 218, 2),
                    YoKai_SPplus = getByteValue.ExtractByteToInt(str, pontualOffset + 220, 2),
                    YoKai_PAplus = getByteValue.ExtractByteToInt(str, pontualOffset + 222, 2),
                    YoKai_SAplus = getByteValue.ExtractByteToInt(str, pontualOffset + 224, 2),
                    YoKai_Unknown1 = getByteValue.ExtractByteToInt(str, pontualOffset + 254, 1),
                    YoKai_Unknown2 = getByteValue.ExtractByteToInt(str, pontualOffset + 255, 1),
                    YoKai_Unknown3 = getByteValue.ExtractByteToInt(str, pontualOffset + 256, 1),
                    YoKai_Unknown4 = getByteValue.ExtractByteToInt(str, pontualOffset + 257, 1),
                    YoKai_Unknown5 = getByteValue.ExtractByteToInt(str, pontualOffset + 258, 1),
                    YoKai_Unknown6 = getByteValue.ExtractByteToInt(str, pontualOffset + 259, 1),
                    YoKai_Unknown7 = getByteValue.ExtractByteToInt(str, pontualOffset + 268, 4),
                    YoKai_Unknown8 = getByteValue.ExtractByteToInt(str, pontualOffset + 292, 4),
                    YoKai_Unknown9 = getByteValue.ExtractByteToInt(str, pontualOffset + 317, 2),
                    YoKai_Unknown10 = getByteValue.ExtractByteToInt(str, pontualOffset + 330, 2),
                    YoKai_Order = getByteValue.ExtractByteToInt(str, pontualOffset + 344, 4),
                    YoKai_Unknown11 = getByteValue.ExtractByteToInt(str, pontualOffset + 356, 4),
                    YoKai_Unknown12 = getByteValue.ExtractByteToInt(str, pontualOffset + 380, 1),
                    YoKai_Unknown13 = getByteValue.ExtractByteToInt(str, pontualOffset + 389, 1),
                    YoKai_Unknown14 = getByteValue.ExtractByteToInt(str, pontualOffset + 398, 1),
                    YoKai_Unknown15 = getByteValue.ExtractByteToInt(str, pontualOffset + 416, 1),
                    YoKai_Unknown16 = getByteValue.ExtractByteToInt(str, pontualOffset + 425, 1),
                    YoKai_Unknown17 = getByteValue.ExtractByteToInt(str, pontualOffset + 452, 1)
                });
                pontualOffset = pontualOffset + 469;
            }

            characterID2List = new List<int>();

            foreach (MainCharacter character in MainCharacterList)
            {
                if (character.ID2 > 0)
                {
                    characterID2List.Add(character.ID2);
                }
            }

            foreach (YoKai yokai in UserYoKaiList)
            {
                if (yokai.ID2 > 0)
                {
                    characterID2List.Add(yokai.ID2);
                }
            }

            characterID2List.Sort();
        }

        public MemoryStream injectParams(MemoryStream str)
        {
            SetByteValue setByteValue = new SetByteValue();
            int pontualOffset = 0;



            //MISC --------------------------------------------------------------------------------/////////////////////////////////

            setByteValue.InjectByteFromFloat(str, misc.LocalParams.PositionX, 131);
            setByteValue.InjectByteFromFloat(str, misc.LocalParams.PositionY, 135);
            setByteValue.InjectByteFromFloat(str, misc.LocalParams.PositionZ, 139);
            setByteValue.InjectByteFromByteString(str, misc.LocalParams.Map, 167);

            setByteValue.InjectByteFromInt(str, misc.Money, 203, 4);

            setByteValue.InjectByteFromString(str, misc.NateName ?? "", 282, 24);
            setByteValue.InjectByteFromString(str, misc.KatieName ?? "", 318, 24);
            setByteValue.InjectByteFromString(str, misc.SummerName ?? "", 354, 24);
            setByteValue.InjectByteFromString(str, misc.ToumaName ?? "", 390, 24);
            setByteValue.InjectByteFromString(str, misc.AkinoriName ?? "", 426, 24);
            setByteValue.InjectByteFromString(str, misc.JackName ?? "", 462, 24);

            setByteValue.InjectByteFromInt(str, misc.Gatcha.gatchaTries, 2082, 1);
            setByteValue.InjectByteFromInt(str, misc.Gatcha.gatchaMaxTries, 2083, 1);

            // CONSUMABLE/FOOD ITEMS ---------------------------------------------------------/////////////////////////////////////

            pontualOffset = 76579;

            foreach (Consumable food in ConsumableList)
            {
                setByteValue.InjectByteFromInt(str, food.ID1, pontualOffset, 2);
                setByteValue.InjectByteFromInt(str, food.ID2, pontualOffset + 2, 2);
                setByteValue.InjectByteFromByteString(str, food.ItemSignature ?? "00-00-00-00", pontualOffset + 12);
                setByteValue.InjectByteFromInt(str, food.Order, pontualOffset + 24, 4);
                setByteValue.InjectByteFromInt(str, food.Quantity, pontualOffset + 36, 2);
                pontualOffset = pontualOffset + 54;
            }

            setByteValue.InjectByteFromInt(str, ConsumableList.Where(food => food.ID2 > 0).Count(), 166587, 2);


            // USER YOKAI ---------------------------------------------------------------------------------------////////////////////////////

            pontualOffset = 169449;

            foreach (YoKai yokai in UserYoKaiList)
            {
                setByteValue.InjectByteFromInt(str, yokai.ID1, pontualOffset, 2);
                setByteValue.InjectByteFromInt(str, yokai.ID2, pontualOffset + 2, 2);
                setByteValue.InjectByteFromString(str, yokai.YoKai_Name, pontualOffset + 28, 24);
                setByteValue.InjectByteFromByteString(str, yokai.YoKai_Signature, pontualOffset + 72);
                setByteValue.InjectByteFromByteString(str, yokai.YoKai_Skill1, pontualOffset + 84);
                setByteValue.InjectByteFromByteString(str, yokai.YoKai_Skill2, pontualOffset + 88);
                setByteValue.InjectByteFromByteString(str, yokai.YoKai_Skill3, pontualOffset + 92);
                setByteValue.InjectByteFromByteString(str, yokai.YoKai_Skill4, pontualOffset + 96);
                setByteValue.InjectByteFromByteString(str, yokai.YoKai_Skill5, pontualOffset + 100);
                setByteValue.InjectByteFromByteString(str, yokai.YoKai_Skill6, pontualOffset + 104);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_XP, pontualOffset + 132, 4);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_HP, pontualOffset + 144, 4);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_YP, pontualOffset + 156, 4);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_PG, pontualOffset + 168, 4);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_Level, pontualOffset + 180, 4);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_Flag1, pontualOffset + 204, 2);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_HPplus, pontualOffset + 214, 2);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_YPplus, pontualOffset + 216, 2);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_STplus, pontualOffset + 218, 2);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_SPplus, pontualOffset + 220, 2);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_PAplus, pontualOffset + 222, 2);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_SAplus, pontualOffset + 224, 2);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_Unknown1, pontualOffset + 254, 1);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_Unknown2, pontualOffset + 255, 1);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_Unknown3, pontualOffset + 256, 1);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_Unknown4, pontualOffset + 257, 1);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_Unknown5, pontualOffset + 258, 1);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_Unknown6, pontualOffset + 259, 1);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_Unknown7, pontualOffset + 268, 1);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_Unknown8, pontualOffset + 292, 1);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_Unknown9, pontualOffset + 317, 1);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_Unknown10, pontualOffset + 330, 1);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_Order, pontualOffset + 330, 1);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_Unknown11, pontualOffset + 356, 1);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_Unknown12, pontualOffset + 380, 1);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_Unknown13, pontualOffset + 389, 1);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_Unknown14, pontualOffset + 398, 1);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_Unknown15, pontualOffset + 416, 1);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_Unknown16, pontualOffset + 425, 1);
                setByteValue.InjectByteFromInt(str, yokai.YoKai_Unknown17, pontualOffset + 452, 1);

                pontualOffset = pontualOffset + 469;
            }

            pontualOffset = 944897;

            foreach (YoKai yokai in UserYoKaiList)
            {
                setByteValue.InjectByteFromInt(str, yokai.ID1, pontualOffset, 2);
                setByteValue.InjectByteFromInt(str, yokai.ID2, pontualOffset + 2, 2);

                pontualOffset = pontualOffset + 4;
            }

            setByteValue.InjectByteFromInt(str, UserYoKaiList.Where(yokai => yokai.ID2 > 0).Count(), 946497, 4);

            // MAIN CHARACTER ---------------------------------------------------------------------------------////////////////////////////

            pontualOffset = 166627;

            foreach (MainCharacter character in MainCharacterList)
            {
                setByteValue.InjectByteFromInt(str, character.ID1, pontualOffset, 2);
                setByteValue.InjectByteFromInt(str, character.ID2, pontualOffset + 2, 2);
                setByteValue.InjectByteFromString(str, character.Character_Name, pontualOffset + 28, 24);
                setByteValue.InjectByteFromByteString(str, character.Character_Signature, pontualOffset + 72);
                setByteValue.InjectByteFromByteString(str, character.Character_Skill1, pontualOffset + 84);
                setByteValue.InjectByteFromByteString(str, character.Character_Skill2, pontualOffset + 88);
                setByteValue.InjectByteFromByteString(str, character.Character_Skill3, pontualOffset + 92);
                setByteValue.InjectByteFromByteString(str, character.Character_Skill4, pontualOffset + 96);
                setByteValue.InjectByteFromByteString(str, character.Character_Skill5, pontualOffset + 100);
                setByteValue.InjectByteFromByteString(str, character.Character_Skill6, pontualOffset + 104);
                setByteValue.InjectByteFromInt(str, character.Character_XP, pontualOffset + 132, 4);
                setByteValue.InjectByteFromInt(str, character.Character_HP, pontualOffset + 144, 4);
                setByteValue.InjectByteFromInt(str, character.Character_YP, pontualOffset + 156, 4);
                setByteValue.InjectByteFromInt(str, character.Character_PG, pontualOffset + 168, 4);
                setByteValue.InjectByteFromInt(str, character.Character_Level, pontualOffset + 180, 4);
                setByteValue.InjectByteFromInt(str, character.Character_Flag1, pontualOffset + 204, 2);
                setByteValue.InjectByteFromInt(str, character.Character_HPplus, pontualOffset + 214, 2);
                setByteValue.InjectByteFromInt(str, character.Character_YPplus, pontualOffset + 216, 2);
                setByteValue.InjectByteFromInt(str, character.Character_STplus, pontualOffset + 218, 2);
                setByteValue.InjectByteFromInt(str, character.Character_SPplus, pontualOffset + 220, 2);
                setByteValue.InjectByteFromInt(str, character.Character_PAplus, pontualOffset + 222, 2);
                setByteValue.InjectByteFromInt(str, character.Character_SAplus, pontualOffset + 224, 2);
                setByteValue.InjectByteFromInt(str, character.Character_Unknown1, pontualOffset + 254, 1);
                setByteValue.InjectByteFromInt(str, character.Character_Unknown2, pontualOffset + 255, 1);
                setByteValue.InjectByteFromInt(str, character.Character_Unknown3, pontualOffset + 256, 1);
                setByteValue.InjectByteFromInt(str, character.Character_Unknown4, pontualOffset + 257, 1);
                setByteValue.InjectByteFromInt(str, character.Character_Unknown5, pontualOffset + 258, 1);
                setByteValue.InjectByteFromInt(str, character.Character_Unknown6, pontualOffset + 259, 1);
                setByteValue.InjectByteFromInt(str, character.Character_Unknown7, pontualOffset + 268, 1);
                setByteValue.InjectByteFromInt(str, character.Character_Unknown8, pontualOffset + 292, 1);
                setByteValue.InjectByteFromInt(str, character.Character_Unknown9, pontualOffset + 317, 1);
                setByteValue.InjectByteFromInt(str, character.Character_Unknown10, pontualOffset + 330, 1);
                setByteValue.InjectByteFromInt(str, character.Character_Order, pontualOffset + 330, 1);
                setByteValue.InjectByteFromInt(str, character.Character_Unknown11, pontualOffset + 356, 1);
                setByteValue.InjectByteFromInt(str, character.Character_Unknown12, pontualOffset + 380, 1);
                setByteValue.InjectByteFromInt(str, character.Character_Unknown13, pontualOffset + 389, 1);
                setByteValue.InjectByteFromInt(str, character.Character_Unknown14, pontualOffset + 398, 1);
                setByteValue.InjectByteFromInt(str, character.Character_Unknown15, pontualOffset + 416, 1);
                setByteValue.InjectByteFromInt(str, character.Character_Unknown16, pontualOffset + 425, 1);
                setByteValue.InjectByteFromInt(str, character.Character_Unknown17, pontualOffset + 452, 1);

                pontualOffset = pontualOffset + 469;
            }

            pontualOffset = 946513;

            foreach (MainCharacter character in MainCharacterList)
            {
                setByteValue.InjectByteFromInt(str, character.ID1, pontualOffset, 2);
                setByteValue.InjectByteFromInt(str, character.ID2, pontualOffset + 2, 2);

                pontualOffset = pontualOffset + 4;
            }

            setByteValue.InjectByteFromInt(str, MainCharacterList.Where(character => character.ID2 > 0).Count(), 946537, 4);

            return str;
        }
    }
}
