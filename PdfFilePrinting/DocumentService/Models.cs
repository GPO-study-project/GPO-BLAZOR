using RenderingDocument = MigraDoc.DocumentObjectModel.Document;
using RenderingSection = MigraDoc.DocumentObjectModel.Section;
using RenderingTable = MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using static MigraDoc.DocumentObjectModel.Text;
using System.ComponentModel;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;
using System.Security.Cryptography;
//using GPO_BLAZOR.Client.Class;

using System.Net.Http.Json;
using System.Net;
using System;

namespace PdfFilePrinting.DocumentService
{

    
    public struct Section
    {
        public Section()
        {

        }

        [XmlElementAttribute(Type = typeof(Paragrapf))]
        [XmlElementAttribute(Type = typeof(MyltiplyParagraph))]
        [XmlElementAttribute(Type = typeof(Table))]
        public BaseParagraph[] paragrapfs { get; set; }

        public async Task Render(RenderingDocument document)
        {
            var section = document.AddSection();
            document.AddStyle("OS TUSUR", "normal");
            section.PageSetup = document.DefaultPageSetup.Clone();
            section.PageSetup.PageFormat = PageFormat.A4;
            foreach (BaseParagraph temp in paragrapfs)
            {
                await temp.Render(section);
            }
        }

        public IEnumerable<(string Name, Func<Task<string>> getter, Func<string, Task> setter)> GetNames()
        {
            var temp = paragrapfs.SelectMany(x => x.GetName());

            return temp;
        }

    }


    [JsonArray]
    [Serializable]
    [JsonDerivedType(typeof(TextFormat), "TextFormat")]
    public abstract class TextFormat
    {

        [XmlAttribute]
        [DefaultValue(14)]
        public int Size { get; set; } = 14;
        [XmlAttribute]
        [DefaultValue(false)]
        public bool Bold { get; set; } = false;

        [XmlAttribute]
        [DefaultValue(Underline.None)]
        public Underline Underline { get; set; } = Underline.None;

        [XmlAttribute]
        [DefaultValue(false)]
        public bool Italic { get; set; } = false;

        [XmlAttribute]
        [DefaultValue(0)]
        public int SpaceBefore { get; set; } = 0;
        [XmlAttribute]
        [DefaultValue(0)]
        public int SpaceAfter { get; set; } = 0;
    }

    [JsonArray]
    [Serializable]
    [JsonDerivedType(typeof(FormatedElement), "FormatedElement")]
    public abstract class FormatedElement: TextFormat
    {
        [XmlAttribute]
        [DefaultValue(ParagraphAlignment.Justify)]
        public ParagraphAlignment Alignment { get => _alignment ?? ParagraphAlignment.Justify; set => _alignment = value; }
        private ParagraphAlignment? _alignment;
        public Borders Borders { get; set; }
        [XmlAttribute]
        [DefaultValue(false)]
        public bool KeepWithNext { get; set; } = false;
        [XmlAttribute]
        [DefaultValue(false)]
        public bool KeepTogether { get; set; } = false;
        [XmlAttribute]
        [DefaultValue(1.5)]
        public double SpaceLine { get; set; } = 1.5;

        [XmlAttribute]
        [DefaultValue(LineSpacingRule.OnePtFive)]
        public LineSpacingRule SpaceLineRule { get; set; } = LineSpacingRule.OnePtFive;

        protected ParagraphFormat SetParametress(ParagraphFormat paragraphFormat)
        {
            paragraphFormat.KeepWithNext = KeepWithNext;
            paragraphFormat.KeepTogether = KeepTogether;
            paragraphFormat.Font = new MigraDoc.DocumentObjectModel.Font("Times New Roman", Size==0?14:Size);
            paragraphFormat.Font.Bold = Bold;
            paragraphFormat.Font.Underline = Underline;
            paragraphFormat.Font.Italic = Italic;
            paragraphFormat.LineSpacing = SpaceLine;
            paragraphFormat.LineSpacingRule = LineSpacingRule.OnePtFive;
            paragraphFormat.SpaceBefore = SpaceBefore * paragraphFormat.Font.Size;
            paragraphFormat.SpaceAfter = SpaceAfter * paragraphFormat.Font.Size;
            if (Borders is null || Borders.BordersCleared)
            {
                paragraphFormat.Borders = new Borders();
            }
            else
            {
                paragraphFormat.Borders = new Borders();
                paragraphFormat.Borders.Visible = Borders.Visible;

                paragraphFormat.Borders.Top.Visible = Borders.Top.Visible;
                paragraphFormat.Borders.Bottom.Visible = Borders.Bottom.Visible;
                paragraphFormat.Borders.Left.Visible = Borders.Left.Visible;
                paragraphFormat.Borders.Right.Visible = Borders.Right.Visible;
            }
            paragraphFormat.Alignment = Alignment;
            return paragraphFormat;
        }
    }

    [JsonArray]
    [Serializable]
    [JsonDerivedType(typeof(Paragrapf), "Paragrapf")]
    [JsonDerivedType(typeof(MyltiplyParagraph), "MyltiplyParagraph")]
    [JsonDerivedType(typeof(Table), "Table")]
    public abstract class BaseParagraph : FormatedElement
    {
        public abstract Task Render(RenderingSection element);
        public abstract Task Render(RenderingTable.Cell element);
        public abstract Task Render(RenderingTable.Cell element, Unit with);

        public abstract IEnumerable<(string Name, Func<Task<string>> getter, Func<string, Task> setter)> GetName();
    }


    [Serializable]
    [JsonDerivedType(typeof(MyltiplyParagraph), "MyltiplyParagraph")]
    [XmlType("Paragrapf")]
    public class Paragrapf: BaseParagraph
    {
        public Paragrapf()
        {

        }
        [XmlElementAttribute(Type = typeof(RawText))]
        [XmlElementAttribute(Type = typeof(MyltiplyInjectElement))]
        [XmlElementAttribute(Type = typeof(InjectElement))]
        public virtual BaseElement[] text { get; set; }
        
        [XmlAttribute]
        [DefaultValue(0)]
        public int SpaceNum { get=>spaceNum; set=>spaceNum=value; }
        private int spaceNum = 0;

        [XmlAttribute]
        [DefaultValue(false)]
        public bool Tab { get => tab; set => tab = value; }
        private bool tab = false;

        public override async Task Render(RenderingSection section)
        {
            var paragraph = section.AddParagraph();
            SetParametress(paragraph.Format);
            paragraph.AddSpace(SpaceNum);
            if (tab) paragraph.AddTab();
            if (text is not null)
                foreach (BaseElement temp in text)
                {
                    await temp.Render(paragraph);
                }
        }

        public override async Task Render(RenderingTable.Cell section)
        {
            var paragraph = section.AddParagraph();

            SetParametress(paragraph.Format);
            paragraph.AddSpace(SpaceNum);
            if (tab) paragraph.AddTab();
            if (text is not null)
                foreach (BaseElement temp in text)
                {
                    await temp.Render(paragraph);
                }
        }
        public override async Task Render(RenderingTable.Cell section, Unit With)
        {
            var paragraph = section.AddParagraph();
            SetParametress(paragraph.Format);
            paragraph.Format.LeftIndent = With;
            paragraph.Format.RightIndent = With;

            paragraph.AddSpace(SpaceNum);
            if (tab) paragraph.AddTab();
            if (text is not null)
                foreach (BaseElement temp in text)
                {
                    await temp.Render(paragraph);
                }
        }

        public override IEnumerable<(string Name, Func<Task<string>> getter, Func<string, Task> setter)> GetName()
        {
            if (text is not null)
            {
                var u = text.Select(x => x.GetName()).Where(x => x is not null).Select(x => x.Value);
                return u;
            }
            else
            {
                return Enumerable.Empty < (string Name, Func<Task<string>> getter, Func<string, Task> setter) > ();
            }
        }
    }

    [XmlType("MyltiplyParagraph")]
    public class MyltiplyParagraph : Paragrapf
    {
        [XmlElementAttribute(Type = typeof(RawText))]
        [XmlElementAttribute(Type = typeof(MyltiplyInjectElement))]
        [XmlElementAttribute(Type = typeof(InjectElement))]
        public override BaseElement[] text { get; set; }
        public override async Task Render(RenderingTable.Cell section)
        {
            var count = text.Select(x => x as MyltiplyInjectElement).Where(x=> x !=null).Select(x=> x.Map = x.Map.Reverse().ToArray()).Max(x=>x.Count());
            //section.MergeDown = count;
            var rowIndex = section.Row.Index;
            var RowCount = section.Table.Rows.Count();
            if (rowIndex+1 == RowCount)
            {
                for (int i = 0; i < section.Table.Columns.Count(); i++)
                    section.Row.Cells[i].MergeDown = count-1;
                var size = Enumerable.Range(0, count-1).ToArray();
                foreach (var x in size)
                    section.Table.AddRow();
            }
            for (int i = count-1; i >= 0; i--)
            {
                
                var temp = i!=0 ? section.Column[rowIndex + i] : section;
                var u = section.Column.Index;
                if (temp is null)
                {
                    var t = section.Table.Rows[rowIndex + i];
                    temp = t[u];                      

                }
                if (temp is not null)
                {
                    if (RowCount > rowIndex + i+1)
                    {
                        var changed = section.Table.Rows[rowIndex + i + 1][u];
                        changed.MergeDown = temp.MergeDown > 0 ? temp.MergeDown - 1 : 0;
                    }
                    temp.MergeDown = 0;
                    await base.Render(temp);
                }
            }
        }

        public override async Task Render(RenderingSection section)
        {
            var count = text.Count(x => x is MyltiplyInjectElement);
            for (int i = 0; i < count; i++)
            {
                await base.Render(section);
            }
        }
    }

    [XmlType("Table")]
    public class Table: BaseParagraph
    {
        public Table()
        {

        }
        public Borders TableBorders { get; set; }
        public Row Head { get; set; }

        [XmlArray]
        public Row[] Rows { get; set; }

        [XmlArray]
        public Column[] Columns { get; set; }

        public override IEnumerable<(string Name, Func<Task<string>> getter, Func<string, Task> setter)> GetName()
        {
            var tempRows = (Rows ?? new Row[] { }).SelectMany(x => x.GetName());
            var tempColumns = Columns.SelectMany(x => x.GetName());
            var tempHead = Head is not null ? Head.GetName() : null;
            var tempsumm = tempRows.Concat(tempColumns);
            if (tempHead is not null)
                return tempsumm.Concat(tempHead);
            return tempsumm;
        }

        public override async Task Render(RenderingSection section)
        {
            var Table = section.AddTable();
            Table.KeepTogether = KeepTogether;

            SetParametress(Table.Format);

            Table = SetBorders(Table);
            Unit width = (Table.Section.PageSetup.PageWidth - Table.Section.PageSetup.LeftMargin - Table.Section.PageSetup.RightMargin) / Columns.Sum(x=>x.Priorety);
            foreach (Column column in Columns)
                await column.Render(Table, width*column.Priorety);
            if (Head is not null)
            {
                Head.Render(Table, true);
            }
            if (Rows is not null)
            foreach (Row row in Rows)
                row.Render(Table);
        }
        public override async Task Render(RenderingTable.Cell section)
        {
            await TableRender (section.AddTextFrame().AddTable(), section.Column.Width);
        }

        public override async Task Render(RenderingTable.Cell section, Unit with)
        {

            var Table = section.AddTextFrame().AddTable();
            await TableRender(Table, with);

        }

        private RenderingTable.Table SetBorders(RenderingTable.Table Table)
        {
            if (TableBorders is not null)
            {
                var a = new Borders();
                var b = TableBorders;
                if (b.Visible)
                    a.Visible = b.Visible;

                if (b.Bottom.Values.BorderCleared.HasValue && ((!b.Bottom.Values.BorderCleared.Value)))
                {
                    a.Bottom = new Border();
                    a.Bottom.Visible = b.Bottom.Visible;
                }
                if (b.Top.Values.BorderCleared.HasValue && (!b.Top.Values.BorderCleared.Value))
                {
                    a.Top = new Border();
                    a.Top.Visible = b.Top.Visible;
                }
                if (b.Left.Values.BorderCleared.HasValue && (!b.Left.Values.BorderCleared.Value))
                {
                    a.Left = new Border();
                    a.Left.Visible = b.Left.Visible;
                }
                if (b.Right.Values.BorderCleared.HasValue && (!b.Right.Values.BorderCleared.Value))
                {
                    a.Right = new Border();
                    a.Right.Visible = b.Right.Visible;
                }
                TableBorders = a;
                ///
                Table.Borders = TableBorders;
            }
            return Table;
        }

        public async Task TableRender (RenderingTable.Table Table, Unit with = default(Unit))
        {
            Table.KeepTogether = KeepTogether;

            SetParametress(Table.Format);

            Table = SetBorders(Table);

            Unit width;
            if (!with.Equals(default(Unit)))
                width = (with) / Columns.Sum(x => x.Priorety);
            else
                width = (Table.Section.PageSetup.PageWidth - Table.Section.PageSetup.LeftMargin - Table.Section.PageSetup.RightMargin) / Columns.Sum(x => x.Priorety);
            foreach (Column column in Columns)
                await column.Render(Table, width * column.Priorety);
            if (Head is not null)
            {
                Head.Render(Table, true);
            }
            if (Rows is not null)
                foreach (Row row in Rows)
                    row.Render(Table);
        }
    }

    public class  Row: FormatedElement
    {
        public Row()
        {

        }
        [XmlAttribute("Count")]
        [DefaultValue(0)]
        public int Count { get; set; }
        
        public Cell[] Cells { get; set; }
        [DefaultValue(0)]
        [XmlAttribute]
        public int KeepWith { get; set; } = 0;
        public virtual async Task Render(RenderingTable.Table section, bool isHead = false)
        {
            var row = section.AddRow();
            row.KeepWith = KeepWith;
            SetParametress(row.Format);
            row.HeadingFormat = isHead;
            if (Cells is not null)
                for (int i = 0; Cells.Length > i; i++)
                {
                    await Cells[i].Render(row.Cells[i]);
                }
        }

        public IEnumerable<(string Name, Func<Task<string>> getter, Func<string, Task> setter)> GetName()
        {
            var temp = Cells.SelectMany(x => x.GetName());
            return temp;
        }
    }




    public class Column: FormatedElement
    {
        public Column()
        {

        }
        [XmlAttribute]
        [DefaultValue(1)]
        public int Priorety { get; set; } = 1;
        public Cell[] Cells { get; set; }

        public async Task Render(RenderingTable.Table section, Unit width, bool isHead = false)
        {
            RenderingTable.Column column;
            var dob = width.Value;
            if (!dob.Equals(double.NaN))
                column = section.AddColumn(width);
            else
                column = section.AddColumn();
            SetParametress(column.Format);
            column.HeadingFormat = isHead;
            if (Cells is not null)
            {
                for (int i = 0; Cells.Length > i; i++)
                    await Cells[i].Render(column[i]);
            }
            return;
        }

        public IEnumerable<(string Name, Func<Task<string>> getter, Func<string, Task> setter)> GetName()
        {
            IEnumerable<(string Name, Func<Task<string>> getter, Func<string, Task> setter)> temp;
            if (Cells is not null)
                temp = Cells.SelectMany(x => x.GetName());
            else
                temp = Enumerable.Empty<(string Name, Func<Task<string>> getter, Func<string, Task> setter)>();
            return temp;
        }
    }

    public class Cell: FormatedElement
    {
        public Cell()
        {

        }
        [XmlAttribute]
        [DefaultValue(0)]
        public int MergeDown { get; set; } = 0;
        [XmlArrayItem("Table", typeof(Table))]
        [XmlArrayItem("Paragrapf", typeof(Paragrapf))]
        [XmlArrayItem("MyltiplyParagraph", typeof(MyltiplyParagraph))]
        public BaseParagraph[] Text { get; set; }
        public async Task Render(RenderingTable.Cell cell)
        {
            if (cell is not null)
            {
                cell.MergeDown = MergeDown;
                SetParametress(cell.Format);
                if (Text is not null)
                foreach (var text in Text)
                    await text.Render(cell);
            }
            return;
        }

        public async Task Render(RenderingTable.Cell cell, Unit with)
        {
            if (cell is not null)
            {
                SetParametress(cell.Format);
                foreach (var text in Text)
                    await text.Render(cell, with);
            }
            return;
        }

        public IEnumerable<(string Name,  Func<Task<string>> getter, Func<string, Task> setter)> GetName()
        {
            var temp = Text.SelectMany(x => x.GetName());
            return temp;
        }

    }



    [JsonDerivedType(typeof(InjectElement), "InjectElement")]
    [JsonDerivedType(typeof(MyltiplyInjectElement), "MyltiplyInjectElement")]
    [JsonDerivedType(typeof(RawText), "Text")]
    public abstract class BaseElement : TextFormat
    {
        public BaseElement()
        {

        }
        [DefaultValue(WordCase.Nominative)]
        [XmlAttribute]
        public WordCase WordCase { get; set; } = WordCase.Nominative;
        [XmlAttribute]
        [DefaultValue(false)]
        public bool SpecialLine { get; set; } = false;
        [XmlAttribute]
        [DefaultValue(false)]
        public bool SuperScript { get; set; } = false;
        [XmlAttribute]
        [DefaultValue(false)]
        public bool SubScript { get; set; } = false;
        public async Task Render(Paragraph paragraph)
        {
            try
            {
                string Text;
                if (WordCase == WordCase.Nominative)
                    Text = _textValue;
                else
                    Text = await ChangeForm(WordCase, _textValue);
                var formatedText = paragraph.AddFormattedText($"{Text}");
                if (SpecialLine)
                {
                    formatedText.Italic = Italic;
                    formatedText.Underline = Underline;
                    formatedText.Bold = Bold;
                    formatedText.Subscript = SubScript;
                    formatedText.Superscript = SuperScript;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Base Element Render -> {ex.Message}");
            }
        }

        protected static async Task<string> ChangeForm(WordCase wordCase, string Name)
        {
            using (HttpClient httpClient = new HttpClient())
            {

                httpClient.BaseAddress = new Uri($"https://localhost:3001/CaseWaord?wordCase={wordCase}&Name={Name}");
                //Console.WriteLine ("Path2: "+ IPaddress.helper + " " + IPaddress.IPAddress);
                using var requestMessage = new HttpRequestMessage(HttpMethod.Get, httpClient.BaseAddress);
  
                var tempresponce = await httpClient.SendAsync(requestMessage);

                return (await tempresponce.Content.ReadFromJsonAsync<string>());
            }

        }

        [XmlIgnore]
        private string _textValue;

        [XmlText]
        public virtual string TextValue
        {
            get 
            {
                return _textValue;
                if (WordCase == WordCase.Nominative)
                    return _textValue;
                var task = ChangeForm(WordCase, _textValue);
                task.Wait();
                if (task.IsCompleted)
                    return task.Result;
                task = ChangeForm(WordCase, _textValue);
                task.Wait();
                return task.Result;
            }
            set
            {
                _textValue = value;
            }
        }

        public abstract (string  Name, Func<Task<string>> getter, Func<string, Task> setter)? GetName();
    }

   public enum WordCase
    {
        Nominative = 0,
        Genitive = 1,
        Dative = 2,
        Accusative = 3,
        Ablative = 4,
        Prepositional = 5
    }

    [JsonDerivedType(typeof(MyltiplyInjectElement), "MyltiplyInjectElement")]
    [XmlType("InjectElement")]
    public class InjectElement: BaseElement
    {
        public InjectElement()
        {

        }

        [XmlAttribute]
        public string Name { get; set; }
        

        public override (string, Func<Task<string>>, Func<string, Task>)? GetName()
        {
            var setter = async () => (TextValue);
            var getter = async (string val) => { TextValue = val; };
            return (Name, setter, getter);
        }

        
    }
    [XmlType("MyltiplyInjectElement")]
    public class MyltiplyInjectElement: InjectElement
    {

        [XmlArray("TextValue")]
        public string[] Map 
        { 
            get => map2 is not null ? map2.ToArray() : null; 
            set{ 
                map = new Stack<string>(value); 
                map2 = new Stack<string>(value); 
            } 
        }
        [XmlIgnore]
        public Stack<string> map;
        [XmlIgnore]
        private Stack<string> map2;

        [XmlIgnore]
        public override string TextValue { get 
            {
                try
                {
                    if (map2 is null)
                        return null;
                    string result;
                    if (map.TryPop(out result))
                    {
                        if (map.Count == 0)
                            map = new Stack<string>(map2.Reverse().ToArray());
                        return result;
                    }
                    else
                    {
                        map = new Stack<string>(map2.ToArray());
                        return map.Pop();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Multiply Inject -> {ex.Message}");
                    return null;
                }
            } 
                set 
                {
                    if (map == null)
                    {
                        map = new Stack<string>();
                        map2 = new Stack<string>();
                    }
                    map.Push(value);
                    map2.Push(value);

                } 
            }
    }
    [XmlType("RawText")]
    public class RawText : BaseElement
    {
        public RawText()
        {

        }

        public override (string, Func<Task<string>>, Func<string, Task>)? GetName()
        {
            return null;
        }

    }
}
