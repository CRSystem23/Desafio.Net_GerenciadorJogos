CREATE TABLE IF NOT EXISTS `ControleEmprestimoJogo` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PessoaId` int NOT NULL,
  `JogoId` int NOT NULL,
  `DataEmprestimo` datetime NOT NULL,
  `DataDevolucao` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `PK_Pessoa_ControleEmprestimoJogo` (`PessoaId`),
  KEY `PK_Jogo_ControleEmprestimoJogo` (`JogoId`),
  CONSTRAINT `PK_Jogo_ControleEmprestimoJogo` FOREIGN KEY (`JogoId`) REFERENCES `Jogo` (`Id`),
  CONSTRAINT `PK_Pessoa_ControleEmprestimoJogo` FOREIGN KEY (`PessoaId`) REFERENCES `Pessoa` (`Id`)
);