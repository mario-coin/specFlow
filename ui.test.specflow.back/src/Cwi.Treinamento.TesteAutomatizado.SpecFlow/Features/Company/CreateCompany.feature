Funcionalidade: Criar empresa

Cenário: Criação de empresa com sucesso
	Dado que a base de dados esteja limpa
	E que o usuário esteja autenticado
	E seja feita uma chamada do tipo 'POST' para o endpoint 'v1/companies' com o corpo da requisição
		"""
		    {
		        "name": "<Name>",
		        "code": "001",
		        "maxEmployeesNumber": 5
		    }
		"""
	Então o código de retorno será '201'
	E o registro estará disponível na tabela 'Company' da base de dados
		| Id | Name     | Code  | Active | MaxEmployeesNumber |
		| 1  | '<Name>' | '001' | True   | 5                  |

	Exemplos:
		| Name      |
		| Empresa 1 |