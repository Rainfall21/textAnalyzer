using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalyzer
{
    public partial class TextStringViewModel
    {
        public string Text { get; set; }

        public string vowelCount { get; set; }

        public string wordCount { get; set; }

        public TextStringViewModel(TextStringModel textStringModel, Verificator verificator)
        {
            this.Text = textStringModel.Text;

            this.vowelCount = verificator.vowelsCount(textStringModel.Text);

            this.wordCount = verificator.wordsCount(textStringModel.Text);
        }
    }
    
    public partial class TextStringModel
    {
        public string Text { get; set; }
    }
}

