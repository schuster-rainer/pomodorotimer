namespace PomodoroTimer
{
	partial class AboutDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose ( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose ();
			}
			base.Dispose ( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager ( typeof ( AboutDialog ) );
			this.richTextBoxInfo = new System.Windows.Forms.RichTextBox ();
			this.SuspendLayout ();
			// 
			// richTextBoxInfo
			// 
			this.richTextBoxInfo.Anchor = ( ( System.Windows.Forms.AnchorStyles )( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom ) 
            | System.Windows.Forms.AnchorStyles.Left ) 
            | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.richTextBoxInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.richTextBoxInfo.Location = new System.Drawing.Point ( 12, 12 );
			this.richTextBoxInfo.Name = "richTextBoxInfo";
			this.richTextBoxInfo.ReadOnly = true;
			this.richTextBoxInfo.Size = new System.Drawing.Size ( 452, 404 );
			this.richTextBoxInfo.TabIndex = 5;
			this.richTextBoxInfo.Text = resources.GetString ( "richTextBoxInfo.Text" );
			// 
			// AboutDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size ( 476, 428 );
			this.Controls.Add ( this.richTextBoxInfo );
			this.Icon = ( ( System.Drawing.Icon )( resources.GetObject ( "$this.Icon" ) ) );
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Clean Code Developer - Pomodoro Timer";
			this.ResumeLayout ( false );

		}

		#endregion

		private System.Windows.Forms.RichTextBox richTextBoxInfo;
	}
}