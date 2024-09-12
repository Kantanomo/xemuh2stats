namespace xemuh2stats
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.go_text_box = new System.Windows.Forms.TextBox();
            this.go_button = new System.Windows.Forms.Button();
            this.valid_label = new System.Windows.Forms.Label();
            this.main_tab_control = new System.Windows.Forms.TabControl();
            this.setup_tab_page = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.profile_disabled_check_box = new System.Windows.Forms.CheckBox();
            this.iso_select = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.exec_text_box = new System.Windows.Forms.TextBox();
            this.players_tab_page = new System.Windows.Forms.TabPage();
            this.players_table = new System.Windows.Forms.DataGridView();
            this.column_player_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_player_score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_player_kills = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_player_deaths = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_player_assists = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column_kda = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weapon_stats_tab = new System.Windows.Forms.TabPage();
            this.weapon_player_select = new System.Windows.Forms.ComboBox();
            this.weapon_stat_table = new System.Windows.Forms.DataGridView();
            this.weapon_name_coluimn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weapon_kills_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weapon_headshot_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weapon_deaths_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weapon_suicide_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weapon_shots_fired_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weapon_shots_hit_column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.debug_tab = new System.Windows.Forms.TabPage();
            this.debug_table = new System.Windows.Forms.DataGridView();
            this.asdf_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.asdf_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.asdf_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.asdf_4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.main_timer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.life_cycle_status_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.variant_status_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.game_type_status_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.map_status_label = new System.Windows.Forms.ToolStripStatusLabel();
            this.main_tab_control.SuspendLayout();
            this.setup_tab_page.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.players_tab_page.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.players_table)).BeginInit();
            this.weapon_stats_tab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weapon_stat_table)).BeginInit();
            this.debug_tab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.debug_table)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // go_text_box
            // 
            this.go_text_box.Location = new System.Drawing.Point(152, 40);
            this.go_text_box.Name = "go_text_box";
            this.go_text_box.Size = new System.Drawing.Size(160, 20);
            this.go_text_box.TabIndex = 0;
            // 
            // go_button
            // 
            this.go_button.Location = new System.Drawing.Point(341, 81);
            this.go_button.Name = "go_button";
            this.go_button.Size = new System.Drawing.Size(69, 20);
            this.go_button.TabIndex = 1;
            this.go_button.Text = "Attach";
            this.go_button.UseVisualStyleBackColor = true;
            this.go_button.Click += new System.EventHandler(this.go_button_Click);
            // 
            // valid_label
            // 
            this.valid_label.AutoSize = true;
            this.valid_label.Location = new System.Drawing.Point(416, 85);
            this.valid_label.Name = "valid_label";
            this.valid_label.Size = new System.Drawing.Size(16, 13);
            this.valid_label.TabIndex = 2;
            this.valid_label.Text = "...";
            // 
            // main_tab_control
            // 
            this.main_tab_control.Controls.Add(this.setup_tab_page);
            this.main_tab_control.Controls.Add(this.players_tab_page);
            this.main_tab_control.Controls.Add(this.weapon_stats_tab);
            this.main_tab_control.Controls.Add(this.debug_tab);
            this.main_tab_control.Location = new System.Drawing.Point(12, 12);
            this.main_tab_control.Name = "main_tab_control";
            this.main_tab_control.SelectedIndex = 0;
            this.main_tab_control.Size = new System.Drawing.Size(776, 650);
            this.main_tab_control.TabIndex = 3;
            // 
            // setup_tab_page
            // 
            this.setup_tab_page.Controls.Add(this.groupBox1);
            this.setup_tab_page.Controls.Add(this.iso_select);
            this.setup_tab_page.Controls.Add(this.label1);
            this.setup_tab_page.Controls.Add(this.valid_label);
            this.setup_tab_page.Controls.Add(this.label2);
            this.setup_tab_page.Controls.Add(this.go_button);
            this.setup_tab_page.Controls.Add(this.exec_text_box);
            this.setup_tab_page.Controls.Add(this.go_text_box);
            this.setup_tab_page.Location = new System.Drawing.Point(4, 22);
            this.setup_tab_page.Name = "setup_tab_page";
            this.setup_tab_page.Padding = new System.Windows.Forms.Padding(3);
            this.setup_tab_page.Size = new System.Drawing.Size(768, 624);
            this.setup_tab_page.TabIndex = 2;
            this.setup_tab_page.Text = "setup";
            this.setup_tab_page.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.profile_disabled_check_box);
            this.groupBox1.Location = new System.Drawing.Point(6, 447);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(756, 171);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "options";
            // 
            // profile_disabled_check_box
            // 
            this.profile_disabled_check_box.AutoSize = true;
            this.profile_disabled_check_box.Location = new System.Drawing.Point(6, 19);
            this.profile_disabled_check_box.Name = "profile_disabled_check_box";
            this.profile_disabled_check_box.Size = new System.Drawing.Size(77, 17);
            this.profile_disabled_check_box.TabIndex = 0;
            this.profile_disabled_check_box.Text = "Dedi mode";
            this.profile_disabled_check_box.UseVisualStyleBackColor = true;
            // 
            // iso_select
            // 
            this.iso_select.FormattingEnabled = true;
            this.iso_select.Items.AddRange(new object[] {
            "1.5 base iso",
            "r1 iso",
            "1.0 base iso"});
            this.iso_select.Location = new System.Drawing.Point(216, 80);
            this.iso_select.Name = "iso_select";
            this.iso_select.Size = new System.Drawing.Size(119, 21);
            this.iso_select.TabIndex = 5;
            this.iso_select.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Address A:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(437, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Address B:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // exec_text_box
            // 
            this.exec_text_box.Location = new System.Drawing.Point(533, 40);
            this.exec_text_box.Name = "exec_text_box";
            this.exec_text_box.Size = new System.Drawing.Size(160, 20);
            this.exec_text_box.TabIndex = 6;
            // 
            // players_tab_page
            // 
            this.players_tab_page.Controls.Add(this.players_table);
            this.players_tab_page.Location = new System.Drawing.Point(4, 22);
            this.players_tab_page.Name = "players_tab_page";
            this.players_tab_page.Padding = new System.Windows.Forms.Padding(3);
            this.players_tab_page.Size = new System.Drawing.Size(768, 624);
            this.players_tab_page.TabIndex = 0;
            this.players_tab_page.Text = "Players";
            this.players_tab_page.UseVisualStyleBackColor = true;
            // 
            // players_table
            // 
            this.players_table.AllowUserToAddRows = false;
            this.players_table.AllowUserToDeleteRows = false;
            this.players_table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.players_table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.column_player_name,
            this.column_player_score,
            this.column_player_kills,
            this.column_player_deaths,
            this.column_player_assists,
            this.column_kda});
            this.players_table.Location = new System.Drawing.Point(6, 6);
            this.players_table.Name = "players_table";
            this.players_table.ReadOnly = true;
            this.players_table.Size = new System.Drawing.Size(756, 599);
            this.players_table.TabIndex = 0;
            // 
            // column_player_name
            // 
            this.column_player_name.HeaderText = "Player Name";
            this.column_player_name.Name = "column_player_name";
            this.column_player_name.ReadOnly = true;
            this.column_player_name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // column_player_score
            // 
            this.column_player_score.HeaderText = "Score";
            this.column_player_score.Name = "column_player_score";
            this.column_player_score.ReadOnly = true;
            // 
            // column_player_kills
            // 
            this.column_player_kills.HeaderText = "Kills";
            this.column_player_kills.Name = "column_player_kills";
            this.column_player_kills.ReadOnly = true;
            this.column_player_kills.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // column_player_deaths
            // 
            this.column_player_deaths.HeaderText = "Deaths";
            this.column_player_deaths.Name = "column_player_deaths";
            this.column_player_deaths.ReadOnly = true;
            this.column_player_deaths.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // column_player_assists
            // 
            this.column_player_assists.HeaderText = "Assists";
            this.column_player_assists.Name = "column_player_assists";
            this.column_player_assists.ReadOnly = true;
            this.column_player_assists.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // column_kda
            // 
            this.column_kda.HeaderText = "KDA";
            this.column_kda.Name = "column_kda";
            this.column_kda.ReadOnly = true;
            this.column_kda.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // weapon_stats_tab
            // 
            this.weapon_stats_tab.Controls.Add(this.weapon_player_select);
            this.weapon_stats_tab.Controls.Add(this.weapon_stat_table);
            this.weapon_stats_tab.Location = new System.Drawing.Point(4, 22);
            this.weapon_stats_tab.Name = "weapon_stats_tab";
            this.weapon_stats_tab.Padding = new System.Windows.Forms.Padding(3);
            this.weapon_stats_tab.Size = new System.Drawing.Size(768, 624);
            this.weapon_stats_tab.TabIndex = 1;
            this.weapon_stats_tab.Text = "Weapon Stats";
            this.weapon_stats_tab.UseVisualStyleBackColor = true;
            // 
            // weapon_player_select
            // 
            this.weapon_player_select.FormattingEnabled = true;
            this.weapon_player_select.Location = new System.Drawing.Point(6, 6);
            this.weapon_player_select.Name = "weapon_player_select";
            this.weapon_player_select.Size = new System.Drawing.Size(121, 21);
            this.weapon_player_select.TabIndex = 1;
            // 
            // weapon_stat_table
            // 
            this.weapon_stat_table.AllowUserToAddRows = false;
            this.weapon_stat_table.AllowUserToDeleteRows = false;
            this.weapon_stat_table.CausesValidation = false;
            this.weapon_stat_table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.weapon_stat_table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.weapon_name_coluimn,
            this.weapon_kills_column,
            this.weapon_headshot_column,
            this.weapon_deaths_column,
            this.weapon_suicide_column,
            this.weapon_shots_fired_column,
            this.weapon_shots_hit_column});
            this.weapon_stat_table.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.weapon_stat_table.Location = new System.Drawing.Point(6, 33);
            this.weapon_stat_table.MultiSelect = false;
            this.weapon_stat_table.Name = "weapon_stat_table";
            this.weapon_stat_table.ReadOnly = true;
            this.weapon_stat_table.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.weapon_stat_table.Size = new System.Drawing.Size(756, 572);
            this.weapon_stat_table.TabIndex = 0;
            // 
            // weapon_name_coluimn
            // 
            this.weapon_name_coluimn.HeaderText = "name";
            this.weapon_name_coluimn.Name = "weapon_name_coluimn";
            this.weapon_name_coluimn.ReadOnly = true;
            this.weapon_name_coluimn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // weapon_kills_column
            // 
            this.weapon_kills_column.HeaderText = "kills";
            this.weapon_kills_column.Name = "weapon_kills_column";
            this.weapon_kills_column.ReadOnly = true;
            this.weapon_kills_column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // weapon_headshot_column
            // 
            this.weapon_headshot_column.HeaderText = "head shot Kills";
            this.weapon_headshot_column.Name = "weapon_headshot_column";
            this.weapon_headshot_column.ReadOnly = true;
            this.weapon_headshot_column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // weapon_deaths_column
            // 
            this.weapon_deaths_column.HeaderText = "deaths";
            this.weapon_deaths_column.Name = "weapon_deaths_column";
            this.weapon_deaths_column.ReadOnly = true;
            this.weapon_deaths_column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // weapon_suicide_column
            // 
            this.weapon_suicide_column.HeaderText = "suicide";
            this.weapon_suicide_column.Name = "weapon_suicide_column";
            this.weapon_suicide_column.ReadOnly = true;
            this.weapon_suicide_column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // weapon_shots_fired_column
            // 
            this.weapon_shots_fired_column.HeaderText = "shots fired";
            this.weapon_shots_fired_column.Name = "weapon_shots_fired_column";
            this.weapon_shots_fired_column.ReadOnly = true;
            this.weapon_shots_fired_column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // weapon_shots_hit_column
            // 
            this.weapon_shots_hit_column.HeaderText = "shots hit";
            this.weapon_shots_hit_column.Name = "weapon_shots_hit_column";
            this.weapon_shots_hit_column.ReadOnly = true;
            this.weapon_shots_hit_column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // debug_tab
            // 
            this.debug_tab.Controls.Add(this.debug_table);
            this.debug_tab.Location = new System.Drawing.Point(4, 22);
            this.debug_tab.Name = "debug_tab";
            this.debug_tab.Padding = new System.Windows.Forms.Padding(3);
            this.debug_tab.Size = new System.Drawing.Size(768, 624);
            this.debug_tab.TabIndex = 3;
            this.debug_tab.Text = "debug";
            this.debug_tab.UseVisualStyleBackColor = true;
            // 
            // debug_table
            // 
            this.debug_table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.debug_table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.asdf_1,
            this.asdf_2,
            this.asdf_3,
            this.asdf_4});
            this.debug_table.Location = new System.Drawing.Point(3, 6);
            this.debug_table.Name = "debug_table";
            this.debug_table.Size = new System.Drawing.Size(756, 612);
            this.debug_table.TabIndex = 0;
            // 
            // asdf_1
            // 
            this.asdf_1.HeaderText = "name";
            this.asdf_1.Name = "asdf_1";
            // 
            // asdf_2
            // 
            this.asdf_2.HeaderText = "score";
            this.asdf_2.Name = "asdf_2";
            // 
            // asdf_3
            // 
            this.asdf_3.HeaderText = "kills";
            this.asdf_3.Name = "asdf_3";
            // 
            // asdf_4
            // 
            this.asdf_4.HeaderText = "kills as";
            this.asdf_4.Name = "asdf_4";
            // 
            // main_timer
            // 
            this.main_timer.Enabled = true;
            this.main_timer.Tick += new System.EventHandler(this.main_timer_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.life_cycle_status_label,
            this.variant_status_label,
            this.game_type_status_label,
            this.map_status_label});
            this.statusStrip1.Location = new System.Drawing.Point(0, 665);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // life_cycle_status_label
            // 
            this.life_cycle_status_label.Name = "life_cycle_status_label";
            this.life_cycle_status_label.Size = new System.Drawing.Size(61, 17);
            this.life_cycle_status_label.Text = "Life Cycle:";
            // 
            // variant_status_label
            // 
            this.variant_status_label.Name = "variant_status_label";
            this.variant_status_label.Size = new System.Drawing.Size(46, 17);
            this.variant_status_label.Text = "Variant:";
            // 
            // game_type_status_label
            // 
            this.game_type_status_label.Name = "game_type_status_label";
            this.game_type_status_label.Size = new System.Drawing.Size(68, 17);
            this.game_type_status_label.Text = "Game Type:";
            // 
            // map_status_label
            // 
            this.map_status_label.Name = "map_status_label";
            this.map_status_label.Size = new System.Drawing.Size(34, 17);
            this.map_status_label.Text = "Map:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 687);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.main_tab_control);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "I hate my life";
            this.main_tab_control.ResumeLayout(false);
            this.setup_tab_page.ResumeLayout(false);
            this.setup_tab_page.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.players_tab_page.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.players_table)).EndInit();
            this.weapon_stats_tab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.weapon_stat_table)).EndInit();
            this.debug_tab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.debug_table)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox go_text_box;
        private System.Windows.Forms.Button go_button;
        private System.Windows.Forms.Label valid_label;
        private System.Windows.Forms.TabControl main_tab_control;
        private System.Windows.Forms.TabPage players_tab_page;
        private System.Windows.Forms.Timer main_timer;
        private System.Windows.Forms.DataGridView players_table;
        private System.Windows.Forms.TabPage weapon_stats_tab;
        private System.Windows.Forms.DataGridView weapon_stat_table;
        private System.Windows.Forms.ComboBox weapon_player_select;
        private System.Windows.Forms.ComboBox iso_select;
        private System.Windows.Forms.TextBox exec_text_box;
        private System.Windows.Forms.TabPage setup_tab_page;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel life_cycle_status_label;
        private System.Windows.Forms.ToolStripStatusLabel variant_status_label;
        private System.Windows.Forms.ToolStripStatusLabel game_type_status_label;
        private System.Windows.Forms.ToolStripStatusLabel map_status_label;
        private System.Windows.Forms.DataGridViewTextBoxColumn weapon_name_coluimn;
        private System.Windows.Forms.DataGridViewTextBoxColumn weapon_kills_column;
        private System.Windows.Forms.DataGridViewTextBoxColumn weapon_headshot_column;
        private System.Windows.Forms.DataGridViewTextBoxColumn weapon_deaths_column;
        private System.Windows.Forms.DataGridViewTextBoxColumn weapon_suicide_column;
        private System.Windows.Forms.DataGridViewTextBoxColumn weapon_shots_fired_column;
        private System.Windows.Forms.DataGridViewTextBoxColumn weapon_shots_hit_column;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_player_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_player_score;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_player_kills;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_player_deaths;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_player_assists;
        private System.Windows.Forms.DataGridViewTextBoxColumn column_kda;
        private System.Windows.Forms.TabPage debug_tab;
        private System.Windows.Forms.DataGridView debug_table;
        private System.Windows.Forms.DataGridViewTextBoxColumn asdf_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn asdf_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn asdf_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn asdf_4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox profile_disabled_check_box;
    }
}

