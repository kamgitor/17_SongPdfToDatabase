using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SongPdfToDatabase
{
	public partial class SimpleSongView : Form
	{
		public SimpleSongView()
		{
			InitializeComponent();
		}


		// przeciazony konstruktor
		public SimpleSongView(string num1, string num2, string name, string text, string repeat, string chords)
		{
			InitializeComponent();

			v_alltitle.Text = num1 + " (" + num2 + ") " + name;
			string[] title_tab = text.Split('\n');
			string[] repeat_tab = repeat.Split('\n');
			string[] chrods_tab = chords.Split('\n');

			for (int i = 0; i < title_tab.Length; i++)
			{
				v_text.Items.Add(title_tab[i]);
				v_repeat.Items.Add(repeat_tab[i]);
				v_chords.Items.Add(chrods_tab[i]);
			}

			/*
			v_text.Items.Add(text);
			v_repeat.Items.Add(repeat);
			v_chords.Items.Add(chords);
			*/

		}   // SimpleSongView


	}
}
