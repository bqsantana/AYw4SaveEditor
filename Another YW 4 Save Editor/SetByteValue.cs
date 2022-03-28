using System.Globalization;
using System.Text;

namespace Another_YW_4_Save_Editor
{
    internal class SetByteValue
    {
        byte[] byte1 = new byte[1];
        byte[] byte2 = new byte[2];
        byte[] byte4 = new byte[4];
        public MemoryStream InjectByteFromInt(MemoryStream str, int value, int initialOffset, int count)
        {
            switch (count)
            {
                case 1:
                    byte1 = BitConverter.GetBytes(value);
                    //Array.Reverse(byte1);
                    str.Seek(initialOffset, SeekOrigin.Begin);
                    str.Write(byte1, 0, 1);
                    return str;
                case 2:
                    byte2 = BitConverter.GetBytes(value);
                    //Array.Reverse(byte2);
                    str.Seek(initialOffset, SeekOrigin.Begin);
                    str.Write(byte2, 0, 2);
                    return str;
                case 4:
                    byte4 = BitConverter.GetBytes(value);
                    //Array.Reverse(byte2);
                    str.Seek(initialOffset, SeekOrigin.Begin);
                    str.Write(byte4, 0, 4);
                    return str;
                default:
                    return str;
            }
        }

        public MemoryStream InjectByteFromFloat(MemoryStream str, float value, int initialOffset)
        {
            byte4 = BitConverter.GetBytes(value);
            //Array.Reverse(byte2);
            str.Seek(initialOffset, SeekOrigin.Begin);
            str.Write(byte4, 0, 4);
            return str;
        }

        public MemoryStream InjectByteFromString(MemoryStream str, string value, int initialOffset, int count)
        {
            byte[] byteString = new byte[count];
            byteString = Encoding.UTF8.GetBytes(value);
            while(byteString.Length < count) {
                byte[] newByte = new byte[byteString.Length + (count - byteString.Length)];
                byteString.CopyTo(newByte, 0);
                byteString = newByte;
            }
            //Array.Reverse(byte1);
            str.Seek(initialOffset, SeekOrigin.Begin);
            str.Write(byteString, 0, count);
            return str;
        }

        public MemoryStream InjectByteFromByteString(MemoryStream str, string value, int initialOffset)
        {
            byte[] byteString = value.Split('-')
                   .Select(t => byte.Parse(t, NumberStyles.AllowHexSpecifier))
                   .ToArray();
            //Array.Reverse(byte1);
            str.Seek(initialOffset, SeekOrigin.Begin);
            str.Write(byteString, 0, byteString.Count());
            return str;
        }

        public byte[] ExtractByteArray(Stream str, int initialOffset, int count)
        {
            switch (count)
            {
                case 1:
                    str.Seek(initialOffset, SeekOrigin.Begin);
                    str.Read(byte1, 0, 1);
                    return byte1;
                case 2:
                    str.Seek(initialOffset, SeekOrigin.Begin);
                    str.Read(byte2, 0, 2);
                    return byte2;
                case 4:
                    str.Seek(initialOffset, SeekOrigin.Begin);
                    str.Read(byte4, 0, 4);
                    return byte4;
                default:
                    return new byte[0x00];
            }
        }
    }
}
