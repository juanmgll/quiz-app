IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'QuizDatabase')
BEGIN
    CREATE DATABASE [QuizDatabase];
END
GO

USE [QuizDatabase]
GO

/****** Object:  Table [dbo].[answer]    Script Date: 9/09/2024 3:07:14 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[answer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[answertext] [varchar](100) NULL,
	[iscorrect] [bit] NULL,
	[questionid] [int] NOT NULL,
 CONSTRAINT [PK_answer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[category]    Script Date: 9/09/2024 3:07:14 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[categoryname] [varchar](50) NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[question]    Script Date: 9/09/2024 3:07:14 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[question](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[categoryid] [int] NOT NULL,
	[title] [varchar](100) NULL,
 CONSTRAINT [PK_question] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/* [FK_answer_question] */

ALTER TABLE [dbo].[answer]  WITH CHECK ADD  CONSTRAINT [FK_answer_question] FOREIGN KEY([questionid])
REFERENCES [dbo].[question] ([id])
GO
ALTER TABLE [dbo].[answer] CHECK CONSTRAINT [FK_answer_question]
GO

/* [FK_question_category] */
ALTER TABLE [dbo].[question]  WITH CHECK ADD  CONSTRAINT [FK_question_category] FOREIGN KEY([categoryid])
REFERENCES [dbo].[category] ([id])
GO
ALTER TABLE [dbo].[question] CHECK CONSTRAINT [FK_question_category]
GO
