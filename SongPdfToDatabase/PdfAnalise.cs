
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using System.Data.SQLite;


// pdf
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;


namespace SongPdfToDatabase
{

	public partial class MainWindow : Window
	{
		bool display = false;           // debug
		bool end = false;
		int strona_startowa = 22;
		byte mode = 0;                      // tryb szukania
		short ilosc_songow = 0;
		string temp_line;
		int mode_offset;
		short act_song_number = 0;

		// pola bazy danych
		string numer1;
		string numer2;

		string tytul;
		string tekst;
		string powtorki;        // wyswietlana osobna kolumna tabelki z powtorzeniami tzn: / / / / / 2x
		string akordy;


		/*
		Narazie NU

		string numer3;          // future
		string numer4;          // future
		
		
		string akordy_alt;
		string tonacje;			// 
		string goto;			// songi do skoczenia goto - np wersja angielska
		string melody;			// melodia poczatkowa na dzwiekach
		string grupe;			// np Boze, krakowskie, szanty, poganskie, itp
		string author;
		string markers;			// gwiazdka, pytek itp
		*/

		// analiza pliku pdf
		public void PdfAnalise(string path)
		{

			using (PdfReader reader = new PdfReader(path))
			{
				// StringBuilder text = new StringBuilder();

				Database.Create();
				Database.ConnectTo();
				Database.CreateTable();

				// Przelot po stronach
				for (int page = 1; page <= reader.NumberOfPages; /*page++*/)
				{
					string temp_str = "";
					string temp_str2 = "";
					int temp_str_len, temp_str2_len;

					/*
					temp_str_len = temp_str.Length;
					temp_str2 = PdfTextExtractor.GetTextFromPage(reader, page + 1);
					*/

					for (; page < strona_startowa;)
						page++;

					temp_str = PdfTextExtractor.GetTextFromPage(reader, page++);
					temp_str_len = temp_str.Length;
					temp_str2 = PdfTextExtractor.GetTextFromPage(reader, page++);
					temp_str2_len = temp_str2.Length;
					temp_str += temp_str2;


					// Przelot po znakach
					for (int i = 0; i < temp_str.Length; )
					{
						if (i > temp_str_len)		// pierwszy string sie skonczyl
						{
							i -= temp_str_len;
							temp_str = temp_str2;
							temp_str_len = temp_str2_len;

							if (page < reader.NumberOfPages)
								temp_str2 = PdfTextExtractor.GetTextFromPage(reader, page++);
							else
								temp_str2 = "                                                                                                   ";

							temp_str2_len = temp_str2.Length;

							temp_str += temp_str2;
						}

						/*
						// Sprawdzam czy nie brakuje znakow
						if (i + 100 > temp_str.Length)
						{
							int prev_len = temp_str.Length;
							++page;
						}
						*/


						switch (mode)
						{
						case 0:         // Numer1			TESTED
							if (GetSongNumber1(i, temp_str, out i, out numer1))
								return;
							++mode;
							break;

						case 1:         // Numer 2			TESTED
							if (GetSongNumber2(i, temp_str, out i, out numer2))
								return;
							++mode;
							break;

						case 2:         // Nazwa			TESTED
							if (GetSongName(i, temp_str, out i, out tytul))
								return;

							tekst = "";
							powtorki = "";
							akordy = "";
							++mode;
							break;

						case 3:         // Linijka tekstu z tekstem songa
						/*	if (GetSongText(i, temp_str, out i, out tekst))
								return;*/

							if (GetSongTextLine(i, temp_str, out i, out temp_line, out mode_offset))
								return;

							if (temp_line == "304                                                                                                  \n")
							{
								end = true;
								mode = 10;
								break;
							}

							tekst += temp_line;

							switch (mode_offset)
							{
							case 0:
								++mode;
								break;
							case 1:						// pomijamy powtorki
								powtorki += "\n";
								mode += 2;
								break;
							case 2:						// pomijamy powrotki i akordy
								powtorki += "\n";
								akordy += "\n";
								mode = 8;
								break;
							case 3:                     // koniec kawalka
								mode = 10;
								break;
							}
							break;

						case 4:         // Dalsza czesc linijki z polem Repeat

							if (GetSongRepeatLine(i, temp_str, out i, out temp_line, out mode_offset))
								return;

							powtorki += temp_line;

							switch (mode_offset)
							{
							case 0:
								++mode;
								break;
							case 1:
								powtorki += "\n";
								akordy += "\n";
								mode = 8;
								break;
							}

							break;

						case 5:         // Dalsza czesc linijki z Akordami
							if (GetSongChordLine(i, temp_str, out i, out temp_line))
								return;

							akordy += temp_line;
							mode = 8;
							break;

						case 8:         // sprawdzam czy to nie jest koniec kawalka
							if (DelSpecialChars(i, temp_str, out i))
								return;

							if (IsThisStartOfSong(i, temp_str))
								mode = 10;
							else
								mode = 3;
							break;

						case 10:        // Koniec kawalka

							// to dziala
							/*List<string> param = new List<string>();
							param.Add("par1");
							param.Add("par2");
							*/
							// SongView podglad = new SongView();

							// Op 2
							// SongViewWF podglad = new SongViewWF(numer1, numer2, tytul, tekst, powtorki, akordy);
							// podglad.ShowDialog();


							// System.Windows.Forms.ListView fast_SongView = new System.Windows.Forms.ListView();
							// fast_SongView.Show();

							// SongViewWF.
							// SongBook.
							// SongView_WF podglda;
							// SongView

							// SongViewWF.SongView_WF.Show();

							// System.Windows.Forms.ListView SongView1 = new System.Windows.Forms.ListView();
							// SongView1.ShowDialog();


							// DEBUG
							// if (true)


							// DEBUG
							// do breakowania na danym kawalku
							if (numer1 == InNumber.Text)
							{
								display = true;
							}


							if (display)
							{
								// Op3
								SimpleSongView podglad = new SimpleSongView(numer1, numer2, tytul, tekst, powtorki, akordy);
								podglad.ShowDialog();
							}


							++ilosc_songow;
							
							// TODO dodanie kawalka do bazy
							mode = 0;
							Database.CreateSong(numer1, numer2, "", tytul, tekst, powtorki, akordy, "Śpiewnik", "", "", "", "", "", "", "");

							// if (numer1 == "609")		// ostatni kawalek		Olewam te najnowsze, wprowadzi sie to recznie
							if (end)
							{
								page = reader.NumberOfPages + 2;        // takie sztuczne zakonczenie petli
								i = temp_str.Length + 2;
							}

							break;
						}
					}


					// text.Append(temp_str);
				}

			}

		}   // PdfAnalise


		// ************************************************************************
		// Po znaku konca wiersza moga byc rozne krzaki, no ciag \u0018 lub numer strony - pomijamy
		public bool DelSpecialChars(int offset, string work_str, out int ret_offset)
		{
			int i;
			char znak;

			ret_offset = offset;

			for (i = offset; i < 10000; i++)
			{
				znak = work_str[i];

				if (work_str[i + 4] == '\u0018')
				{
					ret_offset += 5;
					i += 4;
				}
				else if (work_str[i + 3] == '\u0018')
				{
					ret_offset += 4;
					i += 3;
				}
				else if (work_str[i + 2] == '\u0018')
				{
					ret_offset += 3;
					i += 2;
				}
				else if (work_str[i + 1] == '\u0018')
				{
					ret_offset += 2;
					i += 1;
				}
				else if (znak == '\u0018')
					ret_offset++;
				else
					return false;
			}

			return false;

		}   // DelSpecialChars


		// ************************************************************************
		// Wyodrebnia linie tekstu piosenki
		public bool GetSongTextLine(int offset, string work_str, out int ret_offset, out string text_line, out int mode_offset)
		{
			int i;
			text_line = "";
			char znak;
			ret_offset = offset;
			mode_offset = 0;

			for (i = offset; i < 10000; i++)
			{
				znak = work_str[i];
				if (znak == '\n')
				{
					text_line += '\n';
					ret_offset = i + 1;
					mode_offset = 2;
					if (text_line == "")
						return true;        // error	
					else
						return false;
				}
				else if (znak == '/')
				{


					text_line += '\n';
					mode_offset = 0;
					ret_offset = i;

					if (text_line == "")
						return true;        // error	
					else
						return false;
				}
				else if (znak == ' ')
				{
					try
					{
						if ((work_str[i + 1] != '/') && (IsThisChordsField(i + 1, work_str) == false))
							text_line += znak;
					}
					catch
					{
						text_line += '\n';
						mode_offset = 2;
						// ret_offset = i + 10000;		// brute end
						return false;
					}
		
				}
				else if (IsThisChordsField(i, work_str))
				{
					text_line += '\n';
					mode_offset = 1;		// pomijam powtorki
					ret_offset = i;

					if (text_line == "")
						return true;        // error	
					else
						return false;
				}
				else
					text_line += znak;
			}

			return true;

		}   // GetSongTextLine


		// ************************************************************************
		// czy to jest poczatek kawalka
		public bool IsThisStartOfSong(int offset, string work_str)
		{
			int i;
			char znak;
			byte mode = 0;

			for (i = 0; i < 12; i++)
			{
				znak = work_str[offset + i];
				switch (mode)
				{
				case 0:		// czekam na '('
					if (znak == '(')
						++mode;
					else if (IsThisNumberChar(znak) == false)
						return false;
					break;

				case 1:		// '(' juz byl
					if (znak == ')')
						return true;
					else if (IsThisAbc(znak))
						++mode;
					else if (IsThisNumberChar(znak) == false)
						return false;
					break;

				case 2:     // byla litera teraz musi byc ')'
					return true;
				
				/*	Nie wiem o co tu chodzi. to normalnie bylo wlaczone
					if (znak == ')')
						return true;
					else
						return false;
					break;
				*/
				}
			}

			return false;

		}   // IsThisStartOfSong


		// ************************************************************************
		// Wyodrebnia linie z powtorzeniami - /2x itp
		public bool GetSongRepeatLine(int offset, string work_str, out int ret_offset, out string repeat_line, out int mode_offset)
		{
			int i;
			char znak;
			repeat_line = "";
			ret_offset = offset; ;
			mode_offset = 0;

			for (i = offset; i < 10000; i++)
			{
				znak = work_str[i];
				if (znak == '\n')
				{
					// repeat_line += '\n';
					ret_offset = i + 1;
					mode_offset = 1;
					if (repeat_line == "")
						return true;        // error	
					else
						return false;
				}
				else if (znak == ' ')
				{
					if ( (work_str[i + 1] != '\n') && (IsThisChordsField(i + 1, work_str) == false) )
						repeat_line += znak;
				}
				else if (IsThisChordsField(i, work_str))
				{
					repeat_line += '\n';
					ret_offset = i;
					if (repeat_line == "")
						return true;        // error	
					else
						return false;
				}
				else
					repeat_line += znak;
			}

			return false;

		}   // GetSongRepeatLine


		// ************************************************************************
		// Wyodrebnia linie z akordami
		public bool GetSongChordLine(int offset, string work_str, out int ret_offset, out string chord_line)
		{
			int i;
			char znak;
			chord_line = "";
			ret_offset = offset; ;
			mode_offset = 0;

			for (i = offset; i < 10000; i++)
			{
				znak = work_str[i];
				if (znak == '\n')
				{
					chord_line += '\n';
					ret_offset = i + 1;
					return false;
				}
				else
					chord_line += znak;
			}

			return false;

		}   // GetSongChordLine


		// ************************************************************************
		// Analizuje string do konca wiersza czy to nie jest juz pole akordow
		public bool IsThisChordsField(int offset, string work_str)
		{
			char znak;
			bool chords = false;
			// bool byla_spacja = false;
			byte chord_chars;

			for (int i = offset; i < 1000; )
			{
				znak = work_str[i];
				if (znak == ' ')
				{
					// byla_spacja = true;
					i++;
					continue;
				}

				if (znak == '/')
				{
					if (chords == false)	// kreska jako pierwsza - nie jest to pole akordu
						return false;

					i++;
					continue;
				}

				if (znak == 'x')		// pole x2, x3
				{
					if (IsThisNumberChar(work_str[i + 1]) )
					{
						if (chords == false)    // nie ma przed powtorka akordow - to nie jest pole akordu
							return false;

						i += 2;
						continue;
					}
					else
						return false;
				}

				if (znak == '\n')
				{
					if (chords)
						return true;
					else
						return false;
				}

				chord_chars = IsThisSimpleChord(i, work_str);
				if (chord_chars == 0)
					return false;
				else            // debug - test fix bug: allaluja -> a mol
				{
					if (chords == false)				// pierwszy znak pola akordu
					{
						if (work_str[i - 1] != ' ')		// jeden wczesniej powinna byc spacja
							return false;
					}
				}
				/*	else if (chord_chars == 100)        // end of line
					{
						if (chords)
							return true;
						else
							return false;
					}	*/

				chords = true;

				i += chord_chars;
			}

			return false;

		}   // IsThisChordsField


		// ************************************************************************
		// patrzy czy od tego miejsca przez nastepne max 3 znaki moze to byc akord
		// zwraca ilosc znakow akordu
		public byte IsThisSimpleChord(int offset, string work_str)
		{
			switch (work_str[offset])
			{
			case 'C':			case 'c':
			case 'D':			case 'd':
			case 'F':			case 'f':
			case 'G':			case 'g':

				if (work_str[offset + 1] == 'i')			// next next
				{
					if (work_str[offset + 2] == 's')		// next next next
					{
						if (work_str[offset + 3] == ' ') 
							return 3;
						else
							return 0;
					}
					else
						return 0;
				}

				/* if (work_str[offset + 1] == '7')			// next
					return 2;
				else if (work_str[offset + 1] == '4')		// next
					return 2;
				*/
				if (IsThisNumberChar(work_str[offset + 1]))		// wszelkie akordy 7kowe, 2kowe, 4kowe itp
					return 2;

				return 1;

			case 'E':			case 'e':
			case 'A':			case 'a':
			case 'B':			case 'b':
			case 'H':			case 'h':

				/* if (work_str[offset + 1] == '7')            // next
					return 2;
				*/

				if (IsThisNumberChar(work_str[offset + 1]))     // wszelkie akordy 7kowe, 2kowe, 4kowe itp
					return 2;

				return 1;
			}

			return 0;

		}   // IsThisFirstChord

/*		NU
		// ************************************************************************
		// zwraca pozycje konca stringa, zaczyna szukanie od offset
		public int SetEndOfLine(int offset, string work_str)
		{
			char znak;

			for (int j = offset; j < 100; j++)
			{
				znak = work_str[j];
				if (znak == '\n')
				{
					return (j + 1);
				}
			}

			return offset;

		}   // SetEndOfLine
*/

		// ************************************************************************
		// Wyodrebnia glowny numer piosenki
		public bool GetSongNumber1(int offset, string work_str, out int ret_offset, out string number1)
		{
			int i;
			char znak;
			number1 = "";
			ret_offset = 0;

			for (i = 0; i < 10; i++)
			{
				znak = work_str[offset + i];
				if (znak == '(')
				{
					ret_offset = offset + i + 1;
					if (number1 == "")
						return true;        // error
					else
					{
						if (AnaliseNumber1(++act_song_number, number1, out number1))
							return true;        // error

						return false;
					}
				}
				else if (IsThisNumberChar(znak))
					number1 += znak;
			}

			return true;		// error

		}   // GetSongNumber1


		// ************************************************************************
		// Sprawdza czy odczytany numer songa jest prawidolwy, ewentualnie obcina
		// Numery stron sie tu dopisuja
		public bool AnaliseNumber1(short number, string str_num, out string str_num_out)
		{
			str_num_out = "";

			for (int i = 0; i < str_num.Length; i++)
			{
				str_num_out = str_num.Substring(i);
				if (str_num_out == number.ToString())
					return false;
			}

			return true;	// error

		}   // AnaliseNumber1


		// ************************************************************************
		// Wyodrebnia 2gi numer piosenki
		public bool GetSongNumber2(int offset, string work_str, out int ret_offset, out string number2)
		{
			int i;
			char znak;
			number2 = "";
			ret_offset = 0;

			for (i = 0; i < 5; i++)
			{
				znak = work_str[offset + i];
				if (znak == ')')
				{
					ret_offset = offset + i + 1;
					if (number2 == "")
						return true;        // error
					else
						return false;
				}
				else if (IsThisNumberChar(znak))
				{
					number2 += znak;
				}
				else if (IsThisAbc(znak))
				{
					if (work_str[offset + i + 1] == ')')
						number2 += znak;
				}
			}

			return true;        // error

		}   // GetSongNumber2


		// ************************************************************************
		// Wyodrebnia nazwe piosenki
		public bool GetSongName(int offset, string work_str, out int ret_offset, out string nazwa)
		{
			int i;
			bool starting = true;
			char znak;
			nazwa = "";
			ret_offset = 0;

			for (i = 0; i < 40; i++)
			{
				znak = work_str[offset + i];

				if (starting == true)       // na przodzie olewam spacje
				{
					if (znak == ' ')
						continue;
					starting = false;
				}

				if (znak == '\n')
				{
					ret_offset = offset + i + 1;
					if (nazwa == "")
						return true;        // error	
					else
						return false;
				}
				else if (znak == ' ')           // spacje dodaje jesli za nia nie ma kropki (koniec nazwy)
				{
					if (work_str[offset + i + 1] != '.')
						nazwa += znak;
				}
				else if (IsThisTitleChar(znak))
					nazwa += znak;
				else if (znak == ',')
					nazwa += znak;
				else if (znak == '.')       // olewamy
					znak++;                 // NOP
			}

			return true;

		}   // GetSongName


		// ************************************************************************
		// Czy dany znak jest liczba
		public bool IsThisNumberChar(char znak)
		{
			if ((znak >= '0') && (znak <= '9'))
				return true;
			else
				return false;

		}   // IsThisNumberChar


		// ************************************************************************
		// Na koncu numeru moze byc a, b lub c
		public bool IsThisAbc(char znak)
		{
			if (znak == 'a')
				return true;

			if (znak == 'b')
				return true;

			if (znak == 'c')
				return true;

			return false;

		}   // IsThisAbc

/*		NU
		// ************************************************************************
		public bool IsThisSpecialTextChar(char znak)
		{
			switch (znak)
			{
			case ',':
			case '.':
			case ' ':
				return true;
			}

			return false;

		}   // IsThisSpecialChar
*/

/*		NU
		// ************************************************************************
		public bool IsThisSpecialNameChar(char znak)
		{
			switch (znak)
			{
			case ',':
			case ' ':
				return true;
			}

			return false;

		}   // IsThisSpecialNameChar
*/

		// ************************************************************************
		// Czy dany znak jest znakiem ktory moze wystapic w tytule
		public bool IsThisTitleChar(char znak)
		{
			if ((znak >= 'A') && (znak <= 'Z'))
				return true;

			if ((znak >= 'a') && (znak <= 'z'))
				return true;

			switch (znak)
			{
			case 'ą':			case 'Ą':			case 'ć':			case 'Ć':
			case 'ę':			case 'Ę':			case 'ł':			case 'Ł':
			case 'ń':			case 'Ń':			case 'ó':			case 'Ó':
			case 'ś':			case 'Ś':			case 'ź':			case 'Ź':
			case 'ż':			case 'Ż':
				return true;
			}

			return false;

		}   // IsThisTitleChar


/*		NU
		// ************************************************************************
		public bool GetSongText(int offset, string work_str, out int ret_offset, out string text)
		{
			int i;
			string text_line = "";
			text = "";
			ret_offset = 0;

			for (i = offset; i < 10000; )
			{
				GetSongTextLine(i, work_str, out i, out text_line);
				text += text_line;
			}

			return true;

		}   // GetSongText
*/

	}

}
 