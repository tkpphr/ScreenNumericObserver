/*
   Copyright 2018 tkpphr

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
using ScreenNumericObserver.Core;
using ScreenNumericObserver.GUI.CustomControls;
using ScreenNumericObserver.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace ScreenNumericObserver.GUI.Forms
{
	public partial class MainWindow : Form
	{
		private TesseractEngine TesseractEngine { get; set; }
		private SpeechSynthesizer SpeechSynthesizer { get; set; }
		private ScreenSaverPreventer ScreenSaverPreventer { get; set; }
		private List<Observer> Observers { get; set; }
		private FileListCache RecentFiles { get; set; }

		public MainWindow()
		{
			InitializeComponent();
			Observers = new List<Observer>();
			TesseractEngine = new TesseractEngine("./tessdata","eng");
			SpeechSynthesizer = new SpeechSynthesizer();
			if (string.IsNullOrEmpty(Settings.Default.TTSVoice))
			{
				Settings.Default.TTSVoice = SpeechSynthesizer.GetInstalledVoices().ToList().First().VoiceInfo.Name;
				Settings.Default.Save();
			}
			SpeechSynthesizer.SelectVoice(Settings.Default.TTSVoice);
			SpeechSynthesizer.Rate = Settings.Default.TTSRate;
			SpeechSynthesizer.Volume = Settings.Default.TTSVolume;
			ScreenSaverPreventer = new ScreenSaverPreventer();
			RecentFiles = new FileListCache("recent", 8, false);
			Icon = Resources.Icon;
			Text = Application.ProductName;
			RefreshView();
		}

		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			base.OnDragEnter(drgevent);
			var files = drgevent.Data.GetData(DataFormats.FileDrop, false) as string[];
			drgevent.Effect = DragDropEffects.None;
			if (files != null && files.Any(file=>file.ToLower().EndsWith(".sno")))
			{
				drgevent.Effect = DragDropEffects.Copy;
			}
		}

		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			base.OnDragDrop(drgevent);
			foreach(string file in ((string[])drgevent.Data.GetData(DataFormats.FileDrop, false)))
			{
				AddObserversFromFile(file);
			}
			RefreshView();
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
			foreach (Observer observer in Observers)
			{
				observer.Dispose();
			}
			ScreenSaverPreventer.Dispose();
			SpeechSynthesizer.SpeakAsyncCancelAll();
			SpeechSynthesizer.Dispose();
			TesseractEngine.Dispose();
		}

		private void AddObserversToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var dialog = new OpenFileDialog())
			{
				dialog.Title = Resources.Open;
				dialog.Filter = Resources.ObserversFileFilter;
				dialog.Multiselect = true;
				dialog.RestoreDirectory = true;
				if (dialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}
				foreach (string fileName in dialog.FileNames)
				{
					AddObserversFromFile(fileName);
				}
				RefreshView();
			}
		}

		private void SaveObserversToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var dialog = new SaveFileDialog())
			{
				dialog.Title = Resources.Save;
				dialog.Filter = Resources.ObserversFileFilter;
				dialog.RestoreDirectory = true;
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (ObserversFile.Save(Observers, dialog.FileName))
					{
						if (!RecentFiles.Exists || RecentFiles.CanWrite)
						{
							RecentFiles.Add(dialog.FileName);
						}
					}
					else
					{
						using (var centerAligner = new DialogCenterAligner(this))
						{
							MessageBox.Show(Resources.FailedSaveFile, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
		}

		private void RecentFilesToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			if (!RecentFiles.Exists || !RecentFiles.CanRead)
			{
				return;
			}
			RecentFilesToolStripMenuItem.DropDownItems.Clear();
			var files = RecentFiles.GetFiles();
			if (files.Count == 0)
			{
				RecentFilesToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem() { Text = Resources.None, Enabled = false });
			}
			else
			{
				files.Select(file => new ToolStripMenuItem()
				{
					Text = StringUtils.EllipsisBySeparator(file, "\\", 3),
					Enabled = File.Exists(file)
				})
				.ToList()
				.ForEach(menuItem =>
				{
					RecentFilesToolStripMenuItem.DropDownItems.Add(menuItem);
					menuItem.Click += RecentFileToolStripMenuItem_Click;
				});
			}
		}

		private void RecentFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var menuItem = sender as ToolStripMenuItem;
			int index = RecentFilesToolStripMenuItem.DropDownItems.IndexOf(menuItem);
			string filePath = RecentFiles.GetFile(index);
			AddObserversFromFile(filePath);
			RefreshView();
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void OptionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var dialog = new OptionDialog())
			{
				dialog.ShowDialog(this);
			}
			SpeechSynthesizer.SelectVoice(Settings.Default.TTSVoice);
			SpeechSynthesizer.Rate = Settings.Default.TTSRate;
			SpeechSynthesizer.Volume = Settings.Default.TTSVolume;
			
		}

		private void ManualToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string path = Environment.CurrentDirectory + "/Docs/manual.html";
			if (File.Exists(path))
			{
				Process.Start(path);
			}
			else
			{
				using (var centerAligner = new DialogCenterAligner(this))
				{
					MessageBox.Show(this, Resources.FailedOpenFile, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (var dialog = new AboutDialog())
			{
				dialog.ShowDialog();
			}
		}

		private void AddButton_Click(object sender, EventArgs e)
		{
			var observer = new Observer(TesseractEngine,SpeechSynthesizer);
			observer.Started += Observer_Started;
			observer.Stopped += Observer_Stopped;
			Observers.Add(observer);
			var observerTabPage = new ObserverTabPage(ObserverTabControl,Observers.Count - 1,observer);
			//ObserverTabControl.TabPages.Add(observerTabPage);
			ObserverTabControl.SelectedTab = observerTabPage;
			RefreshView();
		}

		private void Observer_Started(Observer observer)
		{
			if(!ScreenSaverPreventer.IsRunning)
			{
				ScreenSaverPreventer.Start();
			}
		}

		private void Observer_Stopped(Observer stoppedObserver)
		{
			if (ScreenSaverPreventer.IsRunning && Observers.All(observer=>!observer.IsObserving))
			{
				ScreenSaverPreventer.Stop();
			}
		}

		private void WrapperPanel_SizeChanged(object sender, EventArgs e)
		{
			var panel = sender as Panel;
			if (panel.Width < 547 || panel.Height < 376)
			{
				tableLayoutPanel1.Size = new Size(Math.Max(panel.Width - 20, 547), Math.Max(panel.Height - 20, 376));
				tableLayoutPanel1.Dock = DockStyle.None;
			}
			else
			{
				tableLayoutPanel1.Dock = DockStyle.Fill;
			}
		}

		private void RemoveButton_Click(object sender, EventArgs e)
		{
			int selectedIndex = ObserverTabControl.SelectedIndex;
			var selectedTabPage=ObserverTabControl.SelectedTab;
			foreach(Control control in selectedTabPage.Controls)
			{
				control.Dispose();
			}
			selectedTabPage.Dispose();
			ObserverTabControl.TabPages.Remove(selectedTabPage);
			Observers[selectedIndex].Dispose();
			Observers.RemoveAt(selectedIndex);
			if (Observers.Count != 0)
			{
				for (int i = 0; i < Observers.Count; i++)
				{
					((ObserverTabPage)ObserverTabControl.TabPages[i]).Index = i;
				}
				ObserverTabControl.SelectedTab = ObserverTabControl.TabPages[Math.Max(0, selectedIndex - 1)];
			}
			RefreshView();
		}

		private void StartAllButton_Click(object sender, EventArgs e)
		{
			foreach(Observer observer in Observers.Where(observer=>!observer.IsObserving && observer.AreaCaptured))
			{
				observer.Start();
			}
		}

		private void StopAllButton_Click(object sender, EventArgs e)
		{
			foreach (Observer observer in Observers.Where(observer => observer.IsObserving))
			{
				observer.Stop();
			}
			SpeechSynthesizer.SpeakAsyncCancelAll();
		}

		private void StopSpeechButton_Click(object sender, EventArgs e)
		{
			SpeechSynthesizer.SpeakAsyncCancelAll();
		}



		private void AddObserversFromFile(string path)
		{
			var observerSettingsList = ObserversFile.Load(path);
			if (observerSettingsList.Count == 0)
			{
				using (var centerAligner = new DialogCenterAligner(this))
				{
					MessageBox.Show(Resources.FailedOpenFile + "\n" + path, Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				return;
			}
			if (!RecentFiles.Exists || RecentFiles.CanWrite)
			{
				RecentFiles.Add(path);
			}
			var tabPages = new TabPage[observerSettingsList.Count];
			for (int i = 0; i < observerSettingsList.Count; i++)
			{
				var observer = new Observer(TesseractEngine, SpeechSynthesizer, observerSettingsList[i]);
				observer.Started += Observer_Started;
				observer.Stopped += Observer_Stopped;
				Observers.Add(observer);
				var observerTabPage = new ObserverTabPage(ObserverTabControl, Observers.Count - 1, observer);
				tabPages[i] = observerTabPage;
			}
			//ObserverTabControl.TabPages.AddRange(tabPages);
		}

		private void RefreshView()
		{
			bool isExistsObserver = Observers.Count > 0;
			SaveObserversToolStripMenuItem.Enabled = isExistsObserver;
			StartAllButton.Enabled = isExistsObserver;
			StopAllButton.Enabled = isExistsObserver;
			RemoveButton.Enabled = isExistsObserver;
			StopSpeechButton.Enabled = isExistsObserver;
			ObserverTabControl.Visible = isExistsObserver;
			EmptyMessageLabel.Visible = !isExistsObserver;
		}

		
	}
}
