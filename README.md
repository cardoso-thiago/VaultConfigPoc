curl --header "X-Vault-Token: root" --request POST --data '{"data": {"value": "minha-senha-secreta"}}' http://localhost:8200/v1/secret/data/my-secret