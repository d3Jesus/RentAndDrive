﻿-- ACESSOS
INSERT INTO AspNetRoles
VALUES ('CONSULTAR_FUNCIONARIOS','CONSULTAR_FUNCIONARIOS'),
		('REGISTAR_FUNCIONARIOS','REGISTAR_FUNCIONARIOS'),
		('EDITAR_FUNCIONARIOS','EDITAR_FUNCIONARIOS'),
		('ELIMINAR_FUNCIONARIOS','ELIMINAR_FUNCIONARIOS'),
		('GESTAO_DE_CREDENCIAIS_DOS_FUNCIONARIOS','GESTAO_DE_CREDENCIAIS_DOS_FUNCIONARIOS'),
		('REGISTAR_CREDENCIAIS','REGISTAR_CREDENCIAIS'),
		('ACTIVAR_CREDENCIAIS','ACTIVAR_CREDENCIAIS'),
		('DESACTIVAR_CREDENCIAIS','DESACTIVAR_CREDENCIAIS'),
		('CONSULTAR_VIATURAS','GESTAO_DE_VIATURAS'),
		('REGISTAR_VIATURAS','REGISTAR_VIATURAS'),
		('EDITAR_VIATURAS','EDITAR_VIATURAS'),
		('ELIMINAR_VIATURAS','ELIMINAR_VIATURAS'),
		('DISPONIBILIZAR_VIATURAS','DISPONIBILIZAR_VIATURAS'),
		('INDISPONIBILIZAR_VIATURAS','INDISPONIBILIZAR_VIATURAS'),
		('CONSULTAR_MOTORISTAS','CONSULTAR_MOTORISTAS'),
		('CONSULTAR_VIATURAS_ATRIBUIDAS_AOS_MOTORISTAS','CONSULTAR_VIATURAS_ATRIBUIDAS_AOS_MOTORISTAS'),
		('ATRIBUIR_VIATURAS_AOS_MOTORISTAS','ATRIBUIR_VIATURAS_AOS_MOTORISTAS'),
		('REMOVER_VIATURAS_ATRIBUIDAS_AOS_MOTORISTAS','REMOVER_VIATURAS_ATRIBUIDAS_AOS_MOTORISTAS'),
		('CONSULTAR_ALUGUERES','CONSULTAR_ALUGUERES'),
		('REGISTAR_ALUGUER','REGISTAR_ALUGUER'),
		('CANCELAR_ALUGUER','CANCELAR_ALUGUER'),
		('CONSULTAR_ALUGUER_ACTIVO','CONSULTAR_ALUGUER_ACTIVO'),
		('TERMINAR_O_SERVICO_DE_ALUGUER','TERMINAR_O_SERVICO_DE_ALUGUER'),
		('CONSULTAR_CLIENTES','CONSULTAR_CLIENTES'),
		('REGISTAR_CLIENTES','REGISTAR_CLIENTES'),
		('EDITAR_CLIENTES','EDITAR_CLIENTES'),
		('ELIMINAR_CLIENTES','ELIMINAR_CLIENTES'),
		('CONSULTAR_USUARIOS','CONSULTAR_USUARIOS'),
		('GERIR_ACESSOS','GERIR_ACESSOS'),
		('ATRIBUIR_ACESSOS_AOS_UTILIZADORES','ATRIBUIR_ACESSOS_AOS_UTILIZADORES'),
		('REMOVER_ACESSOS_AOS_UTILIZADORES','REMOVER_ACESSOS_AOS_UTILIZADORES'),
		('RESETAR_SENHA_DOS_UTILIZADORES','RESETAR_SENHA_DOS_UTILIZADORES')
GO

alter table DESCRICAO_ACESSOS
alter column Descricao NVARCHAR(100) NOT NULL
GO

INSERT INTO DESCRICAO_ACESSOS
VALUES ('CONSULTAR_FUNCIONARIOS','Permissão Para Consultar Funcionários'),
		('REGISTAR_FUNCIONARIOS','Permissão Para Registar Funcionários'),
		('EDITAR_FUNCIONARIOS','Permissão Para Editar Funcionários'),
		('ELIMINAR_FUNCIONARIOS','Permissão Para Eliminar Funcionários'),
		('GESTAO_DE_CREDENCIAIS_DOS_FUNCIONARIOS','Permissão Para Gerir as Credenciais dos Funcionários'),
		('REGISTAR_CREDENCIAIS','Permissão Para Registar Credenciais'),
		('ACTIVAR_CREDENCIAIS','Permissão Para Activar Credenciais'),
		('DESACTIVAR_CREDENCIAIS','Permissão Para Desactivar Credenciais'),
		('CONSULTAR_VIATURAS','Permissão Para Consultar Viaturas'),
		('REGISTAR_VIATURAS','Permissão Para Registar Viaturas'),
		('EDITAR_VIATURAS','Permissão Para Editar Viaturas'),
		('ELIMINAR_VIATURAS','Permissão Para Eliminar Viaturas'),
		('DISPONIBILIZAR_VIATURAS','Permissão Para Disponibilizar Viaturas'),
		('INDISPONIBILIZAR_VIATURAS','Permissão Para Indisponibilizar Viaturas'),
		('CONSULTAR_MOTORISTAS','Permissão Para Consultar Motoristas'),
		('CONSULTAR_VIATURAS_ATRIBUIDAS_AOS_MOTORISTAS','Permissão Para Consultar Viaturas Atribuídas Aos Motoristas'),
		('ATRIBUIR_VIATURAS_AOS_MOTORISTAS','Permissão Para Atribuir Viaturas Aos Motoristas'),
		('REMOVER_VIATURAS_ATRIBUIDAS_AOS_MOTORISTAS','Permissão Para Remover Viaturas Atribuidas Aos Motoristas'),
		('CONSULTAR_ALUGUERES','Permissão Para Consultar Aluguer'),
		('REGISTAR_ALUGUER','Permissão Para Registar Aluguer'),
		('CANCELAR_ALUGUER','Permissão Para Cancelar Aluguer'),
		('CONSULTAR_ALUGUER_ACTIVO','Permissão Para Consultar Aluguer Activo'),
		('TERMINAR_O_SERVICO_DE_ALUGUER','Permissão Para Terminar Aluguer'),
		('CONSULTAR_CLIENTES','Permissão Para Consultar Clientes'),
		('REGISTAR_CLIENTES','Permissão Para Registar Clientes'),
		('EDITAR_CLIENTES','Permissão Para Editar Clientes'),
		('ELIMINAR_CLIENTES','Permissão Para Eliminar Clientes'),
		('CONSULTAR_USUARIOS','Permissão Para Consultar Usuários'),
		('GERIR_ACESSOS','Permissão Para Gerir Acessos'),
		('ATRIBUIR_ACESSOS_AOS_UTILIZADORES','Permissão Para Atribuir Acessos'),
		('REMOVER_ACESSOS_AOS_UTILIZADORES','Permissão Para Remover Acessos'),
		('RESETAR_SENHA_DOS_UTILIZADORES','Permissão Para Resetar a Senha')
GO

-- 12/01/2022

ALTER VIEW [dbo].[VW_ALUGUER]
AS
SELECT A.Id, C.Nome AS NomeCliente, F.Nome AS NomeFuncionario, CONCAT(Marca.Descricao, ', ', Modelo.Descricao, ', Matricula ', V.Matricula) AS Viatura,
		V.Matricula, ISNULL(M.Nome, '-') AS NomeMotorista, A.DataAluguer, CONCAT(A.PeriodoAluguer, ' ', A.UnidadePeriodo) AS Periodo, A.Estado, A.DataDevolucao, P.ValorPago
FROM ALUGUER A INNER JOIN PAGAMENTO P
	ON A.Id = P.IdAluguer INNER JOIN VIATURA V
	ON A.IdViatura = V.Matricula LEFT OUTER JOIN PESSOA M
	ON A.IdMotorista = M.Id INNER JOIN PESSOA C
	ON A.IdCliente = C.Id INNER JOIN PESSOA F
	ON A.IdFuncionario = F.Id INNER JOIN VIATURA_HELPER Marca
	ON V.Marca = Marca.IdHelper INNER JOIN VIATURA_HELPER Modelo
	ON V.Modelo = Modelo.IdHelper
GO

UPDATE AspNetRoles
SET Name = 'CONSULTAR_VIATURAS'
WHERE Id = 'CONSULTAR_VIATURAS'
GO