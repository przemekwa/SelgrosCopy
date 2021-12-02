# Konfiguracja połączenia z API

Aby połączyć się z API, należy:

1. Utworzyć API key na swoim koncie Confluence poprzez link (https://id.atlassian.com/manage-profile/security/api-tokens)
1. Następnie należy ustawić dotnet secret w następującym formacie

```bash
dotnet user-secrets set "Jira:Pass" "userAsEmail:api-Key"
```

- _userAsEmail_ - pełen adres email
- _api-Key_ - klucz api utworzony w kroku 1.

---

# Aktualizacja ścieżek

Aby aplikacja działała poprawnie na Twoim komputerze, musisz zmienić ścieżki w **appsettings.json**:

- _SchemaFilePath_ - lokalizacja pliku Schemy (aktualnie SelgrosPG_Schema3.sql)
- _TranslationsFilePath_ - lokalizacja zrobionych translacji (SelgrosPG_Translations.sql)
- _PolandDestinationPath_ - lokalizacja, w której mają być zapisane paczki dla Polski
- _RomaniaDestinationPath_ - lokalizacja, w której mają być zapisane paczki dla Rumunii
- _RussiaDestinationPath_ - lokalizacja, w której mają być zapisane paczki dla Rosji
- _ArtifactsZipPath_ - lokalizacja pobranej z Team City paczki (z artefaktów)
