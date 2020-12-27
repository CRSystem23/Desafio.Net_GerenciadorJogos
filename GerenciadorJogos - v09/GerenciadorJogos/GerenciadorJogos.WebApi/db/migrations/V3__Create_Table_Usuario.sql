CREATE TABLE IF NOT EXISTS `Usuario` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PessoaId` int NOT NULL,
  `Login` varchar(20) NOT NULL,
  `Senha` varchar(1000) NOT NULL,
  `IsAdmin` bit(1) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `PK_Pessoa_Usuario` (`PessoaId`),
  CONSTRAINT `PK_Pessoa_Usuario` FOREIGN KEY (`PessoaId`) REFERENCES `Pessoa` (`Id`)
);