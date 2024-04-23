USE MASTER
GO

IF EXISTS(SELECT * FROM sys.databases WHERE name = 'gstportfoliodb')
  BEGIN
    EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'gstportfoliodb'
        
    ALTER DATABASE [gstportfoliodb] SET SINGLE_USER WITH ROLLBACK IMMEDIATE

    DROP DATABASE [gstportfoliodb]
  END
GO

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'gstportfoliodb')
  BEGIN
    CREATE DATABASE [gstportfoliodb]
  END
GO

USE [gstportfoliodb]
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='USUARIO' AND xtype='U')
  BEGIN
    CREATE TABLE USUARIO 
    (
        codigo_usuario INT IDENTITY (1,1) PRIMARY KEY
        ,nome varchar(50) NOT NULL
        ,email varchar(50) NOT NULL 
        ,tipo int not null
        ,data_insercao DATETIME DEFAULT (GETDATE())
    );
  END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CLIENTE' AND xtype='U')
  BEGIN
    CREATE TABLE CLIENTE
    (
        id_cliente INT IDENTITY (1,1) PRIMARY KEY
        ,nome_cliente varchar(50) NOT NULL
        ,numero_conta varchar(10) NOT NULL
        ,numero_agencia varchar(5) NOT NULL
        ,email varchar(50) NOT NULL
        ,tipo_pessoa char NOT NULL
        ,documento varchar(20) NOT NULL
        ,ativo BIT NOT NULL DEFAULT(1)
    )
  END


IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PRODUTO' AND xtype='U')
  BEGIN
    CREATE TABLE PRODUTO
    (
        codigo_produto INT IDENTITY (1,1) PRIMARY KEY
        ,nome VARCHAR (10) NOT NULL
        ,descricao VARCHAR (60) NOT NULL
        ,ativo BIT NOT NULL DEFAULT(1)
        ,data_insercao DATETIME DEFAULT (GETDATE())
        ,data_ultima_atualizacao DATETIME 
        ,codigo_usuario_atualizacao INT FOREIGN KEY REFERENCES USUARIO (codigo_usuario)
    );
  END


IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='OFERTA' AND xtype='U')
  BEGIN
    CREATE TABLE OFERTA
    (
        codigo_oferta               INT   IDENTITY (1,1) PRIMARY KEY
        ,codigo_produto             INT      NOT NULL FOREIGN KEY REFERENCES PRODUTO(codigo_produto)
        ,nome_papel                 varchar(50) NOT NULL
        ,quantidade_disponivel      INT      NOT NULL
        ,quantidade_original        INT      NOT NULL
        ,preco_unitario             FLOAT    NOT NULL
        ,data_vencimento            DATETIME     
        ,ativo                      BIT      NOT NULL DEFAULT (1)
        ,data_insercao              DATETIME DEFAULT (GETDATE())
        ,data_ultima_atualizacao    DATETIME
        ,codigo_usuario_atualizacao INT      FOREIGN KEY REFERENCES USUARIO(codigo_usuario)
    );
  END


IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='OPERACAO' AND xtype='U')
  BEGIN
    CREATE TABLE OPERACAO
    (
        id_operacao INT IDENTITY (1,1) PRIMARY KEY
        ,codigo_oferta INT NOT NULL FOREIGN KEY REFERENCES OFERTA(codigo_oferta)
        ,codigo_produto             INT      NOT NULL FOREIGN KEY REFERENCES PRODUTO(codigo_produto)
        ,tipo_evento INT NOT NULL
        ,quantidade_operacao INT NOT NULL
        ,quantidade_disponivel_estoque INT NOT NULL
        ,valor_preco_unitario FLOAT NOT NULL
        ,valor_total_operacao FLOAT NOT NULL
        ,status INT NOT NULL
        ,data_operacao DATETIME DEFAULT (GETDATE())
        ,id_cliente INT NOT NULL FOREIGN KEY REFERENCES CLIENTE(ID_CLIENTE)
    )
  END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='POSICAO_CLIENTE' AND xtype='U')
  BEGIN
	CREATE TABLE POSICAO_CLIENTE (
		codigo_posicao_cliente INT IDENTITY (1,1) PRIMARY KEY
		,codigo_cliente INT NOT NULL FOREIGN KEY REFERENCES CLIENTE(ID_CLIENTE)
		,nome_cliente VARCHAR(50) NOT NULL
		,codigo_oferta INT NOT NULL FOREIGN KEY REFERENCES OFERTA(codigo_oferta)
		,nome_papel VARCHAR(50) NOT NULL
		,quantidade INT NOT NULL
		,valor_preco_unitario FLOAT NOT NULL
		,valor_total_operacao FLOAT NOT NULL
		,data_vencimento DATE NOT NULL
	)
  END


IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='JOBS' AND xtype='U')
  BEGIN
	CREATE TABLE JOBS (
		codigo_job INT IDENTITY (1,1) PRIMARY KEY
		, sigla VARCHAR(50) NOT NULL
		, descricao VARCHAR(120)
		, inicio INT NOT NULL
		, intervalo_minutos INT NOT NULL
		, ultima_execucao DATETIME
		, proxima_execucao DATETIME
		, tipo_intervalo INT NOT NULL
	)
  END


--Test data
INSERT INTO PRODUTO (nome, descricao, ativo) VALUES ('CDB', 'CERTIFICADO DE DEPOSITO BANCARIO', 1)
GO
INSERT INTO PRODUTO (nome, descricao, ativo) VALUES ('CDI', 'CERTIFICADO DE DEPOSITO INTERBANCARIO', 1)
GO
INSERT INTO PRODUTO (nome, descricao, ativo) VALUES ('LCI', 'LETRA DE CRÉDITO IMOBILIARIO', 1)
GO
INSERT INTO OFERTA (nome_papel, codigo_produto, quantidade_disponivel, quantidade_original,preco_unitario,data_vencimento) VALUES ('CDBTESTE', (SELECT TOP 1 codigo_produto FROM PRODUTO), 100, 100, 50.99, GETDATE() + 30)
GO
INSERT INTO CLIENTE (nome_cliente, numero_conta, numero_agencia, email, tipo_pessoa, documento) values ('GustavoTeste', '0000012345', '00123', 'GustavoTeste@XpInv.com', 'F', '43901846018')
GO
INSERT INTO JOBS (sigla, descricao, inicio, intervalo_minutos, tipo_intervalo) VALUES ('EMAIL_VCTO', 'Comunicacao de vencimento de operacoes', 8, 0, 0)
GO
































  
GO

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoleClaims]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 6/4/2018 10:18:03 PM ******/

GO

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 6/4/2018 10:18:03 PM ******/

GO

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 6/4/2018 10:18:03 PM ******/

GO

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 6/4/2018 10:18:03 PM ******/

GO

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 6/4/2018 10:18:03 PM ******/

GO

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[UserName] [nvarchar](256) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 6/4/2018 10:18:03 PM ******/

GO

GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserTokens]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetRoleClaims_AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetRoleClaims]'))
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetRoleClaims_AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetRoleClaims]'))
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserClaims_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserClaims_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserLogins_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserLogins_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserTokens_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserTokens]'))
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserTokens_AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserTokens]'))
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO