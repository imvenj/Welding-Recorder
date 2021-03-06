﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Welding_Recorder
{
    public partial class EditHistoryForm : Form
    {
        public History History { get; set; }
        private bool isAutoWeld;

        public EditHistoryForm(History history, bool autoWeld = false)
        {
            History = history;
            isAutoWeld = autoWeld;
            InitializeComponent();
            PopulateListViewContent(autoWeld);
            DoOtherUIInitialization(autoWeld);
        }

        private void PopulateListViewContent(bool autoWeld = false)
        {
            SignalsListView.Columns.Add("", 30);
            SignalsListView.Columns.Add("ID", 40);
            SignalsListView.Columns.Add("信号类型", 100);
            SignalsListView.Columns.Add("档速", 50);
            SignalsListView.Columns.Add("时间戳", 150);
            SignalsListView.Columns.Add("时间差(ms)",80);
            if (autoWeld)
            {
                SignalsListView.Columns.Add("控制信号", 100);
                SignalsListView.Columns.Add("通讯时差(ms)", 100);
            }
            SignalsListView.View = View.Details;
            SignalsListView.GridLines = true;
            SignalsListView.HeaderStyle = ColumnHeaderStyle.Clickable;
            SignalsListView.FullRowSelect = true;
            SignalsListView.CheckBoxes = autoWeld ? false : true;

            PopulateData(autoWeld);
            loadWeldingDataLists();
            FillWeldingHistoryData();
        }

        private void DoOtherUIInitialization(bool autoWeld = false)
        {
            string name = History.Name;
            if (name.Length == 0)
            {
                name = autoWeld ? "未命名控制记录" : "未命名焊接记录";
            }
            Text = "正在编辑“" + name + "”";

            if (autoWeld)
            {
                DeleteSelectedItemsButton.Visible = false;
                DeSelectAllButton.Visible = false;
                EditInfoLabel.Text = "自动控制信号列表";
            }
            else
            {
                DeleteSelectedItemsButton.Visible = true;
                DeSelectAllButton.Visible = true;
            }
        }

        private void PopulateData(bool autoWeld = false)
        {
            SignalsListView.Items.Clear();
            
            //foreach (var signal in History.Signals)
            for (var i = 0; i < History.Signals.Count(); i++)
            {
                var signal = History.Signals[i];
                var row = signal.ToListItem();
                if (autoWeld)
                {
                    var autoWeldHistory = (AutoWeldHistory)History;
                    if (!autoWeldHistory.Interupted)
                    {
                        row.SubItems.Add(signal.ToString());
                        var err = signal.Delta - autoWeldHistory.Template.Signals[i].Delta;
                        row.SubItems.Add(string.Format("{0}", err));
                    }
                    else
                    {
                        row.SubItems.Add("-");
                        row.SubItems.Add("-");
                    }
                }

                SignalsListView.Items.Add(row);
            }
        }

        // Load static data from database to UI.
        private void loadWeldingDataLists()
        {
            var db = new DataProcess();
            var gangtaoList = db.GangTaoList();
            gangtaoList.ForEach((item) => {
                GangTaoTypeComboBox.Items.Add(item);
            });
            var operatorList = db.OperatorList();
            operatorList.ForEach((item) => {
                OperatorNameComboBox.Items.Add(item);
            });
            var weldingItemList = db.WeldingItemList();
            weldingItemList.ForEach((item) => {
                WeldingItemComboBox.Items.Add(item);
            });
        }

        private void FillWeldingHistoryData()
        {
            TaskNameTextBox.Text = History.TaskName;
            WeldingCurrentTextBox.Text = History.WeldingCurrent;
            ArGasFlowTextBox.Text = History.ArFlow;
            GangTaoTypeComboBox.Text = History.GangtaoType;
            WeldingItemComboBox.Text = History.WeldingItem;
            RoomTempTextBox.Text = History.RoomTemperature;
            OperatorNameComboBox.Text = History.OperatorName;
        }

        private void DeleteSelectedItemsButton_Click(object sender, EventArgs e)
        {
            var indices = SignalsListView.CheckedIndices;
            if (indices.Count <= 0)
            {
                MessageBox.Show(this, "未选择任何信号条目", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(this, "即将删除你选中的信号条目，信号条目删除后将无法恢复。\r\n\r\n是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result != DialogResult.OK)
            {
                return;
            }

            foreach (var index in indices)
            {
                var signal = History.Signals[int.Parse(index.ToString())];
                signal.Delete();
                //Console.WriteLine("Selected index is {0}, ID is {1}", index.ToString(), History.Signals[int.Parse(index.ToString())].Id);
            }
            History.ReloadSignals();
            PopulateData(isAutoWeld);
            MessageBox.Show(this, "操作成功！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {

            History.TaskName = TaskNameTextBox.Text.Trim();
            History.WeldingCurrent = WeldingCurrentTextBox.Text.Trim();
            History.ArFlow = ArGasFlowTextBox.Text.Trim();
            History.GangtaoType = GangTaoTypeComboBox.Text.Trim();
            History.WeldingItem = WeldingItemComboBox.Text.Trim();
            History.RoomTemperature = RoomTempTextBox.Text.Trim();
            History.OperatorName = OperatorNameComboBox.Text.Trim();
            History.Save();

            DialogResult = DialogResult.OK;
        }

        private void DeSelectAllButton_Click(object sender, EventArgs e)
        {
            foreach (var index in SignalsListView.CheckedIndices)
            {
                var listItem = SignalsListView.Items[int.Parse(index.ToString())];
                listItem.Checked = false;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
