Funcionalidade: Obter todos os funcionários

Cenário: Obter os funcionários sem registros na base
	Dado que a base de dados esteja limpa
    E que o usuário esteja autenticado
    E seja feita uma chamada do tipo 'GET' para o endpoint 'v1/employees'
    Então o código de retorno será '204'