# POC Mapeamento Vault para IConfiguration

## Execução

- Na raiz do projeto, executar o docker-compose: `docker-compose up`
- Em outro terminal, executar o curl para inclusão do secret `my-secret` com o valor `minha-chave-secreta`: `curl --header "X-Vault-Token: root" --request POST --data '{"data": {"value": "minha-senha-secreta"}}' http://localhost:8200/v1/secret/data/my-secret`
- Subir a aplicação e executar o curl que vai obter o valor mapeado para o IConfiguration: `curl localhost:5222/hello`

O resultado deve ser o `minha-chave-secreta`.

