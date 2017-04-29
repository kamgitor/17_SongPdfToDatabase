
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Win32;          // OpenFileDialog
using System.IO;                // Directory.GetCurrentDirectory()


// gryzie sie z Microsoft.Win32
// using System.Windows.Forms;		// Folder browser		Trzeba dodac referencja do windowsowego DLL


/*
	V2	- Poprawiona sciezka otwarcia w funkcji GetFileName



 * Do zrobienia:
 * ok - do biblioteki funkcja podawania sciezki do katalogu
 * - do bibilotego funkcja MassageBox
 * ok - try brak pliku excel
 * ok - try sciezka nie istnieje, nie mozna zapisac do pliku, np jest otwarty przez cos - oba zrobilem na jednym zdazeniu
 * - ewentualnie rozdzielic powyzsze bledy
 * - konwersja na nowym watku
 * 
 */




namespace SongPdfToDatabase
{
	class KamLib
	{
		// ***********************************************************************************************
		// Odpala okno - wskaz z dysku nazwe pliku - plik z sciezka
		// path - sciezka odpalenia okna
		// zwraca nazwe pliku z sciezka
		public static string GetFileName(string path)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			// string path = name;

			// openFileDialog.FileName = "K:\\ProjektyC#\\17_LangKamConverter\\LangKamConverter\\bin\\debug\\oko.txt";
			if ((path == "") || (path == null))
			{
				path = Directory.GetCurrentDirectory();
				// path += "\\filename";				// ustawianie sciezki startowej - musi byc wybrana nazwa pliku
			}
			
			// openFileDialog.FileName = path;
			openFileDialog.InitialDirectory = path;

			if (openFileDialog.ShowDialog() == true)
				return openFileDialog.FileName;
			else
				return path;

		}	// GetFileName


		// ***********************************************************************************************
		// Odpala okno - pokaz katalog na dysku
		// path - sciezka poprzednio wskazana
		// zwraca wskazana sciezke
		public static string GetDirectoryPath(string path)
		{
			// FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			string path_temp = path;

			if ((path == "") || (path == null))
			{
				path_temp = Directory.GetCurrentDirectory();
			}

			folderBrowserDialog.SelectedPath = path_temp;

			if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				return folderBrowserDialog.SelectedPath;
			else
				return path;

		}	// GetDirectoryPath

	}
}
