namespace SongPdfToDatabase
{
	partial class SimpleSongView
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.v_chords = new System.Windows.Forms.ListView();
			this.v_repeat = new System.Windows.Forms.ListView();
			this.v_text = new System.Windows.Forms.ListView();
			this.v_alltitle = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// v_chords
			// 
			this.v_chords.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.v_chords.Location = new System.Drawing.Point(636, 44);
			this.v_chords.Name = "v_chords";
			this.v_chords.Size = new System.Drawing.Size(157, 423);
			this.v_chords.TabIndex = 9;
			this.v_chords.UseCompatibleStateImageBehavior = false;
			this.v_chords.View = System.Windows.Forms.View.List;
			// 
			// v_repeat
			// 
			this.v_repeat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.v_repeat.Location = new System.Drawing.Point(520, 44);
			this.v_repeat.Name = "v_repeat";
			this.v_repeat.Size = new System.Drawing.Size(110, 423);
			this.v_repeat.TabIndex = 8;
			this.v_repeat.UseCompatibleStateImageBehavior = false;
			this.v_repeat.View = System.Windows.Forms.View.List;
			// 
			// v_text
			// 
			this.v_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.v_text.Location = new System.Drawing.Point(23, 44);
			this.v_text.Name = "v_text";
			this.v_text.Size = new System.Drawing.Size(498, 423);
			this.v_text.TabIndex = 7;
			this.v_text.UseCompatibleStateImageBehavior = false;
			this.v_text.View = System.Windows.Forms.View.List;
			// 
			// v_alltitle
			// 
			this.v_alltitle.AutoSize = true;
			this.v_alltitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.v_alltitle.Location = new System.Drawing.Point(17, 10);
			this.v_alltitle.Name = "v_alltitle";
			this.v_alltitle.Size = new System.Drawing.Size(62, 31);
			this.v_alltitle.TabIndex = 6;
			this.v_alltitle.Text = "100";
			// 
			// SimpleSongView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(808, 486);
			this.Controls.Add(this.v_chords);
			this.Controls.Add(this.v_repeat);
			this.Controls.Add(this.v_text);
			this.Controls.Add(this.v_alltitle);
			this.Name = "SimpleSongView";
			this.Text = "SimpleSongView";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView v_chords;
		private System.Windows.Forms.ListView v_repeat;
		private System.Windows.Forms.ListView v_text;
		private System.Windows.Forms.Label v_alltitle;
	}
}