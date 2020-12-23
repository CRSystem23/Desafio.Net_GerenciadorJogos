CREATE DATABASE GerenciadorJogos

USE GerenciadorJogos


CREATE TABLE Pessoa
(
	Id INT IDENTITY(1,1) PRIMARY KEY	,
	Nome VARCHAR(100) NOT NULL			,
	Apelido VARCHAR(50) NULL			,
	Endereco VARCHAR(100) NOT NULL		,
	Celular VARCHAR(11) NOT NULL		,
	Email VARCHAR(100) NULL 
)

CREATE TABLE Usuario
(
	Id INT IDENTITY(1,1) PRIMARY KEY	,
	PessoaId INT NOT NULL				,
	Login VARCHAR(20) NOT NULL			,
	Senha VARCHAR(MAX) NOT NULL			,
	IsAdmin BIT NULL					,
	CONSTRAINT PK_Pessoa_Usuario FOREIGN KEY(PessoaId) REFERENCES Pessoa(Id)
)

CREATE TABLE Jogo
(
	Id INT IDENTITY(1,1) PRIMARY KEY	,
	Nome VARCHAR(100) NOT NULL
)


CREATE TABLE ControleEmprestimoJogo
(
	Id INT IDENTITY(1,1) PRIMARY KEY	,
	PessoaId INT NOT NULL				,
	JogoId INT NOT NULL					,
	DataEmprestimo DATETIME NOT NULL	,
	DataDevolucao DATETIME NULL			,
	CONSTRAINT PK_Pessoa_ControleEmprestimoJogo FOREIGN KEY(PessoaId) REFERENCES Pessoa(Id),
	CONSTRAINT PK_Jogo_ControleEmprestimoJogo FOREIGN KEY(JogoId) REFERENCES Jogo(Id)
)


--Carga Inicial na tabela Pessoa
INSERT INTO Pessoa 
(
	Nome		 ,
	Apelido		 ,
	Endereco	 ,
	Celular		 ,
	Email
)
VALUES
(
	'Administrador do Sistema'									,
	'Admin'														,
	'Rua Imaginaria, 100 Cidadade Satélite, Guarulhos-SP'		,
	'11988440000'												,
	'admin.teste@gmail.com'
),
(
	'Usuário do Sistema'									,
	'User'														,
	'Rua Imaginaria, 100 Cidadade Satélite, Guarulhos-SP'		,
	'11988440000'												,
	'user.teste@gmail.com'
)


--Carga Inicial na tabela Usuario
INSERT INTO Usuario
(
	PessoaId	,
	Login		,
	Senha		,
	IsAdmin		
)
VALUES
(
	1			,
	'admin'	,
	'8D969EEF6ECAD3C29A3A629280E686CFC3F5D5A86AFF3CA122C923ADC6C92'	,
	1
),
(
	2			,
	'user'	,
	'8D969EEF6ECAD3C29A3A629280E686CFC3F5D5A86AFF3CA122C923ADC6C92'	,
	0
)


--Carga Inicial na tabela Jogo
INSERT INTO Jogo
(
	Nome
)
VALUES
('League of Legends')	,
('GTA 5')				,
('Call of Duty')		,
('Fortnite')			,
('Minecraft')			,
('FIFA 20')
