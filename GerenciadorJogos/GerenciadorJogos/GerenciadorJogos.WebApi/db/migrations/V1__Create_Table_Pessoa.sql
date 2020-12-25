CREATE TABLE IF NOT EXISTS `Pessoa` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) NOT NULL,
  `Apelido` varchar(50) DEFAULT NULL,
  `Endereco` varchar(100) NOT NULL,
  `Celular` varchar(11) NOT NULL,
  `Email` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`)
); 