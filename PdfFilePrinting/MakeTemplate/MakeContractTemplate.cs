using PdfFilePrinting.DocumentService;
using Document = PdfFilePrinting.DocumentService.Document;
using Section = PdfFilePrinting.DocumentService.Section;
using Table = PdfFilePrinting.DocumentService.Table;
using Row = PdfFilePrinting.DocumentService.Row;
using Column = PdfFilePrinting.DocumentService.Column;
using Cell = PdfFilePrinting.DocumentService.Cell;
using MigraDoc.DocumentObjectModel;
//using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using MigraDoc.DocumentObjectModel.Tables;

namespace PdfFilePrinting.MakeTemplate
{

    public static class MakeContractTemplate
    {
        private static Border ClearBorder
        {
            get
            {
                var temp = new Border();
                temp.Clear();
                return temp;
            }
        }
        public static Document Make()
        {
            return new Document()
            {
                Sections = new Section[]
                {
                    new Section()
                    {
                        paragrapfs = new BaseParagraph[]
                        {
                            new Paragrapf()
                            {
                                Bold = true,
                                text = new BaseElement[]
                            {
                                new RawText { TextValue = "Договор о практической подготовке обучающихся в форме практики №" },
                                new InjectElement { Name = "ContractNumber" },
                            }
                            },
                            new Table()
                            {
                                SpaceAfter = 1,
                                Alignment = ParagraphAlignment.Justify,
                                Columns = new Column[] {new Column() { Priorety = 1}, new Column() { Priorety = 1} },
                                Rows = new Row[]
                                {
                                    new Row()
                                    {
                                        Alignment = ParagraphAlignment.Right,
                                        Cells = new Cell[]
                                        {
                                            new Cell()
                                            {
                                                Alignment = ParagraphAlignment.Left,
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue ="г. Томск"
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
                                                        text = new BaseElement[]
                                                        {
                                                            new InjectElement()
                                                            {
                                                                Name = "ContractDate",
                                                                //TextValue = $"{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year}"
                                                            },
                                                            new RawText()
                                                            {
                                                                TextValue =" г."
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            new Paragrapf()
                            {
                                SpaceBefore = 1,
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = ("Федеральное государственное автономное образовательное учреждение высшего образования «Томский государственный университет систем управления и радиоэлектроники» (ТУСУР), именуемое в дальнейшем «Университет», в лице директора центра карьеры И.А. Трубчениновой, действующего на основании доверенности от 19.09.2024 №20/3460, с одной стороны, и " )},
                                    new InjectElement() {Name = "FactoryName" }, //TextValue="ООО ДИВИЛАЙН" } ,
                                    new RawText {TextValue = (" именуемое в дальнейшем «Профильная организация», в лице действующего на основании ")},
                                    new InjectElement() {Name="OrganizationRule" },// TextValue=" Устав организации "},
                                    new RawText() {TextValue = " именуемые по отдельности «Сторона», а вместе «Стороны», заключили настоящий Договор о нижеследующем."}
                                }
                            },
                            new Paragrapf()
                            {
                                SpaceAfter = 1,
                                SpaceBefore = 1,
                                Alignment= ParagraphAlignment.Center,
                                Bold = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "1. Предмет Договора"},
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "1.1 Предметом настоящего Договора является организация практической подготовки в форме практики обучающихся (далее – практика) по направлениям подготовки/специальностям: "},
                                    new InjectElement {Name="DerictionType" },//, TextValue="Информационная безопасность "},
                                }
                            },
                            new Paragrapf()
                            {
                                Tab=true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "1.2 Образовательная программа (программы), при реализации которой (-ых) организуется практика, количество обучающихся, сроки организации практики, согласуются Сторонами и являются неотъемлемой частью настоящего Договора (приложением 1)."}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab=true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "1.3 Реализация практики осуществляется в помещениях профильной организации, перечень которых согласуется Сторонами и является неотъемлемой частью настоящего Договора (приложение 2)."}
                                }
                            },
                            new Paragrapf()
                            {
                                SpaceAfter = 1,
                                SpaceBefore =1,
                                Bold = true,
                                Alignment = ParagraphAlignment.Center,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2. Права и обязанности Сторон"}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.1  Университет обязан: "}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab=true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.1.1 Не позднее, чем за 10 рабочих дней до начала практики представить в Профильную организацию поименные списки обучающихся, направляемых для прохождения практики, программу практики, график прохождения практик."}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab=true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.1.2 Назначить руководителя по практической подготовке от Университета, который: "}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab=true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "– обеспечивает организацию практики обучающихся;"}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab=true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "– контролирует участие обучающихся в выполнении определенных видов работ, связанных с будущей профессиональной деятельностью;"}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab=true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "– оказывает методическую помощь обучающимся при выполнении определенных видов работ, связанных с будущей профессиональной деятельностью;"}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab=true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "– несет ответственность совместно с руководителем практики от профильной организации за реализацию программы практики, за жизнь и здоровье обучающихся и работников Университета, соблюдение ими правил противопожарной безопасности, правил охраны труда, техники безопасности и санитарно-эпидемиологических правил и гигиенических нормативов."}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "– несет ответственность совместно с руководителем практики от профильной организации за реализацию программы практики, за жизнь и здоровье обучающихся и работников Университета, соблюдение ими правил противопожарной безопасности, правил охраны труда, техники безопасности и санитарно-эпидемиологических правил и гигиенических нормативов."}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.1.3 При смене руководителя практики в 7-дневный срок сообщить об этом Профильной организации."}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.1.4 Направить обучающихся в Профильную организацию для освоения программы практики."}
                                }
                            },
                            new Paragrapf()
                            {
                                SpaceAfter = 1,
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.1.5 Расследовать и учитывать несчастные случаи, если они произойдут со обучающимися в период прохождения практической подготовки на территории профильной организации."}
                                }
                            },
                            new Paragrapf()
                            {
                                SpaceBefore = 1,
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.2  Профильная организация обязана: "}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.2.1 Создать условия для реализации программы практики, предоставить оборудование и технические средства обучения в объеме, позволяющем выполнять определенные виды работ, связанные с будущей профессиональной деятельностью обучающихся."}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.2.2 Назначить руководителем практики от профильной организации "},
                                    new InjectElement {Name="FactoryLeaderName", TextValue="Иванов Иван Иванович"},
                                    new RawText {TextValue = ", соответствующего требованиям трудового законодательства Российской Федерации о допуске к педагогической деятельности (Приложение 3). "}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.2.3 При смене лица руководителя практики от профильной организации в 7-дневный срок сообщить об этом Университету."}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.2.4 Обеспечить реализацию программы практики со стороны профильной организации;"}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.2.5 Обеспечить безопасные условия реализации программы практики, выполнение правил противопожарной безопасности, правил охраны труда, техники безопасности и санитарно-эпидемиологических правил и гигиенических нормативов;"}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.2.6 Проводить оценку условий труда на рабочих местах, используемых при реализации программы практики;"}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.2.7 Ознакомить обучающихся с правилами внутреннего трудового распорядка профильной организации и иными необходимыми локальными нормативными актами;"}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.2.8 Провести инструктаж обучающихся по охране труда и технике безопасности"}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.2.9 Предоставить обучающимся и руководителю практики от Университета возможность пользоваться помещениями Профильной организации, согласованными Сторонами (приложение 2 к настоящему Договору), а также находящимися в них оборудованием и техническими средствами обучения."}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.2.10 Обо всех случаях нарушения обучающимися правил внутреннего трудового распорядка, охраны труда и техники безопасности сообщить руководителю практики от университета;"}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.2.11 Расследовать и учитывать несчастные случаи, если они произойдут с обучающимися в период прохождения практики в Профильной организации в соответствии с Положением о расследовании и учёте несчастных случаев на производстве."}
                                }
                            },
                            new Paragrapf()
                            {
                                SpaceAfter = 1,
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.2.12 Не допускать использования обучающихся на должностях, не предусмотренных программой практики и не имеющих отношения к направлению подготовки/специальности обучающихся."}
                                }
                            },
                            new Paragrapf()
                            {
                                SpaceBefore = 1,
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "Университет имеет право:"}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.3.1 Требовать от обучающихся соблюдения правил внутреннего трудового распорядка, охраны труда и техники безопасности, режима конфиденциальности, принятого в профильной организации, предпринимать необходимые действия, направленные на предотвращение ситуации, способствующей разглашению конфиденциальной информации."}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "2.3.2 В случае установления факта нарушения обучающимися своих обязанностей в период организации практики, режима конфиденциальности приостановить реализацию программы практики в отношении конкретного обучающегося."}
                                }
                            },
                            new Paragrapf()
                            {
                                SpaceBefore = 1,
                                SpaceAfter = 1,
                                Bold = true,
                                Alignment= ParagraphAlignment.Center,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "3 Срок действия договора"}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "3.1 Настоящий Договор вступает в силу после его подписания и действует до полного исполнения Сторонами обязательств."}
                                }
                            },
                            new Paragrapf()
                            {
                                SpaceBefore = 1,
                                SpaceAfter = 1,
                                Bold = true,
                                Alignment = ParagraphAlignment.Center,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "4. Заключительные положения"}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "4.1 Все споры, возникающие между Сторонами по настоящему Договору, разрешаются Сторонами в порядке, установленном законодательством Российской Федерации."}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "4.2 Изменение настоящего Договора осуществляется по соглашению Сторон в письменной форме в виде дополнительных соглашений к настоящему Договору, которые являются его неотъемлемой частью."}
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "4.3 Настоящий Договор составлен в двух экземплярах, по одному для каждой из Сторон. Все экземпляры имеют одинаковую юридическую силу."}
                                }
                            },
                            new Paragrapf()
                            {
                                SpaceBefore = 1,
                                SpaceAfter = 1,
                                Bold = true,
                                Alignment = ParagraphAlignment.Center,
                                KeepWithNext = true,
                                text = new BaseElement[]
                                {
                                    new RawText {TextValue = "5. Адреса, реквизиты и подписи Сторон"}
                                }
                            },
                            new Table()
                            {
                                KeepTogether = true,
                                KeepWithNext = true,
                                Columns = new Column[] {new Column() { Priorety = 1}, new Column() { Priorety = 1 } },
                                Head = new Row()
                                {
                                    Cells = new Cell[]
                                    {
                                        new Cell()
                                        {
                                            Text = new Paragrapf[]
                                            {
                                                new Paragrapf()
                                                    {
                                                        Alignment = ParagraphAlignment.Left,
                                                        KeepWithNext = true,
                                                        Bold = true,
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue = "Университет: "
                                                            }
                                                        }
                                                    }
                                            }
                                        },
                                        new Cell()
                                        {
                                            Text = new Paragrapf[]
                                            {
                                                new Paragrapf()
                                                    {
                                                        Alignment = ParagraphAlignment.Left,
                                                        KeepWithNext = true,
                                                        Bold = true,
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue = "Профильная организация: "
                                                            }
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
                                        KeepWith = 4,
                                        KeepWithNext = true,
                                        Cells = new Cell[]
                                        {
                                            new Cell()
                                            {
                                                KeepWithNext = true,
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        Alignment = ParagraphAlignment.Left,
                                                        KeepWithNext = true,
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue = "Федеральное государственное автономное образовательное учреждение высшего образования «Томский государственный университет систем управления и радиоэлектроники»"
                                                            }
                                                        }
                                                    },
                                                },
                                            },
                                            new Cell()
                                            {
                                                KeepWithNext = true,
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        Alignment = ParagraphAlignment.Left,
                                                        KeepWithNext = true,
                                                        text = new BaseElement[]
                                                        {
                                                            new InjectElement()
                                                            {
                                                                //TextValue = "ООО ДиВиЛайн",
                                                                Name = "FactoryName"
                                                            }
                                                        }
                                                    },
                                                },
                                            }
                                        }
                                    },
                                    new Row()
                                    {
                                        KeepWithNext = true,
                                        Cells = new Cell[]
                                        {
                                            new Cell()
                                            {
                                                KeepWithNext = true,
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        Alignment = ParagraphAlignment.Left,
                                                        KeepWithNext = true,
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                               TextValue = "Адрес: 634050, г. Томск, пр. Ленина, 40"
                                                            }
                                                        }
                                                    },
                                                }
                                            },
                                            new Cell()
                                            {
                                                KeepWithNext = true,
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        Alignment = ParagraphAlignment.Left,
                                                        KeepWithNext = true,
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue = "Адресс: "
                                                            },
                                                            new InjectElement()
                                                            {
                                                                //TextValue = "634050, г. Томск, ул. Вершинина, 36",
                                                                Name = "FactoryLocation"
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    },
                                    new Row()
                                    {
                                        KeepWithNext = true,
                                        Cells = new Cell[]
                                        {
                                            new Cell()
                                            {
                                                KeepWithNext = true,
                                                Text = new Paragrapf[]
                                                {
                                                     new Paragrapf()
                                                    {
                                                        Alignment = ParagraphAlignment.Left,
                                                        KeepWithNext = true,
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                               TextValue = "Директор центра карьеры "
                                                            }
                                                        }
                                                    },
                                                }
                                            },
                                            new Cell()
                                            {
                                                KeepWithNext = true,
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        Alignment = ParagraphAlignment.Left,
                                                        KeepWithNext = true,
                                                        text = new BaseElement[]
                                                        {
                                                            new InjectElement()
                                                            {
                                                                //TextValue = "Директор",
                                                                Name = "FactoryRank"
                                                            }
                                                        }
                                                    },
                                                }
                                            }
                                        }
                                    },
                                    new Row()
                                    {
                                        KeepWithNext = true,
                                        Cells = new Cell[]
                                        {
                                            new Cell()
                                            {
                                                Text = new BaseParagraph[]
                                                {
                                                    new Table()
                                                    {
                                                        Columns = new Column[]
                                                        {
                                                            new Column() {Priorety=1},
                                                            new Column() {Priorety=1},
                                                        },
                                                        Rows = new Row[]
                                                        {
                                                            new Row()
                                                            {

                                                                Cells = new Cell[]
                                                                {
                                                                    new Cell()
                                                                    {
                                                                        Borders = new Borders()
                                                                        {
                                                                            Bottom = new Border()
                                                                            {
                                                                                Visible = true
                                                                            },
                                                                            Top = ClearBorder,
                                                                            Left = ClearBorder,
                                                                            Right = ClearBorder,
                                                                        },
                                                                        Text = new BaseParagraph[]
                                                                        {
                                                                            new Paragrapf()
                                                                        }

                                                                    },
                                                                    new Cell()
                                                                    {
                                                                        MergeDown = 1,
                                                                        Text = new Paragrapf[]
                                                                        {
                                                                            new Paragrapf()
                                                                            {
                                                                                text = new BaseElement[]
                                                                                {
                                                                                    new InjectElement()
                                                                                    {
                                                                                        TextValue = "И.А. Трубчинова",
                                                                                        Name = "CafedralPracticFielderLeader"
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            },
                                                            new Row()
                                                            {
                                                                Cells = new Cell[]
                                                                {
                                                                    new Cell()
                                                                    {
                                                                        Text = new BaseParagraph[]
                                                                        {
                                                                            new Paragrapf()
                                                                            {
                                                                                Alignment = ParagraphAlignment.Center,
                                                                                text = new RawText[]
                                                                                {
                                                                                    new RawText()
                                                                                    {
                                                                                       SpecialLine = true,
                                                                                       SuperScript = true,
                                                                                       Bold = false,
                                                                                       TextValue = "(Подпись)"
                                                                                    }
                                                                                }
                                                                            }
                                                                        }

                                                                    }
                                                                }
                                                            }
                                                        }
                                                    },
                                                }
                                            },
                                            new Cell()
                                            {
                                                Text = new BaseParagraph[]
                                                {
                                                    new Table()
                                                    {
                                                        Columns = new Column[]
                                                        {
                                                            new Column() {Priorety=1},
                                                            new Column() {Priorety=1},
                                                        },
                                                        Rows = new Row[]
                                                        {
                                                            new Row()
                                                            {

                                                                Cells = new Cell[]
                                                                {
                                                                    new Cell()
                                                                    {
                                                                        Borders = new Borders()
                                                                        {
                                                                            Bottom = new Border()
                                                                            {
                                                                                Visible = true
                                                                            },
                                                                            Top = ClearBorder,
                                                                            Left = ClearBorder,
                                                                            Right = ClearBorder

                                                                        },
                                                                        Text = new BaseParagraph[]
                                                                        {
                                                                            new Paragrapf()
                                                                        }

                                                                    },
                                                                    new Cell()
                                                                    {
                                                                        MergeDown = 1,
                                                                        Text = new Paragrapf[]
                                                                        {
                                                                            new Paragrapf()
                                                                            {
                                                                                text = new BaseElement[]
                                                                                {
                                                                                    new InjectElement()
                                                                                    {
                                                                                        //TextValue = "Иванов Иван Иванович",
                                                                                        Name = "FactoryLeaderName"
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            },
                                                            new Row()
                                                            {
                                                                Cells = new Cell[]
                                                                {
                                                                    new Cell()
                                                                    {
                                                                        Text = new BaseParagraph[]
                                                                        {
                                                                            new Paragrapf()
                                                                            {
                                                                                Alignment = ParagraphAlignment.Center,
                                                                                text = new RawText[]
                                                                                {
                                                                                    new RawText()
                                                                                    {
                                                                                       SpecialLine = true,
                                                                                       SuperScript = true,
                                                                                       Bold = false,
                                                                                       TextValue = "(Подпись)"
                                                                                    }
                                                                                }
                                                                            }
                                                                        }

                                                                    }
                                                                }
                                                            }
                                                        }
                                                    },
                                                }
                                            }
                                        }
                                    },
                                    new Row()
                                    {
                                        KeepWithNext = true,
                                        Cells = new Cell[]
                                        {
                                            new Cell()
                                            {
                                                KeepWithNext = true,
                                                KeepTogether = true,
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        Size = 10,
                                                        KeepTogether = true,
                                                        KeepWithNext = true,
                                                        text = new BaseElement[]
                                                        {
                                                             new RawText()
                                                            {
                                                               SpecialLine = true,
                                                               SuperScript = true,
                                                               Bold = false,
                                                               TextValue = "\n(печать университета)"
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                            new Cell()
                                            {
                                                KeepWithNext = true,
                                                KeepTogether = true,
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        Size = 10,
                                                        KeepTogether = true,
                                                        KeepWithNext = true,
                                                        text = new BaseElement[]
                                                        {
                                                             new RawText()
                                                            {
                                                               SpecialLine = true,
                                                               SuperScript = true,
                                                               Bold = false,
                                                               TextValue = "\n(печать проф. организации, при наличии)"
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    },
                    new Section()
                    {
                        paragrapfs = new BaseParagraph[]
                        {
                            HeadOfExch(1),
                            new Paragrapf()
                            {
                                SpaceBefore = 1,
                                SpaceAfter = 1,
                                Alignment = ParagraphAlignment.Center,
                                Bold= true,
                                text = new BaseElement[]
                                {
                                    new RawText()
                                    {
                                        TextValue = "План-график прохождения практической подготовки в форме практики обучающихся в профильной организации"
                                    }
                                }
                            },
                            new Table()
                            {
                                SpaceBefore = 1,
                                Alignment = ParagraphAlignment.Left,
                                TableBorders = new Borders()
                                {
                                    Visible = true,
                                    DistanceFromRight = 0,
                                    DistanceFromLeft = 0,
                                    Bottom = ClearBorder,
                                    Top = ClearBorder,
                                    Right = ClearBorder,
                                    Left = ClearBorder,
                                },
                                Columns = new Column[]
                                {
                                    new Column() {Priorety = 25},
                                    new Column() {Priorety = 25},
                                    new Column() {Priorety = 16},
                                    new Column() {Priorety = 16},
                                    new Column() {Priorety = 18},
                                },
                                Rows = new Row[]
                                {
                                    new Row()
                                    {
                                        Cells = new Cell[]
                                        {
                                            new Cell()
                                            {
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue =  "Об|ра|зо|ва|тель|на|я прог|рам|ма".Replace('|','\u00AD')
                                                            },
                                                        }
                                                    }
                                                }

                                            },
                                            new Cell()
                                            {
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue = "Ком|по|нент(-ы) об|ра|зо|ва|тель|ной прог|рам|мы".Replace('|','\u00AD')
                                                            }
                                                        }
                                                    }
                                                }

                                            },
                                            new Cell()
                                            {
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        Alignment = ParagraphAlignment.Left,
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue = "Ко|ли|чест|во о|бу|ча|ю|щих|ся".Replace('|','\u00AD')
                                                            }
                                                        }
                                                    }
                                                }

                                            },
                                            new Cell()
                                            {
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue =  "Ф.И.О., курс, груп|па".Replace('|','\u00AD')
                                                            }
                                                        }
                                                    }
                                                }

                                            },
                                            new Cell()
                                            {
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue = "Сро|ки ор|га|ни|за|ции прак|ти|чес|кой под|го|тов|ки".Replace('|','\u00AD')
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                    },
                                    new Row()
                                    {
                                        Cells = new Cell[]
                                        {
                                            new Cell()
                                            {
                                                Text = new Paragrapf[]
                                                {
                                                    new MyltiplyParagraph()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new MyltiplyInjectElement()
                                                            {
                                                                //Map = new[] { "Ин|фор|ма|ци|он|ная бе|зо|пас|ность".Replace('|', '\u00AD'), "Бе|зо|пас|ность те|ле|ком|му|ни|ка|ци|он|ных сис|тем".Replace('|','\u00AD')},
                                                                Name = "DerictionType"
                                                            },
                                                        }
                                                    }
                                                }
                                            },
                                            new Cell()
                                            {
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new InjectElement()
                                                            {
                                                                //TextValue =  "Про|из|вод|ствен|ная".Replace('|','\u00AD'),
                                                                Name = "Practic Type"
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                            new Cell()
                                            {
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new InjectElement()
                                                            {
                                                                TextValue = "2",
                                                                Name = "StudentOnFactoryCount"
                                                            },
                                                        }
                                                    }
                                                }
                                            },
                                            new Cell()
                                            {
                                                Text = new Paragrapf[]
                                                {
                                                    new MyltiplyParagraph()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new MyltiplyInjectElement()
                                                            {
                                                                //Map = new[] { "Реп|ни|ков Ни|ки|та Ива|но|вич".Replace('|', '\u00AD'), "Та|та|ри|нов Мак|сим Де|ни|со|вич".Replace('|','\u00AD')},
                                                                Name = "StudentName"
                                                            },
                                                            new RawText
                                                            {
                                                                TextValue = " "
                                                            },
                                                            new InjectElement()
                                                            {
                                                                //TextValue = "3",
                                                                Name = "Curse"
                                                            },
                                                            new RawText
                                                            {
                                                                TextValue = " курс"
                                                            },
                                                            new MyltiplyInjectElement()
                                                            {
                                                                //Map = new[] { "711-1","731-1"},
                                                                Name = "Group"
                                                            },
                                                        }
                                                    }
                                                }

                                            },
                                            new Cell()
                                            {
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue = " "
                                                            },
                                                            new InjectElement()
                                                            {
                                                                //TextValue = " 2 недели ",
                                                                Name = "TimePrepand"
                                                            },
                                                            new RawText()
                                                            {
                                                                TextValue = "дня"
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }

                            },
                            new Paragrapf()
                            {

                            },
                            new Table()
                            {
                                SpaceAfter = 1,
                                Columns = new Column[]
                                {
                                    new Column() {Priorety = 50},
                                    new Column() {Priorety = 25},
                                    new Column() {Priorety = 25},
                                },
                                Rows = new Row[]
                                {
                                    new Row()
                                    {
                                        KeepWith = 2,
                                        Cells = new Cell[]
                                        {
                                            new Cell
                                            {
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        Alignment = ParagraphAlignment.Left,
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue = "Руководитель практики от Университета"
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                            new Cell
                                            {
                                                Text = new BaseParagraph[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue = "______________"
                                                            }
                                                        }
                                                    },
                                                    new Paragrapf()
                                                    {
                                                        Alignment = ParagraphAlignment.Center,
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                SpecialLine = true,
                                                                SuperScript = true,
                                                                Size = 10,
                                                                TextValue = "(Подпись)"
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                            new Cell
                                            {
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new InjectElement()
                                                            {
                                                                Name = "Cafedral Practic Leader",
                                                                //TextValue = "Новохрёстов Алексей Константинович"
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                        }
                                    },
                                    new Row()
                                    {
                                        Cells = new Cell[]
                                        {
                                            new Cell()
                                            {
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {

                                                    }
                                                }
                                            },
                                            new Cell()
                                            {
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {

                                                    }
                                                }
                                            },
                                            new Cell()
                                            {
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {

                                                    }
                                                }
                                            },
                                        }
                                    },
                                    new Row()
                                    {

                                        Cells = new Cell[]
                                        {
                                            new Cell
                                            {
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        Alignment = ParagraphAlignment.Left,
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue = "Руководитель практики от Профильной организации:"
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                            new Cell
                                            {
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue = "______________"
                                                            }
                                                        }
                                                    },
                                                    new Paragrapf()
                                                    {
                                                        Alignment = ParagraphAlignment.Center,
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                SpecialLine = true,
                                                                Size = 10,
                                                                SuperScript = true,
                                                                TextValue = "(Подпись)"
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                            new Cell
                                            {
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new InjectElement()
                                                            {
                                                                Name= "Factory Practic Leader Name",
                                                                //TextValue = "Иванов Иван Иванович"
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                        }
                                    },
                                }
                            },
                            new Paragrapf()
                            {
                                SpaceBefore = 1,
                                SpaceAfter = 1
                            },
                            StampPlace(),
                        }
                    },
                    new Section()
                    {
                        paragrapfs = new BaseParagraph[]
                        {
                            HeadOfExch(2),
                            new Paragrapf()
                            {
                                SpaceAfter = 1,
                                Bold = true,
                                Alignment = ParagraphAlignment.Center,
                                text = new BaseElement[]
                                {
                                    new RawText()
                                    {
                                        TextValue = "Перечень помещений профильной организации, используемых для реализации практики"
                                    }
                                }
                            },
                            new Table()
                            {
                                TableBorders = new Borders()
                                {
                                    Visible= true,
                                    Bottom = ClearBorder,
                                    Top = ClearBorder,
                                    Right = ClearBorder,
                                    Left = ClearBorder,
                                },
                                Columns = new Column[]
                                {
                                    new Column() {Priorety = 25},
                                    new Column() {Priorety = 25},
                                    new Column() {Priorety = 25},
                                    new Column() {Priorety = 25}
                                },
                                Head = new Row()
                                {
                                    Cells = new Cell[]
                                    {
                                        new Cell()
                                        {
                                            Text = new Paragrapf[]
                                            {
                                                new Paragrapf()
                                                {
                                                    Alignment = ParagraphAlignment.Center,
                                                    text = new RawText[]
                                                    {
                                                        new RawText()
                                                        {
                                                            TextValue = "На|и|ме|но|ва|ние по|ме|ще|ния".Replace('|', '\u00AD')
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                         new Cell()
                                        {
                                            Text = new Paragrapf[]
                                            {
                                                new Paragrapf()
                                                {
                                                    Alignment = ParagraphAlignment.Center,
                                                    text = new RawText[]
                                                    {
                                                        new RawText()
                                                        {
                                                            TextValue = "Ад|рес мес|то|на|хож|де|ния".Replace('|', '\u00AD')
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                          new Cell()
                                        {
                                            Text = new Paragrapf[]
                                            {
                                                new Paragrapf()
                                                {
                                                    Alignment = ParagraphAlignment.Center,
                                                    text = new RawText[]
                                                    {
                                                        new RawText()
                                                        {
                                                            TextValue = "Ком|по|нен|ты об|ра|зо|ва|тель|ной прог|рам|мы, ко|то|рые осущ|ест|вля|ют|ся в дан|ном по|ме|ще|нии\r\n(вид: тип прак|ти|ки)\r\n".Replace('|', '\u00AD')
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                           new Cell()
                                        {
                                            Text = new Paragrapf[]
                                            {
                                                new Paragrapf()
                                                {
                                                    Alignment = ParagraphAlignment.Center,
                                                    text = new RawText[]
                                                    {
                                                        new RawText()
                                                        {
                                                            TextValue = "Пе|ре|чень ма|те|ри|аль|но-|тех|ни|чес|ких средств и прог|рам|мно|го о|бе|спе|че|ния, на|хо|дя|щих|ся в по|ме|ще|ни|и".Replace('|', '\u00AD')
                                                        }
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
                                                Text = new MyltiplyParagraph[]
                                                {
                                                    new MyltiplyParagraph()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new MyltiplyInjectElement()
                                                            {
                                                                Name = "WorksRooms",
                                                                //Map = new[]{"Ауди|то|рия 100".Replace('|', '\u00AD') }
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                            new Cell()
                                            {
                                                Text = new MyltiplyParagraph[]
                                                {
                                                    new MyltiplyParagraph()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new MyltiplyInjectElement()
                                                            {
                                                                Name = "WorkRoomAddress",
                                                                //Map = new[]{ "634050, г. Томск, ул. Вер|ши|ни|на, 36".Replace('|', '\u00AD') }

                                                            },
                                                        }
                                                    }
                                                }
                                            },
                                            new Cell()
                                            {
                                                Text = new MyltiplyParagraph[]
                                                {
                                                    new MyltiplyParagraph()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new MyltiplyInjectElement()
                                                            {
                                                                Name = "Practic Type",
                                                                //Map = new[]{ "Про|из|водст|вен|ная прак|ти|ка".Replace('|', '\u00AD') }
                                                            },
                                                        }
                                                    }
                                                }
                                            },
                                            new Cell()
                                            {
                                                Text = new MyltiplyParagraph[]
                                                {
                                                    new MyltiplyParagraph()
                                                    {
                                                        text = new BaseElement[]
                                                        {
                                                            new MyltiplyInjectElement()
                                                            {
                                                                Name = "Practic Used Tools",
                                                                //Map = new[]{ "Ра|бо|чий компь|ю|тер" }.Select(x=>x.Replace('|', '\u00AD')).ToArray()
                                                            },
                                                        }
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
                            StampPlace(),
                        }
                    },
                    new Section()
                    {
                        paragrapfs = new BaseParagraph[]
                        {
                            HeadOfExch(3),
                            new Paragrapf()
                            {
                                Bold = true,
                                SpaceNum = 3,
                                Alignment= ParagraphAlignment.Center,
                                SpaceAfter = 1,
                                SpaceBefore = 1,
                                text = new BaseElement[]
                                {
                                    new RawText()
                                    {
                                        TextValue = "Требования трудового законодательства Российской Федерации по допуске к педагогической деятельности"
                                    }
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new RawText[]
                                {
                                    new RawText()
                                    {
                                        TextValue = "В соответствии со ст. 331 Трудового кодекса Российской Федерации (далее – ТК РФ) «Право на занятие педагогической деятельностью» к педагогической деятельности допускаются лица, имеющие образовательный ценз, который определяется в порядке, установленном законодательством Российской Федерации в сфере образования."
                                    }
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new RawText[]
                                {
                                    new RawText()
                                    {
                                        TextValue = "К педагогической деятельности не допускаются лица: "
                                    }
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new RawText[]
                                {
                                    new RawText()
                                    {
                                        TextValue = "- лишенные права заниматься педагогической деятельностью в соответствии с вступившим в законную силу приговором суда;"
                                    }
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new RawText[]
                                {
                                    new RawText()
                                    {
                                        TextValue = "- имеющие или имевшие судимость, подвергавшиеся уголовному преследованию (за исключением лиц, уголовное преследование в отношении которых прекращено по реабилитирующим основаниям) за преступления против жизни и здоровья, свободы, чести и достоинства личности (за исключением незаконной госпитализации в медицинскую организацию, оказывающую психиатрическую помощь в стационарных условиях, и клеветы), половой неприкосновенности и половой свободы личности, против семьи и несовершеннолетних, здоровья населения и общественной нравственности, основ конституционного строя и безопасности государства, мира и безопасности человечества, а также против общественной безопасности, за исключением случаев, предусмотренных частью третьей ст. 331 ТК РФ; "
                                    }
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new RawText[]
                                {
                                    new RawText()
                                    {
                                        TextValue = "- имеющие неснятую или непогашенную судимость за иные умышленные тяжкие и особо тяжкие преступления, не указанные в абзаце третьем настоящей части ст. 331 ТК РФ;"
                                    }
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new RawText[]
                                {
                                    new RawText()
                                    {
                                        TextValue = "- признанные недееспособными в установленном федеральным законом порядке;"
                                    }
                                }
                            },
                            new Paragrapf()
                            {
                                Tab = true,
                                text = new RawText[]
                                {
                                    new RawText()
                                    {
                                        TextValue = "- имеющие заболевания, предусмотренные перечнем, утверждаемым федеральным органом исполнительной власти, осуществляющим функции по выработке государственной политики и нормативно-правовому регулированию в области здравоохранения."
                                    }
                                }
                            },
                            new Paragrapf()
                            {
                                SpaceAfter = 1,
                                Tab = true,
                                text = new RawText[]
                                {
                                    new RawText()
                                    {
                                        TextValue = "Лица из числа указанных в абзаце третьем части второй ст. 331 ТК РФ, имевшие судимость за совершение преступлений небольшой тяжести и преступлений средней тяжести против жизни и здоровья, свободы, чести и достоинства личности (за исключением незаконной госпитализации в медицинскую организацию, оказывающую психиатрическую помощь в стационарных условиях, и клеветы), семьи и несовершеннолетних, здоровья населения и общественной нравственности, основ конституционного строя и безопасности государства, мира и безопасности человечества, а также против общественной безопасности, и лица, уголовное преследование в отношении которых по обвинению в совершении этих преступлений прекращено по нереабилитирующим основаниям, могут быть допущены к педагогической деятельности при наличии решения комиссии по делам несовершеннолетних и защите их прав, созданной высшим исполнительным органом государственной власти субъекта Российской Федерации, о допуске их к педагогической деятельности."
                                    }
                                }
                            },
                            StampPlace(),
                        }
                    }
                }

            };
        }

        private static Paragrapf HeadOfExch(int num)
        {
            return new Paragrapf()
            {
                SpaceAfter = 1,
                Alignment = ParagraphAlignment.Right,
                text = new BaseElement[]
                                {
                                    new RawText()
                                    {
                                        TextValue = $"Приложение № {num}\r\nк договору о практической подготовке обучающихся \r\nв форме практики\r\nот "
                                    },
                                    new InjectElement()
                                    {
                                        //TextValue = $"{DateTime.Now.Day.ToString()}.{DateTime.Now.Month.ToString()}.{DateTime.Now.Year.ToString()}",
                                        Name = "ContractDate"
                                    },
                                    new RawText()
                                    {
                                        TextValue = " № "
                                    },
                                    new InjectElement()
                                    {
                                        //TextValue = "1",
                                        Name = "ContractNumber"
                                    },
                }
            };
        }

        private static Table StampPlace()
        {
            return new Table()
            {
                SpaceBefore = 1,
                SpaceAfter = 1,
                KeepTogether = true,
                KeepWithNext = true,
                Columns = new Column[] { new Column() { Priorety = 1 }, new Column() { Priorety = 1 } },
                Rows = new Row[]
                                {
                                    new Row()
                                    {
                                        KeepWith = 3,
                                        KeepWithNext = true,
                                        Cells = new Cell[]
                                        {
                                            new Cell()
                                            {
                                                KeepWithNext = true,
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        KeepWithNext = true,
                                                        Bold = true,
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue = "Университет: "
                                                            }
                                                        }
                                                    },
                                                },
                                            },
                                            new Cell()
                                            {
                                                KeepWithNext = true,
                                                Text = new Paragrapf[]
                                                {

                                                    new Paragrapf()
                                                    {
                                                        KeepWithNext = true,
                                                        Bold = true,
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                                TextValue = "Профильная организация: "
                                                            }
                                                        }
                                                    },

                                                },
                                            }
                                        }
                                    },
                                    new Row()
                                    {
                                        KeepWithNext = true,
                                        Cells = new Cell[]
                                        {
                                            new Cell()
                                            {
                                                KeepWithNext = true,
                                                Text = new Paragrapf[]
                                                {
                                                     new Paragrapf()
                                                    {
                                                        KeepWithNext = true,
                                                        text = new BaseElement[]
                                                        {
                                                            new RawText()
                                                            {
                                                               TextValue = "Директор центра карьеры "
                                                            }
                                                        }
                                                    },
                                                }
                                            },
                                            new Cell()
                                            {
                                                KeepWithNext = true,
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        KeepWithNext = true,
                                                        text = new BaseElement[]
                                                        {
                                                            new InjectElement()
                                                            {
                                                                //TextValue = "Директор",
                                                                Name = "FactoryRank"
                                                            }
                                                        }
                                                    },
                                                }
                                            }
                                        }
                                    },
                                    new Row()
                                    {
                                        KeepWithNext = true,
                                        Cells = new Cell[]
                                        {
                                            new Cell()
                                            {
                                                Text = new BaseParagraph[]
                                                {
                                                    new Table()
                                                    {
                                                        Columns = new Column[]
                                                        {
                                                            new Column() {Priorety=1},
                                                            new Column() {Priorety=1},
                                                        },
                                                        Rows = new Row[]
                                                        {
                                                            new Row()
                                                            {

                                                                Cells = new Cell[]
                                                                {
                                                                    new Cell()
                                                                    {
                                                                        Borders = new Borders()
                                                                        {
                                                                            Bottom = new Border()
                                                                            {
                                                                                Visible = true
                                                                            },
                                                                            Top = ClearBorder,
                                                                            Right = ClearBorder,
                                                                            Left = ClearBorder,
                                                                        },
                                                                        Text = new BaseParagraph[]
                                                                        {
                                                                            new Paragrapf()
                                                                        }

                                                                    },
                                                                    new Cell()
                                                                    {
                                                                        MergeDown = 1,
                                                                        Text = new Paragrapf[]
                                                                        {
                                                                            new Paragrapf()
                                                                            {
                                                                                text = new BaseElement[]
                                                                                {
                                                                                    new InjectElement()
                                                                                    {
                                                                                        //TextValue = "И.А. Трубчинова",
                                                                                        Name = "CafedralPracticFielderLeader"
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            },
                                                            new Row()
                                                            {
                                                                Cells = new Cell[]
                                                                {
                                                                    new Cell()
                                                                    {
                                                                        Text = new BaseParagraph[]
                                                                        {
                                                                            new Paragrapf()
                                                                            {
                                                                                Alignment = ParagraphAlignment.Center,
                                                                                text = new RawText[]
                                                                                {
                                                                                    new RawText()
                                                                                    {
                                                                                       SpecialLine = true,
                                                                                       SuperScript = true,
                                                                                       Bold = false,
                                                                                       TextValue = "(Подпись)"
                                                                                    }
                                                                                }
                                                                            }
                                                                        }

                                                                    }
                                                                }
                                                            }
                                                        }
                                                    },
                                                }
                                            },
                                            new Cell()
                                            {
                                                Text = new BaseParagraph[]
                                                {
                                                    new Table()
                                                    {
                                                        Columns = new Column[]
                                                        {
                                                            new Column() {Priorety=1},
                                                            new Column() {Priorety=1},
                                                        },
                                                        Rows = new Row[]
                                                        {
                                                            new Row()
                                                            {

                                                                Cells = new Cell[]
                                                                {
                                                                    new Cell()
                                                                    {
                                                                        Borders = new Borders()
                                                                        {
                                                                            Bottom = new Border()
                                                                            {
                                                                                Visible = true
                                                                            },
                                                                            Top = ClearBorder,
                                                                            Right = ClearBorder,
                                                                            Left = ClearBorder,
                                                                        },
                                                                        Text = new BaseParagraph[]
                                                                        {
                                                                            new Paragrapf()
                                                                        }

                                                                    },
                                                                    new Cell()
                                                                    {
                                                                        MergeDown = 1,
                                                                        Text = new Paragrapf[]
                                                                        {
                                                                            new Paragrapf()
                                                                            {
                                                                                text = new BaseElement[]
                                                                                {
                                                                                    new InjectElement()
                                                                                    {
                                                                                        //TextValue = "Иванов Иван Иванович",
                                                                                        Name = "FactoryLeaderName"
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            },
                                                            new Row()
                                                            {
                                                                Cells = new Cell[]
                                                                {
                                                                    new Cell()
                                                                    {
                                                                        Text = new BaseParagraph[]
                                                                        {
                                                                            new Paragrapf()
                                                                            {
                                                                                Alignment = ParagraphAlignment.Center,
                                                                                text = new RawText[]
                                                                                {
                                                                                    new RawText()
                                                                                    {
                                                                                       SpecialLine = true,
                                                                                       SuperScript = true,
                                                                                       Bold = false,
                                                                                       TextValue = "(Подпись)"
                                                                                    }
                                                                                }
                                                                            }
                                                                        }

                                                                    }
                                                                }
                                                            }
                                                        }
                                                    },

                                                }
                                            }
                                        }
                                    },
                                    new Row()
                                    {
                                        KeepWithNext = true,
                                        Cells = new Cell[]
                                        {
                                            new Cell()
                                            {
                                                KeepWithNext = true,
                                                KeepTogether = true,
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        Size = 10,
                                                        KeepTogether = true,
                                                        KeepWithNext = true,
                                                        text = new BaseElement[]
                                                        {
                                                             new RawText()
                                                            {
                                                               SpecialLine = true,
                                                               SuperScript = true,
                                                               Bold = false,
                                                               TextValue = "\n(печать университета)"
                                                            }
                                                        }
                                                    }
                                                }
                                            },
                                            new Cell()
                                            {
                                                KeepWithNext = true,
                                                KeepTogether = true,
                                                Text = new Paragrapf[]
                                                {
                                                    new Paragrapf()
                                                    {
                                                        Size = 10,
                                                        KeepTogether = true,
                                                        KeepWithNext = true,
                                                        text = new BaseElement[]
                                                        {
                                                             new RawText()
                                                            {
                                                               SpecialLine = true,
                                                               SuperScript = true,
                                                               Bold = false,
                                                               TextValue = "\n(печать проф. организации, при наличии)"
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
            };
        }
    }

}
