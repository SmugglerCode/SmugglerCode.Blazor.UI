/*

DocumentCategory: Represent a title that can be displayed as a list. Each title represents one document page or can be used to 
				  create a tree of titles.
                  When IsDocumentationPage is set to true, then it is the title for a documentation page else its just a category 
				  title which can hold multiple sub titles. When no parent is set then the category is the root.

DocumentPage: Is the container for a whole page, the data from a page is stored in other tables, each of them will have a 
			  direct or indirect reference to a documentation page, indirect means that a table will not hold a direct link to 
			  the documentation page, but one of the references with an other table will.

DocumentPagePart: Is a placeholder on the documentation page. It has a row and column number to define the position, as well as 
				  a row span and a column span. It creates the layout of the page. 

DocumentTextPart: Represents a text part.

*/

USE CodeGen_DEV
GO

CREATE SCHEMA Doc

CREATE TABLE Doc.DocumentCategory (
	[Id]					INT IDENTITY(1, 1) NOT NULL,
	[Title]					VARCHAR(200) NOT NULL,
	[ParentId]				INT NULL,
	[IsDocumentationPage]	BIT,

	PRIMARY KEY(Id)
)

CREATE TABLE Doc.DocumentPage (
	[Id]					INT IDENTITY(1, 1) NOT NULL,
	[DocumentCategoryId]	INT NULL,

	PRIMARY KEY(Id),
	FOREIGN KEY(DocumentCategoryId) REFERENCES DocumentCategory(Id)
)

CREATE TABLE Doc.DocumentPagePart(
	[Id]					INT IDENTITY(1, 1) NOT NULL,
	[DocumentPageId]		INT NULL,
	[Row]					SMALLINT NOT NULL,
	[Column]				SMALLINT NOT NULL,
	[RowSpan]				SMALLINT NOT NULL,
	[ColumnSpan]			SMALLINT NOT NULL,
	[Width]					VARCHAR(20) NOT NULL,
	[Height]				VARCHAR(20) NOT NULL,

	PRIMARY KEY(Id),
	FOREIGN KEY(DocumentPageId) REFERENCES DocumentPage(Id)
)

CREATE TABLE Doc.DocumentTextPart(
	[Id]					INT IDENTITY(1, 1) NOT NULL,
	[DocumentPagePartId]	INT NOT NULL,
	[DocumentPageId]		INT NOT NULL,
	[Content]				TEXT,

	PRIMARY KEY(Id),
	FOREIGN KEY(DocumentPagePartId) REFERENCES DocumentPagePart(Id),
	FOREIGN KEY(DocumentPageId) REFERENCES DocumentPage(Id)
)