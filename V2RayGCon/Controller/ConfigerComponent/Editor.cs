﻿using Newtonsoft.Json.Linq;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static V2RayGCon.Lib.StringResource;

namespace V2RayGCon.Controller.ConfigerComponet
{
    class Editor : Model.BaseClass.ConfigerComponent
    {
        Service.Cache cache;

        int preSection;
        int separator;
        Scintilla editor;

        Dictionary<int, string> sections;

        public Editor(
            Panel container,
            ComboBox section,
            ComboBox example,
            Button format,
            Button restore)
        {
            cache = Service.Cache.Instance;

            separator = Model.Data.Table.sectionSeparator;
            sections = Model.Data.Table.configSections;
            preSection = 0;

            BindEditor(container);
            AttachEvent(section, example, format, restore);

            Lib.UI.FillComboBox(section, Model.Data.Table.configSections);
        }

        #region properties
        private string _content;

        public string content
        {
            get
            {
                return _content;
            }
            set
            {
                SetField(ref _content, value);
            }
        }
        #endregion

        #region pulbic method
        public Scintilla GetEditor()
        {
            if (editor == null)
            {
                throw new NullReferenceException("Editor not ready!");
            }
            return editor;
        }

        public void ShowSection(int section = -1)
        {
            var index = section < 0 ? preSection : section;
            var config = container.config;

            index = Lib.Utils.Clamp(index, 0, sections.Count);

            if (index == 0)
            {
                content = config.ToString();
                return;
            }

            var part = config[sections[index]];
            if (part == null)
            {
                if (index >= separator)
                {
                    part = new JArray();
                }
                else
                {
                    part = new JObject();
                }
                config[sections[index]] = part;
            }
            content = part.ToString();
        }

        public bool Flush()
        {
            if (!CheckValid())
            {
                if (Lib.UI.Confirm(I18N("EditorDiscardChange")))
                {
                    DiscardChanges();
                }
                else
                {
                    return false;
                }
            }

            SaveChanges();
            return true;
        }

        public override void Update(JObject config)
        {
            // do nothing
        }
        #endregion

        #region private method
        void AttachEvent(
            ComboBox section,
            ComboBox example,
            Button format,
            Button restore)
        {
            section.SelectedIndexChanged += (s, e) =>
            {
                if (!OnSectionChanged(section.SelectedIndex))
                {
                    section.SelectedIndex = preSection;
                }
                else
                {
                    // update examples
                    UpdateExamplesDescription(example);
                }
            };

            format.Click += (s, e) =>
            {
                FormatCurrentContent();
            };

            restore.Click += (s, e) =>
            {
                example.SelectedIndex = 0;
                DiscardChanges();
            };

            example.SelectedIndexChanged += (s, e) =>
            {
                LoadExample(example.SelectedIndex - 1);
            };
        }

        void UpdateExamplesDescription(ComboBox cboxExamples)
        {
            cboxExamples.Items.Clear();

            cboxExamples.Items.Add(I18N("AvailableExamples"));
            var descriptions = GetExamplesDescription();
            if (descriptions.Count < 1)
            {
                cboxExamples.Enabled = false;
            }
            else
            {
                int maxWidth = 0, temp = 0;
                var font = cboxExamples.Font;
                cboxExamples.Enabled = true;
                foreach (var description in descriptions)
                {
                    cboxExamples.Items.Add(description);
                    temp = TextRenderer.MeasureText(description, font).Width;
                    if (temp > maxWidth)
                    {
                        maxWidth = temp;
                    }
                }
                cboxExamples.DropDownWidth = Math.Max(
                    cboxExamples.Width,
                    maxWidth + SystemInformation.VerticalScrollBarWidth);
            }
            cboxExamples.SelectedIndex = 0;
        }

        void LoadExample(int index)
        {
            if (index < 0)
            {
                return;
            }

            var examples = Model.Data.Table.examples;
            try
            {
                string key = examples[preSection][index][1];
                string content;

                if (preSection == Model.Data.Table.inboundIndex)
                {
                    var inTpl = cache.tpl.LoadExample("inTpl");
                    inTpl["protocol"] = examples[preSection][index][2];
                    inTpl["settings"] = cache.tpl.LoadExample(key);
                    content = inTpl.ToString();
                }
                else if (preSection == Model.Data.Table.outboundIndex)
                {
                    var outTpl = cache.tpl.LoadExample("outTpl");
                    outTpl["protocol"] = examples[preSection][index][2];
                    outTpl["settings"] = cache.tpl.LoadExample(key);
                    content = outTpl.ToString();
                }
                else
                {
                    content = cache.tpl.LoadExample(key).ToString();
                }

                this.content = content;
            }
            catch
            {
                MessageBox.Show(I18N("EditorNoExample"));
            }
        }

        bool OnSectionChanged(int curSection)
        {
            if (curSection == preSection)
            {
                // prevent loop infinitely
                return true;
            }

            if (CheckValid())
            {
                SaveChanges();
                preSection = curSection;
                ShowSection();
                container.Update();
            }
            else
            {
                if (Lib.UI.Confirm(I18N("CannotParseJson")))
                {
                    preSection = curSection;
                    ShowSection();
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        List<string> GetExamplesDescription()
        {
            var list = new List<string>();

            var examples = Model.Data.Table.examples;

            if (!examples.ContainsKey(preSection))
            {
                return list;
            }

            foreach (var example in examples[preSection])
            {
                // 0.description 1.keyString
                list.Add(example[0]);
            }

            return list;
        }

        void FormatCurrentContent()
        {
            try
            {
                var json = JToken.Parse(content);
                content = json.ToString();
            }
            catch
            {
                MessageBox.Show(I18N("PleaseCheckConfig"));
            }
        }

        public void DiscardChanges()
        {
            var config = container.config;

            content =
                preSection == 0 ?
                config.ToString() :
                config[sections[preSection]].ToString();
        }

        void SaveChanges()
        {
            var content = JToken.Parse(this.content);

            if (preSection == 0)
            {
                container.config = content as JObject;
                return;
            }

            if (preSection >= separator)
            {
                container.config[sections[preSection]] = content as JArray;
            }
            else
            {
                container.config[sections[preSection]] = content as JObject;
            }
        }

        bool CheckValid()
        {
            try
            {
                JToken.Parse(content);
                return true;
            }
            catch
            {
                return false;
            }
        }

        void BindEditor(Panel container)
        {
            var editor = Lib.UI.CreateScintilla(container);
            this.editor = editor;

            // bind scintilla
            var bs = new BindingSource();
            bs.DataSource = this;
            editor.DataBindings.Add(
                "Text",
                bs,
                nameof(this.content),
                true,
                DataSourceUpdateMode.OnPropertyChanged);
        }
        #endregion
    }
}
