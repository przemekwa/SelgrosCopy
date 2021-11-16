# Konfiguracja połączenia z API

Aby połączyć się z API, należy:

* Utworzyć API key na swoim koncie Confluence poprzez link (https://id.atlassian.com/manage-profile/security/api-tokens)
* Następnie należy ustawić dotnet secret w następującym formacie 

`dotnet user-secrets set "Jira:Pass" "userAsEmail:api-Key"`


### userAsEmail - pełen adres email

### api-Key - klucz api, który jest przypisany do konta.