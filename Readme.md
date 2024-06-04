## .NET
Instalar o .net 8, atualmente o sistema está usando a versão 8.0.200
Link para download: https://dotnet.microsoft.com/pt-br/download/dotnet/8.0

## Scripts para criação das tabelas 

```
CREATE TABLE IF NOT EXISTS `usuario_tb` (
	`usuario_id` BINARY(36) NOT NULL,
	`nome` VARCHAR(100) NOT NULL COLLATE 'utf8_general_ci',
	`email` VARCHAR(100) NOT NULL COLLATE 'utf8_general_ci',
	`senha` VARCHAR(1000) NOT NULL COLLATE 'utf8_general_ci',
	`dt_cadastro` DATETIME NOT NULL,
	`dt_atualizacao` DATETIME NULL DEFAULT NULL,
	PRIMARY KEY (`usuario_id`) USING BTREE,
	INDEX `usuario_id` (`usuario_id`) USING BTREE
)
COLLATE='utf8_general_ci' ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS `cliente_tb` (
	`cliente_id` BINARY(36) NOT NULL,
	`nome` VARCHAR(100) NOT NULL COLLATE 'utf8_general_ci',
	`email` VARCHAR(100) NOT NULL COLLATE 'utf8_general_ci',
	`celular` VARCHAR(11) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
	`dt_cadastro` DATETIME NOT NULL,
	`usuario_cadastro_id` BINARY(36) NOT NULL,
	`dt_atualizacao` DATETIME NULL DEFAULT NULL,
	`usuario_atualizacao_id` BINARY(36) NULL DEFAULT NULL,
	PRIMARY KEY (`cliente_id`) USING BTREE,
	INDEX `cliente_id` (`cliente_id`) USING BTREE
)
COLLATE='utf8_general_ci' ENGINE=InnoDB;
```