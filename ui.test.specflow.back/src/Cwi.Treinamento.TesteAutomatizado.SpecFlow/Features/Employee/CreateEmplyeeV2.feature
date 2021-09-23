Funcionalidade: Criar funcionário com steps genéricos

Cenário: Criação de funcionário com sucesso
	Dado que a base de dados esteja limpa
	E que o usuário esteja autenticado
	E seja feita uma chamada do tipo 'POST' para o endpoint 'v1/employees' com o corpo da requisição
		"""
		    {
		        "name": "<Name>",
		        "email": "funcionario1@empresa.com"
		    }
		"""
	Então o código de retorno será '201'
	E o registro estará disponível na tabela 'Employee' da base de dados
		| Id | Name     | Email                      | Active |
		| 1  | '<Name>' | 'funcionario1@empresa.com' | True   |

	Exemplos:
		| Name            |
		| Funcionário 2 |