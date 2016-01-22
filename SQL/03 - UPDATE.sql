﻿ALTER TABLE Steps
ADD [Date] DATE NOT NULL;
GO

ALTER TABLE Users
ADD [FirstName] NVARCHAR(50) NOT NULL
ALTER TABLE Users
ADD [LastName] NVARCHAR(50) NOT NULL 
ALTER TABLE Users
ADD [Birthday] DATE NOT NULL
ALTER TABLE Users
ADD [Sexe] BIT NOT NULL DEFAULT(0) 
GO

ALTER TABLE [dbo].[Steps]
ALTER COLUMN [Date] DATETIME NOT NULL
GO

ALTER TABLE [dbo].[Failures]
ALTER COLUMN [Date] DATETIME NOT NULL
GO

-- Ajouter le IDENTITY sur la colonne Id de la table Phones

ALTER TABLE [dbo].[Failures]
DROP COLUMN [Failure_Detail_Id];