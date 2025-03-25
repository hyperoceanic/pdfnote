# Document Writer

Creates a PDF you can use as a template for your e-writer.

Unlike most other PDF generators, PDFNote has the ability tobgenerate PDFs directly, ithout needing to print the via your web browser.

## Buildng and running the application

You need to have the .net compiler installed.

Download the PDFNote source code to a convenient folder.

```
cd PDFNote
cd src
dotnet build
dotnet run --project app sample.yaml
```

This will generate a PDF called sample.pdf with a header, title, unicode-based icon and a blue background.

## Customising your PDF with the Configuration File

Everything that PDFNote does is driven by the values in the configurations file, and you can edit the configuration file to change any of the cover options. Here's an example - the file you get if you specify a config file name 'sample.yaml'.

```yaml
FileName: sample.pdf
Title: Document Title
Reader: RemarkablePaperPro
Orientation: Portrait
Cover:
  Header: Document Header
  Symbol: "\U0001683F"
  Color: 01579B
```

`FileName` is what the PDF is going to be called. If the file exists, it will be overwritten.

`Title` will be used as the document title. If the title is too long it may overflows onto another page, you can remove the symbol (below) by setting it to "" - two speech marks.

`Reader` has two permissible values - RemarkablePaperPro and Remarkable2.

`Orientation` can be either Portrait or Landscape.

The `Cover` values are indented - we plan to support other pages and this is how we lay them out.

`Header` goes at the top of the cover. Handy for things like year, or project name.

`Symbol` is either empty "" or a unicode symbol. Here' we've used a unicode character code, but you can also use the symbol direct, eg "☕" for a coffee cup. You can find all the Unicode symbols here: https://www.unicode.org/charts/ I particulalry like the Symbols and Punctuation section. You can coy them from the pages there and paste them into the YAML file. Note that the symbol might show up a little different in the PDF when you generate it, depending on the fonts etc installed on your computer.

`Color` - the colour of the background. I often use https://html-color.codes/ to browse for colour codes to use. Sometimes a promising colour looks muddy on the Remarkable Paper Pro - just experiment to find one that works for you.

### Note regarding Remarkable 2
I've not tested this software on a Remarkable2, though I have coded it to support the screen resolution listed on Remarkables' web site.

## Credits and Thanks

QuestPDF for the PDF Writer,
YamlDotNet for the YAML parser,
Brian Schwabauer for the https://github.com/brianschwabauer/remarkably-organized library.
