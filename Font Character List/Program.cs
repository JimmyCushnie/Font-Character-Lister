using System;
using System.Windows.Media;
using System.Windows;

namespace Font_Character_List
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            start:

            Console.WriteLine("enter path of font");
            string path = Console.ReadLine();

            GlyphTypeface font;
            try
            {
                font = new GlyphTypeface(new Uri(path));
            }
            catch
            {
                Console.WriteLine("there's no font there you IDIOT");
                goto start;
            }

            var charactersList = font.CharacterToGlyphMap.Keys;
            string charactersString = "";
            foreach(var characterInt in charactersList)
            {
                if (characterInt == 0) continue;
                charactersString += (char)characterInt;
            }

            Clipboard.SetText(charactersString, TextDataFormat.UnicodeText);
            Console.WriteLine("All font characters copied to cliboard");

            goto start;
        }
    }
}