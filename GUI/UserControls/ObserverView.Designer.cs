namespace ScreenNumericObserver.GUI.UserControls
{
	partial class ObserverView
	{
		/// <summary> 
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region コンポーネント デザイナーで生成されたコード

		/// <summary> 
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObserverView));
			this.NotifyCheckBox = new System.Windows.Forms.CheckBox();
			this.LogGroupBox = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.LogTextBox = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.WordWrapCheckBox = new System.Windows.Forms.CheckBox();
			this.ClearLogButton = new System.Windows.Forms.Button();
			this.SaveLogButton = new System.Windows.Forms.Button();
			this.StopButton = new System.Windows.Forms.Button();
			this.StartButton = new System.Windows.Forms.Button();
			this.PreviewGroupBox = new System.Windows.Forms.GroupBox();
			this.CapturePictureBox = new System.Windows.Forms.PictureBox();
			this.CaptureMessageLabel = new System.Windows.Forms.Label();
			this.CaptureScaleUpDown = new System.Windows.Forms.NumericUpDown();
			this.CaptureScaleLabel = new System.Windows.Forms.Label();
			this.UpdateIntervalLabel = new System.Windows.Forms.Label();
			this.TitleLabel = new System.Windows.Forms.Label();
			this.TitleTextBox = new System.Windows.Forms.TextBox();
			this.CaptureAreaLabel = new System.Windows.Forms.Label();
			this.UpdateIntervalUpDown = new System.Windows.Forms.NumericUpDown();
			this.CaptureButton = new System.Windows.Forms.Button();
			this.ConfirmButton = new System.Windows.Forms.Button();
			this.CaptureGrayScaleCheckBox = new System.Windows.Forms.CheckBox();
			this.ObserverGoupBox = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.LogGroupBox.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			this.PreviewGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.CapturePictureBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CaptureScaleUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.UpdateIntervalUpDown)).BeginInit();
			this.ObserverGoupBox.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// NotifyCheckBox
			// 
			resources.ApplyResources(this.NotifyCheckBox, "NotifyCheckBox");
			this.NotifyCheckBox.Name = "NotifyCheckBox";
			this.NotifyCheckBox.UseVisualStyleBackColor = true;
			this.NotifyCheckBox.CheckedChanged += new System.EventHandler(this.NotifyCheckBox_CheckedChanged);
			// 
			// LogGroupBox
			// 
			resources.ApplyResources(this.LogGroupBox, "LogGroupBox");
			this.LogGroupBox.Controls.Add(this.tableLayoutPanel5);
			this.LogGroupBox.Name = "LogGroupBox";
			this.LogGroupBox.TabStop = false;
			// 
			// tableLayoutPanel5
			// 
			resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
			this.tableLayoutPanel5.Controls.Add(this.LogTextBox, 0, 1);
			this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel4, 0, 0);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			// 
			// LogTextBox
			// 
			resources.ApplyResources(this.LogTextBox, "LogTextBox");
			this.LogTextBox.Name = "LogTextBox";
			// 
			// tableLayoutPanel4
			// 
			resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
			this.tableLayoutPanel4.Controls.Add(this.WordWrapCheckBox, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.ClearLogButton, 2, 0);
			this.tableLayoutPanel4.Controls.Add(this.SaveLogButton, 1, 0);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			// 
			// WordWrapCheckBox
			// 
			resources.ApplyResources(this.WordWrapCheckBox, "WordWrapCheckBox");
			this.WordWrapCheckBox.Name = "WordWrapCheckBox";
			this.WordWrapCheckBox.UseVisualStyleBackColor = true;
			this.WordWrapCheckBox.CheckedChanged += new System.EventHandler(this.WordWrapCheckBox_CheckedChanged);
			// 
			// ClearLogButton
			// 
			resources.ApplyResources(this.ClearLogButton, "ClearLogButton");
			this.ClearLogButton.Name = "ClearLogButton";
			this.ClearLogButton.UseVisualStyleBackColor = true;
			this.ClearLogButton.Click += new System.EventHandler(this.ClearLogButton_Click);
			// 
			// SaveLogButton
			// 
			resources.ApplyResources(this.SaveLogButton, "SaveLogButton");
			this.SaveLogButton.Name = "SaveLogButton";
			this.SaveLogButton.UseVisualStyleBackColor = true;
			this.SaveLogButton.Click += new System.EventHandler(this.SaveLogButton_Click);
			// 
			// StopButton
			// 
			resources.ApplyResources(this.StopButton, "StopButton");
			this.StopButton.Name = "StopButton";
			this.StopButton.UseVisualStyleBackColor = true;
			this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
			// 
			// StartButton
			// 
			resources.ApplyResources(this.StartButton, "StartButton");
			this.StartButton.Name = "StartButton";
			this.StartButton.UseVisualStyleBackColor = true;
			this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
			// 
			// PreviewGroupBox
			// 
			resources.ApplyResources(this.PreviewGroupBox, "PreviewGroupBox");
			this.PreviewGroupBox.Controls.Add(this.CapturePictureBox);
			this.PreviewGroupBox.Controls.Add(this.CaptureMessageLabel);
			this.PreviewGroupBox.Name = "PreviewGroupBox";
			this.PreviewGroupBox.TabStop = false;
			// 
			// CapturePictureBox
			// 
			resources.ApplyResources(this.CapturePictureBox, "CapturePictureBox");
			this.CapturePictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
			this.CapturePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.CapturePictureBox.Name = "CapturePictureBox";
			this.CapturePictureBox.TabStop = false;
			// 
			// CaptureMessageLabel
			// 
			resources.ApplyResources(this.CaptureMessageLabel, "CaptureMessageLabel");
			this.CaptureMessageLabel.Name = "CaptureMessageLabel";
			// 
			// CaptureScaleUpDown
			// 
			resources.ApplyResources(this.CaptureScaleUpDown, "CaptureScaleUpDown");
			this.CaptureScaleUpDown.DecimalPlaces = 1;
			this.CaptureScaleUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.CaptureScaleUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            65536});
			this.CaptureScaleUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.CaptureScaleUpDown.Name = "CaptureScaleUpDown";
			this.CaptureScaleUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.CaptureScaleUpDown.ValueChanged += new System.EventHandler(this.CaptureScaleUpDown_ValueChanged);
			// 
			// CaptureScaleLabel
			// 
			resources.ApplyResources(this.CaptureScaleLabel, "CaptureScaleLabel");
			this.CaptureScaleLabel.Name = "CaptureScaleLabel";
			// 
			// UpdateIntervalLabel
			// 
			resources.ApplyResources(this.UpdateIntervalLabel, "UpdateIntervalLabel");
			this.UpdateIntervalLabel.Name = "UpdateIntervalLabel";
			// 
			// TitleLabel
			// 
			resources.ApplyResources(this.TitleLabel, "TitleLabel");
			this.TitleLabel.Name = "TitleLabel";
			// 
			// TitleTextBox
			// 
			resources.ApplyResources(this.TitleTextBox, "TitleTextBox");
			this.TitleTextBox.Name = "TitleTextBox";
			this.TitleTextBox.TextChanged += new System.EventHandler(this.TitleTextBox_TextChanged);
			// 
			// CaptureAreaLabel
			// 
			resources.ApplyResources(this.CaptureAreaLabel, "CaptureAreaLabel");
			this.CaptureAreaLabel.Name = "CaptureAreaLabel";
			// 
			// UpdateIntervalUpDown
			// 
			resources.ApplyResources(this.UpdateIntervalUpDown, "UpdateIntervalUpDown");
			this.UpdateIntervalUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
			this.UpdateIntervalUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.UpdateIntervalUpDown.Name = "UpdateIntervalUpDown";
			this.UpdateIntervalUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.UpdateIntervalUpDown.ValueChanged += new System.EventHandler(this.UpdateIntervalUpDown_ValueChanged);
			// 
			// CaptureButton
			// 
			resources.ApplyResources(this.CaptureButton, "CaptureButton");
			this.CaptureButton.Name = "CaptureButton";
			this.CaptureButton.UseVisualStyleBackColor = true;
			this.CaptureButton.Click += new System.EventHandler(this.CaptureButton_Click);
			// 
			// ConfirmButton
			// 
			resources.ApplyResources(this.ConfirmButton, "ConfirmButton");
			this.ConfirmButton.Name = "ConfirmButton";
			this.ConfirmButton.UseVisualStyleBackColor = true;
			this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
			// 
			// CaptureGrayScaleCheckBox
			// 
			resources.ApplyResources(this.CaptureGrayScaleCheckBox, "CaptureGrayScaleCheckBox");
			this.CaptureGrayScaleCheckBox.Name = "CaptureGrayScaleCheckBox";
			this.CaptureGrayScaleCheckBox.UseVisualStyleBackColor = true;
			this.CaptureGrayScaleCheckBox.CheckedChanged += new System.EventHandler(this.CaptureGrayScaleCheckBox_CheckedChanged);
			// 
			// ObserverGoupBox
			// 
			resources.ApplyResources(this.ObserverGoupBox, "ObserverGoupBox");
			this.ObserverGoupBox.Controls.Add(this.tableLayoutPanel2);
			this.ObserverGoupBox.Name = "ObserverGoupBox";
			this.ObserverGoupBox.TabStop = false;
			// 
			// tableLayoutPanel2
			// 
			resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			// 
			// tableLayoutPanel3
			// 
			resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
			this.tableLayoutPanel3.Controls.Add(this.PreviewGroupBox, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.LogGroupBox, 0, 1);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			// 
			// panel1
			// 
			resources.ApplyResources(this.panel1, "panel1");
			this.panel1.Controls.Add(this.StartButton);
			this.panel1.Controls.Add(this.tableLayoutPanel1);
			this.panel1.Controls.Add(this.TitleLabel);
			this.panel1.Controls.Add(this.TitleTextBox);
			this.panel1.Controls.Add(this.StopButton);
			this.panel1.Controls.Add(this.ConfirmButton);
			this.panel1.Controls.Add(this.CaptureAreaLabel);
			this.panel1.Controls.Add(this.NotifyCheckBox);
			this.panel1.Controls.Add(this.CaptureGrayScaleCheckBox);
			this.panel1.Controls.Add(this.CaptureButton);
			this.panel1.Name = "panel1";
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.CaptureScaleUpDown, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.UpdateIntervalUpDown, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.CaptureScaleLabel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.UpdateIntervalLabel, 0, 1);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// ObserverView
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ObserverGoupBox);
			this.Name = "ObserverView";
			this.LogGroupBox.ResumeLayout(false);
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			this.PreviewGroupBox.ResumeLayout(false);
			this.PreviewGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.CapturePictureBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CaptureScaleUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.UpdateIntervalUpDown)).EndInit();
			this.ObserverGoupBox.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.CheckBox NotifyCheckBox;
		private System.Windows.Forms.GroupBox LogGroupBox;
		private System.Windows.Forms.TextBox LogTextBox;
		private System.Windows.Forms.Button StopButton;
		private System.Windows.Forms.Button StartButton;
		private System.Windows.Forms.GroupBox PreviewGroupBox;
		private System.Windows.Forms.PictureBox CapturePictureBox;
		private System.Windows.Forms.Label CaptureMessageLabel;
		private System.Windows.Forms.NumericUpDown CaptureScaleUpDown;
		private System.Windows.Forms.Label CaptureScaleLabel;
		private System.Windows.Forms.Label UpdateIntervalLabel;
		private System.Windows.Forms.Label TitleLabel;
		private System.Windows.Forms.TextBox TitleTextBox;
		private System.Windows.Forms.Label CaptureAreaLabel;
		private System.Windows.Forms.NumericUpDown UpdateIntervalUpDown;
		private System.Windows.Forms.Button CaptureButton;
		private System.Windows.Forms.Button ConfirmButton;
		private System.Windows.Forms.CheckBox CaptureGrayScaleCheckBox;
		private System.Windows.Forms.Button ClearLogButton;
		private System.Windows.Forms.Button SaveLogButton;
		private System.Windows.Forms.CheckBox WordWrapCheckBox;
		private System.Windows.Forms.GroupBox ObserverGoupBox;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.Panel panel1;
	}
}
