Funcionalidade: Inserir e validar inserção de funcionários

Cenário: Inserir e validar funcionários salvos no banco
	Dado que a base de dados esteja limpa
	E que o usuário esteja autenticado
	E os registros sejam inseridos na tabela 'Employee' da base de dados
		| Name            | Email                      | Active |
		| 'Funcionário 1' | 'funcionario1@empresa.com' | True   |
		| 'Funcionário 2' | 'funcionario2@empresa.com' | True   |
	Então a chamada do tipo 'GET' para o endpoint 'v1/employees' terá o seguinte corpo
		"""
			[
				{
				  "id": 1,
				  "name": "Funcionário 1",
				  "email": "funcionario1@empresa.com",
				  "active": true
				},
				{
				  "id": 2,
				  "name": "Funcionário 2",
				  "email": "funcionario2@empresa.com",
				  "active": true
				}
			]
		"""
	E o código de retorno será '200'