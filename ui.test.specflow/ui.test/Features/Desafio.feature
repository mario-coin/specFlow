#language: pt-br
Funcionalidade: Desafio

Cenário: Acesso o site e verifico que cada produto nas páginas de inventário e item de inventário
	Dado que acesso o site
	Quando informo as seguintes credenciais
		| Username      | Password     |
		| standard_user | secret_sauce |
	E me autentico no sistema
	Entao cada produto estará com o valor da página de inventário igual na sua página de item de inventário