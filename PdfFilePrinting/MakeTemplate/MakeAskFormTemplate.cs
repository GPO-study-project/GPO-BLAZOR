using PdfFilePrinting.DocumentService;
using Document = PdfFilePrinting.DocumentService.Document;
using Section = PdfFilePrinting.DocumentService.Section;
using Table = PdfFilePrinting.DocumentService.Table;
using Row = PdfFilePrinting.DocumentService.Row;
using Column = PdfFilePrinting.DocumentService.Column;
using Cell = PdfFilePrinting.DocumentService.Cell;
using MigraDoc.DocumentObjectModel;

namespace PdfFilePrinting.MakeTemplate
{
    public static class MakeAskFormTemplate
    {
        public static Document Make()
        {
            var Document = new Document()
            {
                Sections = new Section[]
                {
                    new Section()
                    {
                        paragrapfs = new BaseParagraph[]
                        {
                            new Paragrapf()
                            {
                                SpaceBefore = 1,
                                Alignment = ParagraphAlignment.Right,
                                text = new BaseElement[]
                                {
                                    new RawText()
                                    {
                                        TextValue = "Заведующему кафедрой "
                                    },
                                    new InjectElement()
                                    {
                                        Name = "Cafedral"
                                    },
                                    new RawText()
                                    {
                                        TextValue = "\n"
                                    },
                                    new InjectElement()
                                    {
                                        WordCase = WordCase.Dative,
                                        Name = "Cafedral Leader"
                                    },
                                    new RawText()
                                    {
                                        TextValue = "\r\nот студента гр. "
                                    },
                                    new InjectElement()
                                    {
                                        Name = "Group"
                                    },
                                    new RawText()
                                    {
                                        TextValue = "\n"
                                    },
                                    new InjectElement()
                                    {
                                        WordCase = WordCase.Genitive,
                                        Name = "StudentName"
                                    },
                                }
                            },
                            new Paragrapf()
                            {
                                SpaceBefore = 1,
                                SpaceAfter = 1,
                                Alignment = ParagraphAlignment.Center,
                                Bold = true,
                                text = new BaseElement[]
                                {
                                    new RawText()
                                    {
                                        TextValue = "Заявление"
                                    }
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                SpaceAfter = 1,
                                SpaceBefore = 1,
                                Alignment = ParagraphAlignment.Justify,
                                text = new BaseElement[]
                                {
                                    new RawText()
                                    {
                                        TextValue = "Прошу направить меня для прохождения "
                                    },
                                    new InjectElement()
                                    {
                                        Name = "Practic Type"
                                    },
                                    new RawText()
                                    {
                                        TextValue = " в профильную организацию "
                                    },
                                    new InjectElement()
                                    {
                                        Name = "FactoryName"
                                    },
                                    new RawText()
                                    {
                                        TextValue = " (адрес: "
                                    },
                                    new InjectElement()
                                    {
                                        Name = "FactoryAdress"
                                    },
                                    new RawText()
                                    {
                                        TextValue = ") с "
                                    },
                                    new InjectElement()
                                    {
                                        //TextValue = $"{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year}",
                                        Name = "StartDate"
                                    },
                                    new RawText()
                                    {
                                        TextValue = " по "
                                    },
                                    new InjectElement()
                                    {
                                        //TextValue = $"{(DateTime.Now.AddDays(14)).Day}.{(DateTime.Now.AddDays(14)).Month}.{(DateTime.Now.AddDays(14)).Year}",
                                        Name = "EndDate"
                                    },
                                }
                            },
                            new Table()
                            {
                                SpaceAfter = 1,
                                SpaceBefore = 1,
                                Columns = new Column[]
                                {
                                    new Column(),
                                    new Column()
                                },
                                Head = new Row()
                                {
                                    Cells = new Cell[]
                                    {
                                        new Cell()
                                        {
                                            Alignment = ParagraphAlignment.Left,
                                            Text = new Paragrapf[]
                                            {
                                                new Paragrapf()
                                                {
                                                    Alignment = ParagraphAlignment.Left,
                                                    text = new BaseElement[]
                                                    {
                                                        new RawText()
                                                        {
                                                            TextValue = "Дата "
                                                        },
                                                        new InjectElement()
                                                        {
                                                            Name = "AskFormTime",
                                                            //TextValue = $"{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year}"
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        new Cell()
                                        {
                                            Alignment = ParagraphAlignment.Right,
                                            Text = new Paragrapf[]
                                            {
                                                new Paragrapf()
                                                {
                                                    Alignment = ParagraphAlignment.Right,
                                                    text = new RawText[]
                                                    {
                                                        new RawText()
                                                        {
                                                            TextValue = "Подпись"
                                                        },
                                                        new RawText()
                                                        {
                                                            TextValue = "\t\t\t",
                                                            SpecialLine = true,
                                                            Underline = Underline.Single
                                                        },

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            new Paragrapf()
                            {
                                SpaceAfter = 1,
                                SpaceBefore = 1,
                            },
                            new Table()
                            {
                                SpaceAfter = 1,
                                SpaceBefore = 1,
                                Columns = new Column[]
                                {
                                    new Column(),
                                    new Column(),
                                    new Column(),
                                },
                                Head = new Row()
                                {
                                    Cells = new Cell[]
                                    {
                                        new Cell()
                                        {
                                            Alignment = ParagraphAlignment.Left,
                                            Text = new Paragrapf[]
                                            {
                                                new Paragrapf()
                                                {
                                                    Alignment = ParagraphAlignment.Left,
                                                    text = new RawText[]
                                                    {
                                                        new RawText()
                                                        {
                                                            TextValue = "Согласовано: "
                                                        },
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },
                                Rows = new Row[]
                                {
                                    new Row()
                                    {
                                        Cells = new Cell[]
                                        {
                                            new Cell()
                                            {
                                                Alignment = ParagraphAlignment.Right,
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        Alignment = ParagraphAlignment.Right,
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue = "Зав. кафедрой "
                                                            },
                                                            new InjectElement()
                                                            {
                                                                Name = "Cafedral"

                                                            },
                                                        }
                                                    }
                                                }
                                            },
                                            new Cell()
                                            {
                                                Borders = new Borders()
                                                {
                                                    Bottom = new Border()
                                                    {
                                                        Visible = true,
                                                    }
                                                },
                                                Text = new BaseParagraph[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue = "\t"
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                            new Cell()
                                            {
                                                Text = new BaseParagraph[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new InjectElement()
                                                            {
                                                                Name = "Cafedral Leader"
                                                            },
                                                        }
                                                    }
                                                }
                                            }

                                        },

                                    },
                                    new Row()
                                    {
                                        Cells = new Cell[]
                                        {
                                            new Cell()
                                            {
                                                Alignment = ParagraphAlignment.Right,
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        Alignment = ParagraphAlignment.Right,
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue = "Руководитель практики от университета "
                                                            },
                                                            new InjectElement()
                                                            {
                                                                Name = "Cafedral"

                                                            },
                                                        }
                                                    }
                                                }
                                            },
                                            new Cell()
                                            {
                                                Borders = new Borders()
                                                {
                                                    Bottom = new Border()
                                                    {
                                                        Visible = true,
                                                    }
                                                },
                                                Text = new BaseParagraph[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue = "\t"
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                            new Cell()
                                            {
                                                Text = new BaseParagraph[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new InjectElement()
                                                            {
                                                                Name = "Cafedral Practic Leader"
                                                            },
                                                        }
                                                    }
                                                }
                                            }

                                        },

                                    }
                                },

                            }
                        }
                    }
                }
            };
            return Document;
        }
    }
}
