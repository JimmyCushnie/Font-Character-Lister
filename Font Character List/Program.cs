using System;
using System.Windows.Media;
using System.Windows;

namespace Font_Character_List
{
    class Program
    {
        static string MostRecentSuccessfulPath;

        [STAThread]
        static void Main(string[] args)
        {
            Console.Title = "Font Character Lister";
            WriteLineColor("Welcome to Font Character Lister. You can use this tool to get a list of all characters contained in a font file.");
            start:

            string opening = "enter path of font to analyze";
            if (MostRecentSuccessfulPath != null) opening += ", 'recopy' to recopy the previously analyzed font,";
            opening += " or 'exit' to close the program";
            WriteLineColor(opening);

            string text = Console.ReadLine();
            if (text.ToLower() == "recopy")
                CopyAtPath(MostRecentSuccessfulPath);
            else if (text.ToLower() == "exit")
                Environment.Exit(0);
            else
                CopyAtPath(text.Replace("\"", ""));

            Console.WriteLine();
            goto start;
        }

        static void CopyAtPath(string path)
        {
            WriteLineColor("analyzing font...");

            GlyphTypeface font;
            try
            {
                font = new GlyphTypeface(new Uri(path));
                MostRecentSuccessfulPath = path;
            }

            catch (UriFormatException)
            {
                WriteLineColor("error: that's not a path", ConsoleColor.Red);
                return;
            }
            catch(System.IO.FileNotFoundException)
            {
                WriteLineColor("error: no file found at path", ConsoleColor.Red);
                return;
            }
            catch (System.IO.FileFormatException)
            {
                WriteLineColor("error: file is not a font. Make sure the extension is .ttf or .otf", ConsoleColor.Red);
                return;
            }
            catch
            {
                WriteLineColor("error: unkown error", ConsoleColor.Red);
                return;
            }
            

            var charactersList = font.CharacterToGlyphMap.Keys;
            string charactersString = "";
            foreach (var characterInt in charactersList)
            {
                if (characterInt == 0) continue;
                charactersString += (char)characterInt;
            }

            Clipboard.SetText(charactersString, TextDataFormat.UnicodeText);

            WriteLineColor("done. All font characters copied to clipboard", ConsoleColor.Green);
        }

        static void WriteLineColor(string text, ConsoleColor color = ConsoleColor.Cyan)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}